using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000FC")]
public class MapPage : WitchBookPageBase<MapDataItem, MapState>
{
	[Token(Token = "0x20000FF")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass9_0
	{
		[Token(Token = "0x40004BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public MapPage _003C_003E4__this;

		[Token(Token = "0x40004BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationTokenSource linkedCts;

		[Token(Token = "0x600040E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass9_0()
		{
		}

		[Token(Token = "0x600040F")]
		[Address(RVA = "0x370770", Offset = "0x36F170", VA = "0x180370770")]
		internal bool _003COnChoiceMapAreaAsync_003Eb__0(MapArea area)
		{
			return default(bool);
		}

		[Token(Token = "0x6000410")]
		[Address(RVA = "0x3707E0", Offset = "0x36F1E0", VA = "0x1803707E0")]
		internal UniTask<MapAreaKind> _003COnChoiceMapAreaAsync_003Eb__1(MapArea area)
		{
			return default(UniTask<MapAreaKind>);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000100")]
	[CompilerGenerated]
	private struct _003CLoadDataAsync_003Ed__5
{
		[Token(Token = "0x40004BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40004BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IList<VersionedItem<MapDataItem>>> _003C_003Et__builder;

		[Token(Token = "0x40004C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MapPage _003C_003E4__this;

		[Token(Token = "0x40004C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IList<VersionedItem<MapDataItem>>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000411")]
		[Address(RVA = "0x370A00", Offset = "0x36F400", VA = "0x180370A00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000412")]
		[Address(RVA = "0x371040", Offset = "0x36FA40", VA = "0x180371040", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000101")]
	[CompilerGenerated]
	private struct _003COnChoiceMapAreaAsync_003Ed__9
{
		[Token(Token = "0x40004C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40004C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<MapAreaKind> _003C_003Et__builder;

		[Token(Token = "0x40004C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MapPage _003C_003E4__this;

		[Token(Token = "0x40004C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass9_0 _003C_003E8__1;

		[Token(Token = "0x40004C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<(int winArgumentIndex, MapAreaKind result)>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000413")]
		[Address(RVA = "0x371070", Offset = "0x36FA70", VA = "0x180371070", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000414")]
		[Address(RVA = "0x371A50", Offset = "0x370450", VA = "0x180371A50", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40004B1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	[SerializeField]
	private MapFloor[] _floors;

	[Token(Token = "0x40004B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
	private IReadOnlyDictionary<IdVersionPair, IReadOnlyDictionary<LocaleKind, string>> _localizedTextData;

	[Token(Token = "0x40004B3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private IReadOnlyCollection<MapAreaKind> _restrictionAreas;

	[Token(Token = "0x40004B4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private MapArea[] _displayableAreas;

	[Token(Token = "0x60003FD")]
	[Address(RVA = "0x36F5B0", Offset = "0x36DFB0", VA = "0x18036F5B0", Slot = "5")]
	protected override void Reset()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36F690, 504 B (entry) + MoveNext 0x370A00, 1585 B
	// RECONSTRUCTED | RVA: 0x3701B0 `<>n__0` (118 B), the base-call accessor for `base.LoadDataAsync(ct)`
	// RECONSTRUCTED | RVA: 0x3702D0 `<>c.b__5_0`, 0x370320 `<>c.b__5_1` (817 B)
	// The entry, `<>n__0` and `b__5_0` are byte-identical to CluePage (pagediff: 0 differing lines).
	// MoveNext differs from Clue only in three places: the element type `IReadOnlyDictionary<LocaleKind, string>`, the cache
	// slot `<>9__5_1` at +0x20, and the store at `_localizedTextData` +0xC0.
	// `b__5_1` has no display class and no Union: `versioned.Item` (fully-shared get_Item inlined), then `_buttonText`
	// (+0x10) → `ToDictionary(b__5_2 Locale, b__5_3 Text)` (sub_4A60). `ProfilePage.b__9_1` is byte-identical.
	[Token(Token = "0x60003FE")]
	[Address(RVA = "0x36F690", Offset = "0x36E090", VA = "0x18036F690", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__5))]
	protected override async UniTask<IList<VersionedItem<MapDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		IList<VersionedItem<MapDataItem>> result = await base.LoadDataAsync(ct);
		_localizedTextData = result.ToDictionary((VersionedItem<MapDataItem> versioned) => versioned.IdVersionPair, (VersionedItem<MapDataItem> versioned) => (IReadOnlyDictionary<LocaleKind, string>)versioned.Item.ButtonText.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text));
		return result;
	}

	// RECONSTRUCTED | Confidence: A (behavior), B (ternary spelling) | RVA: 0x36F890, 311 B
	// - `_localizedTextData (+0xC0)[map.IdVersionPair]` (sub_6C80, slot 2), then `[CurrentLocale]`
	//   (sub_36F0 with ecx = 2, get_Item). The result is null-checked (NRE) for the instance call
	//   `text.Contains("<br>")`.
	// - `backgroundIndex = (byte)contains + 1` (`movzx r8d, al; inc r8d`): the native compiler's folding of
	//   `contains ? 2 : 1`, the same index 2 RulePage uses for its two-line `<br>` label.
	// - Tail jump to `button.SetupWithText(text, backgroundIndex)`.
	[Token(Token = "0x60003FF")]
	[Address(RVA = "0x36F890", Offset = "0x36E290", VA = "0x18036F890", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<MapDataItem> map)
	{
		string text = _localizedTextData[map.IdVersionPair][base.CurrentLocale];
		button.SetupWithText(text, text.Contains("<br>") ? 2 : 1);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36F9D0, 341 B
	// - An array `foreach` over `_floors` (+0xB8), using an index and length with bounds checks.
	// - Per floor, `map.IdVersionPair` is re-read (the getter is called every iteration), then an inlined
	//   `MapFloor.IsMatch(pair)`: `pair.Equals(floor._idAndVersion (+0x38))`. A null pair throws NRE; a null
	//   `_idAndVersion` gives false; same ref gives true; otherwise ordinal Id plus Version compare.
	// - Match → `floor.Show(_restrictionAreas (+0xC8))` (0x36F100); no match → `floor.Hide()` (0x36F420).
	[Token(Token = "0x6000400")]
	[Address(RVA = "0x36F9D0", Offset = "0x36E3D0", VA = "0x18036F9D0", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<MapDataItem> map)
	{
		MapFloor[] floors = _floors;
		foreach (MapFloor mapFloor in floors)
		{
			if (mapFloor.IsMatch(map.IdVersionPair))
			{
				mapFloor.Show(_restrictionAreas);
			}
			else
			{
				mapFloor.Hide();
			}
		}
	}

	[Token(Token = "0x6000401")]
	[Address(RVA = "0x36FB30", Offset = "0x36E530", VA = "0x18036FB30")]
	public void InitializeAreas(IReadOnlyCollection<MapAreaKind> restrictions)
	{
	}

	[Token(Token = "0x6000402")]
	[Address(RVA = "0x36FF80", Offset = "0x36E980", VA = "0x18036FF80")]
	[AsyncStateMachine(typeof(_003COnChoiceMapAreaAsync_003Ed__9))]
	public UniTask<MapAreaKind> OnChoiceMapAreaAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<MapAreaKind>);
	}

	[Token(Token = "0x6000403")]
	[Address(RVA = "0x370170", Offset = "0x36EB70", VA = "0x180370170")]
	public MapPage()
	{
	}
}
