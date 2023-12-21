// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_bwcoercion(string source, SourceCodeKind kind)
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
                            V(SyntaxKind.IdentifierToken, text: "tonumber");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "tointeger");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "tonumber");
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
                                V(SyntaxKind.IdentifierToken, text: "tointeger");
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
                            V(SyntaxKind.IdentifierToken, text: "type");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "getmetatable");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "rawget");
                        }
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "error");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "type");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "getmetatable");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "rawget");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "error");
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
                            V(SyntaxKind.IdentifierToken, text: "strsub");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
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
                    }
                }
                V(SyntaxKind.LocalDeclarationStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.NameAttributeList);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "print");
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "toint");
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
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "tonumber");
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
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.LogicalNotExpression);
                            {
                                V(SyntaxKind.NotKeyword);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "x");
                                }
                            }
                            V(SyntaxKind.ThenKeyword);
                            V(SyntaxKind.Block);
                            {
                                V(SyntaxKind.ReturnStatement);
                                {
                                    V(SyntaxKind.ReturnKeyword);
                                    V(SyntaxKind.FalseLiteralExpression);
                                    {
                                        V(SyntaxKind.FalseKeyword);
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
                                    V(SyntaxKind.IdentifierToken, text: "tointeger");
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "trymt");
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
                            V(SyntaxKind.IdentifierToken, text: "y");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "mtname");
                        }
                        V(SyntaxKind.CloseParenToken);
                    }
                    V(SyntaxKind.Block);
                    {
                        V(SyntaxKind.IfStatement);
                        {
                            V(SyntaxKind.IfKeyword);
                            V(SyntaxKind.NotEqualExpression);
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
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                        V(SyntaxKind.CloseParenToken);
                                    }
                                }
                                V(SyntaxKind.TildeEqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"string\"", value: ImmutableArray.Create("string"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierToken, text: "mt");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
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
                                                        V(SyntaxKind.IdentifierToken, text: "y");
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
                                            V(SyntaxKind.IdentifierToken, text: "mm");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "mt");
                                            }
                                            V(SyntaxKind.AndKeyword);
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
                                                            V(SyntaxKind.IdentifierToken, text: "mt");
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "mtname");
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                        }
                                    }
                                }
                                V(SyntaxKind.IfStatement);
                                {
                                    V(SyntaxKind.IfKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "mm");
                                    }
                                    V(SyntaxKind.ThenKeyword);
                                    V(SyntaxKind.Block);
                                    {
                                        V(SyntaxKind.ReturnStatement);
                                        {
                                            V(SyntaxKind.ReturnKeyword);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "mm");
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
                                                            V(SyntaxKind.IdentifierToken, text: "y");
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
                                    V(SyntaxKind.IdentifierToken, text: "error");
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"attempt to '\"", value: ImmutableArray.Create("attempt to '"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "strsub");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "mtname");
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
                                                V(SyntaxKind.DotDotToken);
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"' a \"", value: ImmutableArray.Create("' a "U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.ConcatenationExpression);
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
                                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                                    }
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                        }
                                                        V(SyntaxKind.DotDotToken);
                                                        V(SyntaxKind.ConcatenationExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\" with a \"", value: ImmutableArray.Create(" with a "U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
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
                                                                            V(SyntaxKind.IdentifierToken, text: "y");
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
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
                V(SyntaxKind.LocalFunctionDefinitionStatement);
                {
                    V(SyntaxKind.LocalKeyword);
                    V(SyntaxKind.FunctionKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                            V(SyntaxKind.IdentifierToken, text: "y");
                        }
                        V(SyntaxKind.CommaToken);
                        V(SyntaxKind.Parameter);
                        {
                            V(SyntaxKind.IdentifierToken, text: "mtname");
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
                                    V(SyntaxKind.IdentifierToken, text: "xi");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "toint");
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "yi");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "toint");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
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
                            V(SyntaxKind.AndExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "xi");
                                }
                                V(SyntaxKind.AndKeyword);
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "yi");
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
                                        V(SyntaxKind.IdentifierToken, text: "xi");
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "yi");
                                    }
                                }
                            }
                            V(SyntaxKind.ElseClause);
                            {
                                V(SyntaxKind.ElseKeyword);
                                V(SyntaxKind.Block);
                                {
                                    V(SyntaxKind.ReturnStatement);
                                    {
                                        V(SyntaxKind.ReturnKeyword);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "trymt");
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
                                                        V(SyntaxKind.IdentifierToken, text: "y");
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "mtname");
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.NilLiteralExpression);
                                        {
                                            V(SyntaxKind.NilKeyword);
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
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
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
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__band");
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
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Parameter);
                            {
                                V(SyntaxKind.IdentifierToken, text: "y");
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
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.NameAttributeList);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "y");
                                    }
                                }
                                V(SyntaxKind.EqualsValuesClause);
                                {
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"__band\"", value: ImmutableArray.Create("__band"U8.ToArray()));
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
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "y");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.BitwiseAndExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.AmpersandToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EndKeyword);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__bor");
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
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Parameter);
                            {
                                V(SyntaxKind.IdentifierToken, text: "y");
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
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.NameAttributeList);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "y");
                                    }
                                }
                                V(SyntaxKind.EqualsValuesClause);
                                {
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"__bor\"", value: ImmutableArray.Create("__bor"U8.ToArray()));
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
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "y");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.BitwiseOrExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.BarToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EndKeyword);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__bxor");
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
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Parameter);
                            {
                                V(SyntaxKind.IdentifierToken, text: "y");
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
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.NameAttributeList);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "y");
                                    }
                                }
                                V(SyntaxKind.EqualsValuesClause);
                                {
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"__bxor\"", value: ImmutableArray.Create("__bxor"U8.ToArray()));
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
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "y");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.BitwiseExclusiveOrExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.TildeToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EndKeyword);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__shl");
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
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Parameter);
                            {
                                V(SyntaxKind.IdentifierToken, text: "y");
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
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.NameAttributeList);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "y");
                                    }
                                }
                                V(SyntaxKind.EqualsValuesClause);
                                {
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"__shl\"", value: ImmutableArray.Create("__shl"U8.ToArray()));
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
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "y");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.BitwiseLeftShiftExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.LessThanLessThanToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EndKeyword);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__shr");
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
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Parameter);
                            {
                                V(SyntaxKind.IdentifierToken, text: "y");
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
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.NameAttributeList);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "y");
                                    }
                                }
                                V(SyntaxKind.EqualsValuesClause);
                                {
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                                                    V(SyntaxKind.IdentifierToken, text: "y");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"__shr\"", value: ImmutableArray.Create("__shr"U8.ToArray()));
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
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "y");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.BitwiseRightShiftExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.GreaterThanGreaterThanToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "y");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EndKeyword);
                    }
                }
                V(SyntaxKind.AssignmentStatement);
                {
                    V(SyntaxKind.SimpleMemberAccessExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "smt");
                        }
                        V(SyntaxKind.DotToken);
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "__bnot");
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
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.NameAttributeList);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "y");
                                    }
                                }
                                V(SyntaxKind.EqualsValuesClause);
                                {
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.InvocationExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "checkargs");
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
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"__bnot\"", value: ImmutableArray.Create("__bnot"U8.ToArray()));
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
                                V(SyntaxKind.OrExpression);
                                {
                                    V(SyntaxKind.AndExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "y");
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.BitwiseNotExpression);
                                        {
                                            V(SyntaxKind.TildeToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.OrKeyword);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                            }
                        }
                        V(SyntaxKind.EndKeyword);
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
