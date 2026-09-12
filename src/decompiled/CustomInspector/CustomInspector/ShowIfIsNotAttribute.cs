using System;
using System.Diagnostics;
using Il2CppDummyDll;

namespace CustomInspector;

[Token(Token = "0x200003E")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class ShowIfIsNotAttribute : ShowIfIsAttribute
{
	[Token(Token = "0x6000069")]
	[Address(RVA = "0x43E360", Offset = "0x43CD60", VA = "0x18043E360")]
	public ShowIfIsNotAttribute(string fieldPath, object value)
		: base(fieldPath, value)
	{
	}
}
