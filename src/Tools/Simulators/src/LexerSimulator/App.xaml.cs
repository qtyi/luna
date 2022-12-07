using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using Luna.Compilers.Simulators;

namespace Luna.Compilers.Tools;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void App_Startup(object sender, StartupEventArgs e)
    {
        Simulator.RegisterSimulatorFromConfiguration(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "config.json"));
    }
}
