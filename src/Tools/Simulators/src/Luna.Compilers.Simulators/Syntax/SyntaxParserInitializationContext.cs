// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;

namespace Luna.Compilers.Simulators;

public readonly struct SyntaxParserInitializationContext
{
    private readonly Lazy<HashSet<string>> _names = new();
    private readonly Lazy<Dictionary<string, IEnumerable>> _radioOptions = new();
    private readonly Lazy<Dictionary<string, (IEnumerable values, Delegate validator)>> _checkedOptions = new();
    private readonly Lazy<Dictionary<string, (Delegate converter, Delegate validator)>> _validator = new();

    public SyntaxParserInitializationContext() { }

    public void RegisterRadioParseOption<T>(string name, T values) where T : IEnumerable
    {
        this.ThrowIfAlreadyRegistered(name);
        this._names.Value.Add(name);
        this._radioOptions.Value.Add(name, values);
    }

    public void RegisterCheckedParseOption<T>(string name, T values, ValueValidator<T> validator) where T : IEnumerable
    {
        this.ThrowIfAlreadyRegistered(name);
        this._names.Value.Add(name);
        this._checkedOptions.Value.Add(name, (values, validator));
    }

    public void RegisterComplexParseOption<T>(string name, Func<string, T?> converter, ValueValidator<T> validator)
    {
        this.ThrowIfAlreadyRegistered(name);
        this._names.Value.Add(name);
        this._validator.Value.Add(name, (converter, validator));
    }

    private void ThrowIfAlreadyRegistered(string name)
    {
        if (this._names.Value.Contains(name))
            throw new InvalidOperationException($"Already registered option '{name}'");
    }
}
