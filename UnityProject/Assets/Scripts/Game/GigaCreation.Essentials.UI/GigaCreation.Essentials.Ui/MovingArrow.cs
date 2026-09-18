using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000008")]
    [RequireComponent(typeof(CanvasGroup))]
    public class MovingArrow : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CMoveAsync_003Ed__13
    {
    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public MovingArrow _003C_003E4__this;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600001C")]
    		[Address(RVA = "0xCD72A0", Offset = "0xCD5CA0", VA = "0x180CD72A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400000F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000010")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _button;

    	[Token(Token = "0x4000011")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private RectTransform _imageRectTransform;

    	[Token(Token = "0x4000012")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[Header("Parameters")]
    	[SerializeField]
    	private float _move;

    	[Token(Token = "0x4000013")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    	[SerializeField]
    	private float _duration;

    	[Token(Token = "0x4000014")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private float _interval;

    	[Token(Token = "0x4000015")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	private Vector2 _imageInitialPosition;

    	[Token(Token = "0x4000016")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private CancellationTokenSource _cts;

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xCD6BE0", Offset = "0xCD55E0", VA = "0x180CD6BE0")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCD6C20", Offset = "0xCD5620", VA = "0x180CD6C20")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0x36B6C0", Offset = "0x36A0C0", VA = "0x18036B6C0")]
    	public void Present()
    	{
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x36B710", Offset = "0x36A110", VA = "0x18036B710")]
    	public void Dismiss()
    	{
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCD6D50", Offset = "0xCD5750", VA = "0x180CD6D50")]
    	public void Activate()
    	{
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCD6F30", Offset = "0xCD5930", VA = "0x180CD6F30")]
    	[AsyncStateMachine(typeof(_003CMoveAsync_003Ed__13))]
    	private UniTask MoveAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xCD70E0", Offset = "0xCD5AE0", VA = "0x180CD70E0")]
    	public void Deactivate()
    	{
    	}

    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xCD7210", Offset = "0xCD5C10", VA = "0x180CD7210")]
    	public IObservable<Unit> OnClickAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xCD7240", Offset = "0xCD5C40", VA = "0x180CD7240")]
    	public MovingArrow()
    	{
    	}
    }
}
