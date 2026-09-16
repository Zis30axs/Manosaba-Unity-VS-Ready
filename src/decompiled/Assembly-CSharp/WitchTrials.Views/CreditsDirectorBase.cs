using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using Naninovel;
using System;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000A8")]
public abstract class CreditsDirectorBase : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20000A9")]
	[CompilerGenerated]
	private struct _003CWaitUnitsAsync_003Ed__18
{
		[Token(Token = "0x40002F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsDirectorBase _003C_003E4__this;

		[Token(Token = "0x40002F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int count;

		[Token(Token = "0x40002F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40002FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600026E")]
		[Address(RVA = "0x3478F0", Offset = "0x3462F0", VA = "0x1803478F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600026F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002EF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private GameObject _gameObject;

	[Token(Token = "0x40002F0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x40002F1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private float _bgmBpm;

	[Token(Token = "0x40002F2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private string _endLabelName;

	[Token(Token = "0x40002F3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x17000046")]
	private IScriptPlayer NaniScriptPlayer
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x347330, 186 B (`??= Engine.GetServiceOrErr<T>()`, +0x40)
		[Token(Token = "0x6000261")]
		[Address(RVA = "0x347330", Offset = "0x345D30", VA = "0x180347330")]
		get
		{
			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3473F0 get (`movss xmm0,[rcx+0x48]; ret`), 0x347400 set (`movss [rcx+0x48],xmm1; ret`)
	// Auto-property with a compiler-generated backing field at +0x48 (both accessors are [CompilerGenerated]).
	[Token(Token = "0x17000047")]
	protected float BgmUnitDuration
	{
		[Token(Token = "0x6000262")]
		[Address(RVA = "0x3473F0", Offset = "0x345DF0", VA = "0x1803473F0")]
		[CompilerGenerated]
		get;
		[Token(Token = "0x6000263")]
		[Address(RVA = "0x347400", Offset = "0x345E00", VA = "0x180347400")]
		[CompilerGenerated]
		private set;
	}

	[Token(Token = "0x17000048")]
	protected string EndLabelName
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x347410 (shared `mov rax,[rcx+0x38]; ret`, `_endLabelName`)
		[Token(Token = "0x6000264")]
		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410")]
		get
		{
			return _endLabelName;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x347420, 260 B
	[Token(Token = "0x6000265")]
	[Address(RVA = "0x347420", Offset = "0x345E20", VA = "0x180347420", Slot = "4")]
	protected virtual void Reset()
	{
		_gameObject = base.gameObject;
		_canvas = GetComponent<Canvas>();
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (ChangeActivity call vs. direct field writes) | RVA: 0x347530, 112 B
	// 
	// - `_gameObject.SetActive(false); _canvas.enabled = false;` is the body of `ChangeActivity(false)`; Act1 inlines
	//   the same pair where it cannot touch the private fields, so the call form is used here too.
	// - sub_331670 is a separately compiled `Mathf.Approximately` (Mathf.Epsilon * 8 vs 1e-6 * max(|a|,|b|)).
	// - One BGM unit (a beat) = 60 / BPM seconds; a BPM of 0 falls back to 1 s (float 1.0 at data 0x2AE2820, 60.0 at 0x2AE2B9C).
	[Token(Token = "0x6000266")]
	[Address(RVA = "0x347530", Offset = "0x345F30", VA = "0x180347530", Slot = "5")]
	public virtual void Initialize()
	{
		ChangeActivity(false);
		BgmUnitDuration = Mathf.Approximately(_bgmBpm, 0f) ? 1f : (60f / _bgmBpm);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3475B0, 96 B
	// Copies the Cysharp `UniTask` static field at +0x10 of the statics block, i.e. `UniTask.CompletedTask`.
	[Token(Token = "0x6000267")]
	[Address(RVA = "0x3475B0", Offset = "0x345FB0", VA = "0x1803475B0", Slot = "6")]
	public virtual Cysharp.Threading.Tasks.UniTask PreloadResourcesAsync([Optional] CancellationToken ct)
	{
		return Cysharp.Threading.Tasks.UniTask.CompletedTask;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x347610, 68 B
	[Token(Token = "0x6000268")]
	[Address(RVA = "0x347610", Offset = "0x346010", VA = "0x180347610")]
	protected void ChangeActivity(bool enable)
	{
		_gameObject.SetActive(enable);
		_canvas.enabled = enable;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x347660, 214 B
	// The NaniScriptPlayer getter is inlined, then a tail call to Naninovel `ScriptPlayerExtensions.ResumeAtLabel(player, label)`.
	[Token(Token = "0x6000269")]
	[Address(RVA = "0x347660", Offset = "0x346060", VA = "0x180347660")]
	protected void ResumeAtLabel(string labelName)
	{
		NaniScriptPlayer.ResumeAtLabel(labelName);
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (Delay argument spelling) | RVA: 0x347740 (builder stub); MoveNext 0x3478F0, 949 B
	// 
	// - `(float)count * BgmUnitDuration` widened to double -> `TimeSpan.FromSeconds` (inlined as `TimeSpan.Interval(v, 1000)`).
	// - `UniTask.Delay` overload 0x180D710 (the DelayType one) with DelayType 0 (DeltaTime), timing 8 (Update), ct and
	//   cancelImmediately false. The `bool ignoreTimeScale` overload is a separate call target (0x180D530), so the source
	//   passed a DelayType explicitly.
	[Token(Token = "0x600026A")]
	[Address(RVA = "0x347740", Offset = "0x346140", VA = "0x180347740")]
	[AsyncStateMachine(typeof(_003CWaitUnitsAsync_003Ed__18))]
	protected async Cysharp.Threading.Tasks.UniTask WaitUnitsAsync(int count, [Optional] CancellationToken ct)
	{
		await Cysharp.Threading.Tasks.UniTask.Delay(TimeSpan.FromSeconds(count * BgmUnitDuration), Cysharp.Threading.Tasks.DelayType.DeltaTime, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, ct);
	}

	[Token(Token = "0x600026B")]
	public abstract Cysharp.Threading.Tasks.UniTask PlayAsync([Optional] CancellationToken ct);

	[Token(Token = "0x600026C")]
	public abstract void Stop();

	[Token(Token = "0x600026D")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	protected CreditsDirectorBase()
	{
	}
}
