using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000030")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class ReadOnlyAttribute : PropertyAttribute
{
	[Token(Token = "0x40000FE")]
	[FieldOffset(Offset = "0x18")]
	public LabelStyle labelStyle;

	[Token(Token = "0x40000FF")]
	[FieldOffset(Offset = "0x1C")]
	public DisableStyle disableStyle;

	[Token(Token = "0x6000049")]
	[Address(RVA = "0x43D630", Offset = "0x43C030", VA = "0x18043D630")]
	public ReadOnlyAttribute(DisableStyle disableStyle = DisableStyle.GreyedOut, LabelStyle labelStyle = LabelStyle.FullSpacing)
	{
	}
}
