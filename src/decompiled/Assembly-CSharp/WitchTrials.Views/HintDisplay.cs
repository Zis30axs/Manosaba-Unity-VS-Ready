using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200008F")]
public class HintDisplay : MonoBehaviour
{
	[Token(Token = "0x2000090")]
	[Flags]
	private enum ButtonKind
	{
		[Token(Token = "0x4000265")]
		None = 0,
		[Token(Token = "0x4000266")]
		App = 1,
		[Token(Token = "0x4000267")]
		WordPrinter = 2,
		[Token(Token = "0x4000268")]
		Witch = 4,
		[Token(Token = "0x4000269")]
		Close = 8,
		[Token(Token = "0x400026A")]
		TextSkip = 7
	}

	[Serializable]
	[Token(Token = "0x2000091")]
	private struct CharacterWait
	{
		[Token(Token = "0x400026B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public char Character;

		[Token(Token = "0x400026C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
		public float WaitTime;
	}

	[StructLayout(3)]
	[Token(Token = "0x2000093")]
	[CompilerGenerated]
	private struct _003CMainLoopAsync_003Ed__31
{
		[Token(Token = "0x4000270")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000271")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000272")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x4000273")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public HintDisplay _003C_003E4__this;

		[Token(Token = "0x4000274")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000275")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<bool>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000206")]
		[Address(RVA = "0x33C620", Offset = "0x33B020", VA = "0x18033C620", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000207")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000094")]
	[CompilerGenerated]
	private struct _003CShowTextAsync_003Ed__32
{
		[Token(Token = "0x4000276")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000277")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000278")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public HintDisplay _003C_003E4__this;

		[Token(Token = "0x4000279")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string request;

		[Token(Token = "0x400027A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x400027B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private int _003Cindex_003E5__2;

		[Token(Token = "0x400027C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private StringBuilder _003CstringBuilder_003E5__3;

		[Token(Token = "0x400027D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400027E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private int _003Clast_003E5__4;

		[Token(Token = "0x6000208")]
		[Address(RVA = "0x33D130", Offset = "0x33BB30", VA = "0x18033D130", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000209")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000095")]
	[CompilerGenerated]
	private struct _003CWaitContinueOrBreakAsync_003Ed__33
{
		[Token(Token = "0x400027F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000280")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x4000281")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public HintDisplay _003C_003E4__this;

		[Token(Token = "0x4000282")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x4000283")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600020A")]
		[Address(RVA = "0x33DD60", Offset = "0x33C760", VA = "0x18033DD60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600020B")]
		[Address(RVA = "0x33E080", Offset = "0x33CA80", VA = "0x18033E080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000252")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private string[] _hintMessages;

	[Token(Token = "0x4000253")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private TextMeshProUGUI _messageLabel;

	[Token(Token = "0x4000254")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private CanvasGroup _onMenuCanvasGroup;

	[Token(Token = "0x4000255")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _enabledCanvasGroup;

	[Token(Token = "0x4000256")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[SerializeField]
	private GameObject _hintExistedNotice;

	[Token(Token = "0x4000257")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private Button _hintApp;

	[Token(Token = "0x4000258")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private Button _screenButton;

	[Token(Token = "0x4000259")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private Button _closeButton;

	[Token(Token = "0x400025A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private Button _wordPrinterButton;

	[Token(Token = "0x400025B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[SerializeField]
	private Button _witchButton;

	[Token(Token = "0x400025C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[SerializeField]
	private float _waitOnChara;

	[Token(Token = "0x400025D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x74")]
	[SerializeField]
	private float _waitSkip;

	[Token(Token = "0x400025E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[SerializeField]
	private CharacterWait[] _charaWaits;

	[Token(Token = "0x400025F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private Dictionary<char, float> _characterWaitDictionary;

	[Token(Token = "0x4000260")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private string _requestMessage;

	[Token(Token = "0x4000261")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private ButtonKind _selectedButtonKind;

	[Token(Token = "0x4000262")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000038")]
	private Dictionary<char, float> CharacterWaitDictionary
	{
		[Token(Token = "0x60001ED")]
		[Address(RVA = "0x33B150", Offset = "0x339B50", VA = "0x18033B150")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000039")]
	public Action OnClose
	{
		[Token(Token = "0x60001EE")]
		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60001EF")]
		[Address(RVA = "0x33B480", Offset = "0x339E80", VA = "0x18033B480")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x60001F0")]
	[Address(RVA = "0x33B4E0", Offset = "0x339EE0", VA = "0x18033B4E0")]
	public void OnEnable()
	{
	}

	[Token(Token = "0x60001F1")]
	[Address(RVA = "0x33B4F0", Offset = "0x339EF0", VA = "0x18033B4F0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x60001F2")]
	[Address(RVA = "0x33B510", Offset = "0x339F10", VA = "0x18033B510")]
	public void OnShow()
	{
	}

	[Token(Token = "0x60001F3")]
	[Address(RVA = "0x33B540", Offset = "0x339F40", VA = "0x18033B540")]
	public void Present()
	{
	}

	[Token(Token = "0x60001F4")]
	[Address(RVA = "0x33B960", Offset = "0x33A360", VA = "0x18033B960")]
	private void RequestNextMessage()
	{
	}

	[Token(Token = "0x60001F5")]
	[Address(RVA = "0x33BA60", Offset = "0x33A460", VA = "0x18033BA60")]
	public void Close()
	{
	}

	[Token(Token = "0x60001F6")]
	[Address(RVA = "0x33BBA0", Offset = "0x33A5A0", VA = "0x18033BBA0")]
	public void Dismiss()
	{
	}

	[Token(Token = "0x60001F7")]
	[Address(RVA = "0x33BBE0", Offset = "0x33A5E0", VA = "0x18033BBE0")]
	private void Initialize()
	{
	}

	[Token(Token = "0x60001F8")]
	[Address(RVA = "0x33BDF0", Offset = "0x33A7F0", VA = "0x18033BDF0")]
	[AsyncStateMachine(typeof(_003CMainLoopAsync_003Ed__31))]
	private UniTask MainLoopAsync(CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60001F9")]
	[Address(RVA = "0x33BFA0", Offset = "0x33A9A0", VA = "0x18033BFA0")]
	[AsyncStateMachine(typeof(_003CShowTextAsync_003Ed__32))]
	private UniTask ShowTextAsync(string request, CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60001FA")]
	[Address(RVA = "0x33C1B0", Offset = "0x33ABB0", VA = "0x18033C1B0")]
	[AsyncStateMachine(typeof(_003CWaitContinueOrBreakAsync_003Ed__33))]
	private UniTask<bool> WaitContinueOrBreakAsync(CancellationToken ct)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x60001FB")]
	[Address(RVA = "0x33C3A0", Offset = "0x33ADA0", VA = "0x18033C3A0")]
	public IObservable<Unit> OnClickAsObservable()
	{
		return null;
	}

	[Token(Token = "0x60001FC")]
	[Address(RVA = "0x33C3D0", Offset = "0x33ADD0", VA = "0x18033C3D0")]
	public HintDisplay()
	{
	}
}
