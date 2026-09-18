using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000061")]
    [Conditional("UNITY_EDITOR")]
    public class SerializableDateTimeAttribute : PropertyAttribute
    {
    	[Token(Token = "0x4000169")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly SerializableDateTime.InspectorFormat format;

    	[Token(Token = "0x6000129")]
    	[Address(RVA = "0x4406E0", Offset = "0x43F0E0", VA = "0x1804406E0")]
    	public SerializableDateTimeAttribute(SerializableDateTime.InspectorFormat format)
    	{
    	}
    }
}
