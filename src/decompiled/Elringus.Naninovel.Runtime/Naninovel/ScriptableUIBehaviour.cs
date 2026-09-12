using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Naninovel;

[Token(Token = "0x20002BD")]
public class ScriptableUIBehaviour : UIBehaviour
{
	[Token(Token = "0x20002BE")]
	public enum FocusMode
	{
		[Token(Token = "0x4000AB9")]
		Visibility,
		[Token(Token = "0x4000ABA")]
		Navigation
	}

	[StructLayout(3)]
	[Token(Token = "0x20002BF")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__64
{
		[Token(Token = "0x4000ABB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000ABC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000ABD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ScriptableUIBehaviour _003C_003E4__this;

		[Token(Token = "0x4000ABE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool visible;

		[Token(Token = "0x4000ABF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float? duration;

		[Token(Token = "0x4000AC0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x4000AC1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000D23")]
		[Address(RVA = "0x5421D0", Offset = "0x540BD0", VA = "0x1805421D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000D24")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000AAB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Tooltip("Whether to permanently disable interaction with the object, no matter the visibility. Requires `Canvas Group` component on the same game object.")]
	[SerializeField]
	private bool disableInteraction;

	[Token(Token = "0x4000AAC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x31")]
	[Tooltip("Whether UI element should be visible or hidden on awake.")]
	[SerializeField]
	private bool visibleOnAwake;

	[Token(Token = "0x4000AAD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x32")]
	[Tooltip("Whether to change opacity (alpha) of Canvas Group in correspondence to visibility of the UI element. Requires `Canvas Group` component on the same game object.")]
	[SerializeField]
	private bool controlOpacity;

	[Token(Token = "0x4000AAE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[Tooltip("When `Control Opacity` is enabled, controls opacity fade duration (in seconds) when changing visibility.")]
	[SerializeField]
	private float fadeTime;

	[Token(Token = "0x4000AAF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Tooltip("When `Control Opacity` is enabled, controls whether to ignore time scale when changing visibility.")]
	[SerializeField]
	private bool ignoreTimeScale;

	[Token(Token = "0x4000AB0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Tooltip("When assigned, will make the object focused (for keyboard or gamepad control) when the UI becomes visible or upon navigation.")]
	[SerializeField]
	private GameObject focusObject;

	[Token(Token = "0x4000AB1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[Tooltip("When `Focus Object` is assigned, determines when to focus the object: on the UI becomes visible or on first navigation attempt (arrow keys or d-pad) while the UI is visible. Be aware, that gamepad support for Navigation mode requires Unity's new input system package installed.")]
	[SerializeField]
	private FocusMode focusMode;

	[Token(Token = "0x4000AB2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Tooltip("Invoked when the UI element is shown.")]
	[SerializeField]
	private UnityEvent onShow;

	[Token(Token = "0x4000AB3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[Tooltip("Invoked when the UI element is hidden.")]
	[SerializeField]
	private UnityEvent onHide;

	[Token(Token = "0x4000AB4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly Tweener<FloatTween> fadeTweener;

	[Token(Token = "0x4000AB5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private RectTransform rectTransform;

	[Token(Token = "0x4000AB6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private Canvas topmostCanvasCache;

	[Token(Token = "0x4000AB7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private bool visibleSelf;

	[Token(Token = "0x170001C7")]
	public virtual float FadeTime
	{
		[Token(Token = "0x6000CF2")]
		[Address(RVA = "0x4C0D50", Offset = "0x4BF750", VA = "0x1804C0D50", Slot = "19")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000CF3")]
		[Address(RVA = "0x4C0D60", Offset = "0x4BF760", VA = "0x1804C0D60", Slot = "20")]
		set
		{
		}
	}

	[Token(Token = "0x170001C8")]
	public virtual bool IgnoreTimeScale
	{
		[Token(Token = "0x6000CF4")]
		[Address(RVA = "0x43D680", Offset = "0x43C080", VA = "0x18043D680", Slot = "21")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000CF5")]
		[Address(RVA = "0x43D690", Offset = "0x43C090", VA = "0x18043D690", Slot = "22")]
		set
		{
		}
	}

	[Token(Token = "0x170001C9")]
	public virtual bool VisibleOnAwake
	{
		[Token(Token = "0x6000CF6")]
		[Address(RVA = "0x5405B0", Offset = "0x53EFB0", VA = "0x1805405B0", Slot = "23")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170001CA")]
	public virtual FocusMode FocusModeType
	{
		[Token(Token = "0x6000CF7")]
		[Address(RVA = "0x5405C0", Offset = "0x53EFC0", VA = "0x1805405C0", Slot = "24")]
		get
		{
			return default(FocusMode);
		}
		[Token(Token = "0x6000CF8")]
		[Address(RVA = "0x5405D0", Offset = "0x53EFD0", VA = "0x1805405D0", Slot = "25")]
		set
		{
		}
	}

	[Token(Token = "0x170001CB")]
	public virtual GameObject FocusObject
	{
		[Token(Token = "0x6000CF9")]
		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "26")]
		get
		{
			return null;
		}
		[Token(Token = "0x6000CFA")]
		[Address(RVA = "0x5405E0", Offset = "0x53EFE0", VA = "0x1805405E0", Slot = "27")]
		set
		{
		}
	}

	[Token(Token = "0x170001CC")]
	public virtual bool Visible
	{
		[Token(Token = "0x6000CFB")]
		[Address(RVA = "0x45DD00", Offset = "0x45C700", VA = "0x18045DD00", Slot = "28")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000CFC")]
		[Address(RVA = "0x3619B0", Offset = "0x3603B0", VA = "0x1803619B0", Slot = "29")]
		set
		{
		}
	}

	[Token(Token = "0x170001CD")]
	public virtual float Opacity
	{
		[Token(Token = "0x6000CFD")]
		[Address(RVA = "0x540640", Offset = "0x53F040", VA = "0x180540640", Slot = "30")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x170001CE")]
	public virtual bool Interactable
	{
		[Token(Token = "0x6000CFE")]
		[Address(RVA = "0x540760", Offset = "0x53F160", VA = "0x180540760", Slot = "31")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000CFF")]
		[Address(RVA = "0x540880", Offset = "0x53F280", VA = "0x180540880", Slot = "32")]
		set
		{
		}
	}

	[Token(Token = "0x170001CF")]
	public virtual bool DisableInteraction
	{
		[Token(Token = "0x6000D00")]
		[Address(RVA = "0x43E270", Offset = "0x43CC70", VA = "0x18043E270", Slot = "33")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170001D0")]
	public virtual RectTransform RectTransform
	{
		[Token(Token = "0x6000D01")]
		[Address(RVA = "0x5408A0", Offset = "0x53F2A0", VA = "0x1805408A0", Slot = "34")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170001D1")]
	public virtual Canvas TopmostCanvas
	{
		[Token(Token = "0x6000D02")]
		[Address(RVA = "0x5409E0", Offset = "0x53F3E0", VA = "0x1805409E0", Slot = "35")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170001D2")]
	public virtual int SortingOrder
	{
		[Token(Token = "0x6000D03")]
		[Address(RVA = "0x540A90", Offset = "0x53F490", VA = "0x180540A90", Slot = "36")]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000D04")]
		[Address(RVA = "0x540B00", Offset = "0x53F500", VA = "0x180540B00", Slot = "37")]
		set
		{
		}
	}

	[Token(Token = "0x170001D3")]
	public virtual RenderMode RenderMode
	{
		[Token(Token = "0x6000D05")]
		[Address(RVA = "0x540B70", Offset = "0x53F570", VA = "0x180540B70", Slot = "38")]
		get
		{
			return default(RenderMode);
		}
		[Token(Token = "0x6000D06")]
		[Address(RVA = "0x540BE0", Offset = "0x53F5E0", VA = "0x180540BE0", Slot = "39")]
		set
		{
		}
	}

	[Token(Token = "0x170001D4")]
	public virtual Camera RenderCamera
	{
		[Token(Token = "0x6000D07")]
		[Address(RVA = "0x540CC0", Offset = "0x53F6C0", VA = "0x180540CC0", Slot = "40")]
		get
		{
			return null;
		}
		[Token(Token = "0x6000D08")]
		[Address(RVA = "0x540D30", Offset = "0x53F730", VA = "0x180540D30", Slot = "41")]
		set
		{
		}
	}

	[Token(Token = "0x170001D5")]
	protected static GameObject FocusOnNavigation
	{
		[Token(Token = "0x6000D09")]
		[Address(RVA = "0x540DA0", Offset = "0x53F7A0", VA = "0x180540DA0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000D0A")]
		[Address(RVA = "0x540DE0", Offset = "0x53F7E0", VA = "0x180540DE0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001D6")]
	protected virtual CanvasGroup CanvasGroup
	{
		[Token(Token = "0x6000D0B")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "42")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000D0C")]
		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001D7")]
	protected virtual bool ControlOpacity
	{
		[Token(Token = "0x6000D0D")]
		[Address(RVA = "0x540E80", Offset = "0x53F880", VA = "0x180540E80", Slot = "43")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x14000046")]
	public event Action<bool> OnVisibilityChanged
	{
		[Token(Token = "0x6000CF0")]
		[Address(RVA = "0x540390", Offset = "0x53ED90", VA = "0x180540390", Slot = "17")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000CF1")]
		[Address(RVA = "0x5404A0", Offset = "0x53EEA0", VA = "0x1805404A0", Slot = "18")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000D0E")]
	[Address(RVA = "0x540E90", Offset = "0x53F890", VA = "0x180540E90", Slot = "44")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__64))]
	public virtual UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000D0F")]
	[Address(RVA = "0x541040", Offset = "0x53FA40", VA = "0x180541040", Slot = "45")]
	public virtual void SetVisibility(bool visible)
	{
	}

	[Token(Token = "0x6000D10")]
	[Address(RVA = "0x5412A0", Offset = "0x53FCA0", VA = "0x1805412A0", Slot = "46")]
	public virtual void ToggleVisibility()
	{
	}

	[Token(Token = "0x6000D11")]
	[Address(RVA = "0x541310", Offset = "0x53FD10", VA = "0x180541310", Slot = "47")]
	[ContextMenu("Show")]
	public virtual void Show()
	{
	}

	[Token(Token = "0x6000D12")]
	[Address(RVA = "0x541350", Offset = "0x53FD50", VA = "0x180541350", Slot = "48")]
	[ContextMenu("Hide")]
	public virtual void Hide()
	{
	}

	[Token(Token = "0x6000D13")]
	[Address(RVA = "0x541390", Offset = "0x53FD90", VA = "0x180541390", Slot = "49")]
	public virtual void SetOpacity(float opacity)
	{
	}

	[Token(Token = "0x6000D14")]
	[Address(RVA = "0x5414B0", Offset = "0x53FEB0", VA = "0x1805414B0", Slot = "50")]
	public virtual void SetInteractable(bool interactable)
	{
	}

	[Token(Token = "0x6000D15")]
	[Address(RVA = "0x5415D0", Offset = "0x53FFD0", VA = "0x1805415D0", Slot = "51")]
	public virtual void ClearFocus()
	{
	}

	[Token(Token = "0x6000D16")]
	[Address(RVA = "0x541740", Offset = "0x540140", VA = "0x180541740", Slot = "52")]
	public virtual void SetFocus()
	{
	}

	[Token(Token = "0x6000D17")]
	[Address(RVA = "0x5417A0", Offset = "0x5401A0", VA = "0x1805417A0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000D18")]
	[Address(RVA = "0x541980", Offset = "0x540380", VA = "0x180541980", Slot = "53")]
	protected virtual void Update()
	{
	}

	[Token(Token = "0x6000D19")]
	[Address(RVA = "0x5419A0", Offset = "0x5403A0", VA = "0x1805419A0", Slot = "54")]
	protected virtual void SetVisibilityOnAwake()
	{
	}

	[Token(Token = "0x6000D1A")]
	[Address(RVA = "0x5419E0", Offset = "0x5403E0", VA = "0x1805419E0", Slot = "55")]
	protected virtual void HandleVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x6000D1B")]
	[Address(RVA = "0x541BA0", Offset = "0x5405A0", VA = "0x180541BA0", Slot = "56")]
	protected virtual void HandleNavigationFocus()
	{
	}

	[Token(Token = "0x6000D1C")]
	[Address(RVA = "0x541D50", Offset = "0x540750", VA = "0x180541D50", Slot = "57")]
	protected virtual bool SampleInputSystemNavigation()
	{
		return default(bool);
	}

	[Token(Token = "0x6000D1D")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "58")]
	protected virtual bool SampleLegacyInputNavigation()
	{
		return default(bool);
	}

	[Token(Token = "0x6000D1E")]
	[Address(RVA = "0x5408A0", Offset = "0x53F2A0", VA = "0x1805408A0")]
	private RectTransform GetRectTransform()
	{
		return null;
	}

	[Token(Token = "0x6000D1F")]
	[Address(RVA = "0x540B00", Offset = "0x53F500", VA = "0x180540B00")]
	private void SetSortingOrder(int value)
	{
	}

	[Token(Token = "0x6000D20")]
	[Address(RVA = "0x540BE0", Offset = "0x53F5E0", VA = "0x180540BE0")]
	private void SetRenderMode(RenderMode renderMode)
	{
	}

	[Token(Token = "0x6000D21")]
	[Address(RVA = "0x540D30", Offset = "0x53F730", VA = "0x180540D30")]
	private void SetRenderCamera(Camera renderCamera)
	{
	}

	[Token(Token = "0x6000D22")]
	[Address(RVA = "0x5420E0", Offset = "0x540AE0", VA = "0x1805420E0")]
	public ScriptableUIBehaviour()
	{
	}
}
