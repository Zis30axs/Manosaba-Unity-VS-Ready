using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001BF")]
[UsedImplicitly]
public class BlockInteraction : Command
{
	[StructLayout(3)]
	[Token(Token = "0x20001C0")]
	[CompilerGenerated]
	private struct _003CBlockAsync_003Ed__3
{
		[Token(Token = "0x400084A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400084B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400084C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public BlockInteraction _003C_003E4__this;

		[Token(Token = "0x400084D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400084E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x400084F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private float _003CstartingTime_003E5__3;

		[Token(Token = "0x4000850")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		private float _003Cduration_003E5__4;

		[Token(Token = "0x4000851")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007A6")]
		[Address(RVA = "0x3B24B0", Offset = "0x3B0EB0", VA = "0x1803B24B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007A7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000848")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x4000849")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	[Token(Token = "0x60007A3")]
	[Address(RVA = "0x3B2260", Offset = "0x3B0C60", VA = "0x1803B2260", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60007A4")]
	[Address(RVA = "0x3B2310", Offset = "0x3B0D10", VA = "0x1803B2310")]
	[AsyncStateMachine(typeof(_003CBlockAsync_003Ed__3))]
	private UniTask BlockAsync(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60007A5")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public BlockInteraction()
	{
	}
}
