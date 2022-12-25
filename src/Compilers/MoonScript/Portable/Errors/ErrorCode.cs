// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript
{
    internal enum ErrorCode
    {
        Void = InternalErrorCode.Void,
        Unknown = InternalErrorCode.Unknown,

        #region 控制行
        /// <summary>重复包含源文件。</summary>
        WRN_FileAlreadyIncluded,
        /// <summary>无法读取<c>app.config</c>文件。</summary>
        ERR_CantReadConfigFile,
        #endregion

        ERR_InternalError,

        ERR_InvalidInstrumentationKind,
        ERR_BadSourceCodeKind,
        ERR_BadDocumentationMode,
        ERR_BadLanguageVersion,
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
        /// <summary>不合法的UTF-8字节序列。</summary>
        ERR_IllegalUtf8ByteSequence,
        WRN_ErrorOverride,

        #region 语法错误
        /// <summary>应输入关键字。</summary>
        ERR_IdentifierExpectedKW,
        #endregion

        #region MoonScript 0.5的消息
        [Obsolete("未正式发行版本")]
        ERR_FeatureNotAvailableInVersion0_5,
        #endregion

        #region Lua实验性版本的消息
        ERR_FeatureIsExperimental = 8501,
        ERR_FeatureInPreview,
        #endregion

        // 更新编译器的警告后应手动运行（eng\generate-compiler-code.cmd）。
    }
}
