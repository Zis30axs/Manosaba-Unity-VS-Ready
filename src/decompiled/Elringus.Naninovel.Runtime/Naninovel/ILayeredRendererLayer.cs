using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20000A2")]
public interface ILayeredRendererLayer : IDisposable
{
	[Token(Token = "0x17000075")]
	string Name
	{
		[Token(Token = "0x60002F3")]
		get;
	}

	[Token(Token = "0x17000076")]
	string Group
	{
		[Token(Token = "0x60002F4")]
		get;
	}

	[Token(Token = "0x17000077")]
	bool Enabled
	{
		[Token(Token = "0x60002F5")]
		get;
		[Token(Token = "0x60002F6")]
		set;
	}

	[Token(Token = "0x17000078")]
	LayeredActorLayer Layer
	{
		[Token(Token = "0x60002F7")]
		get;
	}
}
