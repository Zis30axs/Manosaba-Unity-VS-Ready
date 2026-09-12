using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200035A")]
public class UnityLogger : ILogger
{
	[Token(Token = "0x6001012")]
	[Address(RVA = "0x56F190", Offset = "0x56DB90", VA = "0x18056F190", Slot = "4")]
	public void Log(string message)
	{
	}

	[Token(Token = "0x6001013")]
	[Address(RVA = "0x56F1E0", Offset = "0x56DBE0", VA = "0x18056F1E0", Slot = "5")]
	public void Warn(string message)
	{
	}

	[Token(Token = "0x6001014")]
	[Address(RVA = "0x56F230", Offset = "0x56DC30", VA = "0x18056F230", Slot = "6")]
	public void Err(string message)
	{
	}

	[Token(Token = "0x6001015")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public UnityLogger()
	{
	}
}
