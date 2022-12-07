using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Luna.Compilers.Simulators;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Win32;

namespace Luna.Compilers.Tools.ViewModels;

[ObservableObject]
internal partial class MainViewModel
{
    public ILexerSimulator? Simulator { get; set; } = null;

    [ObservableProperty]
    private SourceText? sourceText = null;

    [RelayCommand]
    private void OpenSource()
    {
        var dialog = new OpenFileDialog()
        {
            CheckFileExists = true,
            CheckPathExists = true,
            Filter = "Lua 文件 (*.lua)|*.lua|MoonScript 文件 (*.moon)|*.moon|所有文件(*.*)|*.*",
            FilterIndex = 3,
            Multiselect = false,
            ShowReadOnly = false,
            Title = "打开文件",
            ValidateNames = true
        };
        if (dialog.ShowDialog() == true)
        {
            if (Simulators.Simulator.TryGetLexerSimulatorByFileExtension(Path.GetExtension(dialog.FileName), out var lexerSimulators))
                this.Simulator = lexerSimulators[0];
            this.SourceText = SourceText.From(dialog.OpenFile());
        }
    }
}
