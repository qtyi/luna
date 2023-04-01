

$RepoRoot = Resolve-Path (Join-Path $PSScriptRoot "..\")
$EngRoot = [System.IO.Path]::Combine($RepoRoot, "eng")
$ExternalReposRoot = [System.IO.Path]::Combine($RepoRoot, ".externalrepos")

function Restore-Repository
{

  [CmdletBinding(PositionalBinding=$false)]
  param (
    [Parameter(Mandatory=$true)][Alias("Root")][string]$ExternalReposRootFolder,
    [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true)][string[]]$Repository,
    [Parameter(ValueFromPipelineByPropertyName=$true)][string]$CommitId,
    [Parameter(ValueFromPipelineByPropertyName=$true)][string]$Branch)

  process
  {
    # $zipUrl - 远程仓库的特定提交的仓库文件压缩包的下载地址。
    # $sourceRootFolder - 远程仓库的特定提交的仓库文件解压地址。
    # $needsUpdate - 是否需要重新下载压缩包并更新所有文件。
    $thisRepoRootFolder = [System.IO.Path]::Combine($ExternalReposRootFolder, [System.IO.Path]::Combine($Repository))
    if ($CommitId -eq "")
    {
      $needsUpdate = $true # 对于远程仓库需要每次都更新。
      if ($Branch -eq "")
      {
        $Branch = "main" # 远程仓库的默认分支名称为“main”。
      }
      $sourceRootFolder = [System.IO.Path]::Combine($thisRepoRootFolder, $Repository[-1] + "-" + $Branch)
      $zipUrl = "https://github.com/" + ($Repository -join "/") + "/archive/refs/heads/" + $Branch + ".zip"
    }
    else
    {
      $sourceRootFolder =[System.IO.Path]::Combine($thisRepoRootFolder, $Repository[-1] + "-" + $CommitId)
      $needsUpdate = !(Test-Path $sourceRootFolder)
      $zipUrl = "https://github.com/" + ($Repository -join "/") + "/archive/" + $CommitId + ".zip"
    }

    # 仓库文件已是最新，无需更新。
    if (!$needsUpdate)
    {
      return $sourceRootFolder
    }

    $downloadCacheFolder = [System.IO.Path]::Combine($thisRepoRootFolder, ".cache")
    $zipPath = [System.IO.Path]::Combine($downloadCacheFolder, [System.IO.Path]::GetFileName($zipUrl))
    # 下载压缩包到指定位置。
    try
    {
      if (!(Test-Path $downloadCacheFolder))
      {
        New-Item $downloadCacheFolder -ItemType Directory
      }
      #Write-Host "正在从远程仓库下载文件……"
      Invoke-WebRequest $zipUrl -OutFile $zipPath
      #Write-Host "从远程仓库下载完成。"

      # 解压下载的压缩包到指定位置。
      try
      {
        # 删除要被替换的目标文件夹。
        if (Test-Path $sourceRootFolder)
        {
          Remove-Item $sourceRootFolder -Recurse
        }
        Write-Debug "正在解压文件……"
        Expand-Archive $zipPath -DestinationPath $thisRepoRootFolder
        Write-Debug "解压完成。"
      }
      catch
      {
        Write-Error ("解压文件时发生错误。" + $_)
        while (Test-Path $sourceRootFolder)
        {
          Remove-Item $sourceRootFolder -Recurse
        }
      }
    }
    catch
    {
      Write-Error ("从远程仓库下载文件时发生错误：" + $_)
    }
    finally
    {
      if (Test-Path $downloadCacheFolder)
      {
        Remove-Item $downloadCacheFolder -Recurse
      }
    }

    if (Test-Path $sourceRootFolder)
    {
      return $sourceRootFolder
    }
    else
    {
      return $null
    }
  }

}

