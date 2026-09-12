using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000B2")]
public class SpecialThanksRoll : CreditRoll
{
	[StructLayout(3)]
	[Token(Token = "0x20000B3")]
	[CompilerGenerated]
	private struct _003CRebuildLayoutAsync_003Ed__9
{
		[Token(Token = "0x4000324")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000325")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000326")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpecialThanksRoll _003C_003E4__this;

		[Token(Token = "0x4000327")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000328")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600028E")]
		[Address(RVA = "0x34B1D0", Offset = "0x349BD0", VA = "0x18034B1D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600028F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400031F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Space]
	[SerializeField]
	private TMP_Text[] _labelPrefabs;

	[Token(Token = "0x4000320")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private Transform _labelContainer;

	[Token(Token = "0x4000321")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x4000322")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private int _maxLineCount;

	[Token(Token = "0x4000323")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly List<TMP_Text> _labels;

	[Token(Token = "0x6000288")]
	[Address(RVA = "0x34A550", Offset = "0x348F50", VA = "0x18034A550", Slot = "4")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x6000289")]
	[Address(RVA = "0x34A600", Offset = "0x349000", VA = "0x18034A600", Slot = "6")]
	protected override void EndToScroll()
	{
	}

	[Token(Token = "0x600028A")]
	[Address(RVA = "0x34A740", Offset = "0x349140", VA = "0x18034A740")]
	public void Clear()
	{
	}

	[Token(Token = "0x600028B")]
	[Address(RVA = "0x34A9A0", Offset = "0x3493A0", VA = "0x18034A9A0")]
	public void SetCreditTexts(int sizeLevel, Dictionary<LocaleKind, string>[] creditNames)
	{
	}

	[Token(Token = "0x600028C")]
	[Address(RVA = "0x34AF30", Offset = "0x349930", VA = "0x18034AF30")]
	[AsyncStateMachine(typeof(_003CRebuildLayoutAsync_003Ed__9))]
	public UniTask RebuildLayoutAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600028D")]
	[Address(RVA = "0x34B0D0", Offset = "0x349AD0", VA = "0x18034B0D0")]
	public SpecialThanksRoll()
	{
	}
}
