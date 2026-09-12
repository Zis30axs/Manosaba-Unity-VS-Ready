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

namespace WitchTrials.Views;

[Token(Token = "0x200010A")]
public class RulePage : WitchBookPageBase<RuleDataItem, RuleState>
{
	[Token(Token = "0x200010B")]
	private record LocalizedTexts
	{
		[Token(Token = "0x1700008C")]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[Token(Token = "0x600044F")]
			[Address(RVA = "0x375840", Offset = "0x374240", VA = "0x180375840", Slot = "5")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700008D")]
		public string Subtitle
		{
			[Token(Token = "0x6000450")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x6000451")]
			[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x1700008E")]
		public string Description
		{
			[Token(Token = "0x6000452")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x6000453")]
			[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x600044E")]
		[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
		public LocalizedTexts([NotNull] string Subtitle, [NotNull] string Description)
		{
		}

		[Token(Token = "0x6000454")]
		[Address(RVA = "0x3758C0", Offset = "0x3742C0", VA = "0x1803758C0", Slot = "3")]
		[CompilerGenerated]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x6000455")]
		[Address(RVA = "0x375A00", Offset = "0x374400", VA = "0x180375A00", Slot = "6")]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return default(bool);
		}

		[Token(Token = "0x6000458")]
		[Address(RVA = "0x375AE0", Offset = "0x3744E0", VA = "0x180375AE0", Slot = "2")]
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return default(int);
		}

		[Token(Token = "0x600045A")]
		[Address(RVA = "0x375C90", Offset = "0x374690", VA = "0x180375C90", Slot = "7")]
		[CompilerGenerated]
		public virtual bool Equals(LocalizedTexts? other)
		{
			return default(bool);
		}

		[Token(Token = "0x600045C")]
		[Address(RVA = "0x36C8D0", Offset = "0x36B2D0", VA = "0x18036C8D0")]
		[CompilerGenerated]
		protected LocalizedTexts(LocalizedTexts original)
		{
		}

		[Token(Token = "0x600045D")]
		[Address(RVA = "0x36C9B0", Offset = "0x36B3B0", VA = "0x18036C9B0")]
		[CompilerGenerated]
		public void Deconstruct(out string Subtitle, out string Description)
		{
			Subtitle = default;
			Description = default;
			Subtitle = default;
			Description = default;
		}
	}

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
		[Token(Token = "0x6000446")]
		[Address(RVA = "0x374FA0", Offset = "0x3739A0", VA = "0x180374FA0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000447")]
	[Address(RVA = "0x375070", Offset = "0x373A70", VA = "0x180375070", Slot = "5")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x6000448")]
	[Address(RVA = "0x375120", Offset = "0x373B20", VA = "0x180375120", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__10))]
	protected override Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<RuleDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<RuleDataItem>>>);
	}

	[Token(Token = "0x6000449")]
	[Address(RVA = "0x375320", Offset = "0x373D20", VA = "0x180375320", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<RuleDataItem> map)
	{
	}

	[Token(Token = "0x600044A")]
	[Address(RVA = "0x375400", Offset = "0x373E00", VA = "0x180375400")]
	private string GetTitleText()
	{
		return null;
	}

	[Token(Token = "0x600044B")]
	[Address(RVA = "0x3755A0", Offset = "0x373FA0", VA = "0x1803755A0", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<RuleDataItem> map)
	{
	}

	[Token(Token = "0x600044C")]
	[Address(RVA = "0x375780", Offset = "0x374180", VA = "0x180375780")]
	public RulePage()
	{
	}
}
