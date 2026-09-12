using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Cameras;

[Token(Token = "0x2000006")]
[UsedImplicitly]
[InitializeAtRuntime(0, typeof(CameraManager))]
public class CameraManagerExtended : CameraManager, ICameraManagerExtended, IEngineService<CameraConfigurationExtended>, IEngineService
{
	[Serializable]
	[Token(Token = "0x2000007")]
	public class SettingsExtended
	{
		[Token(Token = "0x4000014")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public bool FullScreen;

		[Token(Token = "0x4000015")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		public int ResolutionLevel;

		[Token(Token = "0x4000016")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int TargetFpsLevel;

		[Token(Token = "0x4000017")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public bool PostProcessingActivity;

		[Token(Token = "0x6000023")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public SettingsExtended()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000008")]
	[CompilerGenerated]
	private struct _003CChangeOffset_003Ed__36
{
		[Token(Token = "0x4000018")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000019")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400001A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraManagerExtended _003C_003E4__this;

		[Token(Token = "0x400001B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Vector3 offset;

		[Token(Token = "0x400001C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public Tween tween;

		[Token(Token = "0x400001D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken asyncToken;

		[Token(Token = "0x400001E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000024")]
		[Address(RVA = "0xCF6910", Offset = "0xCF5310", VA = "0x180CF6910", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000025")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000009")]
	[CompilerGenerated]
	private struct _003CInitializeMainCamera_003Ed__38
{
		[Token(Token = "0x400001F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000020")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<Camera> _003C_003Et__builder;

		[Token(Token = "0x4000021")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CameraManagerExtended _003C_003E4__this;

		[Token(Token = "0x4000022")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CameraConfiguration config;

		[Token(Token = "0x4000023")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public Transform parent;

		[Token(Token = "0x4000024")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public int uiLayer;

		[Token(Token = "0x4000025")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<Camera>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000026")]
		[Address(RVA = "0xCF6DE0", Offset = "0xCF57E0", VA = "0x180CF6DE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000027")]
		[Address(RVA = "0xCF7320", Offset = "0xCF5D20", VA = "0x180CF7320", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000A")]
	[CompilerGenerated]
	private struct _003CInitializeService_003Ed__32
{
		[Token(Token = "0x4000026")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000027")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000028")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraManagerExtended _003C_003E4__this;

		[Token(Token = "0x4000029")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000028")]
		[Address(RVA = "0xCF7350", Offset = "0xCF5D50", VA = "0x180CF7350", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000029")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400000A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private readonly Tweener<VectorTween> _offsetTweener;

	[Token(Token = "0x400000B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private Vector3 _offset;

	[Token(Token = "0x400000C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC4")]
	private bool _isFullScreen;

	[Token(Token = "0x400000D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private int _resolutionLevel;

	[Token(Token = "0x400000E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xCC")]
	private int _targetFpsLevel;

	[Token(Token = "0x400000F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private bool _isPostProcessingEnabled;

	[Token(Token = "0x4000010")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD4")]
	private Resolution _nativeResolution;

	[Token(Token = "0x4000011")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private Resolution[] _screenResolutions;

	[Token(Token = "0x4000012")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private Vector2Int[] _availableResolutionsForPc;

	[Token(Token = "0x17000001")]
	public new CameraConfigurationExtended Configuration
	{
		[Token(Token = "0x6000005")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "88")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000002")]
	public bool FullScreen
	{
		[Token(Token = "0x6000006")]
		[Address(RVA = "0xCF52A0", Offset = "0xCF3CA0", VA = "0x180CF52A0", Slot = "79")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000007")]
		[Address(RVA = "0xCF52B0", Offset = "0xCF3CB0", VA = "0x180CF52B0", Slot = "80")]
		set
		{
		}
	}

	[Token(Token = "0x17000003")]
	public int ResolutionLevel
	{
		[Token(Token = "0x6000008")]
		[Address(RVA = "0xCF52D0", Offset = "0xCF3CD0", VA = "0x180CF52D0", Slot = "81")]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000009")]
		[Address(RVA = "0xCF52E0", Offset = "0xCF3CE0", VA = "0x180CF52E0", Slot = "82")]
		set
		{
		}
	}

	[Token(Token = "0x17000004")]
	public int TargetFpsLevel
	{
		[Token(Token = "0x600000A")]
		[Address(RVA = "0xCF52F0", Offset = "0xCF3CF0", VA = "0x180CF52F0", Slot = "83")]
		get
		{
			return default(int);
		}
		[Token(Token = "0x600000B")]
		[Address(RVA = "0xCF5300", Offset = "0xCF3D00", VA = "0x180CF5300", Slot = "84")]
		set
		{
		}
	}

	[Token(Token = "0x17000005")]
	public bool IsPostProcessingEnabled
	{
		[Token(Token = "0x600000C")]
		[Address(RVA = "0xCF53C0", Offset = "0xCF3DC0", VA = "0x180CF53C0", Slot = "85")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600000D")]
		[Address(RVA = "0xCF53D0", Offset = "0xCF3DD0", VA = "0x180CF53D0", Slot = "86")]
		set
		{
		}
	}

	[Token(Token = "0x17000006")]
	public Vector2Int[] AvailableResolutionsForPc
	{
		[Token(Token = "0x600000E")]
		[Address(RVA = "0xCF53E0", Offset = "0xCF3DE0", VA = "0x180CF53E0", Slot = "87")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000007")]
	private Resolution[] ScreenResolutions
	{
		[Token(Token = "0x600000F")]
		[Address(RVA = "0xCF5680", Offset = "0xCF4080", VA = "0x180CF5680")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000008")]
	public override Vector3 Offset
	{
		[Token(Token = "0x6000010")]
		[Address(RVA = "0xCF5700", Offset = "0xCF4100", VA = "0x180CF5700", Slot = "38")]
		get
		{
			return default(Vector3);
		}
		[Token(Token = "0x6000011")]
		[Address(RVA = "0x486B20", Offset = "0x485520", VA = "0x180486B20", Slot = "39")]
		set
		{
		}
	}

	[Token(Token = "0x6000012")]
	[Address(RVA = "0xCF5720", Offset = "0xCF4120", VA = "0x180CF5720")]
	public CameraManagerExtended(CameraConfigurationExtended configExtended, CameraConfiguration config, IInputManager inputManager, IEngineBehaviour engineBehaviour)
	{
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0xCF58C0", Offset = "0xCF42C0", VA = "0x180CF58C0", Slot = "49")]
	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__32))]
	public override UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000014")]
	[Address(RVA = "0xCF59E0", Offset = "0xCF43E0", VA = "0x180CF59E0", Slot = "52")]
	public override void SaveServiceState(SettingsStateMap stateMap)
	{
	}

	[Token(Token = "0x6000015")]
	[Address(RVA = "0xCF5BD0", Offset = "0xCF45D0", VA = "0x180CF5BD0", Slot = "53")]
	public override UniTask LoadServiceState(SettingsStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0xCF5E80", Offset = "0xCF4880", VA = "0x180CF5E80", Slot = "57")]
	public override Texture2D CaptureThumbnail()
	{
		return null;
	}

	[Token(Token = "0x6000017")]
	[Address(RVA = "0xCF5FB0", Offset = "0xCF49B0", VA = "0x180CF5FB0", Slot = "58")]
	[AsyncStateMachine(typeof(_003CChangeOffset_003Ed__36))]
	public override UniTask ChangeOffset(Vector3 offset, Tween tween, [Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000018")]
	[Address(RVA = "0xCF6180", Offset = "0xCF4B80", VA = "0x180CF6180")]
	private Vector3 ModifyOffset(Vector3 offset)
	{
		return default(Vector3);
	}

	[Token(Token = "0x6000019")]
	[Address(RVA = "0xCF63A0", Offset = "0xCF4DA0", VA = "0x180CF63A0", Slot = "61")]
	[AsyncStateMachine(typeof(_003CInitializeMainCamera_003Ed__38))]
	protected override UniTask<Camera> InitializeMainCamera(CameraConfiguration config, Transform parent, int uiLayer)
	{
		return default(UniTask<Camera>);
	}

	[Token(Token = "0x600001A")]
	[Address(RVA = "0xCF6600", Offset = "0xCF5000", VA = "0x180CF6600", Slot = "68")]
	protected override void SetOffset(Vector3 value)
	{
	}

	[Token(Token = "0x600001B")]
	[Address(RVA = "0xCF6660", Offset = "0xCF5060", VA = "0x180CF6660")]
	private void CompleteOffsetTween()
	{
	}

	[Token(Token = "0x600001C")]
	[Address(RVA = "0xCF52B0", Offset = "0xCF3CB0", VA = "0x180CF52B0")]
	private void SetFullScreenEnabled(bool enable)
	{
	}

	[Token(Token = "0x600001D")]
	[Address(RVA = "0xCF6710", Offset = "0xCF5110", VA = "0x180CF6710")]
	private void SetResolutionLevel(int level)
	{
	}

	[Token(Token = "0x600001E")]
	[Address(RVA = "0xCF5300", Offset = "0xCF3D00", VA = "0x180CF5300")]
	private void SetTargetFpsLevel(int level)
	{
	}

	[Token(Token = "0x600001F")]
	[Address(RVA = "0xCF53D0", Offset = "0xCF3DD0", VA = "0x180CF53D0")]
	private void SetPostProcessingEnabled(bool enable)
	{
	}

	[Token(Token = "0x6000020")]
	[Address(RVA = "0xCF6880", Offset = "0xCF5280", VA = "0x180CF6880")]
	private bool IsWithinResolutionRange(Vector2Int size)
	{
		return default(bool);
	}
}
