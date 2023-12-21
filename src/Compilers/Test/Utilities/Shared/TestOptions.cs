// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Test.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Test.Utilities;

using ThisCompilationOptions = LuaCompilationOptions;
using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Test.Utilities;

using ThisCompilationOptions = MoonScriptCompilationOptions;
using ThisParseOptions = MoonScriptParseOptions;
#endif

public static class TestOptions
{
    public static readonly ThisParseOptions Regular = new(kind: SourceCodeKind.Regular, documentationMode: DocumentationMode.Parse);
    public static readonly ThisParseOptions Script = new(kind: SourceCodeKind.Script, documentationMode: DocumentationMode.Parse);

    public static readonly ThisParseOptions RegularDefault = Regular.WithLanguageVersion(LanguageVersion.Default);
    public static readonly ThisParseOptions RegularPreview = Regular.WithLanguageVersion(LanguageVersion.Preview);

    public static readonly ThisCompilationOptions ReleaseDll = CreateTestOptions(outputKind: OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release);
    public static readonly ThisCompilationOptions ReleaseExe = CreateTestOptions(outputKind: OutputKind.ConsoleApplication, optimizationLevel: OptimizationLevel.Release);
    public static readonly ThisCompilationOptions ReleaseDebugDll = ReleaseDll.WithDebugPlusMode(true);
    public static readonly ThisCompilationOptions ReleaseDebugExe = ReleaseExe.WithDebugPlusMode(true);

    public static readonly ThisCompilationOptions DebugDll = CreateTestOptions(outputKind: OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Debug);
    public static readonly ThisCompilationOptions DebugExe = CreateTestOptions(outputKind: OutputKind.ConsoleApplication, optimizationLevel: OptimizationLevel.Debug);
    public static readonly ThisCompilationOptions DebugDllThrowing = DebugDll.WithMetadataReferenceResolver(ThrowingMetadataReferenceResolver.Default);
    public static readonly ThisCompilationOptions DebugExeThrowing = DebugExe.WithMetadataReferenceResolver(ThrowingMetadataReferenceResolver.Default);

    public static readonly ThisCompilationOptions ReleaseWinMD = CreateTestOptions(outputKind: OutputKind.WindowsRuntimeMetadata, optimizationLevel: OptimizationLevel.Release);
    public static readonly ThisCompilationOptions DebugWinMD = CreateTestOptions(outputKind: OutputKind.WindowsRuntimeMetadata, optimizationLevel: OptimizationLevel.Debug);

    public static readonly ThisCompilationOptions ReleaseNetmodule = CreateTestOptions(outputKind: OutputKind.NetModule, optimizationLevel: OptimizationLevel.Release);
    public static readonly ThisCompilationOptions DebugNetmodule = CreateTestOptions(outputKind: OutputKind.NetModule, optimizationLevel: OptimizationLevel.Debug);

    public static readonly ThisCompilationOptions SigningReleaseDll = ReleaseDll.WithStrongNameProvider(SigningTestHelpers.DefaultDesktopStrongNameProvider);
    public static readonly ThisCompilationOptions SigningReleaseExe = ReleaseExe.WithStrongNameProvider(SigningTestHelpers.DefaultDesktopStrongNameProvider);
    public static readonly ThisCompilationOptions SigningReleaseNetmodule = ReleaseNetmodule.WithStrongNameProvider(SigningTestHelpers.DefaultDesktopStrongNameProvider);
    public static readonly ThisCompilationOptions SigningDebugDll = DebugDll.WithStrongNameProvider(SigningTestHelpers.DefaultDesktopStrongNameProvider);

    public static readonly EmitOptions NativePdbEmit = EmitOptions.Default.WithDebugInformationFormat(DebugInformationFormat.Pdb);

    public static ThisParseOptions WithFeature(this ThisParseOptions options, string feature, string value = "true")
        => options.WithFeatures(options.Features.Concat(new[] { new KeyValuePair<string, string>(feature, value) }));

    internal static ThisParseOptions WithExperimental(this ThisParseOptions options, params MessageID[] features)
    {
        if (features.Length == 0)
            throw new InvalidOperationException("Need at least one feature to enable");

        var list = new List<KeyValuePair<string, string>>();
        foreach (var feature in features)
        {
            var name = feature.RequiredFeature();
            if (name is null)
                throw new InvalidOperationException($"{feature} is not a valid experimental feature");

            list.Add(new KeyValuePair<string, string>(name, "true"));
        }

        return options.WithFeatures(options.Features.Concat(list));
    }

    /// <summary>
    /// Create <see cref="ThisCompilationOptions"/> with the maximum warning level.
    /// </summary>
    /// <param name="outputKind">The output kind of the created compilation options.</param>
    /// <param name="optimizationLevel">The optimization level of the created compilation options.</param>
    /// <returns>A CSharpCompilationOptions with the specified <paramref name="outputKind"/>, and <paramref name="optimizationLevel"/>.</returns>
    internal static ThisCompilationOptions CreateTestOptions(
        OutputKind outputKind,
        OptimizationLevel optimizationLevel)
        => new(
            outputKind: outputKind,
            optimizationLevel: optimizationLevel,
            warningLevel: Diagnostic.MaxWarningLevel);
}
