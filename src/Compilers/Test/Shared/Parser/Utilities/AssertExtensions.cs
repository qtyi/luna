using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.VisualStudio.TestTools.UnitTesting;

internal static partial class AssertExtensions
{
    [DoesNotReturn]
    public static void Raise(this Assert assert, string message, object[]? args = null, [CallerMemberName] string? memberName = null) => assert.Raise<AssertFailedException>(message, args, memberName);

    [DoesNotReturn]
    public static void Raise(this Assert assert, Exception innerException, [CallerMemberName] string? memberName = null) => assert.Raise(innerException.Message, memberName: memberName);

    [DoesNotReturn]
    public static void Raise<TException>(this Assert assert, string message, object[]? args = null, [CallerMemberName] string? memberName = null)
        where TException : AssertFailedException
    {
        var exceptionMessage = $"{memberName ?? string.Empty}断言失败：{message}";
        object[] exceptionArgs;
        if (args is null)
        {
            exceptionArgs = new object[] { exceptionMessage };
        }
        else
        {
            exceptionArgs = new object[args.Length + 1];
            exceptionArgs[0] = exceptionMessage;
            args.CopyTo(exceptionArgs, 1);
        }
        var exception = Activator.CreateInstance(typeof(TException), exceptionArgs) as TException;
        Debug.Assert(exception is not null);
        throw exception;
    }
}
