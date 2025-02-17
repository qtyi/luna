﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.Lua;

#pragma warning disable CS0659
partial class LuaCompilationOptions
#pragma warning restore CS0659
{
    /// <value>
    /// Returns <see cref="LanguageNames.Lua"/>.
    /// </value>
    /// <inheritdoc/>
    public override string Language => LanguageNames.Lua;

    #region
#warning 未实现
    internal override partial Diagnostic? FilterDiagnostic(Diagnostic diagnostic, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override partial ImmutableArray<string> GetImports() => ImmutableArray<string>.Empty;

    internal override partial void ValidateOptions(ArrayBuilder<Diagnostic> builder)
    {
        ValidateOptions(builder, ThisMessageProvider.Instance);

        throw new NotImplementedException();
    }
    #endregion

    public partial bool Equals(ThisCompilationOptions? other)
    {
        if (ReferenceEquals(this, other)) return true;

        if (!EqualsHelper(other)) return false;

        return true;
    }

    protected override partial int ComputeHashCode()
    {
        return GetHashCodeHelper();
    }
}
