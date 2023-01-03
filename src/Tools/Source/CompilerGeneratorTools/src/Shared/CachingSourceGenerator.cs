// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Text;
using MSCA::System.Diagnostics.CodeAnalysis;

namespace Luna.Compilers.Generators;

public abstract class CachingSourceGenerator : ISourceGenerator
{
    /// <remarks>
    /// ⚠ 此字段可能可能会被多个线程同时访问。
    /// </remarks>
    private static readonly WeakReference<CachedSourceGeneratorResult?> s_cachedResult = new(null);

    /// <summary>
    /// 尝试筛选有价值的输入。
    /// </summary>
    /// <param name="context">生成器执行上下文。</param>
    /// <param name="inputPath">有价值的文件的路径。</param>
    /// <param name="inputText">有价值的文件的文本内容。</param>
    /// <returns></returns>
    protected abstract bool TryGetRelevantInput(
        in GeneratorExecutionContext context,
        out string? inputPath,
        [NotNullWhen(true)] out SourceText? inputText);

    protected abstract bool TryGenerateSources(
        string? inputPath,
        SourceText inputText,
        out ImmutableArray<(string hintName, SourceText sourceText)> sources,
        out ImmutableArray<Diagnostic> diagnostics,
        CancellationToken cancellationToken);

    public virtual void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        if (!this.TryGetRelevantInput(in context, out var input, out var inputText)) return;

        // 获取当前输入的检验和，用于验证或更新当前缓存。
        var currentChecksum = inputText.GetChecksum();

        // 仅读取一次当前缓存以避免竞争状态。
        if (s_cachedResult.TryGetTarget(out var cachedResult)
            && cachedResult!.Checksum.SequenceEqual(currentChecksum))
        { // 检验和相等，输入未发生更改。
            // 添加上一次缓存的源文件，不更改缓存。
            addSources(sources: cachedResult.Sources);
            return;
        }

        // 检验和不相等，输入发生更改。
        if (this.TryGenerateSources(input, inputText, out var sources, out var diagnostics, context.CancellationToken))
        {
            addSources(sources);

            if (diagnostics.IsEmpty)
            {
                var result = new CachedSourceGeneratorResult(currentChecksum, sources);

                // Default Large Object Heap size threshold
                // https://github.com/dotnet/runtime/blob/c9d69e38d0e54bea5d188593ef6c3b30139f3ab1/src/coreclr/src/gc/gc.h#L111
                const int Threshold = 85000;

                // Overwrite the cached result with the new result. This is an opportunistic cache, so as long as
                // the write is atomic (which it is for SetTarget) synchronization is unnecessary. We allocate an
                // array on the Large Object Heap (which is always part of Generation 2) and give it a reference to
                // the cached object to ensure this weak reference is not reclaimed prior to a full GC pass.
                var largeArray = new CachedSourceGeneratorResult[Threshold / Unsafe.SizeOf<CachedSourceGeneratorResult>()];
                Debug.Assert(GC.GetGeneration(largeArray) >= 2);
                largeArray[0] = result;
                s_cachedResult.SetTarget(result);
                GC.KeepAlive(largeArray);
            }
            else
            {
                // Invalidate the cache since we cannot currently cache diagnostics
                s_cachedResult.SetTarget(null);
            }
        }
        else
        {
            // 生成失败，取消缓存。
            s_cachedResult.SetTarget(null);
        }

        // 报告所有的诊断。
        foreach (var diagnostic in diagnostics)
        {
            context.ReportDiagnostic(diagnostic);
        }

        void addSources(ImmutableArray<(string hintName, SourceText sourceText)> sources)
        {
            foreach (var (hintName, sourceText) in sources)
            {
                context.AddSource(hintName, sourceText);
            }
        }
    }

    private sealed record CachedSourceGeneratorResult(
        ImmutableArray<byte> Checksum,
        ImmutableArray<(string hintName, SourceText sourceText)> Sources);
}
