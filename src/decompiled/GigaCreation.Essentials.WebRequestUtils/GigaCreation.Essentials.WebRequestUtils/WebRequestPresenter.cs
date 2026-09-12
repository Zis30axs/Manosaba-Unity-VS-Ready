using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.WebRequestUtils;

[Token(Token = "0x200000A")]
public class WebRequestPresenter : MonoBehaviour
{
	[Token(Token = "0x17000001")]
	protected IWebRequestManager WebRequestManager
	{
		[Token(Token = "0x6000010")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000011")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6000012")]
	[Address(RVA = "0xCF41D0", Offset = "0xCF2BD0", VA = "0x180CF41D0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0xCF43D0", Offset = "0xCF2DD0", VA = "0x180CF43D0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000014")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WebRequestPresenter()
	{
	}
}
