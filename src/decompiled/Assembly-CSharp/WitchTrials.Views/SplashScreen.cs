using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Overlays.Models;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000146")]
public class SplashScreen : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x2000147")]
	[CompilerGenerated]
	private struct _003CPlaySequenceAsync_003Ed__12
{
		[Token(Token = "0x400064E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400064F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000650")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SplashScreen _003C_003E4__this;

		[Token(Token = "0x4000651")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000652")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000653")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000654")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<int>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000576")]
		[Address(RVA = "0x38C070", Offset = "0x38AA70", VA = "0x18038C070", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000577")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000648")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x4000649")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Button _wholeScreenButton;

	[Token(Token = "0x400064A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Header("Parameters")]
	[SerializeField]
	private float _fadeDuration;

	[Token(Token = "0x400064B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private float _displayDuration;

	[Token(Token = "0x400064C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private IInputManager _inputManager;

	[Token(Token = "0x400064D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IOverlayFader _overlayFader;

	[Token(Token = "0x170000B3")]
	private IInputManager InputManager
	{
		[Token(Token = "0x6000570")]
		[Address(RVA = "0x38BC10", Offset = "0x38A610", VA = "0x18038BC10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B4")]
	private IOverlayFader OverlayFader
	{
		[Token(Token = "0x6000571")]
		[Address(RVA = "0x38BCD0", Offset = "0x38A6D0", VA = "0x18038BCD0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000572")]
	[Address(RVA = "0x36E750", Offset = "0x36D150", VA = "0x18036E750")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000573")]
	[Address(RVA = "0x38BD90", Offset = "0x38A790", VA = "0x18038BD90")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000574")]
	[Address(RVA = "0x38BEC0", Offset = "0x38A8C0", VA = "0x18038BEC0")]
	[AsyncStateMachine(typeof(_003CPlaySequenceAsync_003Ed__12))]
	public UniTask PlaySequenceAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000575")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SplashScreen()
	{
	}
}
