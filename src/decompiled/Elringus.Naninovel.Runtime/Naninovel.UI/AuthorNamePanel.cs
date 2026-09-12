using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000590")]
public abstract class AuthorNamePanel : MonoBehaviour
{
	[Token(Token = "0x170003EA")]
	public abstract string Text
	{
		[Token(Token = "0x6001B93")]
		get;
		[Token(Token = "0x6001B94")]
		set;
	}

	[Token(Token = "0x170003EB")]
	public abstract Color TextColor
	{
		[Token(Token = "0x6001B95")]
		get;
		[Token(Token = "0x6001B96")]
		set;
	}

	[Token(Token = "0x6001B97")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	protected AuthorNamePanel()
	{
	}
}
