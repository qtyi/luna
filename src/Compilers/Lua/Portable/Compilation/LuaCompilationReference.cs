// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaCompilationReference
{
    #region Debugger Display
    private partial string GetDebugDisplay() => LuaResources.CompilationLua + this.Compilation.AssemblyName;
    #endregion
}
