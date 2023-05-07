// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis.CommandLine;

namespace Qtyi.CodeAnalysis.CommandLine;

/// <summary>
/// Class for logging information about what happens in the server and client parts of the 
/// Luna command line compiler and build tasks. Useful for debugging what is going on.
/// </summary>
/// <remarks>
/// To use the logging, set the environment variable LunaCommandLineLogFile to the name
/// of a file to log to. This file is logged to by both client and server components.
/// </remarks>
internal sealed class CompilerServerLogger : ICompilerServerLogger, IDisposable
{
    // Environment variable, if set, to enable logging and set the file to log to.
    internal const string EnvironmentVariableName = "LunaCommandLineLogFile";
    internal const string LoggingPrefix = "---";

    private Stream? _loggingStream;
    private readonly string _identifier;

    public bool IsLogging => _loggingStream is not null;

    /// <summary>
    /// Static class initializer that initializes logging.
    /// </summary>
    public CompilerServerLogger(string identifier)
    {
        _identifier = identifier;

        try
        {
            // Check if the environment
            if (Environment.GetEnvironmentVariable(EnvironmentVariableName) is string loggingFileName)
            {
                // If the environment variable contains the path of a currently existing directory,
                // then use a process-specific name for the log file and put it in that directory.
                // Otherwise, assume that the environment variable specifies the name of the log file.
                if (Directory.Exists(loggingFileName))
                {
                    var processId = Process.GetCurrentProcess().Id;
                    loggingFileName = Path.Combine(loggingFileName, $"server.{processId}.log");
                }

                // Open allowing sharing. We allow multiple processes to log to the same file, so we use share mode to allow that.
                _loggingStream = new FileStream(loggingFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            }
        }
        catch (Exception e)
        {
            Debug.Assert(false, e.Message);
        }
    }

    public void Dispose()
    {
        _loggingStream?.Dispose();
        _loggingStream = null;
    }

    public void Log(string message)
    {
        if (_loggingStream is not null)
        {
            var threadId = Environment.CurrentManagedThreadId;
            var prefix = $"ID={_identifier} TID={threadId}: ";
            var output = prefix + message + Environment.NewLine;
            var bytes = Encoding.UTF8.GetBytes(output);

            // Because multiple processes might be logging to the same file, we always seek to the end,
            // write, and flush.
            _loggingStream.Seek(0, SeekOrigin.End);
            _loggingStream.Write(bytes, 0, bytes.Length);
            _loggingStream.Flush();
        }
    }
}
