using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000115")]
[InitializeAtRuntime(-1, null)]
public class CameraManager : ICameraManager, IEngineService<CameraConfiguration>, IEngineService, IStatefulService<GameStateMap>, IStatefulService<SettingsStateMap>
{
	[Serializable]
	[Token(Token = "0x2000116")]
	public class Settings
	{
		[Token(Token = "0x4000457")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public int QualityLevel;

		[Token(Token = "0x600053D")]
		[Address(RVA = "0x48AB40", Offset = "0x489540", VA = "0x18048AB40")]
		public Settings()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x2000117")]
	public class GameState
	{
		[Token(Token = "0x4000458")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public Vector3 Offset;

		[Token(Token = "0x4000459")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public Quaternion Rotation;

		[Token(Token = "0x400045A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public float Zoom;

		[Token(Token = "0x400045B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public bool Orthographic;

		[Token(Token = "0x400045C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public CameraLookState LookMode;

		[Token(Token = "0x400045D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public CameraComponentState[] CameraComponents;

		[Token(Token = "0x400045E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		public bool RenderUI;

		[Token(Token = "0x600053E")]
		[Address(RVA = "0x48AB50", Offset = "0x489550", VA = "0x18048AB50")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200011A")]
	[CompilerGenerated]
	private struct _003CChangeOffset_003Ed__61
{
		[Token(Token = "0x4000463")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000464")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000465")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraManager _003C_003E4__this;

		[Token(Token = "0x4000466")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Tween tween;

		[Token(Token = "0x4000467")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public Vector3 offset;

		[Token(Token = "0x4000468")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x4000469")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000542")]
		[Address(RVA = "0x48ACB0", Offset = "0x4896B0", VA = "0x18048ACB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000543")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200011B")]
	[CompilerGenerated]
	private struct _003CChangeRotation_003Ed__62
{
		[Token(Token = "0x400046A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400046B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400046C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraManager _003C_003E4__this;

		[Token(Token = "0x400046D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Tween tween;

		[Token(Token = "0x400046E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public Quaternion rotation;

		[Token(Token = "0x400046F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public AsyncToken token;

		[Token(Token = "0x4000470")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000544")]
		[Address(RVA = "0x48B150", Offset = "0x489B50", VA = "0x18048B150", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000545")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200011C")]
	[CompilerGenerated]
	private struct _003CChangeZoom_003Ed__63
{
		[Token(Token = "0x4000471")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000472")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000473")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraManager _003C_003E4__this;

		[Token(Token = "0x4000474")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Tween tween;

		[Token(Token = "0x4000475")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public float zoom;

		[Token(Token = "0x4000476")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x4000477")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000546")]
		[Address(RVA = "0x48B7A0", Offset = "0x48A1A0", VA = "0x18048B7A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000547")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200011D")]
	[CompilerGenerated]
	private struct _003CInitializeMainCamera_003Ed__64
{
		[Token(Token = "0x4000478")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000479")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<Camera> _003C_003Et__builder;

		[Token(Token = "0x400047A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CameraConfiguration config;

		[Token(Token = "0x400047B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Transform parent;

		[Token(Token = "0x400047C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public int uiLayer;

		[Token(Token = "0x400047D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Camera>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000548")]
		[Address(RVA = "0x48BBD0", Offset = "0x48A5D0", VA = "0x18048BBD0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000549")]
		[Address(RVA = "0x48C400", Offset = "0x48AE00", VA = "0x18048C400", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200011E")]
	[CompilerGenerated]
	private struct _003CInitializeService_003Ed__52
{
		[Token(Token = "0x400047E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400047F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000480")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraManager _003C_003E4__this;

		[Token(Token = "0x4000481")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Camera>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600054A")]
		[Address(RVA = "0x48C430", Offset = "0x48AE30", VA = "0x18048C430", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600054B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200011F")]
	[CompilerGenerated]
	private struct _003CInitializeUICamera_003Ed__65
{
		[Token(Token = "0x4000482")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000483")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<Camera> _003C_003Et__builder;

		[Token(Token = "0x4000484")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CameraConfiguration config;

		[Token(Token = "0x4000485")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Transform parent;

		[Token(Token = "0x4000486")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public CameraManager _003C_003E4__this;

		[Token(Token = "0x4000487")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public int uiLayer;

		[Token(Token = "0x4000488")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<Camera>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600054C")]
		[Address(RVA = "0x48D0A0", Offset = "0x48BAA0", VA = "0x18048D0A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600054D")]
		[Address(RVA = "0x48D6B0", Offset = "0x48C0B0", VA = "0x18048D6B0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000447")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly IInputManager inputManager;

	[Token(Token = "0x4000448")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly IEngineBehaviour engineBehaviour;

	[Token(Token = "0x4000449")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly RenderTexture thumbnailRenderTexture;

	[Token(Token = "0x400044A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private readonly List<MonoBehaviour> cameraComponentsCache;

	[Token(Token = "0x400044B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private readonly Tweener<VectorTween> offsetTweener;

	[Token(Token = "0x400044C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private readonly Tweener<VectorTween> rotationTweener;

	[Token(Token = "0x400044D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly Tweener<FloatTween> zoomTweener;

	[Token(Token = "0x400044E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private IReadOnlyCollection<CameraComponentState> initialComponentState;

	[Token(Token = "0x400044F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private GameObject serviceObject;

	[Token(Token = "0x4000450")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private Transform lookContainer;

	[Token(Token = "0x4000451")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private Vector3 offset;

	[Token(Token = "0x4000452")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8C")]
	private Quaternion rotation;

	[Token(Token = "0x4000453")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x9C")]
	private float zoom;

	[Token(Token = "0x4000454")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private float initialOrthoSize;

	[Token(Token = "0x4000455")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA4")]
	private float initialFOV;

	[Token(Token = "0x4000456")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private int uiLayer;

	[Token(Token = "0x170000D4")]
	public virtual CameraConfiguration Configuration
	{
		[Token(Token = "0x6000505")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "31")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000D5")]
	public virtual Camera Camera
	{
		[Token(Token = "0x6000506")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "32")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000507")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200", Slot = "33")]
		[CompilerGenerated]
		protected set
		{
		}
	}

	[Token(Token = "0x170000D6")]
	public virtual Camera UICamera
	{
		[Token(Token = "0x6000508")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "34")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000509")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0", Slot = "35")]
		[CompilerGenerated]
		protected set
		{
		}
	}

	[Token(Token = "0x170000D7")]
	public virtual bool RenderUI
	{
		[Token(Token = "0x600050A")]
		[Address(RVA = "0x486AC0", Offset = "0x4854C0", VA = "0x180486AC0", Slot = "36")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600050B")]
		[Address(RVA = "0x486AE0", Offset = "0x4854E0", VA = "0x180486AE0", Slot = "37")]
		set
		{
		}
	}

	[Token(Token = "0x170000D8")]
	public virtual Vector3 Offset
	{
		[Token(Token = "0x600050C")]
		[Address(RVA = "0x486B00", Offset = "0x485500", VA = "0x180486B00", Slot = "38")]
		get
		{
			return default(Vector3);
		}
		[Token(Token = "0x600050D")]
		[Address(RVA = "0x486B20", Offset = "0x485520", VA = "0x180486B20", Slot = "39")]
		set
		{
		}
	}

	[Token(Token = "0x170000D9")]
	public virtual Quaternion Rotation
	{
		[Token(Token = "0x600050E")]
		[Address(RVA = "0x486B60", Offset = "0x485560", VA = "0x180486B60", Slot = "40")]
		get
		{
			return default(Quaternion);
		}
		[Token(Token = "0x600050F")]
		[Address(RVA = "0x486B70", Offset = "0x485570", VA = "0x180486B70", Slot = "41")]
		set
		{
		}
	}

	[Token(Token = "0x170000DA")]
	public virtual float Zoom
	{
		[Token(Token = "0x6000510")]
		[Address(RVA = "0x486BA0", Offset = "0x4855A0", VA = "0x180486BA0", Slot = "42")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000511")]
		[Address(RVA = "0x486BB0", Offset = "0x4855B0", VA = "0x180486BB0", Slot = "43")]
		set
		{
		}
	}

	[Token(Token = "0x170000DB")]
	public virtual bool Orthographic
	{
		[Token(Token = "0x6000512")]
		[Address(RVA = "0x486BD0", Offset = "0x4855D0", VA = "0x180486BD0", Slot = "44")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000513")]
		[Address(RVA = "0x486C80", Offset = "0x485680", VA = "0x180486C80", Slot = "45")]
		set
		{
		}
	}

	[Token(Token = "0x170000DC")]
	public virtual int QualityLevel
	{
		[Token(Token = "0x6000514")]
		[Address(RVA = "0x486CA0", Offset = "0x4856A0", VA = "0x180486CA0", Slot = "46")]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000515")]
		[Address(RVA = "0x486CF0", Offset = "0x4856F0", VA = "0x180486CF0", Slot = "47")]
		set
		{
		}
	}

	[Token(Token = "0x170000DD")]
	protected virtual CameraLookController LookController
	{
		[Token(Token = "0x6000516")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "48")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000517")]
		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6000518")]
	[Address(RVA = "0x486DA0", Offset = "0x4857A0", VA = "0x180486DA0")]
	public CameraManager(CameraConfiguration config = default, IInputManager inputManager = default, IEngineBehaviour engineBehaviour = default)
	{
	}

	[Token(Token = "0x6000519")]
	[Address(RVA = "0x487200", Offset = "0x485C00", VA = "0x180487200", Slot = "49")]
	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__52))]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600051A")]
	[Address(RVA = "0x487320", Offset = "0x485D20", VA = "0x180487320", Slot = "50")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x600051B")]
	[Address(RVA = "0x487580", Offset = "0x485F80", VA = "0x180487580", Slot = "51")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x600051C")]
	[Address(RVA = "0x487670", Offset = "0x486070", VA = "0x180487670", Slot = "52")]
	public virtual void SaveServiceState(SettingsStateMap stateMap)
	{
	}

	[Token(Token = "0x600051D")]
	[Address(RVA = "0x487790", Offset = "0x486190", VA = "0x180487790", Slot = "53")]
	public virtual UniTask LoadServiceState(SettingsStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600051E")]
	[Address(RVA = "0x487890", Offset = "0x486290", VA = "0x180487890", Slot = "54")]
	public virtual void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x600051F")]
	[Address(RVA = "0x487B90", Offset = "0x486590", VA = "0x180487B90", Slot = "55")]
	public virtual UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000520")]
	[Address(RVA = "0x487D60", Offset = "0x486760", VA = "0x180487D60", Slot = "56")]
	public virtual void SetLookMode(bool enabled, Vector2 lookZone, Vector2 lookSpeed, bool gravity)
	{
	}

	[Token(Token = "0x6000521")]
	[Address(RVA = "0x487E40", Offset = "0x486840", VA = "0x180487E40", Slot = "57")]
	public virtual Texture2D CaptureThumbnail()
	{
		return null;
	}

	[Token(Token = "0x6000522")]
	[Address(RVA = "0x488920", Offset = "0x487320", VA = "0x180488920", Slot = "58")]
	[AsyncStateMachine(typeof(_003CChangeOffset_003Ed__61))]
	public virtual UniTask ChangeOffset(Vector3 offset, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000523")]
	[Address(RVA = "0x488AF0", Offset = "0x4874F0", VA = "0x180488AF0", Slot = "59")]
	[AsyncStateMachine(typeof(_003CChangeRotation_003Ed__62))]
	public virtual UniTask ChangeRotation(Quaternion rotation, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000524")]
	[Address(RVA = "0x488CC0", Offset = "0x4876C0", VA = "0x180488CC0", Slot = "60")]
	[AsyncStateMachine(typeof(_003CChangeZoom_003Ed__63))]
	public virtual UniTask ChangeZoom(float zoom, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000525")]
	[Address(RVA = "0x488E80", Offset = "0x487880", VA = "0x180488E80", Slot = "61")]
	[AsyncStateMachine(typeof(_003CInitializeMainCamera_003Ed__64))]
	protected virtual UniTask<Camera> InitializeMainCamera(CameraConfiguration config, Transform parent, int uiLayer)
	{
		return default(UniTask<Camera>);
	}

	[Token(Token = "0x6000526")]
	[Address(RVA = "0x489080", Offset = "0x487A80", VA = "0x180489080", Slot = "62")]
	[AsyncStateMachine(typeof(_003CInitializeUICamera_003Ed__65))]
	protected virtual UniTask<Camera> InitializeUICamera(CameraConfiguration config, Transform parent, int uiLayer)
	{
		return default(UniTask<Camera>);
	}

	[Token(Token = "0x6000527")]
	[Address(RVA = "0x4892E0", Offset = "0x487CE0", VA = "0x1804892E0", Slot = "63")]
	protected virtual void ConfigureUICameraForURP(Camera camera)
	{
	}

	[Token(Token = "0x6000528")]
	[Address(RVA = "0x489570", Offset = "0x487F70", VA = "0x180489570", Slot = "64")]
	protected virtual CameraComponentState[] GetComponentState(Camera camera)
	{
		return null;
	}

	[Token(Token = "0x6000529")]
	[Address(RVA = "0x4897F0", Offset = "0x4881F0", VA = "0x1804897F0", Slot = "65")]
	protected virtual void ApplyComponentState(Camera camera, IReadOnlyCollection<CameraComponentState> state)
	{
	}

	[Token(Token = "0x600052A")]
	[Address(RVA = "0x489A60", Offset = "0x488460", VA = "0x180489A60", Slot = "66")]
	protected virtual bool GetRenderUI()
	{
		return default(bool);
	}

	[Token(Token = "0x600052B")]
	[Address(RVA = "0x489AF0", Offset = "0x4884F0", VA = "0x180489AF0", Slot = "67")]
	protected virtual void SetRenderUI(bool value)
	{
	}

	[Token(Token = "0x600052C")]
	[Address(RVA = "0x489BE0", Offset = "0x4885E0", VA = "0x180489BE0", Slot = "68")]
	protected virtual void SetOffset(Vector3 value)
	{
	}

	[Token(Token = "0x600052D")]
	[Address(RVA = "0x489C40", Offset = "0x488640", VA = "0x180489C40", Slot = "69")]
	protected virtual void SetRotation(Quaternion value)
	{
	}

	[Token(Token = "0x600052E")]
	[Address(RVA = "0x489C90", Offset = "0x488690", VA = "0x180489C90", Slot = "70")]
	protected virtual void SetZoom(float value)
	{
	}

	[Token(Token = "0x600052F")]
	[Address(RVA = "0x489CE0", Offset = "0x4886E0", VA = "0x180489CE0", Slot = "71")]
	protected virtual void SetOrthographic(bool value)
	{
	}

	[Token(Token = "0x6000530")]
	[Address(RVA = "0x489D60", Offset = "0x488760", VA = "0x180489D60", Slot = "72")]
	protected virtual void SetCameraOffset(Vector3 offset)
	{
	}

	[Token(Token = "0x6000531")]
	[Address(RVA = "0x489E70", Offset = "0x488870", VA = "0x180489E70", Slot = "73")]
	protected virtual Vector3 GetCameraOffset()
	{
		return default(Vector3);
	}

	[Token(Token = "0x6000532")]
	[Address(RVA = "0x489FA0", Offset = "0x4889A0", VA = "0x180489FA0", Slot = "74")]
	protected virtual void SetCameraRotation(Quaternion rotation)
	{
	}

	[Token(Token = "0x6000533")]
	[Address(RVA = "0x48A050", Offset = "0x488A50", VA = "0x18048A050", Slot = "75")]
	protected virtual void SetCameraRotation(Vector3 rotation)
	{
	}

	[Token(Token = "0x6000534")]
	[Address(RVA = "0x48A190", Offset = "0x488B90", VA = "0x18048A190", Slot = "76")]
	protected virtual Quaternion GetCameraRotation()
	{
		return default(Quaternion);
	}

	[Token(Token = "0x6000535")]
	[Address(RVA = "0x48A260", Offset = "0x488C60", VA = "0x18048A260", Slot = "77")]
	protected virtual void SetCameraZoom(float zoom)
	{
	}

	[Token(Token = "0x6000536")]
	[Address(RVA = "0x48A350", Offset = "0x488D50", VA = "0x18048A350", Slot = "78")]
	protected virtual float GetCameraZoom()
	{
		return default(float);
	}

	[Token(Token = "0x6000537")]
	[Address(RVA = "0x48A480", Offset = "0x488E80", VA = "0x18048A480")]
	private void CompleteOffsetTween()
	{
	}

	[Token(Token = "0x6000538")]
	[Address(RVA = "0x48A520", Offset = "0x488F20", VA = "0x18048A520")]
	private void CompleteRotationTween()
	{
	}

	[Token(Token = "0x6000539")]
	[Address(RVA = "0x48A5C0", Offset = "0x488FC0", VA = "0x18048A5C0")]
	private void CompleteZoomTween()
	{
	}
}
