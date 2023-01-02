// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Text;
#if !NETCOREAPP || NETCOREAPP3_1
using MemberNotNullWhenAttribute = MSCA::System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute;
#endif

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <summary>
/// 提供构建词法分析器的必要成员及部分实现，此类必须被继承。
/// </summary>
internal abstract partial class AbstractLexer : IDisposable
{
    /// <summary>滑动的缓冲区域。</summary>
    internal readonly SlidingTextWindow TextWindow;
    /// <summary>词法分析过程中收集到的诊断错误信息列表。</summary>
    private List<SyntaxDiagnosticInfo>? _errors;

    /// <summary>
    /// 获取一个值，指示词法分析过程中是否收集到错误。
    /// </summary>
    /// <value>
    /// 若为<see langword="true"/>时，表示收集到错误；若为<see langword="false"/>时，表示未收集到错误。
    /// </value>
    [MemberNotNullWhen(true, nameof(AbstractLexer._errors))]
    protected bool HasErrors => this._errors is not null;

    /// <summary>
    /// 创建<see cref="AbstractLexer"/>的新实例。
    /// </summary>
    /// <param name="text">要分析的代码文本。</param>
    protected AbstractLexer(SourceText text) => this.TextWindow = new(text);

    public virtual void Dispose() => this.TextWindow.Dispose();

    /// <summary>
    /// 开始词法分析器的分析工作。
    /// </summary>
    protected void Start()
    {
        this.TextWindow.Start();
        this._errors = null;
    }

    /// <summary>
    /// 获取词法分析过程中收集到的所有诊断错误信息。
    /// </summary>
    /// <param name="leadingTriviaWidth">前方语法琐碎内容的宽度，即偏移值。</param>
    /// <returns>词法分析过程中收集到的所有诊断错误信息。</returns>
    /// <remarks>
    /// 若前方语法琐碎内容的宽度<paramref name="leadingTriviaWidth"/>不为零，则将原诊断错误信息的偏移量加上<paramref name="leadingTriviaWidth"/>构成新诊断错误信息。
    /// </remarks>
    protected SyntaxDiagnosticInfo[]? GetErrors(int leadingTriviaWidth)
    {
        if (!this.HasErrors) return null;

        if (leadingTriviaWidth > 0)
        {
            // 调整偏移量，加上起始语法琐碎内容的宽度。
            var array = new SyntaxDiagnosticInfo[this._errors.Count];
            for (int i = 0; i < this._errors.Count; i++)
                array[i] = this._errors[i].WithOffset(this._errors[i].Offset + leadingTriviaWidth);

            return array;
        }
        else return this._errors.ToArray();
    }

    protected void AddError(int position, int width, ErrorCode code) => this.AddError(this.MakeError(position, width, code));

    protected void AddError(int position, int width, ErrorCode code, params object[] args) => this.AddError(this.MakeError(position, width, code, args));

    protected void AddError(ErrorCode code) => this.AddError(AbstractLexer.MakeError(code));

    protected void AddError(ErrorCode code, params object[] args) => this.AddError(AbstractLexer.MakeError(code, args));

    protected void AddError(SyntaxDiagnosticInfo? error)
    {
        if (error is null) return;

        if (!this.HasErrors) this._errors = new(8);

        this._errors.Add(error);
    }

    protected SyntaxDiagnosticInfo MakeError(int position, int width, ErrorCode code) => new(this.GetLexemeOffsetFromPosition(position), width, code);

    protected SyntaxDiagnosticInfo MakeError(int position, int width, ErrorCode code, params object[] args) => new(this.GetLexemeOffsetFromPosition(position), width, code, args);

    private int GetLexemeOffsetFromPosition(int position) => position >= this.TextWindow.LexemeStartPosition ? position - TextWindow.LexemeStartPosition : position;

    protected static SyntaxDiagnosticInfo MakeError(ErrorCode code) => new(code);

    protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args) => new(code, args);
}
