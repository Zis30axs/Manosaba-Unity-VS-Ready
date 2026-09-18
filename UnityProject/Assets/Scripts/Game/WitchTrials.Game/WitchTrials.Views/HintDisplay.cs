using System.Linq;
using System.Text.RegularExpressions;
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

namespace WitchTrials.Views
{
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
    	private float _waitOnChara = 0.1f;

    	[Token(Token = "0x400025D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x74")]
    	[SerializeField]
    	private float _waitSkip = 0.5f;

    	[Token(Token = "0x400025E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	[SerializeField]
    	private CharacterWait[] _charaWaits = new CharacterWait[2]
    	{
    		new CharacterWait { Character = '。', WaitTime = 0.3f },
    		new CharacterWait { Character = '】', WaitTime = 0.3f }
    	};

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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33B150, lambdas <>c.b__18_0 0x33C600 / b__18_1 0x33C610
    		// Lazy +0x80 ??= `_charaWaits` (+0x78).ToDictionary(Character, WaitTime) with two cached static delegates.
    		[Token(Token = "0x60001ED")]
    		[Address(RVA = "0x33B150", Offset = "0x339B50", VA = "0x18033B150")]
    		get
    		{
    			return _characterWaitDictionary ??= _charaWaits.ToDictionary((CharacterWait wait) => wait.Character, (CharacterWait wait) => wait.WaitTime);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33B470 get (shared +0xA0 getter), 0x33B480 set (shared setter body)
    	// [CompilerGenerated] auto-property, backing field +0xA0.
    	[Token(Token = "0x17000039")]
    	public Action OnClose
    	{
    		[Token(Token = "0x60001EE")]
    		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x60001EF")]
    		[Address(RVA = "0x33B480", Offset = "0x339E80", VA = "0x18033B480")]
    		[CompilerGenerated]
    		set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33B4E0
    	// Tail jump to Initialize.
    	[Token(Token = "0x60001F0")]
    	[Address(RVA = "0x33B4E0", Offset = "0x339EE0", VA = "0x18033B4E0")]
    	public void OnEnable()
    	{
    		Initialize();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33B4F0
    	// `_cts` (+0x98) null-checked, then CancellationTokenSource.Dispose.
    	[Token(Token = "0x60001F1")]
    	[Address(RVA = "0x33B4F0", Offset = "0x339EF0", VA = "0x18033B4F0")]
    	private void OnDestroy()
    	{
    		_cts?.Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33B510
    	// `_hintExistedNotice` (+0x40).SetActive(`_hintMessages` (+0x20).Length > 0) (`seta`).
    	[Token(Token = "0x60001F2")]
    	[Address(RVA = "0x33B510", Offset = "0x339F10", VA = "0x18033B510")]
    	public void OnShow()
    	{
    		_hintExistedNotice.SetActive(_hintMessages.Length > 0);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33B540, 1046 B
    	// - `_messageLabel` (+0x28).SetText(string.Empty).
    	// - `_enabledCanvasGroup` (+0x38) alpha 1 / interactable / blocksRaycasts true; `_onMenuCanvasGroup` (+0x30) all off.
    	// - RequestNextMessage; `_cts?.Cancel()` (disposed check + NotifyCancellation(false) inlined); new CancellationTokenSource
    	//   (ctor inlined) into +0x98.
    	// - Cysharp `CancellationTokenExtensions.AddTo(IDisposable, CancellationToken)` inlined: RegisterWithoutCapture-
    	//   ExecutionContext(destroyCancellationToken, static disposeCallback (+0x8), _cts); the registration is discarded.
    	// - MainLoopAsync(_cts.Token) kickoff inlined -> Cysharp Forget.
    	[Token(Token = "0x60001F3")]
    	[Address(RVA = "0x33B540", Offset = "0x339F40", VA = "0x18033B540")]
    	public void Present()
    	{
    		_messageLabel.SetText(string.Empty);
    		_enabledCanvasGroup.alpha = 1f;
    		_enabledCanvasGroup.interactable = true;
    		_enabledCanvasGroup.blocksRaycasts = true;
    		_onMenuCanvasGroup.alpha = 0f;
    		_onMenuCanvasGroup.interactable = false;
    		_onMenuCanvasGroup.blocksRaycasts = false;
    		RequestNextMessage();
    		_cts?.Cancel();
    		_cts = new CancellationTokenSource();
    		_cts.AddTo(destroyCancellationToken);
    		MainLoopAsync(_cts.Token).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33B960
    	// Empty `_hintMessages` returns; otherwise +0x88 = _hintMessages[icall `UnityEngine.Random::RandomRangeInt(0, Length)`]
    	// (icall name read from the resolver string at 0x2604968).
    	[Token(Token = "0x60001F4")]
    	[Address(RVA = "0x33B960", Offset = "0x33A360", VA = "0x18033B960")]
    	private void RequestNextMessage()
    	{
    		if (_hintMessages.Length != 0)
    		{
    			_requestMessage = _hintMessages[UnityEngine.Random.Range(0, _hintMessages.Length)];
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33BA60
    	// Mirror of Present's groups (`_onMenuCanvasGroup` on, `_enabledCanvasGroup` off), then `_cts?.Cancel()` and `_cts = null`.
    	[Token(Token = "0x60001F5")]
    	[Address(RVA = "0x33BA60", Offset = "0x33A460", VA = "0x18033BA60")]
    	public void Close()
    	{
    		_onMenuCanvasGroup.alpha = 1f;
    		_onMenuCanvasGroup.interactable = true;
    		_onMenuCanvasGroup.blocksRaycasts = true;
    		_enabledCanvasGroup.alpha = 0f;
    		_enabledCanvasGroup.interactable = false;
    		_enabledCanvasGroup.blocksRaycasts = false;
    		_cts?.Cancel();
    		_cts = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33BBA0 (folded with <Initialize>b__30_0)
    	// Close (real call), then OnClose (+0xA0) invoked when non-null.
    	[Token(Token = "0x60001F6")]
    	[Address(RVA = "0x33BBA0", Offset = "0x33A5A0", VA = "0x18033BBA0")]
    	public void Dismiss()
    	{
    		Close();
    		OnClose?.Invoke();
    	}

    	// RECONSTRUCTED | Confidence: A (OnClickAsObservable spelling: B) | RVA: 0x33BBE0, lambdas b__30_0 0x33BBA0 (= Dismiss body),
    	// b__30_1 0x33C510, b__30_2 0x33C520, b__30_3 0x33C530
    	// - `_closeButton` (+0x58) onClick (+0x100) AsObservable -> Subscribe(_ => Dismiss()) -> AddTo(this).
    	// - UnityEvent.AddListener instance lambdas storing `_selectedButtonKind` (+0x90): `_screenButton` (+0x50) -> 1 App,
    	//   `_witchButton` (+0x68) -> 4 Witch, `_wordPrinterButton` (+0x60) -> 2 WordPrinter.
    	[Token(Token = "0x60001F7")]
    	[Address(RVA = "0x33BBE0", Offset = "0x33A5E0", VA = "0x18033BBE0")]
    	private void Initialize()
    	{
    		_closeButton.OnClickAsObservable().Subscribe(_ => Dismiss()).AddTo(this);
    		_screenButton.onClick.AddListener(() => _selectedButtonKind = ButtonKind.App);
    		_witchButton.onClick.AddListener(() => _selectedButtonKind = ButtonKind.Witch);
    		_wordPrinterButton.onClick.AddListener(() => _selectedButtonKind = ButtonKind.WordPrinter);
    	}

    	// RECONSTRUCTED | Confidence: A (tail spelling: B) | RVA: 0x33BDF0 (stub), MoveNext 0x33C620, lambda b__31_0 0x33C540
    	// - Loop while !ct.IsCancellationRequested (state -1 enters at the check): state 0 `await UniTask.Yield(ct)`.
    	// - Empty `_requestMessage` (+0x88) -> state 1 `await UniTask.WaitWhile(() => string.IsNullOrEmpty(_requestMessage),
    	//   Update (8), ct)`.
    	// - State 2 `await ShowTextAsync(_requestMessage, ct)`; state 3 `await WaitContinueOrBreakAsync(ct)` (kickoff inlined):
    	//   true -> RequestNextMessage and loop, false -> leave.
    	// - After the loop: Close then OnClose?.Invoke(): Dismiss inlined.
    	[Token(Token = "0x60001F8")]
    	[Address(RVA = "0x33BDF0", Offset = "0x33A7F0", VA = "0x18033BDF0")]
    	[AsyncStateMachine(typeof(_003CMainLoopAsync_003Ed__31))]
    	private async UniTask MainLoopAsync(CancellationToken ct)
    	{
    		while (!ct.IsCancellationRequested)
    		{
    			await UniTask.Yield(ct);
    			if (string.IsNullOrEmpty(_requestMessage))
    			{
    				await UniTask.WaitWhile(() => string.IsNullOrEmpty(_requestMessage), PlayerLoopTiming.Update, ct);
    			}
    			await ShowTextAsync(_requestMessage, ct);
    			if (!await WaitContinueOrBreakAsync(ct))
    			{
    				break;
    			}
    			RequestNextMessage();
    		}
    		Dismiss();
    	}

    	// RECONSTRUCTED | Confidence: A (IndexOf and RoundToInt spelling: B) | RVA: 0x33BFA0 (stub), MoveNext 0x33D130
    	// Hoisted: index +0x30, stringBuilder +0x38, last +0x50.
    	// - Entry: index = 0; new StringBuilder; `_messageLabel.SetText(stringBuilder)` (TMP SetText(StringBuilder) inlined);
    	//   `_selectedButtonKind` = None.
    	// - Loop until ct cancelled or index == request.Length:
    	//   - Any of App|WordPrinter|Witch (7, `TextSkip`) set -> Append(Regex.Replace(request.Substring(index),
    	//     "\\[(?:\\d+\\.?\\d*|\\.\\d+)+\\]", string.Empty)), SetText, state 0 `await UniTask.WaitForSeconds(_waitSkip +0x74,
    	//     cancellationToken: ct)`, then return.
    	//   - Otherwise c = request[index]; for '[' (0x5B): last = sub.IndexOf("]") ordinal (CompareInfo.IndexOfOrdinal inlined);
    	//     float.TryParse(sub.Substring(1, last - 1)) true -> state 1 WaitForSeconds(value); then index += last + 1 and
    	//     c = request[index].
    	//   - Append(c), SetText (inlined), TMP slot 106 ForceMeshUpdate(false, false), index++; wait = _waitOnChara (+0x70)
    	//     plus the dictionary's extra for c when present; state 2 `await UniTask.Delay(RoundToInt(wait * 1000f), false,
    	//     Update, ct)` (rounding helper 0x355C90 = (int)Math.Round(double)).
    	// - stringBuilder cleared on completion.
    	[Token(Token = "0x60001F9")]
    	[Address(RVA = "0x33BFA0", Offset = "0x33A9A0", VA = "0x18033BFA0")]
    	[AsyncStateMachine(typeof(_003CShowTextAsync_003Ed__32))]
    	private async UniTask ShowTextAsync(string request, CancellationToken ct)
    	{
    		int index = 0;
    		StringBuilder stringBuilder = new StringBuilder();
    		_messageLabel.SetText(stringBuilder);
    		_selectedButtonKind = ButtonKind.None;
    		while (!ct.IsCancellationRequested && index != request.Length)
    		{
    			if ((_selectedButtonKind & ButtonKind.TextSkip) != 0)
    			{
    				stringBuilder.Append(Regex.Replace(request.Substring(index), "\\[(?:\\d+\\.?\\d*|\\.\\d+)+\\]", string.Empty));
    				_messageLabel.SetText(stringBuilder);
    				await UniTask.WaitForSeconds(_waitSkip, cancellationToken: ct);
    				return;
    			}
    			char character = request[index];
    			if (character == '[')
    			{
    				string rest = request.Substring(index);
    				int last = rest.IndexOf("]", StringComparison.Ordinal);
    				if (float.TryParse(rest.Substring(1, last - 1), out float seconds))
    				{
    					await UniTask.WaitForSeconds(seconds, cancellationToken: ct);
    				}
    				index += last + 1;
    				character = request[index];
    			}
    			stringBuilder.Append(character);
    			_messageLabel.SetText(stringBuilder);
    			_messageLabel.ForceMeshUpdate();
    			index++;
    			float wait = _waitOnChara;
    			if (CharacterWaitDictionary.TryGetValue(character, out float extraWait))
    			{
    				wait += extraWait;
    			}
    			await UniTask.Delay(Mathf.RoundToInt(wait * 1000f), false, PlayerLoopTiming.Update, ct);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (flag-test spelling: B) | RVA: 0x33C1B0 (stub), MoveNext 0x33DD60
    	// Loop while !ct.IsCancellationRequested: `_selectedButtonKind` = None, `await UniTask.Yield(ct)`; bit 4 (Witch) -> true,
    	// bit 2 (WordPrinter) -> false; cancellation -> false.
    	[Token(Token = "0x60001FA")]
    	[Address(RVA = "0x33C1B0", Offset = "0x33ABB0", VA = "0x18033C1B0")]
    	[AsyncStateMachine(typeof(_003CWaitContinueOrBreakAsync_003Ed__33))]
    	private async UniTask<bool> WaitContinueOrBreakAsync(CancellationToken ct)
    	{
    		while (!ct.IsCancellationRequested)
    		{
    			_selectedButtonKind = ButtonKind.None;
    			await UniTask.Yield(ct);
    			if ((_selectedButtonKind & ButtonKind.Witch) != 0)
    			{
    				return true;
    			}
    			if ((_selectedButtonKind & ButtonKind.WordPrinter) != 0)
    			{
    				return false;
    			}
    		}
    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A (spelling: B) | RVA: 0x33C3A0
    	// `_hintApp` (+0x48) onClick (+0x100) -> UniRx UnityEventExtensions.AsObservable (tail jump).
    	[Token(Token = "0x60001FB")]
    	[Address(RVA = "0x33C3A0", Offset = "0x33ADA0", VA = "0x18033C3A0")]
    	public IObservable<Unit> OnClickAsObservable()
    	{
    		return _hintApp.OnClickAsObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x33C3D0, 319 B
    	// Field initializers only: `_waitOnChara` = 0.1f (0x3DCCCCCD), `_waitSkip` = 0.5f (0x3F000000), `_charaWaits` =
    	// { ('。' U+3002, 0.3f), ('】' U+3011, 0.3f) } (0x3E99999A), then the empty MonoBehaviour ctor chain.
    	[Token(Token = "0x60001FC")]
    	[Address(RVA = "0x33C3D0", Offset = "0x33ADD0", VA = "0x18033C3D0")]
    	public HintDisplay()
    	{
    	}
    }
}
