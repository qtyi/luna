namespace Microsoft.VisualStudio.TestTools.UnitTesting;

internal sealed class UnexpectedSyntaxKindException<TSyntaxKind> : AssertFailedException
    where TSyntaxKind : struct, Enum
{
    public UnexpectedSyntaxKindException() : base(MakeMessage()) { }

    public UnexpectedSyntaxKindException(TSyntaxKind actual) : base(MakeMessage(null, actual)) { }

    public UnexpectedSyntaxKindException(TSyntaxKind expected, TSyntaxKind actual) : base(MakeMessage(expected, actual)) { }

    public UnexpectedSyntaxKindException(string message) : base(message) { }

    public UnexpectedSyntaxKindException(TSyntaxKind actual, Exception innerException) : base(MakeMessage(null, actual), innerException) { }

    public UnexpectedSyntaxKindException(TSyntaxKind expected, TSyntaxKind actual, Exception innerException) : base(MakeMessage(expected, actual), innerException) { }

    public UnexpectedSyntaxKindException(string message, Exception innerException) : base(message, innerException) { }

    internal static string MakeMessage(TSyntaxKind? expected = null, TSyntaxKind? actual = null)
    {
        var message = "意外的语法种类";
        var expectedMessage = expected.HasValue ? Enum.GetName(typeof(TSyntaxKind), expected) : null;
        var actualMessage = actual.HasValue ? Enum.GetName(typeof(TSyntaxKind), actual) : null;
        if (expectedMessage is null && actualMessage is null)
            return message;
        else if (expectedMessage is not null && actualMessage is not null)
            return $"{message}（应为{expectedMessage}，实为{actualMessage}）";
        else if (expectedMessage is not null)
            return $"{message}（应为{expectedMessage}）";
        else
            return $"{message}（实为{actualMessage}）";
    }
}
