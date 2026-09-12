using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20003E1")]
public interface IScriptManager : IEngineService<ScriptsConfiguration>, IEngineService
{
	[Token(Token = "0x1700028A")]
	IResourceLoader<Script> ScriptLoader
	{
		[Token(Token = "0x60012B8")]
		get;
	}

	[Token(Token = "0x1700028B")]
	IResourceLoader<Script> ExternalScriptLoader
	{
		[Token(Token = "0x60012B9")]
		get;
	}

	[Token(Token = "0x1700028C")]
	int TotalCommandsCount
	{
		[Token(Token = "0x60012BA")]
		get;
	}
}
