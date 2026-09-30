using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000D4")]
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class DocAttribute : Attribute
    {
    	[Token(Token = "0x400033E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	public readonly string Summary;

    	[Token(Token = "0x400033F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	public readonly string Remarks;

    	[Token(Token = "0x4000340")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	public readonly string[] Examples;

    	[Token(Token = "0x6000410")]
    	[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
    	public DocAttribute(string summary, [Optional] string remarks, params string[] examples)
    	{
    	}
    }
}
