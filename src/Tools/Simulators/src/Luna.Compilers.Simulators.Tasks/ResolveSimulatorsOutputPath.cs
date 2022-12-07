using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Luna.Compilers.Simulators.Tasks;

public class ResolveSimulatorsOutputPath : Task
{
#pragma warning disable CS8618
    [Required]
    public string ConfigFile { get; set; }

    [Output]
    public string OutputPath { get; set; }
#pragma warning restore CS8618

    public override bool Execute()
    {
        if (!this.TryGetOutputPath(out var outputPath)) return false;

        this.OutputPath = outputPath;
        return !Log.HasLoggedErrors;
    }

    private bool TryGetOutputPath([NotNullWhen(true)] out string? outputPath)
    {
        outputPath = null;

        if (!File.Exists(this.ConfigFile))
        {
            Log.LogError("无法找到文件“{0}”", this.ConfigFile);
            return false;
        }
        var doc = JsonDocument.Parse(File.ReadAllText(this.ConfigFile));

        if (doc.RootElement.TryGetProperty("paths", out var path))
        {
            if (path.ValueKind == JsonValueKind.Array && path.GetArrayLength() > 0)
                path = path[0];

            if (path.ValueKind == JsonValueKind.String)
                outputPath = path.GetString();
        }
        if (outputPath is null)
        {
            Log.LogError(
                subcategory: null,
                errorCode: "LUNA8001",
                helpKeyword: null,
                file: this.ConfigFile,
                0, 0, 0, 0,
                message: "无法在配置文件中找到输出地址。");
            return false;
        }

        return true;
    }
}
