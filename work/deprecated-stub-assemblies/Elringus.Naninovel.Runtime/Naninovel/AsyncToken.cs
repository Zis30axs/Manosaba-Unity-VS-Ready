using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000127")]
    public readonly struct AsyncToken : IEquatable<AsyncToken>
    {
    	[Token(Token = "0x170000EB")]
    	public CancellationToken CancellationToken
    	{
    		[Token(Token = "0x6000580")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		[CompilerGenerated]
    		get
    		{
    			return default(CancellationToken);
    		}
    	}

    	[Token(Token = "0x170000EC")]
    	public CancellationToken CompletionToken
    	{
    		[Token(Token = "0x6000581")]
    		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
    		[CompilerGenerated]
    		get
    		{
    			return default(CancellationToken);
    		}
    	}

    	[Token(Token = "0x170000ED")]
    	public bool Canceled
    	{
    		[Token(Token = "0x6000582")]
    		[Address(RVA = "0x510A50", Offset = "0x50F450", VA = "0x180510A50")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170000EE")]
    	public bool Completed
    	{
    		[Token(Token = "0x6000583")]
    		[Address(RVA = "0x510AC0", Offset = "0x50F4C0", VA = "0x180510AC0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6000584")]
    	[Address(RVA = "0x510B80", Offset = "0x50F580", VA = "0x180510B80")]
    	public AsyncToken(CancellationToken cancellationToken, bool completion = false)
    	{ this = default; }

    	[Token(Token = "0x6000585")]
    	[Address(RVA = "0x457060", Offset = "0x455A60", VA = "0x180457060")]
    	public AsyncToken(CancellationToken cancellationToken, CancellationToken completionToken)
    	{ this = default; }

    	[Token(Token = "0x6000586")]
    	[Address(RVA = "0x510C40", Offset = "0x50F640", VA = "0x180510C40")]
    	public void ThrowIfCanceled([Optional] UnityEngine.Object obj)
    	{
    	}

    	[Token(Token = "0x6000587")]
    	[Address(RVA = "0x510D90", Offset = "0x50F790", VA = "0x180510D90")]
    	public bool EnsureNotCanceled([Optional] UnityEngine.Object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000588")]
    	[Address(RVA = "0x510DB0", Offset = "0x50F7B0", VA = "0x180510DB0")]
    	public bool EnsureNotCanceledOrCompleted([Optional] UnityEngine.Object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000589")]
    	[Address(RVA = "0x510DE0", Offset = "0x50F7E0", VA = "0x180510DE0")]
    	public CancellationTokenSource CreateLinkedTokenSource()
    	{
    		return null;
    	}

    	[Token(Token = "0x600058A")]
    	[Address(RVA = "0x510E50", Offset = "0x50F850", VA = "0x180510E50")]
    	public static implicit operator AsyncToken(CancellationToken token)
    	{
    		return default(AsyncToken);
    	}

    	[Token(Token = "0x600058B")]
    	[Address(RVA = "0x510EB0", Offset = "0x50F8B0", VA = "0x180510EB0")]
    	public static bool operator ==(AsyncToken left, AsyncToken right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600058C")]
    	[Address(RVA = "0x510ED0", Offset = "0x50F8D0", VA = "0x180510ED0")]
    	public static bool operator !=(AsyncToken left, AsyncToken right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600058D")]
    	[Address(RVA = "0x510F90", Offset = "0x50F990", VA = "0x180510F90", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600058E")]
    	[Address(RVA = "0x511030", Offset = "0x50FA30", VA = "0x180511030", Slot = "4")]
    	public bool Equals(AsyncToken other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600058F")]
    	[Address(RVA = "0x5110E0", Offset = "0x50FAE0", VA = "0x1805110E0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
