using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Dialogs.Views;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200008B")]
public class GoogleForm : MonoBehaviour
{
	[Token(Token = "0x200008C")]
	private enum EntryKind
	{
		[Token(Token = "0x4000243")]
		Version,
		[Token(Token = "0x4000244")]
		Platform,
		[Token(Token = "0x4000245")]
		Resolution,
		[Token(Token = "0x4000246")]
		PlaybackSpot,
		[Token(Token = "0x4000247")]
		PrintedMessage
	}

	[Serializable]
	[Token(Token = "0x200008D")]
	public class EntryInfo
	{
		[Token(Token = "0x4000248")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private static Regex s_scenarioNamePattern;

		[Token(Token = "0x4000249")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private EntryKind _kind;

		[Token(Token = "0x400024A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		private string _id;

		[Token(Token = "0x17000036")]
		public string Id
		{
			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18 `_id`)
			[Token(Token = "0x60001E7")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			get
			{
				return _id;
			}
		}

		[Token(Token = "0x17000037")]
		public string Data
		{
			[Token(Token = "0x60001E8")]
			[Address(RVA = "0x33A4F0", Offset = "0x338EF0", VA = "0x18033A4F0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x60001E9")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public EntryInfo()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200008E")]
	[CompilerGenerated]
	private struct _003COpenGoogleFormAsync_003Ed__9
{
		[Token(Token = "0x400024B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400024C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400024D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GoogleForm _003C_003E4__this;

		[Token(Token = "0x400024E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400024F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private INaninovelInteractionBlocker _003Cblocker_003E5__2;

		[Token(Token = "0x4000250")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ProvidableDialog _003Cdialog_003E5__3;

		[Token(Token = "0x4000251")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<GigaCreation.Essentials.Input.ResponseKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60001EB")]
		[Address(RVA = "0x33A8D0", Offset = "0x3392D0", VA = "0x18033A8D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001EC")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400023D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Button _button;

	[Token(Token = "0x400023E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("Parameters")]
	[SerializeField]
	private string _formUrl;

	[Token(Token = "0x400023F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private EntryInfo[] _entryInfo;

	[Token(Token = "0x4000240")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private DialogData _openGoogleFormDialogData;

	[Token(Token = "0x4000241")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IDialogProvider _dialogProvider;

	[Token(Token = "0x17000035")]
	private IDialogProvider DialogProvider
	{
		[Token(Token = "0x60001E0")]
		[Address(RVA = "0x339E20", Offset = "0x338820", VA = "0x180339E20")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001E1")]
	[Address(RVA = "0x339EE0", Offset = "0x3388E0", VA = "0x180339EE0")]
	private void Awake()
	{
	}

	[Token(Token = "0x60001E2")]
	[Address(RVA = "0x339F80", Offset = "0x338980", VA = "0x180339F80")]
	private void Reset()
	{
	}

	[Token(Token = "0x60001E3")]
	[Address(RVA = "0x33A020", Offset = "0x338A20", VA = "0x18033A020")]
	[AsyncStateMachine(typeof(_003COpenGoogleFormAsync_003Ed__9))]
	public UniTask OpenGoogleFormAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60001E4")]
	[Address(RVA = "0x33A1D0", Offset = "0x338BD0", VA = "0x18033A1D0")]
	private string BuildUrl()
	{
		return null;
	}

	[Token(Token = "0x60001E5")]
	[Address(RVA = "0x33A4B0", Offset = "0x338EB0", VA = "0x18033A4B0")]
	public IObservable<Unit> OnClickAsObservable()
	{
		return null;
	}

	[Token(Token = "0x60001E6")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public GoogleForm()
	{
	}
}
