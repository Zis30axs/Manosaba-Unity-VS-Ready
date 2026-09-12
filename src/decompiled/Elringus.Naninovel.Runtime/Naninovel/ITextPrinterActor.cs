using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000066")]
public interface ITextPrinterActor : IActor
{
	[Token(Token = "0x17000048")]
	IReadOnlyList<PrintedMessage> Messages
	{
		[Token(Token = "0x60001B0")]
		get;
		[Token(Token = "0x60001B1")]
		set;
	}

	[Token(Token = "0x17000049")]
	IReadOnlyList<MessageTemplate> Templates
	{
		[Token(Token = "0x60001B2")]
		get;
		[Token(Token = "0x60001B3")]
		set;
	}

	[Token(Token = "0x1700004A")]
	float RevealProgress
	{
		[Token(Token = "0x60001B4")]
		get;
		[Token(Token = "0x60001B5")]
		set;
	}

	[Token(Token = "0x60001B6")]
	void AddMessage(PrintedMessage message);

	[Token(Token = "0x60001B7")]
	void AppendText(LocalizableText text);

	[Token(Token = "0x60001B8")]
	UniTask Reveal(float delay, [Optional] AsyncToken token);
}
