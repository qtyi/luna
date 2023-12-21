// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators.Syntax;

internal abstract class SyntaxInfoList<TSyntax, TSyntaxInfo> : IReadOnlyList<TSyntaxInfo>
    where TSyntaxInfo : SyntaxNodeOrTokenOrTriviaInfo
{
    private readonly IReadOnlyList<TSyntax> _underlying;
    private readonly TSyntaxInfo?[] _infos;

    protected SyntaxInfoList(IReadOnlyList<TSyntax> underlying)
    {
        this._underlying = underlying;
        this._infos = new TSyntaxInfo?[underlying.Count];
    }

    protected abstract TSyntaxInfo GetSyntaxInfo(TSyntax syntax);

    private TSyntaxInfo GetAndCache(int index)
    {
        var info = this._infos[index];
        if (info is null)
        {
            info = this.GetSyntaxInfo(this._underlying[index]);
            Interlocked.CompareExchange(ref this._infos[index], info, null);
        }

        return info;
    }

    public TSyntaxInfo this[int index] => this.GetAndCache(index);

    public int Count => this._underlying.Count;

    public IEnumerator<TSyntaxInfo> GetEnumerator()
    {
        for (int i = 0, count = this._underlying.Count; i < count; i++)
            yield return this.GetAndCache(i);
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

internal sealed class SyntaxNodeOrTokenInfoList : SyntaxInfoList<SyntaxNodeOrToken, SyntaxNodeOrTokenInfo>
{
    public SyntaxNodeOrTokenInfoList(IReadOnlyList<SyntaxNodeOrToken> underlying) : base(underlying) { }

    protected override SyntaxNodeOrTokenInfo GetSyntaxInfo(SyntaxNodeOrToken syntax) => (SyntaxNodeOrTokenInfo)syntax;
}

internal sealed class SyntaxNodeInfoList : SyntaxInfoList<SyntaxNode, SyntaxNodeInfo>
{
    public SyntaxNodeInfoList(IReadOnlyList<SyntaxNode> underlying) : base(underlying) { }

    protected override SyntaxNodeInfo GetSyntaxInfo(SyntaxNode syntax) => (SyntaxNodeInfo)syntax;
}

internal sealed class SyntaxTokenInfoList : SyntaxInfoList<SyntaxToken, SyntaxTokenInfo>
{
    public SyntaxTokenInfoList(IReadOnlyList<SyntaxToken> underlying) : base(underlying) { }

    protected override SyntaxTokenInfo GetSyntaxInfo(SyntaxToken syntax) => (SyntaxTokenInfo)syntax;
}

internal sealed class SyntaxTriviaInfoList : SyntaxInfoList<SyntaxTrivia, SyntaxTriviaInfo>
{
    public SyntaxTriviaInfoList(IReadOnlyList<SyntaxTrivia> underlying) : base(underlying) { }

    protected override SyntaxTriviaInfo GetSyntaxInfo(SyntaxTrivia syntax) => (SyntaxTriviaInfo)syntax;
}
