using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel;

[Token(Token = "0x200040C")]
public class CommentLineParser : ScriptLineParser<CommentScriptLine, CommentLine>
{
	[Token(Token = "0x600139D")]
	[Address(RVA = "0x5AF020", Offset = "0x5ADA20", VA = "0x1805AF020", Slot = "9")]
	protected override CommentScriptLine Parse(CommentLine lineModel)
	{
		return null;
	}

	[Token(Token = "0x600139E")]
	[Address(RVA = "0x5AF180", Offset = "0x5ADB80", VA = "0x1805AF180")]
	public CommentLineParser()
	{
	}
}
