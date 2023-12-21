// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_attrib(string source, SourceCodeKind kind)
    {
        var V = ParseSource(source, options: TestOptions.RegularDefault.WithKind(kind));

        V(SyntaxKind.Chunk);
        {
            V(SyntaxKind.Block);
            {
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "\"testing require\"", value: ImmutableArray.Create("testing require"U8.ToArray()));
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                        V(SyntaxKind.ArgumentString);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "string");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                        V(SyntaxKind.ArgumentString);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"math\"", value: ImmutableArray.Create("math"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "math");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                        V(SyntaxKind.ArgumentString);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"table\"", value: ImmutableArray.Create("table"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "table");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                        V(SyntaxKind.ArgumentString);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"io\"", value: ImmutableArray.Create("io"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "io");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                        V(SyntaxKind.ArgumentString);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"os\"", value: ImmutableArray.Create("os"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "os");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                        V(SyntaxKind.ArgumentString);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"coroutine\"", value: ImmutableArray.Create("coroutine"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "coroutine");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "type");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "path");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "type");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "cpath");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "type");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "loaded");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"table\"", value: ImmutableArray.Create("table"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "type");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "preload");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"table\"", value: ImmutableArray.Create("table"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "type");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "config");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"package config: \"", value: ImmutableArray.Create("package config: "U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "string");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "gsub");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "config");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\n\"", value: ImmutableArray.Create("\n"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"|\"", value: ImmutableArray.Create("|"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "max");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "_soft");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "100", value: 100L);
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "2000", value: 2000L);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "t");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.CloseBraceToken);
                                }
                            }
                        }
                        V(SyntaxKind.ForStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "max");
                            }
                            V(SyntaxKind.DoKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "t");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "i");
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "string");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "rep");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.AdditionExpression);
                                                {
                                                    V(SyntaxKind.ModuloExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                        V(SyntaxKind.PersentToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                        }
                                                    }
                                                    V(SyntaxKind.PlusToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "t");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.AdditionExpression);
                                {
                                    V(SyntaxKind.LengthExpression);
                                    {
                                        V(SyntaxKind.HashToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "t");
                                        }
                                    }
                                    V(SyntaxKind.PlusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                    }
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\";\"", value: ImmutableArray.Create(";"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "table");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "concat");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "t");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\";\"", value: ImmutableArray.Create(";"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "err");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "package");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "searchpath");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"xuxu\"", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "path");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.LogicalNotExpression);
                                                {
                                                    V(SyntaxKind.NotKeyword);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "string");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "find");
                                                        }
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "err");
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "string");
                                                                    }
                                                                    V(SyntaxKind.DotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "rep");
                                                                    }
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"xuxu\"", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.GreaterThanOrEqualExpression);
                                            {
                                                V(SyntaxKind.LengthExpression);
                                                {
                                                    V(SyntaxKind.HashToken);
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "string");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "gsub");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "err");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"[^\\n]\"", value: ImmutableArray.Create("[^\n]"U8.ToArray()));
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.GreaterThanEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "max");
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "string");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "rep");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "max");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "err");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "package");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "searchpath");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"xuxu\"", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "path");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.LogicalNotExpression);
                                            {
                                                V(SyntaxKind.NotKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "string");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "find");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "err");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "string");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "rep");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "'xuxu'", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "max");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "oldpath");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "path");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.CloseBraceToken);
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "err");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pcall");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "require");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"no-such-file\"", value: ImmutableArray.Create("no-such-file"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.LogicalNotExpression);
                                            {
                                                V(SyntaxKind.NotKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "string");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "find");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "err");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"package.path\"", value: ImmutableArray.Create("package.path"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "oldpath");
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentString);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"testing 'require' message\"", value: ImmutableArray.Create("testing 'require' message"U8.ToArray()));
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "oldpath");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "path");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "oldcpath");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "cpath");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"?.lua;?/?\"", value: ImmutableArray.Create("?.lua;?/?"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "cpath");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"?.so;?/init\"", value: ImmutableArray.Create("?.so;?/init"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "st");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "msg");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pcall");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "require");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "'XXX'", value: ImmutableArray.Create("XXX"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "expected");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.MultiLineRawStringLiteralToken, text: "[[module 'XXX' not found:\n\tno field package.preload['XXX']\n\tno file 'XXX.lua'\n\tno file 'XXX/XXX'\n\tno file 'XXX.so'\n\tno file 'XXX/init']]", value: ImmutableArray.Create("module 'XXX' not found:\n\tno field package.preload['XXX']\n\tno file 'XXX.lua'\n\tno file 'XXX/XXX'\n\tno file 'XXX.so'\n\tno file 'XXX/init'"U8.ToArray()));
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "msg");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "expected");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "oldpath");
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "cpath");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "oldcpath");
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "'+'", value: ImmutableArray.Create("+"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.IfStatement);
                {
                    V(SyntaxKind.IfKeyword);
                    V(SyntaxKind.LogicalNotExpression);
                    {
                        V(SyntaxKind.NotKeyword);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "_port");
                        }
                    }
                    V(SyntaxKind.ThenKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "dirsep");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "string");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "match");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "config");
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"^([^\\n]+)\\n\"", value: ImmutableArray.Create("^([^\n]+)\n"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "DIR");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"libs\"", value: ImmutableArray.Create("libs"U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "dirsep");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "D");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "x");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "string");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "gsub");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"/\"", value: ImmutableArray.Create("/"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "dirsep");
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.ReturnStatement);
                                {
                                    V(SyntaxKind.ReturnKeyword);
                                    V(SyntaxKind.ConcatenationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "DIR");
                                        }
                                        V(SyntaxKind.DotDotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "x");
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "DC");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "ext");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.OrExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.ParenthesizedExpression);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "dirsep");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "'\\\\'", value: ImmutableArray.Create("\\"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\".dll\"", value: ImmutableArray.Create(".dll"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.OrKeyword);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\".so\"", value: ImmutableArray.Create(".so"U8.ToArray()));
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.ReturnStatement);
                                {
                                    V(SyntaxKind.ReturnKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "D");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "ext");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "createfiles");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "files");
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "preextras");
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "posextras");
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.ForInStatement);
                                {
                                    V(SyntaxKind.ForKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "n");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "c");
                                    }
                                    V(SyntaxKind.InKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pairs");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "files");
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.DoKeyword);
                                    V(SyntaxKind.Block);
                                    {
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "io");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "output");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "io");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "write");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "string");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "format");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "preextras");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "io");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "write");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "c");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "io");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "write");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "string");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "format");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "posextras");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "io");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "close");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "io");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "output");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EndKeyword);
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "removefiles");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "files");
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.ForInStatement);
                                {
                                    V(SyntaxKind.ForKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "n");
                                    }
                                    V(SyntaxKind.InKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pairs");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "files");
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.DoKeyword);
                                    V(SyntaxKind.Block);
                                    {
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "os");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "remove");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EndKeyword);
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "files");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"names.lua\"", value: ImmutableArray.Create("names.lua"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"do return {...} end\\n\"", value: ImmutableArray.Create("do return {...} end\n"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"err.lua\"", value: ImmutableArray.Create("err.lua"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"B = 15; a = a + 1;\"", value: ImmutableArray.Create("B = 15; a = a + 1;"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"synerr.lua\"", value: ImmutableArray.Create("synerr.lua"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"B =\"", value: ImmutableArray.Create("B ="U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"A.lua\"", value: ImmutableArray.Create("A.lua"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"B.lua\"", value: ImmutableArray.Create("B.lua"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"assert(...=='B');require 'A'\"", value: ImmutableArray.Create("assert(...=='B');require 'A'"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"A.lc\"", value: ImmutableArray.Create("A.lc"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"A\"", value: ImmutableArray.Create("A"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"L\"", value: ImmutableArray.Create("L"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"XXxX\"", value: ImmutableArray.Create("XXxX"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.KeyValueField);
                                    {
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"package.loaded[...] = 25; require'C'\"", value: ImmutableArray.Create("package.loaded[...] = 25; require'C'"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.CloseBraceToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "extras");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.MultiLineRawStringLiteralToken, text: "[[\nNAME = '%s'\nREQUIRED = ...\nreturn AA]]", value: ImmutableArray.Create("NAME = '%s'\nREQUIRED = ...\nreturn AA"U8.ToArray()));
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "createfiles");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "files");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "extras");
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\".\"", value: ImmutableArray.Create("."U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\".\"", value: ImmutableArray.Create("."U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"--x-\"", value: ImmutableArray.Create("--x-"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"-\"", value: ImmutableArray.Create("-"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"X\"", value: ImmutableArray.Create("X"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"XXxX\"", value: ImmutableArray.Create("XXxX"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"---xX\"", value: ImmutableArray.Create("---xX"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"---\"", value: ImmutableArray.Create("---"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"XX\"", value: ImmutableArray.Create("XX"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"XXxX\"", value: ImmutableArray.Create("XXxX"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "dirsep");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\".\\\\C.lua\"", value: ImmutableArray.Create(".\\C.lua"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "D");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\\\\"", value: ImmutableArray.Create("\\"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"./C.lua\"", value: ImmutableArray.Create("./C.lua"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "oldpath");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "path");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "string");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "gsub");
                                    }
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"D/?.lua;D/?.lc;D/?;D/??x?;D/L\"", value: ImmutableArray.Create("D/?.lua;D/?.lc;D/?;D/??x?;D/L"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"D/\"", value: ImmutableArray.Create("D/"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "DIR");
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.FunctionDefinitionExpression);
                                {
                                    V(SyntaxKind.FunctionKeyword);
                                    V(SyntaxKind.ParameterList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Parameter);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "p");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Parameter);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Parameter);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "r");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Parameter);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "ext");
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.Block);
                                    {
                                        V(SyntaxKind.AssignmentStatement);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "NAME");
                                            }
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.NilLiteralExpression);
                                            {
                                                V(SyntaxKind.NilKeyword);
                                            }
                                        }
                                        V(SyntaxKind.LocalDeclarationStatement);
                                        {
                                            V(SyntaxKind.LocalKeyword);
                                            V(SyntaxKind.NameAttributeList);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "rr");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.NameAttributeList);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.EqualsValuesClause);
                                            {
                                                V(SyntaxKind.EqualsToken);
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "require");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "p");
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "NAME");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "n");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "REQUIRED");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "p");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "rr");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "r");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.InvocationStatement);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "ext");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "x");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EndKeyword);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "require");
                                    }
                                    V(SyntaxKind.ArgumentString);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"names\"", value: ImmutableArray.Create("names"U8.ToArray()));
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"names\"", value: ImmutableArray.Create("names"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "D");
                                                    }
                                                    V(SyntaxKind.ArgumentString);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"names.lua\"", value: ImmutableArray.Create("names.lua"U8.ToArray()));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "st");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "msg");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pcall");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "require");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"err\"", value: ImmutableArray.Create("err"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.LogicalNotExpression);
                                                {
                                                    V(SyntaxKind.NotKeyword);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "st");
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "string");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "find");
                                                        }
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "msg");
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"arithmetic\"", value: ImmutableArray.Create("arithmetic"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "B");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "15", value: 15L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "st");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "msg");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pcall");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "require");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"synerr\"", value: ImmutableArray.Create("synerr"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.LogicalNotExpression);
                                            {
                                                V(SyntaxKind.NotKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "st");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "string");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "find");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "msg");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"error loading module\"", value: ImmutableArray.Create("error loading module"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"C\"", value: ImmutableArray.Create("C"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "package");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "path");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C.lua\"", value: ImmutableArray.Create("C.lua"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C\"", value: ImmutableArray.Create("C"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "25", value: 25L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C\"", value: ImmutableArray.Create("C"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "25", value: 25L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'B'", value: ImmutableArray.Create("B"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'B.lua'", value: ImmutableArray.Create("B.lua"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.TrueLiteralExpression);
                                        {
                                            V(SyntaxKind.TrueKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"libs/B.lua\"", value: ImmutableArray.Create("libs/B.lua"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "loaded");
                                                }
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "B");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"B\"", value: ImmutableArray.Create("B"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.TrueLiteralExpression);
                                            {
                                                V(SyntaxKind.TrueKeyword);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "loaded");
                                                }
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "A");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"C\"", value: ImmutableArray.Create("C"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "25", value: 25L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "loaded");
                                    }
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "A");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'B'", value: ImmutableArray.Create("B"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.TrueLiteralExpression);
                                        {
                                            V(SyntaxKind.TrueKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'A'", value: ImmutableArray.Create("A"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'A.lua'", value: ImmutableArray.Create("A.lua"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.TrueLiteralExpression);
                                        {
                                            V(SyntaxKind.TrueKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"libs/A.lua\"", value: ImmutableArray.Create("libs/A.lua"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "loaded");
                                    }
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "A");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "os");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "remove");
                                    }
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "D");
                                            }
                                            V(SyntaxKind.ArgumentString);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "'A.lua'", value: ImmutableArray.Create("A.lua"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.CloseBraceToken);
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'A'", value: ImmutableArray.Create("A"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'A.lc'", value: ImmutableArray.Create("A.lc"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "AA");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"libs/A.lc\"", value: ImmutableArray.Create("libs/A.lc"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"A\"", value: ImmutableArray.Create("A"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "package");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "path");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"A.lc\"", value: ImmutableArray.Create("A.lc"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"A\"", value: ImmutableArray.Create("A"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "AA");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.FalseLiteralExpression);
                            {
                                V(SyntaxKind.FalseKeyword);
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'K'", value: ImmutableArray.Create("K"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'L'", value: ImmutableArray.Create("L"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.FalseLiteralExpression);
                                        {
                                            V(SyntaxKind.FalseKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"libs/L\"", value: ImmutableArray.Create("libs/L"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'K'", value: ImmutableArray.Create("K"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'L'", value: ImmutableArray.Create("L"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.FalseLiteralExpression);
                                        {
                                            V(SyntaxKind.FalseKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"libs/L\"", value: ImmutableArray.Create("libs/L"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "rawget");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "_G");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"_REQUIREDNAME\"", value: ImmutableArray.Create("_REQUIREDNAME"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NilLiteralExpression);
                                            {
                                                V(SyntaxKind.NilKeyword);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"x\"", value: ImmutableArray.Create("x"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "try");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"X\"", value: ImmutableArray.Create("X"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"XXxX\"", value: ImmutableArray.Create("XXxX"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "AA");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"libs/XXxX\"", value: ImmutableArray.Create("libs/XXxX"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "removefiles");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "files");
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "NAME");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "REQUIRED");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "B");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "_G");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "_G");
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "string");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "gsub");
                                    }
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"D/?.lua;D/?/init.lua\"", value: ImmutableArray.Create("D/?.lua;D/?/init.lua"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"D/\"", value: ImmutableArray.Create("D/"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "DIR");
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "files");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.KeyValueField);
                                {
                                    V(SyntaxKind.OpenBracketToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"P1/init.lua\"", value: ImmutableArray.Create("P1/init.lua"U8.ToArray()));
                                    }
                                    V(SyntaxKind.CloseBracketToken);
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"AA = 10\"", value: ImmutableArray.Create("AA = 10"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.KeyValueField);
                                {
                                    V(SyntaxKind.OpenBracketToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"P1/xuxu.lua\"", value: ImmutableArray.Create("P1/xuxu.lua"U8.ToArray()));
                                    }
                                    V(SyntaxKind.CloseBracketToken);
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"AA = 20\"", value: ImmutableArray.Create("AA = 20"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.CloseBraceToken);
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "createfiles");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "files");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"_ENV = {}\\n\"", value: ImmutableArray.Create("_ENV = {}\n"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\nreturn _ENV\\n\"", value: ImmutableArray.Create("\nreturn _ENV\n"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "m");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "ext");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "assert");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"P1\"", value: ImmutableArray.Create("P1"U8.ToArray()));
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "ext");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"libs/P1/init.lua\"", value: ImmutableArray.Create("libs/P1/init.lua"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "AA");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "m");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "AA");
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"P1\"", value: ImmutableArray.Create("P1"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "m");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"P1\"", value: ImmutableArray.Create("P1"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "m");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "searchpath");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"P1.xuxu\"", value: ImmutableArray.Create("P1.xuxu"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "package");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "path");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "D");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"P1/xuxu.lua\"", value: ImmutableArray.Create("P1/xuxu.lua"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "m");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "xuxu");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "ext");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "require");
                                            }
                                            V(SyntaxKind.ArgumentString);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"P1.xuxu\"", value: ImmutableArray.Create("P1.xuxu"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "AA");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "m");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "xuxu");
                                                        }
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "AA");
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "ext");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"libs/P1/xuxu.lua\"", value: ImmutableArray.Create("libs/P1/xuxu.lua"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"P1.xuxu\"", value: ImmutableArray.Create("P1.xuxu"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "m");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "xuxu");
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"P1.xuxu\"", value: ImmutableArray.Create("P1.xuxu"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "m");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "xuxu");
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "require");
                                                    }
                                                    V(SyntaxKind.ArgumentString);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"P1\"", value: ImmutableArray.Create("P1"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "m");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "m");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "AA");
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "removefiles");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "files");
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "AA");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pcall");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "require");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"file_does_not_exist\"", value: ImmutableArray.Create("file_does_not_exist"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"??\\0?\"", value: ImmutableArray.Create("??\0?"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pcall");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "require");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"file_does_not_exist1\"", value: ImmutableArray.Create("file_does_not_exist1"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "package");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "path");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "oldpath");
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "fname");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"file_does_not_exist2\"", value: ImmutableArray.Create("file_does_not_exist2"U8.ToArray()));
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "m");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "err");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pcall");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "require");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "fname");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.ForInStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "t");
                            }
                            V(SyntaxKind.InKeyword);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "string");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "gmatch");
                                    }
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "path");
                                                }
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\";\"", value: ImmutableArray.Create(";"U8.ToArray()));
                                                }
                                                V(SyntaxKind.DotDotToken);
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "cpath");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"[^;]+\"", value: ImmutableArray.Create("[^;]+"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                            V(SyntaxKind.DoKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "t");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "string");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "gsub");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "fname");
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "string");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "find");
                                                        }
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "err");
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "t");
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.TrueLiteralExpression);
                                                            {
                                                                V(SyntaxKind.TrueKeyword);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.DoStatement);
                        {
                            V(SyntaxKind.DoKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "searchers");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "package");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "searchers");
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "package");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "searchers");
                                        }
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                    }
                                }
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "st");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "msg");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pcall");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "require");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.LogicalNotExpression);
                                                    {
                                                        V(SyntaxKind.NotKeyword);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "st");
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "string");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "find");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "msg");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"must be a table\"", value: ImmutableArray.Create("must be a table"U8.ToArray()));
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "package");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "searchers");
                                        }
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "searchers");
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "import");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.DotDotDotToken);
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "f");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.TableConstructorExpression);
                                        {
                                            V(SyntaxKind.OpenBraceToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.VariousArgumentsExpression);
                                                {
                                                    V(SyntaxKind.DotDotDotToken);
                                                }
                                            }
                                            V(SyntaxKind.CloseBraceToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.ReturnStatement);
                                {
                                    V(SyntaxKind.ReturnKeyword);
                                    V(SyntaxKind.FunctionDefinitionExpression);
                                    {
                                        V(SyntaxKind.FunctionKeyword);
                                        V(SyntaxKind.ParameterList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Parameter);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "m");
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                        V(SyntaxKind.Block);
                                        {
                                            V(SyntaxKind.ForStatement);
                                            {
                                                V(SyntaxKind.ForKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                }
                                                V(SyntaxKind.EqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.LengthExpression);
                                                {
                                                    V(SyntaxKind.HashToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "f");
                                                    }
                                                }
                                                V(SyntaxKind.DoKeyword);
                                                V(SyntaxKind.Block);
                                                {
                                                    V(SyntaxKind.AssignmentStatement);
                                                    {
                                                        V(SyntaxKind.IndexMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "m");
                                                            }
                                                            V(SyntaxKind.OpenBracketToken);
                                                            V(SyntaxKind.IndexMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "f");
                                                                }
                                                                V(SyntaxKind.OpenBracketToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                                V(SyntaxKind.CloseBracketToken);
                                                            }
                                                            V(SyntaxKind.CloseBracketToken);
                                                        }
                                                        V(SyntaxKind.EqualsToken);
                                                        V(SyntaxKind.IndexMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "_G");
                                                            }
                                                            V(SyntaxKind.OpenBracketToken);
                                                            V(SyntaxKind.IndexMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "f");
                                                                }
                                                                V(SyntaxKind.OpenBracketToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                                V(SyntaxKind.CloseBracketToken);
                                                            }
                                                            V(SyntaxKind.CloseBracketToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.EndKeyword);
                                            }
                                        }
                                        V(SyntaxKind.EndKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pcall");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "module");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "'XUXU'", value: ImmutableArray.Create("XUXU"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "p");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "st");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "err");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "when");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "package");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "loadlib");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "DC");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"lib1\"", value: ImmutableArray.Create("lib1"U8.ToArray()));
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"*\"", value: ImmutableArray.Create("*"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.LogicalNotExpression);
                            {
                                V(SyntaxKind.NotKeyword);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "st");
                                }
                            }
                            V(SyntaxKind.ThenKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "f");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "err");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "when");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "loadlib");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"donotexist\"", value: ImmutableArray.Create("donotexist"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.ConcatenationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                        }
                                                        V(SyntaxKind.DotDotToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"xuxu\"", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.LogicalNotExpression);
                                                        {
                                                            V(SyntaxKind.NotKeyword);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "f");
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "type");
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "err");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "when");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"absent\"", value: ImmutableArray.Create("absent"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.EmptyStatement);
                                {
                                    V(SyntaxKind.SemicolonToken);
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.ParenthesizedExpression);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.OrExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "Message");
                                                }
                                                V(SyntaxKind.OrKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "print");
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "'\\n >>> cannot load dynamic library <<<\\n'", value: ImmutableArray.Create("\n >>> cannot load dynamic library <<<\n"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "print");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "err");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "when");
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.ElseClause);
                            {
                                V(SyntaxKind.ElseKeyword);
                                V(SyntaxKind.Block);
                                {
                                    V(SyntaxKind.LocalDeclarationStatement);
                                    {
                                        V(SyntaxKind.LocalKeyword);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "f");
                                            }
                                        }
                                        V(SyntaxKind.EqualsValuesClause);
                                        {
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "loadlib");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "DC");
                                                                        }
                                                                        V(SyntaxKind.ArgumentString);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"lib1\"", value: ImmutableArray.Create("lib1"U8.ToArray()));
                                                                        }
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                                        }
                                                                        V(SyntaxKind.DotDotToken);
                                                                        V(SyntaxKind.StringLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"onefunction\"", value: ImmutableArray.Create("onefunction"U8.ToArray()));
                                                                        }
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.LocalDeclarationStatement);
                                    {
                                        V(SyntaxKind.LocalKeyword);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                        }
                                        V(SyntaxKind.EqualsValuesClause);
                                        {
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "f");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "15", value: 15L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "25", value: 25L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "25", value: 25L);
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "b");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "15", value: 15L);
                                                            }
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "f");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "package");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "loadlib");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.InvocationExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "DC");
                                                                    }
                                                                    V(SyntaxKind.ArgumentString);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"lib1\"", value: ImmutableArray.Create("lib1"U8.ToArray()));
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.ConcatenationExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "p");
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"anotherfunc\"", value: ImmutableArray.Create("anotherfunc"U8.ToArray()));
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "f");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"10%20\\n\"", value: ImmutableArray.Create("10%20\n"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.LocalDeclarationStatement);
                                    {
                                        V(SyntaxKind.LocalKeyword);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "f");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "err");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "when");
                                            }
                                        }
                                        V(SyntaxKind.EqualsValuesClause);
                                        {
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "loadlib");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "DC");
                                                            }
                                                            V(SyntaxKind.ArgumentString);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"lib1\"", value: ImmutableArray.Create("lib1"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.ConcatenationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "p");
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"xuxu\"", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.AndExpression);
                                                        {
                                                            V(SyntaxKind.LogicalNotExpression);
                                                            {
                                                                V(SyntaxKind.NotKeyword);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "f");
                                                                }
                                                            }
                                                            V(SyntaxKind.AndKeyword);
                                                            V(SyntaxKind.EqualExpression);
                                                            {
                                                                V(SyntaxKind.InvocationExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "type");
                                                                    }
                                                                    V(SyntaxKind.ArgumentList);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.Argument);
                                                                        {
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "err");
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                }
                                                                V(SyntaxKind.EqualsEqualsToken);
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "when");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"init\"", value: ImmutableArray.Create("init"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "f");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "err");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "when");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "package");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "loadlib");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"donotexist\"", value: ImmutableArray.Create("donotexist"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.ConcatenationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                        }
                                                        V(SyntaxKind.DotDotToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"xuxu\"", value: ImmutableArray.Create("xuxu"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.AndExpression);
                                                        {
                                                            V(SyntaxKind.LogicalNotExpression);
                                                            {
                                                                V(SyntaxKind.NotKeyword);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "f");
                                                                }
                                                            }
                                                            V(SyntaxKind.AndKeyword);
                                                            V(SyntaxKind.EqualExpression);
                                                            {
                                                                V(SyntaxKind.InvocationExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "type");
                                                                    }
                                                                    V(SyntaxKind.ArgumentList);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.Argument);
                                                                        {
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "err");
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                }
                                                                V(SyntaxKind.EqualsEqualsToken);
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "when");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"open\"", value: ImmutableArray.Create("open"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "f");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "package");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "loadlib");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.InvocationExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "DC");
                                                                    }
                                                                    V(SyntaxKind.ArgumentString);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"lib11\"", value: ImmutableArray.Create("lib11"U8.ToArray()));
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.ConcatenationExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "p");
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"luaopen_lib11\"", value: ImmutableArray.Create("luaopen_lib11"U8.ToArray()));
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "f");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"exported\"", value: ImmutableArray.Create("exported"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "package");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "cpath");
                                            }
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "DC");
                                            }
                                            V(SyntaxKind.ArgumentString);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"?\"", value: ImmutableArray.Create("?"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.LocalDeclarationStatement);
                                    {
                                        V(SyntaxKind.LocalKeyword);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lib2");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "ext");
                                            }
                                        }
                                        V(SyntaxKind.EqualsValuesClause);
                                        {
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"lib2-v2\"", value: ImmutableArray.Create("lib2-v2"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "string");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "find");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "ext");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"libs/lib2-v2\"", value: ImmutableArray.Create("libs/lib2-v2"U8.ToArray()));
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.TrueLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.TrueKeyword);
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "_ENV");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"lib2-v2\"", value: ImmutableArray.Create("lib2-v2"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "_ENV");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "DC");
                                                                }
                                                                V(SyntaxKind.ArgumentString);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"lib2-v2\"", value: ImmutableArray.Create("lib2-v2"U8.ToArray()));
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "lib2");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "id");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"x\"", value: ImmutableArray.Create("x"U8.ToArray()));
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.TrueLiteralExpression);
                                                        {
                                                            V(SyntaxKind.TrueKeyword);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.LocalDeclarationStatement);
                                    {
                                        V(SyntaxKind.LocalKeyword);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "fs");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "ext");
                                            }
                                        }
                                        V(SyntaxKind.EqualsValuesClause);
                                        {
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "require");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"lib1.sub\"", value: ImmutableArray.Create("lib1.sub"U8.ToArray()));
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "_ENV");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"lib1.sub\"", value: ImmutableArray.Create("lib1.sub"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "_ENV");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "DC");
                                                                }
                                                                V(SyntaxKind.ArgumentString);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"lib1\"", value: ImmutableArray.Create("lib1"U8.ToArray()));
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "string");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "find");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "ext");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"libs/lib1\"", value: ImmutableArray.Create("libs/lib1"U8.ToArray()));
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.TrueLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.TrueKeyword);
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.InvocationStatement);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "fs");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "id");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "45", value: 45L);
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "45", value: 45L);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "_ENV");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "_ENV");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "_ENV");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "_G");
                            }
                        }
                        V(SyntaxKind.DoStatement);
                        {
                            V(SyntaxKind.DoKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "p");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "package");
                                        }
                                    }
                                }
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.TableConstructorExpression);
                                    {
                                        V(SyntaxKind.OpenBraceToken);
                                        V(SyntaxKind.CloseBraceToken);
                                    }
                                }
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "p");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "preload");
                                            }
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pl");
                                        }
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.FunctionDefinitionExpression);
                                    {
                                        V(SyntaxKind.FunctionKeyword);
                                        V(SyntaxKind.ParameterList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Parameter);
                                            {
                                                V(SyntaxKind.DotDotDotToken);
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                        V(SyntaxKind.Block);
                                        {
                                            V(SyntaxKind.LocalDeclarationStatement);
                                            {
                                                V(SyntaxKind.LocalKeyword);
                                                V(SyntaxKind.NameAttributeList);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "_ENV");
                                                    }
                                                }
                                                V(SyntaxKind.EqualsValuesClause);
                                                {
                                                    V(SyntaxKind.EqualsToken);
                                                    V(SyntaxKind.TableConstructorExpression);
                                                    {
                                                        V(SyntaxKind.OpenBraceToken);
                                                        V(SyntaxKind.ItemField);
                                                        {
                                                            V(SyntaxKind.VariousArgumentsExpression);
                                                            {
                                                                V(SyntaxKind.DotDotDotToken);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseBraceToken);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.FunctionDefinitionStatement);
                                            {
                                                V(SyntaxKind.FunctionKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "xuxu");
                                                }
                                                V(SyntaxKind.ParameterList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Parameter);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                                V(SyntaxKind.Block);
                                                {
                                                    V(SyntaxKind.ReturnStatement);
                                                    {
                                                        V(SyntaxKind.ReturnKeyword);
                                                        V(SyntaxKind.AdditionExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "x");
                                                            }
                                                            V(SyntaxKind.PlusToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                            }
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.EndKeyword);
                                            }
                                            V(SyntaxKind.ReturnStatement);
                                            {
                                                V(SyntaxKind.ReturnKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "_ENV");
                                                }
                                            }
                                        }
                                        V(SyntaxKind.EndKeyword);
                                    }
                                }
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pl");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "ext");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "require");
                                            }
                                            V(SyntaxKind.ArgumentString);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"pl\"", value: ImmutableArray.Create("pl"U8.ToArray()));
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "require");
                                                        }
                                                        V(SyntaxKind.ArgumentString);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"pl\"", value: ImmutableArray.Create("pl"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pl");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pl");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "xuxu");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "30", value: 30L);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IndexMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pl");
                                                                }
                                                                V(SyntaxKind.OpenBracketToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
                                                                V(SyntaxKind.CloseBracketToken);
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"pl\"", value: ImmutableArray.Create("pl"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IndexMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pl");
                                                                }
                                                                V(SyntaxKind.OpenBracketToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                }
                                                                V(SyntaxKind.CloseBracketToken);
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\":preload:\"", value: ImmutableArray.Create(":preload:"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "ext");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\":preload:\"", value: ImmutableArray.Create(":preload:"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "package");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "p");
                                    }
                                }
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "assert");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "type");
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "package");
                                                                    }
                                                                    V(SyntaxKind.DotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "path");
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'+'", value: ImmutableArray.Create("+"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"testing assignments, logical operators, and constructors\"", value: ImmutableArray.Create("testing assignments, logical operators, and constructors"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "res");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "res2");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "27", value: 27L);
                        }
                    }
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "b");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                            }
                            V(SyntaxKind.PlusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                            }
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "b");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.TableConstructorExpression);
                    {
                        V(SyntaxKind.OpenBraceToken);
                        V(SyntaxKind.CloseBraceToken);
                    }
                }
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "f");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "x");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "b");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "f");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "b");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                            V(SyntaxKind.PlusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.StringLiteralExpression);
                    {
                        V(SyntaxKind.StringLiteralToken, text: "'x'", value: ImmutableArray.Create("x"U8.ToArray()));
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "13", value: 13L);
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'x'", value: ImmutableArray.Create("x"U8.ToArray()));
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.FunctionDefinitionExpression);
                                {
                                    V(SyntaxKind.FunctionKeyword);
                                    V(SyntaxKind.ParameterList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Parameter);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.Block);
                                    {
                                        V(SyntaxKind.LocalDeclarationStatement);
                                        {
                                            V(SyntaxKind.LocalKeyword);
                                            V(SyntaxKind.NameAttributeList);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.EqualsValuesClause);
                                            {
                                                V(SyntaxKind.EqualsToken);
                                                V(SyntaxKind.TableConstructorExpression);
                                                {
                                                    V(SyntaxKind.OpenBraceToken);
                                                    V(SyntaxKind.CloseBraceToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.EmptyStatement);
                                        {
                                            V(SyntaxKind.SemicolonToken);
                                        }
                                        V(SyntaxKind.ForStatement);
                                        {
                                            V(SyntaxKind.ForKeyword);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                            V(SyntaxKind.EqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "n");
                                            }
                                            V(SyntaxKind.DoKeyword);
                                            V(SyntaxKind.Block);
                                            {
                                                V(SyntaxKind.AssignmentStatement);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "x");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.EndKeyword);
                                        }
                                        V(SyntaxKind.EmptyStatement);
                                        {
                                            V(SyntaxKind.SemicolonToken);
                                        }
                                        V(SyntaxKind.ReturnStatement);
                                        {
                                            V(SyntaxKind.ReturnKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "table");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "x");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EndKeyword);
                                }
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "b");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "c");
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "b");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "b");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "c");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "c");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "c");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "c");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NilLiteralExpression);
                                                {
                                                    V(SyntaxKind.NilKeyword);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "b");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "c");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "d");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.AndExpression);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.AndKeyword);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.OrExpression);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.OrKeyword);
                            V(SyntaxKind.NilLiteralExpression);
                            {
                                V(SyntaxKind.NilKeyword);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ParenthesizedExpression);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.AndExpression);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                }
                                V(SyntaxKind.AndKeyword);
                                V(SyntaxKind.ParenthesizedExpression);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.OrExpression);
                                    {
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                        V(SyntaxKind.OrKeyword);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.LogicalNotExpression);
                                            {
                                                V(SyntaxKind.NotKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "c");
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "d");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "d");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "b");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "c");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "d");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "b");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "c");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "d");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "b");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "b");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.LessThanExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.LessThanToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.FalseLiteralExpression);
                                        {
                                            V(SyntaxKind.FalseKeyword);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.GreaterThanExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.GreaterThanToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.TrueLiteralExpression);
                                        {
                                            V(SyntaxKind.TrueKeyword);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.OrExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                            V(SyntaxKind.OrKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.OrExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                            V(SyntaxKind.OrKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NilLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NilKeyword);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.LogicalNotExpression);
                                {
                                    V(SyntaxKind.NotKeyword);
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.NilLiteralExpression);
                                            {
                                                V(SyntaxKind.NilKeyword);
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NilLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NilKeyword);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.OrExpression);
                                        {
                                            V(SyntaxKind.NilLiteralExpression);
                                            {
                                                V(SyntaxKind.NilKeyword);
                                            }
                                            V(SyntaxKind.OrKeyword);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"alo\"", value: ImmutableArray.Create("alo"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"alo\"", value: ImmutableArray.Create("alo"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.NilLiteralExpression);
                                            {
                                                V(SyntaxKind.NilKeyword);
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NilLiteralExpression);
                                    {
                                        V(SyntaxKind.NilKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.FalseLiteralExpression);
                                            {
                                                V(SyntaxKind.FalseKeyword);
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.OrExpression);
                                        {
                                            V(SyntaxKind.TrueLiteralExpression);
                                            {
                                                V(SyntaxKind.TrueKeyword);
                                            }
                                            V(SyntaxKind.OrKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.TrueLiteralExpression);
                                    {
                                        V(SyntaxKind.TrueKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.OrExpression);
                                        {
                                            V(SyntaxKind.FalseLiteralExpression);
                                            {
                                                V(SyntaxKind.FalseKeyword);
                                            }
                                            V(SyntaxKind.OrKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NotEqualExpression);
                                {
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                    V(SyntaxKind.TildeEqualsToken);
                                    V(SyntaxKind.NilLiteralExpression);
                                    {
                                        V(SyntaxKind.NilKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NotEqualExpression);
                                {
                                    V(SyntaxKind.NilLiteralExpression);
                                    {
                                        V(SyntaxKind.NilKeyword);
                                    }
                                    V(SyntaxKind.TildeEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.TrueLiteralExpression);
                                    {
                                        V(SyntaxKind.TrueKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.NilLiteralExpression);
                                            {
                                                V(SyntaxKind.NilKeyword);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.TrueLiteralExpression);
                                    {
                                        V(SyntaxKind.TrueKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.TrueLiteralExpression);
                                    {
                                        V(SyntaxKind.TrueKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.OrExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
                                                    }
                                                    V(SyntaxKind.OrKeyword);
                                                    V(SyntaxKind.NilLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NilKeyword);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.TrueLiteralExpression);
                                    {
                                        V(SyntaxKind.TrueKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.NilLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NilKeyword);
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "56", value: 56L);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.LogicalNotExpression);
                                        {
                                            V(SyntaxKind.NotKeyword);
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.NilLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NilKeyword);
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.TrueLiteralExpression);
                                                    {
                                                        V(SyntaxKind.TrueKeyword);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.TableConstructorExpression);
                                        {
                                            V(SyntaxKind.OpenBraceToken);
                                            V(SyntaxKind.CloseBraceToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0.5", value: 0.5D);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"x\"", value: ImmutableArray.Create("x"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NotEqualExpression);
                                {
                                    V(SyntaxKind.TableConstructorExpression);
                                    {
                                        V(SyntaxKind.OpenBraceToken);
                                        V(SyntaxKind.CloseBraceToken);
                                    }
                                    V(SyntaxKind.TildeEqualsToken);
                                    V(SyntaxKind.TableConstructorExpression);
                                    {
                                        V(SyntaxKind.OpenBraceToken);
                                        V(SyntaxKind.CloseBraceToken);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "'+'", value: ImmutableArray.Create("+"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.TableConstructorExpression);
                    {
                        V(SyntaxKind.OpenBraceToken);
                        V(SyntaxKind.CloseBraceToken);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.TrueLiteralExpression);
                        {
                            V(SyntaxKind.TrueKeyword);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.FalseLiteralExpression);
                        {
                            V(SyntaxKind.FalseKeyword);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.LessThanExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                                V(SyntaxKind.LessThanToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.GreaterThanExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                                V(SyntaxKind.GreaterThanToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "f");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.TableConstructorExpression);
                        {
                            V(SyntaxKind.OpenBraceToken);
                            V(SyntaxKind.CloseBraceToken);
                        }
                    }
                }
                V(SyntaxKind.ForStatement);
                {
                    V(SyntaxKind.ForKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "i");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "3000", value: 3000L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.UnaryMinusExpression);
                    {
                        V(SyntaxKind.MinusToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "3000", value: 3000L);
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.UnaryMinusExpression);
                    {
                        V(SyntaxKind.MinusToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                    }
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.AdditionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.PlusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "0.0", value: 0D);
                                    }
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "10e30", value: 1E+31D);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.StringLiteralExpression);
                    {
                        V(SyntaxKind.StringLiteralToken, text: "\"alo\"", value: ImmutableArray.Create("alo"U8.ToArray()));
                    }
                }
                V(SyntaxKind.EmptyStatement);
                {
                    V(SyntaxKind.SemicolonToken);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.TrueLiteralExpression);
                        {
                            V(SyntaxKind.TrueKeyword);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                    }
                }
                V(SyntaxKind.EmptyStatement);
                {
                    V(SyntaxKind.SemicolonToken);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.FalseLiteralExpression);
                        {
                            V(SyntaxKind.FalseKeyword);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10e30", value: 1E+31D);
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "'alo'", value: ImmutableArray.Create("alo"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.LogicalNotExpression);
                                                {
                                                    V(SyntaxKind.NotKeyword);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                    }
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.LessThanExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                                V(SyntaxKind.LessThanToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.ForStatement);
                {
                    V(SyntaxKind.ForKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "i");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "3000", value: 3000L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.UnaryMinusExpression);
                    {
                        V(SyntaxKind.MinusToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "3000", value: 3000L);
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.UnaryMinusExpression);
                    {
                        V(SyntaxKind.MinusToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                    }
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "assert");
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "print");
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "print");
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "assert");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IndexMemberAccessExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.OpenBracketToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "print");
                            }
                            V(SyntaxKind.CloseBracketToken);
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "f");
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "print");
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.LogicalNotExpression);
                                {
                                    V(SyntaxKind.NotKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pcall");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.FunctionDefinitionExpression);
                                                {
                                                    V(SyntaxKind.FunctionKeyword);
                                                    V(SyntaxKind.ParameterList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                    V(SyntaxKind.Block);
                                                    {
                                                        V(SyntaxKind.LocalDeclarationStatement);
                                                        {
                                                            V(SyntaxKind.LocalKeyword);
                                                            V(SyntaxKind.NameAttributeList);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsValuesClause);
                                                            {
                                                                V(SyntaxKind.EqualsToken);
                                                                V(SyntaxKind.TableConstructorExpression);
                                                                {
                                                                    V(SyntaxKind.OpenBraceToken);
                                                                    V(SyntaxKind.CloseBraceToken);
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.EmptyStatement);
                                                        {
                                                            V(SyntaxKind.SemicolonToken);
                                                        }
                                                        V(SyntaxKind.AssignmentStatement);
                                                        {
                                                            V(SyntaxKind.IndexMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                }
                                                                V(SyntaxKind.OpenBracketToken);
                                                                V(SyntaxKind.NilLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NilKeyword);
                                                                }
                                                                V(SyntaxKind.CloseBracketToken);
                                                            }
                                                            V(SyntaxKind.EqualsToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.EndKeyword);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.LogicalNotExpression);
                                {
                                    V(SyntaxKind.NotKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pcall");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.FunctionDefinitionExpression);
                                                {
                                                    V(SyntaxKind.FunctionKeyword);
                                                    V(SyntaxKind.ParameterList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                    V(SyntaxKind.Block);
                                                    {
                                                        V(SyntaxKind.LocalDeclarationStatement);
                                                        {
                                                            V(SyntaxKind.LocalKeyword);
                                                            V(SyntaxKind.NameAttributeList);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsValuesClause);
                                                            {
                                                                V(SyntaxKind.EqualsToken);
                                                                V(SyntaxKind.TableConstructorExpression);
                                                                {
                                                                    V(SyntaxKind.OpenBraceToken);
                                                                    V(SyntaxKind.KeyValueField);
                                                                    {
                                                                        V(SyntaxKind.OpenBracketToken);
                                                                        V(SyntaxKind.NilLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.NilKeyword);
                                                                        }
                                                                        V(SyntaxKind.CloseBracketToken);
                                                                        V(SyntaxKind.EqualsToken);
                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseBraceToken);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.EndKeyword);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "undef");
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NilLiteralExpression);
                    {
                        V(SyntaxKind.NilKeyword);
                    }
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "b");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "c");
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.TableConstructorExpression);
                    {
                        V(SyntaxKind.OpenBraceToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "9", value: 9L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "8", value: 8L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "7", value: 7L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                            }
                        }
                        V(SyntaxKind.SemicolonToken);
                        V(SyntaxKind.KeyValueField);
                        {
                            V(SyntaxKind.OpenBracketToken);
                            V(SyntaxKind.UnaryMinusExpression);
                            {
                                V(SyntaxKind.MinusToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                }
                            }
                            V(SyntaxKind.CloseBracketToken);
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.KeyValueField);
                        {
                            V(SyntaxKind.OpenBracketToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "f");
                            }
                            V(SyntaxKind.CloseBracketToken);
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "print");
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.NameValueField);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.NameValueField);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "'ab'", value: ImmutableArray.Create("ab"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.CloseBraceToken);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "x");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "y");
                        }
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.UnaryMinusExpression);
                        {
                            V(SyntaxKind.MinusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.UnaryMinusExpression);
                                                        {
                                                            V(SyntaxKind.MinusToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "f");
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "print");
                                                }
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "f");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "a");
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "b");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "c");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.TableConstructorExpression);
                    {
                        V(SyntaxKind.OpenBraceToken);
                        V(SyntaxKind.KeyValueField);
                        {
                            V(SyntaxKind.OpenBracketToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "'alo'", value: ImmutableArray.Create("alo"U8.ToArray()));
                            }
                            V(SyntaxKind.CloseBracketToken);
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "assert");
                            }
                        }
                        V(SyntaxKind.CloseBraceToken);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "23", value: 23L);
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.SimpleMemberAccessExpression);
                        {
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.DotToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "alo");
                            }
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "c");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "print");
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "aVeryLongName012345678901234567890123456789012345678901234567890123456789");
                        }
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                    }
                }
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "foo");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "aVeryLongName012345678901234567890123456789012345678901234567890123456789");
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "foo");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "aVeryLongName012345678901234567890123456789012345678901234567890123456789");
                                            }
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "foo");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.LocalDeclarationStatement);
                                {
                                    V(SyntaxKind.LocalKeyword);
                                    V(SyntaxKind.NameAttributeList);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "_ENV");
                                        }
                                        V(SyntaxKind.AttributeList);
                                        {
                                            V(SyntaxKind.LessThanToken);
                                            V(SyntaxKind.Attribute);
                                            {
                                                V(SyntaxKind.ConstKeyword);
                                            }
                                            V(SyntaxKind.GreaterThanToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
                                        }
                                    }
                                }
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "X");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"hi\"", value: ImmutableArray.Create("hi"U8.ToArray()));
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "st");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "msg");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pcall");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "foo");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.LogicalNotExpression);
                                            {
                                                V(SyntaxKind.NotKeyword);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "st");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "string");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "find");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "msg");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"number\"", value: ImmutableArray.Create("number"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "maxint");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.SimpleMemberAccessExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "math");
                            }
                            V(SyntaxKind.DotToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxinteger");
                            }
                        }
                    }
                }
                V(SyntaxKind.WhileStatement);
                {
                    V(SyntaxKind.WhileKeyword);
                    V(SyntaxKind.OrExpression);
                    {
                        V(SyntaxKind.NotEqualExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxint");
                            }
                            V(SyntaxKind.TildeEqualsToken);
                            V(SyntaxKind.ParenthesizedExpression);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.AdditionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "maxint");
                                    }
                                    V(SyntaxKind.PlusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "0.0", value: 0D);
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.OrKeyword);
                        V(SyntaxKind.NotEqualExpression);
                        {
                            V(SyntaxKind.ParenthesizedExpression);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.SubtractionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "maxint");
                                    }
                                    V(SyntaxKind.MinusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.TildeEqualsToken);
                            V(SyntaxKind.ParenthesizedExpression);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.SubtractionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "maxint");
                                    }
                                    V(SyntaxKind.MinusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1.0", value: 1D);
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                    }
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxint");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.FloorDivisionExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "maxint");
                                }
                                V(SyntaxKind.SlashSlashToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "maxintF");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxint");
                            }
                            V(SyntaxKind.PlusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0.0", value: 0D);
                            }
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "maxintF");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "maxint");
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "math");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "type");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "maxintF");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"float\"", value: ImmutableArray.Create("float"U8.ToArray()));
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.GreaterThanOrEqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "maxintF");
                                        }
                                        V(SyntaxKind.GreaterThanEqualsToken);
                                        V(SyntaxKind.ExponentiationExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2.0", value: 2D);
                                            }
                                            V(SyntaxKind.CaretToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "14", value: 14L);
                                            }
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "maxintF");
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                    }
                }
                V(SyntaxKind.EmptyStatement);
                {
                    V(SyntaxKind.SemicolonToken);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.SubtractionExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxintF");
                            }
                            V(SyntaxKind.MinusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1.0", value: 1D);
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
                    }
                }
                V(SyntaxKind.EmptyStatement);
                {
                    V(SyntaxKind.SemicolonToken);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.UnaryMinusExpression);
                        {
                            V(SyntaxKind.MinusToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxintF");
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
                    }
                }
                V(SyntaxKind.EmptyStatement);
                {
                    V(SyntaxKind.SemicolonToken);
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.UnaryMinusExpression);
                            {
                                V(SyntaxKind.MinusToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "maxintF");
                                }
                            }
                            V(SyntaxKind.PlusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1.0", value: 1D);
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "13", value: 13L);
                    }
                }
                V(SyntaxKind.EmptyStatement);
                {
                    V(SyntaxKind.SemicolonToken);
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "maxint");
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "maxint");
                                                        }
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.UnaryMinusExpression);
                                                {
                                                    V(SyntaxKind.MinusToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "maxint");
                                                    }
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.AdditionExpression);
                                            {
                                                V(SyntaxKind.UnaryMinusExpression);
                                                {
                                                    V(SyntaxKind.MinusToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "maxint");
                                                    }
                                                }
                                                V(SyntaxKind.PlusToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "13", value: 13L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "maxint");
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.UnaryMinusExpression);
                        {
                            V(SyntaxKind.MinusToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "maxint");
                            }
                        }
                        V(SyntaxKind.CloseBracketToken);
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NumericLiteralExpression);
                    {
                        V(SyntaxKind.NumericLiteralToken, text: "22", value: 22L);
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "maxintF");
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "maxintF");
                                                        }
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1.0", value: 1D);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.UnaryMinusExpression);
                                                {
                                                    V(SyntaxKind.MinusToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "maxintF");
                                                    }
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "22", value: 22L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.AdditionExpression);
                                            {
                                                V(SyntaxKind.UnaryMinusExpression);
                                                {
                                                    V(SyntaxKind.MinusToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "maxintF");
                                                    }
                                                }
                                                V(SyntaxKind.PlusToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1.0", value: 1D);
                                                }
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "13", value: 13L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.NilLiteralExpression);
                    {
                        V(SyntaxKind.NilKeyword);
                    }
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "j");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "b");
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "'b'", value: ImmutableArray.Create("b"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CloseBraceToken);
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "j");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "j");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "j");
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.AdditionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.PlusToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "j");
                                    }
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "j");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "j");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.AndExpression);
                                                        {
                                                            V(SyntaxKind.EqualExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                                V(SyntaxKind.EqualsEqualsToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                }
                                                            }
                                                            V(SyntaxKind.AndKeyword);
                                                            V(SyntaxKind.EqualExpression);
                                                            {
                                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                                    }
                                                                    V(SyntaxKind.OpenBracketToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                    V(SyntaxKind.CloseBracketToken);
                                                                }
                                                                V(SyntaxKind.EqualsEqualsToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "j");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "b");
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "b");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.CloseBraceToken);
                            }
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "foo");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "b");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "x");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "a");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "a");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "foo");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.DoStatement);
                {
                    V(SyntaxKind.DoKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "j");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "b");
                                }
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "'b'", value: ImmutableArray.Create("b"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CloseBraceToken);
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "j");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "b");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "a");
                            }
                        }
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "foo");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "i");
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "a");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "j");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "j");
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IndexMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.OpenBracketToken);
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                            V(SyntaxKind.PlusToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "j");
                                            }
                                        }
                                        V(SyntaxKind.CloseBracketToken);
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "j");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "b");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "j");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "foo");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.AndExpression);
                                            {
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.AndExpression);
                                                    {
                                                        V(SyntaxKind.AndExpression);
                                                        {
                                                            V(SyntaxKind.EqualExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                                V(SyntaxKind.EqualsEqualsToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                }
                                                            }
                                                            V(SyntaxKind.AndKeyword);
                                                            V(SyntaxKind.EqualExpression);
                                                            {
                                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                                    }
                                                                    V(SyntaxKind.OpenBracketToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                    V(SyntaxKind.CloseBracketToken);
                                                                }
                                                                V(SyntaxKind.EqualsEqualsToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.AndKeyword);
                                                        V(SyntaxKind.EqualExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "j");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "b");
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "b");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "b");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "t");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.TableConstructorExpression);
                                        {
                                            V(SyntaxKind.OpenBraceToken);
                                            V(SyntaxKind.CloseBraceToken);
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.FunctionDefinitionExpression);
                                                {
                                                    V(SyntaxKind.FunctionKeyword);
                                                    V(SyntaxKind.ParameterList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Parameter);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                    V(SyntaxKind.Block);
                                                    {
                                                        V(SyntaxKind.AssignmentStatement);
                                                        {
                                                            V(SyntaxKind.IndexMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                                }
                                                                V(SyntaxKind.OpenBracketToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                }
                                                                V(SyntaxKind.CloseBracketToken);
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                            }
                                                            V(SyntaxKind.EqualsToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.EndKeyword);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                                V(SyntaxKind.OpenBracketToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "foo");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                            }
                        }
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.FunctionDefinitionExpression);
                            {
                                V(SyntaxKind.FunctionKeyword);
                                V(SyntaxKind.ParameterList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.CloseParenToken);
                                }
                                V(SyntaxKind.Block);
                                {
                                    V(SyntaxKind.LocalDeclarationStatement);
                                    {
                                        V(SyntaxKind.LocalKeyword);
                                        V(SyntaxKind.NameAttributeList);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "b");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "14", value: 14L);
                                        }
                                    }
                                    V(SyntaxKind.ReturnStatement);
                                    {
                                        V(SyntaxKind.ReturnKeyword);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "b");
                                        }
                                    }
                                }
                                V(SyntaxKind.EndKeyword);
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "b");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "foo");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "assert");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AndExpression);
                                {
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "b");
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "14", value: 14L);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "'OK'", value: ImmutableArray.Create("OK"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.ReturnStatement);
                {
                    V(SyntaxKind.ReturnKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "res");
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
