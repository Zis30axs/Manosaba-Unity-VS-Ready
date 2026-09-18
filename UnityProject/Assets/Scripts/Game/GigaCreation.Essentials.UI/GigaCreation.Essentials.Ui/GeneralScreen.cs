using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000038")]
    [RequireComponent(typeof(Canvas))]
    public class GeneralScreen : MonoBehaviour, ICancelEventBlockable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000039")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__31
    {
    		[Token(Token = "0x40000E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool instantly;

    		[Token(Token = "0x40000E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40000E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000116")]
    		[Address(RVA = "0xCE8800", Offset = "0xCE7200", VA = "0x180CE8800", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000117")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003A")]
    	[CompilerGenerated]
    	private struct _003CFadeAsync_003Ed__43
    {
    		[Token(Token = "0x40000E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float duration;

    		[Token(Token = "0x40000EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float to;

    		[Token(Token = "0x40000ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool updateMotionManually;

    		[Token(Token = "0x40000EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000118")]
    		[Address(RVA = "0xCE8EC0", Offset = "0xCE78C0", VA = "0x180CE8EC0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000119")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003B")]
    	[CompilerGenerated]
    	private struct _003CFadeInAsync_003Ed__41
    {
    		[Token(Token = "0x40000F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool instantly;

    		[Token(Token = "0x40000F4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x21")]
    		public bool updateMotionManually;

    		[Token(Token = "0x40000F5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000F6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600011A")]
    		[Address(RVA = "0xCE93F0", Offset = "0xCE7DF0", VA = "0x180CE93F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003C")]
    	[CompilerGenerated]
    	private struct _003CFadeOutAsync_003Ed__42
    {
    		[Token(Token = "0x40000F7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000F8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000F9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000FA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool instantly;

    		[Token(Token = "0x40000FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x21")]
    		public bool updateMotionManually;

    		[Token(Token = "0x40000FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600011C")]
    		[Address(RVA = "0xCE9700", Offset = "0xCE8100", VA = "0x180CE9700", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003D")]
    	[CompilerGenerated]
    	private struct _003CPresentAsync_003Ed__28
    {
    		[Token(Token = "0x40000FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000FF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000100")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x4000101")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000102")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool instantly;

    		[Token(Token = "0x4000103")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000104")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600011E")]
    		[Address(RVA = "0xCE9A10", Offset = "0xCE8410", VA = "0x180CE9A10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000D0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x40000D1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x40000D2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Underlay _underlay;

    	[Token(Token = "0x40000D3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private Button _closeButton;

    	[Token(Token = "0x40000D4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _visibleOnAwake;

    	[Token(Token = "0x40000D5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	[SerializeField]
    	private float _contentPresentDuration;

    	[Token(Token = "0x40000D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private float _contentDismissDuration;

    	[Token(Token = "0x40000D7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x4C")]
    	[Space]
    	[SerializeField]
    	private bool _enableCancelAction;

    	[Token(Token = "0x40000D8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private UnityEvent _onBeginToPresent;

    	[Token(Token = "0x40000D9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private UnityEvent _onEndToPresent;

    	[Token(Token = "0x40000DA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private UnityEvent _onBeginToDismiss;

    	[Token(Token = "0x40000DB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[SerializeField]
    	private UnityEvent _onEndToDismiss;

    	[Token(Token = "0x40000DC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x40000DD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private IDisposable _closeButtonDisposable;

    	[Token(Token = "0x40000DE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private CancellationTokenSource _presentCts;

    	[Token(Token = "0x40000DF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	private CancellationTokenSource _dismissCts;

    	[Token(Token = "0x17000031")]
    	public bool IsCancelEventBlocked
    	{
    		[Token(Token = "0x60000FC")]
    		[Address(RVA = "0x619430", Offset = "0x617E30", VA = "0x180619430")]
    		[CompilerGenerated]
    		private get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60000FD")]
    		[Address(RVA = "0x87B020", Offset = "0x879A20", VA = "0x18087B020", Slot = "5")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000032")]
    	private IInputManager InputManager
    	{
    		[Token(Token = "0x60000FE")]
    		[Address(RVA = "0xCE6C00", Offset = "0xCE5600", VA = "0x180CE6C00")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000033")]
    	public int SortingOrder
    	{
    		[Token(Token = "0x60000FF")]
    		[Address(RVA = "0xCE6CC0", Offset = "0xCE56C0", VA = "0x180CE6CC0")]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x6000100")]
    		[Address(RVA = "0xCE6D60", Offset = "0xCE5760", VA = "0x180CE6D60")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000101")]
    	[Address(RVA = "0xCE6D80", Offset = "0xCE5780", VA = "0x180CE6D80")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000102")]
    	[Address(RVA = "0xCE6EA0", Offset = "0xCE58A0", VA = "0x180CE6EA0")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000103")]
    	[Address(RVA = "0xCE7040", Offset = "0xCE5A40", VA = "0x180CE7040")]
    	private void Dismiss()
    	{
    	}

    	[Token(Token = "0x6000104")]
    	[Address(RVA = "0xCE7090", Offset = "0xCE5A90", VA = "0x180CE7090")]
    	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__28))]
    	public UniTask PresentAsync(bool instantly = false, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000105")]
    	[Address(RVA = "0xCE7260", Offset = "0xCE5C60", VA = "0x180CE7260")]
    	public void BeginToPresent()
    	{
    	}

    	[Token(Token = "0x6000106")]
    	[Address(RVA = "0xCE7400", Offset = "0xCE5E00", VA = "0x180CE7400")]
    	public void EndToPresent()
    	{
    	}

    	[Token(Token = "0x6000107")]
    	[Address(RVA = "0xCE7510", Offset = "0xCE5F10", VA = "0x180CE7510")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__31))]
    	public UniTask DismissAsync(bool instantly = false, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000108")]
    	[Address(RVA = "0xCE76E0", Offset = "0xCE60E0", VA = "0x180CE76E0")]
    	public void BeginToDismiss()
    	{
    	}

    	[Token(Token = "0x6000109")]
    	[Address(RVA = "0xCE7950", Offset = "0xCE6350", VA = "0x180CE7950")]
    	public void EndToDismiss()
    	{
    	}

    	[Token(Token = "0x600010A")]
    	[Address(RVA = "0xCE7A60", Offset = "0xCE6460", VA = "0x180CE7A60")]
    	private void SetCanvasesEnabled(bool enable, bool keepHidden = false)
    	{
    	}

    	[Token(Token = "0x600010B")]
    	[Address(RVA = "0xCE7C60", Offset = "0xCE6660", VA = "0x180CE7C60")]
    	private void SetGraphicRaycastersEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x600010C")]
    	[Address(RVA = "0xCE7D90", Offset = "0xCE6790", VA = "0x180CE7D90")]
    	private void EnableCancelEvent()
    	{
    	}

    	[Token(Token = "0x600010D")]
    	[Address(RVA = "0xCE8050", Offset = "0xCE6A50", VA = "0x180CE8050")]
    	private void DisableCancelEvent()
    	{
    	}

    	[Token(Token = "0x600010E")]
    	[Address(RVA = "0xCE81D0", Offset = "0xCE6BD0", VA = "0x180CE81D0")]
    	public bool TryGetClickUnderlayTask(out UniTask task, CancellationToken ct = default)
    	{
    		task = default;
    		task = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600010F")]
    	[Address(RVA = "0xCE81F0", Offset = "0xCE6BF0", VA = "0x180CE81F0")]
    	private void OnCancelActionPerformed(InputAction.CallbackContext _)
    	{
    	}

    	[Token(Token = "0x6000110")]
    	[Address(RVA = "0xCE8200", Offset = "0xCE6C00", VA = "0x180CE8200")]
    	private void Cancel()
    	{
    	}

    	[Token(Token = "0x6000111")]
    	[Address(RVA = "0xCE82A0", Offset = "0xCE6CA0", VA = "0x180CE82A0")]
    	[AsyncStateMachine(typeof(_003CFadeInAsync_003Ed__41))]
    	public UniTask FadeInAsync(bool instantly = false, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000112")]
    	[Address(RVA = "0xCE8460", Offset = "0xCE6E60", VA = "0x180CE8460")]
    	[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__42))]
    	public UniTask FadeOutAsync(bool instantly = false, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000113")]
    	[Address(RVA = "0xCE8620", Offset = "0xCE7020", VA = "0x180CE8620")]
    	[AsyncStateMachine(typeof(_003CFadeAsync_003Ed__43))]
    	private UniTask FadeAsync(float to, float duration, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000114")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public GeneralScreen()
    	{
    	}
    }
}
