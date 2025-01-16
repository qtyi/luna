// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial struct DirectiveStack
{
    public partial DefineState IsDefined(string id)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }

    public partial DirectiveStack Add(Directive directive)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }

    private static partial ConsList<Directive>? SkipInsignificantDirectives(ConsList<Directive>? directives)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }
}
