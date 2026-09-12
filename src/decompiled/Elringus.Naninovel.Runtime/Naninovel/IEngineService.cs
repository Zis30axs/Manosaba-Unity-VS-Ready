using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000346")]
public interface IEngineService
{
	[Token(Token = "0x6000FCA")]
	UniTask InitializeService();

	[Token(Token = "0x6000FCB")]
	void ResetService();

	[Token(Token = "0x6000FCC")]
	void DestroyService();
}
[Token(Token = "0x2000347")]
public interface IEngineService<TConfig> : IEngineService where TConfig : Configuration
{
	[Token(Token = "0x17000229")]
	TConfig Configuration
	{
		[Token(Token = "0x6000FCD")]
		get;
	}
}
