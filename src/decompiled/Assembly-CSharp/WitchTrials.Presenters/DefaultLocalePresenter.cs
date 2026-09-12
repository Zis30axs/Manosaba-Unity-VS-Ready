using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using WitchTrials.Models;

namespace WitchTrials.Presenters;

[Token(Token = "0x2000149")]
public class DefaultLocalePresenter : IDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x200014A")]
	[CompilerGenerated]
	private struct _003CLoadAsync_003Ed__3
{
		[Token(Token = "0x4000657")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000658")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000659")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DefaultLocalePresenter _003C_003E4__this;

		[Token(Token = "0x400065A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400065B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600057E")]
		[Address(RVA = "0x38D3C0", Offset = "0x38BDC0", VA = "0x18038D3C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600057F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000656")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly DefaultLocaleSelector _defaultLocaleSelector;

	[Token(Token = "0x600057B")]
	[Address(RVA = "0x38D0E0", Offset = "0x38BAE0", VA = "0x18038D0E0")]
	public DefaultLocalePresenter()
	{
	}

	[Token(Token = "0x600057C")]
	[Address(RVA = "0x38D1B0", Offset = "0x38BBB0", VA = "0x18038D1B0", Slot = "4")]
	public void Dispose()
	{
	}

	[Token(Token = "0x600057D")]
	[Address(RVA = "0x38D220", Offset = "0x38BC20", VA = "0x18038D220")]
	[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__3))]
	public UniTask LoadAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}
}
