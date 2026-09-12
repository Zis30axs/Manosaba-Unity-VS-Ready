using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Models;

[Token(Token = "0x200001B")]
public class ManualMotionSkipper : IManualMotionSkipper, IDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x200001C")]
	[CompilerGenerated]
	private struct _003CUpdateAsync_003Ed__5
{
		[Token(Token = "0x4000060")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000061")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000062")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ManualMotionSkipper _003C_003E4__this;

		[Token(Token = "0x4000063")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool ignoreTimeScale;

		[Token(Token = "0x4000064")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000056")]
		[Address(RVA = "0xCCF5D0", Offset = "0xCCDFD0", VA = "0x180CCF5D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000057")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400005E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly CancellationTokenSource _cts;

	[Token(Token = "0x400005F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private bool _started;

	[Token(Token = "0x6000052")]
	[Address(RVA = "0xCCF120", Offset = "0xCCDB20", VA = "0x180CCF120")]
	public ManualMotionSkipper()
	{
	}

	[Token(Token = "0x6000053")]
	[Address(RVA = "0xCCF220", Offset = "0xCCDC20", VA = "0x180CCF220", Slot = "5")]
	public void Dispose()
	{
	}

	[Token(Token = "0x6000054")]
	[Address(RVA = "0xCCF2A0", Offset = "0xCCDCA0", VA = "0x180CCF2A0", Slot = "4")]
	public void StartUpdate(float startTime, bool ignoreTimeScale = false)
	{
	}

	[Token(Token = "0x6000055")]
	[Address(RVA = "0xCCF480", Offset = "0xCCDE80", VA = "0x180CCF480")]
	[AsyncStateMachine(typeof(_003CUpdateAsync_003Ed__5))]
	private UniTask UpdateAsync(bool ignoreTimeScale = false)
	{
		return default(UniTask);
	}
}
