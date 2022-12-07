using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Luna.Compilers.Simulators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Tools;

internal class Program
{
    public static int Main(string[] args)
    {
        if (args.Length is < 1 or > 3)
        {
            return WriteUsage();
        }

        string inputPath = args[0];
        string[]? inputFiles = null;
        if (Directory.Exists(inputPath))
            inputFiles = Directory.GetFiles(inputPath);
        else if (File.Exists(inputPath))
            inputFiles = new[] { inputPath };
        else
        {
            Console.WriteLine($"未找到“{inputPath}”");
            return 1;
        }

        var length = inputFiles.Length;
        if (length == 0) return 0;

        string? outputPath = null;
        string[] outputFiles;
        string? cssPath = null;
        string[]? cssFiles = null;
        if (args.Length == 2)
        {
            if (args[1].StartsWith("/css:"))
            {
                cssPath = args[1].Substring(5);
            }
            else
            {
                outputPath = args[1];
            }
        }
        else if (args.Length == 3)
        {
            outputPath = args[1];
            cssPath = args[2].Substring(5);
        }

ProcessOutputPath:
        if (string.IsNullOrEmpty(outputPath))
        {
            outputFiles = new string[length];
            for (var i = 0; i < length; i++)
                outputFiles[i] = inputFiles[i] + ".html";
        }
        else
        {
            var extension = Path.GetExtension(outputPath);
            if (Directory.Exists(outputPath) || string.IsNullOrEmpty(extension))
            {
                outputFiles = new string[length];
                for (var i = 0; i < length; i++)
                    outputFiles[i] = Path.Combine(outputPath, Path.GetFileName(inputFiles[i]) + ".html");
            }
            else if (extension != ".html")
            {
                outputPath = Path.GetDirectoryName(outputPath);
                goto ProcessOutputPath;
            }
            else if (inputFiles.Length > 1)
            {
                outputFiles = new string[length];
                for (var i = 0; i < length; i++)
                    outputFiles[i] = Path.Combine(outputPath, Path.GetFileName(inputFiles[i]) + ".html");
            }
            else
            {
                outputFiles = new[] { outputPath };
            }
        }

        if (cssPath is not null)
        {
            if (Directory.Exists(cssPath))
                cssFiles = Directory.GetFiles(cssPath, "*.css");
            else if (File.Exists(cssPath))
                cssFiles = new[] { cssPath };
        }

        Simulator.RegisterSimulatorFromConfiguration(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "config.json"));

        var destinationDir = Path.GetDirectoryName(outputFiles[0])!;

        int errorCode;

        // 创建目标文件夹。
        errorCode = Program.CreateDirectory(Path.GetFullPath(destinationDir));
        if (errorCode != 0) return errorCode;

        // 复制CSS样式表。
        errorCode = Program.CopyCss(destinationDir, cssFiles);
        if (errorCode != 0) return errorCode;

        // 生成渲染后的网页。
        for (var i = 0; i < length; i++)
        {
            errorCode = Program.WriteHtml(inputFiles[i], outputFiles[i], cssFiles);
            if (errorCode != 0) return errorCode;
        }

