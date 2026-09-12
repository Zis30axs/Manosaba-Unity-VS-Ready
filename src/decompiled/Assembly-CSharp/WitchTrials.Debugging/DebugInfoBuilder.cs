using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001AD")]
public static class DebugInfoBuilder
{
	[Token(Token = "0x600076A")]
	[Address(RVA = "0x3A95E0", Offset = "0x3A7FE0", VA = "0x1803A95E0")]
	public static string Quote(string text)
	{
		return null;
	}

	[Token(Token = "0x600076B")]
	[Address(RVA = "0x3A9660", Offset = "0x3A8060", VA = "0x1803A9660")]
	public static bool TryGetPlaybackSpot(out string spot)
	{
		spot = default;
		spot = default;
		return default(bool);
	}

	[Token(Token = "0x600076C")]
	[Address(RVA = "0x3A9980", Offset = "0x3A8380", VA = "0x1803A9980")]
	public static bool TryGetPrintedMessage(out string message)
	{
		message = default;
		message = default;
		return default(bool);
	}

	[Token(Token = "0x600076D")]
	[Address(RVA = "0x3A9E00", Offset = "0x3A8800", VA = "0x1803A9E00")]
	private static string BuildMessage(PrintedMessage printed)
	{
		return null;
	}
}
