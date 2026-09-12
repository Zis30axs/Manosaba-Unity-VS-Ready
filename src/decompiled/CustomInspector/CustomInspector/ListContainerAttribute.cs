using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200005C")]
[Conditional("UNITY_EDITOR")]
public class ListContainerAttribute : PropertyAttribute
{
	[Token(Token = "0x600010E")]
	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
	public ListContainerAttribute()
	{
	}
}
