// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

partial class Symbol
{
    internal static LuaSemanticModel GetLuaSemanticModel<TSemanticModel>(TSemanticModel semanticModel) where TSemanticModel : Microsoft.CodeAnalysis.SemanticModel
    {
        var luaModel = semanticModel as LuaSemanticModel;
        if (luaModel is null)
            throw new ArgumentException(LuaResources.WrongSemanticModelType, LanguageNames.Lua);

        return luaModel;
    }

    #region Microsoft.CodeAnalysis.ISymbol
    string Microsoft.CodeAnalysis.ISymbol.Language => LanguageNames.Lua;

    ImmutableArray<Microsoft.CodeAnalysis.SymbolDisplayPart> Microsoft.CodeAnalysis.ISymbol.ToMinimalDisplayParts(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayParts(this, GetLuaSemanticModel(semanticModel), position, format);
    string Microsoft.CodeAnalysis.ISymbol.ToMinimalDisplayString(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayString(this, GetLuaSemanticModel(semanticModel), position, format);
    #endregion


    #region Microsoft.CodeAnalysis.ISymbol
    ImmutableArray<Microsoft.CodeAnalysis.SymbolDisplayPart> ISymbol.ToMinimalDisplayParts(SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayParts(this, GetLuaSemanticModel(semanticModel), position, format);
    string ISymbol.ToMinimalDisplayString(SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayString(this, GetLuaSemanticModel(semanticModel), position, format);
    #endregion
}
