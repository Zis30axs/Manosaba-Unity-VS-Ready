using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000306")]
    public static class SyncContextUtils
    {
    	[Token(Token = "0x170001F0")]
    	public static SynchronizationContext UnitySynchronizationContext
    	{
    		[Token(Token = "0x6000E7A")]
    		[Address(RVA = "0x553B60", Offset = "0x552560", VA = "0x180553B60")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000E7B")]
    		[Address(RVA = "0x553BA0", Offset = "0x5525A0", VA = "0x180553BA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6000E7C")]
    	[Address(RVA = "0x553C40", Offset = "0x552640", VA = "0x180553C40")]
    	public static void InvokeOnUnityThread(Action action)
    	{
    	}

    	[Token(Token = "0x6000E7D")]
    	[Address(RVA = "0x553E00", Offset = "0x552800", VA = "0x180553E00")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    	private static void Initialize()
    	{
    	}
    }
}
