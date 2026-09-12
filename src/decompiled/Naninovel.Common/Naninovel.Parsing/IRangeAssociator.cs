using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000024")]
public interface IRangeAssociator
{
	[Token(Token = "0x6000093")]
	void Associate(ILineComponent component, InlineRange range);
}
