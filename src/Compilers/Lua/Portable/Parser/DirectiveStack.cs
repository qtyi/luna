// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial struct DirectiveStack
{
    public partial DefineState IsDefined(string id)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }

    // true if any previous section of the closest #if or #ifnot has its branch taken
    public bool PreviousBranchTaken()
    {
        for (var current = _directives; current is not null && current.Any(); current = current.Tail)
        {
            if (current.Head.BranchTaken)
                return true;
            else if (current.Head.Kind is SyntaxKind.IfDirectiveTrivia
                                       or SyntaxKind.IfNotDirectiveTrivia)
                return false;
        }

        return false;
    }

    public bool HasUnfinishedIf()
    {
        var prev = GetPreviousIfElse(_directives);
        return prev is not null && prev.Any();
    }

    public bool HasPreviousIf()
    {
        var prev = GetPreviousIfElse(_directives);
        return prev is not null && prev.Any()
            && prev.Head.Kind is SyntaxKind.IfDirectiveTrivia or SyntaxKind.IfNotDirectiveTrivia;
    }

    [return: NotNullIfNotNull(nameof(directives))]
    private static ConsList<Directive>? GetPreviousIf(ConsList<Directive>? directives)
    {
        var current = directives;
        while (current is not null && current.Any())
        {
            switch (current.Head.Kind)
            {
                case SyntaxKind.IfDirectiveTrivia:
                case SyntaxKind.IfNotDirectiveTrivia:
                    return current;
            }

            current = current.Tail;
        }

        return current;
    }

    [return: NotNullIfNotNull(nameof(directives))]
    private static ConsList<Directive>? GetPreviousIfElse(ConsList<Directive>? directives)
    {
        var current = directives;
        while (current is not null && current.Any())
        {
            switch (current.Head.Kind)
            {
                case SyntaxKind.IfDirectiveTrivia:
                case SyntaxKind.IfNotDirectiveTrivia:
                case SyntaxKind.ElseDirectiveTrivia:
                    return current;
            }

            current = current.Tail;
        }

        return current;
    }

    public partial DirectiveStack Add(Directive directive)
    {
        switch (directive.Kind)
        {
            case SyntaxKind.EndInputDirectiveTrivia:
                // Pop out all directives in stack.
                return Empty;

            case SyntaxKind.EndDirectiveTrivia:
                var prevIf = GetPreviousIf(_directives);
                if (prevIf is null || !prevIf.Any())
                    // no matching #if/#ifnot, so just Leave it alone.
                    goto default;

                Debug.Assert(_directives is not null);
                return new(CompleteIf(_directives, out _));

            default:
                return new(new(directive, _directives ?? ConsList<Directive>.Empty));
        }
    }

    // removes unfinished if & related directives from stack and leaves active branch directives
    private static ConsList<Directive> CompleteIf(ConsList<Directive> stack, out bool include)
    {
        // if we get to the top, the default rule is to include anything that follows
        if (!stack.Any())
        {
            include = true;
            return stack;
        }

        // If we reach the #if/#ifnot directive, then we stop unwinding and start rebuilding the stack without the #if/#ifnot/#else/#end directives.
        // Only including content from sections that are considered included
        if (stack.Head.Kind is SyntaxKind.IfDirectiveTrivia or SyntaxKind.IfNotDirectiveTrivia)
        {
            include = stack.Head.BranchTaken;
            return stack.Tail;
        }

        var newStack = CompleteIf(stack.Tail, out include);
        switch (stack.Head.Kind)
        {
            case SyntaxKind.ElseDirectiveTrivia:
                include = stack.Head.BranchTaken;
                break;
            default:
                if (include)
                    newStack = new(stack.Head, newStack);
                break;
        }

        return newStack;
    }

    private static partial ConsList<Directive>? SkipInsignificantDirectives(ConsList<Directive>? directives)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }
}
