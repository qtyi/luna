// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript;

partial class Symbol
{
    /// <summary>
    /// 此符号是否能在代码中通过名称引用。
    /// </summary>
    /// <value>
    /// 若此符号是否能在代码中通过名称引用，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    public bool CanBeReferencedByName
    {
        get
        {
            switch (this.Kind)
            {
                case SymbolKind.Local:
                case SymbolKind.Label:
                    return true;

                case SymbolKind.Assembly:
                case SymbolKind.DynamicType:
                case SymbolKind.Netmodule:
                    return false;

                case SymbolKind.Namespace:
                case SymbolKind.Field:
                case SymbolKind.Parameter:
                case SymbolKind.NamedType:
                    break;

                default:
                    throw ExceptionUtilities.UnexpectedValue(this.Kind);
            }

            return SyntaxFacts.IsValidIdentifier(this.Name);
        }
    }
}
