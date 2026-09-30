using System;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.Internal;
using UnityEngine;
using UnityEngine.LowLevel;

namespace Naninovel.Async
{
    [Token(Token = "0x200062D")]
    public static class PlayerLoopHelper
    {
    	[Token(Token = "0x400179D")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly ContinuationQueue ThrowMarkerContinuationQueue;

    	[Token(Token = "0x400179E")]
    	[FieldOffset(Offset = "0x8")]
    	private static readonly PlayerLoopRunner ThrowMarkerPlayerLoopRunner;

    	[Token(Token = "0x400179F")]
    	[FieldOffset(Offset = "0x10")]
    	private static int mainThreadId;

    	[Token(Token = "0x40017A0")]
    	[FieldOffset(Offset = "0x18")]
    	private static string applicationDataPath;

    	[Token(Token = "0x40017A1")]
    	[FieldOffset(Offset = "0x20")]
    	private static SynchronizationContext unitySynchronizationContext;

    	[Token(Token = "0x40017A2")]
    	[FieldOffset(Offset = "0x28")]
    	private static ContinuationQueue[] yielders;

    	[Token(Token = "0x40017A3")]
    	[FieldOffset(Offset = "0x30")]
    	private static PlayerLoopRunner[] runners;

    	[Token(Token = "0x1700048B")]
    	public static SynchronizationContext UnitySynchronizationContext
    	{
    		[Token(Token = "0x6001E5A")]
    		[Address(RVA = "0x4C7890", Offset = "0x4C6290", VA = "0x1804C7890")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700048C")]
    	public static int MainThreadId
    	{
    		[Token(Token = "0x6001E5B")]
    		[Address(RVA = "0x4C78F0", Offset = "0x4C62F0", VA = "0x1804C78F0")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700048D")]
    	internal static string ApplicationDataPath
    	{
    		[Token(Token = "0x6001E5C")]
    		[Address(RVA = "0x4C7950", Offset = "0x4C6350", VA = "0x1804C7950")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700048E")]
    	public static bool IsMainThread
    	{
    		[Token(Token = "0x6001E5D")]
    		[Address(RVA = "0x4C79B0", Offset = "0x4C63B0", VA = "0x1804C79B0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001E5E")]
    	[Address(RVA = "0x4C7A70", Offset = "0x4C6470", VA = "0x1804C7A70")]
    	private static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem loopSystem, bool injectOnFirst, Type loopRunnerYieldType, ContinuationQueue cq, Type loopRunnerType, PlayerLoopRunner runner)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001E5F")]
    	[Address(RVA = "0x4C7FD0", Offset = "0x4C69D0", VA = "0x1804C7FD0")]
    	private static PlayerLoopSystem[] RemoveRunner(PlayerLoopSystem loopSystem, Type loopRunnerYieldType, Type loopRunnerType)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001E60")]
    	[Address(RVA = "0x4C81F0", Offset = "0x4C6BF0", VA = "0x1804C81F0")]
    	private static PlayerLoopSystem[] InsertUniTaskSynchronizationContext(PlayerLoopSystem loopSystem)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001E61")]
    	[Address(RVA = "0x4C89B0", Offset = "0x4C73B0", VA = "0x1804C89B0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    	private static void Init()
    	{
    	}

    	[Token(Token = "0x6001E62")]
    	[Address(RVA = "0x4C8C80", Offset = "0x4C7680", VA = "0x1804C8C80")]
    	private static int FindLoopSystemIndex(PlayerLoopSystem[] playerLoopList, Type systemType)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001E63")]
    	[Address(RVA = "0x4C8D80", Offset = "0x4C7780", VA = "0x1804C8D80")]
    	private static void InsertLoop(PlayerLoopSystem[] copyList, InjectPlayerLoopTimings injectTimings, Type loopType, InjectPlayerLoopTimings targetTimings, int index, bool injectOnFirst, Type loopRunnerYieldType, Type loopRunnerType, PlayerLoopTiming playerLoopTiming)
    	{
    	}

    	[Token(Token = "0x6001E64")]
    	[Address(RVA = "0x4C91D0", Offset = "0x4C7BD0", VA = "0x1804C91D0")]
    	public static void Initialize(ref PlayerLoopSystem playerLoop, InjectPlayerLoopTimings injectTimings = InjectPlayerLoopTimings.All)
    	{
    	}

    	[Token(Token = "0x6001E65")]
    	[Address(RVA = "0x4CA6B0", Offset = "0x4C90B0", VA = "0x1804CA6B0")]
    	public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
    	{
    	}

    	[Token(Token = "0x6001E66")]
    	[Address(RVA = "0x4CA750", Offset = "0x4C9150", VA = "0x1804CA750")]
    	private static void ThrowInvalidLoopTiming(PlayerLoopTiming playerLoopTiming)
    	{
    	}

    	[Token(Token = "0x6001E67")]
    	[Address(RVA = "0x4CA7F0", Offset = "0x4C91F0", VA = "0x1804CA7F0")]
    	public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
    	{
    	}

    	[Token(Token = "0x6001E68")]
    	[Address(RVA = "0x4CA890", Offset = "0x4C9290", VA = "0x1804CA890")]
    	public static void DumpCurrentPlayerLoop()
    	{
    	}

    	[Token(Token = "0x6001E69")]
    	[Address(RVA = "0x4CACD0", Offset = "0x4C96D0", VA = "0x1804CACD0")]
    	public static bool IsInjectedUniTaskPlayerLoop()
    	{
    		return default(bool);
    	}
    }
}
