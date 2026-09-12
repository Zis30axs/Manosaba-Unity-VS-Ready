using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200005D")]
[Conditional("UNITY_EDITOR")]
public class MessageDrawerAttribute : PropertyAttribute
{
	[Token(Token = "0x600010F")]
	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
	public MessageDrawerAttribute()
	{
	}
}
