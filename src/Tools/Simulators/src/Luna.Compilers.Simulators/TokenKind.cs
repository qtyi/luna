using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna.Compilers.Simulators;

public enum TokenKind
{
    None = 0,
    Keyword,
    Identifier,
    Operator,
    Punctuation,
    NumericLiteral,
    StringLiteral,
    WhiteSpace,
    NewLine,
    Comment,
    Documentation,
    Skipped
}
