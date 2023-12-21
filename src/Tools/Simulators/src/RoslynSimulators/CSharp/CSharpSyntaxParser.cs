// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Luna.Compilers.Simulators.CSharp;

[Export(LanguageNames.CSharp)]
public sealed class CSharpSyntaxParser : AbstractSyntaxParser
{
    public override void Initialize(SyntaxParserInitializationContext context)
    {
        base.Initialize(context);

        context.RegisterRadioParseOption(nameof(CSharpParseOptions.Language), Enum.GetValues(typeof(LanguageVersion)));
        context.RegisterComplexParseOption(nameof(CSharpParseOptions.PreprocessorSymbolNames),
            converter: static text =>
            {
                if (string.IsNullOrWhiteSpace(text))
                    return null;
                else
                    return text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(static s => s.Trim());
            },
            validator: static (IEnumerable<string>? value, out IEnumerable<string>? validValue) =>
            {
                if (value is null)
                {
                    validValue = null;
                    return true;
                }

                var hasError = false;
                var set = new HashSet<string>();
                foreach (var preprocessorSymbolName in value)
                {
                    if (SyntaxFacts.IsValidIdentifier(preprocessorSymbolName))
                        set.Add(preprocessorSymbolName);
                    else
                        hasError = true;
                }
                validValue = set;
                return !hasError;
            });
        context.RegisterComplexParseOption(nameof(CSharpParseOptions.Features),
            converter: static text =>
            {
                if (string.IsNullOrWhiteSpace(text))
                    return null;
                else

                    return from span in text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                           let pair = span.Split(new[] { '=' }, 2)
                           select pair.Length == 1 ? new KeyValuePair<string, string>(pair[0].Trim(), string.Empty) : new KeyValuePair<string, string>(pair[0].Trim(), pair[1].Trim());
            },
            validator: static (IEnumerable<KeyValuePair<string, string>>? value, out IEnumerable<KeyValuePair<string, string>>? validValue) =>
            {
                if (value is null)
                {
                    validValue = null;
                    return true;
                }

                var hasError = false;
                var dic = new Dictionary<string, string>();
                foreach (var pair in value)
                {
                    var featureName = pair.Key;
                    var featureText = pair.Value;
                    if (SyntaxFacts.IsValidIdentifier(featureName))
                    {
                        if (dic.ContainsKey(featureName))
                            dic[featureName] = featureText;
                        else
                            dic.Add(featureName, featureText);
                    }
                    else
                        hasError = true;
                }
                validValue = dic;
                return !hasError;
            });
    }

    public override SyntaxTree Parse(SyntaxParserExecutionContext context)
    {
        return SyntaxFactory.ParseSyntaxTree(context.SourceText, GetParseOptions(context.ParseOptions), context.FilePath, context.CancellationToken);
    }

    private static ParseOptions GetParseOptions(IDictionary<string, object?> options)
    {
        var result = CSharpParseOptions.Default;
        foreach (var pair in options)
        {
            var value = pair.Value;
            switch (pair.Key)
            {
                case nameof(CSharpParseOptions.LanguageVersion):
                    Debug.Assert(value is not null);
                    result = result.WithLanguageVersion((LanguageVersion)value);
                    break;

                case nameof(ParseOptions.Kind):
                    Debug.Assert(value is not null);
                    result = result.WithKind((SourceCodeKind)value);
                    break;

                case nameof(ParseOptions.DocumentationMode):
                    Debug.Assert(value is not null);
                    result = result.WithDocumentationMode((DocumentationMode)value);
                    break;

                case nameof(ParseOptions.PreprocessorSymbolNames):
                    result = result.WithPreprocessorSymbols(value as IEnumerable<string>);
                    break;

                case nameof(ParseOptions.Features):
                    result = result.WithFeatures(value as IEnumerable<KeyValuePair<string, string>>);
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        return result;
    }
}
