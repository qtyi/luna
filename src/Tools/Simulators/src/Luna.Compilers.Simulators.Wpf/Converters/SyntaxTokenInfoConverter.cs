// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using Luna.Compilers.Simulators.Syntax;
using Roslyn.Utilities;

namespace Luna.Compilers.Simulators.Converters;

public abstract class SyntaxTokenInfoConverter<T> : SyntaxNodeOrTokenInfoConverter<T>
{
    protected abstract object? ConvertBad(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertNormalIdentifier(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertContextualKeyword(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertKeyword(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertOperator(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertPunctuation(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertNumericLiteral(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertStringLiteral(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertLeadingDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertMessageTextDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertKeywordDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertIdentifierDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertOperatorDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertPunctuationDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertLiteralDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertEndOfDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertDisabledTextDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertEndOfFile(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertDocumentation(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);

    protected override object? Convert(bool isSelected, bool isHighlighted, object?[] values, object? parameter, CultureInfo culture)
    {
        Debug.Assert(values.Length > 0);
        Debug.Assert(values[0] is TokenClassification);
        switch (values[0])
        {
            case TokenClassification.Bad:
                return this.ConvertBad(isSelected, isHighlighted, parameter, culture);
            case TokenClassification.Keyword:
                return this.ConvertKeyword(isSelected, isHighlighted, parameter, culture);

            case TokenClassification.Identifier:
                Debug.Assert(values.Length > 1);
                Debug.Assert(values[1] is IdentifierTokenClassification);
                switch (values[1])
                {
                    case IdentifierTokenClassification.Normal:
                        return this.ConvertNormalIdentifier(isSelected, isHighlighted, parameter, culture);
                    case IdentifierTokenClassification.ContextualKeyword:
                        return this.ConvertContextualKeyword(isSelected, isHighlighted, parameter, culture);
                }
                goto default;

            case TokenClassification.Operator:
                return this.ConvertOperator(isSelected, isHighlighted, parameter, culture);
            case TokenClassification.Punctuation:
                return this.ConvertPunctuation(isSelected, isHighlighted, parameter, culture);

            case TokenClassification.Literal:
                Debug.Assert(values.Length > 2);
                Debug.Assert(values[2] is LiteralTokenClassification);
                switch (values[2])
                {
                    case LiteralTokenClassification.Numeric:
                        return this.ConvertNumericLiteral(isSelected, isHighlighted, parameter, culture);
                    case LiteralTokenClassification.String:
                        return this.ConvertStringLiteral(isSelected, isHighlighted, parameter, culture);
                }
                goto default;

            case TokenClassification.Directive:
                Debug.Assert(values.Length > 3);
                Debug.Assert(values[3] is DirectiveTokenClassification);
                switch (values[3])
                {
                    case DirectiveTokenClassification.Leading:
                        return this.ConvertLeadingDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.MessageText:
                        return this.ConvertMessageTextDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.Keyword:
                        return this.ConvertKeywordDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.Identifier:
                        return this.ConvertIdentifierDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.Operator:
                        return this.ConvertOperatorDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.Punctuation:
                        return this.ConvertPunctuationDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.Literal:
                        return this.ConvertLiteralDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.EndOfDirective:
                        return this.ConvertEndOfDirective(isSelected, isHighlighted, parameter, culture);
                    case DirectiveTokenClassification.DisabledText:
                        return this.ConvertDisabledTextDirective(isSelected, isHighlighted, parameter, culture);

                }
                goto default;

            case TokenClassification.EndOfFile:
                return this.ConvertEndOfFile(isSelected, isHighlighted, parameter, culture);

            case TokenClassification.Documentation:
                return this.ConvertDocumentation(isSelected, isHighlighted, parameter, culture);

            default:
                throw ExceptionUtilities.Unreachable();
        }
    }
}
