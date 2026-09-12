using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000348")]
public interface ILogger
{
	[Token(Token = "0x6000FCE")]
	void Log(string message);

	[Token(Token = "0x6000FCF")]
	void Warn(string message);

	[Token(Token = "0x6000FD0")]
	void Err(string message);
}
