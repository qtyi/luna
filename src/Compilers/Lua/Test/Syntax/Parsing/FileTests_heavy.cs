// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_heavy(string source, SourceCodeKind kind)
    {
        var V = ParseSource(source, options: TestOptions.RegularDefault.WithKind(kind));

        V(SyntaxKind.Chunk);
        {
            V(SyntaxKind.Block);
            {
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "teststring");
                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"creating a string too long\"", value: ImmutableArray.Create("creating a string too long"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierToken, text: "a");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"x\"", value: ImmutableArray.Create("x"U8.ToArray()));
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
                                                            V(SyntaxKind.WhileStatement);
                                                            {
                                                                V(SyntaxKind.WhileKeyword);
                                                                V(SyntaxKind.TrueLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.TrueKeyword);
                                                                }
                                                                V(SyntaxKind.DoKeyword);
                                                                V(SyntaxKind.Block);
                                                                {
                                                                    V(SyntaxKind.AssignmentStatement);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                        }
                                                                        V(SyntaxKind.EqualsToken);
                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                        {
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                            }
                                                                            V(SyntaxKind.DotDotToken);
                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                            {
                                                                                V(SyntaxKind.IdentifierName);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                }
                                                                                V(SyntaxKind.DotDotToken);
                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                    }
                                                                                    V(SyntaxKind.DotDotToken);
                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierName);
                                                                                        {
                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                        }
                                                                                        V(SyntaxKind.DotDotToken);
                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                        {
                                                                                            V(SyntaxKind.IdentifierName);
                                                                                            {
                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                            }
                                                                                            V(SyntaxKind.DotDotToken);
                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                            {
                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                {
                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                }
                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                {
                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                    {
                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                    }
                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                    {
                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                        {
                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                        }
                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                        {
                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                            {
                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                            }
                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                            {
                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                {
                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                }
                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                {
                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                    {
                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                    }
                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                    {
                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                        {
                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                        }
                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                        {
                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                            {
                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                            }
                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                            {
                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                {
                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                }
                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                {
                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                    {
                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                    }
                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                    {
                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                        {
                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                        }
                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                        {
                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                            {
                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                            }
                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                            {
                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                {
                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                }
                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                {
                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                    {
                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                    }
                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                    {
                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                        {
                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                        }
                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                        {
                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                            {
                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                            }
                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                            {
                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                {
                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                }
                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                {
                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                    {
                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                    }
                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                    {
                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                        {
                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                        }
                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                        {
                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                            {
                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                            }
                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                            {
                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                {
                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                }
                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                {
                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                    {
                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                    }
                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                    {
                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                        {
                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                        }
                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                        {
                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                            {
                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                            }
                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                            {
                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                {
                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                }
                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                {
                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                    {
                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                    }
                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                    {
                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                        {
                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                        }
                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                        {
                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                            {
                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                            }
                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                            {
                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                }
                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                V(SyntaxKind.ConcatenationExpression);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.DotDotToken);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    V(SyntaxKind.IdentifierName);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
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
                                                                                V(SyntaxKind.IdentifierToken, text: "print");
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
                                                                                                V(SyntaxKind.StringLiteralExpression);
                                                                                                {
                                                                                                    V(SyntaxKind.StringLiteralToken, text: "\"string with %d bytes\"", value: ImmutableArray.Create("string with %d bytes"U8.ToArray()));
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
                                                                                                        V(SyntaxKind.IdentifierToken, text: "a");
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
                                                        V(SyntaxKind.EndKeyword);
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
                                                    V(SyntaxKind.ParenthesizedExpression);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.OrExpression);
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
                                                                            V(SyntaxKind.IdentifierToken, text: "msg");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CommaToken);
                                                                    V(SyntaxKind.Argument);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"string length overflow\"", value: ImmutableArray.Create("string length overflow"U8.ToArray()));
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                            }
                                                            V(SyntaxKind.OrKeyword);
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
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"not enough memory\"", value: ImmutableArray.Create("not enough memory"U8.ToArray()));
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
                                                        V(SyntaxKind.StringLiteralToken, text: "\"string length overflow with \"", value: ImmutableArray.Create("string length overflow with "U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.MultiplicationExpression);
                                                    {
                                                        V(SyntaxKind.LengthExpression);
                                                        {
                                                            V(SyntaxKind.HashToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "a");
                                                            }
                                                        }
                                                        V(SyntaxKind.AsteriskToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "100", value: 100L);
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "loadrep");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "x");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "what");
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
                                    V(SyntaxKind.IdentifierToken, text: "p");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.BitwiseLeftShiftExpression);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                    }
                                    V(SyntaxKind.LessThanLessThanToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
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
                            }
                        }
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "count");
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
                                V(SyntaxKind.AssignmentStatement);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "count");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "count");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "p");
                                        }
                                    }
                                }
                                V(SyntaxKind.IfStatement);
                                {
                                    V(SyntaxKind.IfKeyword);
                                    V(SyntaxKind.EqualExpression);
                                    {
                                        V(SyntaxKind.ModuloExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "count");
                                            }
                                            V(SyntaxKind.PersentToken);
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.MultiplicationExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "0x80", value: 128L);
                                                    }
                                                    V(SyntaxKind.AsteriskToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "p");
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
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
                                                V(SyntaxKind.ImplicitSelfParameterExpression);
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
                                                            V(SyntaxKind.IdentifierToken, text: "stderr");
                                                        }
                                                    }
                                                    V(SyntaxKind.ColonToken);
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
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"(\"", value: ImmutableArray.Create("("U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.FloorDivisionExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "count");
                                                            }
                                                            V(SyntaxKind.SlashSlashToken);
                                                            V(SyntaxKind.ExponentiationExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                }
                                                                V(SyntaxKind.CaretToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\" M)\"", value: ImmutableArray.Create(" M)"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "s");
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
                                        V(SyntaxKind.IdentifierToken, text: "load");
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
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"=big\"", value: ImmutableArray.Create("=big"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\nmemory: \"", value: ImmutableArray.Create("\nmemory: "U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.MultiplicationExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "'count'", value: ImmutableArray.Create("count"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.AsteriskToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1024", value: 1024L);
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
                                V(SyntaxKind.IdentifierToken, text: "msg");
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
                                        V(SyntaxKind.IdentifierToken, text: "match");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"^[^\\n]+\"", value: ImmutableArray.Create("^[^\n]+"U8.ToArray()));
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
                                                        V(SyntaxKind.StringLiteralToken, text: "\"total: 0x%x %s ('%s')\"", value: ImmutableArray.Create("total: 0x%x %s ('%s')"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "count");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "what");
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
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                        V(SyntaxKind.ReturnStatement);
                        {
                            V(SyntaxKind.ReturnKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "st");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "msg");
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
                        V(SyntaxKind.IdentifierToken, text: "controlstruct");
                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"control structure too long\"", value: ImmutableArray.Create("control structure too long"U8.ToArray()));
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
                                V(SyntaxKind.FloorDivisionExpression);
                                {
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "24", value: 24L);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                    V(SyntaxKind.SlashSlashToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"a = a + 1\\n\"", value: ImmutableArray.Create("a = a + 1\n"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lim");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
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
                            V(SyntaxKind.ConcatenationExpression);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"while true do \"", value: ImmutableArray.Create("while true do "U8.ToArray()));
                                }
                                V(SyntaxKind.DotDotToken);
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "s");
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"end\"", value: ImmutableArray.Create("end"U8.ToArray()));
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
                                                        V(SyntaxKind.IdentifierToken, text: "s");
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"ok with \"", value: ImmutableArray.Create("ok with "U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "lim");
                                                }
                                                V(SyntaxKind.DotDotToken);
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\" lines\"", value: ImmutableArray.Create(" lines"U8.ToArray()));
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
                                V(SyntaxKind.IdentifierToken, text: "lim");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.AdditionExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "lim");
                                }
                                V(SyntaxKind.PlusToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"a = a + 1\\n\"", value: ImmutableArray.Create("a = a + 1\n"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "lim");
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
                            V(SyntaxKind.ConcatenationExpression);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"while true do \"", value: ImmutableArray.Create("while true do "U8.ToArray()));
                                }
                                V(SyntaxKind.DotDotToken);
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "s");
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"end\"", value: ImmutableArray.Create("end"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "load");
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
                                                            V(SyntaxKind.StringLiteralToken, text: "\"too long\"", value: ImmutableArray.Create("too long"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
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
                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.IdentifierToken, text: "manylines");
                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"loading chunk with too many lines\"", value: ImmutableArray.Create("loading chunk with too many lines"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "loadrep");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"lines\"", value: ImmutableArray.Create("lines"U8.ToArray()));
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
                                                            V(SyntaxKind.StringLiteralToken, text: "\"too many lines\"", value: ImmutableArray.Create("too many lines"U8.ToArray()));
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
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "hugeid");
                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"loading chunk with huge identifier\"", value: ImmutableArray.Create("loading chunk with huge identifier"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "loadrep");
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"chars\"", value: ImmutableArray.Create("chars"U8.ToArray()));
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
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.OrExpression);
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
                                                                    V(SyntaxKind.IdentifierToken, text: "msg");
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"lexical element too long\"", value: ImmutableArray.Create("lexical element too long"U8.ToArray()));
                                                                }
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.OrKeyword);
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
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"not enough memory\"", value: ImmutableArray.Create("not enough memory"U8.ToArray()));
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
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "toomanyinst");
                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"loading chunk with too many instructions\"", value: ImmutableArray.Create("loading chunk with too many instructions"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "loadrep");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"a = 10; \"", value: ImmutableArray.Create("a = 10; "U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"instructions\"", value: ImmutableArray.Create("instructions"U8.ToArray()));
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "loadrepfunc");
                    }
                    V(SyntaxKind.ParameterList);
                    {
                        V(SyntaxKind.OpenParenToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "prefix");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "f");
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
                                    V(SyntaxKind.IdentifierToken, text: "count");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.UnaryMinusExpression);
                                {
                                    V(SyntaxKind.MinusToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                V(SyntaxKind.IdentifierToken, text: "aux");
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
                                        V(SyntaxKind.IdentifierToken, text: "count");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "count");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                            V(SyntaxKind.IdentifierToken, text: "count");
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
                                        V(SyntaxKind.ReturnStatement);
                                        {
                                            V(SyntaxKind.ReturnKeyword);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "prefix");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.ElseClause);
                                    {
                                        V(SyntaxKind.ElseKeyword);
                                        V(SyntaxKind.Block);
                                        {
                                            V(SyntaxKind.IfStatement);
                                            {
                                                V(SyntaxKind.IfKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.ModuloExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "count");
                                                        }
                                                        V(SyntaxKind.PersentToken);
                                                        V(SyntaxKind.ParenthesizedExpression);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0x100000", value: 1048576L);
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
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
                                                            V(SyntaxKind.ImplicitSelfParameterExpression);
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
                                                                        V(SyntaxKind.IdentifierToken, text: "stderr");
                                                                    }
                                                                }
                                                                V(SyntaxKind.ColonToken);
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
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"(\"", value: ImmutableArray.Create("("U8.ToArray()));
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.FloorDivisionExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "count");
                                                                        }
                                                                        V(SyntaxKind.SlashSlashToken);
                                                                        V(SyntaxKind.ExponentiationExpression);
                                                                        {
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                            }
                                                                            V(SyntaxKind.CaretToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\" M)\"", value: ImmutableArray.Create(" M)"U8.ToArray()));
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
                                                                V(SyntaxKind.IdentifierToken, text: "count");
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
                                        V(SyntaxKind.IdentifierToken, text: "load");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "aux");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"k\"", value: ImmutableArray.Create("k"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\nmemory: \"", value: ImmutableArray.Create("\nmemory: "U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.MultiplicationExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "'count'", value: ImmutableArray.Create("count"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.AsteriskToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1024", value: 1024L);
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
                                V(SyntaxKind.IdentifierToken, text: "msg");
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
                                        V(SyntaxKind.IdentifierToken, text: "match");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"^[^\\n]+\"", value: ImmutableArray.Create("^[^\n]+"U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"expected error: \"", value: ImmutableArray.Create("expected error: "U8.ToArray()));
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
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.FunctionDefinitionStatement);
                {
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "toomanyconst");
                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "print");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"loading function with too many constants\"", value: ImmutableArray.Create("loading function with too many constants"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "loadrepfunc");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"function foo () return {0,\"", value: ImmutableArray.Create("function foo () return {0,"U8.ToArray()));
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
                                                V(SyntaxKind.Parameter);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "n");
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
                                                        V(SyntaxKind.SimpleMemberAccessExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "string");
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
                                                                    V(SyntaxKind.NumericLiteralToken, text: "34", value: 34L);
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.AdditionExpression);
                                                                {
                                                                    V(SyntaxKind.ParenthesizedExpression);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.BitwiseAndExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.FloorDivisionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                                    }
                                                                                    V(SyntaxKind.SlashSlashToken);
                                                                                    V(SyntaxKind.ExponentiationExpression);
                                                                                    {
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                                        }
                                                                                        V(SyntaxKind.CaretToken);
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.AmpersandToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "127", value: 127L);
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                    V(SyntaxKind.PlusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.AdditionExpression);
                                                                {
                                                                    V(SyntaxKind.ParenthesizedExpression);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.BitwiseAndExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.FloorDivisionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                                    }
                                                                                    V(SyntaxKind.SlashSlashToken);
                                                                                    V(SyntaxKind.ExponentiationExpression);
                                                                                    {
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                                        }
                                                                                        V(SyntaxKind.CaretToken);
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.AmpersandToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "127", value: 127L);
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                    V(SyntaxKind.PlusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.AdditionExpression);
                                                                {
                                                                    V(SyntaxKind.ParenthesizedExpression);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.BitwiseAndExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.FloorDivisionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                                    }
                                                                                    V(SyntaxKind.SlashSlashToken);
                                                                                    V(SyntaxKind.ExponentiationExpression);
                                                                                    {
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                                        }
                                                                                        V(SyntaxKind.CaretToken);
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.AmpersandToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "127", value: 127L);
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                    V(SyntaxKind.PlusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.AdditionExpression);
                                                                {
                                                                    V(SyntaxKind.ParenthesizedExpression);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.BitwiseAndExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.FloorDivisionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                                    }
                                                                                    V(SyntaxKind.SlashSlashToken);
                                                                                    V(SyntaxKind.ExponentiationExpression);
                                                                                    {
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                                        }
                                                                                        V(SyntaxKind.CaretToken);
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.AmpersandToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "127", value: 127L);
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                    V(SyntaxKind.PlusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.AdditionExpression);
                                                                {
                                                                    V(SyntaxKind.ParenthesizedExpression);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.BitwiseAndExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.FloorDivisionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "n");
                                                                                    }
                                                                                    V(SyntaxKind.SlashSlashToken);
                                                                                    V(SyntaxKind.ExponentiationExpression);
                                                                                    {
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                                        }
                                                                                        V(SyntaxKind.CaretToken);
                                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.AmpersandToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "127", value: 127L);
                                                                            }
                                                                        }
                                                                        V(SyntaxKind.CloseParenToken);
                                                                    }
                                                                    V(SyntaxKind.PlusToken);
                                                                    V(SyntaxKind.NumericLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.NumericLiteralToken, text: "128", value: 128L);
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "34", value: 34L);
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "44", value: 44L);
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
                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.IdentifierToken, text: "toomanystr");
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
                                                                V(SyntaxKind.IdentifierToken, text: "math");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "huge");
                                                            }
                                                        }
                                                        V(SyntaxKind.DoKeyword);
                                                        V(SyntaxKind.Block);
                                                        {
                                                            V(SyntaxKind.IfStatement);
                                                            {
                                                                V(SyntaxKind.IfKeyword);
                                                                V(SyntaxKind.EqualExpression);
                                                                {
                                                                    V(SyntaxKind.ModuloExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                        V(SyntaxKind.PersentToken);
                                                                        V(SyntaxKind.ParenthesizedExpression);
                                                                        {
                                                                            V(SyntaxKind.OpenParenToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "0x100000", value: 1048576L);
                                                                            }
                                                                            V(SyntaxKind.CloseParenToken);
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
                                                                            V(SyntaxKind.ImplicitSelfParameterExpression);
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
                                                                                        V(SyntaxKind.IdentifierToken, text: "stderr");
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.ColonToken);
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
                                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                                    {
                                                                                        V(SyntaxKind.StringLiteralToken, text: "\"(\"", value: ImmutableArray.Create("("U8.ToArray()));
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CommaToken);
                                                                                V(SyntaxKind.Argument);
                                                                                {
                                                                                    V(SyntaxKind.FloorDivisionExpression);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierName);
                                                                                        {
                                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                                        }
                                                                                        V(SyntaxKind.SlashSlashToken);
                                                                                        V(SyntaxKind.ExponentiationExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                                            {
                                                                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                                            }
                                                                                            V(SyntaxKind.CaretToken);
                                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                                            {
                                                                                                V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CommaToken);
                                                                                V(SyntaxKind.Argument);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                                    {
                                                                                        V(SyntaxKind.StringLiteralToken, text: "\" M)\"", value: ImmutableArray.Create(" M)"U8.ToArray()));
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
                                                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.ArgumentList);
                                                                    {
                                                                        V(SyntaxKind.OpenParenToken);
                                                                        V(SyntaxKind.Argument);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.StringLiteralToken, text: "\"I\"", value: ImmutableArray.Create("I"U8.ToArray()));
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "size");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.LengthExpression);
                                {
                                    V(SyntaxKind.HashToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "a");
                                    }
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
                                    V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                                }
                                V(SyntaxKind.ArgumentString);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "'count'", value: ImmutableArray.Create("count"U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\nmemory:\"", value: ImmutableArray.Create("\nmemory:"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.MultiplicationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "a");
                                            }
                                            V(SyntaxKind.AsteriskToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1024", value: 1024L);
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"expected error:\"", value: ImmutableArray.Create("expected error:"U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"size:\"", value: ImmutableArray.Create("size:"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "size");
                                        }
                                    }
                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.IdentifierToken, text: "toomanyidx");
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
                                                                V(SyntaxKind.IdentifierToken, text: "math");
                                                            }
                                                            V(SyntaxKind.DotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "huge");
                                                            }
                                                        }
                                                        V(SyntaxKind.DoKeyword);
                                                        V(SyntaxKind.Block);
                                                        {
                                                            V(SyntaxKind.IfStatement);
                                                            {
                                                                V(SyntaxKind.IfKeyword);
                                                                V(SyntaxKind.EqualExpression);
                                                                {
                                                                    V(SyntaxKind.ModuloExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                        V(SyntaxKind.PersentToken);
                                                                        V(SyntaxKind.ParenthesizedExpression);
                                                                        {
                                                                            V(SyntaxKind.OpenParenToken);
                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                            {
                                                                                V(SyntaxKind.NumericLiteralToken, text: "0x100000", value: 1048576L);
                                                                            }
                                                                            V(SyntaxKind.CloseParenToken);
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
                                                                            V(SyntaxKind.ImplicitSelfParameterExpression);
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
                                                                                        V(SyntaxKind.IdentifierToken, text: "stderr");
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.ColonToken);
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
                                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                                    {
                                                                                        V(SyntaxKind.StringLiteralToken, text: "\"(\"", value: ImmutableArray.Create("("U8.ToArray()));
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CommaToken);
                                                                                V(SyntaxKind.Argument);
                                                                                {
                                                                                    V(SyntaxKind.FloorDivisionExpression);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierName);
                                                                                        {
                                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                                        }
                                                                                        V(SyntaxKind.SlashSlashToken);
                                                                                        V(SyntaxKind.ExponentiationExpression);
                                                                                        {
                                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                                            {
                                                                                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                                                                                            }
                                                                                            V(SyntaxKind.CaretToken);
                                                                                            V(SyntaxKind.NumericLiteralExpression);
                                                                                            {
                                                                                                V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                V(SyntaxKind.CommaToken);
                                                                                V(SyntaxKind.Argument);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                                    {
                                                                                        V(SyntaxKind.StringLiteralToken, text: "\" M)\"", value: ImmutableArray.Create(" M)"U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"\\nmemory: \"", value: ImmutableArray.Create("\nmemory: "U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.MultiplicationExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "collectgarbage");
                                                }
                                                V(SyntaxKind.ArgumentString);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "'count'", value: ImmutableArray.Create("count"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.AsteriskToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1024", value: 1024L);
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"expected error: \"", value: ImmutableArray.Create("expected error: "U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"size:\"", value: ImmutableArray.Create("size:"U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierToken, text: "a");
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
                            V(SyntaxKind.IdentifierToken, text: "toomanyidx");
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
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "\"OK\"", value: ImmutableArray.Create("OK"U8.ToArray()));
                        }
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
