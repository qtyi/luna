// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Generators.Model;
using Luna.Compilers.Generators.Syntax.Model;

namespace Luna.Compilers.Generators;

internal abstract class TreeFileWriter<TTree, TTreeType, TTreeTypeChild> : IndentWriter
    where TTree : ITree<TTreeType, TTreeTypeChild>
    where TTreeType : ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
    private readonly TTree _tree;
    private readonly IDictionary<string, string?> _parentMap;
    private readonly ILookup<string, string> _childMap;

    private readonly IDictionary<string, TTreeType> _typeMap;

    protected IDictionary<string, string?> ParentMap => this._parentMap;
    protected ILookup<string, string> ChildMap => this._childMap;
    protected TTree Tree => this._tree;

    protected TreeFileWriter(TextWriter writer, TTree tree, CancellationToken cancellationToken) : base(writer, 4, cancellationToken)
    {
        _tree = tree;
        _typeMap = tree.Types.ToDictionary(n => n.Name);
        _parentMap = tree.Types.ToDictionary(n => n.Name, n => n.Base);
        _parentMap.Add(tree.Root, null);
        _childMap = tree.Types.Where(n => n.Base is not null).ToLookup(n => n.Base!, n => n.Name);
    }

    #region 帮助方法
    protected bool IsDerivedType(string typeName, string? derivedTypeName)
    {
        if (typeName == derivedTypeName)
            return true;
        if (derivedTypeName is not null && _parentMap.TryGetValue(derivedTypeName, out var baseType))
        {
            return IsDerivedType(typeName, baseType);
        }
        return false;
    }

    protected TTreeType? GetTreeType(string? typeName)
        => typeName is not null && _typeMap.TryGetValue(typeName, out var type) ? type : default;

    protected static bool IsTrue(string? val)
        => val is not null && string.Compare(val, "true", true) == 0;

    protected static string CamelCase(string name)
    {
        if (char.IsUpper(name[0]))
        {
            name = char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
        return FixKeyword(name);
    }

    protected static string FixKeyword(string name)
    {
        if (IsKeyword(name))
        {
            return "@" + name;
        }
        return name;
    }

    protected static string StripPost(string name, string post)
    {
        return name.EndsWith(post, StringComparison.Ordinal)
            ? name.Substring(0, name.Length - post.Length)
            : name;
    }

    protected static bool IsKeyword(string name) => name is
        "bool" or
        "byte" or
        "sbyte" or
        "short" or
        "ushort" or
        "int" or
        "uint" or
        "long" or
        "ulong" or
        "double" or
        "float" or
        "decimal" or
        "string" or
        "char" or
        "object" or
        "typeof" or
        "sizeof" or
        "null" or
        "true" or
        "false" or
        "if" or
        "else" or
        "while" or
        "for" or
        "foreach" or
        "do" or
        "switch" or
        "case" or
        "default" or
        "lock" or
        "try" or
        "throw" or
        "catch" or
        "finally" or
        "goto" or
        "break" or
        "continue" or
        "return" or
        "public" or
        "private" or
        "internal" or
        "protected" or
        "static" or
        "readonly" or
        "sealed" or
        "const" or
        "new" or
        "override" or
        "abstract" or
        "virtual" or
        "partial" or
        "ref" or
        "out" or
        "in" or
        "where" or
        "params" or
        "this" or
        "base" or
        "namespace" or
        "using" or
        "class" or
        "struct" or
        "interface" or
        "delegate" or
        "checked" or
        "get" or
        "set" or
        "add" or
        "remove" or
        "operator" or
        "implicit" or
        "explicit" or
        "fixed" or
        "extern" or
        "event" or
        "enum" or
        "unsafe";
    #endregion
}
