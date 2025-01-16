// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

public readonly struct ForInStatementInfo : IEquatable<ForInStatementInfo>
{
    private readonly SymbolInfo _controlVariableInfo;
    private readonly SymbolInfo _iteratorFunctionInfo;
    private readonly SymbolInfo _stateInfo;
    private readonly SymbolInfo _initialValueInfo;
    private readonly SymbolInfo _closingValueInfo;

    public SymbolInfo ControlVariableInfo => _controlVariableInfo;

    public SymbolInfo IteratorFunctionInfo => _iteratorFunctionInfo;

    public SymbolInfo StateInfo => _stateInfo;

    public SymbolInfo InitialValueInfo => _initialValueInfo;

    public SymbolInfo ClosingValueInfo => _closingValueInfo;

    public ForInStatementInfo(
        SymbolInfo controlVariableInfo,
        SymbolInfo iteratorFunctionInfo,
        SymbolInfo stateInfo,
        SymbolInfo initialValueInfo,
        SymbolInfo closingValueInfo)
    {
        _controlVariableInfo = controlVariableInfo;
        _iteratorFunctionInfo = iteratorFunctionInfo;
        _stateInfo = stateInfo;
        _initialValueInfo = initialValueInfo;
        _closingValueInfo = closingValueInfo;
    }

    public override bool Equals(object? obj) => obj is ForInStatementInfo info && Equals(info);

    public bool Equals(ForInStatementInfo other) =>
        _controlVariableInfo.Equals(other._controlVariableInfo) &&
        _iteratorFunctionInfo.Equals(other._iteratorFunctionInfo) &&
        _stateInfo.Equals(other._stateInfo) &&
        _initialValueInfo.Equals(other._initialValueInfo) &&
        _closingValueInfo.Equals(other._closingValueInfo);

    public override int GetHashCode() =>
        Hash.CombineValues(ImmutableArray.Create(
            _controlVariableInfo,
            _iteratorFunctionInfo,
            _stateInfo,
            _initialValueInfo,
            _closingValueInfo
        ));
}
