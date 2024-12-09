// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Threading;
using Luna.Compilers.Generators.CSharp;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.CompilationOptions;

using Model;

internal abstract class CompilationOptionsFileWriter : TreeFileWriter<OptionList, Option>
{
    protected CompilationOptionsFileWriter(TextWriter writer, OptionList tree, CancellationToken cancellationToken) : base(writer, tree, cancellationToken) { }

    #region Helper Methods
    /// <inheritdoc cref="IndentWriter.CamelCase(string)"/>
    /// <remarks>Results name is escaped and is not conflict with C# keywords.</remarks>
    protected static new string CamelCase(string name)
        => IndentWriter.CamelCase(name).FixKeyword();
    #endregion
}
