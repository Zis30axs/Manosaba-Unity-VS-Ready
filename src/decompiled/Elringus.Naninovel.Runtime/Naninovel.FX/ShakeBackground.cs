using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.FX;

[Token(Token = "0x20005F4")]
public class ShakeBackground : ShakeTransform
{
	[Token(Token = "0x6001DDE")]
	[Address(RVA = "0x4BF510", Offset = "0x4BDF10", VA = "0x1804BF510", Slot = "31")]
	protected override Transform GetShakenTransform()
	{
		return null;
	}

	[Token(Token = "0x6001DDF")]
	[Address(RVA = "0x4BF720", Offset = "0x4BE120", VA = "0x1804BF720")]
	public ShakeBackground()
	{
	}
}
