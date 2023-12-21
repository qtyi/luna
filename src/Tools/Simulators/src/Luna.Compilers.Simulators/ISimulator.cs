namespace Luna.Compilers.Simulators;

public interface ISimulator
{
    ISyntaxInfoProvider SyntaxInfoProvider { get; }

    void Initialize(SimulatorContext context);
}
