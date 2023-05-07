// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript;

/// <summary>
/// Declare all kinds of MoonScript declaration.
/// </summary>
internal enum DeclarationKind : byte
{
    /// <summary>A module declaration.</summary>
    Module,
    /// <summary>A class declaration.</summary>
    Class,
    /// <summary>A script declaration.</summary>
    Script,
    /// <summary>A script submission declaration.</summary>
    Submission
}

internal static partial class EnumConversions
{
    /// <summary>
    /// Get the <see cref="DeclarationKind"/> which a specified <see cref="SyntaxKind"/> associated with.
    /// </summary>
    /// <param name="kind">The syntax kind.</param>
    /// <returns>The declaration kind which <paramref name="kind"/> associated with.</returns>
    internal static DeclarationKind ToDeclarationKind(this SyntaxKind kind) => kind switch
    {
        SyntaxKind.Chunk => DeclarationKind.Module,
        SyntaxKind.ClassStatement or
        SyntaxKind.ClassExpression or
        SyntaxKind.AnomymousClassExpression => DeclarationKind.Class,
        _ => throw ExceptionUtilities.UnexpectedValue(kind),
    };
}
