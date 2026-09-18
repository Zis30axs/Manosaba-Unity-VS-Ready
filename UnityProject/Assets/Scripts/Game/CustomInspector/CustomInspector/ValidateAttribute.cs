using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200004B")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ValidateAttribute : PropertyAttribute
    {
    	[Token(Token = "0x400013E")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string methodPath;

    	[Token(Token = "0x400013F")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly string errorMessage;

    	[Token(Token = "0x4000140")]
    	[FieldOffset(Offset = "0x28")]
    	public MessageBoxType errorType;

    	[Token(Token = "0x6000078")]
    	[Address(RVA = "0x43E890", Offset = "0x43D290", VA = "0x18043E890")]
    	public ValidateAttribute(string methodPath, string errorMessage = "Value is not valid", MessageBoxType errorType = MessageBoxType.Error)
    	{
    	}
    }
}
