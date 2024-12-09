// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Syntax;

using Luna.Compilers.Generators.Model;
using Model;

/// <summary>
/// Represents a writer that is the base class of all syntax tree file writer.
/// </summary>
internal abstract class SyntaxFileWriter : TreeFileWriter<SyntaxTree, SyntaxTreeType>
{
    private readonly Dictionary<string, Node> _nodeMap;

    protected string ThisLanguageName { get; }

    protected SyntaxFileWriter(TextWriter writer, SyntaxSourceProductionContext context) : base(writer, context.SyntaxTree)
    {
        ThisLanguageName = context.ThisLanguageName;
        this._nodeMap = context.SyntaxTree.Types.OfType<Node>().ToDictionary(static n => n.Name);
    }

    #region 帮助方法
    protected static string OverrideOrNewModifier(Field field)
    {
        return field.IsOverride() ? "override " : field.IsNew() ? "new " : "";
    }

    protected static bool CanBeField(Field field)
    {
        return field.Type != "SyntaxToken" && !field.Type.IsAnyList() && !field.IsOverride() && !field.IsNew();
    }

    protected static string GetFieldType(Field field, bool green)
    {
        // Fields in red trees are lazily initialized, with null as the uninitialized value
        return getNullableAwareType(field.Type, optionalOrLazy: field.IsOptional() || !green, green);

        static string getNullableAwareType(string fieldType, bool optionalOrLazy, bool green)
        {
            if (fieldType.IsAnyList())
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
            || derivedType.IsAnyNodeList()
                && IsDerivedType(baseType, GetElementType(derivedType));
    }

    protected bool IsNodeOrNodeList(string typeName)
    {
        return IsNode(typeName) || typeName.IsAnyList();
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

    protected bool IsNode(string typeName)
    {
        return this.ParentMap.ContainsKey(typeName);
    }

    protected Node? GetNode(string? typeName)
        => typeName is not null && _nodeMap.TryGetValue(typeName, out var node) ? node : null;

    protected static bool HasErrors(Node n)
    {
        return n.Errors is null || string.Compare(n.Errors, "true", true) == 0;
    }

    protected List<Kind> GetKindsOfFieldOrNearestParent(SyntaxTreeType type, Field field)
    {
        while ((field.Kinds is null || field.Kinds.Count == 0) && field.IsOverride())
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

    /// <inheritdoc cref="IndentWriter.CamelCase(string)"/>
    /// <remarks>Results name is escaped and is not conflict with C# keywords.</remarks>
    protected static new string CamelCase(string name)
        => IndentWriter.CamelCase(name).FixKeyword();
    #endregion
}
