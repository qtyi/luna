using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

public readonly struct SimulatorContext
{
    private readonly string _languageName;

    public string LanguageName => this._languageName;

    internal SimulatorContext(string languageName)
    {
        this._languageName = languageName;
    }
}
