// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Mvvm.ComponentModel;

namespace Luna.Compilers.Simulators.Syntax;

[ObservableObject]
public abstract partial class SyntaxNodeOrTokenOrTriviaInfo
{
    [ObservableProperty]
    private bool isSelected;
    [ObservableProperty]
    private bool isHighlighted;

    public abstract bool IsNode { get; }
    public abstract bool IsToken { get; }
    public abstract bool IsTrivia { get; }
}
