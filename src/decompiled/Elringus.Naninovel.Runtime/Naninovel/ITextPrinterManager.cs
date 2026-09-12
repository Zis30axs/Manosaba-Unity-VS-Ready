using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000067")]
public interface ITextPrinterManager : IActorManager<ITextPrinterActor, TextPrinterState, TextPrinterMetadata, TextPrintersConfiguration>, IActorManager, IEngineService, IEngineService<TextPrintersConfiguration>, IStatefulService<GameStateMap>
{
	[Token(Token = "0x1700004B")]
	string DefaultPrinterId
	{
		[Token(Token = "0x60001BD")]
		get;
		[Token(Token = "0x60001BE")]
		set;
	}

	[Token(Token = "0x1700004C")]
	float BaseRevealSpeed
	{
		[Token(Token = "0x60001BF")]
		get;
		[Token(Token = "0x60001C0")]
		set;
	}

	[Token(Token = "0x1700004D")]
	float BaseAutoDelay
	{
		[Token(Token = "0x60001C1")]
		get;
		[Token(Token = "0x60001C2")]
		set;
	}

	[Token(Token = "0x14000007")]
	event Action<PrintMessageArgs> OnPrintStarted;

	[Token(Token = "0x14000008")]
	event Action<PrintMessageArgs> OnPrintFinished;

	[Token(Token = "0x60001C3")]
	UniTask Print(string printerId, PrintedMessage message, bool append = false, float speed = 1f, AsyncToken token = default);
}
