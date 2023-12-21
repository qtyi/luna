// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_vararg(string source, SourceCodeKind kind)
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
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "'testing vararg'", value: ImmutableArray.Create("testing vararg"U8.ToArray()));
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
                        V(SyntaxKind.IdentifierToken, text: "f");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
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
                                    V(SyntaxKind.IdentifierToken, text: "x");
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
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "select");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "'#'", value: ImmutableArray.Create("#"U8.ToArray()));
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
                                    V(SyntaxKind.CommaToken);
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
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "n");
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
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "n");
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
                        V(SyntaxKind.IdentifierToken, text: "c12");
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
                                                V(SyntaxKind.IdentifierToken, text: "arg");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "_G");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "arg");
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
                                    V(SyntaxKind.IdentifierToken, text: "x");
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
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.SimpleMemberAccessExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                                V(SyntaxKind.DotToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "n");
                                }
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.LengthExpression);
                            {
                                V(SyntaxKind.HashToken);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
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
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.ParenthesizedExpression);
                                {
                                    V(SyntaxKind.OpenParenToken);
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
                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                    }
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
                                                        V(SyntaxKind.IdentifierToken, text: "x");
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
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
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
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "res");
                            }
                            V(SyntaxKind.ThenKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "res");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "55", value: 55L);
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
                                V(SyntaxKind.IdentifierToken, text: "res");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                        V(SyntaxKind.IdentifierToken, text: "vararg");
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
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.TableConstructorExpression);
                            {
                                V(SyntaxKind.OpenBraceToken);
                                V(SyntaxKind.NameValueField);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "n");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "select");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "'#'", value: ImmutableArray.Create("#"U8.ToArray()));
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
                                V(SyntaxKind.CommaToken);
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
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "call");
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
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "args");
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.ReturnStatement);
                                {
                                    V(SyntaxKind.ReturnKeyword);
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
                                                                V(SyntaxKind.IdentifierToken, text: "args");
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
                                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "args");
                                                                }
                                                                V(SyntaxKind.DotToken);
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
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                            }
                            V(SyntaxKind.EndKeyword);
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
                                                V(SyntaxKind.TableConstructorExpression);
                                                {
                                                    V(SyntaxKind.OpenBraceToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBraceToken);
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
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                                V(SyntaxKind.TableConstructorExpression);
                                                {
                                                    V(SyntaxKind.OpenBraceToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"alo\"", value: ImmutableArray.Create("alo"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NilLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NilKeyword);
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "f");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NilLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NilKeyword);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBraceToken);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
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
                                                V(SyntaxKind.NilLiteralExpression);
                                                {
                                                    V(SyntaxKind.NilKeyword);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "45", value: 45L);
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
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
                                                V(SyntaxKind.NilLiteralExpression);
                                                {
                                                    V(SyntaxKind.NilKeyword);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
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
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "vararg");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
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
                                V(SyntaxKind.EqualExpression);
                                {
                                    V(SyntaxKind.SimpleMemberAccessExpression);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "vararg");
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
                                        V(SyntaxKind.DotToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
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
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "c12");
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
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "55", value: 55L);
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
                                            V(SyntaxKind.IdentifierToken, text: "call");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "c12");
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                    V(SyntaxKind.CloseBraceToken);
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
                                            V(SyntaxKind.NumericLiteralToken, text: "55", value: 55L);
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
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "call");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "c12");
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
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                    V(SyntaxKind.NameValueField);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                                            V(SyntaxKind.NumericLiteralToken, text: "55", value: 55L);
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
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "call");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "c12");
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
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                    V(SyntaxKind.NameValueField);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "a");
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
                                            V(SyntaxKind.IdentifierToken, text: "c12");
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
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
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
                                V(SyntaxKind.IdentifierToken, text: "vararg");
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
                                            V(SyntaxKind.IdentifierToken, text: "call");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "next");
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "_G");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NilLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NilKeyword);
                                                        }
                                                    }
                                                    V(SyntaxKind.SemicolonToken);
                                                    V(SyntaxKind.NameValueField);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "n");
                                                        }
                                                        V(SyntaxKind.EqualsToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBraceToken);
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
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "next");
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "b");
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "c");
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
                                                V(SyntaxKind.IdentifierToken, text: "n");
                                            }
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "vararg");
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
                                        V(SyntaxKind.IdentifierToken, text: "call");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "call");
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
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "c12");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.ItemField);
                                                {
                                                    V(SyntaxKind.TableConstructorExpression);
                                                    {
                                                        V(SyntaxKind.OpenBraceToken);
                                                        V(SyntaxKind.ItemField);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                        V(SyntaxKind.CloseBraceToken);
                                                    }
                                                }
                                                V(SyntaxKind.CloseBraceToken);
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
                                                    V(SyntaxKind.IdentifierToken, text: "n");
                                                }
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
                                                V(SyntaxKind.NumericLiteralToken, text: "55", value: 55L);
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
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "call");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "print");
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
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "'+'", value: ImmutableArray.Create("+"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "a");
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
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.ItemField);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "10", value: 10L);
                                }
                            }
                            V(SyntaxKind.CloseBraceToken);
                        }
                    }
                }
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.ImplicitSelfParameterName);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "t");
                        }
                        V(SyntaxKind.ColonToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
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
                                    V(SyntaxKind.IdentifierToken, text: "arg");
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
                        V(SyntaxKind.EmptyStatement);
                        {
                            V(SyntaxKind.SemicolonToken);
                        }
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.AdditionExpression);
                            {
                                V(SyntaxKind.IndexMemberAccessExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "self");
                                    }
                                    V(SyntaxKind.OpenBracketToken);
                                    V(SyntaxKind.VariousArgumentsExpression);
                                    {
                                        V(SyntaxKind.DotDotDotToken);
                                    }
                                    V(SyntaxKind.CloseBracketToken);
                                }
                                V(SyntaxKind.PlusToken);
                                V(SyntaxKind.LengthExpression);
                                {
                                    V(SyntaxKind.HashToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "arg");
                                    }
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
                                            V(SyntaxKind.ImplicitSelfParameterExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                                V(SyntaxKind.ColonToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "f");
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
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.ImplicitSelfParameterExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "t");
                                                }
                                                V(SyntaxKind.ColonToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "f");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
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
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "11", value: 11L);
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
                                    V(SyntaxKind.StringLiteralToken, text: "'+'", value: ImmutableArray.Create("+"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "lim");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
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
                    V(SyntaxKind.CommaToken);
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
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.TableConstructorExpression);
                        {
                            V(SyntaxKind.OpenBraceToken);
                            V(SyntaxKind.CloseBraceToken);
                        }
                    }
                }
                V(SyntaxKind.WhileStatement);
                {
                    V(SyntaxKind.WhileKeyword);
                    V(SyntaxKind.LessThanOrEqualExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "i");
                        }
                        V(SyntaxKind.LessThanEqualsToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "lim");
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
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                                V(SyntaxKind.CloseBracketToken);
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
                                    V(SyntaxKind.NumericLiteralToken, text: "0.3", value: 0.3D);
                                }
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
                    }
                    V(SyntaxKind.EndKeyword);
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
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "b");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "c");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "d");
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
                                    V(SyntaxKind.IdentifierToken, text: "more");
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "1.3", value: 1.3D);
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IndexMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "more");
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "5.3", value: 5.3D);
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
                                                            V(SyntaxKind.IdentifierToken, text: "more");
                                                        }
                                                        V(SyntaxKind.OpenBracketToken);
                                                        V(SyntaxKind.SubtractionExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "lim");
                                                            }
                                                            V(SyntaxKind.MinusToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseBracketToken);
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.AdditionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lim");
                                                        }
                                                        V(SyntaxKind.PlusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "0.3", value: 0.3D);
                                                        }
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.LogicalNotExpression);
                                            {
                                                V(SyntaxKind.NotKeyword);
                                                V(SyntaxKind.IndexMemberAccessExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "more");
                                                    }
                                                    V(SyntaxKind.OpenBracketToken);
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lim");
                                                        }
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBracketToken);
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
                        V(SyntaxKind.IdentifierToken, text: "g");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "b");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "c");
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "1.3", value: 1.3D);
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "2.3", value: 2.3D);
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
                                                    V(SyntaxKind.NumericLiteralToken, text: "3.3", value: 3.3D);
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
                            V(SyntaxKind.IdentifierToken, text: "call");
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
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
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
                            V(SyntaxKind.IdentifierToken, text: "call");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "g");
                                }
                            }
                            V(SyntaxKind.CommaToken);
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
                V(SyntaxKind.WhileStatement);
                {
                    V(SyntaxKind.WhileKeyword);
                    V(SyntaxKind.LessThanOrEqualExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "i");
                        }
                        V(SyntaxKind.LessThanEqualsToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "lim");
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
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "call");
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
                                                        V(SyntaxKind.IdentifierToken, text: "math");
                                                    }
                                                    V(SyntaxKind.DotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "max");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
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
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "lim");
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"+\"", value: ImmutableArray.Create("+"U8.ToArray()));
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
                        V(SyntaxKind.IdentifierToken, text: "oneless");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
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
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.VariousArgumentsExpression);
                            {
                                V(SyntaxKind.DotDotDotToken);
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
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
                            V(SyntaxKind.IdentifierToken, text: "n");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "a");
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
                                    V(SyntaxKind.IdentifierToken, text: "b");
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
                                                V(SyntaxKind.IdentifierToken, text: "arg");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.SimpleMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "_G");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "arg");
                                                }
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.EqualExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "n");
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
                                        V(SyntaxKind.VariousArgumentsExpression);
                                        {
                                            V(SyntaxKind.DotDotDotToken);
                                        }
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
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "oneless");
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
                                                        V(SyntaxKind.IdentifierToken, text: "oneless");
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
                                                                    V(SyntaxKind.IdentifierToken, text: "oneless");
                                                                }
                                                                V(SyntaxKind.ArgumentList);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
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
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
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
                                    V(SyntaxKind.AssignmentStatement);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "n");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "b");
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.SubtractionExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "n");
                                            }
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.VariousArgumentsExpression);
                                        {
                                            V(SyntaxKind.DotDotDotToken);
                                        }
                                        V(SyntaxKind.CommaToken);
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "b");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.VariousArgumentsExpression);
                                                        {
                                                            V(SyntaxKind.DotDotDotToken);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
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
                                                V(SyntaxKind.IdentifierToken, text: "f");
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
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
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
                            }
                            V(SyntaxKind.EndKeyword);
                        }
                    }
                    V(SyntaxKind.EndKeyword);
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
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "e");
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
                                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
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
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                                                    V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "e");
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
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "d");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "e");
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
                                                    V(SyntaxKind.NilLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NilKeyword);
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
                                                    V(SyntaxKind.NilLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NilKeyword);
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
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "e");
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
                                V(SyntaxKind.IdentifierToken, text: "load");
                            }
                            V(SyntaxKind.ArgumentString);
                            {
                                V(SyntaxKind.MultiLineRawStringLiteralToken, text: "[[ return {...} ]]", value: ImmutableArray.Create(" return {...} "U8.ToArray()));
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
                                V(SyntaxKind.IdentifierToken, text: "f");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
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
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.IndexMemberAccessExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
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
                                                    V(SyntaxKind.IdentifierToken, text: "x");
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
                                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.OpenBracketToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                            V(SyntaxKind.IdentifierToken, text: "load");
                        }
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.MultiLineRawStringLiteralToken, text: "[[\n  local x = {...}\n  for i=1,select('#', ...) do assert(x[i] == select(i, ...)) end\n  assert(x[select('#', ...)+1] == undef)\n  return true\n]]", value: ImmutableArray.Create("  local x = {...}\n  for i=1,select('#', ...) do assert(x[i] == select(i, ...)) end\n  assert(x[select('#', ...)+1] == undef)\n  return true\n"U8.ToArray()));
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"a\"", value: ImmutableArray.Create("a"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"b\"", value: ImmutableArray.Create("b"U8.ToArray()));
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
                                            V(SyntaxKind.TableConstructorExpression);
                                            {
                                                V(SyntaxKind.OpenBraceToken);
                                                V(SyntaxKind.CloseBraceToken);
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "assert");
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
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "select");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "table");
                                                }
                                                V(SyntaxKind.DotToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                            }
                                            V(SyntaxKind.ArgumentTable);
                                            {
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "30", value: 30L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.ItemField);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "40", value: 40L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseBraceToken);
                                                }
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                }
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
                                                V(SyntaxKind.NumericLiteralToken, text: "30", value: 30L);
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
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
                                            V(SyntaxKind.CloseBracketToken);
                                        }
                                        V(SyntaxKind.EqualsEqualsToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "40", value: 40L);
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
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "select");
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
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "next");
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
                        V(SyntaxKind.IdentifierToken, text: "a");
                    }
                    V(SyntaxKind.EqualsToken);
                    V(SyntaxKind.TableConstructorExpression);
                    {
                        V(SyntaxKind.OpenBraceToken);
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "select");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
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
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "7", value: 7L);
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
                                            V(SyntaxKind.NumericLiteralToken, text: "7", value: 7L);
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
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "undef");
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
                        V(SyntaxKind.ItemField);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "select");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.UnaryMinusExpression);
                                        {
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
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
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "7", value: 7L);
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
                                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
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
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "7", value: 7L);
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
                                                V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                            V(SyntaxKind.IdentifierToken, text: "pcall");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "select");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "10000", value: 10000L);
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
                            V(SyntaxKind.IdentifierToken, text: "pcall");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "select");
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
                                        V(SyntaxKind.NumericLiteralToken, text: "10000", value: 10000L);
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
                            V(SyntaxKind.IdentifierToken, text: "p1");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p2");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p3");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p4");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p5");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p6");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p7");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p8");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p9");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p10");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p11");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p12");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p13");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p14");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p15");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p16");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p17");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p18");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p19");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p20");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p21");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p22");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p23");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p24");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p25");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p26");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p27");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p28");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p29");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p30");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p31");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p32");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p33");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p34");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p35");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p36");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p37");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p38");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p39");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p40");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p41");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p42");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p43");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p44");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p45");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p46");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p48");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p49");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "p50");
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
                                    V(SyntaxKind.IdentifierToken, text: "a1");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a2");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a3");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a4");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a5");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a6");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a7");
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
                                    V(SyntaxKind.IdentifierToken, text: "a8");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a9");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a10");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a11");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a12");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a13");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a14");
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
                            V(SyntaxKind.IdentifierToken, text: "f");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
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
                                V(SyntaxKind.IdentifierToken, text: "f");
                            }
                            V(SyntaxKind.ParameterList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "a");
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "b");
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.Parameter);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "c");
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
                                        V(SyntaxKind.IdentifierToken, text: "c");
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
                        V(SyntaxKind.LocalFunctionDefinitionStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.FunctionKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "g");
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
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "g");
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
                                                V(SyntaxKind.NilLiteralExpression);
                                                {
                                                    V(SyntaxKind.NilKeyword);
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
                                    V(SyntaxKind.StringLiteralToken, text: "'OK'", value: ImmutableArray.Create("OK"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
