using GigaCreation.Tools.Service;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000A2")]
    public sealed class CreditsDirectorAct2 : CreditsDirectorBase
    {
    	[Token(Token = "0x20000A4")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass17_0
    	{
    		[Token(Token = "0x40002D9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public CancellationToken ct;

    		[Token(Token = "0x6000259")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass17_0()
    		{
    		}

    		[Token(Token = "0x600025A")]
    		[Address(RVA = "0x344040", Offset = "0x342A40", VA = "0x180344040")]
    		internal UniTask _003CPlayAsync_003Eb__0(CreditRoll roll)
    		{
    			return default(UniTask);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000A5")]
    	[CompilerGenerated]
    	private struct _003CPlayAsync_003Ed__17
    {
    		[Token(Token = "0x40002DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002DB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40002DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x40002DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CreditsDirectorAct2 _003C_003E4__this;

    		[Token(Token = "0x40002DE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private _003C_003Ec__DisplayClass17_0 _003C_003E8__1;

    		[Token(Token = "0x40002DF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IList<UniTask> _003CrebuildLayoutTasks_003E5__2;

    		[Token(Token = "0x40002E0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private int _003Ci_003E5__3;

    		[Token(Token = "0x40002E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600025B")]
    		[Address(RVA = "0x344120", Offset = "0x342B20", VA = "0x180344120", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600025C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000A6")]
    	[CompilerGenerated]
    	private struct _003CPreloadResourcesAsync_003Ed__16
    {
    		[Token(Token = "0x40002E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40002E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CreditsDirectorAct2 _003C_003E4__this;

    		[Token(Token = "0x40002E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x40002E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<SpecialThanksData>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600025D")]
    		[Address(RVA = "0x345220", Offset = "0x343C20", VA = "0x180345220", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600025E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000A7")]
    	[CompilerGenerated]
    	private struct _003CShowStillsAsync_003Ed__20
    {
    		[Token(Token = "0x40002E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40002E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CreditsDirectorAct2 _003C_003E4__this;

    		[Token(Token = "0x40002EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x40002EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40002EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private EndingStill[] _003C_003E7__wrap1;

    		[Token(Token = "0x40002ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private int _003C_003E7__wrap2;

    		[Token(Token = "0x40002EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private EndingStill _003Cstill_003E5__4;

    		[Token(Token = "0x600025F")]
    		[Address(RVA = "0x346530", Offset = "0x344F30", VA = "0x180346530", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000260")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40002BE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[Space]
    	[SerializeField]
    	private AssetReferenceT<SpecialThanksData> _specialThanksDataAsset;

    	[Token(Token = "0x40002BF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[Space]
    	[SerializeField]
    	private RectTransform _rectTransform;

    	[Token(Token = "0x40002C0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private EndingStill[] _stills;

    	[Token(Token = "0x40002C1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[SerializeField]
    	private CreditRoll[] _creditRolls;

    	[Token(Token = "0x40002C2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	[Space]
    	[SerializeField]
    	private float _scrollSpeed;

    	[Token(Token = "0x40002C3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x74")]
    	[SerializeField]
    	private float _stillDelayUnits;

    	[Token(Token = "0x40002C4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	[SerializeField]
    	private float _stillFadeUnits;

    	[Token(Token = "0x40002C5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x7C")]
    	[SerializeField]
    	private float _stillDisplayUnits;

    	[Token(Token = "0x40002C6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private IAddressableAssetLoader _addressableAssetLoader;

    	[Token(Token = "0x40002C7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	private TextSizeByCourse[][] _specialThanksCourseData;

    	[Token(Token = "0x40002C8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	private Dictionary<SpecialThanksCourse, Dictionary<LocaleKind, string>[]> _specialThanksCreditNames;

    	[Token(Token = "0x40002C9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	private int _specialThanksPhase;

    	[Token(Token = "0x17000045")]
    	private IAddressableAssetLoader AddressableAssetLoader
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x342E80, 202 B
    		// Lazy cache in +0x80 via `ServiceLocator.Get<IAddressablesManager>(true)` (cl=1); the manager is an IAddressableAssetLoader.
    		[Token(Token = "0x6000240")]
    		[Address(RVA = "0x342E80", Offset = "0x341880", VA = "0x180342E80")]
    		get
    		{
    			return _addressableAssetLoader ??= ServiceLocator.Get<IAddressablesManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x342F50, 308 B (`transform` isinst RectTransform)
    	[Token(Token = "0x6000241")]
    	[Address(RVA = "0x342F50", Offset = "0x341950", VA = "0x180342F50", Slot = "4")]
    	protected override void Reset()
    	{
    		base.Reset();
    		_rectTransform = base.transform as RectTransform;
    		_stills = GetComponentsInChildren<EndingStill>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x343090, 147 B (CreditRoll.SetGameObjectActive/SetCanvasEnabled inlined)
    	[Token(Token = "0x6000242")]
    	[Address(RVA = "0x343090", Offset = "0x341A90", VA = "0x180343090", Slot = "5")]
    	public override void Initialize()
    	{
    		base.Initialize();
    		CreditRoll[] creditRolls = _creditRolls;
    		foreach (CreditRoll creditRoll in creditRolls)
    		{
    			creditRoll.SetGameObjectActive(active: false);
    			creditRoll.SetCanvasEnabled(enable: false);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (`is not null` spelling of b__16_8) | RVA: 0x343130 (builder stub);
    	// MoveNext 0x345220, 4863 B; <>c lambdas b__16_0..16_11
    	// 
    	// - Nothing happens when both caches (+0x88, +0x90) already exist.
    	// - The SpecialThanksData asset (+0x50) loads through IAddressableAssetLoader.GetOrLoadAddressableAsset<T>(AssetReference, ct).
    	// - Course data: TextSizeByCourses (+0x18) ordered by Level (+0x18) desc, then Fee (+0x1C) desc, grouped by Phase
    	//   (+0x14), each group materialised as an array. Credit names: Items (+0x20) grouped by Course (+0x10), keyed by
    	//   the group key (IGrouping slot 0), each item's LocalizedText[] (+0x18) turned into Locale -> Text dictionaries.
    	// - `_creditRolls[..2]` is RuntimeHelpers.GetSubArray with Range(0, 2). b__16_7 is an `as SpecialThanksRoll` cast;
    	//   b__16_8 is a plain reference test (`test rdx,rdx; setne`), not UnityEngine.Object's overloaded `!=`.
    	[Token(Token = "0x6000243")]
    	[Address(RVA = "0x343130", Offset = "0x341B30", VA = "0x180343130", Slot = "6")]
    	[AsyncStateMachine(typeof(_003CPreloadResourcesAsync_003Ed__16))]
    	public override async UniTask PreloadResourcesAsync([Optional] CancellationToken ct)
    	{
    		if (_specialThanksCourseData != null && _specialThanksCreditNames != null)
    		{
    			return;
    		}
    		SpecialThanksData specialThanksData = await AddressableAssetLoader.GetOrLoadAddressableAsset<SpecialThanksData>(_specialThanksDataAsset, ct);
    		_specialThanksCourseData = specialThanksData.TextSizeByCourses.OrderByDescending((TextSizeByCourse size) => size.Level).ThenByDescending((TextSizeByCourse size) => size.Fee).GroupBy((TextSizeByCourse size) => size.Phase)
    			.Select((IGrouping<int, TextSizeByCourse> grouping) => grouping.ToArray())
    			.ToArray();
    		_specialThanksCreditNames = specialThanksData.Items.GroupBy((SpecialThanksDataItem item) => item.Course).ToDictionary((IGrouping<SpecialThanksCourse, SpecialThanksDataItem> grouping) => grouping.Key, (IGrouping<SpecialThanksCourse, SpecialThanksDataItem> grouping) => grouping
    			.Select((SpecialThanksDataItem item) => item.Name.ToDictionary((LocalizedText localizedText) => localizedText.Locale, (LocalizedText localizedText) => localizedText.Text))
    			.ToArray());
    		_specialThanksPhase = 0;
    		foreach (SpecialThanksRoll specialThanksRoll in _creditRolls[..2].Select((CreditRoll roll) => roll as SpecialThanksRoll).Where((SpecialThanksRoll roll) => roll is not null))
    		{
    			PrepareSpecialThanksRoll(specialThanksRoll);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (the single await of `scroll or delay` written as a conditional) |
    	// RVA: 0x3432E0 (builder stub); MoveNext 0x344120, 4343 B; display-class lambda <PlayAsync>b__0 0x344040;
    	// <>c lambdas b__17_1 0x343FC0 (as-cast), b__17_2 folded into b__16_8
    	// 
    	// - `ct` is captured by <>c__DisplayClass17_0 (+0x10), so every later use reads it from the closure.
    	// - The first loop runs CreditRoll.SetCanvasEnabled(false) / SetGameObjectActive(true) inlined; ChangeActivity(true)
    	//   is inlined; ShowStillsAsync (stub inlined) goes to Cysharp `Forget()`.
    	// - The pre-loop over the first two special-thanks rolls only runs while `_specialThanksPhase == 0` (jne 0x1803449EC).
    	// - The lambda (Func<CreditRoll, UniTask>) binds to Cysharp EnumerableAsyncExtensions.Select; `ToList()` fills
    	//   `<rebuildLayoutTasks>5__2` (IList<UniTask>, +0x30). ICollection<UniTask> slots: 0 Count, 2 Add, 3 Clear.
    	// - Per roll i (+0x38): for 0 < i < Length - 1 whose next roll is a SpecialThanksRoll, that roll is prepared and its
    	//   RebuildLayoutAsync joins the list. viewport = rect.height (+0xC) / speed; the scroll length is
    	//   ContentHeight / speed - viewport. ScrollAsync always starts; all but the last roll then await
    	//   `UniTask.Delay(length - viewport)` (the `bool ignoreTimeScale` overload, 0x180D530) instead of the scroll itself,
    	//   so consecutive rolls overlap. Pending layout rebuilds are awaited as a batch (UniTaskExtensions.GetAwaiter over
    	//   IEnumerable<UniTask>) and then cleared.
    	// - No try/finally; the end label jump is skipped on cancellation.
    	[Token(Token = "0x6000244")]
    	[Address(RVA = "0x3432E0", Offset = "0x341CE0", VA = "0x1803432E0", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CPlayAsync_003Ed__17))]
    	public override async UniTask PlayAsync([Optional] CancellationToken ct)
    	{
    		CreditRoll[] creditRolls = _creditRolls;
    		foreach (CreditRoll creditRoll in creditRolls)
    		{
    			creditRoll.SetCanvasEnabled(enable: false);
    			creditRoll.SetGameObjectActive(active: true);
    		}
    		ChangeActivity(enable: true);
    		ShowStillsAsync(ct).Forget();
    		if (_specialThanksPhase == 0)
    		{
    			foreach (SpecialThanksRoll specialThanksRoll in _creditRolls[..2].Select((CreditRoll roll) => roll as SpecialThanksRoll).Where((SpecialThanksRoll roll) => roll is not null))
    			{
    				PrepareSpecialThanksRoll(specialThanksRoll);
    			}
    		}
    		IList<UniTask> rebuildLayoutTasks = _creditRolls[..2].Select((CreditRoll roll) => (roll is SpecialThanksRoll specialThanksRoll2) ? specialThanksRoll2.RebuildLayoutAsync(ct) : UniTask.CompletedTask).ToList();
    		for (int i = 0; i < _creditRolls.Length; i++)
    		{
    			if (i > 0 && i < _creditRolls.Length - 1 && _creditRolls[i + 1] is SpecialThanksRoll nextRoll)
    			{
    				PrepareSpecialThanksRoll(nextRoll);
    				rebuildLayoutTasks.Add(nextRoll.RebuildLayoutAsync(ct));
    			}
    			float viewportDuration = _rectTransform.rect.height / _scrollSpeed;
    			float scrollDuration = _creditRolls[i].ContentHeight / _scrollSpeed - viewportDuration;
    			UniTask scrollTask = _creditRolls[i].ScrollAsync(scrollDuration, ct);
    			await ((i < _creditRolls.Length - 1) ? UniTask.Delay(TimeSpan.FromSeconds(scrollDuration - viewportDuration), ignoreTimeScale: false, cancellationToken: ct) : scrollTask);
    			if (rebuildLayoutTasks.Count > 0)
    			{
    				await rebuildLayoutTasks;
    				rebuildLayoutTasks.Clear();
    			}
    		}
    		ResumeAtLabel(base.EndLabelName);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x343490, 316 B
    	// ChangeActivity(false) and the two CreditRoll setters are inlined; rolls that are SpecialThanksRoll are cleared.
    	[Token(Token = "0x6000245")]
    	[Address(RVA = "0x343490", Offset = "0x341E90", VA = "0x180343490", Slot = "8")]
    	public override void Stop()
    	{
    		ChangeActivity(enable: false);
    		CreditRoll[] creditRolls = _creditRolls;
    		foreach (CreditRoll creditRoll in creditRolls)
    		{
    			creditRoll.SetGameObjectActive(active: false);
    			creditRoll.SetCanvasEnabled(enable: false);
    			if (creditRoll is SpecialThanksRoll specialThanksRoll)
    			{
    				specialThanksRoll.Clear();
    			}
    		}
    		_specialThanksPhase = 0;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3435D0, 279 B
    	// One phase of the course table per call: each entry supplies its Level (+0x18) and the names of its Course (+0x10).
    	[Token(Token = "0x6000246")]
    	[Address(RVA = "0x3435D0", Offset = "0x341FD0", VA = "0x1803435D0")]
    	private void PrepareSpecialThanksRoll(SpecialThanksRoll roll)
    	{
    		roll.Clear();
    		TextSizeByCourse[] array = _specialThanksCourseData[_specialThanksPhase];
    		foreach (TextSizeByCourse textSizeByCourse in array)
    		{
    			roll.SetCreditTexts(textSizeByCourse.Level, _specialThanksCreditNames[textSizeByCourse.Course]);
    		}
    		_specialThanksPhase++;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3436F0 (builder stub); MoveNext 0x346530, 3572 B, four await states
    	// 
    	// Delays use the DelayType overload (0x180D710); the still fades use the EndingStill builder stubs inlined.
    	// `<>7__wrap1`/`<>7__wrap2` are the foreach array and index; `<still>5__4` is cleared after each dismissal.
    	[Token(Token = "0x6000247")]
    	[Address(RVA = "0x3436F0", Offset = "0x3420F0", VA = "0x1803436F0")]
    	[AsyncStateMachine(typeof(_003CShowStillsAsync_003Ed__20))]
    	private async UniTask ShowStillsAsync([Optional] CancellationToken ct)
    	{
    		await UniTask.Delay(TimeSpan.FromSeconds(_stillDelayUnits * base.BgmUnitDuration), Cysharp.Threading.Tasks.DelayType.DeltaTime, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, ct);
    		EndingStill[] stills = _stills;
    		foreach (EndingStill still in stills)
    		{
    			await still.PresentAsync(_stillFadeUnits * base.BgmUnitDuration, ct);
    			await UniTask.Delay(TimeSpan.FromSeconds(_stillDisplayUnits * base.BgmUnitDuration), Cysharp.Threading.Tasks.DelayType.DeltaTime, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, ct);
    			await still.DismissAsync(_stillFadeUnits * base.BgmUnitDuration, ct);
    		}
    	}

    	[Token(Token = "0x6000248")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public CreditsDirectorAct2()
    	{
    	}
    }
}
