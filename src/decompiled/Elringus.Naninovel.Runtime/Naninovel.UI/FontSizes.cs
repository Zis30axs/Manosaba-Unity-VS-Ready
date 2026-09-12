using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x200058A")]
[CreateAssetMenu]
public class FontSizes : ScriptableObject
{
	[Token(Token = "0x400152D")]
	[FieldOffset(Offset = "0x18")]
	public int[] Sizes;

	[Token(Token = "0x6001B6E")]
	[Address(RVA = "0x48E610", Offset = "0x48D010", VA = "0x18048E610")]
	public FontSizes()
	{
	}
}
