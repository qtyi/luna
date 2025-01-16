// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript;

internal enum ErrorCode
{
    Void = InternalErrorCode.Void,
    Unknown = InternalErrorCode.Unknown,

    #region 控制行
    /// <summary>Missing file specification for option.</summary>
    ERR_NoFileSpec,
    /// <summary>Include duplicate source files.</summary>
    WRN_FileAlreadyIncluded,
    /// <summary>The search path is invalid.</summary>
    WRN_InvalidSearchPathDir,
    /// <summary>Cannot read <c>app.config</c> file.</summary>
    ERR_CantReadConfigFile,
    /// <summary>Command line switch needs extra argument.</summary>
    ERR_SwitchNeedsArg,
    /// <summary>Language version string are explicitly treat as identifiers, thus leading zeros are not allowed.</summary>
    ERR_LanguageVersionCannotHaveLeadingZeroes,
    /// <summary>Unrecognized language version string.</summary>
    ERR_BadCompatMode,
    /// <summary>Unrecognized platform type.</summary>
    ERR_BadPlatformType,
    /// <summary>Unrecognized output target type.</summary>
    FTL_InvalidTarget,
    /// <summary>The extern alias is not specified.</summary>
    ERR_BadExternAlias,
    /// <summary>Extern alias is not a valid identifier.</summary>
    ERR_BadExternIdentifier,
    /// <summary>One reference alias option can only have one alias.</summary>
    ERR_OneAliasPerReference,
    /// <summary>Missing filename in reference alias option.</summary>
    ERR_AliasMissingFile,
    /// <summary>Do not use refout when using refonly.</summary>
    ERR_NoRefOutWhenRefOnly,
    /// <summary>Cannot compile net modules when using /refout or /refonly.</summary>
    ERR_NoNetModuleOutputWhenRefOutOrRefOnly,
    /// <summary>Source file could not be opened.</summary>
    WRN_NoSources,
    /// <summary>Invalid image base number.</summary>
    ERR_BadBaseNumber,
    /// <summary>Output directory could not be determined.</summary>
    ERR_NoOutputDirectory,
    /// <summary>/sourcelink switch is only supported when emitting PDB.</summary>
    ERR_SourceLinkRequiresPdb,
    /// <summary>/embed switch is only supported when emitting a PDB.</summary>
    ERR_CannotEmbedWithoutPdb,
    /// <summary>Extern alias is not a valid identifier.</summary>
    /// <summary>Unrecognized command line switch.</summary>
    ERR_BadSwitch,
    /// <summary>Output name is needed if there is no input source file.</summary>
    ERR_OutputNeedsName,
    /// <summary>Cannot locate the input file, its path is invalid.</summary>
    FTL_InvalidInputFileName,
    /// <summary>Assembly name are specified when output type is not 'netmodule'.</summary>
    ERR_AssemblyNameOnNonModule,
    #endregion

    ERR_InternalError,

    ERR_InvalidInstrumentationKind,
    ERR_BadSourceCodeKind,
    ERR_BadDocumentationMode,
    ERR_BadLanguageVersion,
    /// <summary>迭代过深，执行栈空间不足。</summary>
    ERR_InsufficientStack,
    /// <summary>意外的字符</summary>
    ERR_UnexpectedCharacter,
    /// <summary>语法错误。</summary>
    ERR_SyntaxError,
    /// <summary>无效的数字。</summary>
    ERR_InvalidNumber,
    /// <summary>数字溢出。</summary>
    ERR_NumberOverflow,
    /// <summary>没有结束配对的注释。</summary>
    ERR_OpenEndedComment,
    /// <summary>未终止的字符串常量。</summary>
    ERR_UnterminatedStringLiteral,
    /// <summary>不合法的转义序列。</summary>
    ERR_IllegalEscape,
    ERR_UnpairedSurrogates,
    WRN_ErrorOverride,

    #region 语法错误
    ERR_BadDirectivePlacement,
    /// <summary>应输入关键字。</summary>
    ERR_IdentifierExpectedKW,
    #endregion

    WRN_UnifyReferenceMajMin,
    WRN_UnifyReferenceBldRev,
    ERR_AssemblyMatchBadVersion,

    #region Forwarded Types
    /// <summary>The type forwarder for a type causes a cycle.</summary>
    ERR_CycleInTypeForwarder,
    /// <summary>There are duplicate <see cref="System.Runtime.CompilerServices.TypeForwardedToAttribute"/>s.</summary>
    ERR_DuplicateTypeForwarder,
    /// <summary>A type is forwarded to multiple assemblies.</summary>
    ERR_TypeForwardedToMultipleAssemblies,
    #endregion

    #region MoonScript 0.1的消息
    ERR_FeatureNotAvailableInVersion0_1 = 1001,
    WRN_ShebangOnlySupportedInScript,
    #endregion

    #region MoonScript 0.2的消息
    ERR_FeatureNotAvailableInVersion0_2 = 1501,
    #endregion

    #region MoonScript 0.3的消息
    ERR_FeatureNotAvailableInVersion0_3 = 2001,
    #endregion

    #region MoonScript 0.4的消息
    ERR_FeatureNotAvailableInVersion0_4 = 2501,
    #endregion

    #region MoonScript 0.5的消息
    ERR_FeatureNotAvailableInVersion0_5 = 3001,
    #endregion

    #region MoonScript实验性版本的消息
    ERR_FeatureIsExperimental = 3501,
    ERR_FeatureNotAvailableInPreview,
    #endregion

    #region MoonScript dotnet的消息
    ERR_FeatureNotAvailableInVersionDotNet = 4001,
    #endregion

    // 更新编译器的警告后应手动运行（eng\generate-compiler-code.cmd）。
}
