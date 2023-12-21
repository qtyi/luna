// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_tracegc(string source, SourceCodeKind kind)
    {
        var V = ParseSource(source, options: TestOptions.RegularDefault.WithKind(kind));

        V(SyntaxKind.Chunk);
        {
            V(SyntaxKind.Block);
            {
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "M");
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
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "setmetatable");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "stderr");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "setmetatable");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.SimpleMemberAccessExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "io");
                            }
                            V(SyntaxKind.DotToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "stderr");
                            }
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                        }
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "_ENV");
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
                            V(SyntaxKind.IdentifierToken, text: "active");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.FalseLiteralExpression);
                        {
                            V(SyntaxKind.FalseKeyword);
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
                            V(SyntaxKind.IdentifierToken, text: "mt");
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
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.QualifiedName);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "mt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__gc");
                        }
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "o");
                        }
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.InvocationStatement);
                        {
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.ImplicitSelfParameterExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "stderr");
                                    }
                                    V(SyntaxKind.ColonToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "write");
                                    }
                                }
                                V(SyntaxKind.ArgumentString);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "'.'", value: ImmutableArray.Create("."U8.ToArray()));
                                }
                            }
                        }
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "active");
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
                                            V(SyntaxKind.IdentifierToken, text: "setmetatable");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "o");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "mt");
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
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.QualifiedName);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "M");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "start");
                        }
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.LogicalNotExpression);
                            {
                                V(SyntaxKind.NotKeyword);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "active");
                                }
                            }
                            V(SyntaxKind.ThenKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "active");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.TrueLiteralExpression);
                                    {
                                        V(SyntaxKind.TrueKeyword);
                                    }
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
                                                    V(SyntaxKind.IdentifierToken, text: "mt");
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
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.QualifiedName);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "M");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "stop");
                        }
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "active");
                            }
                            V(SyntaxKind.ThenKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "active");
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
                                            V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
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
                V(SyntaxKind.ReturnStatement);
                {
                    V(SyntaxKind.ReturnKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "M");
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
