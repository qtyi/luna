namespace Luna.Compilers.Simulators;

/// <summary>
/// 表示附加的类实现为一个词法器模拟器。
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class LexerSimulatorAttribute : SimulatorAttribute
{
    /// <summary>
    /// 附加在类定义上，指示此类是一个词法器模拟器，并收集其支持的所有语言名称。
    /// </summary>
    /// <inheritdoc/>
    public LexerSimulatorAttribute(string firstLanguage, params string[] additionalLanguages) : base(firstLanguage, additionalLanguages) { }
}
