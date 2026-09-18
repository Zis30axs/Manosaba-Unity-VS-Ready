#nullable enable annotations
﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    [Token(Token = "0x2000107")]
    public sealed class ProfilePage : WitchBookPageBase<ProfileDataItem, ProfileState>
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000109")]
    	[CompilerGenerated]
    	private struct _003CLoadDataAsync_003Ed__9
    {
    		[Token(Token = "0x40004EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40004EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IList<VersionedItem<ProfileDataItem>>> _003C_003Et__builder;

    		[Token(Token = "0x40004EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ProfilePage _003C_003E4__this;

    		[Token(Token = "0x40004ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x40004EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask _003CinitializeAuthorBuilderTask_003E5__2;

    		[Token(Token = "0x40004EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private IList<VersionedItem<ProfileDataItem>> _003Cresult_003E5__3;

    		[Token(Token = "0x40004F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<ProfileDataItem>>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40004F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000444")]
    		[Address(RVA = "0x3745D0", Offset = "0x372FD0", VA = "0x1803745D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000445")]
    		[Address(RVA = "0x374F70", Offset = "0x373970", VA = "0x180374F70", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40004DE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	[Header("For Profile Page")]
    	[SerializeField]
    	private TMP_Text _authorLabel;

    	[Token(Token = "0x40004DF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	[SerializeField]
    	private TMP_Text _descriptionLabel;

    	[Token(Token = "0x40004E0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	[SerializeField]
    	private WitchBookItemThumbnail _thumbnail;

    	[Token(Token = "0x40004E1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	[SerializeField]
    	private AuthorTextBuilder.Settings _authorTextSettings;

    	[Token(Token = "0x40004E2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private ICharacterManager _naniCharacterManager;

    	[Token(Token = "0x40004E3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	private AuthorTextBuilder _authorTextBuilder;

    	[Token(Token = "0x40004E4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private IReadOnlyDictionary<IdVersionPair, IReadOnlyDictionary<LocaleKind, string>> _localizedTextData;

    	[Token(Token = "0x1700008A")]
    	private ICharacterManager NaniCharacterManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3738F0, 202 B
    		// Lazy cache: `_naniCharacterManager (+0xD8) ??= Engine.GetServiceOrErr<ICharacterManager>()`.
    		[Token(Token = "0x6000438")]
    		[Address(RVA = "0x3738F0", Offset = "0x3722F0", VA = "0x1803738F0")]
    		get
    		{
    			return _naniCharacterManager ??= Engine.GetServiceOrErr<ICharacterManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3739C0, 501 B (entry) + MoveNext 0x3745D0, 2456 B
    	// RECONSTRUCTED | RVA: 0x374120 `<>n__0` (118 B), the base-call accessor for `base.LoadDataAsync(ct)`
    	// RECONSTRUCTED | RVA: 0x374240 `<>c.b__9_0`, 0x374290 `<>c.b__9_1`
    	// The entry differs from CluePage only in the larger stack frame for the bigger state struct. MoveNext (hoisted
    	// locals `initializeAuthorBuilderTask` +0x30 and `result` +0x40; awaiters +0x48 and +0x60) runs in this order:
    	// - `_authorTextBuilder (+0xE0) = new AuthorTextBuilder(_authorTextSettings (+0xD0))` (ctor 0x3A35F0), then
    	//   `InitializeAsync(ct)` (0x3A3780) on the re-read field. The UniTask is stored without awaiting.
    	// - State 0: `await base.LoadDataAsync(ct)`, with the result in +0x40.
    	// - State 1: `await initializeAuthorBuilderTask` (UniTask.Awaiter, IUniTaskSource status/GetResult thunks).
    	//   The author text builder is therefore initialized concurrently with the base data load.
    	// - `_localizedTextData (+0xE8) = result.ToDictionary(b__9_0 → IdVersionPair, b__9_1)`. `b__9_1` is
    	//   byte-identical to `MapPage.b__5_1` (`Item._description` +0x10 → ToDictionary(Locale, Text)).
    	// - The hoisted locals are cleared, then SetResult(result).
    	[Token(Token = "0x6000439")]
    	[Address(RVA = "0x3739C0", Offset = "0x3723C0", VA = "0x1803739C0", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__9))]
    	protected override async Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<ProfileDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
    	{
    		_authorTextBuilder = new AuthorTextBuilder(_authorTextSettings);
    		Cysharp.Threading.Tasks.UniTask initializeAuthorBuilderTask = _authorTextBuilder.InitializeAsync(ct);
    		IList<VersionedItem<ProfileDataItem>> result = await base.LoadDataAsync(ct);
    		await initializeAuthorBuilderTask;
    		_localizedTextData = result.ToDictionary((VersionedItem<ProfileDataItem> versioned) => versioned.IdVersionPair, (VersionedItem<ProfileDataItem> versioned) => (IReadOnlyDictionary<LocaleKind, string>)versioned.Item.Description.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text));
    		return result;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x373BC0, 235 B
    	// `map.Id`, then an inlined `WitchBookDataHelper.BuildProfileTextureAddress` (same flag data_30D94EE and literal
    	// "General/WitchBook/Profile_" as 0x38F970), then a tail jump to `button.SetupWithAddressableTexture(address)`.
    	[Token(Token = "0x600043A")]
    	[Address(RVA = "0x373BC0", Offset = "0x3725C0", VA = "0x180373BC0", Slot = "17")]
    	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<ProfileDataItem> map)
    	{
    		button.SetupWithAddressableTexture(WitchBookDataHelper.BuildProfileTextureAddress(map.Id));
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (local-vs-inline spelling of the author name) | RVA: 0x373CB0, 1062 B
    	// - `NaniCharacterManager` (real call to 0x3738F0), then an inlined `ActorManagerExtensions.GetActorMetaOrDefault(
    	//   manager, map.Id)`: `manager.Configuration` (IEngineService slot 0) → `GetMetadataOrDefault(id)`. The
    	//   result is NRE-checked.
    	// - `color = meta.UseCharacterColor (+0x80) ? new Color?(meta.NameColor (+0x84)) : null`.
    	// - `_authorTextBuilder.TryBuildAuthorText(map.Id, color, out text)` (0x3A3920). On false:
    	//   `NaniCharacterManager.GetAuthorName(map.Id)` (ICharacterManager slot 7), then `_authorTextBuilder` is
    	//   re-read after that call, then `BuildTaggedSimpleName(name, htmlStringRgb: null)` → text.
    	// - `_authorLabel (+0xB8).SetText(text)`.
    	// - `_descriptionLabel (+0xC0).SetText(_localizedTextData (+0xE8)[map.IdVersionPair][CurrentLocale])`
    	//   (sub_6C80 slot 2, then sub_36F0 with ecx = 2).
    	// - `_thumbnail (+0xC8).Setup(BuildProfileTextureAddress(map.Id))` (helper inlined).
    	[Token(Token = "0x600043B")]
    	[Address(RVA = "0x373CB0", Offset = "0x3726B0", VA = "0x180373CB0", Slot = "18")]
    	protected override void RefreshPageContent(VersionedItem<ProfileDataItem> map)
    	{
    		CharacterMetadata actorMetaOrDefault = NaniCharacterManager.GetActorMetaOrDefault(map.Id);
    		Color? color = actorMetaOrDefault.UseCharacterColor ? new Color?(actorMetaOrDefault.NameColor) : null;
    		if (!_authorTextBuilder.TryBuildAuthorText(map.Id, color, out string text))
    		{
    			string authorName = NaniCharacterManager.GetAuthorName(map.Id);
    			text = _authorTextBuilder.BuildTaggedSimpleName(authorName, null);
    		}
    		_authorLabel.SetText(text);
    		_descriptionLabel.SetText(_localizedTextData[map.IdVersionPair][base.CurrentLocale]);
    		_thumbnail.Setup(WitchBookDataHelper.BuildProfileTextureAddress(map.Id));
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x3740E0, 62 B
    	// Only the call to WitchBookPageBase<ProfileDataItem, ProfileState>..ctor; no field initializers.
    	[Token(Token = "0x600043C")]
    	[Address(RVA = "0x3740E0", Offset = "0x372AE0", VA = "0x1803740E0")]
    	public ProfilePage()
    	{
    	}
    }
}
