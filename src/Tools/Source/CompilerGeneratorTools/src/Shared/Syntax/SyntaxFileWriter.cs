// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Syntax;

using System.Runtime.CompilerServices;
using Model;

internal abstract class SyntaxFileWriter : TreeFileWriter<SyntaxTree, SyntaxTreeType, SyntaxTreeTypeChild>
{
    private readonly IDictionary<string, Node> _nodeMap;

    protected SyntaxFileWriter(TextWriter writer, SyntaxTree tree, CancellationToken cancellationToken) : base(writer, tree, cancellationToken)
    {
        _nodeMap = tree.Types.OfType<Node>().ToDictionary(n => n.Name);
    }

    #region 帮助方法
    protected static string OverrideOrNewModifier(Field field)
    {
        return IsOverride(field) ? "override " : IsNew(field) ? "new " : "";
    }

    protected static bool CanBeField(Field field)
    {
        return field.Type != "SyntaxToken" && !IsAnyList(field.Type) && !IsOverride(field) && !IsNew(field);
    }

    protected static string GetFieldType(Field field, bool green)
    {
        // Fields in red trees are lazily initialized, with null as the uninitialized value
        return getNullableAwareType(field.Type, optionalOrLazy: IsOptional(field) || !green, green);

        static string getNullableAwareType(string fieldType, bool optionalOrLazy, bool green)
        {
            if (IsAnyList(fieldType))
            {
                if (optionalOrLazy)
                    return green ? "GreenNode?" : "SyntaxNode?";
                else
                    return green ? "GreenNode?" : "SyntaxNode";
            }

            switch (fieldType)
            {
                case var _ when !optionalOrLazy:
                    return fieldType;

                case "bool":
                case "SyntaxToken" when !green:
                    return fieldType;

                default:
                    return fieldType + "?";
            }
        }
    }

    protected bool IsDerivedOrListOfDerived(string baseType, string derivedType)
    {
        return IsDerivedType(baseType, derivedType)
            || ((IsNodeList(derivedType) || IsSeparatedNodeList(derivedType))
                && IsDerivedType(baseType, GetElementType(derivedType)));
    }

    protected static bool IsSeparatedNodeList(string typeName)
    {
        return typeName.StartsWith("SeparatedSyntaxList<", StringComparison.Ordinal);
    }

    protected static bool IsNodeList(string typeName)
    {
        return typeName.StartsWith("SyntaxList<", StringComparison.Ordinal);
    }

    public static bool IsAnyNodeList(string typeName)
    {
        return IsNodeList(typeName) || IsSeparatedNodeList(typeName);
    }

    protected bool IsNodeOrNodeList(string typeName)
    {
        return IsNode(typeName) || IsNodeList(typeName) || IsSeparatedNodeList(typeName) || typeName == "SyntaxNodeOrTokenList";
    }

    protected static string GetElementType(string typeName)
    {
        if (!typeName.Contains("<"))
            return string.Empty;
        int iStart = typeName.IndexOf('<');
        int iEnd = typeName.IndexOf('>', iStart + 1);
        if (iEnd < iStart)
            return string.Empty;
        var sub = typeName.Substring(iStart + 1, iEnd - iStart - 1);
        return sub;
    }

    protected static bool IsAnyList(string typeName)
    {
        return IsNodeList(typeName) || IsSeparatedNodeList(typeName) || typeName == "SyntaxNodeOrTokenList";
    }

    protected bool IsNode(string typeName)
    {
        return this.ParentMap.ContainsKey(typeName);
    }

    protected Node? GetNode(string? typeName)
        => typeName is not null && _nodeMap.TryGetValue(typeName, out var node) ? node : null;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static bool IsOptional(Field f)
        => f.IsOptional();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static bool IsOverride(Field f)
        => f.IsOverride();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static bool IsNew(Field f)
        => f.IsNew();

    protected static bool HasErrors(Node n)
    {
        return n.Errors is null || string.Compare(n.Errors, "true", true) == 0;
    }

    protected List<Kind> GetKindsOfFieldOrNearestParent(SyntaxTreeType type, Field field)
    {
        while ((field.Kinds is null || field.Kinds.Count == 0) && IsOverride(field))
        {
            var t = GetTreeType(type.Base);
            field = (t switch
            {
                Node node => node.Fields,
                AbstractNode abstractNode => abstractNode.Fields,
                _ => throw new InvalidOperationException("Unexpected node type.")
            }).Single(f => f.Name == field.Name);
            type = t!;
        }

        return field.Kinds.Distinct().ToList();
    }
    #endregion
}
