using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000591")]
public class AuthorNameTMProPanel : AuthorNamePanel
{
	[Token(Token = "0x400154C")]
	[FieldOffset(Offset = "0x20")]
	private TextMeshProUGUI textCompCache;

	[Token(Token = "0x170003EC")]
	public override string Text
	{
		[Token(Token = "0x6001B98")]
		[Address(RVA = "0x492710", Offset = "0x491110", VA = "0x180492710", Slot = "4")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001B99")]
		[Address(RVA = "0x492820", Offset = "0x491220", VA = "0x180492820", Slot = "5")]
		set
		{
		}
	}

	[Token(Token = "0x170003ED")]
	public override Color TextColor
	{
		[Token(Token = "0x6001B9A")]
		[Address(RVA = "0x492950", Offset = "0x491350", VA = "0x180492950", Slot = "6")]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x6001B9B")]
		[Address(RVA = "0x492A70", Offset = "0x491470", VA = "0x180492A70", Slot = "7")]
		set
		{
		}
	}

	[Token(Token = "0x170003EE")]
	private TextMeshProUGUI TextComp
	{
		[Token(Token = "0x6001B9C")]
		[Address(RVA = "0x492B90", Offset = "0x491590", VA = "0x180492B90")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001B9D")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public AuthorNameTMProPanel()
	{
	}
}
