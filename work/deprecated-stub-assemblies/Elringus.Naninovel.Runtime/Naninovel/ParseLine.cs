using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x2000414")]
    public delegate TLine ParseLine<TLine>(string lineText, IReadOnlyList<Token> tokens) where TLine : IScriptLine;
}
