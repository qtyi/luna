namespace Luna.Compilers.Simulators;

/// <summary>
/// 模拟器特性基类，表示附加的类实现为一种模拟器。
/// </summary>
public abstract class SimulatorAttribute : Attribute
{
    /// <summary>
    /// 模拟器支持的所有语言名称。
    /// </summary>
    public string[] Languages { get; }

    /// <summary>
    /// 附加在类定义上，指示此类是一种模拟器，并收集其支持的所有语言名称。
    /// </summary>
    /// <param name="firstLanguage">词法器模拟器支持的一种语言名称。</param>
    /// <param name="additionalLanguages">词法器模拟器支持的其他语言名称。</param>
    protected SimulatorAttribute(string firstLanguage, params string[] additionalLanguages)
    {
        if (string.IsNullOrEmpty(firstLanguage)) throw new ArgumentException($"“{nameof(firstLanguage)}”不能为 null 或空。", nameof(firstLanguage));
        if (additionalLanguages is null) throw new ArgumentNullException(nameof(additionalLanguages));

        var languages = new List<string>(additionalLanguages.Length + 1);
        languages.Add(firstLanguage);
        for (int index = 0; index < additionalLanguages.Length; index++)
        {
            var additionalLanguage = additionalLanguages[index];
            if (string.IsNullOrEmpty(additionalLanguage)) throw new ArgumentException($"“{nameof(additionalLanguages)}”的第 {index} 项不能为 null 或空。");

            languages.Add(additionalLanguage);
        }

        this.Languages = languages.ToArray();
    }

}
