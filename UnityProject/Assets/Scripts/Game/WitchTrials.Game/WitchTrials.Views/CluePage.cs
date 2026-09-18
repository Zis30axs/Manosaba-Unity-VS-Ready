using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000EE")]
    public sealed class CluePage : WitchBookPageBase<ClueDataItem, ClueState>
    {
    	// RECONSTRUCTED | Confidence: A (shape), B (members not individually byte-diffed) | RVA: 0x36C0D0 (primary ctor, shared two-ref-store body)
    	// This is a positional record: dump.cs has `Deconstruct(out Name, out Description)`, `<Clone>$` and a
    	// copy ctor, all [CompilerGenerated], with backing fields +0x10 Name and +0x18 Description.
    	// The remaining RVAs are compiler-synthesized record members:
    	// RECONSTRUCTED | RVA: 0x36C180 EqualityContract, 0x36C200 init Description, 0x36C260 ToString, 0x36C3A0 PrintMembers,
    	// RECONSTRUCTED | RVA: 0x36C480 op_Inequality, 0x36C4C0 op_Equality, 0x36C4F0 GetHashCode, 0x36C600 Equals(object),
    	// RECONSTRUCTED | RVA: 0x36C6A0 Equals(LocalizedTexts), 0x36C7D0 <Clone>$, 0x36C8D0 copy ctor, 0x36C9B0 Deconstruct
    	[Token(Token = "0x20000EF")]
    	private record LocalizedTexts(string Name, string Description);

    	[StructLayout(3)]
    	[Token(Token = "0x20000F2")]
    	[CompilerGenerated]
    	private struct _003CLoadDataAsync_003Ed__4
    {
    		[Token(Token = "0x4000494")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000495")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IList<VersionedItem<ClueDataItem>>> _003C_003Et__builder;

    		[Token(Token = "0x4000496")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CluePage _003C_003E4__this;

    		[Token(Token = "0x4000497")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000498")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<IList<VersionedItem<ClueDataItem>>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60003D3")]
    		[Address(RVA = "0x36D4E0", Offset = "0x36BEE0", VA = "0x18036D4E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60003D4")]
    		[Address(RVA = "0x36DB20", Offset = "0x36C520", VA = "0x18036DB20", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000484")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	[Header("For Clue Page")]
    	[SerializeField]
    	private WitchBookItemSubjectLabel _subjectLabel;

    	[Token(Token = "0x4000485")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	[SerializeField]
    	private TMP_Text _descriptionLabel;

    	[Token(Token = "0x4000486")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	[SerializeField]
    	private WitchBookItemThumbnail _thumbnail;

    	[Token(Token = "0x4000487")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	private IReadOnlyDictionary<IdVersionPair, IReadOnlyDictionary<LocaleKind, LocalizedTexts>> _localizedTextData;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36BB30, 504 B (entry) + MoveNext 0x36D4E0, 1585 B
    	// RECONSTRUCTED | RVA: 0x36C050 `<>n__0` (118 B): the base-call accessor for `base.LoadDataAsync(ct)` (shared generic 0xB4E720)
    	// - `await base.LoadDataAsync(ct)`. The awaiter is at +0x30, `this` at +0x20, `ct` at +0x28.
    	// - `_localizedTextData (+0xD0) = result.ToDictionary(keySelector, elementSelector)`, with a null
    	//   comparer (r9 = 0):
    	//   - `<>c.<LoadDataAsync>b__4_0` 0x36CB00 → `versioned.IdVersionPair`; cached `<>9__4_0` at +0x08.
    	//   - `<>c.<LoadDataAsync>b__4_1` 0x36CB50 (2056 B), cached `<>9__4_1` at +0x38:
    	//     - Allocates `<>c__DisplayClass4_0` first.
    	//     - `names (+0x10) = versioned.Item._name.ToDictionary(b__4_2 Locale, b__4_3 Text)`;
    	//       `descriptions (+0x18)` does the same with `_description` (b__4_4 / b__4_5). The shared
    	//       bodies are 0x312620 (`[rdx+0x10]`) and 0x312640 (`[rdx+0x18]`).
    	//     - `names.Keys.Union(descriptions.Keys)` (IReadOnlyDictionary slot 3 = Keys; UnionIterator
    	//       with a null comparer), then `.ToDictionary(b__4_6 identity (0x329530), DisplayClass.b__7)`.
    	//     - `b__7` 0x36D360 → `new LocalizedTexts(names.GetValueOrDefault(locale) ?? string.Empty,
    	//       descriptions.GetValueOrDefault(locale) ?? string.Empty)`, with the record ctor inlined.
    	// - The awaited list itself is returned (SetResult(rbx), the GetResult value).
    	[Token(Token = "0x60003B3")]
    	[Address(RVA = "0x36BB30", Offset = "0x36A530", VA = "0x18036BB30", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__4))]
    	protected override async UniTask<IList<VersionedItem<ClueDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
    	{
    		IList<VersionedItem<ClueDataItem>> result = await base.LoadDataAsync(ct);
    		_localizedTextData = result.ToDictionary((VersionedItem<ClueDataItem> versioned) => versioned.IdVersionPair, delegate(VersionedItem<ClueDataItem> versioned)
    		{
    			IReadOnlyDictionary<LocaleKind, string> names = versioned.Item.Name.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
    			IReadOnlyDictionary<LocaleKind, string> descriptions = versioned.Item.Description.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
    			return (IReadOnlyDictionary<LocaleKind, LocalizedTexts>)names.Keys.Union(descriptions.Keys).ToDictionary((LocaleKind locale) => locale, (LocaleKind locale) => new LocalizedTexts(names.GetValueOrDefault(locale) ?? string.Empty, descriptions.GetValueOrDefault(locale) ?? string.Empty));
    		});
    		return result;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36BD30, 190 B
    	// `map.Id` (VersionedItem<T>._id through the rgctx field offset; NRE on null map), then
    	// `WitchBookDataHelper.BuildClueTextureAddress`, then a tail jump to
    	// `button.SetupWithAddressableTexture(address)`.
    	[Token(Token = "0x60003B4")]
    	[Address(RVA = "0x36BD30", Offset = "0x36A730", VA = "0x18036BD30", Slot = "17")]
    	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<ClueDataItem> map)
    	{
    		button.SetupWithAddressableTexture(WitchBookDataHelper.BuildClueTextureAddress(map.Id));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36BDF0, 543 B
    	// The field loads come before each lookup: `_subjectLabel` +0xB8 and `_localizedTextData` +0xD0,
    	// then `_descriptionLabel` +0xC0, then `_thumbnail` +0xC8.
    	// - `_localizedTextData[map.IdVersionPair]`: the interface thunk sub_6C80 resolves slot 2 = get_Item.
    	// - `[CurrentLocale]`: sub_6BF0 resolves slot 2 for the LocaleKind key.
    	// - `.Name` (+0x10) → `WitchBookItemSubjectLabel.SetText`.
    	// - The same double lookup again, `.Description` (+0x18) → `TMP_Text.SetText(string)`.
    	// - Tail jump to `_thumbnail.Setup(BuildClueTextureAddress(map.Id))`.
    	[Token(Token = "0x60003B5")]
    	[Address(RVA = "0x36BDF0", Offset = "0x36A7F0", VA = "0x18036BDF0", Slot = "18")]
    	protected override void RefreshPageContent(VersionedItem<ClueDataItem> map)
    	{
    		_subjectLabel.SetText(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Name);
    		_descriptionLabel.SetText(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Description);
    		_thumbnail.Setup(WitchBookDataHelper.BuildClueTextureAddress(map.Id));
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x36C010, 62 B
    	// Only the call to WitchBookPageBase<ClueDataItem, ClueState>..ctor; no field initializers.
    	[Token(Token = "0x60003B6")]
    	[Address(RVA = "0x36C010", Offset = "0x36AA10", VA = "0x18036C010")]
    	public CluePage()
    	{
    	}
    }
}
