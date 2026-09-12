using Il2CppDummyDll;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000017")]
public interface ICancelEventBlockable
{
	[Token(Token = "0x17000009")]
	bool Exists
	{
		[Token(Token = "0x6000055")]
		[Address(RVA = "0xCDBF60", Offset = "0xCDA960", VA = "0x180CDBF60", Slot = "0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700000A")]
	bool IsCancelEventBlocked
	{
		[Token(Token = "0x6000056")]
		set;
	}
}
