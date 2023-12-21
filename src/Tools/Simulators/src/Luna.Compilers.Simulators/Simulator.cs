using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Luna.Compilers.Tools;
using Roslyn.Utilities;

namespace Luna.Compilers.Simulators;

public static class Simulator
{
    private static readonly Dictionary<string, HashSet<Type>> s_languageNameMap = new();
    internal static readonly Dictionary<string, HashSet<string>> s_fileExtensionMap = new(StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyCollection<Type> GetExportedComponents(string fileExtension) => GetExportedComponents(fileExtension, predicate: null);

    public static IReadOnlyCollection<Type> GetExportedComponents<T>(string fileExtension) => GetExportedComponents(fileExtension, predicate: static type => typeof(T).IsAssignableFrom(type));

    private static IReadOnlyCollection<Type> GetExportedComponents(string fileExtension, Func<Type, bool>? predicate = null)
    {
        var result = new HashSet<Type>();
        if (s_fileExtensionMap.TryGetValue(fileExtension, out var languageNames))
        {
            foreach (var languageName in languageNames)
            {
                if (s_languageNameMap.TryGetValue(languageName, out var types))
                    result.UnionWith(predicate is null ? types : types.Where(predicate));
            }
        }
        return result;
    }

    public static void RegisterSimulatorFrom(Assembly assembly, Func<string, IEnumerable<string>?>? languageNameToFileExtensionsProvider = null)
    {
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));
        RegisterSimulatorFromCore(assembly, languageNameToFileExtensionsProvider);
    }

    internal static void RegisterSimulatorFromCore(Assembly assembly, Func<string, IEnumerable<string>?>? languageNameToFileExtensionsProvider = null)
    {
        languageNameToFileExtensionsProvider ??= GetFileExtensionsFromLanguageName;

        foreach (var type in assembly.ExportedTypes)
        {
            if (type.IsAbstract || type.IsInterface || type.IsEnum) continue;
            if (!type.IsClass && !type.IsValueType) continue;

            var attributes = type.GetCustomAttributes().OfType<ExportAttribute>();
            foreach (var attribute in attributes)
            {
                if (attribute.Languages.Length == 0) continue;

                foreach (var languageName in attribute.Languages)
                {
                    var extensions = languageNameToFileExtensionsProvider(languageName);
                    if (extensions is null) continue;

                    foreach (var extension in extensions)
                    {
                        AddMapItem(s_fileExtensionMap, extension, languageName);
                        AddMapItem(s_languageNameMap, languageName, type);
                    }
                }
            }
        }

    }

    public static void RegisterSimulatorFromConfiguration(SimulatorConfiguration config)
    {
        Debug.Assert(config is not null);

        RegisterSimulatorFromConfigurationCore(config);
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
        RegisterSimulatorFromConfigurationCore(config);
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
                    AddMapItem(languageNameToFileExtensionMap, languageName, pair.Key);
                }
            }
        }

        searchPaths ??= new[] { "Simulators" + Path.DirectorySeparatorChar };
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

                RegisterSimulatorFromCore(assembly, fileExtensionsProvider);
            }
        }
    }

    private static void ClearAll()
    {
        s_languageNameMap.Clear();
        s_fileExtensionMap.Clear();
    }

    private static bool AddMapItem<TKey, TValue>(IDictionary<TKey, HashSet<TValue>> map, TKey key, TValue value, IEqualityComparer<TValue>? comparer = null) where TKey : notnull
    {
        if (map.TryGetValue(key, out var items))
            return items.Add(value);
        else
        {
            map.Add(key, new(comparer) { value });
            return true;
        }
    }

    private static IEnumerable<string>? GetFileExtensionsFromLanguageName(string languageName) =>
        languageName switch
        {
            Microsoft.CodeAnalysis.LanguageNames.CSharp => new[] { ".cs" },
            Microsoft.CodeAnalysis.LanguageNames.VisualBasic => new[] { ".vb" },
            Qtyi.CodeAnalysis.LanguageNames.Lua => new[] { ".lua" },
            Qtyi.CodeAnalysis.LanguageNames.MoonScript => new[] { ".moon" },

            _ => null
        };
}
