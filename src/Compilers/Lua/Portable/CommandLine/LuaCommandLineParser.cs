// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaCommandLineParser
{
    /// <summary>
    /// File extension of a regular Lua source file.
    /// </summary>
    protected override string RegularFileExtension => ".lua";
    /// <summary>
    /// File extension of a Lua script file.
    /// </summary>
    protected override string ScriptFileExtension => ".lua";

    public new partial LuaCommandLineArguments Parse(IEnumerable<string> args, string? baseDirectory, string? sdkDirectory, string? additionalReferenceDirectories)
    {
        Debug.Assert(baseDirectory is not null || PathUtilities.IsAbsolute(baseDirectory));

        List<Diagnostic> diagnostics = new();
        var flattenedArgs = ArrayBuilder<string>.GetInstance();
        List<string>? scriptArgs = this.IsScriptCommandLineParser ? new() : null;
        List<string>? responsePaths = this.IsScriptCommandLineParser ? new() : null;
        this.FlattenArgs(args, diagnostics, flattenedArgs, scriptArgs, baseDirectory, responsePaths);

        var appConfigPath = (string?)null;
        var displayLogo = true;
        var displayHelp = false;
        var displayVersion = false;
        var displayLangVersions = false;
        var optimize = false;
        var checkOverflow = false;
        var concurrentBuild = true;
        var deterministic = true;
        var emitPdb = false;
        var debugInformationFormat = PathUtilities.IsUnixLikePlatform ? DebugInformationFormat.PortablePdb : DebugInformationFormat.Pdb;
        var debugPlus = false;
        var pdbPath = (string?)null;
        var noStdLib = this.IsScriptCommandLineParser; // don't add mscorlib from sdk dir when running scripts
        var outputDirectory = baseDirectory;
        var pathMap = ImmutableArray<KeyValuePair<string, string>>.Empty;
        var outputFileName = (string?)null;
        var outputRefFilePath = (string?)null;
        var refOnly = false;
        var generatedFilesOutputDirectory = (string?)null;
        var documentationPath = (string?)null;
        var errorLogOptions = (ErrorLogOptions?)null;
        var utf8output = false;
        var outputKind = OutputKind.ConsoleApplication;
        var subsystemVersion = SubsystemVersion.None;
        var languageVersion = LanguageVersion.Default;
        var mainModuleName = (string?)null;
        var win32ManifestFile = (string?)null;
        var win32ResourceFile = (string?)null;
        var win32IconFile = (string?)null;
        var noWin32Manifest = false;
        var platform = Platform.AnyCpu;
        var baseAddress = 0UL;
        var fileAlignment = 0;
        var delaySignSetting = (bool?)null;
        var keyFileSetting = (string?)null;
        var keyContainerSetting = (string?)null;
        var managedResources = new List<ResourceDescription>();
        var sourceFiles = new List<CommandLineSourceFile>();
        var additionalFiles = new List<CommandLineSourceFile>();
        var analyzerConfigPaths = ArrayBuilder<string>.GetInstance();
        var embeddedFiles = new List<CommandLineSourceFile>();
        var sourceFilesSpecified = false;
        var embedAllSourceFiles = false;
        var resourcesOrModulesSpecified = false;
        var codepage = (Encoding?)null;
        var checksumAlgorithm = SourceHashAlgorithms.Default;
        var defines = ArrayBuilder<string>.GetInstance();
        var metadataReferences = new List<CommandLineReference>();
        var analyzers = new List<CommandLineAnalyzerReference>();
        var libPaths = new List<string>();
        var sourcePaths = new List<string>();
        var keyFileSearchPaths = new List<string>();
        var usings = new List<string>();
        var generalDiagnosticOption = ReportDiagnostic.Default;
        var diagnosticOptions = new Dictionary<string, ReportDiagnostic>();
        var noWarns = new Dictionary<string, ReportDiagnostic>();
        var warnAsErrors = new Dictionary<string, ReportDiagnostic>();
        var warningLevel = Diagnostic.DefaultWarningLevel;
        var highEntropyVA = false;
        var printFullPaths = false;
        var netmoduleAssemblyName = (string?)null;
        var netmoduleName = (string?)null;
        var features = new List<string>();
        var runtimeMetadataVersion = (string?)null;
        var errorEndLocation = false;
        var reportAnalyzer = false;
        var skipAnalyzers = false;
        var instrumentationKinds = ArrayBuilder<InstrumentationKind>.GetInstance();
        var preferredUILang = (CultureInfo?)null;
        var touchedFilesPath = (string?)null;
        var optionsEnded = false;
        var interactiveMode = false;
        var publicSign = false;
        var sourceLink = (string?)null;
        var ruleSetPath = (string?)null;

        // Process ruleset files first so that diagnostic severity settings specified on the command line via
        // /nowarn and /warnaserror can override diagnostic severity settings specified in the ruleset file.
        if (!this.IsScriptCommandLineParser)
        {
            foreach (var arg in flattenedArgs)
            {

                if (IsOption("ruleset", arg, out var name, out var value))
                {
                    var unquoted = RemoveQuotesAndSlashes(value);

                    if (RoslynString.IsNullOrEmpty(unquoted))
                        AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, MessageID.IDS_Text.Localize(), name.ToString());
                    else
                    {
                        ruleSetPath = this.ParseGenericPathToFile(unquoted, diagnostics, baseDirectory);
                        generalDiagnosticOption = this.GetDiagnosticOptionsFromRulesetFile(ruleSetPath, out diagnosticOptions, diagnostics);
                    }
                }
            }
        }

        foreach (var arg in flattenedArgs)
        {
            Debug.Assert(optionsEnded || !arg.StartsWith("@", StringComparison.Ordinal));

            ArrayBuilder<string> filePathBuilder;
            ReadOnlyMemory<char> nameMemory;
            ReadOnlyMemory<char>? valueMemory;
            if (optionsEnded || !TryParseOption(arg, out nameMemory, out valueMemory))
            {
                filePathBuilder = ArrayBuilder<string>.GetInstance();
                this.ParseFileArgument(arg.AsMemory(), baseDirectory, filePathBuilder, diagnostics);
                foreach (var path in filePathBuilder)
                    sourceFiles.Add(ToCommandLineSourceFile(path));
                filePathBuilder.Free();

                if (sourceFiles.Count > 0)
                    sourceFilesSpecified = true;

                continue;
            }

            string? value;
            string? GetValueMemoryString() => valueMemory is { } m ? m.Span.ToString() : null;

            // The main 'switch' for argument handling forces an allocation of the option name field. For the most 
            // common options we special case the handling below to avoid this allocation as it can contribute significantly 
            // to parsing allocations.
            //
            // When we allow for switching on Span<char> this can be undone as the name 'switch' will be allocation free
            // https://github.com/dotnet/roslyn/pull/44388
            if (IsOptionName("r", "reference", nameMemory))
            {
                ParseAssemblyReferences(arg, valueMemory, diagnostics, embedInteropTypes: false, metadataReferences);
                continue;
            }
            else if (IsOptionName("langversion", nameMemory))
            {
                value = RemoveQuotesAndSlashes(valueMemory);
                if (RoslynString.IsNullOrEmpty(value))
                {
                    AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, MessageID.IDS_Text.Localize(), "/langversion:");
                }
                else if (value.StartsWith("0", StringComparison.Ordinal))
                {
                    // Stop parsing versions as ints, but explicitly treat them as identifiers.
                    AddDiagnostic(diagnostics, ErrorCode.ERR_LanguageVersionCannotHaveLeadingZeroes, value);
                }
                else if (value == "?")
                {
                    displayLangVersions = true;
                }
                else if (!LanguageVersionFacts.TryParse(value, out languageVersion))
                {
                    AddDiagnostic(diagnostics, ErrorCode.ERR_BadCompatMode, value);
                }
                continue;
            }
            else if (!IsScriptCommandLineParser && IsOptionName("a", "analyzer", nameMemory))
            {
                ParseAnalyzers(arg, valueMemory, analyzers, diagnostics);
                continue;
            }
            else if (!IsScriptCommandLineParser && IsOptionName("nowarn", nameMemory))
            {
                if (valueMemory is null)
                {
                    AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, LuaResources.IDS_Number, nameMemory.ToString());
                    continue;
                }

                if (valueMemory.Value.Length == 0)
                {
                    AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, LuaResources.IDS_Number, nameMemory.ToString());
                }
                else
                {
                    AddWarnings(noWarns, ReportDiagnostic.Suppress, valueMemory.Value);
                }
                continue;
            }

            var name = nameMemory.Span.ToString().ToLowerInvariant();
            switch (name)
            {
                case "?":
                case "help":
                    displayHelp = true;
                    continue;

                case "version":
                    displayVersion = true;
                    continue;

                case "features":
                    value = GetValueMemoryString();
                    if (value is null)
                    {
                        features.Clear();
                    }
                    else
                    {
                        features.Add(value);
                    }
                    continue;

                case "lib":
                case "libpath":
                case "libpaths":
                    ParseAndResolveReferencePaths(name, valueMemory, baseDirectory, libPaths, MessageID.IDS_LIB_OPTION, diagnostics);
                    continue;

#if DEBUG
                case "attachdebugger":
                    Debugger.Launch();
                    continue;
#endif
            }

