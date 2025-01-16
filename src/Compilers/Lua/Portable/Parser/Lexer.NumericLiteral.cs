// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class Lexer
{
    /// <summary>
    /// 扫描一个完整的整型数字字面量。
    /// </summary>
    /// <param name="integerIsAbsent">扫描到的整型数字字面量是否缺失。</param>
    /// <param name="isHex">是否是十六进制格式。</param>
    private void ScanNumericLiteralSingleInteger(ref bool integerIsAbsent, bool isHex)
    {
        char c;
        while (true)
        {
            c = TextWindow.PeekChar();
            if (isHex ?
                SyntaxFacts.IsHexDigit(c) :
                SyntaxFacts.IsDecDigit(c)
            )
            {
                _builder.Append(c); // 将接受的字符推入缓存。
                integerIsAbsent = false;
                TextWindow.AdvanceChar();
            }
            else break;
        }
    }

    /// <summary>
    /// 扫描一个数字字面量
    /// </summary>
    /// <param name="info">要填充的标记信息对象。</param>
    /// <returns>
    /// 若扫描成功，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </returns>
    /// <remarks>
    /// 扫描接受的格式有：
    /// <list type="bullet">
    ///     <item>
    ///         <term>十进制小数格式</term>
    ///         <description>由于第一个字符为<c>.</c>，所以没有十六进制前缀。</description>
    ///     </item>
    ///     <item>
    ///         <term>十进制整数格式</term>
    ///         <description>不含小数点；后方有可选的指数表示：<c>e</c>或<c>E</c>后跟可正可负十进制整型数字。</description>
    ///     </item>
    ///     <item>
    ///         <term>十六进制整数或小数格式</term>
    ///         <description>小数格式时含小数点，整数部分和小数部分不能同时缺省；前方有<c>0x</c>或<c>0X</c>前缀；后方有可选的指数表示：<c>p</c>或<c>P</c>后跟可正可负十进制整型数字。</description>
    ///     </item>
    /// </list>
    /// </remarks>
    private partial bool ScanNumericLiteral(ref TokenInfo info)
    {
        var isHex = false; // 是否为十六进制。
        var hasDecimal = false; // 是否含有小数部分。
        var mayHasDecimal = false; // 可能含有小数部分。
        var hasExponent = false; // 是否含有指数部分。
        var integeralPartIsAbsent = true; // 整数部分是否缺省。
        var fractionalPartMayAbsent = true; // 小数部分是否缺省。
        info.Text = null;
        info.ValueKind = TokenValueType.None;
        _builder.Clear();

        // 扫描可能存在的十六进制前缀。
        var c = TextWindow.PeekChar();
        if (c == '0')
        {
            c = TextWindow.PeekChar(1);
            switch (c)
            {
                case 'x':
                case 'X':
                    TextWindow.AdvanceChar(2);
                    isHex = true;
                    break;
            }
        }

        /* 向后扫描一个完整的整型数字字面量，可能遇到这个字面量的宽度为零的情况。
         * 作为小数格式的整数部分时是合法的，但是作为整数格式时是不合法的。
         * 后者情况将在排除前者情况后生成诊断错误。
         */
        ScanNumericLiteralSingleInteger(ref integeralPartIsAbsent, isHex);

        var resetMarker = TextWindow.Position; // 回退记号。
        if (TextWindow.PeekChar() == '.') // 扫描小数点。
        {
            c = TextWindow.PeekChar(1);
            if (isHex ?
                SyntaxFacts.IsHexDigit(c) :
                SyntaxFacts.IsDecDigit(c)
            ) // 符合小数部分格式。
            {
                // 可能含有小数部分。
                mayHasDecimal = true;
                _builder.Append('.');
                TextWindow.AdvanceChar();

                // 先将回退记号推进到第一个连续的0-9的最后一位。
                ScanNumericLiteralSingleInteger(ref fractionalPartMayAbsent, isHex: false);
                if (!fractionalPartMayAbsent)
                {
                    resetMarker = TextWindow.Position;
                    // 确认含有小数部分。
                    hasDecimal = true;
                }

                ScanNumericLiteralSingleInteger(ref fractionalPartMayAbsent, isHex);
                Debug.Assert(fractionalPartMayAbsent == false); // 可能不缺失小数部分。
            }
            else if (integeralPartIsAbsent)
            {
                // 整数和小数部分同时缺失。
                if (isHex) // 存在十六进制前缀，则推断数字字面量格式错误。
                    AddError(MakeError(ErrorCode.ERR_InvalidNumber));
                else // 除了一个“.”以外没有任何其他字符，推断不是数字字面量标记。
                    return false;

            }
            else
            {
                // 可能含有小数部分。
                mayHasDecimal = true;
                _builder.Append('.');
                TextWindow.AdvanceChar();
            }
        }

        // 现在数字部分已经处理完，接下来处理指数表示。
        c = TextWindow.PeekChar();
        if (isHex ?
            c == 'p' || c == 'P' :
            c == 'e' || c == 'E'
        )
        {
            var c2 = TextWindow.PeekChar(1);
            var sign = char.MaxValue;
            var signedExponent = false;
            if (c2 == '-' || c2 == '+') // 有符号指数
            {
                signedExponent = true;
                sign = c2;
                c2 = TextWindow.PeekChar(2);
            }

            if (SyntaxFacts.IsDecDigit(c2)) // 符合指数格式。
            {
                // 确认含有指数部分。
                hasExponent = true;
                hasDecimal = true;
                _builder.Append(c);

                if (signedExponent)
                {
                    _builder.Append(sign);
                    TextWindow.AdvanceChar(2);
                }
                else
                {
                    TextWindow.AdvanceChar();
                }

                var exponentPartIsAbsent = true;
                ScanNumericLiteralSingleInteger(ref exponentPartIsAbsent, isHex: false);
                Debug.Assert(exponentPartIsAbsent == false); // 必定存在指数部分。
            }
        }

        // 确认点字符及其后方已扫描的部分是否为小数部分，若不是则回退。
        if (!hasDecimal && mayHasDecimal)
        {
            if (TextWindow.Position - resetMarker == 1 ? // 是否以小数点结尾。
                // 只考虑与标识符产生语法歧义的情况。
                SyntaxFacts.IsIdentifierStartCharacter(TextWindow.PeekChar()) :
                SyntaxFacts.IsIdentifierPartCharacter(TextWindow.PeekChar()))
            { // 后续是标识符，为了防止破坏后续的标记，回退到上一个可接受的回退记号的位置。
                if (resetMarker != TextWindow.Position)
                {
                    var length = TextWindow.Position - resetMarker;
                    _builder.Remove(_builder.Length - length, length);
                    TextWindow.Reset(resetMarker);
                }
            }
            else
                hasDecimal = true;
        }

        // 填充标记信息前最后一步：检查特性的可用性。
        if (isHex)
        {
            if (hasDecimal) // 十六进制浮点数自Lua 5.2添加，需要检查特性是否可用。
                CheckFeatureAvaliability(MessageID.IDS_FeatureHexadecimalFloatConstant);

            if (hasExponent) // 以2为底数的指数表示自Lua 5.2添加，需要检查特性是否可用。
                CheckFeatureAvaliability(MessageID.IDS_FeatureBinaryExponent);
        }

        info.Kind = SyntaxKind.NumericLiteralToken;
        info.Text = TextWindow.GetText(true);
        Debug.Assert(info.Text is not null);
        var valueText = TextWindow.Intern(_builder);
        if (hasDecimal)
            ParseRealValue(ref info, valueText, isHex);
        else
            ParseIntegerValue(ref info, valueText, isHex);

        return true;
    }

    /// <summary>
    /// 解析整型数字。
    /// </summary>
    private void ParseIntegerValue(ref TokenInfo info, string text, bool isHex)
    {
        if (isHex)
        {
            if (IntegerParser.TryParseHexadecimalInt64(text, out var result))
            {
                info.ValueKind = TokenValueType.Int64;
                info.LongValue = result;
                return;
            }
            else if (RealParser.TryParseHexadecimalDouble(text, out var doubleValue))
            {
                info.ValueKind = TokenValueType.Double;
                info.DoubleValue = doubleValue;
                return;
            }
        }
        else
        {
            if (IntegerParser.TryParseDecimalInt64(text, out var result))
            {
                if (result <= (ulong)long.MaxValue)
                {
                    info.ValueKind = TokenValueType.Int64;
                    info.LongValue = (long)result;
                }
                else
                {
                    info.ValueKind = TokenValueType.UInt64;
                    info.ULongValue = result;
                }
                return;
            }
            else if (RealParser.TryParseDecimalDouble(text, out var doubleValue))
            {
                info.ValueKind = TokenValueType.Double;
                info.DoubleValue = doubleValue;
                return;
            }
        }

        info.ValueKind = TokenValueType.Double;
        info.DoubleValue = double.PositiveInfinity;
        AddError(0, text.Length, ErrorCode.ERR_NumberOverflow);
    }

    /// <summary>
    /// 解析浮点型数字。
    /// </summary>
    private void ParseRealValue(ref TokenInfo info, string text, bool isHex)
    {
        if (isHex)
        {
            if (RealParser.TryParseHexadecimalDouble(text, out var result))
            {
                info.ValueKind = TokenValueType.Double;
                info.DoubleValue = result;
                return;
            }
        }
        else
        {
            if (RealParser.TryParseDecimalDouble(text, out var result))
            {
                info.ValueKind = TokenValueType.Double;
                info.DoubleValue = result;
                return;
            }
        }

        info.ValueKind = TokenValueType.Double;
        info.DoubleValue = double.PositiveInfinity;
        AddError(0, text.Length, ErrorCode.ERR_NumberOverflow);
    }
}
