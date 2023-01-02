// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

public readonly struct ForInStatementInfo : IEquatable<ForInStatementInfo>
{
    private readonly SymbolInfo _controlVariableInfo;
    private readonly SymbolInfo _iteratorFunctionInfo;
    private readonly SymbolInfo _stateInfo;
    private readonly SymbolInfo _initialValueInfo;
    private readonly SymbolInfo _closingValueInfo;

    public SymbolInfo ControlVariableInfo => this._controlVariableInfo;

    public SymbolInfo IteratorFunctionInfo => this._iteratorFunctionInfo;

    public SymbolInfo StateInfo => this._stateInfo;

    public SymbolInfo InitialValueInfo => this._initialValueInfo;

    public SymbolInfo ClosingValueInfo => this._closingValueInfo;

    public ForInStatementInfo(
        SymbolInfo controlVariableInfo,
        SymbolInfo iteratorFunctionInfo,
        SymbolInfo stateInfo,
        SymbolInfo initialValueInfo,
        SymbolInfo closingValueInfo)
    {
        this._controlVariableInfo = controlVariableInfo;
        this._iteratorFunctionInfo = iteratorFunctionInfo;
        this._stateInfo = stateInfo;
        this._initialValueInfo = initialValueInfo;
        this._closingValueInfo = closingValueInfo;
    }

    public override bool Equals(object? obj) => obj is ForInStatementInfo info && this.Equals(info);

    public bool Equals(ForInStatementInfo other) =>
        this._controlVariableInfo.Equals(other._controlVariableInfo) &&
        this._iteratorFunctionInfo.Equals(other._iteratorFunctionInfo) &&
        this._stateInfo.Equals(other._stateInfo) &&
        this._initialValueInfo.Equals(other._initialValueInfo) &&
        this._closingValueInfo.Equals(other._closingValueInfo);

    public override int GetHashCode() =>
        Hash.CombineValues(ImmutableArray.Create(
            this._controlVariableInfo,
            this._iteratorFunctionInfo,
            this._stateInfo,
            this._initialValueInfo,
            this._closingValueInfo
        ));
}
