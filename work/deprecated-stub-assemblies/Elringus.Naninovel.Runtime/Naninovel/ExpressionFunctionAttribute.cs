using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000D6")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class ExpressionFunctionAttribute : Attribute
    {
    	[Token(Token = "0x4000346")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	public readonly string Alias;

    	[Token(Token = "0x6000411")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public ExpressionFunctionAttribute([Optional] string alias)
    	{
    	}
    }
}