#warning 未实现

            AddDiagnostic(diagnostics, ErrorCode.ERR_BadSwitch, arg);
        }

        foreach (var (id, option) in warnAsErrors)
            diagnosticOptions[id] = option;

        // Specific nowarn options always override specific warnaserror options.
        foreach (var (id, option) in noWarns)
            diagnosticOptions[id] = option;

        if (refOnly && outputRefFilePath != null)
            AddDiagnostic(diagnostics, diagnosticOptions, ErrorCode.ERR_NoRefOutWhenRefOnly);

        if (outputKind == OutputKind.NetModule && (refOnly || outputRefFilePath != null))
            AddDiagnostic(diagnostics, diagnosticOptions, ErrorCode.ERR_NoNetModuleOutputWhenRefOutOrRefOnly);

        if (!IsScriptCommandLineParser && !sourceFilesSpecified && (outputKind.IsNetModule() || !resourcesOrModulesSpecified))
            AddDiagnostic(diagnostics, diagnosticOptions, ErrorCode.WRN_NoSources);

        if (!noStdLib && sdkDirectory != null)
            metadataReferences.Insert(0, new CommandLineReference(Path.Combine(sdkDirectory, "mscorlib.dll"), MetadataReferenceProperties.Assembly));

        if (!platform.Requires64Bit())
        {
            if (baseAddress > uint.MaxValue - 0x8000)
            {
                AddDiagnostic(diagnostics, ErrorCode.ERR_BadBaseNumber, string.Format("0x{0:X}", baseAddress));
                baseAddress = 0;
            }
        }

        // add additional reference paths if specified
        if (!string.IsNullOrEmpty(additionalReferenceDirectories))
            ParseAndResolveReferencePaths(null, additionalReferenceDirectories.AsMemory(), baseDirectory, libPaths, MessageID.IDS_LIB_ENV, diagnostics);

        var referencePaths = BuildSearchPaths(sdkDirectory, libPaths, responsePaths);

        // Dev11 searches for the key file in the current directory and assembly output directory.
        // We always look to base directory and then examine the search paths.
        if (!RoslynString.IsNullOrEmpty(baseDirectory))
            keyFileSearchPaths.Add(baseDirectory);

        if (RoslynString.IsNullOrEmpty(outputDirectory))
            AddDiagnostic(diagnostics, ErrorCode.ERR_NoOutputDirectory);
        else if (baseDirectory != outputDirectory)
            keyFileSearchPaths.Add(outputDirectory);

        // Public sign doesn't use the legacy search path settings
        if (publicSign && !RoslynString.IsNullOrEmpty(keyFileSetting))
            keyFileSetting = ParseGenericPathToFile(keyFileSetting, diagnostics, baseDirectory);

        if (sourceLink != null && !emitPdb)
            AddDiagnostic(diagnostics, ErrorCode.ERR_SourceLinkRequiresPdb);

        if (embedAllSourceFiles)
            embeddedFiles.AddRange(sourceFiles);

        if (embeddedFiles.Count > 0 && !emitPdb)
            AddDiagnostic(diagnostics, ErrorCode.ERR_CannotEmbedWithoutPdb);

        var parsedFeatures = ParseFeatures(features);

        string? compilationName;
        GetCompilationAndModuleNames(diagnostics, outputKind, sourceFiles, sourceFilesSpecified, netmoduleAssemblyName, ref outputFileName, ref netmoduleName, out compilationName);

        flattenedArgs.Free();

        var parseOptions = new LuaParseOptions(
            languageVersion: languageVersion,
            documentationMode: DocumentationMode.None,
            kind: this.IsScriptCommandLineParser ? SourceCodeKind.Script : SourceCodeKind.Regular,
            features: parsedFeatures);

        // We want to report diagnostics with source suppression in the error log file.
        // However, these diagnostics won't be reported on the command line.
        var reportSuppressedDiagnostics = errorLogOptions is object;

        var options = new LuaCompilationOptions(
            outputKind: outputKind,
            platform: platform,
            netmoduleName: netmoduleName,
            mainModuleName: mainModuleName,
            scriptModuleName: WellKnownMemberNames.DefaultScriptClassName,
            optimizationLevel: optimize ? OptimizationLevel.Release : OptimizationLevel.Debug,
            warningLevel: warningLevel,
            concurrentBuild: concurrentBuild,
            deterministic: deterministic,
            cryptoKeyContainer: keyContainerSetting,
            cryptoKeyFile: keyFileSetting,
            delaySign: delaySignSetting,
            publicSign: publicSign,
            generalDiagnosticOption: generalDiagnosticOption,
            specificDiagnosticOptions: diagnosticOptions);

        if (debugPlus)
            options = options.WithDebugPlusMode(debugPlus);

        var emitOptions = new EmitOptions(
            metadataOnly: refOnly,
            includePrivateMembers: !refOnly && outputRefFilePath is null,
            debugInformationFormat: debugInformationFormat,
            pdbFilePath: null, // to be determined later.
            outputNameOverride: null, // to be determined later.
            baseAddress: baseAddress,
            highEntropyVirtualAddressSpace: highEntropyVA,
            fileAlignment: fileAlignment,
            subsystemVersion: subsystemVersion,
            runtimeMetadataVersion: runtimeMetadataVersion,
            instrumentationKinds: instrumentationKinds.ToImmutableAndFree(),
            // TODO: set from /checksumalgorithm (see https://github.com/dotnet/roslyn/issues/24735)
            pdbChecksumAlgorithm: HashAlgorithmName.SHA256,
            defaultSourceFileEncoding: codepage);

        // add option incompatibility errors if any
        diagnostics.AddRange(options.Errors);
        diagnostics.AddRange(parseOptions.Errors);

        pathMap = SortPathMap(pathMap);

        return new LuaCommandLineArguments
        {
            IsScriptRunner = this.IsScriptCommandLineParser,
            InteractiveMode = interactiveMode || this.IsScriptCommandLineParser && sourceFiles.Count == 0,
            BaseDirectory = baseDirectory,
            PathMap = pathMap,
            Errors = diagnostics.AsImmutable(),
            Utf8Output = utf8output,
            CompilationName = compilationName,
            OutputFileName = outputFileName,
            OutputRefFilePath = outputRefFilePath,
            PdbPath = pdbPath,
            EmitPdb = emitPdb && !refOnly, // silently ignore emitPdb when refOnly is set
            SourceLink = sourceLink,
            RuleSetPath = ruleSetPath,
            OutputDirectory = outputDirectory!, // error produced when null
            DocumentationPath = documentationPath,
            GeneratedFilesOutputDirectory = generatedFilesOutputDirectory,
            ErrorLogOptions = errorLogOptions,
            AppConfigPath = appConfigPath,
            SourceFiles = sourceFiles.AsImmutable(),
            Encoding = codepage,
            ChecksumAlgorithm = checksumAlgorithm,
            MetadataReferences = metadataReferences.AsImmutable(),
            AnalyzerReferences = analyzers.AsImmutable(),
            AnalyzerConfigPaths = analyzerConfigPaths.ToImmutableAndFree(),
            AdditionalFiles = additionalFiles.AsImmutable(),
            ReferencePaths = referencePaths,
            SourcePaths = sourcePaths.AsImmutable(),
            KeyFileSearchPaths = keyFileSearchPaths.AsImmutable(),
            Win32ResourceFile = win32ResourceFile,
            Win32Icon = win32IconFile,
            Win32Manifest = win32ManifestFile,
            NoWin32Manifest = noWin32Manifest,
            DisplayLogo = displayLogo,
            DisplayHelp = displayHelp,
            DisplayVersion = displayVersion,
            DisplayLangVersions = displayLangVersions,
            ManifestResources = managedResources.AsImmutable(),
            CompilationOptions = options,
            ParseOptions = parseOptions,
            EmitOptions = emitOptions,
            ScriptArguments = scriptArgs.AsImmutableOrEmpty(),
            TouchedFilesPath = touchedFilesPath,
            PrintFullPaths = printFullPaths,
            ShouldIncludeErrorEndLocation = errorEndLocation,
            PreferredUILang = preferredUILang,
            ReportAnalyzer = reportAnalyzer,
            SkipAnalyzers = skipAnalyzers,
            EmbeddedFiles = embeddedFiles.AsImmutable()
        };
    }

    private static void ParseAndResolveReferencePaths(string? switchName, ReadOnlyMemory<char>? switchValue, string? baseDirectory, List<string> builder, MessageID origin, List<Diagnostic> diagnostics)
    {
        if (switchValue is not { Length: > 0 })
        {
            RoslynDebug.Assert(!RoslynString.IsNullOrEmpty(switchName));
            AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, MessageID.IDS_PathList.Localize(), switchName);
            return;
        }

        foreach (var path in ParseSeparatedPaths(switchValue.Value.ToString()))
        {
            var resolvedPath = FileUtilities.ResolveRelativePath(path, baseDirectory);
            if (resolvedPath is null)
            {
                AddDiagnostic(diagnostics, ErrorCode.WRN_InvalidSearchPathDir, path, origin.Localize(), MessageID.IDS_DirectoryHasInvalidPath.Localize());
            }
            else if (!Directory.Exists(resolvedPath))
            {
                AddDiagnostic(diagnostics, ErrorCode.WRN_InvalidSearchPathDir, path, origin.Localize(), MessageID.IDS_DirectoryDoesNotExist.Localize());
            }
            else
                builder.Add(resolvedPath);
        }
    }

    private void GetCompilationAndModuleNames(
        List<Diagnostic> diagnostics,
        OutputKind outputKind,
        List<CommandLineSourceFile> sourceFiles,
        bool sourceFilesSpecified,
        string? netmoduleAssemblyName,
        ref string? outputFileName,
        ref string? moduleName,
        out string? compilationName)
    {
        string? simpleName;
        if (outputFileName is null)
        {
            // In lua, if the output file name isn't specified explicitly, then executables and libraries
            // derive their names from their first input files and bytecodes has its default name 'luac.out'.
            if (!this.IsScriptCommandLineParser && !sourceFilesSpecified)
            {
                AddDiagnostic(diagnostics, ErrorCode.ERR_OutputNeedsName);
                simpleName = null;
            }
            else if (outputKind.IsLuaIntermediateBytecodes())
                simpleName = "lua";
            else
            {
                simpleName = PathUtilities.RemoveExtension(PathUtilities.GetFileName(sourceFiles.FirstOrDefault().Path));
                outputFileName = simpleName + outputKind.GetDefaultExtension();

                if (simpleName.Length == 0 && !outputKind.IsNetModule())
                {
                    AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidInputFileName, outputFileName);
                    outputFileName = simpleName = null;
                }
            }
        }
        else
        {
            simpleName = PathUtilities.RemoveExtension(outputFileName);

            if (simpleName.Length == 0)
            {
                AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidInputFileName, outputFileName);
                outputFileName = simpleName = null;
            }
        }

        if (outputKind.IsNetModule())
        {
            Debug.Assert(!this.IsScriptCommandLineParser);

            compilationName = netmoduleAssemblyName;
        }
        else
        {
            if (netmoduleAssemblyName is not null)
                AddDiagnostic(diagnostics, ErrorCode.ERR_AssemblyNameOnNonModule);

            compilationName = simpleName;
        }

        if (moduleName is null)
        {
            moduleName = outputFileName;
        }
    }

    private ImmutableArray<string> BuildSearchPaths(string? sdkDirectoryOpt, List<string> libPaths, List<string>? responsePathsOpt)
    {
        var builder = ArrayBuilder<string>.GetInstance();

        // current folder first -- base directory is searched by default

        // Add SDK directory if it is available
        if (sdkDirectoryOpt is not null)
            builder.Add(sdkDirectoryOpt);

        // libpath
        builder.AddRange(libPaths);

        // lua.exe adds paths of the response file(s) to the search paths, so that we can initialize the script environment
        // with references relative to lua.exe (e.g. System.ValueTuple.dll).
        if (responsePathsOpt is not null)
        {
            Debug.Assert(this.IsScriptCommandLineParser);
            builder.AddRange(responsePathsOpt);
        }

        return builder.ToImmutableAndFree();
    }

    private static Platform ParsePlatform(string value, IList<Diagnostic> diagnostics)
    {
        switch (value.ToLowerInvariant())
        {
            case "x86":
                return Platform.X86;
            case "x64":
                return Platform.X64;
            case "itanium":
                return Platform.Itanium;
            case "anycpu":
                return Platform.AnyCpu;
            case "anycpu32bitpreferred":
                return Platform.AnyCpu32BitPreferred;
            case "arm":
                return Platform.Arm;
            case "arm64":
                return Platform.Arm64;
            default:
                AddDiagnostic(diagnostics, ErrorCode.ERR_BadPlatformType, value);
                return Platform.AnyCpu;
        }
    }

    private static OutputKind ParseTarget(string value, IList<Diagnostic> diagnostics)
    {
        switch (value.ToLowerInvariant())
        {
            case "exe":
                return OutputKind.ConsoleApplication;

            case "winexe":
                return OutputKind.WindowsApplication;

            case "library":
                return OutputKind.DynamicallyLinkedLibrary;

            case "netmodule":
                return OutputKind.NetModule;

            case "appcontainerexe":
                return OutputKind.WindowsRuntimeApplication;

            case "winmdobj":
                return OutputKind.WindowsRuntimeMetadata;

            case "intermediate":
                return OutputKind.LuaIntermediateBytecodes;

            default:
                AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidTarget);
                return OutputKind.ConsoleApplication;
        }
    }

    private static void ParseAnalyzers(string arg, ReadOnlyMemory<char>? valueMemory, List<CommandLineAnalyzerReference> analyzerReferences, List<Diagnostic> diagnostics)
    {
        if (valueMemory is not { } value)
        {
            AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, MessageID.IDS_Text.Localize(), arg);
            return;
        }
        else if (value.Length == 0)
        {
            AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, arg);
            return;
        }

        var builder = ArrayBuilder<ReadOnlyMemory<char>>.GetInstance();
        ParseSeparatedPathsEx(value, builder);
        foreach (var path in builder)
        {
            if (path.Length == 0)
            {
                continue;
            }

            analyzerReferences.Add(new CommandLineAnalyzerReference(path.ToString()));
        }
        builder.Free();
    }

    private static void ParseAssemblyReferences(string arg, ReadOnlyMemory<char>? valueMemory, IList<Diagnostic> diagnostics, bool embedInteropTypes, List<CommandLineReference> commandLineReferences)
    {
        if (valueMemory is null)
        {
            AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsArg, MessageID.IDS_Text.Localize(), arg);
            return;
        }

        var value = valueMemory.Value;
        if (value.Length == 0)
        {
            AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, arg);
            return;
        }

        // /r:"reference"
        // /r:alias=reference
        // /r:alias="reference"
        // /r:reference;reference
        // /r:"path;containing;semicolons"
        // /r:"unterminated_quotes
        // /r:"quotes"in"the"middle
        // /r:alias=reference;reference      ... error 2034
        // /r:nonidf=reference               ... error 1679

        var valueSpan = value.Span;
        var eqlOrQuote = valueSpan.IndexOfAny(s_quoteOrEquals);

        string? alias;
        if (eqlOrQuote >= 0 && valueSpan[eqlOrQuote] == '=')
        {
            alias = value[..eqlOrQuote].ToString();
            value = value[(eqlOrQuote + 1)..];

            if (!SyntaxFacts.IsValidIdentifier(alias))
            {
                AddDiagnostic(diagnostics, ErrorCode.ERR_BadExternIdentifier, alias);
                return;
            }
        }
        else
            alias = null;

        var builder = ArrayBuilder<ReadOnlyMemory<char>>.GetInstance();
        ParseSeparatedPathsEx(value, builder);
        var pathCount = 0;
        foreach (var path in builder)
        {
            if (path.IsWhiteSpace())
                continue;

            pathCount++;

            // NOTE(tomat): Dev10 used to report CS1541: ERR_CantIncludeDirectory if the path was a directory.
            // Since we now support /referencePaths option we would need to search them to see if the resolved path is a directory.

            var aliases = (alias is not null) ? ImmutableArray.Create(alias) : ImmutableArray<string>.Empty;

            var properties = new MetadataReferenceProperties(MetadataImageKind.Assembly, aliases, embedInteropTypes);
            commandLineReferences.Add(new(path.ToString(), properties));
        }
        builder.Free();

        if (alias is not null)
        {
            if (pathCount > 1)
            {
                commandLineReferences.RemoveRange(commandLineReferences.Count - pathCount, pathCount);
                AddDiagnostic(diagnostics, ErrorCode.ERR_OneAliasPerReference);
                return;
            }

            if (pathCount == 0)
            {
                AddDiagnostic(diagnostics, ErrorCode.ERR_AliasMissingFile, alias);
                return;
            }
        }
    }
}
