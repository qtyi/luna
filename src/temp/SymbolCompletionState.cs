// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal struct SymbolCompletionState
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 由于此字段作为一个标记来指示其他赋值的完成情况，因此它必须为volatile以此确保先写后读操作而不会被打乱顺序或优化。
    /// </remarks>
    private volatile int _completeParts;

    /// <summary>
    /// 获取未完成的组件。
    /// </summary>
    /// <value>未完成的组件。</value>
    internal int IncompleteParts => ~_completeParts & (int)CompletionPart.All;

    internal CompletionPart NextIncompletePart
    {
        get
        {
            // 注：必须将IncompleteParts引入本地，因为属性值可能在两次访问之间发生变化。
            var incomplete = IncompleteParts;
            var next = incomplete & ~(incomplete - 1);
            Debug.Assert(HasAtMostOneBitSet(next), $"当设置多个标记位时，{nameof(Symbol.ForceComplete)}无法正确处理结果。");
            return (CompletionPart)next;
        }
    }

    internal static bool HasAtMostOneBitSet(int bits) => (bits & (bits - 1)) == 0;

    /// <summary>
    /// 指定的组件是否已完成。
    /// </summary>
    /// <param name="part">要检查是否已完成的组件。</param>
    /// <returns>若指定的组件已完成，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal bool HasComplete(CompletionPart part) => (_completeParts & (int)part) == (int)part;

#pragma warning disable CS0420
    internal bool NotePartComplete(CompletionPart part) => ThreadSafeFlagOperations.Set(ref _completeParts, (int)part);
#pragma warning restore CS0420

    /// <summary>
    /// 自旋等待直到指定组件完成。
    /// </summary>
    /// <param name="part">要完成的组件。</param>
    /// <param name="cancellationToken">操作的取消标记。</param>
    internal void SpinWaitComplete(CompletionPart part, CancellationToken cancellationToken)
    {
        if (HasComplete(part)) return;

        // 在所有要完成的组件成功完成前一直等待。确保（在其他线程上）所有诊断消息都报告完毕。
        var spinWait = new SpinWait();
        while (!HasComplete(part))
        {
            cancellationToken.ThrowIfCancellationRequested();
            spinWait.SpinOnce(); // 等待一次。
        }
    }

    /// <summary>
    /// 返回符号完成状态的字符串表示。
    /// </summary>
    /// <returns>
    /// 形如“<c>CompletionParts(x, y, ..., z)</c>”的字符串。
    /// 其中<c>x</c>、<c>y</c>、<c>z</c>均为各个已完成的组件对应的<see cref="CompletionPart"/>的标记位序数。
    /// </returns>
    public override string ToString()
    {
        var parts = _completeParts;
        var result = new StringBuilder();

        result.Append("CompletionParts(");
        var any = false;
        for (var i = 0; ; i++)
        {
            var bit = 1 << i;
            if ((bit & (int)CompletionPart.All) == 0) break;
            if ((bit & parts) != 0)
            {
                if (any) result.Append(", ");
                result.Append(i);
                any = true;
            }
        }
        result.Append(')');

        return result.ToString();
    }
}