function Update-ExternalDirectoriesProps
{
  [CmdletBinding(PositionalBinding=$false)]
  param(
    [Parameter(Mandatory=$true)][string[]]$Repository,
    [Parameter(Mandatory=$true, ValueFromPipeline=$true)][string]$SourceRootFolder)

  process
  {
    # 不接受错误的地址。
    if ($null -eq $SourceRootFolder)
    {
      return
    }

    $propsPath = [System.IO.Path]::Combine($ExternalReposRoot, "ExternalDirectories.props")
    if (!(Test-Path $propsPath))
    {
      [xml]$props = New-Object System.Xml.XmlDocument
      $props.AppendChild($props.CreateXmlDeclaration("1.0", "utf-8", $null))
      $props.AppendChild($props.CreateComment("Licensed to the Qtyi under one or more agreements. The Qtyi licenses this file to you under the MIT license. See the LICENSE file in the project root for more information."))
      $props.AppendChild($props.CreateElement("Project"))
    }
    else
    {
      [xml]$props = Get-Content $propsPath
    }
    
    $propertyGroupLabel = $Repository -join "/"
    $propertyGroup = ($props.Project.PropertyGroup | Where-Object Label -EQ $propertyGroupLabel)
    if ($null -eq $propertyGroup)
    {
      $propertyGroup = $props.CreateElement("PropertyGroup")
      $propertyGroup.SetAttribute("Label", $propertyGroupLabel)
      $props.DocumentElement.AppendChild($propertyGroup)
    }

    function ConvertTo-PascalCase
    {
        [OutputType("System.String")]
        param(
            [Parameter(Position=0, ValueFromPipeline=$true)]
            [string] $Value
        )

        return [regex]::Replace($Value.ToLower(), "(^|_)(.)", { $args[0].Groups[2].Value.ToUpper()})
    }

    $propNamePrefix = [string]::Concat(($Repository | ForEach-Object { $_ | ConvertTo-PascalCase }))
    $repoDirPropName = $propNamePrefix + "RepositoryDirectory";
    $repoDir = $propertyGroup[$repoDirPropName]
    if ($null -eq $repoDir)
    {
      $repoDir = $props.CreateElement($repoDirPropName)
      $propertyGroup.AppendChild($repoDir)
    }
    $repoDir.InnerText = "`$(MSBuildThisFileDirectory)" + [System.IO.Path]::GetRelativePath($ExternalReposRoot, $SourceRootFolder)

    $props.Save($propsPath)
  }
}

function GetSourceLink-MicrosoftNetCompilersToolset
{

  [CmdletBinding(PositionalBinding=$false)]
  param(
    [Alias("Version")][string]$PackageVersion)

  process
  {
    $registrationUrl = "https://api.nuget.org/v3/registration5-gz-semver2/microsoft.net.compilers.toolset/index.json"
    $registration = Invoke-RestMethod $registrationUrl

    $list = New-Object System.Collections.Generic.List[PSCustomObject]
    foreach ($itemgroup in $registration.items)
    {
      foreach ($item in $itemgroup.items)
      {
        $projectUrl = $item.catalogEntry.projectUrl
        $version = $item.catalogEntry.version
        if ([string]::IsNullOrEmpty($projectUrl))
        {
          Write-Warning ("找不到版本" + $version + "的项目地址。")
          $projectUrl = "https://github.com/dotnet/roslyn"
        }

        if (!($item.catalogEntry.description -match $projectUrl.TrimEnd("/") + "/commit/[0-9A-Za-z]+"))
        {
          Write-Warning ("找不到版本" + $version + "的提交地址，将跳过此版本")
          continue
        }
        $commitUrl = $Matches.0
        Write-Debug ($version + ": " + $commitUrl)

        $entry = [PSCustomObject]@{
          Repository = (New-Object System.Uri -ArgumentList $projectUrl).Segments.ForEach({ $_.Trim("/") }).Where({ ![string]::IsNullOrEmpty($_) })
          CommitId = (New-Object System.Uri -ArgumentList $commitUrl).Segments[-1].Trim("/")
        }
        if ($PackageVersion -eq $version)
        {
          Write-Debug ($PackageVersion + " == " + $version)
          return $entry
        }
        else
        {
          Write-Debug ($PackageVersion + " != " + $version)
          $list.Add($entry)
        }
      }
    }

    return $list.ToArray()
  }

}

function GetVersion-Luna
{
  [xml]$VersionsProps = Get-Content ([System.IO.Path]::Combine($EngRoot, "Versions.props"))
  $properties = $VersionsProps.Project.PropertyGroup
  $major = [int]$properties.MajorVersion[0]
  $minor = [int]$properties.MinorVersion[0]
  $patch = [int]$properties.PatchVersion[0]
  $prerelease = $properties.PreReleaseVersionLabel[0]

  $version = [string]$major + "." + [string]$minor + "." + [string]$patch;
  if ([string]::IsNullOrEmpty($prerelease))
  {
    return $version
  }

  if (($major -le 3) -and ($minor -le 5))
  {
    $prerelease = "beta" + $prerelease
  }
  return $version + "-" + $prerelease + ".final"
}

$repos = @()

# 在下方添加所有要跟踪的源代码链接。
$repos += GetSourceLink-MicrosoftNetCompilersToolset -Version (GetVersion-Luna)

$repos | ForEach-Object { $_ | Restore-Repository -Root $ExternalReposRoot | Update-ExternalDirectoriesProps -Repository $_.Repository | Out-Null }
