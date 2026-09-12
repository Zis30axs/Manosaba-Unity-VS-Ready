using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20000D9")]
public class LocalesPopupAttribute : PropertyAttribute
{
	[Token(Token = "0x4000348")]
	[FieldOffset(Offset = "0x18")]
	public readonly bool IncludeEmpty;

	[Token(Token = "0x6000414")]
	[Address(RVA = "0x43D660", Offset = "0x43C060", VA = "0x18043D660")]
	public LocalesPopupAttribute(bool includeEmpty)
	{
	}
}
