using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000023")]
public interface IErrorHandler
{
	[Token(Token = "0x6000092")]
	void HandleError(ParseError error);
}
