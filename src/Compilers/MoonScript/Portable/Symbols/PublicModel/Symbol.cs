// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

partial class Symbol
{
    internal static MoonScriptSemanticModel GetMoonScriptSemanticModel<TSemanticModel>(TSemanticModel semanticModel) where TSemanticModel : Microsoft.CodeAnalysis.SemanticModel
    {
        var moonScriptModel = semanticModel as MoonScriptSemanticModel;
        if (moonScriptModel is null)
            throw new ArgumentException(MoonScriptResources.WrongSemanticModelType, LanguageNames.MoonScript);

        return moonScriptModel;
    }

    #region Microsoft.CodeAnalysis.ISymbol
    string Microsoft.CodeAnalysis.ISymbol.Language => LanguageNames.MoonScript;

    ImmutableArray<Microsoft.CodeAnalysis.SymbolDisplayPart> Microsoft.CodeAnalysis.ISymbol.ToMinimalDisplayParts(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayParts(this, GetMoonScriptSemanticModel(semanticModel), position, format);
    string Microsoft.CodeAnalysis.ISymbol.ToMinimalDisplayString(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayString(this, GetMoonScriptSemanticModel(semanticModel), position, format);
    #endregion

    #region Microsoft.CodeAnalysis.ISymbol
    ImmutableArray<Microsoft.CodeAnalysis.SymbolDisplayPart> ISymbol.ToMinimalDisplayParts(SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayParts(this, GetMoonScriptSemanticModel(semanticModel), position, format);
    string ISymbol.ToMinimalDisplayString(SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayString(this, GetMoonScriptSemanticModel(semanticModel), position, format);
    #endregion
}
