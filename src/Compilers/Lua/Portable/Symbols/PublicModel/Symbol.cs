// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

partial class Symbol : ISymbol
{
    internal static ThisSemanticModel GetLuaSemanticModel<TSemanticModel>(TSemanticModel semanticModel) where TSemanticModel : Microsoft.CodeAnalysis.SemanticModel
    {
        var luaModel = semanticModel as ThisSemanticModel;
        if (luaModel is null)
            throw new ArgumentException(ThisResources.WrongSemanticModelType, LanguageNames.Lua);

        return luaModel;
    }

    #region Microsoft.CodeAnalysis.ISymbol
    string ISymbol.Language => LanguageNames.Lua;

    ImmutableArray<SymbolDisplayPart> ISymbol.ToMinimalDisplayParts(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayParts(this, GetLuaSemanticModel(semanticModel), position, format);
    string ISymbol.ToMinimalDisplayString(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayString(this, GetLuaSemanticModel(semanticModel), position, format);
    #endregion
}
