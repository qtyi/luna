// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_big(string source, SourceCodeKind kind)
    {
        var V = ParseSource(source, options: TestOptions.RegularDefault.WithKind(kind));

        V(SyntaxKind.Chunk);
        {
            V(SyntaxKind.Block);
            {
                V(SyntaxKind.IfStatement);
                {
                    V(SyntaxKind.IfKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "_soft");
                    }
                    V(SyntaxKind.ThenKeyword);
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.StringLiteralExpression);
                            {
                                V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
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
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "\"testing large tables\"", value: ImmutableArray.Create("testing large tables"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "debug");
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
                                V(SyntaxKind.StringLiteralToken, text: "\"debug\"", value: ImmutableArray.Create("debug"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "lim");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.ExponentiationExpression);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                }
                                V(SyntaxKind.CaretToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "18", value: 18L);
                                }
                            }
                            V(SyntaxKind.PlusToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1000", value: 1000L);
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
                            V(SyntaxKind.IdentifierToken, text: "prog");
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"local y = {0\"", value: ImmutableArray.Create("local y = {0"U8.ToArray()));
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
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "lim");
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
                                    V(SyntaxKind.IdentifierToken, text: "prog");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.AdditionExpression);
                                {
                                    V(SyntaxKind.LengthExpression);
                                    {
                                        V(SyntaxKind.HashToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "prog");
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
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "i");
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
                            V(SyntaxKind.IdentifierToken, text: "prog");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "prog");
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
                        V(SyntaxKind.StringLiteralToken, text: "\"}\\n\"", value: ImmutableArray.Create("}\n"U8.ToArray()));
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "prog");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "prog");
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
                        V(SyntaxKind.StringLiteralToken, text: "\"X = y\\n\"", value: ImmutableArray.Create("X = y\n"U8.ToArray()));
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "prog");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "prog");
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
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.ImplicitSelfParameterExpression);
                        {
                            V(SyntaxKind.ParenthesizedExpression);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"assert(X[%d] == %d)\"", value: ImmutableArray.Create("assert(X[%d] == %d)"U8.ToArray()));
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.ColonToken);
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
                                V(SyntaxKind.SubtractionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "lim");
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
                                V(SyntaxKind.SubtractionExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "lim");
                                    }
                                    V(SyntaxKind.MinusToken);
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
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IndexMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "prog");
                        }
                        V(SyntaxKind.OpenBracketToken);
                        V(SyntaxKind.AdditionExpression);
                        {
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "prog");
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
                        V(SyntaxKind.StringLiteralToken, text: "\"return 0\"", value: ImmutableArray.Create("return 0"U8.ToArray()));
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "prog");
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
                                    V(SyntaxKind.IdentifierToken, text: "prog");
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
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "env");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.TableConstructorExpression);
                        {
                            V(SyntaxKind.OpenBraceToken);
                            V(SyntaxKind.NameValueField);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "string");
                                }
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "string");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameValueField);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "assert");
                                }
                            }
                            V(SyntaxKind.CloseBraceToken);
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
                                                    V(SyntaxKind.IdentifierToken, text: "prog");
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
                                                V(SyntaxKind.NilLiteralExpression);
                                                {
                                                    V(SyntaxKind.NilKeyword);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "env");
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
                V(SyntaxKind.InvocationStatement);
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
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "env");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "X");
                                                }
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lim");
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.SubtractionExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lim");
                                            }
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IndexMemberAccessExpression);
                                        {
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "env");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "X");
                                                }
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.AdditionExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "lim");
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
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "lim");
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.ForInStatement);
                {
                    V(SyntaxKind.ForKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "k");
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
                                    V(SyntaxKind.IdentifierToken, text: "env");
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
                            V(SyntaxKind.IndexMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "env");
                                }
                                V(SyntaxKind.OpenBracketToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "k");
                                }
                                V(SyntaxKind.CloseBracketToken);
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "undef");
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
                            V(SyntaxKind.IdentifierToken, text: "setmetatable");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "env");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.TableConstructorExpression);
                                {
                                    V(SyntaxKind.OpenBraceToken);
                                    V(SyntaxKind.NameValueField);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "__index");
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
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Parameter);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "n");
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
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
                                                                V(SyntaxKind.IdentifierToken, text: "coroutine");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "yield");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "'g'", value: ImmutableArray.Create("g"U8.ToArray()));
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
                                                V(SyntaxKind.ReturnStatement);
                                                {
                                                    V(SyntaxKind.ReturnKeyword);
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "_G");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "n");
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.EndKeyword);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.NameValueField);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "__newindex");
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
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Parameter);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "n");
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Parameter);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "v");
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
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
                                                                V(SyntaxKind.IdentifierToken, text: "coroutine");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "yield");
                                                            }
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "'s'", value: ImmutableArray.Create("s"U8.ToArray()));
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
                                                V(SyntaxKind.AssignmentStatement);
                                                {
                                                    V(SyntaxKind.IndexMemberAccessExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "_G");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "n");
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "v");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.EndKeyword);
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
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "X");
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
                            V(SyntaxKind.IdentifierToken, text: "co");
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
                                    V(SyntaxKind.IdentifierToken, text: "coroutine");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "wrap");
                                }
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
                                            V(SyntaxKind.IdentifierToken, text: "co");
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
                                        V(SyntaxKind.StringLiteralToken, text: "'s'", value: ImmutableArray.Create("s"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierToken, text: "co");
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
                                        V(SyntaxKind.StringLiteralToken, text: "'g'", value: ImmutableArray.Create("g"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierToken, text: "co");
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
                                        V(SyntaxKind.StringLiteralToken, text: "'g'", value: ImmutableArray.Create("g"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierToken, text: "co");
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
                                                V(SyntaxKind.IdentifierToken, text: "X");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lim");
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.SubtractionExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lim");
                                            }
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                V(SyntaxKind.IdentifierToken, text: "X");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.AdditionExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "lim");
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
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "lim");
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
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "getmetatable");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "env");
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__index");
                        }
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.FunctionDefinitionExpression);
                    {
                        V(SyntaxKind.FunctionKeyword);
                        V(SyntaxKind.ParameterList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                        V(SyntaxKind.Block);
                        V(SyntaxKind.EndKeyword);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "getmetatable");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "env");
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__newindex");
                        }
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.FunctionDefinitionExpression);
                    {
                        V(SyntaxKind.FunctionKeyword);
                        V(SyntaxKind.ParameterList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.CloseParenToken);
                        }
                        V(SyntaxKind.Block);
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
                            V(SyntaxKind.IdentifierToken, text: "e");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "m");
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
                                            V(SyntaxKind.IdentifierToken, text: "e");
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "m");
                                            }
                                            V(SyntaxKind.ColonToken);
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
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"global 'X'\"", value: ImmutableArray.Create("global 'X'"U8.ToArray()));
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
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "getmetatable");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "env");
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__newindex");
                        }
                    }
                    V(SyntaxKind.EqualsToken);
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
                            V(SyntaxKind.InvocationStatement);
                            {
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "error");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"hi\"", value: ImmutableArray.Create("hi"U8.ToArray()));
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
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "e");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "m");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "xpcall");
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
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "debug");
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "traceback");
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
                                    V(SyntaxKind.LogicalNotExpression);
                                    {
                                        V(SyntaxKind.NotKeyword);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "e");
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "m");
                                            }
                                            V(SyntaxKind.ColonToken);
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
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"'newindex'\"", value: ImmutableArray.Create("'newindex'"U8.ToArray()));
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
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "f");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "X");
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
                                V(SyntaxKind.IdentifierToken, text: "coroutine");
                            }
                            V(SyntaxKind.DotToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "yield");
                            }
                        }
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "'b'", value: ImmutableArray.Create("b"U8.ToArray()));
                        }
                    }
                }
                V(SyntaxKind.IfStatement);
                {
                    V(SyntaxKind.IfKeyword);
                    V(SyntaxKind.EqualExpression);
                    {
                        V(SyntaxKind.ExponentiationExpression);
                        {
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                            }
                            V(SyntaxKind.CaretToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "32", value: 32L);
                            }
                        }
                        V(SyntaxKind.EqualsEqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                        }
                    }
                    V(SyntaxKind.ThenKeyword);
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"testing string length overflow\"", value: ImmutableArray.Create("testing string length overflow"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "repstrings");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "192", value: 192L);
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
                                    V(SyntaxKind.IdentifierToken, text: "ssize");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.AdditionExpression);
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
                                                V(SyntaxKind.IdentifierToken, text: "ceil");
                                            }
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.DivisionExpression);
                                                {
                                                    V(SyntaxKind.ExponentiationExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2.0", value: 2D);
                                                        }
                                                        V(SyntaxKind.CaretToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "32", value: 32L);
                                                        }
                                                    }
                                                    V(SyntaxKind.SlashToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "repstrings");
                                                    }
                                                }
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
                                        V(SyntaxKind.GreaterThanExpression);
                                        {
                                            V(SyntaxKind.MultiplicationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "repstrings");
                                                }
                                                V(SyntaxKind.AsteriskToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "ssize");
                                                }
                                            }
                                            V(SyntaxKind.GreaterThanToken);
                                            V(SyntaxKind.ExponentiationExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "2.0", value: 2D);
                                                }
                                                V(SyntaxKind.CaretToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "32", value: 32L);
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
                                    V(SyntaxKind.IdentifierToken, text: "longs");
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "ssize");
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
                                    V(SyntaxKind.IdentifierToken, text: "rep");
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
                                                    V(SyntaxKind.IdentifierToken, text: "load");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"local a = ...; return \"", value: ImmutableArray.Create("local a = ...; return "U8.ToArray()));
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
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"a\"", value: ImmutableArray.Create("a"U8.ToArray()));
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "repstrings");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"..\"", value: ImmutableArray.Create(".."U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "pcall");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "rep");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "longs");
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
                                                            V(SyntaxKind.IdentifierToken, text: "b");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "'OK'", value: ImmutableArray.Create("OK"U8.ToArray()));
                        }
                    }
                }
                V(SyntaxKind.ReturnStatement);
                {
                    V(SyntaxKind.ReturnKeyword);
                    V(SyntaxKind.StringLiteralExpression);
                    {
                        V(SyntaxKind.StringLiteralToken, text: "'a'", value: ImmutableArray.Create("a"U8.ToArray()));
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
