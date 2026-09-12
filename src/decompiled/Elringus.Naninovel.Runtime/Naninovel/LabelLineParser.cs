using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel;

[Token(Token = "0x2000410")]
public class LabelLineParser : ScriptLineParser<LabelScriptLine, LabelLine>
{
	[Token(Token = "0x60013B7")]
	[Address(RVA = "0x5B1F10", Offset = "0x5B0910", VA = "0x1805B1F10", Slot = "9")]
	protected override LabelScriptLine Parse(LabelLine lineModel)
	{
		return null;
	}

	[Token(Token = "0x60013B8")]
	[Address(RVA = "0x5B2070", Offset = "0x5B0A70", VA = "0x1805B2070")]
	public LabelLineParser()
	{
	}
}
