// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Luna.Compilers.Generators;
using Luna.Compilers.Generators.Syntax;
using Luna.Compilers.Generators.Syntax.Model;
using LanguageNames = Qtyi.CodeAnalysis.LanguageNames;

namespace Luna.Compilers.Tools;

[SuppressMessage("Reliability", "CA2007")]
internal static class Program
{
    private static readonly CancellationTokenSource s_cancellationTokenSource = new();

    public static async Task<int> Main(string[] args)
    {
        Console.CancelKeyPress += static (_, e) =>
        {
            if (!e.Cancel)
                s_cancellationTokenSource.Cancel();
        };

        if (args.Length is < 2 or > 3)
            return WriteUsage();

        string inputFile = args[0];
        if (!File.Exists(inputFile))
        {
            Console.WriteLine(inputFile + " not found.");
            return 1;
        }

        bool writeSource = true;
        bool writeTests = false;
        bool writeSignatures = false;
        bool writeGrammar = false;
        string? outputPath = null;

        if (args.Length == 3)
        {
            outputPath = args[1];

            if (args[2] == "/test")
            {
                writeTests = true;
                writeSource = false;
            }
            else if (args[2] == "/grammar")
                writeGrammar = true;
            else
                return WriteUsage();
        }
        else if (args.Length == 2)
        {
            if (args[1] == "/sig")
                writeSignatures = true;
            else
                outputPath = args[1];
        }
        Debug.Assert(outputPath is not null);

        return writeGrammar
            ? await WriteGrammarFileAsync(inputFile, outputPath)
            : await WriteSourceFilesAsync(inputFile, writeSource, writeTests, writeSignatures, outputPath);
    }

    private static int WriteUsage()
    {
        Console.WriteLine("Invalid usage:");
        var programName = "  " + Path.GetFileNameWithoutExtension(typeof(Program).GetTypeInfo().Assembly.ManifestModule.Name);
        Console.WriteLine(programName + " input-file output-path [/test | /grammar]");
        Console.WriteLine(programName + " input-file /sig");
        Console.WriteLine();
        Console.WriteLine("  input-file:\tThe XML file containing syntax tree structure. Usually 'Syntax.xml'.");
        Console.WriteLine("  output-path:\tLocation for the generated files. May overwrite existing files.*");

        return 1;
    }

    private static Task<SyntaxTree> ReadTreeAsync(string inputFile)
    {
        return Task.Run(() =>
        {
            var reader = XmlReader.Create(inputFile, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });
            var serializer = new XmlSerializer(typeof(SyntaxTree));
            return (SyntaxTree)serializer.Deserialize(reader);
        }, s_cancellationTokenSource.Token);
    }

    private static async Task<int> WriteGrammarFileAsync(string inputFile, string outputPath)
    {
        var tree = await ReadTreeAsync(inputFile);

        try
        {
            var outputMainFile = Path.HasExtension(outputPath) ? outputPath : Path.Combine(outputPath, $"{LanguageNames.This}.Generated.g4");

            await WriteToFileAsync(GrammarWriter.WriteFile, tree, outputMainFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Generating grammar failed.");
            Console.WriteLine(ex);

            // Purposefully fall out here and don't return an error code.
            // We don't want to fail the build in this case.
            // Instead, we want to have the program fixed up if necessary.
        }

        return 0;
    }

    private static async Task<int> WriteSourceFilesAsync(string inputFile, bool writeSource, bool writeTests, bool writeSignatures, string outputFile)
    {
        var tree = await ReadTreeAsync(inputFile);

        // The syntax.xml doc contains some nodes that are useful for other tools, but which are
        // not needed by this syntax generator.  Specifically, we have `<Choice>` and
        // `<Sequence>` nodes in the xml file to help others tools understand the relationship
        // between some fields (i.e. 'only one of these children can be non-null').  To make our
        // life easier, we just flatten all those nodes, grabbing all the nested `<Field>` nodes
        // and placing into a single linear list that we can then process.
        SyntaxTreeFlattener.Instance.Flatten(tree, s_cancellationTokenSource.Token);

        if (writeSignatures)
            await WriteToConsoleAsync(SignatureWriter.WriteFile, tree);
        else
        {
            var tasks = new List<Task>(4);
            if (writeSource)
            {
                var outputPath = outputFile.Trim('"');
                var prefix = Path.GetFileName(inputFile);
                var outputMainFile = Path.Combine(outputPath, $"{prefix}.Main.Generated.cs");
                var outputInternalFile = Path.Combine(outputPath, $"{prefix}.Internal.Generated.cs");
                var outputSyntaxFile = Path.Combine(outputPath, $"{prefix}.Syntax.Generated.cs");

                tasks.Add(WriteToFileAsync(SyntaxSourceWriter.WriteMain, tree, outputMainFile));
                tasks.Add(WriteToFileAsync(SyntaxSourceWriter.WriteInternal, tree, outputInternalFile));
                tasks.Add(WriteToFileAsync(SyntaxSourceWriter.WriteSyntax, tree, outputSyntaxFile));
            }
            if (writeTests)
            {
                tasks.Add(WriteToFileAsync(TestWriter.Write, tree, outputFile));
            }
            await Task.WhenAll(tasks);
        }

        return 0;
    }

    private static Task WriteToConsoleAsync(Action<TextWriter, SyntaxTree, CancellationToken> writeAction, SyntaxTree tree)
    {
        return Task.Run(() =>
        {
            writeAction(Console.Out, tree, s_cancellationTokenSource.Token);
        }, s_cancellationTokenSource.Token);
    }

    private static Task WriteToFileAsync(Action<TextWriter, SyntaxTree, Microsoft.CodeAnalysis.Compilation, CancellationToken> writeAction, SyntaxTree tree, string outputFile)
    {
        return Task.Run(() =>
        {
            try
            {
                using var outFile = new StreamWriter(File.Open(outputFile, FileMode.Create), Encoding.UTF8);
                writeAction(outFile, tree, null, s_cancellationTokenSource.Token);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unable to access {0}.  Is it checked out?", outputFile);
            }
        }, s_cancellationTokenSource.Token);
    }
}
