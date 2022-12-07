using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Evaluation;

namespace Luna.Compilers.Simulators.Tasks;

public sealed class CopySimulatorsProjectsOutput : Task
{
    public string? Configuration { get; set; }

    public string? TargetFramework { get; set; }

#pragma warning disable CS8618
    [Required]
    public string DestinationDir { get; set; }

    [Required]
    public ITaskItem[] Projects { get; set; }

    [Output]
    public ITaskItem[] ProjectOutputPaths { get; set; }
#pragma warning restore CS8618

    public override bool Execute()
    {
        List<ITaskItem2> outputPaths = new();

        var projectItems = this.Projects
            .Where(proj => proj.MetadataNames.OfType<string>().Contains("ContainsSimulators") &&
                proj.GetMetadata("ContainsSimulators") == "true");
        foreach (var projectItem in projectItems)
        {
            var properties = new Dictionary<string, string>();
            if (projectItem.MetadataNames.OfType<string>().Contains("SetConfiguration") &&
                projectItem.GetMetadata("SetConfiguration") is string setConfiguration &&
                setConfiguration.StartsWith("Configuration="))
            {
                properties.Add("Configuration", setConfiguration.Substring(24));
            }
            else
            {
                properties.Add("Configuration", this.Configuration ?? string.Empty);
            }
            if (projectItem.MetadataNames.OfType<string>().Contains("SetTargetFramework") &&
                projectItem.GetMetadata("SetTargetFramework") is string setTargetFramework &&
                setTargetFramework.StartsWith("TargetFramework="))
            {
                properties.Add("TargetFramework", setTargetFramework.Substring(24));
            }
            else
            {
                properties.Add("TargetFramework", this.TargetFramework ?? string.Empty);
            }

            var collection = new ProjectCollection(properties);
            var project = collection.LoadProject(projectItem.GetMetadata("FullPath"));

            var outputPath = project.ExpandString("$(OutputPath)");
            var item = new TaskItem(outputPath);
            outputPaths.Add(item);

            this.XCopy(new(outputPath), new DirectoryInfo(this.DestinationDir));
        }

        this.ProjectOutputPaths = outputPaths.ToArray();
        return !this.Log.HasLoggedErrors;
    }

    private void XCopy(DirectoryInfo sourceDir, DirectoryInfo targetDir)
    {
        try
        {
            if (!targetDir.Exists) targetDir.Create();
            foreach (var file in sourceDir.GetFiles())
            {
                try
                {
                    file.CopyTo(Path.Combine(targetDir.FullName, file.Name), true);
                }
                catch (Exception _ex)
                {
                    this.Log.LogErrorFromException(_ex);
                    continue;
                }
            }
            foreach (var directory in sourceDir.GetDirectories())
            {
                try
                {
                    this.XCopy(directory, targetDir.CreateSubdirectory(directory.Name));
                }
                catch (Exception _ex)
                {
                    this.Log.LogErrorFromException(_ex);
                    continue;
                }
            }
        }
        catch (Exception ex)
        {
            this.Log.LogErrorFromException(ex);
        }
    }
}
