using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using Naninovel;
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x200010A")]
    public class RulePage : WitchBookPageBase<RuleDataItem, RuleState>
    {
    	// RECONSTRUCTED | Confidence: A (shape), B (members not individually byte-diffed) | RVA: 0x36C0D0 (primary ctor, shared two-ref-store body)
    	// This is a positional record, the same shape as `CluePage.LocalizedTexts`, with backing fields +0x10 Subtitle and +0x18 Description.
    	// RECONSTRUCTED | RVA: 0x375840 EqualityContract, 0x36C200 init Description, 0x3758C0 ToString, 0x375A00 PrintMembers,
    	// RECONSTRUCTED | RVA: 0x36C480 op_Inequality, 0x36C4C0 op_Equality, 0x375AE0 GetHashCode, 0x375BF0 Equals(object),
    	// RECONSTRUCTED | RVA: 0x375C90 Equals(LocalizedTexts), 0x375DC0 <Clone>$, 0x36C8D0 copy ctor, 0x36C9B0 Deconstruct
    	[Token(Token = "0x200010B")]
    	private record LocalizedTexts(string Subtitle, string Description);

    	[StructLayout(3)]
    	[Token(Token = "0x200010E")]
    	[CompilerGenerated]
    	private struct _003CLoadDataAsync_003Ed__10
    {
    		[Token(Token = "0x4000507")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000508")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IList<VersionedItem<RuleDataItem>>> _003C_003Et__builder;

    		[Token(Token = "0x4000509")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public RulePage _003C_003E4__this;

    		[Token(Token = "0x400050A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x400050B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<RuleDataItem>>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600046B")]
    		[Address(RVA = "0x3769E0", Offset = "0x3753E0", VA = "0x1803769E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600046C")]
    		[Address(RVA = "0x3772B0", Offset = "0x375CB0", VA = "0x1803772B0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40004F2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	[Header("For Rule Page")]
    	[SerializeField]
    	private ManagedTextProvider _managedTextProvider;

    	[Token(Token = "0x40004F3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	[SerializeField]
    	private TMP_Text _titleNumLabel;

    	[Token(Token = "0x40004F4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	[SerializeField]
    	private TMP_Text _subtitleLabel;

    	[Token(Token = "0x40004F5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	[SerializeField]
    	private TMP_Text _descriptionLabel;

    	[Token(Token = "0x40004F6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private ITextManager _naniTextManager;

    	[Token(Token = "0x40004F7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	private IReadOnlyDictionary<IdVersionPair, string> _numberings;

    	[Token(Token = "0x40004F8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private IReadOnlyDictionary<IdVersionPair, IReadOnlyDictionary<LocaleKind, LocalizedTexts>> _localizedTextData;

    	[Token(Token = "0x1700008B")]
    	private ITextManager NaniTextManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x374FA0, 202 B
    		// Lazy cache: `_naniTextManager (+0xD8) ??= Engine.GetServiceOrErr<ITextManager>()`
    		// (the same body is inlined into `GetTitleText`).
    		[Token(Token = "0x6000446")]
    		[Address(RVA = "0x374FA0", Offset = "0x3739A0", VA = "0x180374FA0")]
    		get
    		{
    			return _naniTextManager ??= Engine.GetServiceOrErr<ITextManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x375070, 167 B
    	// A direct (non-virtual) call to `WitchBookPageBase.Reset` 0x379840, then
    	// `_managedTextProvider (+0xB8) = GetComponent<ManagedTextProvider>()`.
    	[Token(Token = "0x6000447")]
    	[Address(RVA = "0x375070", Offset = "0x373A70", VA = "0x180375070", Slot = "5")]
    	protected override void Reset()
    	{
    		base.Reset();
    		_managedTextProvider = GetComponent<ManagedTextProvider>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x375120, 504 B (entry) + MoveNext 0x3769E0, 2241 B
    	// RECONSTRUCTED | RVA: 0x3757C0 `<>n__0` (118 B), the base-call accessor for `base.LoadDataAsync(ct)`
    	// RECONSTRUCTED | RVA: 0x375F60 b__10_0, 0x375FB0 b__10_1, 0x376000 b__10_2, 0x376050 b__10_3, 0x376860 DisplayClass10_0.b__9
    	// The entry and `<>n__0` are byte-identical to CluePage (pagediff: 0 differing lines). After the await, two ToDictionary calls:
    	// - `_numberings (+0xE0) = result.ToDictionary(b__10_0 → IdVersionPair, b__10_1 → Item.Numbering)`.
    	//   `b__10_1` is get_Item (sub_6480) then `[+0x10]`, with an NRE on a null item.
    	// - `_localizedTextData (+0xE8) = result.ToDictionary(b__10_2 → IdVersionPair, b__10_3)`. `b__10_2` is
    	//   byte-identical to `b__10_0`: C# does not merge identical lambdas, so it is a second lambda in source.
    	// - `b__10_3` equals Clue `b__4_1` except for the item offsets `Subtitle` +0x18 / `Description` +0x20
    	//   (RuleDataItem has `_numbering` at +0x10) and the shifted `<>9__10_N` cache slots.
    	// - `b__9` is byte-identical to Clue `b__7` (0 differing lines).
    	// - Returns the awaited list.
    	[Token(Token = "0x6000448")]
    	[Address(RVA = "0x375120", Offset = "0x373B20", VA = "0x180375120", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__10))]
    	protected override async Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<RuleDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
    	{
    		IList<VersionedItem<RuleDataItem>> result = await base.LoadDataAsync(ct);
    		_numberings = result.ToDictionary((VersionedItem<RuleDataItem> versioned) => versioned.IdVersionPair, (VersionedItem<RuleDataItem> versioned) => versioned.Item.Numbering);
    		_localizedTextData = result.ToDictionary((VersionedItem<RuleDataItem> versioned) => versioned.IdVersionPair, delegate(VersionedItem<RuleDataItem> versioned)
    		{
    			IReadOnlyDictionary<LocaleKind, string> names = versioned.Item.Subtitle.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
    			IReadOnlyDictionary<LocaleKind, string> descriptions = versioned.Item.Description.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
    			return (IReadOnlyDictionary<LocaleKind, LocalizedTexts>)names.Keys.Union(descriptions.Keys).ToDictionary((LocaleKind locale) => locale, (LocaleKind locale) => new LocalizedTexts(names.GetValueOrDefault(locale) ?? string.Empty, descriptions.GetValueOrDefault(locale) ?? string.Empty));
    		});
    		return result;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x375320, 223 B
    	// `GetTitleText()` is called first, then `_numberings (+0xE0)[map.IdVersionPair]` (sub_6AA0), then
    	// `string.Concat(title, "<br>", numbering)`, then a tail jump to `button.SetupWithText(text, backgroundIndex: 2)`.
    	[Token(Token = "0x6000449")]
    	[Address(RVA = "0x375320", Offset = "0x373D20", VA = "0x180375320", Slot = "17")]
    	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<RuleDataItem> map)
    	{
    		button.SetupWithText(GetTitleText() + "<br>" + _numberings[map.IdVersionPair], 2);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x375400, 411 B
    	// - `NaniTextManager` (lazy getter inlined), then `_managedTextProvider.Key`, then `.Document`. Key-before-Document
    	//   is the argument order of `TextManagerExtensions.GetRecordValue(manager, key, documentPath)` (0x596010), whose
    	//   160 B body is inlined here instruction for instruction, including its metadata flag data_30DC11F:
    	//   - `manager.GetDocument(documentPath)` (ITextManager slot 1); null → fallback.
    	//   - `ManagedTextDocument.TryGet(key, out record)`; false → fallback.
    	//   - Result `record.Value` (+0x8).
    	// - A non-empty value (`Length > 0`) is returned; otherwise `_managedTextProvider.DefaultValue` (+0x30).
    	[Token(Token = "0x600044A")]
    	[Address(RVA = "0x375400", Offset = "0x373E00", VA = "0x180375400")]
    	private string GetTitleText()
    	{
    		string recordValue = NaniTextManager.GetRecordValue(_managedTextProvider.Key, _managedTextProvider.Document);
    		return string.IsNullOrEmpty(recordValue) ? _managedTextProvider.DefaultValue : recordValue;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3755A0, 471 B
    	// Each label field is loaded before its lookup:
    	// - `_titleNumLabel (+0xC0).SetText(_numberings[map.IdVersionPair])`.
    	// - `_subtitleLabel (+0xC8).SetText(_localizedTextData (+0xE8)[map.IdVersionPair][CurrentLocale].Subtitle)`.
    	// - `_descriptionLabel (+0xD0).SetText(...same lookup....Description)`, as a tail jump.
    	[Token(Token = "0x600044B")]
    	[Address(RVA = "0x3755A0", Offset = "0x373FA0", VA = "0x1803755A0", Slot = "18")]
    	protected override void RefreshPageContent(VersionedItem<RuleDataItem> map)
    	{
    		_titleNumLabel.SetText(_numberings[map.IdVersionPair]);
    		_subtitleLabel.SetText(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Subtitle);
    		_descriptionLabel.SetText(_localizedTextData[map.IdVersionPair][base.CurrentLocale].Description);
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x375780, 62 B
    	// Only the call to WitchBookPageBase<RuleDataItem, RuleState>..ctor; no field initializers.
    	[Token(Token = "0x600044C")]
    	[Address(RVA = "0x375780", Offset = "0x374180", VA = "0x180375780")]
    	public RulePage()
    	{
    	}
    }
}
