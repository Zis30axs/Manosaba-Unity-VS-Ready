using System.Collections.Generic;
using Naninovel.Parsing;

namespace Naninovel
{
    public delegate TLine ParseLine<TLine> (
        string lineText,
        IReadOnlyList<Token> tokens)
        where TLine : IScriptLine;
}
