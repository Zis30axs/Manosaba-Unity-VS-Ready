using GigaCreation.Tools.UndoExtensions;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000F5")]
public class MapArea : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20000F7")]
	[CompilerGenerated]
	private struct _003COnClickAsync_003Ed__9
{
		[Token(Token = "0x400049E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400049F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<MapAreaKind> _003C_003Et__builder;

		[Token(Token = "0x40004A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MapArea _003C_003E4__this;

		[Token(Token = "0x40004A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003E8")]
		[Address(RVA = "0x36E320", Offset = "0x36CD20", VA = "0x18036E320", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003E9")]
		[Address(RVA = "0x36E680", Offset = "0x36D080", VA = "0x18036E680", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000499")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private MapAreaFocusButton _focusButton;

	[Token(Token = "0x400049A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private MapAreaLabel _areaLabel;

	[Token(Token = "0x400049B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Header("Parameters")]
	[SerializeField]
	private MapAreaKind _areaKind;

	[Token(Token = "0x17000085")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x36DB50 (`mov eax,[rcx+0x30]; ret`)
	public MapAreaKind AreaKind
	{
		[Token(Token = "0x60003DD")]
		[Address(RVA = "0x36DB50", Offset = "0x36C550", VA = "0x18036DB50")]
		get
		{
			return _areaKind;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36DB60
	// 
	// Two `Component.GetComponentInChildren<T>()` METHODINFOs (MapAreaFocusButton,
	// MapAreaLabel) stored to +0x20 / +0x28, each followed by the write barrier.
	[Token(Token = "0x60003DE")]
	[Address(RVA = "0x36DB60", Offset = "0x36C560", VA = "0x18036DB60")]
	private void Reset()
	{
		_focusButton = GetComponentInChildren<MapAreaFocusButton>();
		_areaLabel = GetComponentInChildren<MapAreaLabel>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36DC90
	// 
	// `_areaLabel` (+0x28) -> its `_canvasGroup` (+0x20) -> set_alpha(1f): the
	// body of MapAreaLabel.Show (0x36E750) inlined.
	[Token(Token = "0x60003DF")]
	[Address(RVA = "0x36DC90", Offset = "0x36C690", VA = "0x18036DC90")]
	public void Show()
	{
		_areaLabel.Show();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36DCC0
	// 
	// Mirror of Show with `xorps xmm1` (alpha 0f): MapAreaLabel.Hide (0x3491C0) inlined.
	[Token(Token = "0x60003E0")]
	[Address(RVA = "0x36DCC0", Offset = "0x36C6C0", VA = "0x18036DCC0")]
	public void Hide()
	{
		_areaLabel.Hide();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36DCF0
	// 
	// Branches on `dl` into the inlined MapAreaFocusButton.Enable / Disable bodies
	// (`_canvasGroup` +0x118: set_interactable + set_blocksRaycasts with 1 / 0).
	[Token(Token = "0x60003E1")]
	[Address(RVA = "0x36DCF0", Offset = "0x36C6F0", VA = "0x18036DCF0")]
	public void ChangeFocusButtonActivity(bool enable)
	{
		if (enable)
		{
			_focusButton.Enable();
		}
		else
		{
			_focusButton.Disable();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36DD60 (builder stub); MoveNext 0x36E320
	// 
	// Cysharp `AsyncUniTaskMethodBuilder<MapAreaKind>`. MoveNext reads `_focusButton`
	// (+0x20) -> `onClick` (Button +0x100), news `AsyncUnityEventHandler(onClick, ct,
	// callOnce: true)` and awaits `OnInvokeAsync()` - exactly the inlined body of
	// Cysharp's `UnityAsyncExtensions.OnClickAsync(Button, CancellationToken)`.
	// After the await it returns `_areaKind` (+0x30) via SetResult.
	[Token(Token = "0x60003E2")]
	[Address(RVA = "0x36DD60", Offset = "0x36C760", VA = "0x18036DD60")]
	[AsyncStateMachine(typeof(_003COnClickAsync_003Ed__9))]
	public async UniTask<MapAreaKind> OnClickAsync([Optional] CancellationToken ct)
	{
		await _focusButton.OnClickAsync(ct);
		return _areaKind;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36DF50; closures <>c__DisplayClass10_0.b__0 0x36E140, b__1 0x36E180
	// 
	// The DisplayClass captures `showFocusButton` (+0x10) and `labelMaxWidths` (+0x18).
	// Two `ObjectExtensions.DoActionAsUndoable<T>` calls with the literal undo names
	// 'Modify Map Area Focus Button' / 'Modify Map Area Label'. b__0 is the 57-byte
	// alpha set of MapAreaFocusButton.ModifyInEditor; b__1 (410 B) is
	// MapAreaLabel.ModifyInEditor inlined. The same shape is inlined once more per
	// element in MapAreaModifier.ModifyInEditor (0x36E9F0).
	[Token(Token = "0x60003E3")]
	[Address(RVA = "0x36DF50", Offset = "0x36C950", VA = "0x18036DF50")]
	public void ModifyInEditor(bool showFocusButton, float[] labelMaxWidths)
	{
		_focusButton.DoActionAsUndoable("Modify Map Area Focus Button", button => button.ModifyInEditor(showFocusButton));
		_areaLabel.DoActionAsUndoable("Modify Map Area Label", label => label.ModifyInEditor(labelMaxWidths));
	}

	[Token(Token = "0x60003E4")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MapArea()
	{
	}
}
