// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;
using Luna.Compilers.Generators.Model;
using XmlObjectWithParseContext<TXmlObject, TOutput> = (TXmlObject xmlObject, Luna.Compilers.Generators.Model.XmlParser<TOutput>.ParseContext context) where TXmlObject : System.Xml.Linq.XObject;
using Microsoft.CodeAnalysis.CSharp;

namespace Luna.Compilers.Generators.Syntax.Model;

[Obsolete]
internal sealed class SyntaxXmlParser<TOutput> : XmlParser<TOutput>
{
    private StringComparer NameComparer { get; }

    public SyntaxXmlParser(StringComparer nameComparer)
    {
        NameComparer = nameComparer;
    }

    protected override TOutput? ParseDocuments(ImmutableArray<XmlObjectWithParseContext<XDocument, TOutput>> documents, DiagnosticBag diagnosticBag)
    {
        if (documents.IsDefaultOrEmpty)
            return default;

        var pool = PooledDictionary<string, ArrayBuilder<XmlObjectWithParseContext<XAttribute, TOutput>>>.CreatePool(NameComparer);
        // The declaration map, from name to XML objects.
        var declMap = pool.Allocate();
        foreach ((var document, var context) in documents)
        {
            GetDeclarationsFromDocument(context, document, declMap, diagnosticBag);
        }

        if (declMap.Count == 0)
        {
            pool.Free(declMap);
            return default;
        }

        CheckDeclarations(declMap, diagnosticBag);

        return default;
    }

    private void GetDeclarationsFromDocument(ParseContext context, XDocument document, Dictionary<string, ArrayBuilder<XmlObjectWithParseContext<XAttribute, TOutput>>> declMap, DiagnosticBag diagnosticBag)
    {
        var root = document.Root;
        if (root is null)
            return;

        Debug.Assert(IsName(root.Name, "Tree"));
        if (IsSingleAttributeOrReportDiagnostic(context, root, "Root", diagnosticBag, out var rootAttr) &&
            IsValidIdentifierOrReportDiagnostics(context, rootAttr, diagnosticBag))
        {
            declMap.GetOrAdd(rootAttr.Value, ArrayBuilder<XmlObjectWithParseContext<XAttribute, TOutput>>.GetInstance()).Add((rootAttr, context));
        }

        foreach (var element in root.Elements())
        {
            if (IsName(element.Name, "PredefinedNode") ||
                IsName(element.Name, "AbstractNode") ||
                IsName(element.Name, "Node"))
            {
                if (IsSingleAttributeOrReportDiagnostic(context, element, "Name", diagnosticBag, out var nameAttr) &&
                    IsValidIdentifierOrReportDiagnostics(context, nameAttr, diagnosticBag))
                {
                    declMap.GetOrAdd(nameAttr.Value, ArrayBuilder<XmlObjectWithParseContext<XAttribute, TOutput>>.GetInstance()).Add((nameAttr, context));
                }
            }
        }
    }

    private void CheckDeclarations(Dictionary<string, ArrayBuilder<XmlObjectWithParseContext<XAttribute, TOutput>>> declMap, DiagnosticBag diagnosticBag)
    {
        var root = ImmutableArray.CreateBuilder<XmlObjectWithParseContext<XAttribute, TOutput>>();
        foreach ((var nodeName, var objects) in declMap)
        {
            foreach ((var attr, var context) in objects)
            {
                if (IsName(attr.Name, "Root"))
                {
                    root.Add((attr, context));
                }
            }

            var nodeDecl = objects.Select(attr => attr.xmlObject.Parent.Name.LocalName).Distinct(StringComparer.OrdinalIgnoreCase);
            if (nodeDecl.IsSingle())
            {
                ReportXmlExceptionDiagnostic(
                    diagnosticBag,
                    $"Duplicate node '{nodeName}' declarations.",
                    objects.SelectAsArray(static tuple => GetLocation(tuple.context, tuple.xmlObject))
                );
            }

            throw new NotImplementedException();
        }

        if (!root.Select(attr => attr.xmlObject.Value).Distinct(NameComparer).IsSingle())
        {
            ReportXmlExceptionDiagnostic(
                diagnosticBag,
                "Duplicate root node.",
                root.SelectAsArray(static tuple => GetLocation(tuple.context, tuple.xmlObject))
            );
        }
    }

    private bool IsSingleAttributeOrReportDiagnostic(ParseContext context, XElement element, XName name, DiagnosticBag diagnosticBag, [NotNullWhen(true)] out XAttribute? obj)
        => IsSingleOrReportDiagnostic(context, element.Attributes(name).ToImmutableArray(), $"attribute '{name.LocalName}'", diagnosticBag, out obj);

    private bool IsSingleOrReportDiagnostic<TXObject>(ParseContext context, ImmutableArray<TXObject> objects, string message, DiagnosticBag diagnosticBag, [NotNullWhen(true)] out TXObject? obj)
        where TXObject : XObject
    {
        if (objects.IsEmpty)
        {
            ReportXmlExceptionDiagnostic(diagnosticBag, context, $"Missing {message}.", objects);
            obj = null;
            return false;
        }
        else if (objects.Length > 1)
        {
            ReportXmlExceptionDiagnostic(diagnosticBag, context, $"Duplicate {message}.", objects);
            obj = null;
            return false;
        }

        obj = objects[0];
        return true;
    }

    private bool IsValidIdentifierOrReportDiagnostics(ParseContext context, XAttribute name, DiagnosticBag diagnosticBag)
    {
        if (!IsValidIdentifier(name.Value))
        {
            ReportXmlExceptionDiagnostic(diagnosticBag, context, $"'{name.Value}' is not a valid identifier", [name]);
            return false;
        }

        return true;
    }

    private bool IsValidIdentifier(string name) => SyntaxFacts.IsValidIdentifier(name);

    private static bool IsName(XName name, string expected) => StringComparer.OrdinalIgnoreCase.Equals(expected, name.LocalName);
}
