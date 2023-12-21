// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using Luna.Compilers.Simulators.Syntax;
using Roslyn.Utilities;

namespace Luna.Compilers.Simulators.Converters;

public abstract class SyntaxTriviaInfoConverter<T> : SyntaxNodeOrTokenInfoConverter<T>
{
    protected abstract object? ConvertWhitespace(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertSingleLineComment(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertMultiLineComment(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertEndOfLine(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertDocumentation(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);
    protected abstract object? ConvertDirectiveTrivia(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture);

    protected override object? Convert(bool isSelected, bool isHighlighted, object?[] values, object? parameter, CultureInfo culture)
    {
        Debug.Assert(values.Length > 0);
        Debug.Assert(values[0] is TriviaClassification);
        switch (values[0])
        {
            case TriviaClassification.Whitespace:
                return this.ConvertWhitespace(isSelected, isHighlighted, parameter, culture);
            case TriviaClassification.EndOfLine:
                return this.ConvertEndOfLine(isSelected, isHighlighted, parameter, culture);

            case TriviaClassification.Comment:
                Debug.Assert(values.Length > 1);
                Debug.Assert(values[1] is CommentTriviaClassification);
                switch (values[1])
                {
                    case CommentTriviaClassification.SingleLine:
                        return this.ConvertSingleLineComment(isSelected, isHighlighted, parameter, culture);
                    case CommentTriviaClassification.MultiLine:
                        return this.ConvertMultiLineComment(isSelected, isHighlighted, parameter, culture);
                }
                goto default;

            case TriviaClassification.Documentation:
                return this.ConvertDocumentation(isSelected, isHighlighted, parameter, culture);

            case TriviaClassification.Structured:
                Debug.Assert(values.Length > 2);
                Debug.Assert(values[2] is StructuredTriviaClassification);
                switch (values[2])
                {
                    case StructuredTriviaClassification.Directive:
                        return this.ConvertDirectiveTrivia(isSelected, isHighlighted, parameter, culture);
                }
                goto default;

            default:
                throw ExceptionUtilities.Unreachable();
        }
    }
}
