using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000032")]
public class CharacterLipSyncer : IDisposable
{
	[Token(Token = "0x40000A8")]
	[FieldOffset(Offset = "0x18")]
	private readonly string authorId;

	[Token(Token = "0x40000A9")]
	[FieldOffset(Offset = "0x20")]
	private readonly Action<bool> setIsSpeaking;

	[Token(Token = "0x40000AA")]
	[FieldOffset(Offset = "0x28")]
	private readonly ITextPrinterManager textPrinterManager;

	[Token(Token = "0x40000AB")]
	[FieldOffset(Offset = "0x30")]
	private readonly IAudioManager audioManager;

	[Token(Token = "0x17000019")]
	public bool SyncAllowed
	{
		[Token(Token = "0x60000C0")]
		[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60000C1")]
		[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x60000C2")]
	[Address(RVA = "0x44D330", Offset = "0x44BD30", VA = "0x18044D330")]
	public CharacterLipSyncer(string authorId, Action<bool> setIsSpeaking)
	{
	}

	[Token(Token = "0x60000C3")]
	[Address(RVA = "0x44D5B0", Offset = "0x44BFB0", VA = "0x18044D5B0", Slot = "4")]
	public void Dispose()
	{
	}

	[Token(Token = "0x60000C4")]
	[Address(RVA = "0x44D6B0", Offset = "0x44C0B0", VA = "0x18044D6B0")]
	private void HandlePrintStarted(PrintMessageArgs args)
	{
	}

	[Token(Token = "0x60000C5")]
	[Address(RVA = "0x44D9F0", Offset = "0x44C3F0", VA = "0x18044D9F0")]
	private void HandlePrintFinished(PrintMessageArgs args)
	{
	}

	[Token(Token = "0x60000C6")]
	[Address(RVA = "0x44DBF0", Offset = "0x44C5F0", VA = "0x18044DBF0")]
	private void HandleVoiceClipStopped()
	{
	}
}
