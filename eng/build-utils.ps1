# Collection of powershell build utility functions that we use across our scripts.

Set-StrictMode -version 2.0
$ErrorActionPreference="Stop"

# Import Arcade functions
. (Join-Path $PSScriptRoot "common\tools.ps1")

$VSSetupDir = Join-Path $ArtifactsDir "VSSetup\$configuration"
$PackagesDir = Join-Path $ArtifactsDir "packages\$configuration"
$PublishDataUrl = "https://raw.githubusercontent.com/qtyi/luna/main/eng/config/PublishData.json"
$ExternalReposDir = Join-Path $RepoRoot ".externalrepos"

$binaryLog = if (Test-Path variable:binaryLog) { $binaryLog } else { $false }
$nodeReuse = if (Test-Path variable:nodeReuse) { $nodeReuse } else { $false }
$properties = if (Test-Path variable:properties) { $properties } else { @() }
$originalTemp = $env:TEMP;

function GetProjectOutputBinary([string]$fileName, [string]$projectName = "", [string]$configuration = $script:configuration, [string]$tfm = "net472", [string]$rid = "", [bool]$published = $false) {
  $projectName = if ($projectName -ne "") { $projectName } else { [System.IO.Path]::GetFileNameWithoutExtension($fileName) }
  $publishDir = if ($published) { "publish\" } else { "" }
  $ridDir = if ($rid -ne "") { "$rid\" } else { "" }
  return Join-Path $ArtifactsDir "bin\$projectName\$configuration\$tfm\$ridDir$publishDir$fileName"
}

function GetPublishData() {
  if (Test-Path variable:global:_PublishData) {
    return $global:_PublishData
  }

  Write-Host "Downloading $PublishDataUrl"
  $content = (Invoke-WebRequest -Uri $PublishDataUrl -UseBasicParsing).Content

  return $global:_PublishData = ConvertFrom-Json $content
}

function GetBranchPublishData([string]$branchName) {
  $data = GetPublishData

  if (Get-Member -InputObject $data.branches -Name $branchName) {
    return $data.branches.$branchName
  } else {
    return $null
  }
}

function GetFeedPublishData() {
  $data = GetPublishData
  return $data.feeds
}

function GetPackagesPublishData([string]$packageFeeds) {
  $data = GetPublishData
  if (Get-Member -InputObject $data.packages -Name $packageFeeds) {
    return $data.packages.$packageFeeds
  } else {
    return $null
  }
}

function GetReleasePublishData([string]$releaseName) {
  $data = GetPublishData

  if (Get-Member -InputObject $data.releases -Name $releaseName) {
    return $data.releases.$releaseName
  } else {
    return $null
  }
}

function Exec-CommandCore([string]$command, [string]$commandArgs, [switch]$useConsole = $true, [switch]$echoCommand = $true) {
  if ($echoCommand) {
    Write-Host "$command $commandArgs"
  }

  if ($useConsole) {
    $exitCode = Exec-Process $command $commandArgs
    if ($exitCode -ne 0) { 
      throw "Command failed to execute with exit code $($exitCode): $command $commandArgs" 
    }
    return
  }

  $startInfo = New-Object System.Diagnostics.ProcessStartInfo
  $startInfo.FileName = $command
  $startInfo.Arguments = $commandArgs

  $startInfo.UseShellExecute = $false
  $startInfo.WorkingDirectory = Get-Location
  $startInfo.RedirectStandardOutput = $true
  $startInfo.CreateNoWindow = $true

  $process = New-Object System.Diagnostics.Process
  $process.StartInfo = $startInfo
  $process.Start() | Out-Null

  $finished = $false
  try {
    # The OutputDataReceived event doesn't fire as events are sent by the 
    # process in powershell.  Possibly due to subtlties of how Powershell
    # manages the thread pool that I'm not aware of.  Using blocking
    # reading here as an alternative which is fine since this blocks 
    # on completion already.
    $out = $process.StandardOutput
    while (-not $out.EndOfStream) {
      $line = $out.ReadLine()
      Write-Output $line
    }

    while (-not $process.WaitForExit(100)) { 
      # Non-blocking loop done to allow ctr-c interrupts
    }

    $finished = $true
    if ($process.ExitCode -ne 0) { 
      throw "Command failed to execute with exit code $($process.ExitCode): $command $commandArgs" 
    }
  }
  finally {
    # If we didn't finish then an error occurred or the user hit ctrl-c.  Either
    # way kill the process
    if (-not $finished) {
      $process.Kill()
    }
  }
}

# Handy function for executing a windows command which needs to go through 
# windows command line parsing.  
#
# Use this when the command arguments are stored in a variable.  Particularly 
# when the variable needs reparsing by the windows command line. Example:
#
#   $args = "/p:ManualBuild=true Test.proj"
#   Exec-Command $msbuild $args
# 
# The -useConsole argument controls if the process should re-use the current
# console for output or return output as a string
function Exec-Command([string]$command, [string]$commandArgs, [switch]$useConsole = $false, [switch]$echoCommand = $true) {
  if ($args -ne "") {
    throw "Extra arguments passed to Exec-Command: $args"
  }
  Exec-CommandCore -command $command -commandArgs $commandArgs -useConsole:$useConsole -echoCommand:$echoCommand
}

# Handy function for executing a dotnet command without having to track down the 
# proper dotnet executable or ensure it's on the path.
function Exec-DotNet([string]$commandArgs = "", [switch]$useConsole = $true, [switch]$echoCommand = $true) {
  if ($args -ne "") {
    throw "Extra arguments passed to Exec-DotNet: $args"
  }
  $dotnet = Ensure-DotNetSdk
  Exec-CommandCore -command $dotnet -commandArgs $commandArgs -useConsole:$useConsole -echoCommand:$echoCommand
}


# Ensure the proper .NET Core SDK is available. Returns the location to the dotnet.exe.
function Ensure-DotnetSdk() {
  $dotnetInstallDir = (InitializeDotNetCli -install:$true)
  $dotnetTestPath = Join-Path $dotnetInstallDir "dotnet.exe"
  if (Test-Path -Path $dotnetTestPath) {
    return $dotnetTestPath
  }

  $dotnetTestPath = Join-Path $dotnetInstallDir "dotnet"
  if (Test-Path -Path $dotnetTestPath) {
    return $dotnetTestPath
  }

  throw "Could not find dotnet executable in $dotnetInstallDir"
}

function Test-LastExitCode() {
  if ($LASTEXITCODE -ne 0) {
    throw "Last command failed with exit code $LASTEXITCODE"
  }
}

# Walks up the source tree, starting at the given file's directory, and returns a FileInfo object for the first .csproj file it finds, if any.
function Get-ProjectFile([object]$fileInfo) {
  Push-Location

  Set-Location $fileInfo.Directory
  try {
    while ($true) {
      # search up from the current file for a folder containing a project file
      $files = Get-ChildItem *.csproj,*.vbproj
      if ($files) {
        return $files[0]
      }
      else {
        $location = Get-Location
        Set-Location ..
        if ((Get-Location).Path -eq $location.Path) {
          # our location didn't change. We must be at the drive root, so give up
          return $null
        }
      }
    }
  }
  finally {
    Pop-Location
  }
}

function Get-VersionCore([string]$name, [string]$versionFile) {
  $name = $name.Replace(".", "")
  $name = $name.Replace("-", "")
  $nodeName = "$($name)Version"
  $x = [xml](Get-Content -raw $versionFile)
  $node = $x.SelectSingleNode("//Project/PropertyGroup/$nodeName")
  if ($node -ne $null) {
    return $node.InnerText
  }

  throw "Cannot find package $name in $versionFile"

}

# Read version configuration of Luna from "eng/Versions.props" and return full version string.
function Get-LunaVersion([bool]$includePrerelease = $true) {
  $x = [xml](Get-Content -Raw (Join-Path $EngRoot "Versions.props"))
  $major = [int]$x.SelectSingleNode("//Project/PropertyGroup/MajorVersion").InnerText
  $minor = [int]$x.SelectSingleNode("//Project/PropertyGroup/MinorVersion").InnerText
  $patch = [int]$x.SelectSingleNode("//Project/PropertyGroup/PatchVersion").InnerText
  $prerelease = $x.SelectSingleNode("//Project/PropertyGroup/PreReleaseVersionLabel").InnerText

  $version = [string]$major + "." + [string]$minor + "." + [string]$patch
  if (-not $includePrerelease) {
    return $version
  }
  elseif ([string]::IsNullOrWhiteSpace($prerelease)) {
    return $version
  }
  elseif ($prerelease -eq "dev") {
    return $version
  }

  # Before 3.5.* prerelese version string of Roslyn ends with '-beta[prerelease].final'.
  # Start from 3.6.* prerelese version string of Roslyn ends with '-[prerelease].final'.
  if (($major -le 3) -and ($minor -le 5)) {
    $prerelease = "beta" + $prerelease
  }
  return $version + "-" + $prerelease + ".final"
}

# Return the version of the NuGet package as used in this repo
function Get-PackageVersion([string]$name) {
  return Get-VersionCore $name (Join-Path $EngRoot "Versions.props")
}

# Locate the directory where our NuGet packages will be deployed.  Needs to be kept in sync
# with the logic in Version.props
function Get-PackagesDir() {
  $d = $null
  if ($env:NUGET_PACKAGES -ne $null) {
    $d = $env:NUGET_PACKAGES
  }
  else {
    $d = Join-Path $env:UserProfile ".nuget\packages\"
  }

  Create-Directory $d
  return $d
}

# Locate the directory of a specific NuGet package which is restored via our main 
# toolset values.
function Get-PackageDir([string]$name, [string]$version = "") {
  if ($version -eq "") {
    $version = Get-PackageVersion $name
  }

  $p = Get-PackagesDir
  $p = Join-Path $p $name.ToLowerInvariant()
  $p = Join-Path $p $version
  return $p
}

function Subst-TempDir() {
  if ($ci) {
    Exec-Command "subst" "T: $TempDir"

    $env:TEMP='T:\'
    $env:TMP='T:\'
  }
}

function Unsubst-TempDir() {
  if ($ci) {
    Exec-Command "subst" "T: /d"

    # Restore the original temp directory
    $env:TEMP=$originalTemp
    $env:TMP=$originalTemp
  }
}

function Prepare-ExternalReposDir() {
  Create-Directory $ExternalReposDir

  @"
<!-- Licensed to the Qtyi under one or more agreements. The Qtyi licenses this file to you under the MIT license. See the LICENSE file in the project root for more information. -->
<Project>
  <!-- This file intentionally left blank to avoid global settings of Luna repository accidentally blend into those of external repositories. -->
</Project>
"@ | Set-Content (Join-Path $ExternalReposDir "Directory.Build.props")

  @"
<!-- Licensed to the Qtyi under one or more agreements. The Qtyi licenses this file to you under the MIT license. See the LICENSE file in the project root for more information. -->
<Project>
  <!-- This file intentionally left blank to avoid global settings of Luna repository accidentally blend into those of external repositories. -->
</Project>
"@ | Set-Content (Join-Path $ExternalReposDir "Directory.Build.targets")

  @"
# This file intentionally left blank to avoid global settings of Luna repository accidentally blend into those of external repositories.
"@ | Set-Content (Join-Path $ExternalReposDir "Directory.Build.rsp")
}

# Download an archive from a GitHub repository.
# Set $branch to specify a certain branch name to search for commit id, or the default branch name if it is empty.
# Set $commitId to specify a certain commit id, or the latest if it is empty.
# Set $force switch to update even if the codebase of $commitId exist.
function Archive-Codebase([string]$repoOwner, [string]$repoName, [string]$branch = "", [string]$commitId = "", [switch]$force) {
  # This function do the actual download job.
  function Download() {
    $ProgressPreference = 'SilentlyContinue' # Don't display the console progress UI.

    # Clean up repository owner directory.
    if (Test-Path $repoDir) {
      Remove-Item -Path $repoDir -Recurse
    }

    $commitFilePath = Join-Path $repoOwnerDir "$commitId.zip"
    if ($force -or -not (Test-Path $commitFilePath)) {
      Create-Directory $repoOwnerDir
      $archiveUrl = "https://github.com/$repoOwner/$repoName/archive/$commitId.zip"
      try {
        Write-Host "Downloading archive from $archiveUrl."
        Invoke-WebRequest $archiveUrl -OutFile $commitFilePath
      }
      catch {
        # Downloading failed.
        Write-Host "Downloading failed with $_."
        if (Test-Path $commitFilePath) {
          Remove-Item -Path $commitFilePath
        }
      }
    }

    try {
      Write-Host "Unzipping $commitFilePath."
      Unzip $commitFilePath $repoOwnerDir
      # Rename directory from $repoName-$commitId to $repoName only (eq to $repoDir).
      Rename-Item -Path (Join-Path $repoOwnerDir "$repoName-$commitId") -NewName $repoName
    }
    catch {
      Write-Host "Unzipping failed with $_."
    }
    finally {
      # Delete zip file downloaded.
      Remove-Item -Path $commitFilePath
    }
  }

  if ($commitId -eq "") {
    while ($true) {
      try {
        Write-Host "Fetching HEAD commit SHA for $repoOwner/$repoName."
        if ($branch -ne "") {
          $commitInfo = ((Invoke-WebRequest -Uri "https://api.github.com/repos/$repoOwner/$repoName/branches/$branch" -UseBasicParsing).Content | ConvertFrom-Json).commit
        }
        else {
          # Set per_page to 1 to avoid exceeding API rate limit.
          $commitInfo = ((Invoke-WebRequest -Uri "https://api.github.com/repos/$repoOwner/$repoName/commits?per_page=1" -UseBasicParsing).Content | ConvertFrom-Json)[0]
        }
        $commitId = $commitInfo.sha
        Write-Host "HEAD commit SHA for $repoOwner/$repoName$(&{ if ($branch -ne '') { '/' + $branch } else { '' } }) is $commitId."
        break
      }
      catch {
        $response = $_.Exception.Response
        if ($response.StatusCode.value__ -eq 403) {
          Write-Host "API rate limit exceeded for current IP."
          if ($null -ne $response.Headers["retry-after"]) {
            $retryAfterSeconds = [int]$response.Headers["retry-after"]
          }
          elseif ($null -ne $response.Headers["x-ratelimit-reset"]) {
            $retryAfterSeconds = [int]$response.Headers["x-ratelimit-reset"] - [int](New-TimeSpan -Start (Get-Date "01/01/1970") -End (Get-Date)).TotalSeconds
          }
          else {
            # Retry after two minutes by default.
            $retryAfterSeconds = 120
          }
          Write-Host "Retry after $retryAfterSeconds seconds."
          Start-Sleep -Seconds $retryAfterSeconds

          continue
        }

        # Throw again if we cannot recover from this situation.
        throw
      }
    }
  }

  $repoOwnerDir = Join-Path $ExternalReposDir $repoOwner
  $repoDir = Join-Path $repoOwnerDir $repoName
  # Get old commit id present in ExternalDirectories.props to check if we need to update.
  $oldCommitId = Get-ExternalDirectoriesCommitId $repoOwner $repoName
  if ($force -or ($null -eq $oldCommitId) -or ($oldCommitId -ne $commitId)) {
    Write-Host "Restoring $repoOwner/$repoName."
    Download
  }
  else {
    Write-Host "$repoOwner/$repoName already been restored."
  }

  Update-ExternalDirectoriesProps $repoOwner $repoName $commitId
}

function Update-ExternalDirectoriesProps([string]$repoOwner, [string]$repoName, [string]$commitId) {
  $propsPath = Join-Path $ExternalReposDir "ExternalDirectories.props"
  if (-not (Test-Path $propsPath)) {
    [xml]$props = New-Object System.Xml.XmlDocument
    $props.AppendChild($props.CreateXmlDeclaration("1.0", "utf-8", $null)) | Out-Null
    $props.AppendChild($props.CreateComment("Licensed to the Qtyi under one or more agreements. The Qtyi licenses this file to you under the MIT license. See the LICENSE file in the project root for more information.")) | Out-Null
    $props.AppendChild($props.CreateElement("Project")) | Out-Null
  }
  else {
    [xml]$props = Get-Content $propsPath
  }

  $propertyGroupLabel = "$repoOwner/$repoName"
  if ($null -eq $props.Project["PropertyGroup"]) {
    $propertyGroup = $null
  }
  else {
    $propertyGroup = ($props.Project.PropertyGroup | Where-Object Label -EQ $propertyGroupLabel)
  }
  if ($null -eq $propertyGroup) {
    $propertyGroup = $props.CreateElement("PropertyGroup")
    $propertyGroup.SetAttribute("Label", $propertyGroupLabel)
    $props.DocumentElement.AppendChild($propertyGroup) | Out-Null
  }

  function ConvertTo-PascalCase([string]$value) {
    $spans = $value -split "-" | ForEach-Object {
      [regex]::Replace($_.ToLower(), "(^|_)(.)", { $args[0].Groups[2].Value.ToUpper()})
    }
    return $spans -join ""
  }

  $propNamePrefix = "$(ConvertTo-PascalCase $repoOwner)$(ConvertTo-PascalCase $repoName)"

  # Repository directory property
  $repoDirPropName = $propNamePrefix + "RepositoryDirectory";
  $repoDir = $propertyGroup[$repoDirPropName]
  if ($null -eq $repoDir) {
    $repoDir = $props.CreateElement($repoDirPropName)
    $propertyGroup.AppendChild($repoDir) | Out-Null
  }
  $repoDir.InnerText = "`$(MSBuildThisFileDirectory)" + (@($repoOwner, $repoName, "") -join [System.IO.Path]::DirectorySeparatorChar)

  # Repository commit ID property
  $repoCommitIdPropName = $propNamePrefix + "RepositoryCommitId";
  $repoCommitId = $propertyGroup[$repoCommitIdPropName]
  if ($null -eq $repoCommitId) {
    $repoCommitId = $props.CreateElement($repoCommitIdPropName)
    $propertyGroup.AppendChild($repoCommitId) | Out-Null
  }
  $repoCommitId.InnerText = $commitId
  
  $props.Save($propsPath)
}

function Get-ExternalDirectoriesCommitId([string]$repoOwner, [string]$repoName) {
  $propsPath = Join-Path $ExternalReposDir "ExternalDirectories.props"
  if (-not (Test-Path $propsPath)) {
    return;
  }
  [xml]$props = Get-Content $propsPath

  $propertyGroupLabel = "$repoOwner/$repoName"
  if ($null -eq $props.Project["PropertyGroup"]) {
    return;
  }
  $propertyGroup = ($props.Project.PropertyGroup | Where-Object Label -EQ $propertyGroupLabel)
  if ($null -eq $propertyGroup) {
    return;
  }

  function ConvertTo-PascalCase([string]$value) {
    $spans = $value -split "-" | ForEach-Object {
      [regex]::Replace($_.ToLower(), "(^|_)(.)", { $args[0].Groups[2].Value.ToUpper()})
    }
    return $spans -join ""
  }

  $propNamePrefix = "$(ConvertTo-PascalCase $repoOwner)$(ConvertTo-PascalCase $repoName)"

  # Repository directory property
  $repoDirPropName = $propNamePrefix + "RepositoryDirectory";
  $repoDirProp = $propertyGroup[$repoDirPropName]
  if ($null -eq $repoDirProp) {
    return;
  }
  if (-not (Test-Path (Join-Path (Join-Path $ExternalReposDir $repoOwner) $repoName))) {
    return;
  }

  # Repository commit ID property
  $repoCommitIdPropName = $propNamePrefix + "RepositoryCommitId";
  $repoCommitIdProp = $propertyGroup[$repoCommitIdPropName]
  if ($null -eq $repoCommitIdProp) {
    return;
  }
  return $repoCommitIdProp.InnerText;
}

# Get source-link GitHub repository location.
# Set $packageVersion to return location for a certain package version, or all locations with version infos if it is empty.
function Get-SourceLink([string]$packageName, [string]$packageVersion = "") {
  function GetSourceLinkFromNuspec([string]$existingVersion) {
    $lowercasePackageName = $packageName.ToLowerInvariant()
    $nuspecUrl = "https://api.nuget.org/v3-flatcontainer/$lowercasePackageName/$existingVersion/$lowercasePackageName.nuspec"
    Write-Host "GET $nuspecUrl"
    $nuspec = [xml](Invoke-RestMethod -Uri $nuspecUrl)
    $repository = $nuspec.package.metadata.repository
    $projectUrl = $nuspec.package.metadata.projectUrl

    if ($null -eq $repository) {
      throw "Nuspec file do not have repository information for $packageName ($packageVersion)."
    }

    if ($repository.type -ne "git") {
      throw "Unsupported repository metadata type '$repository.type'."
    }

    if ($null -ne $repository.url) {
      if ($repository.url -match "github.com/(?<owner>[^/]+)/(?<name>[^/]+?)(\.git)?$") {
        $owner = $Matches.owner
        $name = $Matches.name
      }
      else {
        throw "Unsupported repository git url '" + $repository.url + "'."
      }
    }
    elseif ($null -ne $projectUrl) {
      if (-not $projectUrl -match "github.com/(?<owner>[^/]+)/(?<name>[^/]+)") {
        $owner = $Matches.owner
        $name = $Matches.name
      }
      else {
        throw "Unsupported project url '" + $repository.url + "'."
      }
    }
    else {
      throw "Nuspec file do not have repository information for $packageName ($packageVersion)."
    }
    Write-Host "Repository of $packageName ($packageVersion) is $owner/$name"

    return [PSCustomObject]@{
      Owner = $owner
      Name = $name
      Branch = if ($repository.HasAttribute("branch")) { $repository.branch } else { "" }
      CommitId = if ($repository.HasAttribute("commit")) { $repository.commit } else { "" }
    }
  }

  if ($packageVersion -ne "") {
    return GetSourceLinkFromNuspec $packageVersion
  }

  $registrationUrl = "https://api.nuget.org/v3/registration5-gz-semver2/"+ $packageName.ToLowerInvariant() + "/index.json"
  $registration = (Invoke-WebRequest -Uri $registrationUrl -UseBasicParsing).Content | ConvertFrom-Json

  $list = New-Object System.Collections.Generic.List[PSCustomObject]
  foreach ($itemgroup in $registration.items)
  {
    foreach ($item in $itemgroup.items)
    {
      try {
        $list.Add((GetSourceLinkFromNuspec $item.catalogEntry.version))
      }
      catch {
        Write-Warning $_
      }
    }
  }

  return $list.ToArray()
}
