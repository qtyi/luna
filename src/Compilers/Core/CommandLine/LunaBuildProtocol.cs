// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.CommandLine;

using RoslynBuildRequest = Microsoft.CodeAnalysis.CommandLine.BuildRequest;
using RoslynRequestLanguage = Microsoft.CodeAnalysis.CommandLine.RequestLanguage;
using Argument = Microsoft.CodeAnalysis.CommandLine.BuildRequest.Argument;

internal sealed class BuildRequest
{
    private readonly RoslynBuildRequest _underlying;

    public BuildRequest(RequestLanguage language,
                        string compilerHash,
                        IEnumerable<Argument> arguments,
                        Guid? requestId = null) =>
        this._underlying = new((RoslynRequestLanguage)language, compilerHash, arguments, requestId);

    private BuildRequest(RoslynBuildRequest buildRequest) => this._underlying = buildRequest;

    public static BuildRequest Create(
        RequestLanguage language,
        IList<string> args,
        string workingDirectory,
        string tempDirectory,
        string compilerHash,
        Guid? requestId = null,
        string? keepAlive = null,
        string? libDirectory = null) =>
        new(RoslynBuildRequest.Create((RoslynRequestLanguage)language, args, workingDirectory, tempDirectory, compilerHash, requestId, keepAlive, libDirectory));

    public static BuildRequest CreateShutdown() =>
        new(RoslynBuildRequest.CreateShutdown());

    public static async Task<BuildRequest> ReadAsync(Stream inStream, CancellationToken cancellationToken) =>
        new(await RoslynBuildRequest.ReadAsync(inStream, cancellationToken).ConfigureAwait(false));

    public async Task WriteAsync(Stream outStream, CancellationToken cancellationToken = default) =>
        await this._underlying.WriteAsync(outStream, cancellationToken).ConfigureAwait(false);

    public static implicit operator BuildRequest(RoslynBuildRequest buildRequest) => new(buildRequest);
    public static implicit operator RoslynBuildRequest(BuildRequest buildRequest) => buildRequest._underlying;
}

internal enum RequestLanguage
{
    #region Roslyn支持的语言
    CSharpCompile = RoslynRequestLanguage.CSharpCompile,
    VisualBasicCompile = RoslynRequestLanguage.VisualBasicCompile,
    #endregion

    LuaCompile = 0x44532531,
    MoonScriptCompile = 0x44532532,
}
