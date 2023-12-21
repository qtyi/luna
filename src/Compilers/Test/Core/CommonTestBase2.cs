// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.CodeGen;

namespace Qtyi.CodeAnalysis.Test.Utilities;

public abstract partial class CommonTestBase : Microsoft.CodeAnalysis.Test.Utilities.CommonTestBase
{
    #region IL Validation
    internal sealed override string VisualizeRealIL(Microsoft.CodeAnalysis.IModuleSymbol peNetmodule, CompilationTestData.MethodData methodData, IReadOnlyDictionary<int, string> markers, bool areLocalsZeroed) => this.VisualizeRealIL((INetmoduleSymbol)peNetmodule, methodData, markers, areLocalsZeroed);

    internal abstract string VisualizeRealIL(INetmoduleSymbol peNetmodule, CompilationTestData.MethodData methodData, IReadOnlyDictionary<int, string> markers, bool areLocalsZeroed);
    #endregion
}
