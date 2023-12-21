// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

partial class FileTests
{
    private void TestOfficialTestFile_tpack(string source, SourceCodeKind kind)
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
                            V(SyntaxKind.IdentifierToken, text: "pack");
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
                                V(SyntaxKind.IdentifierToken, text: "pack");
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
                            V(SyntaxKind.IdentifierToken, text: "packsize");
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
                                V(SyntaxKind.IdentifierToken, text: "packsize");
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
                            V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                V(SyntaxKind.IdentifierToken, text: "unpack");
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
                        V(SyntaxKind.ArgumentString);
                        {
                            V(SyntaxKind.StringLiteralToken, text: "\"testing pack/unpack\"", value: ImmutableArray.Create("testing pack/unpack"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "NB");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "16", value: 16L);
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
                            V(SyntaxKind.IdentifierToken, text: "sizeshort");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"h\"", value: ImmutableArray.Create("h"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizeint");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"i\"", value: ImmutableArray.Create("i"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizelong");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"l\"", value: ImmutableArray.Create("l"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizesize_t");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"T\"", value: ImmutableArray.Create("T"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizeLI");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"j\"", value: ImmutableArray.Create("j"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizefloat");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"f\"", value: ImmutableArray.Create("f"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizedouble");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"d\"", value: ImmutableArray.Create("d"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "sizenumber");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"n\"", value: ImmutableArray.Create("n"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "little");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.ParenthesizedExpression);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.EqualExpression);
                            {
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"i2\"", value: ImmutableArray.Create("i2"U8.ToArray()));
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\1\\0\"", value: ImmutableArray.Create("\u0001\0"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "align");
                        }
                    }
                    V(SyntaxKind.EqualsValuesClause);
                    {
                        V(SyntaxKind.EqualsToken);
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"!xXi16\"", value: ImmutableArray.Create("!xXi16"U8.ToArray()));
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
                                        V(SyntaxKind.AndExpression);
                                        {
                                            V(SyntaxKind.LessThanOrEqualExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                }
                                                V(SyntaxKind.LessThanEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "sizeshort");
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.LessThanOrEqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "sizeshort");
                                                }
                                                V(SyntaxKind.LessThanEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "sizeint");
                                                }
                                            }
                                        }
                                        V(SyntaxKind.AndKeyword);
                                        V(SyntaxKind.LessThanOrEqualExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizeint");
                                            }
                                            V(SyntaxKind.LessThanEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizelong");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.AndKeyword);
                                    V(SyntaxKind.LessThanOrEqualExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "sizefloat");
                                        }
                                        V(SyntaxKind.LessThanEqualsToken);
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "sizedouble");
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"platform:\"", value: ImmutableArray.Create("platform:"U8.ToArray()));
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\tshort %d, int %d, long %d, size_t %d, float %d, double %d,\\n\\z\n   \\tlua Integer %d, lua Number %d\"", value: ImmutableArray.Create("\tshort %d, int %d, long %d, size_t %d, float %d, double %d,\n\tlua Integer %d, lua Number %d"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizeshort");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizeint");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizelong");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizesize_t");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizefloat");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizedouble");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizeLI");
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "sizenumber");
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
                                        V(SyntaxKind.StringLiteralToken, text: "\"\\t\"", value: ImmutableArray.Create("\t"U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.ConcatenationExpression);
                                    {
                                        V(SyntaxKind.ParenthesizedExpression);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.OrExpression);
                                            {
                                                V(SyntaxKind.AndExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "little");
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"little\"", value: ImmutableArray.Create("little"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.OrKeyword);
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"big\"", value: ImmutableArray.Create("big"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                        V(SyntaxKind.DotDotToken);
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\" endian\"", value: ImmutableArray.Create(" endian"U8.ToArray()));
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
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"\\talignment: \"", value: ImmutableArray.Create("\talignment: "U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "align");
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
                                    V(SyntaxKind.IdentifierToken, text: "status");
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
                                                    V(SyntaxKind.IdentifierToken, text: "status");
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"B\"", value: ImmutableArray.Create("B"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"B\"", value: ImmutableArray.Create("B"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0xff", value: 255L);
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0xff", value: 255L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
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
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
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
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0x7f", value: 127L);
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0x7f", value: 127L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
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
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
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
                                                            V(SyntaxKind.UnaryMinusExpression);
                                                            {
                                                                V(SyntaxKind.MinusToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "0x80", value: 128L);
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
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x80", value: 128L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"H\"", value: ImmutableArray.Create("H"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"H\"", value: ImmutableArray.Create("H"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0xffff", value: 65535L);
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0xffff", value: 65535L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"h\"", value: ImmutableArray.Create("h"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"h\"", value: ImmutableArray.Create("h"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0x7fff", value: 32767L);
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0x7fff", value: 32767L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"h\"", value: ImmutableArray.Create("h"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"h\"", value: ImmutableArray.Create("h"U8.ToArray()));
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
                                                                    V(SyntaxKind.NumericLiteralToken, text: "0x8000", value: 32768L);
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
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x8000", value: 32768L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"L\"", value: ImmutableArray.Create("L"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"L\"", value: ImmutableArray.Create("L"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0xffffffff", value: 4294967295L);
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0xffffffff", value: 4294967295L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"l\"", value: ImmutableArray.Create("l"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"l\"", value: ImmutableArray.Create("l"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "0x7fffffff", value: 2147483647L);
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0x7fffffff", value: 2147483647L);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"l\"", value: ImmutableArray.Create("l"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"l\"", value: ImmutableArray.Create("l"U8.ToArray()));
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
                                                                    V(SyntaxKind.NumericLiteralToken, text: "0x80000000", value: 2147483648L);
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
                                    V(SyntaxKind.EqualsEqualsToken);
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0x80000000", value: 2147483648L);
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
                        V(SyntaxKind.IdentifierToken, text: "NB");
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\xff\"", value: ImmutableArray.Create<byte>(255));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"i\"", value: ImmutableArray.Create("i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"i\"", value: ImmutableArray.Create("i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"i\"", value: ImmutableArray.Create("i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
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
                                            V(SyntaxKind.EqualsEqualsToken);
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xAA\"", value: ImmutableArray.Create<byte>(170));
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.SubtractionExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "i");
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<I\"", value: ImmutableArray.Create("<I"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "0xAA", value: 170L);
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<I\"", value: ImmutableArray.Create("<I"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0xAA", value: 170L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\">I\"", value: ImmutableArray.Create(">I"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "0xAA", value: 170L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.ImplicitSelfParameterExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                    }
                                                    V(SyntaxKind.ColonToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\">I\"", value: ImmutableArray.Create(">I"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.ImplicitSelfParameterExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                                V(SyntaxKind.ColonToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0xAA", value: 170L);
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
                                    V(SyntaxKind.IdentifierToken, text: "lnum");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "0x13121110090807060504030201", value: 85886224804380780D);
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
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.AdditionExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "sizeLI");
                                                                }
                                                                V(SyntaxKind.PlusToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
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
                                                                V(SyntaxKind.IdentifierToken, text: "s");
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.AdditionExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "sizeLI");
                                                                }
                                                                V(SyntaxKind.PlusToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                                }
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
                                                                V(SyntaxKind.IdentifierToken, text: "s");
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "lnum");
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
                            V(SyntaxKind.AdditionExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "sizeLI");
                                }
                                V(SyntaxKind.PlusToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "NB");
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
                                            V(SyntaxKind.IdentifierToken, text: "s");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
                                                    }
                                                }
                                                V(SyntaxKind.CommaToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
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
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.StringLiteralExpression);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xFF\"", value: ImmutableArray.Create<byte>(255));
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.ColonToken);
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
                                                                                V(SyntaxKind.SubtractionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                                    }
                                                                                    V(SyntaxKind.MinusToken);
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "sizeLI");
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
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                        V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.ConcatenationExpression);
                                                                {
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.StringLiteralExpression);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xFF\"", value: ImmutableArray.Create<byte>(255));
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.ColonToken);
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
                                                                                V(SyntaxKind.SubtractionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                                    }
                                                                                    V(SyntaxKind.MinusToken);
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "sizeLI");
                                                                                    }
                                                                                }
                                                                            }
                                                                            V(SyntaxKind.CloseParenToken);
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                                        {
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "s");
                                                                            }
                                                                            V(SyntaxKind.ColonToken);
                                                                            V(SyntaxKind.IdentifierName);
                                                                            {
                                                                                V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"<I\"", value: ImmutableArray.Create("<I"U8.ToArray()));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.StringLiteralExpression);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\0\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.ColonToken);
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
                                                                                V(SyntaxKind.SubtractionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                                    }
                                                                                    V(SyntaxKind.MinusToken);
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "sizeLI");
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
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "lnum");
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
                                                    V(SyntaxKind.StringLiteralToken, text: "\"does not fit\"", value: ImmutableArray.Create("does not fit"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"<I\"", value: ImmutableArray.Create("<I"U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                        {
                                                            V(SyntaxKind.ParenthesizedExpression);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\x00\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                            V(SyntaxKind.ColonToken);
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
                                                                V(SyntaxKind.SubtractionExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"\\1\"", value: ImmutableArray.Create("\u0001"U8.ToArray()));
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
                                                    V(SyntaxKind.StringLiteralToken, text: "\"does not fit\"", value: ImmutableArray.Create("does not fit"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"\\1\"", value: ImmutableArray.Create("\u0001"U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                        {
                                                            V(SyntaxKind.ParenthesizedExpression);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\x00\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                                                }
                                                                V(SyntaxKind.CloseParenToken);
                                                            }
                                                            V(SyntaxKind.ColonToken);
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
                                                                V(SyntaxKind.SubtractionExpression);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                        V(SyntaxKind.IdentifierToken, text: "sizeLI");
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
                                    V(SyntaxKind.IdentifierToken, text: "lstr");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"\\1\\2\\3\\4\\5\\6\\7\\8\\9\\10\\11\\12\\13\"", value: ImmutableArray.Create("\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "lnum");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "0x13121110090807060504030201", value: 85886224804380780D);
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
                                    V(SyntaxKind.IdentifierToken, text: "n");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.BitwiseAndExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "lnum");
                                    }
                                    V(SyntaxKind.AmpersandToken);
                                    V(SyntaxKind.ParenthesizedExpression);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.BitwiseNotExpression);
                                        {
                                            V(SyntaxKind.TildeToken);
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.BitwiseLeftShiftExpression);
                                                {
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                        }
                                                    }
                                                    V(SyntaxKind.LessThanLessThanToken);
                                                    V(SyntaxKind.ParenthesizedExpression);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.MultiplicationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                            V(SyntaxKind.AsteriskToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "8", value: 8L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
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
                                                V(SyntaxKind.IdentifierToken, text: "lstr");
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
                                                V(SyntaxKind.IdentifierToken, text: "i");
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.ImplicitSelfParameterExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "s");
                                                    }
                                                    V(SyntaxKind.ColonToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.ImplicitSelfParameterExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                                }
                                                                V(SyntaxKind.ColonToken);
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                    V(SyntaxKind.IdentifierToken, text: "u");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.NumericLiteralExpression);
                                {
                                    V(SyntaxKind.NumericLiteralToken, text: "0xf0", value: 240L);
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
                            V(SyntaxKind.SubtractionExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "sizeLI");
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.ConcatenationExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"\\xf0\"", value: ImmutableArray.Create<byte>(240));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.StringLiteralExpression);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xff\"", value: ImmutableArray.Create<byte>(255));
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.ColonToken);
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
                                                                                V(SyntaxKind.SubtractionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "16", value: 16L);
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
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                        V(SyntaxKind.StringLiteralToken, text: "\">I\"", value: ImmutableArray.Create(">I"U8.ToArray()));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                                    }
                                                                }
                                                            }
                                                            V(SyntaxKind.CommaToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.ConcatenationExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"\\xf0\"", value: ImmutableArray.Create<byte>(240));
                                                                    }
                                                                    V(SyntaxKind.DotDotToken);
                                                                    V(SyntaxKind.InvocationExpression);
                                                                    {
                                                                        V(SyntaxKind.ImplicitSelfParameterExpression);
                                                                        {
                                                                            V(SyntaxKind.ParenthesizedExpression);
                                                                            {
                                                                                V(SyntaxKind.OpenParenToken);
                                                                                V(SyntaxKind.StringLiteralExpression);
                                                                                {
                                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xff\"", value: ImmutableArray.Create<byte>(255));
                                                                                }
                                                                                V(SyntaxKind.CloseParenToken);
                                                                            }
                                                                            V(SyntaxKind.ColonToken);
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
                                                                                V(SyntaxKind.SubtractionExpression);
                                                                                {
                                                                                    V(SyntaxKind.IdentifierName);
                                                                                    {
                                                                                        V(SyntaxKind.IdentifierToken, text: "i");
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
                                                            }
                                                            V(SyntaxKind.CloseParenToken);
                                                        }
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "u");
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
                                        V(SyntaxKind.IdentifierToken, text: "u");
                                    }
                                    V(SyntaxKind.EqualsToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.MultiplicationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "u");
                                            }
                                            V(SyntaxKind.AsteriskToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "256", value: 256L);
                                            }
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0xff", value: 255L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">i2 <i2\"", value: ImmutableArray.Create(">i2 <i2"U8.ToArray()));
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\\10\\20\\0\"", value: ImmutableArray.Create("\0\n\u0014\0"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<i2 >i2\"", value: ImmutableArray.Create("<i2 >i2"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\10\\0\\0\\20\"", value: ImmutableArray.Create("\n\0\0\u0014"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"=i4\"", value: ImmutableArray.Create("=i4"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2001", value: 2001L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"i4\"", value: ImmutableArray.Create("i4"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "2001", value: 2001L);
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
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"testing invalid formats\"", value: ImmutableArray.Create("testing invalid formats"U8.ToArray()));
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"out of limits\"", value: ImmutableArray.Create("out of limits"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"i0\"", value: ImmutableArray.Create("i0"U8.ToArray()));
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"out of limits\"", value: ImmutableArray.Create("out of limits"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"i\"", value: ImmutableArray.Create("i"U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "NB");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"out of limits\"", value: ImmutableArray.Create("out of limits"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"!\"", value: ImmutableArray.Create("!"U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "NB");
                                        }
                                        V(SyntaxKind.PlusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                    }
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"%(17%) out of limits %[1,16%]\"", value: ImmutableArray.Create("%(17%) out of limits %[1,16%]"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"Xi\"", value: ImmutableArray.Create("Xi"U8.ToArray()));
                                    }
                                    V(SyntaxKind.DotDotToken);
                                    V(SyntaxKind.AdditionExpression);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "NB");
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"invalid format option 'r'\"", value: ImmutableArray.Create("invalid format option 'r'"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"i3r\"", value: ImmutableArray.Create("i3r"U8.ToArray()));
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"16%-byte integer\"", value: ImmutableArray.Create("16%-byte integer"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"i16\"", value: ImmutableArray.Create("i16"U8.ToArray()));
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
                                                V(SyntaxKind.StringLiteralToken, text: "'\\3'", value: ImmutableArray.Create("\u0003"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "16", value: 16L);
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"not power of 2\"", value: ImmutableArray.Create("not power of 2"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"!4i3\"", value: ImmutableArray.Create("!4i3"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"missing size\"", value: ImmutableArray.Create("missing size"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"c\"", value: ImmutableArray.Create("c"U8.ToArray()));
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
                            V(SyntaxKind.IdentifierToken, text: "checkerror");
                        }
                        V(SyntaxKind.ArgumentList);
                        {
                            V(SyntaxKind.OpenParenToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"variable%-length format\"", value: ImmutableArray.Create("variable%-length format"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"variable%-length format\"", value: ImmutableArray.Create("variable%-length format"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"z\"", value: ImmutableArray.Create("z"U8.ToArray()));
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
                                    V(SyntaxKind.StringLiteralToken, text: "\"invalid format\"", value: ImmutableArray.Create("invalid format"U8.ToArray()));
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.Argument);
                            {
                                V(SyntaxKind.ConcatenationExpression);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"c1\"", value: ImmutableArray.Create("c1"U8.ToArray()));
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
                                                    V(SyntaxKind.StringLiteralToken, text: "\"0\"", value: ImmutableArray.Create("0"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "40", value: 40L);
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
                V(SyntaxKind.IfStatement);
                {
                    V(SyntaxKind.IfKeyword);
                    V(SyntaxKind.EqualExpression);
                    {
                        V(SyntaxKind.InvocationExpression);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "packsize");
                            }
                            V(SyntaxKind.ArgumentList);
                            {
                                V(SyntaxKind.OpenParenToken);
                                V(SyntaxKind.Argument);
                                {
                                    V(SyntaxKind.StringLiteralExpression);
                                    {
                                        V(SyntaxKind.StringLiteralToken, text: "\"i\"", value: ImmutableArray.Create("i"U8.ToArray()));
                                    }
                                }
                                V(SyntaxKind.CloseParenToken);
                            }
                        }
                        V(SyntaxKind.EqualsEqualsToken);
                        V(SyntaxKind.NumericLiteralExpression);
                        {
                            V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"c268435456\"", value: ImmutableArray.Create("c268435456"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
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
                                                    V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"too large\"", value: ImmutableArray.Create("too large"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "packsize");
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
                        V(SyntaxKind.AssignmentStatement);
                        {
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "s");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.ConcatenationExpression);
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"c268435456\"", value: ImmutableArray.Create("c268435456"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.SubtractionExpression);
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
                                                        V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
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
                                V(SyntaxKind.DotDotToken);
                                V(SyntaxKind.StringLiteralExpression);
                                {
                                    V(SyntaxKind.StringLiteralToken, text: "\"c268435455\"", value: ImmutableArray.Create("c268435455"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
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
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "0x7fffffff", value: 2147483647L);
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
                    V(SyntaxKind.SubtractionExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "sizeLI");
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "umax");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
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
                                            V(SyntaxKind.ParenthesizedExpression);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.MultiplicationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "i");
                                                    }
                                                    V(SyntaxKind.AsteriskToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "8", value: 8L);
                                                    }
                                                }
                                                V(SyntaxKind.CloseParenToken);
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
                                V(SyntaxKind.BitwiseRightShiftExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "umax");
                                    }
                                    V(SyntaxKind.GreaterThanGreaterThanToken);
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
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
                                    V(SyntaxKind.IdentifierToken, text: "min");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.BitwiseNotExpression);
                                {
                                    V(SyntaxKind.TildeToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "max");
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<I\"", value: ImmutableArray.Create("<I"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<I\"", value: ImmutableArray.Create("<I"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "min");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\">I\"", value: ImmutableArray.Create(">I"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "umax");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "umax");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
                                            }
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "max");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"overflow\"", value: ImmutableArray.Create("overflow"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.ConcatenationExpression);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                            }
                                            V(SyntaxKind.DotDotToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "i");
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
                                                V(SyntaxKind.IdentifierToken, text: "min");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                            V(SyntaxKind.StringLiteralToken, text: "\">i\"", value: ImmutableArray.Create(">i"U8.ToArray()));
                                                                        }
                                                                        V(SyntaxKind.DotDotToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "max");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"<i\"", value: ImmutableArray.Create("<i"U8.ToArray()));
                                                                        }
                                                                        V(SyntaxKind.DotDotToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "min");
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
                                                V(SyntaxKind.IdentifierToken, text: "min");
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\">I\"", value: ImmutableArray.Create(">I"U8.ToArray()));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                                            V(SyntaxKind.StringLiteralToken, text: "\">I\"", value: ImmutableArray.Create(">I"U8.ToArray()));
                                                                        }
                                                                        V(SyntaxKind.DotDotToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "umax");
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
                                                V(SyntaxKind.IdentifierToken, text: "umax");
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
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\">j\"", value: ImmutableArray.Create(">j"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\">j\"", value: ImmutableArray.Create(">j"U8.ToArray()));
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
                                                                    V(SyntaxKind.IdentifierToken, text: "maxinteger");
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
                                    V(SyntaxKind.EqualsEqualsToken);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
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
                                            }
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
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
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                        V(SyntaxKind.ArgumentList);
                                        {
                                            V(SyntaxKind.OpenParenToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"<J\"", value: ImmutableArray.Create("<J"U8.ToArray()));
                                                }
                                            }
                                            V(SyntaxKind.CommaToken);
                                            V(SyntaxKind.Argument);
                                            {
                                                V(SyntaxKind.InvocationExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"<j\"", value: ImmutableArray.Create("<j"U8.ToArray()));
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
                                            V(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                    V(SyntaxKind.EqualsEqualsToken);
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
                            V(SyntaxKind.CloseParenToken);
                        }
                    }
                }
                V(SyntaxKind.IfStatement);
                {
                    V(SyntaxKind.IfKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "little");
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"f\"", value: ImmutableArray.Create("f"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "24", value: 24L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<f\"", value: ImmutableArray.Create("<f"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "24", value: 24L);
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
                    V(SyntaxKind.ElseClause);
                    {
                        V(SyntaxKind.ElseKeyword);
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
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"f\"", value: ImmutableArray.Create("f"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "24", value: 24L);
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
                                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                                    }
                                                    V(SyntaxKind.ArgumentList);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\">f\"", value: ImmutableArray.Create(">f"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "24", value: 24L);
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
                            V(SyntaxKind.StringLiteralToken, text: "\"testing pack/unpack of floating-point numbers\"", value: ImmutableArray.Create("testing pack/unpack of floating-point numbers"U8.ToArray()));
                        }
                    }
                }
                V(SyntaxKind.ForInStatement);
                {
                    V(SyntaxKind.ForKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "_");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "n");
                    }
                    V(SyntaxKind.InKeyword);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "ipairs");
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1.1", value: 1.1D);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1.9", value: 1.9D);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.DivisionExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                        V(SyntaxKind.SlashToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.DivisionExpression);
                                    {
                                        V(SyntaxKind.UnaryMinusExpression);
                                        {
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                        V(SyntaxKind.SlashToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1e20", value: 1E+20D);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1e20", value: 1E+20D);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "0.1", value: 0.1D);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "2000.7", value: 2000.7D);
                                    }
                                }
                                V(SyntaxKind.CloseBraceToken);
                            }
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"n\"", value: ImmutableArray.Create("n"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"n\"", value: ImmutableArray.Create("n"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<n\"", value: ImmutableArray.Create("<n"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"<n\"", value: ImmutableArray.Create("<n"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">n\"", value: ImmutableArray.Create(">n"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\">n\"", value: ImmutableArray.Create(">n"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<f\"", value: ImmutableArray.Create("<f"U8.ToArray()));
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.ImplicitSelfParameterExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\">f\"", value: ImmutableArray.Create(">f"U8.ToArray()));
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
                                                    V(SyntaxKind.ColonToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">d\"", value: ImmutableArray.Create(">d"U8.ToArray()));
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
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.ImplicitSelfParameterExpression);
                                                {
                                                    V(SyntaxKind.InvocationExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"<d\"", value: ImmutableArray.Create("<d"U8.ToArray()));
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
                                                    V(SyntaxKind.ColonToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "reverse");
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
                                    V(SyntaxKind.CloseParenToken);
                                }
                            }
                        }
                    }
                    V(SyntaxKind.EndKeyword);
                }
                V(SyntaxKind.ForInStatement);
                {
                    V(SyntaxKind.ForKeyword);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "_");
                    }
                    V(SyntaxKind.CommaToken);
                    V(SyntaxKind.IdentifierName);
                    {
                        V(SyntaxKind.IdentifierToken, text: "n");
                    }
                    V(SyntaxKind.InKeyword);
                    V(SyntaxKind.InvocationExpression);
                    {
                        V(SyntaxKind.IdentifierName);
                        {
                            V(SyntaxKind.IdentifierToken, text: "ipairs");
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
                                        V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1.5", value: 1.5D);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.DivisionExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                        }
                                        V(SyntaxKind.SlashToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.DivisionExpression);
                                    {
                                        V(SyntaxKind.UnaryMinusExpression);
                                        {
                                            V(SyntaxKind.MinusToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                            }
                                        }
                                        V(SyntaxKind.SlashToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "1e10", value: 10000000000D);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.UnaryMinusExpression);
                                    {
                                        V(SyntaxKind.MinusToken);
                                        V(SyntaxKind.NumericLiteralExpression);
                                        {
                                            V(SyntaxKind.NumericLiteralToken, text: "1e9", value: 1000000000D);
                                        }
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "0.5", value: 0.5D);
                                    }
                                }
                                V(SyntaxKind.CommaToken);
                                V(SyntaxKind.ItemField);
                                {
                                    V(SyntaxKind.NumericLiteralExpression);
                                    {
                                        V(SyntaxKind.NumericLiteralToken, text: "2000.25", value: 2000.25D);
                                    }
                                }
                                V(SyntaxKind.CloseBraceToken);
                            }
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<f\"", value: ImmutableArray.Create("<f"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"<f\"", value: ImmutableArray.Create("<f"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">f\"", value: ImmutableArray.Create(">f"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\">f\"", value: ImmutableArray.Create(">f"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<d\"", value: ImmutableArray.Create("<d"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"<d\"", value: ImmutableArray.Create("<d"U8.ToArray()));
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
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">d\"", value: ImmutableArray.Create(">d"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.InvocationExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pack");
                                                            }
                                                            V(SyntaxKind.ArgumentList);
                                                            {
                                                                V(SyntaxKind.OpenParenToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\">d\"", value: ImmutableArray.Create(">d"U8.ToArray()));
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
                            V(SyntaxKind.StringLiteralToken, text: "\"testing pack/unpack of strings\"", value: ImmutableArray.Create("testing pack/unpack of strings"U8.ToArray()));
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
                                                V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "1000", value: 1000L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"zB\"", value: ImmutableArray.Create("zB"U8.ToArray()));
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "247", value: 247L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                                V(SyntaxKind.DotDotToken);
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\0\\xF7\"", value: ImmutableArray.Create<byte>(0, 247));
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
                                    V(SyntaxKind.IdentifierToken, text: "s1");
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
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"zB\"", value: ImmutableArray.Create("zB"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
                                                }
                                                V(SyntaxKind.DotDotToken);
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\0\\xF9\"", value: ImmutableArray.Create<byte>(0, 249));
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
                                                    V(SyntaxKind.IdentifierToken, text: "b");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "249", value: 249L);
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s1");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "s");
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
                                V(SyntaxKind.IdentifierToken, text: "s1");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
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
                                        V(SyntaxKind.EqualExpression);
                                        {
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "s1");
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"does not fit\"", value: ImmutableArray.Create("does not fit"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"s1\"", value: ImmutableArray.Create("s1"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"contains zeros\"", value: ImmutableArray.Create("contains zeros"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"z\"", value: ImmutableArray.Create("z"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"alo\\0\"", value: ImmutableArray.Create("alo\0"U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"unfinished string\"", value: ImmutableArray.Create("unfinished string"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"zc10000000\"", value: ImmutableArray.Create("zc10000000"U8.ToArray()));
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
                                V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "NB");
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
                                            V(SyntaxKind.IdentifierToken, text: "s1");
                                        }
                                    }
                                    V(SyntaxKind.EqualsValuesClause);
                                    {
                                        V(SyntaxKind.EqualsToken);
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pack");
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
                                                            V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
                                                        }
                                                        V(SyntaxKind.DotDotToken);
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "i");
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
                                                                V(SyntaxKind.IdentifierToken, text: "unpack");
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
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
                                                                        }
                                                                        V(SyntaxKind.DotDotToken);
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                                        }
                                                                    }
                                                                }
                                                                V(SyntaxKind.CommaToken);
                                                                V(SyntaxKind.Argument);
                                                                {
                                                                    V(SyntaxKind.IdentifierName);
                                                                    {
                                                                        V(SyntaxKind.IdentifierToken, text: "s1");
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
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.LengthExpression);
                                                        {
                                                            V(SyntaxKind.HashToken);
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "s1");
                                                            }
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
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
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "i");
                                                            }
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
                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"too short\"", value: ImmutableArray.Create("too short"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"s\"", value: ImmutableArray.Create("s"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.ImplicitSelfParameterExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                                V(SyntaxKind.ColonToken);
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"too short\"", value: ImmutableArray.Create("too short"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"c5\"", value: ImmutableArray.Create("c5"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"abcd\"", value: ImmutableArray.Create("abcd"U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"out of limits\"", value: ImmutableArray.Create("out of limits"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"s100\"", value: ImmutableArray.Create("s100"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c0\"", value: ImmutableArray.Create("c0"U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c0\"", value: ImmutableArray.Create("c0"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c0\"", value: ImmutableArray.Create("c0"U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<! c3\"", value: ImmutableArray.Create("<! c3"U8.ToArray()));
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
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<! c3\"", value: ImmutableArray.Create("<! c3"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">!4 c6\"", value: ImmutableArray.Create(">!4 c6"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"abcdef\"", value: ImmutableArray.Create("abcdef"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"abcdef\"", value: ImmutableArray.Create("abcdef"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c3\"", value: ImmutableArray.Create("c3"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"123\"", value: ImmutableArray.Create("123"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"123\"", value: ImmutableArray.Create("123"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c0\"", value: ImmutableArray.Create("c0"U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c8\"", value: ImmutableArray.Create("c8"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"123456\"", value: ImmutableArray.Create("123456"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"123456\\0\\0\"", value: ImmutableArray.Create("123456\0\0"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c88\"", value: ImmutableArray.Create("c88"U8.ToArray()));
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
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "88", value: 88L);
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"c188\"", value: ImmutableArray.Create("c188"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CommaToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"ab\"", value: ImmutableArray.Create("ab"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"ab\"", value: ImmutableArray.Create("ab"U8.ToArray()));
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
                                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\"", value: ImmutableArray.Create("\0"U8.ToArray()));
                                                            }
                                                        }
                                                        V(SyntaxKind.CommaToken);
                                                        V(SyntaxKind.Argument);
                                                        {
                                                            V(SyntaxKind.SubtractionExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "188", value: 188L);
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
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!4 z c3\"", value: ImmutableArray.Create("!4 z c3"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"abcdefghi\\0xyz\"", value: ImmutableArray.Create("abcdefghi\0xyz"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "a");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"abcdefghi\"", value: ImmutableArray.Create("abcdefghi"U8.ToArray()));
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
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"xyz\"", value: ImmutableArray.Create("xyz"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"longer than\"", value: ImmutableArray.Create("longer than"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"c3\"", value: ImmutableArray.Create("c3"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"1234\"", value: ImmutableArray.Create("1234"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<b h b f d f n i\"", value: ImmutableArray.Create("<b h b f d f n i"U8.ToArray()));
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
                                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
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
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "8", value: 8L);
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"<b h b f d f n i\"", value: ImmutableArray.Create("<b h b f d f n i"U8.ToArray()));
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
                            V(SyntaxKind.CommaToken);
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
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "g");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "h");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"<b h b f d f n i\"", value: ImmutableArray.Create("<b h b f d f n i"U8.ToArray()));
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
                                                                            V(SyntaxKind.NumericLiteralToken, text: "2", value: 2L);
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
                                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                                                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "f");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "6", value: 6L);
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "g");
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "h");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "8", value: 8L);
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
                            V(SyntaxKind.StringLiteralToken, text: "\"testing alignment\"", value: ImmutableArray.Create("testing alignment"U8.ToArray()));
                        }
                    }
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
                                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\" < i1 i2 \"", value: ImmutableArray.Create(" < i1 i2 "U8.ToArray()));
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
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\2\\3\\0\"", value: ImmutableArray.Create("\u0002\u0003\0"U8.ToArray()));
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
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\">!8 b Xh i4 i8 c1 Xi8\"", value: ImmutableArray.Create(">!8 b Xh i4 i8 c1 Xi8"U8.ToArray()));
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
                                                    V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
                                                }
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "100", value: 100L);
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "200", value: 200L);
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\xEC\"", value: ImmutableArray.Create<byte>(236));
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
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.InvocationExpression);
                                            {
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\">!8 b Xh i4 i8 c1 Xi8\"", value: ImmutableArray.Create(">!8 b Xh i4 i8 c1 Xi8"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.ConcatenationExpression);
                                            {
                                                V(SyntaxKind.StringLiteralExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xf4\"", value: ImmutableArray.Create<byte>(244));
                                                }
                                                V(SyntaxKind.DotDotToken);
                                                V(SyntaxKind.ConcatenationExpression);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"\\0\\0\\0\"", value: ImmutableArray.Create("\0\0\0"U8.ToArray()));
                                                    }
                                                    V(SyntaxKind.DotDotToken);
                                                    V(SyntaxKind.ConcatenationExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"\\0\\0\\0\\100\"", value: ImmutableArray.Create("\0\0\0d"U8.ToArray()));
                                                        }
                                                        V(SyntaxKind.DotDotToken);
                                                        V(SyntaxKind.ConcatenationExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"\\0\\0\\0\\0\\0\\0\\0\\xC8\"", value: ImmutableArray.Create<byte>(0, 0, 0, 0, 0, 0, 0, 200));
                                                            }
                                                            V(SyntaxKind.DotDotToken);
                                                            V(SyntaxKind.ConcatenationExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\xEC\"", value: ImmutableArray.Create<byte>(236));
                                                                }
                                                                V(SyntaxKind.DotDotToken);
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"\\0\\0\\0\\0\\0\\0\\0\"", value: ImmutableArray.Create("\0\0\0\0\0\0\0"U8.ToArray()));
                                                                }
                                                            }
                                                        }
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
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\">!8 c1 Xh i4 i8 b Xi8 XI XH\"", value: ImmutableArray.Create(">!8 c1 Xh i4 i8 b Xi8 XI XH"U8.ToArray()));
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
                                                            V(SyntaxKind.StringLiteralExpression);
                                                            {
                                                                V(SyntaxKind.StringLiteralToken, text: "\"\\xF4\"", value: ImmutableArray.Create<byte>(244));
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
                                                                V(SyntaxKind.NumericLiteralToken, text: "100", value: 100L);
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
                                                            V(SyntaxKind.NumericLiteralToken, text: "200", value: 200L);
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
                                                    V(SyntaxKind.UnaryMinusExpression);
                                                    {
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "20", value: 20L);
                                                        }
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.ParenthesizedExpression);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.SubtractionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "pos");
                                                        }
                                                        V(SyntaxKind.MinusToken);
                                                        V(SyntaxKind.NumericLiteralExpression);
                                                        {
                                                            V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.LengthExpression);
                                                {
                                                    V(SyntaxKind.HashToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
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
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\">!4 c3 c4 c2 z i4 c5 c2 Xi4\"", value: ImmutableArray.Create(">!4 c3 c4 c2 z i4 c5 c2 Xi4"U8.ToArray()));
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
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"abcd\"", value: ImmutableArray.Create("abcd"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"xz\"", value: ImmutableArray.Create("xz"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"hello\"", value: ImmutableArray.Create("hello"U8.ToArray()));
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
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"world\"", value: ImmutableArray.Create("world"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"xy\"", value: ImmutableArray.Create("xy"U8.ToArray()));
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
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"abcabcdxzhello\\0\\0\\0\\0\\0\\5worldxy\\0\"", value: ImmutableArray.Create("abcabcdxzhello\0\0\0\0\0\u0005worldxy\0"U8.ToArray()));
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
                            V(SyntaxKind.CommaToken);
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
                                    V(SyntaxKind.IdentifierToken, text: "f");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "g");
                                }
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\">!4 c3 c4 c2 z i4 c5 c2 Xh Xi4\"", value: ImmutableArray.Create(">!4 c3 c4 c2 z i4 c5 c2 Xh Xi4"U8.ToArray()));
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
                                                                            V(SyntaxKind.IdentifierToken, text: "a");
                                                                        }
                                                                        V(SyntaxKind.EqualsEqualsToken);
                                                                        V(SyntaxKind.StringLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"abc\"", value: ImmutableArray.Create("abc"U8.ToArray()));
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
                                                                        V(SyntaxKind.StringLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.StringLiteralToken, text: "\"abcd\"", value: ImmutableArray.Create("abcd"U8.ToArray()));
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
                                                                    V(SyntaxKind.StringLiteralExpression);
                                                                    {
                                                                        V(SyntaxKind.StringLiteralToken, text: "\"xz\"", value: ImmutableArray.Create("xz"U8.ToArray()));
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
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"hello\"", value: ImmutableArray.Create("hello"U8.ToArray()));
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
                                                                V(SyntaxKind.NumericLiteralToken, text: "5", value: 5L);
                                                            }
                                                        }
                                                    }
                                                    V(SyntaxKind.AndKeyword);
                                                    V(SyntaxKind.EqualExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "f");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"world\"", value: ImmutableArray.Create("world"U8.ToArray()));
                                                        }
                                                    }
                                                }
                                                V(SyntaxKind.AndKeyword);
                                                V(SyntaxKind.EqualExpression);
                                                {
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "g");
                                                    }
                                                    V(SyntaxKind.EqualsEqualsToken);
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"xy\"", value: ImmutableArray.Create("xy"U8.ToArray()));
                                                    }
                                                }
                                            }
                                            V(SyntaxKind.AndKeyword);
                                            V(SyntaxKind.EqualExpression);
                                            {
                                                V(SyntaxKind.ModuloExpression);
                                                {
                                                    V(SyntaxKind.ParenthesizedExpression);
                                                    {
                                                        V(SyntaxKind.OpenParenToken);
                                                        V(SyntaxKind.SubtractionExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pos");
                                                            }
                                                            V(SyntaxKind.MinusToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                                                            }
                                                        }
                                                        V(SyntaxKind.CloseParenToken);
                                                    }
                                                    V(SyntaxKind.PersentToken);
                                                    V(SyntaxKind.NumericLiteralExpression);
                                                    {
                                                        V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                    }
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
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
                                V(SyntaxKind.IdentifierToken, text: "x");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pack");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\" b b Xd b Xb x\"", value: ImmutableArray.Create(" b b Xd b Xb x"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\" b b Xd b Xb x\"", value: ImmutableArray.Create(" b b Xd b Xb x"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
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
                                                V(SyntaxKind.IdentifierToken, text: "x");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"\\1\\2\\3\\0\"", value: ImmutableArray.Create("\u0001\u0002\u0003\0"U8.ToArray()));
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
                                V(SyntaxKind.IdentifierToken, text: "pos");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.InvocationExpression);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "unpack");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"bbXdb\"", value: ImmutableArray.Create("bbXdb"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.LengthExpression);
                                                {
                                                    V(SyntaxKind.HashToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
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
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"!8 xXi8\"", value: ImmutableArray.Create("!8 xXi8"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!8 xXi8\"", value: ImmutableArray.Create("!8 xXi8"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"0123456701234567\"", value: ImmutableArray.Create("0123456701234567"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pos");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "9", value: 9L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"!8 xXi2\"", value: ImmutableArray.Create("!8 xXi2"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!8 xXi2\"", value: ImmutableArray.Create("!8 xXi2"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"0123456701234567\"", value: ImmutableArray.Create("0123456701234567"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pos");
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"!2 xXi2\"", value: ImmutableArray.Create("!2 xXi2"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!2 xXi2\"", value: ImmutableArray.Create("!2 xXi2"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"0123456701234567\"", value: ImmutableArray.Create("0123456701234567"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pos");
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"!2 xXi8\"", value: ImmutableArray.Create("!2 xXi8"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
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
                        V(SyntaxKind.LocalDeclarationStatement);
                        {
                            V(SyntaxKind.LocalKeyword);
                            V(SyntaxKind.NameAttributeList);
                            {
                                V(SyntaxKind.IdentifierName);
                                {
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!2 xXi8\"", value: ImmutableArray.Create("!2 xXi8"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"0123456701234567\"", value: ImmutableArray.Create("0123456701234567"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pos");
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
                                                    V(SyntaxKind.IdentifierToken, text: "packsize");
                                                }
                                                V(SyntaxKind.ArgumentList);
                                                {
                                                    V(SyntaxKind.OpenParenToken);
                                                    V(SyntaxKind.Argument);
                                                    {
                                                        V(SyntaxKind.StringLiteralExpression);
                                                        {
                                                            V(SyntaxKind.StringLiteralToken, text: "\"!16 xXi16\"", value: ImmutableArray.Create("!16 xXi16"U8.ToArray()));
                                                        }
                                                    }
                                                    V(SyntaxKind.CloseParenToken);
                                                }
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "16", value: 16L);
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
                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                }
                            }
                            V(SyntaxKind.EqualsValuesClause);
                            {
                                V(SyntaxKind.EqualsToken);
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!16 xXi16\"", value: ImmutableArray.Create("!16 xXi16"U8.ToArray()));
                                            }
                                        }
                                        V(SyntaxKind.CommaToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"0123456701234567\"", value: ImmutableArray.Create("0123456701234567"U8.ToArray()));
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
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "pos");
                                            }
                                            V(SyntaxKind.EqualsEqualsToken);
                                            V(SyntaxKind.NumericLiteralExpression);
                                            {
                                                V(SyntaxKind.NumericLiteralToken, text: "17", value: 17L);
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"invalid next option\"", value: ImmutableArray.Create("invalid next option"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"invalid next option\"", value: ImmutableArray.Create("invalid next option"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"XXi\"", value: ImmutableArray.Create("XXi"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"invalid next option\"", value: ImmutableArray.Create("invalid next option"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"X i\"", value: ImmutableArray.Create("X i"U8.ToArray()));
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
                                    V(SyntaxKind.IdentifierToken, text: "checkerror");
                                }
                                V(SyntaxKind.ArgumentList);
                                {
                                    V(SyntaxKind.OpenParenToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"invalid next option\"", value: ImmutableArray.Create("invalid next option"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "pack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"Xc1\"", value: ImmutableArray.Create("Xc1"U8.ToArray()));
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
                                        V(SyntaxKind.IdentifierToken, text: "pack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"i4i4i4i4\"", value: ImmutableArray.Create("i4i4i4i4"U8.ToArray()));
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
                        V(SyntaxKind.ForStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "pos");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "1", value: 1L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "16", value: 16L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                                            V(SyntaxKind.IdentifierToken, text: "i");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
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
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "unpack");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"i4\"", value: ImmutableArray.Create("i4"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "pos");
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
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.AdditionExpression);
                                                        {
                                                            V(SyntaxKind.FloorDivisionExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "pos");
                                                                }
                                                                V(SyntaxKind.SlashSlashToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                                }
                                                            }
                                                            V(SyntaxKind.PlusToken);
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
                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.AdditionExpression);
                                                        {
                                                            V(SyntaxKind.IdentifierName);
                                                            {
                                                                V(SyntaxKind.IdentifierToken, text: "pos");
                                                            }
                                                            V(SyntaxKind.PlusToken);
                                                            V(SyntaxKind.NumericLiteralExpression);
                                                            {
                                                                V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                            }
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
                        V(SyntaxKind.ForStatement);
                        {
                            V(SyntaxKind.ForKeyword);
                            V(SyntaxKind.IdentifierName);
                            {
                                V(SyntaxKind.IdentifierToken, text: "pos");
                            }
                            V(SyntaxKind.EqualsToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "0", value: 0L);
                            }
                            V(SyntaxKind.CommaToken);
                            V(SyntaxKind.NumericLiteralExpression);
                            {
                                V(SyntaxKind.NumericLiteralToken, text: "12", value: 12L);
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
                                            V(SyntaxKind.IdentifierToken, text: "i");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
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
                                        V(SyntaxKind.InvocationExpression);
                                        {
                                            V(SyntaxKind.IdentifierName);
                                            {
                                                V(SyntaxKind.IdentifierToken, text: "unpack");
                                            }
                                            V(SyntaxKind.ArgumentList);
                                            {
                                                V(SyntaxKind.OpenParenToken);
                                                V(SyntaxKind.Argument);
                                                {
                                                    V(SyntaxKind.StringLiteralExpression);
                                                    {
                                                        V(SyntaxKind.StringLiteralToken, text: "\"!4 i4\"", value: ImmutableArray.Create("!4 i4"U8.ToArray()));
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
                                                    V(SyntaxKind.AdditionExpression);
                                                    {
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "pos");
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
                                                            V(SyntaxKind.IdentifierToken, text: "i");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.AdditionExpression);
                                                        {
                                                            V(SyntaxKind.FloorDivisionExpression);
                                                            {
                                                                V(SyntaxKind.ParenthesizedExpression);
                                                                {
                                                                    V(SyntaxKind.OpenParenToken);
                                                                    V(SyntaxKind.AdditionExpression);
                                                                    {
                                                                        V(SyntaxKind.IdentifierName);
                                                                        {
                                                                            V(SyntaxKind.IdentifierToken, text: "pos");
                                                                        }
                                                                        V(SyntaxKind.PlusToken);
                                                                        V(SyntaxKind.NumericLiteralExpression);
                                                                        {
                                                                            V(SyntaxKind.NumericLiteralToken, text: "3", value: 3L);
                                                                        }
                                                                    }
                                                                    V(SyntaxKind.CloseParenToken);
                                                                }
                                                                V(SyntaxKind.SlashSlashToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
                                                                }
                                                            }
                                                            V(SyntaxKind.PlusToken);
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
                                                            V(SyntaxKind.IdentifierToken, text: "p");
                                                        }
                                                        V(SyntaxKind.EqualsEqualsToken);
                                                        V(SyntaxKind.AdditionExpression);
                                                        {
                                                            V(SyntaxKind.MultiplicationExpression);
                                                            {
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                                V(SyntaxKind.AsteriskToken);
                                                                V(SyntaxKind.NumericLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                            V(SyntaxKind.CommaToken);
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
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!4 i4\"", value: ImmutableArray.Create("!4 i4"U8.ToArray()));
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
                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "17", value: 17L);
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
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                            }
                            V(SyntaxKind.CommaToken);
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
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!4 i4\"", value: ImmutableArray.Create("!4 i4"U8.ToArray()));
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
                                            V(SyntaxKind.UnaryMinusExpression);
                                            {
                                                V(SyntaxKind.MinusToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "7", value: 7L);
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
                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                }
                                                V(SyntaxKind.EqualsEqualsToken);
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "4", value: 4L);
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
                                                V(SyntaxKind.NumericLiteralExpression);
                                                {
                                                    V(SyntaxKind.NumericLiteralToken, text: "17", value: 17L);
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
                                    V(SyntaxKind.IdentifierToken, text: "i");
                                }
                            }
                            V(SyntaxKind.CommaToken);
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
                                V(SyntaxKind.InvocationExpression);
                                {
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "unpack");
                                    }
                                    V(SyntaxKind.ArgumentList);
                                    {
                                        V(SyntaxKind.OpenParenToken);
                                        V(SyntaxKind.Argument);
                                        {
                                            V(SyntaxKind.StringLiteralExpression);
                                            {
                                                V(SyntaxKind.StringLiteralToken, text: "\"!4 i4\"", value: ImmutableArray.Create("!4 i4"U8.ToArray()));
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
                                            V(SyntaxKind.UnaryMinusExpression);
                                            {
                                                V(SyntaxKind.MinusToken);
                                                V(SyntaxKind.LengthExpression);
                                                {
                                                    V(SyntaxKind.HashToken);
                                                    V(SyntaxKind.IdentifierName);
                                                    {
                                                        V(SyntaxKind.IdentifierToken, text: "x");
                                                    }
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
                                                    V(SyntaxKind.IdentifierToken, text: "i");
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
                                                    V(SyntaxKind.IdentifierToken, text: "p");
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
                            V(SyntaxKind.AdditionExpression);
                            {
                                V(SyntaxKind.LengthExpression);
                                {
                                    V(SyntaxKind.HashToken);
                                    V(SyntaxKind.IdentifierName);
                                    {
                                        V(SyntaxKind.IdentifierToken, text: "x");
                                    }
                                }
                                V(SyntaxKind.PlusToken);
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
                                                        V(SyntaxKind.IdentifierName);
                                                        {
                                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                                        }
                                                        V(SyntaxKind.ArgumentList);
                                                        {
                                                            V(SyntaxKind.OpenParenToken);
                                                            V(SyntaxKind.Argument);
                                                            {
                                                                V(SyntaxKind.StringLiteralExpression);
                                                                {
                                                                    V(SyntaxKind.StringLiteralToken, text: "\"c0\"", value: ImmutableArray.Create("c0"U8.ToArray()));
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
                                                                V(SyntaxKind.IdentifierName);
                                                                {
                                                                    V(SyntaxKind.IdentifierToken, text: "i");
                                                                }
                                                            }
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
                                            V(SyntaxKind.StringLiteralToken, text: "\"out of string\"", value: ImmutableArray.Create("out of string"U8.ToArray()));
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.IdentifierName);
                                        {
                                            V(SyntaxKind.IdentifierToken, text: "unpack");
                                        }
                                    }
                                    V(SyntaxKind.CommaToken);
                                    V(SyntaxKind.Argument);
                                    {
                                        V(SyntaxKind.StringLiteralExpression);
                                        {
                                            V(SyntaxKind.StringLiteralToken, text: "\"c0\"", value: ImmutableArray.Create("c0"U8.ToArray()));
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
                                        V(SyntaxKind.AdditionExpression);
                                        {
                                            V(SyntaxKind.LengthExpression);
                                            {
                                                V(SyntaxKind.HashToken);
                                                V(SyntaxKind.IdentifierName);
                                                {
                                                    V(SyntaxKind.IdentifierToken, text: "x");
                                                }
                                            }
                                            V(SyntaxKind.PlusToken);
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
                            V(SyntaxKind.StringLiteralToken, text: "\"OK\"", value: ImmutableArray.Create("OK"U8.ToArray()));
                        }
                    }
                }
            }
            V(SyntaxKind.EndOfFileToken);
        }
    }
}
