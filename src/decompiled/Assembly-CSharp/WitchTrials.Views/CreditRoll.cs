using LitMotion;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200009E")]
public class CreditRoll : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x200009F")]
	[CompilerGenerated]
	private struct _003CScrollAsync_003Ed__9
{
		[Token(Token = "0x40002A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditRoll _003C_003E4__this;

		[Token(Token = "0x40002A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float duration;

		[Token(Token = "0x40002A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40002AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000234")]
		[Address(RVA = "0x340A30", Offset = "0x33F430", VA = "0x180340A30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000235")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002A1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private GameObject _gameObject;

	[Token(Token = "0x40002A2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x40002A3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private ScrollRect _scrollRect;

	[Token(Token = "0x40002A4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Scrollbar _scrollbar;

	[Token(Token = "0x17000043")]
	public float ContentHeight
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x340550, 50 B
		// `_scrollRect` (+0x30) -> m_Content (+0x20, inlined `content`) -> `sizeDelta`, returning the y half ([rsp+0x34]).
		[Token(Token = "0x600022B")]
		[Address(RVA = "0x340550", Offset = "0x33EF50", VA = "0x180340550")]
		get
		{
			return _scrollRect.content.sizeDelta.y;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x340590, 517 B
	[Token(Token = "0x600022C")]
	[Address(RVA = "0x340590", Offset = "0x33EF90", VA = "0x180340590", Slot = "4")]
	protected virtual void Reset()
	{
		_gameObject = base.gameObject;
		_canvas = GetComponent<Canvas>();
		_scrollRect = GetComponent<ScrollRect>();
		_scrollbar = GetComponentInChildren<Scrollbar>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3407A0, 30 B (identical-code-folded with Naninovel LayeredCameraLayer.set_Enabled)
	[Token(Token = "0x600022D")]
	[Address(RVA = "0x3407A0", Offset = "0x33F1A0", VA = "0x1803407A0")]
	public void SetGameObjectActive(bool active)
	{
		_gameObject.SetActive(active);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3407C0, 30 B (identical-code-folded with StageCameraContainer.SetCameraEnabled)
	[Token(Token = "0x600022E")]
	[Address(RVA = "0x3407C0", Offset = "0x33F1C0", VA = "0x1803407C0")]
	public void SetCanvasEnabled(bool enable)
	{
		_canvas.enabled = enable;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3407E0 (builder stub); MoveNext 0x340A30, 1190 B; lambda <ScrollAsync>b__9_0 0x340A00
	// 
	// - Virtual `BeginToScroll()` (vtable +0x188, slot 5) before and `EndToScroll()` (+0x198, slot 6) after.
	// - `LMotion.Create(1f, 0f, duration)` (from = [rsp+0xC8] = 0x3F800000), `Bind` inlined over an
	//   `Action<float>` on the instance lambda, `AddTo(this)` (MonoBehaviour overload), `ToUniTask(ct)`.
	// - No try/finally: EndToScroll is skipped if the await throws or is cancelled.
	// - The lambda calls the private `Scrollbar.Set(f, sendCallback: true)`, which is the inlined `value` setter.
	[Token(Token = "0x600022F")]
	[Address(RVA = "0x3407E0", Offset = "0x33F1E0", VA = "0x1803407E0")]
	[AsyncStateMachine(typeof(_003CScrollAsync_003Ed__9))]
	public async UniTask ScrollAsync(float duration, [Optional] CancellationToken ct)
	{
		BeginToScroll();
		await LMotion.Create(1f, 0f, duration).Bind(delegate(float f)
		{
			_scrollbar.value = f;
		}).AddTo(this).ToUniTask(ct);
		EndToScroll();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3409A0, 32 B
	[Token(Token = "0x6000230")]
	[Address(RVA = "0x3409A0", Offset = "0x33F3A0", VA = "0x1803409A0", Slot = "5")]
	protected virtual void BeginToScroll()
	{
		_canvas.enabled = true;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3409D0, 32 B
	[Token(Token = "0x6000231")]
	[Address(RVA = "0x3409D0", Offset = "0x33F3D0", VA = "0x1803409D0", Slot = "6")]
	protected virtual void EndToScroll()
	{
		_canvas.enabled = false;
	}

	[Token(Token = "0x6000232")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public CreditRoll()
	{
	}
}
