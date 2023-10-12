// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Symbols;

using Luna.Compilers.Generators.Model;
using Model;

internal abstract class SymbolsFileWriter : TreeFileWriter<SymbolTree, SymbolTreeType>
{
    private readonly IDictionary<string, Symbol> _symbolMap;

    protected SymbolsFileWriter(TextWriter writer, SymbolTree tree, CancellationToken cancellationToken) : base(writer, tree, cancellationToken)
    {
        _symbolMap = tree.Types.OfType<Symbol>().ToDictionary(n => n.Name);
    }

    #region 帮助方法
    protected bool IsSymbol(string typeName) => this.ParentMap.ContainsKey(typeName);

    protected Symbol? GetSymbol(string? typeName)
        => typeName is not null && _symbolMap.TryGetValue(typeName, out var node) ? node : null;

    protected IEnumerable<string> GetImplement(Symbol symbol) =>
        symbol.Implement is null ? Enumerable.Empty<string>() : symbol.Implement.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
    #endregion
}
