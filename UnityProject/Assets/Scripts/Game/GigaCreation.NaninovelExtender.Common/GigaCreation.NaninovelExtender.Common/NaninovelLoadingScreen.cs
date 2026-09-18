using System;
using System.Runtime.CompilerServices;
using System.Threading;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x200001C")]
    public class NaninovelLoadingScreen : IDisposable
    {
    	[Token(Token = "0x17000006")]
    	private IInputManager NaniInputManager
    	{
    		[Token(Token = "0x600003B")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000007")]
    	private ILoadingUiExtended LoadingUiExtended
    	{
    		[Token(Token = "0x600003C")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000008")]
    	private bool IsInputProcessingHalted
    	{
    		[Token(Token = "0x600003D")]
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600003E")]
    		[Address(RVA = "0x468AD0", Offset = "0x4674D0", VA = "0x180468AD0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000009")]
    	private ICancelEventBlockable[] Blockables
    	{
    		[Token(Token = "0x600003F")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000040")]
    		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	private CancellationTokenSource Cts
    	{
    		[Token(Token = "0x6000041")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000042")]
    		[Address(RVA = "0x58DF10", Offset = "0x58C910", VA = "0x18058DF10")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000043")]
    	[Address(RVA = "0xCFC550", Offset = "0xCFAF50", VA = "0x180CFC550")]
    	private NaninovelLoadingScreen()
    	{
    	}

    	[Token(Token = "0x6000044")]
    	[Address(RVA = "0xCFC960", Offset = "0xCFB360", VA = "0x180CFC960", Slot = "4")]
    	public void Dispose()
    	{
    	}

    	[Token(Token = "0x6000045")]
    	[Address(RVA = "0xCFCED0", Offset = "0xCFB8D0", VA = "0x180CFCED0")]
    	public static IDisposable Show(LoadingMode mode, bool haltInput, params ICancelEventBlockable[] blockables)
    	{
    		return null;
    	}
    }
}
