using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000022")]
    [RequireComponent(typeof(CustomUI))]
    public class NaninovelScreen : MonoBehaviour, ICancelEventBlockable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000023")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__30
    {
    		[Token(Token = "0x4000085")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000086")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000087")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public NaninovelScreen _003C_003E4__this;

    		[Token(Token = "0x4000088")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000089")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float? duration;

    		[Token(Token = "0x400008A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private NaninovelInteractionBlocker _003C_003E7__wrap1;

    		[Token(Token = "0x400008B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003ClinkedCts_003E5__3;

    		[Token(Token = "0x400008C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400008D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000093")]
    		[Address(RVA = "0xD3DC00", Offset = "0xD3C600", VA = "0x180D3DC00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000094")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000024")]
    	[CompilerGenerated]
    	private struct _003CPresentAsync_003Ed__27
    {
    		[Token(Token = "0x400008E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400008F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000090")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public NaninovelScreen _003C_003E4__this;

    		[Token(Token = "0x4000091")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000092")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float? duration;

    		[Token(Token = "0x4000093")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000094")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000095")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000095")]
    		[Address(RVA = "0xD3E7C0", Offset = "0xD3D1C0", VA = "0x180D3E7C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000096")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000078")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private CustomUI _customUI;

    	[Token(Token = "0x4000079")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _closeButton;

    	[Token(Token = "0x400007A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _enableCancelAction;

    	[Token(Token = "0x400007B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private UnityEvent _onBeginToPresent;

    	[Token(Token = "0x400007C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private UnityEvent _onEndToPresent;

    	[Token(Token = "0x400007D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private UnityEvent _onBeginToDismiss;

    	[Token(Token = "0x400007E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private UnityEvent _onEndToDismiss;

    	[Token(Token = "0x400007F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000080")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private Naninovel.IInputManager _naniInputManager;

    	[Token(Token = "0x4000081")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private IDisposable _closeButtonDisposable;

    	[Token(Token = "0x4000082")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private CancellationTokenSource _presentCts;

    	[Token(Token = "0x4000083")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private CancellationTokenSource _dismissCts;

    	[Token(Token = "0x17000012")]
    	public bool IsCancelEventBlocked
    	{
    		[Token(Token = "0x6000078")]
    		[Address(RVA = "0x492EF0", Offset = "0x4918F0", VA = "0x180492EF0")]
    		[CompilerGenerated]
    		private get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000079")]
    		[Address(RVA = "0x492F00", Offset = "0x491900", VA = "0x180492F00", Slot = "5")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000013")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		[Token(Token = "0x600007A")]
    		[Address(RVA = "0xD3C6E0", Offset = "0xD3B0E0", VA = "0x180D3C6E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000014")]
    	private Naninovel.IInputManager NaniInputManager
    	{
    		[Token(Token = "0x600007B")]
    		[Address(RVA = "0xD3C7A0", Offset = "0xD3B1A0", VA = "0x180D3C7A0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600007C")]
    	[Address(RVA = "0xD3C860", Offset = "0xD3B260", VA = "0x180D3C860", Slot = "6")]
    	protected virtual void Awake()
    	{
    	}

    	[Token(Token = "0x600007D")]
    	[Address(RVA = "0xD3C8D0", Offset = "0xD3B2D0", VA = "0x180D3C8D0")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x600007E")]
    	[Address(RVA = "0xD3CA40", Offset = "0xD3B440", VA = "0x180D3CA40")]
    	private void OnDisable()
    	{
    	}

    	[Token(Token = "0x600007F")]
    	[Address(RVA = "0xD3CBB0", Offset = "0xD3B5B0", VA = "0x180D3CBB0", Slot = "7")]
    	protected virtual void Reset()
    	{
    	}

    	[Token(Token = "0x6000080")]
    	[Address(RVA = "0xD3CC50", Offset = "0xD3B650", VA = "0x180D3CC50")]
    	private void OnVisibilityChangedByCustomUi(bool visible)
    	{
    	}

    	[Token(Token = "0x6000081")]
    	[Address(RVA = "0xD3CCC0", Offset = "0xD3B6C0", VA = "0x180D3CCC0")]
    	private void Present()
    	{
    	}

    	[Token(Token = "0x6000082")]
    	[Address(RVA = "0xD3CD10", Offset = "0xD3B710", VA = "0x180D3CD10")]
    	private void Dismiss()
    	{
    	}

    	[Token(Token = "0x6000083")]
    	[Address(RVA = "0xD3CD60", Offset = "0xD3B760", VA = "0x180D3CD60")]
    	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__27))]
    	public Cysharp.Threading.Tasks.UniTask PresentAsync([Optional] float? duration, [Optional] CancellationToken ct)
    	{
    		return default(Cysharp.Threading.Tasks.UniTask);
    	}

    	[Token(Token = "0x6000084")]
    	[Address(RVA = "0xD3CF30", Offset = "0xD3B930", VA = "0x180D3CF30", Slot = "8")]
    	protected virtual void BeginToPresent()
    	{
    	}

    	[Token(Token = "0x6000085")]
    	[Address(RVA = "0xD3CF70", Offset = "0xD3B970", VA = "0x180D3CF70", Slot = "9")]
    	protected virtual void EndToPresent()
    	{
    	}

    	[Token(Token = "0x6000086")]
    	[Address(RVA = "0xD3CFB0", Offset = "0xD3B9B0", VA = "0x180D3CFB0")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__30))]
    	public Cysharp.Threading.Tasks.UniTask DismissAsync([Optional] float? duration, [Optional] CancellationToken ct)
    	{
    		return default(Cysharp.Threading.Tasks.UniTask);
    	}

    	[Token(Token = "0x6000087")]
    	[Address(RVA = "0xD3D180", Offset = "0xD3BB80", VA = "0x180D3D180", Slot = "10")]
    	protected virtual void BeginToDismiss()
    	{
    	}

    	[Token(Token = "0x6000088")]
    	[Address(RVA = "0xD3D330", Offset = "0xD3BD30", VA = "0x180D3D330", Slot = "11")]
    	protected virtual void EndToDismiss()
    	{
    	}

    	[Token(Token = "0x6000089")]
    	[Address(RVA = "0xD3D360", Offset = "0xD3BD60", VA = "0x180D3D360")]
    	private void SetCanvasesEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x600008A")]
    	[Address(RVA = "0xD3D490", Offset = "0xD3BE90", VA = "0x180D3D490")]
    	private void SetGraphicRaycastersEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x600008B")]
    	[Address(RVA = "0xD3D5C0", Offset = "0xD3BFC0", VA = "0x180D3D5C0")]
    	private void EnableCancelEvent()
    	{
    	}

    	[Token(Token = "0x600008C")]
    	[Address(RVA = "0xD3D880", Offset = "0xD3C280", VA = "0x180D3D880")]
    	private void DisableCancelEvent()
    	{
    	}

    	[Token(Token = "0x600008D")]
    	[Address(RVA = "0xD3DA10", Offset = "0xD3C410", VA = "0x180D3DA10")]
    	private void Cancel()
    	{
    	}

    	[Token(Token = "0x600008E")]
    	[Address(RVA = "0xD3DA80", Offset = "0xD3C480", VA = "0x180D3DA80", Slot = "12")]
    	protected virtual void PlayCancelSfx()
    	{
    	}

    	[Token(Token = "0x600008F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public NaninovelScreen()
    	{
    	}
    }
}
