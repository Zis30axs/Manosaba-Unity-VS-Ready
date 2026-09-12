using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20005B9")]
public interface ITipsUI : IManagedUI
{
	[Token(Token = "0x17000435")]
	int TipsCount
	{
		[Token(Token = "0x6001CA5")]
		get;
	}

	[Token(Token = "0x6001CA6")]
	void SelectTipRecord(string tipId);
}
