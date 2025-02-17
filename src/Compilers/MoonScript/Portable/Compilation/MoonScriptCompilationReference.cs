﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MoonScriptCompilationReference
{
    #region Debugger Display
    private partial string GetDebugDisplay() => MoonScriptResources.CompilationMoonScript + Compilation.AssemblyName;
    #endregion
}
