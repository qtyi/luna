using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Luna.Compilers.Tools;

namespace Luna.Compilers.Simulators;

public static class Simulator
{
    private static readonly Dictionary<(Type type, string languageName), ISimulator> s_simulators = new();
    private static readonly Dictionary<string, HashSet<Type>> s_languageNameMap = new();
    private static readonly Dictionary<string, HashSet<string>> s_fileExtensionMap = new();

    public static bool TryGetLexerSimulatorByLanguageName(string languageName, [NotNullWhen(true)] out ILexerSimulator[]? lexerSimulators) =>
        Simulator.TryGetSimulatorByLanguageName(languageName, out lexerSimulators);

    public static bool TryGetLanguageParserSimulatorByLanguageName(string languageName, [NotNullWhen(true)] out ILanguageParserSimulator[]? languageParserSimulators) =>
        Simulator.TryGetSimulatorByLanguageName(languageName, out languageParserSimulators);

    internal static bool TryGetSimulatorByLanguageName<TSimulator>(string languageName, [NotNullWhen(true)] out TSimulator[]? simulators)
        where TSimulator : ISimulator
    {
        simulators = null;

        if (!s_languageNameMap.TryGetValue(languageName, out var types) || types.Count == 0) return false;

        simulators = types.Select(type =>
        {
            if (!s_simulators.TryGetValue((type, languageName), out var simulator))
            {
                try
                {
                    simulator = Activator.CreateInstance(type) as ISimulator;
                    Debug.Assert(simulator is not null);
                    simulator!.Initialize(new(languageName));
                    s_simulators.Add((type, languageName), simulator);
                }
                catch
                {
                    return null;
                }
            }
            return simulator;
        })
            .OfType<TSimulator>()
            .ToArray();

        if (simulators.Length == 0)
        {
            simulators = null;
            return false;
        }
        else return true;
    }

    public static bool TryGetLexerSimulatorByFileExtension(string fileExtension, [NotNullWhen(true)] out ILexerSimulator[]? lexerSimulators) =>
        Simulator.TryGetSimulatorByFileExtension(fileExtension, out lexerSimulators);

    public static bool TryGetLanguageParserSimulatorByFileExtension(string fileExtension, [NotNullWhen(true)] out ILanguageParserSimulator[]? languageParserSimulators) =>
        Simulator.TryGetSimulatorByFileExtension(fileExtension, out languageParserSimulators);

    internal static bool TryGetSimulatorByFileExtension<TSimulator>(string fileExtension, [NotNullWhen(true)] out TSimulator[]? simulators)
        where TSimulator : ISimulator
    {
        simulators = null;

        if (!s_fileExtensionMap.TryGetValue(fileExtension, out var languageNames) || languageNames.Count == 0) return false;

        simulators = languageNames.SelectMany(languageName =>
        {
            if (Simulator.TryGetSimulatorByLanguageName<TSimulator>(languageName, out var result))
                return result;
            else return Enumerable.Empty<TSimulator>();
        })
            .ToArray();

        if (simulators.Length == 0)
        {
            simulators = null;
            return false;
        }
        else return true;
    }

    public static void RegisterSimulator(string fileExtension, string languageName, Type simulatorType)
    {
        var interfaceType = typeof(ISimulator);
        if (!interfaceType.IsAssignableFrom(simulatorType)) throw new ArgumentException($"“{nameof(simulatorType)}” 必须是从 “{simulatorType.FullName}” 派生的类型。", nameof(simulatorType));

        var attributes = simulatorType.GetCustomAttributes().OfType<SimulatorAttribute>();
        if (!attributes.Any()) return; // 未包含指定的特性，不注册。

        Simulator.AddMapItem(s_fileExtensionMap, fileExtension, languageName);
        Simulator.AddMapItem(s_languageNameMap, languageName, simulatorType);
    }

    public static void RegisterSimulatorFrom(Assembly assembly, Func<string, IEnumerable<string>?>? languageNameToFileExtensionsProvider = null)
    {
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));
        languageNameToFileExtensionsProvider ??= Simulator.GetFileExtensionsFromLanguageName;

        var interfaceType = typeof(ISimulator);
        foreach (var type in assembly.DefinedTypes)
        {
            if (type.IsAbstract || type.IsInterface || type.IsEnum) continue;
            if (!interfaceType.IsAssignableFrom(type)) continue;

            var attributes = type.GetCustomAttributes().OfType<SimulatorAttribute>();
            foreach (var attribute in attributes)
            {
                if (attribute.Languages.Length == 0) continue;

                foreach (var languageName in attribute.Languages)
                {
                    var extensions = languageNameToFileExtensionsProvider(languageName);
                    if (extensions is null) continue;

                    foreach (var extension in extensions)
                        Simulator.RegisterSimulator(extension, languageName, type);
                }
            }
        }

    }

    public static void RegisterSimulatorFromConfiguration(SimulatorConfiguration config)
    {
        Debug.Assert(config is not null);

        Simulator.RegisterSimulatorFromConfigurationCore(config);
    }

    public static void RegisterSimulatorFromConfiguration(string configFilePath)
    {
        Debug.Assert(configFilePath is not null);

        SimulatorConfiguration? config = null;
        if (File.Exists(configFilePath))
        {
            using var fs = File.OpenRead(configFilePath);
            config = SimulatorConfiguration.Deserialize(fs);
        }
        Simulator.RegisterSimulatorFromConfigurationCore(config);
    }

    internal static void RegisterSimulatorFromConfigurationCore(SimulatorConfiguration? config)
    {
        string[]? searchPaths = null;
        Dictionary<string, HashSet<string>>? languageNameToFileExtensionMap = null;
        if (config is not null)
        {
            searchPaths = config.Paths;
            foreach (var pair in config.Extensions)
            {
                foreach (var languageName in pair.Value)
                {
                    languageNameToFileExtensionMap ??= new();
                    Simulator.AddMapItem(languageNameToFileExtensionMap, languageName, pair.Key);
                }
            }
        }

        searchPaths ??= new[] { "Simulators/" };
        var fileExtensionsProvider = languageNameToFileExtensionMap is null ? null :
            new Func<string, IEnumerable<string>?>(languageName =>
                languageNameToFileExtensionMap.TryGetValue(languageName, out var fileExtensions) ? fileExtensions : null);

        foreach (var searchPath in searchPaths)
        {
            if (!Directory.Exists(searchPath)) continue;

            foreach (var file in Directory.GetFiles(searchPath, "*.dll"))
            {
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFrom(file);
                }
                catch
                {
                    continue;
                }

                Simulator.RegisterSimulatorFrom(assembly, fileExtensionsProvider);
            }
        }
    }

    private static bool AddMapItem<TKey, TValue>(IDictionary<TKey, HashSet<TValue>> map, TKey key, TValue value) where TKey : notnull
    {
        if (map.TryGetValue(key, out var items))
            return items.Add(value);
        else
        {
            map.Add(key, new() { value });
            return true;
        }
    }

    private static IEnumerable<string>? GetFileExtensionsFromLanguageName(string languageName) =>
        languageName switch
        {
            "Lua" => new[] { ".lua" },
            "MoonScript" => new[] { ".moon" },

            _ => null
        };
}
