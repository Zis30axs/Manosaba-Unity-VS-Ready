using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200001E")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class HookAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000D4")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string methodPath;

    	[Token(Token = "0x40000D5")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly ExecutionTarget target;

    	[Token(Token = "0x40000D6")]
    	[FieldOffset(Offset = "0x24")]
    	public bool useHookOnly;

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0x43CB00", Offset = "0x43B500", VA = "0x18043CB00")]
    	public HookAttribute(string methodPath, ExecutionTarget target = ExecutionTarget.Always)
    	{
    	}
    }
}
