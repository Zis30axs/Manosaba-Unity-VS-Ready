using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.Async;

[Token(Token = "0x2000631")]
public static class UniTaskScheduler
{
	[Token(Token = "0x40017AC")]
	[FieldOffset(Offset = "0x8")]
	public static LogType UnobservedExceptionWriteLogType;

	[Token(Token = "0x40017AD")]
	[FieldOffset(Offset = "0xC")]
	public static bool DispatchUnityMainThread;

	[Token(Token = "0x40017AF")]
	[FieldOffset(Offset = "0x18")]
	private static readonly SendOrPostCallback handleExceptionInvoke;

	[Token(Token = "0x1700048F")]
	public static HashSet<Predicate<Exception>> IgnoredExceptions
	{
		[Token(Token = "0x6001E74")]
		[Address(RVA = "0x4CB5D0", Offset = "0x4C9FD0", VA = "0x1804CB5D0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x14000091")]
	public static event Action<Exception> UnobservedTaskException
	{
		[Token(Token = "0x6001E72")]
		[Address(RVA = "0x4CB310", Offset = "0x4C9D10", VA = "0x1804CB310")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001E73")]
		[Address(RVA = "0x4CB470", Offset = "0x4C9E70", VA = "0x1804CB470")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6001E75")]
	[Address(RVA = "0x4CB630", Offset = "0x4CA030", VA = "0x1804CB630")]
	internal static void PublishUnobservedTaskException(Exception ex)
	{
	}

	[Token(Token = "0x6001E76")]
	[Address(RVA = "0x4CBB10", Offset = "0x4CA510", VA = "0x1804CBB10")]
	private static void InvokeUnobservedTaskException(object state)
	{
	}
}
