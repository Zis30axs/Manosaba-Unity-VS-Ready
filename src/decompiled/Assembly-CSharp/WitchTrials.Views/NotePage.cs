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
	[Token(Token = "0x2000103")]
	private record LocalizedTexts
	{
		[Token(Token = "0x17000087")]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[Token(Token = "0x600041C")]
			[Address(RVA = "0x372180", Offset = "0x370B80", VA = "0x180372180", Slot = "5")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000088")]
		public string Title
		{
			[Token(Token = "0x600041D")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x600041E")]
			[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x17000089")]
		public string Description
		{
			[Token(Token = "0x600041F")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x6000420")]
			[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x600041B")]
		[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
		public LocalizedTexts([NotNull] string Title, [NotNull] string Description)
		{
		}

		[Token(Token = "0x6000421")]
		[Address(RVA = "0x372200", Offset = "0x370C00", VA = "0x180372200", Slot = "3")]
		[CompilerGenerated]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x6000422")]
		[Address(RVA = "0x372340", Offset = "0x370D40", VA = "0x180372340", Slot = "6")]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return default(bool);
		}

		[Token(Token = "0x6000425")]
		[Address(RVA = "0x372420", Offset = "0x370E20", VA = "0x180372420", Slot = "2")]
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return default(int);
		}

		[Token(Token = "0x6000427")]
		[Address(RVA = "0x3725D0", Offset = "0x370FD0", VA = "0x1803725D0", Slot = "7")]
		[CompilerGenerated]
		public virtual bool Equals(LocalizedTexts? other)
		{
			return default(bool);
		}

		[Token(Token = "0x6000429")]
		[Address(RVA = "0x36C8D0", Offset = "0x36B2D0", VA = "0x18036C8D0")]
		[CompilerGenerated]
		protected LocalizedTexts(LocalizedTexts original)
		{
		}

		[Token(Token = "0x600042A")]
		[Address(RVA = "0x36C9B0", Offset = "0x36B3B0", VA = "0x18036C9B0")]
		[CompilerGenerated]
		public void Deconstruct(out string Title, out string Description)
		{
			Title = default;
			Description = default;
			Title = default;
			Description = default;
		}
	}

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

	[Token(Token = "0x40004C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly Regex s_tagPattern;

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

	[Token(Token = "0x6000415")]
	[Address(RVA = "0x371A80", Offset = "0x370480", VA = "0x180371A80", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__4))]
	protected override UniTask<IList<VersionedItem<NoteDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<IList<VersionedItem<NoteDataItem>>>);
	}

	[Token(Token = "0x6000416")]
	[Address(RVA = "0x371C80", Offset = "0x370680", VA = "0x180371C80", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<NoteDataItem> map)
	{
	}

	[Token(Token = "0x6000417")]
	[Address(RVA = "0x371D80", Offset = "0x370780", VA = "0x180371D80", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<NoteDataItem> map)
	{
	}

	[Token(Token = "0x6000418")]
	[Address(RVA = "0x371FE0", Offset = "0x3709E0", VA = "0x180371FE0")]
	public NotePage()
	{
	}
}
