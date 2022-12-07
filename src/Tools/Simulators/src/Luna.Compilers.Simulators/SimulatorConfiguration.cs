using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Luna.Compilers.Tools;

#pragma warning disable CS0649
public partial class SimulatorConfiguration
{
    private readonly config _config;

#pragma warning disable CS8981
    private sealed class config
#pragma warning restore CS8981
    {
        public object? paths;
        public IDictionary<string, object?>? extensions;
    }

    public string[] Paths => StringOrArray(this._config.paths);

    public IDictionary<string, string[]> Extensions => this._config.extensions?.ToDictionary(pair => pair.Key, pair => StringOrArray(pair.Value))
        ?? new Dictionary<string, string[]>(0);

    private SimulatorConfiguration(config config) => this._config = config;

    private static string[] StringOrArray(object? obj)
    {
        var emptyArray = Array.Empty<string>();
        if (obj is null)
            return emptyArray;

        if (obj is JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.String)
                return element.GetString() is string value ? new[] { value } : emptyArray;
            else if (element.ValueKind == JsonValueKind.Array)
            {
                var count = element.GetArrayLength();
                List<string> list = new(count);
                for (var i = 0; i < count; i++)
                {
                    var item = element[i];
                    if (item.ValueKind == JsonValueKind.String && item.GetString() is string value)
                        list.Add(value);
                }
                return list.ToArray();
            }
        }

        throw new InvalidCastException();
    }
}
#pragma warning restore CS0649

partial class SimulatorConfiguration
{
    public static SimulatorConfiguration? Deserialize(Stream stream)
    {
        if (stream is null) throw new ArgumentNullException(nameof(stream));

        var doc = JsonDocument.Parse(stream);
        var config = doc.Deserialize<SimulatorConfiguration.config>(new JsonSerializerOptions()
        {
            IncludeFields = true
        });
        return config is not null ? new(config) : null;
    }
}
