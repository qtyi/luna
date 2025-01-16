// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal abstract class MissingMetadataTypeSymbol : ErrorTypeSymbol
{
#warning 未完成
    internal sealed class TopLevel : MissingMetadataTypeSymbol
    {
        private NetmoduleSymbol _netmoduleSymbol;
        private MetadataTypeName _emittedName;
        private DiagnosticInfo? _diagnosticInfo;

        public TopLevel(NetmoduleSymbol netmoduleSymbol, ref MetadataTypeName emittedName, DiagnosticInfo? diagnosticInfo = null)
        {
            throw new NotImplementedException();
        }

        public TopLevel(NetmoduleSymbol netmoduleSymbol, ref MetadataTypeName fullName, SpecialType specialType, DiagnosticInfo? errorInfo = null)
        {
            throw new NotImplementedException();
        }

        public override int Arity => throw new NotImplementedException();

        internal override DiagnosticInfo? ErrorInfo => throw new NotImplementedException();
    }
}
