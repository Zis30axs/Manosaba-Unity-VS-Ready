using Il2CppDummyDll;
using TMPro;

namespace Naninovel.UI;

[Token(Token = "0x20005B1")]
public class RevealPaginator : TextRevealEffect
{
	[Token(Token = "0x6001C5E")]
	[Address(RVA = "0x4A0930", Offset = "0x49F330", VA = "0x1804A0930")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x6001C5F")]
	[Address(RVA = "0x4A09F0", Offset = "0x49F3F0", VA = "0x1804A09F0")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x6001C60")]
	[Address(RVA = "0x4A0B70", Offset = "0x49F570", VA = "0x1804A0B70")]
	private void HandleRevealChanged()
	{
	}

	[Token(Token = "0x6001C61")]
	[Address(RVA = "0x4A0E30", Offset = "0x49F830", VA = "0x1804A0E30")]
	private int GetPageNumber(int charIndex)
	{
		return default(int);
	}

	[Token(Token = "0x6001C62")]
	[Address(RVA = "0x4A0F30", Offset = "0x49F930", VA = "0x1804A0F30")]
	private bool IsInsidePage(TMP_PageInfo page, int charIndex)
	{
		return default(bool);
	}

	[Token(Token = "0x6001C63")]
	[Address(RVA = "0x4A0F60", Offset = "0x49F960", VA = "0x1804A0F60")]
	private bool AnyVisibleCharsBeforePageBreak()
	{
		return default(bool);
	}

	[Token(Token = "0x6001C64")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public RevealPaginator()
	{
	}
}