        return 0;
    }

    private static int CreateDirectory(string destinationDir)
    {
        try
        {
            Directory.CreateDirectory(destinationDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
#if DEBUG
            throw;
#else
            return ex.HResult;
#endif
        }
        return 0;
    }

    private static int WriteHtml(
        string inputFile,
        string outputFile,
        string[]? cssFiles)
    {
        var extension = Path.GetExtension(inputFile);
        if (Simulator.TryGetLexerSimulatorByFileExtension(extension, out var simulators))
        {
            Debug.Assert(simulators is not null);
            for (var i = 0; i < simulators.Length; i++)
            {
                var simulator = simulators[i];

                try
                {
                    var doc = new HtmlDocument();

                    var head = doc.CreateElement("head");
                    {
                        var meta = doc.CreateElement("meta");
                        meta.SetAttributeValue("charset", "utf-8");
                        head.AppendChild(meta);
                        if (cssFiles is not null && cssFiles.Length > 0)
                        {
                            foreach (var cssFile in cssFiles)
                            {
                                var link = doc.CreateElement("link");
                                link.SetAttributeValue("rel", "stylesheet");
                                link.SetAttributeValue("type", "text/css");
                                link.SetAttributeValue("href", cssFile);
                                head.AppendChild(link);
                            }
                        }
                        var title = doc.CreateElement("title");
                        title.AppendChild(doc.CreateTextNode($"{Path.GetFileName(inputFile)} - {simulator.GetType().FullName}"));
                        head.AppendChild(title);
                    }
                    doc.DocumentNode.AppendChild(head);

                    var body = doc.CreateElement("body");
                    {
                        var div = doc.CreateElement("div");
                        div.AddClass("code-box");
                        body.AppendChild(div);

                        using var fs = File.OpenRead(inputFile);
                        var sourceText = SourceText.From(fs);
                        foreach (var token in simulator.LexToEnd(sourceText))
                        {
                            if (token.HasLeadingTrivia) ProcessTriviaList(token.LeadingTrivia);
                            ProcessToken(simulator.GetTokenKind(token.RawKind), token.Text);
                            if (token.HasTrailingTrivia) ProcessTriviaList(token.TrailingTrivia);
                        }

                        void ProcessTriviaList(SyntaxTriviaList triviaList)
                        {
                            foreach (var trivia in triviaList)
                            {
                                ProcessToken(simulator.GetTokenKind(trivia.RawKind), sourceText.ToString(trivia.FullSpan));
                            }
                        }
                        void ProcessToken(TokenKind kind, string text)
                        {
                            if (kind == TokenKind.NewLine)
                            {
                                div.AppendChild(doc.CreateElement("br"));
                            }
                            else
                            {
                                var span = doc.CreateElement("span");
                                div.AppendChild(span);

                                span.AddClass(kind switch
                                {
                                    TokenKind.None => "none",
                                    TokenKind.Keyword => "kwd",
                                    TokenKind.Identifier => "ind",
                                    TokenKind.Operator => "op",
                                    TokenKind.Punctuation => "punct",
                                    TokenKind.NumericLiteral => "num",
                                    TokenKind.StringLiteral => "str",
                                    TokenKind.WhiteSpace => "space",
                                    TokenKind.Comment => "comment",
                                    TokenKind.Documentation => "doc",
                                    TokenKind.Skipped => "skipped",
                                    _ => throw new InvalidOperationException(),
                                });

                                var ss = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                                for (int i = 0; i < ss.Length; i++)
                                {
                                    if (i > 0) span.AppendChild(doc.CreateElement("br"));
                                    span.AppendChild(doc.CreateTextNode(escapeText(ss[i])));
                                }

                                string escapeText(string text) => Regex.Replace(text, @"\s", m =>
                                {
                                    return m.Value switch
                                    {
                                        "\t" => "&nbsp;&nbsp;",
                                        _ => "&nbsp;"
                                    };
                                });
                            }
                        };
                    }
                    doc.DocumentNode.AppendChild(body);

                    doc.Save(
                        simulators.Length == 1 ? outputFile :
                            Path.Combine(Path.GetDirectoryName(outputFile)!, $"{Path.GetFileNameWithoutExtension(outputFile)}.{i}{Path.GetExtension(outputFile)}"),
                        Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
#if DEBUG
                    throw;
#else
                    return ex.HResult;
#endif
                }
            }

            return 0;
        }
        else
        {
            Console.WriteLine($"不支持的文件后缀名“{extension}”");
            return 1;
        }
    }

    private static int CopyCss(
        string destinationDir,
        string[]? cssFiles)
    {
        if (cssFiles is not null && cssFiles.Length > 0)
        {
            try
            {
                foreach (var cssFile in cssFiles)
                {
                    File.Copy(cssFile, Path.Combine(destinationDir, $"{Path.GetFileName(cssFile)}"), true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
#if DEBUG
                throw;
#else
                return ex.HResult;
#endif
            }
        }
        return 0;
    }

    private static int WriteUsage()
    {
        Console.WriteLine("Invalid usage:");
        var programName = "  " + typeof(Program).GetTypeInfo().Assembly.ManifestModule.Name;
        Console.WriteLine(programName + " inputPath outputPath [/css:css-path]");
        Console.WriteLine(programName + " inputPath [/css:css-path]");
        return 1;
    }
}
