// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_utf8(string source, SourceCodeKind kind)
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
                            V(SyntaxKind.StringLiteralToken, text: "\"testing UTF-8 library\"", value: ImmutableArray.Create("testing UTF-8 library"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "utf8");
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
                                V(SyntaxKind.StringLiteralToken, text: "'utf8'", value: ImmutableArray.Create("utf8"U8.ToArray()));
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
                        V(SyntaxKind.IdentifierToken, text: "checkerror");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "msg");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.CommaToken);
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
                                                V(SyntaxKind.IdentifierToken, text: "f");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.VariousArgumentsExpression);
                                            {
                                                V(SyntaxKind.DotDotDotToken);
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "msg");
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "len");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "s");
                        }
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
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
                                                V(SyntaxKind.IdentifierToken, text: "s");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"[\\x80-\\xBF]\"", value: ImmutableArray.Create<byte>(91, 128, 45, 191, 93));
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
                            V(SyntaxKind.IdentifierToken, text: "justone");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.ConcatenationExpression);
                        {
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"^\"", value: ImmutableArray.Create("^"U8.ToArray()));
                            }
                            V(SyntaxKind.DotDotToken);
                            V(SyntaxKind.ConcatenationExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "charpattern");
                                    }
                                }
                                V(SyntaxKind.DotDotToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"$\"", value: ImmutableArray.Create("$"U8.ToArray()));
                                }
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
                        V(SyntaxKind.IdentifierToken, text: "checksyntax");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "s");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "t");
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
                                    V(SyntaxKind.IdentifierToken, text: "ts");
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
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"return '\"", value: ImmutableArray.Create("return '"U8.ToArray()));
                                        }
                                    }
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
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "t");
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
                                            V(SyntaxKind.IdentifierToken, text: "ts");
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
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
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
                                                V(SyntaxKind.IdentifierToken, text: "format");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\\\u{%x}\"", value: ImmutableArray.Create("\\u{%x}"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
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
                                    V(SyntaxKind.IdentifierToken, text: "ts");
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
                                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                    }
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"'\"", value: ImmutableArray.Create("'"U8.ToArray()));
                            }
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "ts");
                            }
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
                                            V(SyntaxKind.IdentifierToken, text: "ts");
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
                                                                    V(SyntaxKind.IdentifierToken, text: "load");
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "ts");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "s");
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
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "offset");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"alo\"", value: ImmutableArray.Create("alo"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
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
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "offset");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"alo\"", value: ImmutableArray.Create("alo"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.UnaryMinusExpression);
                                                {
                                                    V(SyntaxKind.MinusToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "check");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "s");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "t");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "nonstrict");
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
                                    V(SyntaxKind.IdentifierToken, text: "l");
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
                                            V(SyntaxKind.IdentifierToken, text: "utf8");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "len");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "s");
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
                                            V(SyntaxKind.UnaryMinusExpression);
                                            {
                                                V(SyntaxKind.MinusToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "nonstrict");
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
                                                V(SyntaxKind.LengthExpression);
                                                {
                                                    V(SyntaxKind.HashToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "t");
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "l");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "len");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "s");
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "l");
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
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "char");
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
                                                                        V(SyntaxKind.IdentifierToken, text: "t");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "s");
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
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "s");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
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
                                            V(SyntaxKind.EqualsEqualsToken);
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
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "checksyntax");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                                    V(SyntaxKind.IdentifierToken, text: "t1");
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
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
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
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseBraceToken);
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t1");
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
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "t");
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
                                                            V(SyntaxKind.IdentifierToken, text: "t");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "t1");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
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
                                V(SyntaxKind.IdentifierToken, text: "l");
                            }
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
                                            V(SyntaxKind.IdentifierToken, text: "pi");
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
                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "offset");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                                            V(SyntaxKind.IdentifierToken, text: "pi1");
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
                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "offset");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pi");
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
                                                                        V(SyntaxKind.IdentifierToken, text: "sub");
                                                                    }
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "s");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "pi");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.SubtractionExpression);
                                                                        {
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "pi1");
                                                                            }
                                                                            V(SyntaxKind.MinusToken);
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
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "justone");
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "offset");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.UnaryMinusExpression);
                                                                {
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi1");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pi");
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "offset");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.SubtractionExpression);
                                                                {
                                                                    V(SyntaxKind.SubtractionExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                        V(SyntaxKind.MinusToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "l");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pi");
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
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "pi1");
                                                        }
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "pi");
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.LengthExpression);
                                                    {
                                                        V(SyntaxKind.HashToken);
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "char");
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
                                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                                            }
                                                                            V(SyntaxKind.DotToken);
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.ArgumentList);
                                                                        {
                                                                            V(SyntaxKind.OpenParenToken);
                                                                            V(SyntaxKind.Argument);
                                                                            {
                                                                                V(SyntaxKind.IdentifierName);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                                }
                                                                            }
                                                                            V(SyntaxKind.CommaToken);
                                                                            V(SyntaxKind.Argument);
                                                                            {
                                                                                V(SyntaxKind.IdentifierName);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                                }
                                                                            }
                                                                            V(SyntaxKind.CommaToken);
                                                                            V(SyntaxKind.Argument);
                                                                            {
                                                                                V(SyntaxKind.IdentifierName);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                                }
                                                                            }
                                                                            V(SyntaxKind.CommaToken);
                                                                            V(SyntaxKind.Argument);
                                                                            {
                                                                                V(SyntaxKind.IdentifierName);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
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
                                        V(SyntaxKind.IdentifierToken, text: "j");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pi");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.SubtractionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pi1");
                                        }
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
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                                    }
                                                                    V(SyntaxKind.DotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                                                    }
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "s");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "j");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                            }
                                                            V(SyntaxKind.EqualsEqualsToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pi");
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
                                V(SyntaxKind.ForStatement);
                                {
                                    V(SyntaxKind.ForKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "j");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pi");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.SubtractionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pi1");
                                        }
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
                                                        V(SyntaxKind.LogicalNotExpression);
                                                        {
                                                            V(SyntaxKind.NotKeyword);
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                                    }
                                                                    V(SyntaxKind.DotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "len");
                                                                    }
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "s");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "j");
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "len");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "len");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.SubtractionExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "pi1");
                                                                    }
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "len");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.UnaryMinusExpression);
                                                                {
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.AdditionExpression);
                                                    {
                                                        V(SyntaxKind.SubtractionExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "l");
                                                            }
                                                            V(SyntaxKind.MinusToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                        V(SyntaxKind.PlusToken);
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
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "len");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi1");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.UnaryMinusExpression);
                                                                {
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "l");
                                                        }
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "len");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pi");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
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
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                }
                            }
                        }
                        V(SyntaxKind.ForInStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "p");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "c");
                            }
                            V(SyntaxKind.InKeyword);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "codes");
                                    }
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "nonstrict");
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
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "i");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                            V(SyntaxKind.IdentifierToken, text: "c");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
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
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                                }
                                                                V(SyntaxKind.DotToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "offset");
                                                                }
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
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
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "p");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "p");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "nonstrict");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "c");
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
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
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
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                        }
                        V(SyntaxKind.ForInStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "c");
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
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "charpattern");
                                            }
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
                                        V(SyntaxKind.IdentifierToken, text: "i");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "i");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                        V(SyntaxKind.IdentifierToken, text: "c");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "char");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
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
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
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
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "l");
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
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "offset");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "offset");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.SubtractionExpression);
                                                                {
                                                                    V(SyntaxKind.SubtractionExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                        V(SyntaxKind.MinusToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "l");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.AdditionExpression);
                                                                {
                                                                    V(SyntaxKind.LengthExpression);
                                                                    {
                                                                        V(SyntaxKind.HashToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                                V(SyntaxKind.IdentifierToken, text: "check");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "p");
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
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "len");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
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
                                                            V(SyntaxKind.IdentifierToken, text: "a");
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
                                                            V(SyntaxKind.IdentifierToken, text: "p");
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
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"abc\\xE3def\"", value: ImmutableArray.Create<byte>(97, 98, 99, 227, 100, 101, 102));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\xF4\\x9F\\xBF\"", value: ImmutableArray.Create<byte>(244, 159, 191));
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\xF4\\x9F\\xBF\\xBF\"", value: ImmutableArray.Create<byte>(244, 159, 191, 191));
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"汉字\\x80\"", value: ImmutableArray.Create<byte>(230, 177, 137, 229, 173, 151, 128));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.ParenthesizedExpression);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"汉字\"", value: ImmutableArray.Create("汉字"U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\x80hello\"", value: ImmutableArray.Create<byte>(128, 104, 101, 108, 108, 111));
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"hel\\x80lo\"", value: ImmutableArray.Create<byte>(104, 101, 108, 128, 108, 111));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"汉字\\xBF\"", value: ImmutableArray.Create<byte>(230, 177, 137, 229, 173, 151, 191));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.ParenthesizedExpression);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"汉字\"", value: ImmutableArray.Create("汉字"U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\xBFhello\"", value: ImmutableArray.Create<byte>(191, 104, 101, 108, 108, 111));
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"hel\\xBFlo\"", value: ImmutableArray.Create<byte>(104, 101, 108, 191, 108, 111));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
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
                    }
                    V(SyntaxKind.EndKeyword);
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
                                V(SyntaxKind.IdentifierToken, text: "errorcodes");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.InvocationStatement);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"invalid UTF%-8 code\"", value: ImmutableArray.Create("invalid UTF%-8 code"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
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
                                                        V(SyntaxKind.ForInStatement);
                                                        {
                                                            V(SyntaxKind.ForKeyword);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "c");
                                                            }
                                                            V(SyntaxKind.InKeyword);
                                                            V(SyntaxKind.InvocationExpression);
                                                            {
                                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                                    }
                                                                    V(SyntaxKind.DotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "codes");
                                                                    }
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "assert");
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
                                                            }
                                                            V(SyntaxKind.EndKeyword);
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
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "errorcodes");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"ab\\xff\"", value: ImmutableArray.Create<byte>(97, 98, 255));
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
                                    V(SyntaxKind.IdentifierToken, text: "errorcodes");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\u{110000}\"", value: ImmutableArray.Create<byte>(244, 144, 128, 128));
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
                                    V(SyntaxKind.IdentifierToken, text: "errorcodes");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"in\\x80valid\"", value: ImmutableArray.Create<byte>(105, 110, 128, 118, 97, 108, 105, 100));
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
                                    V(SyntaxKind.IdentifierToken, text: "errorcodes");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\xbfinvalid\"", value: ImmutableArray.Create<byte>(191, 105, 110, 118, 97, 108, 105, 100));
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
                                    V(SyntaxKind.IdentifierToken, text: "errorcodes");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"αλφ\\xBFα\"", value: ImmutableArray.Create<byte>(206, 177, 206, 187, 207, 134, 191, 206, 177));
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
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "utf8");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "codes");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
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
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.UnaryMinusExpression);
                                                        {
                                                            V(SyntaxKind.MinusToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                            }
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
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
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
                                                                V(SyntaxKind.IdentifierToken, text: "mininteger");
                                                            }
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
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"position out of bounds\"", value: ImmutableArray.Create("position out of bounds"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"position out of bounds\"", value: ImmutableArray.Create("position out of bounds"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
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
                                V(SyntaxKind.UnaryMinusExpression);
                                {
                                    V(SyntaxKind.MinusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"position out of bounds\"", value: ImmutableArray.Create("position out of bounds"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"position out of bounds\"", value: ImmutableArray.Create("position out of bounds"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.UnaryMinusExpression);
                                {
                                    V(SyntaxKind.MinusToken);
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
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"continuation byte\"", value: ImmutableArray.Create("continuation byte"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\uD85A\uDDFA\"", value: ImmutableArray.Create("\uD85A\uDDFA"U8.ToArray()));
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"continuation byte\"", value: ImmutableArray.Create("continuation byte"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\uD85A\uDDFA\"", value: ImmutableArray.Create("\uD85A\uDDFA"U8.ToArray()));
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"continuation byte\"", value: ImmutableArray.Create("continuation byte"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "offset");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\x80\"", value: ImmutableArray.Create<byte>(128));
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"out of bounds\"", value: ImmutableArray.Create("out of bounds"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "len");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"out of bounds\"", value: ImmutableArray.Create("out of bounds"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "len");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                            V(SyntaxKind.IdentifierToken, text: "s");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.StringLiteralExpression);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "\"hello World\"", value: ImmutableArray.Create("hello World"U8.ToArray()));
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
                            V(SyntaxKind.ItemField);
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
                                            V(SyntaxKind.IdentifierToken, text: "byte");
                                        }
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "s");
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
                                            V(SyntaxKind.UnaryMinusExpression);
                                            {
                                                V(SyntaxKind.MinusToken);
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
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.SimpleMemberAccessExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "utf8");
                            }
                            V(SyntaxKind.DotToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "len");
                            }
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                }
                                                V(SyntaxKind.CloseBracketToken);
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
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
                                                        V(SyntaxKind.IdentifierToken, text: "byte");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "s");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
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
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "check");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                            V(SyntaxKind.IdentifierToken, text: "check");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"汉字/漢字\"", value: ImmutableArray.Create("汉字/漢字"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "27721", value: 27721L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "23383", value: 23383L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "47", value: 47L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "28450", value: 28450L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "23383", value: 23383L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.CloseBraceToken);
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
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"áéí\\128\"", value: ImmutableArray.Create<byte>(195, 161, 195, 169, 195, 173, 128));
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
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "utf8");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
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
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.LengthExpression);
                                                        {
                                                            V(SyntaxKind.HashToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "s");
                                                            }
                                                        }
                                                        V(SyntaxKind.MinusToken);
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
                                    V(SyntaxKind.CloseBraceToken);
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
                                                        V(SyntaxKind.LengthExpression);
                                                        {
                                                            V(SyntaxKind.HashToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "t");
                                                            }
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "225", value: 225L);
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
                                                            V(SyntaxKind.IdentifierToken, text: "t");
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "233", value: 233L);
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
                                                        V(SyntaxKind.IdentifierToken, text: "t");
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
                                                    V(SyntaxKind.NumericLiteralToken, text: "237", value: 237L);
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"invalid UTF%-8 code\"", value: ImmutableArray.Create("invalid UTF%-8 code"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                                        V(SyntaxKind.LengthExpression);
                                        {
                                            V(SyntaxKind.HashToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "s");
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"out of bounds\"", value: ImmutableArray.Create("out of bounds"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "t");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                V(SyntaxKind.CloseBraceToken);
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"out of bounds\"", value: ImmutableArray.Create("out of bounds"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.UnaryMinusExpression);
                                        {
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.AdditionExpression);
                                                {
                                                    V(SyntaxKind.LengthExpression);
                                                    {
                                                        V(SyntaxKind.HashToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "s");
                                                        }
                                                    }
                                                    V(SyntaxKind.PlusToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"out of bounds\"", value: ImmutableArray.Create("out of bounds"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\u{D7FF}\"", value: ImmutableArray.Create("퟿"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.SubtractionExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0xD800", value: 55296L);
                                                }
                                                V(SyntaxKind.MinusToken);
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
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\u{E000}\"", value: ImmutableArray.Create(""U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.AdditionExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0xDFFF", value: 57343L);
                                                }
                                                V(SyntaxKind.PlusToken);
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
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\u{D800}\"", value: ImmutableArray.Create<byte>(237, 160, 128));
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0xD800", value: 55296L);
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
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\u{DFFF}\"", value: ImmutableArray.Create<byte>(237, 191, 191));
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0xDFFF", value: 57343L);
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
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "codepoint");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\u{7FFFFFFF}\"", value: ImmutableArray.Create<byte>(253, 191, 191, 191, 191, 191));
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0x7FFFFFFF", value: 2147483647L);
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
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "char");
                                            }
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
                                        V(SyntaxKind.StringLiteralToken, text: "\"\"", value: ImmutableArray.Create(""U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "char");
                                            }
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
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "97", value: 97L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "98", value: 98L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "99", value: 99L);
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
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"\\0abc\\1\"", value: ImmutableArray.Create("\0abc\u0001"U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
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
                                                            V(SyntaxKind.IdentifierToken, text: "utf8");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "char");
                                                        }
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0x10FFFF", value: 1114111L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "0x10FFFF", value: 1114111L);
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
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
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
                                                            V(SyntaxKind.IdentifierToken, text: "utf8");
                                                        }
                                                        V(SyntaxKind.DotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "char");
                                                        }
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0x7FFFFFFF", value: 2147483647L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
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
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.SubtractionExpression);
                                    {
                                        V(SyntaxKind.ParenthesizedExpression);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.BitwiseLeftShiftExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                                V(SyntaxKind.LessThanLessThanToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "31", value: 31L);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                        V(SyntaxKind.MinusToken);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"value out of range\"", value: ImmutableArray.Create("value out of range"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "char");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.AdditionExpression);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "0x7FFFFFFF", value: 2147483647L);
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
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"value out of range\"", value: ImmutableArray.Create("value out of range"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.SimpleMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                    }
                                    V(SyntaxKind.DotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "char");
                                    }
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.UnaryMinusExpression);
                                {
                                    V(SyntaxKind.MinusToken);
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "invalid");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "s");
                        }
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"invalid UTF%-8 code\"", value: ImmutableArray.Create("invalid UTF%-8 code"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "codepoint");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "len");
                                                    }
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "s");
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
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xF4\\x9F\\xBF\\xBF\"", value: ImmutableArray.Create<byte>(244, 159, 191, 191));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\u{D800}\"", value: ImmutableArray.Create<byte>(237, 160, 128));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\u{DFFF}\"", value: ImmutableArray.Create<byte>(237, 191, 191));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xC0\\x80\"", value: ImmutableArray.Create<byte>(192, 128));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xC1\\xBF\"", value: ImmutableArray.Create<byte>(193, 191));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xE0\\x9F\\xBF\"", value: ImmutableArray.Create<byte>(224, 159, 191));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xF0\\x8F\\xBF\\xBF\"", value: ImmutableArray.Create<byte>(240, 143, 191, 191));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\x80\"", value: ImmutableArray.Create<byte>(128));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xBF\"", value: ImmutableArray.Create<byte>(191));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xFE\"", value: ImmutableArray.Create<byte>(254));
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
                            V(SyntaxKind.IdentifierToken, text: "invalid");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xFF\"", value: ImmutableArray.Create<byte>(255));
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
                            V(SyntaxKind.IdentifierToken, text: "check");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
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
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.CloseBraceToken);
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
                        V(SyntaxKind.IdentifierToken, text: "s");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.StringLiteralExpression);
                    {
                        V(SyntaxKind.StringLiteralToken, text: "\"\\0 \\x7F\\z\n     \\xC2\\x80 \\xDF\\xBF\\z\n     \\xE0\\xA0\\x80 \\xEF\\xBF\\xBF\\z\n     \\xF0\\x90\\x80\\x80  \\xF4\\x8F\\xBF\\xBF\"", value: ImmutableArray.Create("\0 \u0080 ߿ࠀ \xffff\uD800\uDC00  \uDBFF\uDFFF"U8.ToArray()));
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "s");
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
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\" \"", value: ImmutableArray.Create(" "U8.ToArray()));
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
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "check");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x7F", value: 127L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x80", value: 128L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x7FF", value: 2047L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x800", value: 2048L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0xFFFF", value: 65535L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x10000", value: 65536L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x10FFFF", value: 1114111L);
                                        }
                                    }
                                    V(SyntaxKind.CloseBraceToken);
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
                                    V(SyntaxKind.IdentifierToken, text: "s");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\u{4000000}\\u{7FFFFFFF}\"", value: ImmutableArray.Create<byte>(252, 132, 128, 128, 128, 128, 253, 191, 191, 191, 191, 191));
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.TableConstructorExpression);
                                        {
                                            V(SyntaxKind.OpenBraceToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0x4000000", value: 67108864L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0x7FFFFFFF", value: 2147483647L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBraceToken);
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
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "s");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"\\u{200000}\\u{3FFFFFF}\"", value: ImmutableArray.Create<byte>(248, 136, 128, 128, 128, 251, 191, 191, 191, 191));
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.TableConstructorExpression);
                                        {
                                            V(SyntaxKind.OpenBraceToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0x200000", value: 2097152L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0x3FFFFFF", value: 67108863L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBraceToken);
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
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "s");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "\"\\u{10000}\\u{1fffff}\"", value: ImmutableArray.Create<byte>(240, 144, 128, 128, 247, 191, 191, 191));
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "8", value: 8L);
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
                                    V(SyntaxKind.IdentifierToken, text: "check");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.TableConstructorExpression);
                                        {
                                            V(SyntaxKind.OpenBraceToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0x10000", value: 65536L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.ItemField);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0x1FFFFF", value: 2097151L);
                                                }
                                            }
                                            V(SyntaxKind.CloseBraceToken);
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
                            V(SyntaxKind.IdentifierToken, text: "x");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.StringLiteralExpression);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "\"日本語a-4\\0éó\"", value: ImmutableArray.Create("日本語a-4\0éó"U8.ToArray()));
                        }
                    }
                }
                V(SyntaxKind.InvocationStatement);
                {
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "check");
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
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "26085", value: 26085L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "26412", value: 26412L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "35486", value: 35486L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "97", value: 97L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "45", value: 45L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "52", value: 52L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "233", value: 233L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "243", value: 243L);
                                        }
                                    }
                                    V(SyntaxKind.CloseBraceToken);
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
                            V(SyntaxKind.IdentifierToken, text: "check");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\uD84F\uDCB7\uD841\uDF0E\uD843\uDC53\uD844\uDC7B\uD843\uDD7Cab\uD843\uDEA2\"", value: ImmutableArray.Create("\uD84F\uDCB7\uD841\uDF0E\uD843\uDC53\uD844\uDC7B\uD843\uDD7Cab\uD843\uDEA2"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x23CB7", value: 146615L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x2070E", value: 132878L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x20C53", value: 134227L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x2107B", value: 135291L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x20D7C", value: 134524L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x61", value: 97L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x62", value: 98L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x20EA2", value: 134818L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.CloseBraceToken);
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
                            V(SyntaxKind.IdentifierToken, text: "check");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\uD863\uDCCA\uD867\uDD98\uD85A\uDDFA\uD863\uDCD2\uD854\uDD2B\uD851\uDCD3\\xF4\\x8F\\xBF\\xBF\"", value: ImmutableArray.Create("\uD863\uDCCA\uD867\uDD98\uD85A\uDDFA\uD863\uDCD2\uD854\uDD2B\uD851\uDCD3\uDBFF\uDFFF"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x28CCA", value: 167114L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x29D98", value: 171416L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x269FA", value: 158202L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x28CD2", value: 167122L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x2512B", value: 151851L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x244D3", value: 148691L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.ItemField);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x10ffff", value: 1114111L);
                                        }
                                    }
                                    V(SyntaxKind.CloseBraceToken);
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
                            V(SyntaxKind.IdentifierToken, text: "i");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                        }
                    }
                }
                V(SyntaxKind.ForInStatement);
                {
                    V(SyntaxKind.ForKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "p");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "c");
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
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"()(\"", value: ImmutableArray.Create("()("U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.ConcatenationExpression);
                                    {
                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                            }
                                            V(SyntaxKind.DotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "charpattern");
                                            }
                                        }
                                        V(SyntaxKind.DotDotToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\")\"", value: ImmutableArray.Create(")"U8.ToArray()));
                                        }
                                    }
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
                                V(SyntaxKind.IdentifierToken, text: "i");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.AdditionExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                                V(SyntaxKind.PlusToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "offset");
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "len");
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.AdditionExpression);
                                            {
                                                V(SyntaxKind.SubtractionExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "len");
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
                                                    V(SyntaxKind.MinusToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                    }
                                                }
                                                V(SyntaxKind.PlusToken);
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
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.SimpleMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "utf8");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "len");
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
                                            V(SyntaxKind.EqualsEqualsToken);
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
                        V(SyntaxKind.ForStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "j");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.SubtractionExpression);
                            {
                                V(SyntaxKind.LengthExpression);
                                {
                                    V(SyntaxKind.HashToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "c");
                                    }
                                }
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
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "utf8");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "offset");
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
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.SubtractionExpression);
                                                                {
                                                                    V(SyntaxKind.AdditionExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                                        }
                                                                        V(SyntaxKind.PlusToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "j");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.MinusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
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
                            }
                            V(SyntaxKind.EndKeyword);
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
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "'ok'", value: ImmutableArray.Create("ok"U8.ToArray()));
                        }
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
