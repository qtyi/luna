// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua;

internal enum ErrorCode
{
    Void = InternalErrorCode.Void,
    Unknown = InternalErrorCode.Unknown,

    #region Command line
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
    /// <summary>意外的字符。</summary>
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
    /// <summary>字符串常量中有换行符。</summary>
    ERR_NewlineInConst,
    /// <summary>不合法的转义序列。</summary>
    ERR_IllegalEscape,
    ERR_UnpairedSurrogates,
    WRN_ErrorOverride,

    #region 语法错误
    ERR_BadDirectivePlacement,
    /// <summary>应输入“,”。</summary>
    ERR_CommaExpected,
    /// <summary>应输入“;”。</summary>
    ERR_SemicolonExpected,
    /// <summary>应输入“,”或“;”。</summary>
    ERR_FieldSeparatorExpected,
    /// <summary>未于<see langword="if"/>语句后的<see langword="elseif"/>块。</summary>
    ERR_ElseIfCannotStartStatement,
    /// <summary>增量<see langword="for"/>循环语句中定义了过多的标识符。</summary>
    ERR_TooManyIdentifiers,
    /// <summary>无效的表达式项。</summary>
    ERR_InvalidExprTerm,
    /// <summary>应输入标识符。</summary>
    ERR_IdentifierExpected,
    /// <summary>应输入关键字。</summary>
    ERR_IdentifierExpectedKW,
    /// <summary>无效的字段值项。</summary>
    ERR_InvalidFieldValueTerm,
    /// <summary>应为表达式。</summary>
    ERR_ExpressionExpected,
    /// <summary>应为调用参数式。</summary>
    ERR_InvocationArgumentsExpected,
    /// <summary>应为赋值符号左侧表达式。</summary>
    ERR_AssgLvalueExpected,
    /// <summary>不正确的语句。</summary>
    ERR_IllegalStatement,
    /// <summary>应输入特性。</summary>
    ERR_AttributeExpected,
    /// <summary>无效的特性项。</summary>
    ERR_InvalidAttrTerm,
    /// <summary>位于错误位置的返回语句。</summary>
    ERR_MisplacedReturnStat,
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

    #region Lua 1.1的消息
    ERR_FeatureNotAvailableInVersion1_1 = 1001,
    #endregion

    #region Lua 2.1的消息
    ERR_FeatureNotAvailableInVersion2_1 = 1501,
    #endregion

    #region Lua 2.2的消息
    ERR_FeatureNotAvailableInVersion2_2 = 2001,
    #endregion

    #region Lua 2.4的消息
    ERR_FeatureNotAvailableInVersion2_4 = 3001,
    #endregion

    #region Lua 2.5的消息
    ERR_FeatureNotAvailableInVersion2_5 = 3501,
    WRN_ShebangOnlySupportedInScript,
    #endregion

    #region Lua 3.1的消息
    ERR_FeatureNotAvailableInVersion3_1 = 4501,
    ERR_InvalidPreprocExpr,
    ERR_UnexpectedDirective,
    ERR_EndDirectiveExpected,
    #endregion

    #region Lua 3.2的消息
    ERR_FeatureNotAvailableInVersion3_2 = 5001,
    #endregion

    #region Lua 4.0的消息
    ERR_FeatureNotAvailableInVersion4 = 5501,
    #endregion

    #region Lua 5.0的消息
    ERR_FeatureNotAvailableInVersion5 = 6001,
    #endregion

    #region Lua 5.1的消息
    ERR_FeatureNotAvailableInVersion5_1 = 6501,
    #endregion

    #region Lua 5.2的消息
    ERR_FeatureNotAvailableInVersion5_2 = 7001,
    #endregion

    #region Lua 5.3的消息
    ERR_FeatureNotAvailableInVersion5_3 = 7501,
    #endregion

    #region Lua 5.4的消息
    ERR_FeatureNotAvailableInVersion5_4 = 8001,
    #endregion

    #region Lua实验性版本的消息
    ERR_FeatureIsExperimental = 8501,
    ERR_FeatureNotAvailableInPreview,
    #endregion

    #region Lua dotnet的消息
    ERR_FeatureNotAvailableInVersionDotNet = 9001,
    #endregion

    // 更新编译器的警告后应手动运行（eng\generate-compiler-code.cmd）。
}
