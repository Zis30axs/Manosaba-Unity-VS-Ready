using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000102")]
public class NotePage : WitchBookPageBase<NoteDataItem, NoteState>
{
	// RECONSTRUCTED | Confidence: A (shape), B (members not individually byte-diffed) | RVA: 0x36C0D0 (primary ctor, shared two-ref-store body)
	// This is a positional record, the same shape as `CluePage.LocalizedTexts`: `Deconstruct(out Title, out Description)` and
	// `<Clone>$`, all [CompilerGenerated], with backing fields +0x10 Title and +0x18 Description. Compiler-synthesized members:
	// RECONSTRUCTED | RVA: 0x372180 EqualityContract, 0x36C200 init Description, 0x372200 ToString, 0x372340 PrintMembers,
	// RECONSTRUCTED | RVA: 0x36C480 op_Inequality, 0x36C4C0 op_Equality, 0x372420 GetHashCode, 0x372530 Equals(object),
	// RECONSTRUCTED | RVA: 0x3725D0 Equals(LocalizedTexts), 0x372700 <Clone>$, 0x36C8D0 copy ctor, 0x36C9B0 Deconstruct
	[Token(Token = "0x2000103")]
	private record LocalizedTexts(string Title, string Description);

	[StructLayout(3)]
	[Token(Token = "0x2000106")]
	[CompilerGenerated]
	private struct _003CLoadDataAsync_003Ed__4
{
		[Token(Token = "0x40004D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40004DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IList<VersionedItem<NoteDataItem>>> _003C_003Et__builder;

		[Token(Token = "0x40004DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public NotePage _003C_003E4__this;

		[Token(Token = "0x40004DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IList<VersionedItem<NoteDataItem>>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000436")]
		[Address(RVA = "0x373280", Offset = "0x371C80", VA = "0x180373280", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000437")]
		[Address(RVA = "0x3738C0", Offset = "0x3722C0", VA = "0x1803738C0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x372020 (.cctor), 215 B
	// `new Regex("<.+?>", (RegexOptions)8 = Compiled)` stored to static +0x0. It strips rich-text tags from the title.
	[Token(Token = "0x40004C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly Regex s_tagPattern = new Regex("<.+?>", RegexOptions.Compiled);

	[Token(Token = "0x40004CA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	[Header("For Note Page")]
	[SerializeField]
	private TMP_Text _titleLabel;

	[Token(Token = "0x40004CB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
	[SerializeField]
	private TMP_Text _descriptionLabel;

	[Token(Token = "0x40004CC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private IReadOnlyDictionary<IdVersionPair, IReadOnlyDictionary<LocaleKind, LocalizedTexts>> _localizedTextData;

	// RECONSTRUCTED | Confidence: A | RVA: 0x371A80, 504 B (entry) + MoveNext 0x373280, 1585 B
	// RECONSTRUCTED | RVA: 0x372100 `<>n__0` (118 B), the base-call accessor for `base.LoadDataAsync(ct)`
	// RECONSTRUCTED | RVA: 0x3728A0 `<>c.b__4_0`, 0x3728F0 `<>c.b__4_1`, 0x373100 `<>c__DisplayClass4_0.b__7`
	// Byte-identical to `CluePage.LoadDataAsync` after normalizing addresses and type names (pagediff):
	// - The entry, `<>n__0`, `b__4_0` and `b__7` have 0 differing lines.
	// - In MoveNext only the store offset differs: `_localizedTextData` is at +0xC8 here, +0xD0 in Clue.
	// - `b__4_1` differs only in metadata-init order and in the rgctx thunk for `VersionedItem<NoteDataItem>.get_Item`
	//   (sub_63C0 vs sub_6300). It reads the same +0x10 (`Title`) and +0x18 (`Description`) offsets.
	// The display-class fields are `title` (+0x10) and `descriptions` (+0x18).
	[Token(Token = "0x6000415")]
	[Address(RVA = "0x371A80", Offset = "0x370480", VA = "0x180371A80", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__4))]
	protected override async UniTask<IList<VersionedItem<NoteDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		IList<VersionedItem<NoteDataItem>> result = await base.LoadDataAsync(ct);
		_localizedTextData = result.ToDictionary((VersionedItem<NoteDataItem> versioned) => versioned.IdVersionPair, delegate(VersionedItem<NoteDataItem> versioned)
		{
			IReadOnlyDictionary<LocaleKind, string> title = versioned.Item.Title.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
			IReadOnlyDictionary<LocaleKind, string> descriptions = versioned.Item.Description.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
			return (IReadOnlyDictionary<LocaleKind, LocalizedTexts>)title.Keys.Union(descriptions.Keys).ToDictionary((LocaleKind locale) => locale, (LocaleKind locale) => new LocalizedTexts(title.GetValueOrDefault(locale) ?? string.Empty, descriptions.GetValueOrDefault(locale) ?? string.Empty));
		});
		return result;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x371C80, 250 B
	// `_localizedTextData` (+0xC8) is loaded first. Then `[map.IdVersionPair]` (sub_6C80, slot 2 get_Item),
	// then `[CurrentLocale]` (sub_6B30, slot 2). Then `.Title` (+0x10) goes to a tail jump
	// `button.SetupWithText(title, backgroundIndex: 0)` (r8d = 0). Note entries show text, not a texture.
	[Token(Token = "0x6000416")]
	[Address(RVA = "0x371C80", Offset = "0x370680", VA = "0x180371C80", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<NoteDataItem> map)
	{
		button.SetupWithText(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Title, 0);
	}

	// RECONSTRUCTED | Confidence: A (behavior), B (local-vs-inline spelling) | RVA: 0x371D80, 595 B
	// - `s_tagPattern` is loaded before the lookup, then the double lookup `.Title` (+0x10).
	// - `Regex.Replace(input, "")` is inlined: a null input throws `ArgumentNullException("input")`,
	//   `startat` is the input length when RightToLeft (options & 0x40), else 0, and count is -1. The literal is "".
	// - `_titleLabel` (+0xB8) is read only after Replace returns. In `CluePage` the label field is read
	//   first, so this points to a separate local, not an inlined argument.
	// - `_descriptionLabel` (+0xC0) is read first, then the double lookup `.Description` (+0x18), then a tail jump
	//   to `TMP_Text.SetText(string)`.
	[Token(Token = "0x6000417")]
	[Address(RVA = "0x371D80", Offset = "0x370780", VA = "0x180371D80", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<NoteDataItem> map)
	{
		string text = s_tagPattern.Replace(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Title, "");
		_titleLabel.SetText(text);
		_descriptionLabel.SetText(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Description);
	}

	[Token(Token = "0x6000418")]
	[Address(RVA = "0x371FE0", Offset = "0x3709E0", VA = "0x180371FE0")]
	public NotePage()
	{
	}
}
