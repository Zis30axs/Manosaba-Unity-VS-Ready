using Il2CppDummyDll;
using Naninovel.UI;

namespace GigaCreation.NaninovelExtender.Common;

[Token(Token = "0x2000013")]
public interface IClickThroughPanelExtended : IClickThroughPanel, IManagedUI
{
	[Token(Token = "0x17000003")]
	bool Exists
	{
		[Token(Token = "0x6000022")]
		[Address(RVA = "0xCFA5B0", Offset = "0xCF8FB0", VA = "0x180CFA5B0", Slot = "0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000023")]
	void DontBlockInteractionOnNext();

	[Token(Token = "0x6000024")]
	void SetSortingOrder(int order);
}
