using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000129")]
public class WitchBookUi : CustomUI, IWitchBookUi, IManagedUI
{
	[Serializable]
	[Token(Token = "0x200012A")]
	public new class GameState
	{
		[Token(Token = "0x400059C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public WitchBookCategory[] RestrictionCategories;

		[Token(Token = "0x400059D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public MapAreaKind[] RestrictionMapAreas;

		[Token(Token = "0x6000510")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200012C")]
	[CompilerGenerated]
	private struct _003CChoiceAreaAsync_003Ed__7
{
		[Token(Token = "0x40005A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<MapAreaKind> _003C_003Et__builder;

		[Token(Token = "0x40005A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40005A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public WitchBookUi _003C_003E4__this;

		[Token(Token = "0x40005A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40005A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Cysharp.Threading.Tasks.UniTask<MapAreaKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000514")]
		[Address(RVA = "0x37F570", Offset = "0x37DF70", VA = "0x18037F570", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000515")]
		[Address(RVA = "0x37FC60", Offset = "0x37E660", VA = "0x18037FC60", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200012D")]
	[CompilerGenerated]
	private struct _003CChoiceEvidenceAsync_003Ed__6
{
		[Token(Token = "0x40005A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

		[Token(Token = "0x40005A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40005A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public WitchBookUi _003C_003E4__this;

		[Token(Token = "0x40005AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public IReadOnlyList<WitchBookCategory> categories;

		[Token(Token = "0x40005AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40005AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Cysharp.Threading.Tasks.UniTask<string>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000516")]
		[Address(RVA = "0x37FC90", Offset = "0x37E690", VA = "0x18037FC90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000517")]
		[Address(RVA = "0x380220", Offset = "0x37EC20", VA = "0x180380220", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200012E")]
	[CompilerGenerated]
	private struct _003CDeserializeState_003Ed__13
{
		[Token(Token = "0x40005AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchBookUi _003C_003E4__this;

		[Token(Token = "0x40005B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameStateMap stateMap;

		[Token(Token = "0x40005B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000518")]
		[Address(RVA = "0x380250", Offset = "0x37EC50", VA = "0x180380250", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000519")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200012F")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__2
{
		[Token(Token = "0x40005B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchBookUi _003C_003E4__this;

		[Token(Token = "0x40005B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask _003Cresult_003E5__2;

		[Token(Token = "0x40005B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40005B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600051A")]
		[Address(RVA = "0x3807A0", Offset = "0x37F1A0", VA = "0x1803807A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600051B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400059A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Header("References for WitchBook")]
	[SerializeField]
	private WitchBookScreen _witchBookScreen;

	[Token(Token = "0x400059B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private FontMaterialChanger _fontMaterialChanger;

	// RECONSTRUCTED | Confidence: A (local spelling: B) | RVA: 0x37E520 (stub), MoveNext 0x3807A0
	// - `<result>5__2` (+0x20) is assigned first from `base.Initialize()`: dump.cs lists this class's `<>n__0`
	//   base-call accessor at CustomUI.Initialize (0x3483B0), whose 65-byte body (`return default`) is inlined
	//   here as a stored null awaiter.
	// - Then `_witchBookScreen` (+0xE0) is loaded, then `destroyCancellationToken`, then WitchBookScreen.InitializeAsync
	//   (inlined: Cysharp builder Start<<InitializeAsync>d__37>), awaited through the Cysharp awaiter (u__1).
	// - Finally `result` is awaited through the Naninovel IAwaiter (u__2, state 1).
	[Token(Token = "0x6000501")]
	[Address(RVA = "0x37E520", Offset = "0x37CF20", VA = "0x18037E520", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__2))]
	public override async Naninovel.UniTask Initialize()
	{
		Naninovel.UniTask result = base.Initialize();
		await _witchBookScreen.InitializeAsync(destroyCancellationToken);
		await result;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37E650
	// Inlined WitchBookScreen.PresentationMode setter: stores `mode` into `_witchBookScreen`+0xF8.
	[Token(Token = "0x6000502")]
	[Address(RVA = "0x37E650", Offset = "0x37D050", VA = "0x18037E650", Slot = "95")]
	public void SetPresentationMode(WitchBookPresentationMode mode)
	{
		_witchBookScreen.PresentationMode = mode;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37E680
	// Inlined WitchBookScreen.RestrictionCategories setter (+0x100, with write barrier).
	[Token(Token = "0x6000503")]
	[Address(RVA = "0x37E680", Offset = "0x37D080", VA = "0x18037E680", Slot = "96")]
	public void SetRestrictionCategories(IReadOnlyList<WitchBookCategory> categories)
	{
		_witchBookScreen.RestrictionCategories = categories;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37E6F0
	// Inlined WitchBookScreen.RestrictionAreas setter (+0x108, with write barrier).
	[Token(Token = "0x6000504")]
	[Address(RVA = "0x37E6F0", Offset = "0x37D0F0", VA = "0x18037E6F0", Slot = "97")]
	public void SetRestrictionMapAreas(IReadOnlyCollection<MapAreaKind> areas)
	{
		_witchBookScreen.RestrictionAreas = areas;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37E760 (stub), MoveNext 0x37FC90
	// `<linkedCts>5__2` (+0x40) = Naninovel AsyncToken.CreateLinkedTokenSource() on entry (skipped when resuming);
	// WitchBookScreen.ChoiceEvidenceAsync (0x37B230, real call) with `categories` and `linkedCts.Token`, awaited
	// through the Cysharp awaiter; linkedCts disposed via IDisposable in the `using` finally (state < 0) before SetResult.
	[Token(Token = "0x6000505")]
	[Address(RVA = "0x37E760", Offset = "0x37D160", VA = "0x18037E760", Slot = "99")]
	[AsyncStateMachine(typeof(_003CChoiceEvidenceAsync_003Ed__6))]
	public async Naninovel.UniTask<string> ChoiceEvidenceAsync(IReadOnlyList<WitchBookCategory> categories, [Optional] AsyncToken token)
	{
		using CancellationTokenSource linkedCts = token.CreateLinkedTokenSource();
		return await _witchBookScreen.ChoiceEvidenceAsync(categories, linkedCts.Token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37E9B0 (stub), MoveNext 0x37F570
	// Same shape as ChoiceEvidenceAsync: `<linkedCts>5__2` (+0x38) from AsyncToken.CreateLinkedTokenSource();
	// WitchBookScreen.ChoiceAreaAsync inlined (Cysharp builder Start<<ChoiceAreaAsync>d__50>) with linkedCts.Token,
	// awaited; linkedCts disposed in the `using` finally.
	[Token(Token = "0x6000506")]
	[Address(RVA = "0x37E9B0", Offset = "0x37D3B0", VA = "0x18037E9B0", Slot = "100")]
	[AsyncStateMachine(typeof(_003CChoiceAreaAsync_003Ed__7))]
	public async Naninovel.UniTask<MapAreaKind> ChoiceAreaAsync([Optional] AsyncToken token)
	{
		using CancellationTokenSource linkedCts = token.CreateLinkedTokenSource();
		return await _witchBookScreen.ChoiceAreaAsync(linkedCts.Token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37EBA0
	// WitchBookScreen.GetActiveEvidences (0x37B090) inlined: get_PageData, IDictionary get_Item,
	// Page slot 14 (+0x218) tail-jump.
	[Token(Token = "0x6000507")]
	[Address(RVA = "0x37EBA0", Offset = "0x37D5A0", VA = "0x18037EBA0", Slot = "98")]
	public IReadOnlyCollection<IdVersionPair> GetActiveEvidences(WitchBookCategory category)
	{
		return _witchBookScreen.GetActiveEvidences(category);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37EC30
	// WitchBookScreen.UpdateVersion (0x37B110) inlined: get_PageData, get_Item, Page slot 11 (+0x1E8).
	[Token(Token = "0x6000508")]
	[Address(RVA = "0x37EC30", Offset = "0x37D630", VA = "0x18037EC30", Slot = "101")]
	public void UpdateVersion(WitchBookCategory category, string id, int version)
	{
		_witchBookScreen.UpdateVersion(category, id, version);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37ECE0
	// WitchBookScreen.ClearState (0x37B1B0) inlined: get_PageData, get_Item, Page slot 12 (+0x1F8).
	[Token(Token = "0x6000509")]
	[Address(RVA = "0x37ECE0", Offset = "0x37D6E0", VA = "0x18037ECE0", Slot = "102")]
	public void ClearState(WitchBookCategory category)
	{
		_witchBookScreen.ClearState(category);
	}

	// RECONSTRUCTED | Confidence: A (base-call spelling: B) | RVA: 0x37ED70, 907 B
	// Byte-for-byte the DebateTextPrinterPanel.SetFont shape (0x367720):
	// - `FontChanger.ChangeFont(font, FontChangeConfigurations)` (Slot 79, klass+0x628) = the whole of
	//   CustomUI.SetFont (0x619440), i.e. `base.SetFont(font)` inlined.
	// - foreach over `FontChangeConfigurations.SelectMany(config => config.Components)` (`<>c.<SetFont>b__11_0`,
	//   folded at 0x368A00, reads config+0x28); per component `_fontMaterialChanger` (+0xE8), then
	//   `destroyCancellationToken`, then ChangeFontMaterialAsync -> Cysharp `Forget`.
	[Token(Token = "0x600050A")]
	[Address(RVA = "0x37ED70", Offset = "0x37D770", VA = "0x18037ED70", Slot = "84")]
	public override void SetFont(TMP_FontAsset font)
	{
		base.SetFont(font);
		foreach (TMP_Text text in FontChangeConfigurations.SelectMany((CustomUI.FontChangeConfiguration config) => config.Components))
		{
			_fontMaterialChanger.ChangeFontMaterialAsync(text, destroyCancellationToken).Forget();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37F100
	// CustomUI.SerializeState (0x61A010, real call); `new GameState` (empty ctor inlined); +0x10 =
	// `_witchBookScreen.RestrictionCategories` (+0x100) ToArray when non-null else null; +0x18 = RestrictionAreas
	// (+0x108) likewise; then StateMap.SetState<GameState> inlined to Dictionary<string, object>.set_Item.
	[Token(Token = "0x600050B")]
	[Address(RVA = "0x37F100", Offset = "0x37DB00", VA = "0x18037F100", Slot = "87")]
	protected override void SerializeState(GameStateMap stateMap)
	{
		base.SerializeState(stateMap);
		GameState state = new GameState
		{
			RestrictionCategories = _witchBookScreen.RestrictionCategories?.ToArray(),
			RestrictionMapAreas = _witchBookScreen.RestrictionAreas?.ToArray()
		};
		stateMap.SetState(state);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x37F340 (stub), MoveNext 0x380250
	// - Awaits CustomUI.DeserializeState (0x61A0E0, real call) through the Naninovel IAwaiter (u__1).
	// - StateMap.GetState<GameState>(); null -> stores null into `_witchBookScreen`+0x100 and +0x108 and returns.
	// - Otherwise RestrictionCategories setter (0x37A050) with `new List<WitchBookCategory>(array)` when the array is
	//   non-null (Enumerable.ToList inlined) else null; RestrictionAreas setter (0x37A0C0) likewise.
	[Token(Token = "0x600050C")]
	[Address(RVA = "0x37F340", Offset = "0x37DD40", VA = "0x18037F340", Slot = "88")]
	[AsyncStateMachine(typeof(_003CDeserializeState_003Ed__13))]
	protected override async Naninovel.UniTask DeserializeState(GameStateMap stateMap)
	{
		await base.DeserializeState(stateMap);
		GameState state = stateMap.GetState<GameState>();
		if (state == null)
		{
			_witchBookScreen.RestrictionCategories = null;
			_witchBookScreen.RestrictionAreas = null;
			return;
		}
		_witchBookScreen.RestrictionCategories = state.RestrictionCategories?.ToList();
		_witchBookScreen.RestrictionAreas = state.RestrictionMapAreas?.ToList();
	}

	[Token(Token = "0x600050D")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public WitchBookUi()
	{
	}
}
