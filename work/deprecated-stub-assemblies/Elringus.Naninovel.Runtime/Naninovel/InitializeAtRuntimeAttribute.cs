using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000349")]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InitializeAtRuntimeAttribute : Attribute
    {
    	[Token(Token = "0x1700022A")]
    	public int InitializationPriority
    	{
    		[Token(Token = "0x6000FD1")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700022B")]
    	public Type Override
    	{
    		[Token(Token = "0x6000FD2")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000FD3")]
    	[Address(RVA = "0x569C10", Offset = "0x568610", VA = "0x180569C10")]
    	public InitializeAtRuntimeAttribute(int initializationPriority = 0, Type @override = default)
    	{
    	}
    }
}
