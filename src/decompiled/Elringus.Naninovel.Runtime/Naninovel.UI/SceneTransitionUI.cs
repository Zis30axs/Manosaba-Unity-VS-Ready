using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x200057F")]
public class SceneTransitionUI : CustomUI, ISceneTransitionUI, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x2000580")]
	[CompilerGenerated]
	private struct _003CCaptureScene_003Ed__7
{
		[Token(Token = "0x400150C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400150D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400150E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SceneTransitionUI _003C_003E4__this;

		[Token(Token = "0x400150F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private RenderTexture _003CinitialRenderTexture_003E5__2;

		[Token(Token = "0x4001510")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B3F")]
		[Address(RVA = "0x6387D0", Offset = "0x6371D0", VA = "0x1806387D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B40")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000581")]
	[CompilerGenerated]
	private struct _003CTransition_003Ed__8
{
		[Token(Token = "0x4001511")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001512")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001513")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SceneTransitionUI _003C_003E4__this;

		[Token(Token = "0x4001514")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Transition transition;

		[Token(Token = "0x4001515")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public Tween tween;

		[Token(Token = "0x4001516")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		public AsyncToken token;

		[Token(Token = "0x4001517")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private RenderTexture _003CtransitionTexture_003E5__2;

		[Token(Token = "0x4001518")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private RenderTexture _003CinitialRenderTexture_003E5__3;

		[Token(Token = "0x4001519")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B41")]
		[Address(RVA = "0x638E80", Offset = "0x637880", VA = "0x180638E80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B42")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001507")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[SerializeField]
	private RawImage image;

	[Token(Token = "0x4001508")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private readonly Tweener<FloatTween> transitionTweener;

	[Token(Token = "0x4001509")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private ICameraManager cameraManager;

	[Token(Token = "0x400150A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	private TransitionalMaterial material;

	[Token(Token = "0x400150B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	private RenderTexture sceneTexture;

	[Token(Token = "0x170003DC")]
	protected RawImage Image
	{
		[Token(Token = "0x6001B38")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001B39")]
	[Address(RVA = "0x638000", Offset = "0x636A00", VA = "0x180638000", Slot = "97")]
	[AsyncStateMachine(typeof(_003CCaptureScene_003Ed__7))]
	public virtual UniTask CaptureScene()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001B3A")]
	[Address(RVA = "0x638120", Offset = "0x636B20", VA = "0x180638120", Slot = "98")]
	[AsyncStateMachine(typeof(_003CTransition_003Ed__8))]
	public virtual UniTask Transition(Transition transition, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001B3B")]
	[Address(RVA = "0x638340", Offset = "0x636D40", VA = "0x180638340", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001B3C")]
	[Address(RVA = "0x638560", Offset = "0x636F60", VA = "0x180638560", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x6001B3D")]
	[Address(RVA = "0x6386F0", Offset = "0x6370F0", VA = "0x1806386F0")]
	public SceneTransitionUI()
	{
	}
}
