using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Core;

[Token(Token = "0x200000A")]
public class NaninovelPresenter : MonoBehaviour
{
	[Token(Token = "0x4000017")]
	[FieldOffset(Offset = "0x20")]
	private INaninovelManager _naninovelManager;

	[Token(Token = "0x6000014")]
	[Address(RVA = "0xD03A20", Offset = "0xD02420", VA = "0x180D03A20", Slot = "4")]
	protected virtual void Awake()
	{
	}

	[Token(Token = "0x6000015")]
	[Address(RVA = "0xD03AF0", Offset = "0xD024F0", VA = "0x180D03AF0", Slot = "5")]
	protected virtual void OnDestroy()
	{
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public NaninovelPresenter()
	{
	}
}
