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
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000EE")]
public sealed class CluePage : WitchBookPageBase<ClueDataItem, ClueState>
{
	[Token(Token = "0x20000EF")]
	private record LocalizedTexts
	{
		[Token(Token = "0x17000082")]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[Token(Token = "0x60003B9")]
			[Address(RVA = "0x36C180", Offset = "0x36AB80", VA = "0x18036C180", Slot = "5")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000083")]
		public string Name
		{
			[Token(Token = "0x60003BA")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x60003BB")]
			[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x17000084")]
		public string Description
		{
			[Token(Token = "0x60003BC")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x60003BD")]
			[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x60003B8")]
		[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
		public LocalizedTexts([NotNull] string Name, [NotNull] string Description)
		{
		}

		[Token(Token = "0x60003BE")]
		[Address(RVA = "0x36C260", Offset = "0x36AC60", VA = "0x18036C260", Slot = "3")]
		[CompilerGenerated]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x60003BF")]
		[Address(RVA = "0x36C3A0", Offset = "0x36ADA0", VA = "0x18036C3A0", Slot = "6")]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return default(bool);
		}

		[Token(Token = "0x60003C2")]
		[Address(RVA = "0x36C4F0", Offset = "0x36AEF0", VA = "0x18036C4F0", Slot = "2")]
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return default(int);
		}

		[Token(Token = "0x60003C4")]
		[Address(RVA = "0x36C6A0", Offset = "0x36B0A0", VA = "0x18036C6A0", Slot = "7")]
		[CompilerGenerated]
		public virtual bool Equals(LocalizedTexts? other)
		{
			return default(bool);
		}

		[Token(Token = "0x60003C6")]
		[Address(RVA = "0x36C8D0", Offset = "0x36B2D0", VA = "0x18036C8D0")]
		[CompilerGenerated]
		protected LocalizedTexts(LocalizedTexts original)
		{
		}

		[Token(Token = "0x60003C7")]
		[Address(RVA = "0x36C9B0", Offset = "0x36B3B0", VA = "0x18036C9B0")]
		[CompilerGenerated]
		public void Deconstruct(out string Name, out string Description)
		{
			Name = default;
			Description = default;
			Name = default;
			Description = default;
		}
	}

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

	[Token(Token = "0x60003B3")]
	[Address(RVA = "0x36BB30", Offset = "0x36A530", VA = "0x18036BB30", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__4))]
	protected override UniTask<IList<VersionedItem<ClueDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<IList<VersionedItem<ClueDataItem>>>);
	}

	[Token(Token = "0x60003B4")]
	[Address(RVA = "0x36BD30", Offset = "0x36A730", VA = "0x18036BD30", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<ClueDataItem> map)
	{
	}

	[Token(Token = "0x60003B5")]
	[Address(RVA = "0x36BDF0", Offset = "0x36A7F0", VA = "0x18036BDF0", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<ClueDataItem> map)
	{
	}

	[Token(Token = "0x60003B6")]
	[Address(RVA = "0x36C010", Offset = "0x36AA10", VA = "0x18036C010")]
	public CluePage()
	{
	}
}
