// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

partial class Symbol
{
    internal static ThisSemanticModel GetMoonScriptSemanticModel<TSemanticModel>(TSemanticModel semanticModel) where TSemanticModel : Microsoft.CodeAnalysis.SemanticModel
    {
        var moonScriptModel = semanticModel as ThisSemanticModel;
        if (moonScriptModel is null)
            throw new ArgumentException(ThisResources.WrongSemanticModelType, LanguageNames.MoonScript);

        return moonScriptModel;
    }

    #region Microsoft.CodeAnalysis.ISymbol
    string ISymbol.Language => LanguageNames.MoonScript;

    ImmutableArray<SymbolDisplayPart> ISymbol.ToMinimalDisplayParts(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayParts(this, GetMoonScriptSemanticModel(semanticModel), position, format);
    string ISymbol.ToMinimalDisplayString(Microsoft.CodeAnalysis.SemanticModel semanticModel, int position, SymbolDisplayFormat? format) => SymbolDisplay.ToMinimalDisplayString(this, GetMoonScriptSemanticModel(semanticModel), position, format);
    #endregion
}
