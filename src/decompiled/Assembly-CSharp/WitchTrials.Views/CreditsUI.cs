using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000AA")]
[RequireComponent(typeof(CanvasGroup))]
public class CreditsUI : CustomUI, ICreditsUI, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x20000AB")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__3
{
		[Token(Token = "0x40002FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000300")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsUI _003C_003E4__this;

		[Token(Token = "0x4000301")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000276")]
		[Address(RVA = "0x348400", Offset = "0x346E00", VA = "0x180348400", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000277")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AC")]
	[CompilerGenerated]
	private struct _003CPlayAsync_003Ed__5
{
		[Token(Token = "0x4000302")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000303")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000304")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsUI _003C_003E4__this;

		[Token(Token = "0x4000305")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int act;

		[Token(Token = "0x4000306")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4000307")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000308")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000278")]
		[Address(RVA = "0x348770", Offset = "0x347170", VA = "0x180348770", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000279")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AD")]
	[CompilerGenerated]
	private struct _003CPreloadResourcesAsync_003Ed__4
{
		[Token(Token = "0x4000309")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400030A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400030B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsUI _003C_003E4__this;

		[Token(Token = "0x400030C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int act;

		[Token(Token = "0x400030D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x400030E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600027A")]
		[Address(RVA = "0x348EE0", Offset = "0x3478E0", VA = "0x180348EE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600027B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002FB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[SerializeField]
	private CreditsDirectorBase[] _creditsDirectors;

	[Token(Token = "0x40002FC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private CreditsDirectorBase _currentDirector;

	[Token(Token = "0x40002FD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private CancellationTokenSource _cts;

	// RECONSTRUCTED | Confidence: A | RVA: 0x347CB0 (builder stub); MoveNext 0x348400, 864 B
	// 
	// - The directors array is loaded once and walked by index (a foreach over an array); vtable +0x188 = slot 5 Initialize.
	// - dump.cs lists `CreditsUI.<>n__0`, a base-call helper that folds with CustomUI.Initialize (0x3483B0), so the
	//   awaited completed Naninovel UniTask is `await base.Initialize()` with the helper inlined.
	[Token(Token = "0x6000270")]
	[Address(RVA = "0x347CB0", Offset = "0x3466B0", VA = "0x180347CB0", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__3))]
	public override async Naninovel.UniTask Initialize()
	{
		CreditsDirectorBase[] creditsDirectors = _creditsDirectors;
		foreach (CreditsDirectorBase creditsDirector in creditsDirectors)
		{
			creditsDirector.Initialize();
		}
		await base.Initialize();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x347DD0 (builder stub); MoveNext 0x348EE0, 732 B
	// `_creditsDirectors[act - 1]` (array bounds check only) -> vtable +0x198 slot 6 with the token's first field
	// (`AsyncToken.CancellationToken`, +0x0).
	[Token(Token = "0x6000271")]
	[Address(RVA = "0x347DD0", Offset = "0x3467D0", VA = "0x180347DD0", Slot = "95")]
	[AsyncStateMachine(typeof(_003CPreloadResourcesAsync_003Ed__4))]
	public async Naninovel.UniTask PreloadResourcesAsync(int act, [Optional] AsyncToken token)
	{
		await _creditsDirectors[act - 1].PreloadResourcesAsync(token.CancellationToken);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x347F80 (builder stub); MoveNext 0x348770, 1896 B
	// 
	// - A running `_cts` (+0xF0) is Cancel()ed and Dispose()d. When `act` is outside 1..Length, `_cts` is set to null and
	//   a warning ("章番号に該当するクレジット演出がありません：{0}", the act boxed) is logged before returning; this
	//   happens before the try region.
	// - `ChangeVisibility(true)` is ScriptableUIBehaviour slot 44 (vtable +0x3F8) with a null duration and a default token,
	//   then Naninovel `Forget()`.
	// - `<linkedCts>5__2` links the new `_cts` with `token.CancellationToken`; the finally disposes it.
	// - Director `PlayAsync` is vtable +0x1A8 (slot 7). After the await `_currentDirector` is cleared inside the using.
	[Token(Token = "0x6000272")]
	[Address(RVA = "0x347F80", Offset = "0x346980", VA = "0x180347F80", Slot = "96")]
	[AsyncStateMachine(typeof(_003CPlayAsync_003Ed__5))]
	public async Naninovel.UniTask PlayAsync(int act, [Optional] AsyncToken token)
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
		}
		if (act < 1 || act > _creditsDirectors.Length)
		{
			_cts = null;
			UnityEngine.Debug.LogWarning(string.Format("章番号に該当するクレジット演出がありません：{0}", act));
			return;
		}
		_currentDirector = _creditsDirectors[act - 1];
		ChangeVisibility(true).Forget();
		_cts = new CancellationTokenSource();
		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, token.CancellationToken);
		await _currentDirector.PlayAsync(linkedCts.Token);
		_currentDirector = null;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x348130, 634 B
	// 
	// - `Cancel()` is inlined here (disposed check, then NotifyCancellation(false)).
	// - `_currentDirector != null` is the inlined UnityEngine.Object inequality (reference plus m_CachedPtr at +0x10);
	//   Stop is vtable +0x1B8 (slot 8).
	// - The fade-out is `ChangeVisibility(false)` (slot 44) with Naninovel `Forget()` as a tail call.
	[Token(Token = "0x6000273")]
	[Address(RVA = "0x348130", Offset = "0x346B30", VA = "0x180348130", Slot = "97")]
	public void Stop()
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
			_cts = null;
		}
		if (_currentDirector != null)
		{
			_currentDirector.Stop();
		}
		_currentDirector = null;
		ChangeVisibility(false).Forget();
	}

	[Token(Token = "0x6000274")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public CreditsUI()
	{
	}
}
