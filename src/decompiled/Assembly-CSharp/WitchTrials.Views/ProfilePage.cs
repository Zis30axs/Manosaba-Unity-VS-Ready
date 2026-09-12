using System.Collections.Generic;
using System.Diagnostics;
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

namespace WitchTrials.Views;

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
		[Token(Token = "0x6000438")]
		[Address(RVA = "0x3738F0", Offset = "0x3722F0", VA = "0x1803738F0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000439")]
	[Address(RVA = "0x3739C0", Offset = "0x3723C0", VA = "0x1803739C0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__9))]
	protected override Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<ProfileDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<ProfileDataItem>>>);
	}

	[Token(Token = "0x600043A")]
	[Address(RVA = "0x373BC0", Offset = "0x3725C0", VA = "0x180373BC0", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<ProfileDataItem> map)
	{
	}

	[Token(Token = "0x600043B")]
	[Address(RVA = "0x373CB0", Offset = "0x3726B0", VA = "0x180373CB0", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<ProfileDataItem> map)
	{
	}

	[Token(Token = "0x600043C")]
	[Address(RVA = "0x3740E0", Offset = "0x372AE0", VA = "0x1803740E0")]
	public ProfilePage()
	{
	}
}
