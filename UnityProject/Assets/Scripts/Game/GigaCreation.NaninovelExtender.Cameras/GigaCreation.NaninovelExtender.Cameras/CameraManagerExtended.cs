using System;
using System.Diagnostics;
using GigaCreation.Essentials.Platforms;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Cameras
{
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF5720 (field initializer; 405 B total)
    	// `_offsetTweener` (+0xB0) = new Tweener<VectorTween>(): the constructor emits
    	// `il2cpp_codegen_object_new(Tweener<VectorTween>_TypeInfo)` and stores the result to
    	// +0xB0 BEFORE the base constructor call, i.e. this is a C# field initializer.
    	[Token(Token = "0x400000A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private readonly Tweener<VectorTween> _offsetTweener = new Tweener<VectorTween>();

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF5720 (field initializer; 405 B total)
    	// `_offset` (+0xB8) = Vector3.zero: 12 bytes are copied from [Vector3_TypeInfo+0xB8]
    	// (the type's static_fields block) + 0, i.e. Vector3's first static field
    	// `private static readonly Vector3 zeroVector` (dump.cs), which is what the
    	// `Vector3.zero` property returns; the next field `_isFullScreen` is at +0xC4, which
    	// bounds the copied 12 bytes. Emitted before the base constructor call -> field initializer.
    	[Token(Token = "0x400000B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private Vector3 _offset = Vector3.zero;

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

    	// RECONSTRUCTED | Confidence: A | RVA: 0x49D250, 8 B (folded)
    	// Get-only auto-property: the trivial `[CompilerGenerated]` read of the backing field
    	// `<Configuration>k__BackingField` at +0xF8 (dump.cs); the RVA is shared with dozens of
    	// unrelated trivial getters. `get;` instead of the stub `return null;` is required so the
    	// recovered constructor can assign the backing field.
    	[Token(Token = "0x17000001")]
    	public new CameraConfigurationExtended Configuration
    	{
    		[Token(Token = "0x6000005")]
    		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "88")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x17000002")]
    	public bool FullScreen
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF52A0 -- movzx eax, byte [rcx+0xC4] (_isFullScreen)
    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0xCF52A0", Offset = "0xCF3CA0", VA = "0x180CF52A0", Slot = "79")]
    		get
    		{
    			return _isFullScreen;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF52B0 -- the same code as SetFullScreenEnabled (identical-code-folded)
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xCF52B0", Offset = "0xCF3CB0", VA = "0x180CF52B0", Slot = "80")]
    		set
    		{
    			SetFullScreenEnabled(value);
    		}
    	}

    	[Token(Token = "0x17000003")]
    	public int ResolutionLevel
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF52D0 -- mov eax, [rcx+0xC8] (_resolutionLevel)
    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xCF52D0", Offset = "0xCF3CD0", VA = "0x180CF52D0", Slot = "81")]
    		get
    		{
    			return _resolutionLevel;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF52E0 -- tail jump SetResolutionLevel(value)
    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xCF52E0", Offset = "0xCF3CE0", VA = "0x180CF52E0", Slot = "82")]
    		set
    		{
    			SetResolutionLevel(value);
    		}
    	}

    	[Token(Token = "0x17000004")]
    	public int TargetFpsLevel
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF52F0 -- mov eax, [rcx+0xCC] (_targetFpsLevel)
    		[Token(Token = "0x600000A")]
    		[Address(RVA = "0xCF52F0", Offset = "0xCF3CF0", VA = "0x180CF52F0", Slot = "83")]
    		get
    		{
    			return _targetFpsLevel;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF5300 -- the same code as SetTargetFpsLevel (identical-code-folded)
    		[Token(Token = "0x600000B")]
    		[Address(RVA = "0xCF5300", Offset = "0xCF3D00", VA = "0x180CF5300", Slot = "84")]
    		set
    		{
    			SetTargetFpsLevel(value);
    		}
    	}

    	[Token(Token = "0x17000005")]
    	public bool IsPostProcessingEnabled
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF53C0 -- movzx eax, byte [rcx+0xD0] (_isPostProcessingEnabled)
    		[Token(Token = "0x600000C")]
    		[Address(RVA = "0xCF53C0", Offset = "0xCF3DC0", VA = "0x180CF53C0", Slot = "85")]
    		get
    		{
    			return _isPostProcessingEnabled;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF53D0 -- the same code as SetPostProcessingEnabled (identical-code-folded)
    		[Token(Token = "0x600000D")]
    		[Address(RVA = "0xCF53D0", Offset = "0xCF3DD0", VA = "0x180CF53D0", Slot = "86")]
    		set
    		{
    			SetPostProcessingEnabled(value);
    		}
    	}

    	[Token(Token = "0x17000006")]
    	public Vector2Int[] AvailableResolutionsForPc
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF53E0, 659 B -- computed once, cached in _availableResolutionsForPc (+0xF0)
    		//   ScreenResolutions non-null with Length > 0 (0x180CF548C):
    		//     Configuration.ResolutionsForPc (+0x18).Where(IsWithinResolutionRange).ToArray(); an empty result becomes
    		//     new[] { ResolutionsForPc[Length - 1] } (0x180CF555C: new Vector2Int[1] holding the last configured size)
    		//   otherwise (0x180CF55C2): Configuration.ResolutionsForPc itself
    		// Annotated disassembly: work/title-path/disas/cme_09_get_AvailableResolutionsForPc.txt
    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0xCF53E0", Offset = "0xCF3DE0", VA = "0x180CF53E0", Slot = "87")]
    		get
    		{
    			if (_availableResolutionsForPc == null)
    			{
    				if (ScreenResolutions != null && ScreenResolutions.Length > 0)
    				{
    					Vector2Int[] candidates = Configuration.ResolutionsForPc.Where(IsWithinResolutionRange).ToArray();
    					if (candidates.Length == 0)
    					{
    						candidates = new Vector2Int[] { Configuration.ResolutionsForPc[Configuration.ResolutionsForPc.Length - 1] };
    					}
    					_availableResolutionsForPc = candidates;
    				}
    				else
    				{
    					_availableResolutionsForPc = Configuration.ResolutionsForPc;
    				}
    			}
    			return _availableResolutionsForPc;
    		}
    	}

    	[Token(Token = "0x17000007")]
    	private Resolution[] ScreenResolutions
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF5680 -- _screenResolutions (+0xE8) ??= Screen.resolutions (0x180CF5697)
    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0xCF5680", Offset = "0xCF4080", VA = "0x180CF5680")]
    		get
    		{
    			if (_screenResolutions == null)
    			{
    				_screenResolutions = Screen.resolutions;
    			}
    			return _screenResolutions;
    		}
    	}

    	[Token(Token = "0x17000008")]
    	public override Vector3 Offset
    	{
    		get => base.Offset;
    		set => base.Offset = value;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF5720, 405 B
    	// ABI: rcx=__this, rdx=configExtended, r8=config, r9=inputManager; the 5th argument
    	// (engineBehaviour) is [rsp+0x60] after `push r14` + `sub rsp,0x30`.
    	// 1. rbx=__this, rbp=configExtended, rsi=config, rdi=inputManager.
    	// 2. field initializers (see `_offsetTweener` / `_offset` above).
    	// 3. r8=rdi=inputManager, r9=[rsp+0x60]=engineBehaviour, rdx=rsi=config, [rsp+0x20]=0
    	//    (MethodInfo*) -> `call 0x180486da0 Naninovel.CameraManager$$.ctor`
    	//    == base(config, inputManager, engineBehaviour).
    	// 4. `mov [rbx+0xF8], rbp` (+ write barrier) -> this.<Configuration>k__BackingField (+0xF8)
    	//    = configExtended.
    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0xCF5720", Offset = "0xCF4120", VA = "0x180CF5720")]
    	public CameraManagerExtended(CameraConfigurationExtended configExtended, CameraConfiguration config, IInputManager inputManager, IEngineBehaviour engineBehaviour)
    		: base(config, inputManager, engineBehaviour)
    	{
    		Configuration = configExtended;
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xCF58C0", Offset = "0xCF42C0", VA = "0x180CF58C0", Slot = "49")]
    	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__32))]
    	public override UniTask InitializeService()
    	{
    		return InitializeServiceCore();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF58C0 (kickoff) / 0xCF7350 (<InitializeService>d__32.MoveNext, 1088 B)
    	//   0x180CF73FE  _nativeResolution (+0xD4) = Screen.currentResolution
    	//                (icall 'UnityEngine.Screen::get_currentResolution_Injected(UnityEngine.Resolution&)')
    	//   0x180CF7512  await base.InitializeService()   (CameraManager.<InitializeService> state machine started inline)
    	// Annotated disassembly: work/title-path/disas/cme_13_InitializeService_MoveNext.txt
    	private async UniTask InitializeServiceCore()
    	{
    		_nativeResolution = Screen.currentResolution;
    		await base.InitializeService();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF59E0, 488 B
    	//   0x180CF5A59..0x180CF5B18  CameraManager.SaveServiceState inlined: new Settings { QualityLevel } -> SetState
    	//   0x180CF5B1F  on PC (CurrentPlatform MacOs/Windows) the saved ResolutionLevel is
    	//                max(_resolutionLevel, ResolutionsForPc.Length - AvailableResolutionsForPc.Length) (cmovle); else _resolutionLevel
    	//   0x180CF5B70  stateMap.SetState(new SettingsExtended { FullScreen, ResolutionLevel, TargetFpsLevel, PostProcessingActivity })
    	// Annotated disassembly: work/title-path/disas/cme_14_SaveServiceState.txt
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCF59E0", Offset = "0xCF43E0", VA = "0x180CF59E0", Slot = "52")]
    	public override void SaveServiceState(SettingsStateMap stateMap)
    	{
    		base.SaveServiceState(stateMap);
    		int resolutionLevel = _resolutionLevel;
    		if (Platform.IsPc)
    		{
    			resolutionLevel = Math.Max(_resolutionLevel, Configuration.ResolutionsForPc.Length - AvailableResolutionsForPc.Length);
    		}
    		stateMap.SetState(new SettingsExtended
    		{
    			FullScreen = _isFullScreen,
    			ResolutionLevel = resolutionLevel,
    			TargetFpsLevel = _targetFpsLevel,
    			PostProcessingActivity = _isPostProcessingEnabled
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF5BD0, 673 B (not async: returns UniTask.CompletedTask)
    	//   0x180CF5C68..0x180CF5CDA  CameraManager.LoadServiceState inlined (QualityLevel restore)
    	//   unavailable = ResolutionsForPc.Length - AvailableResolutionsForPc.Length (0x180CF5D30)
    	//   saved SettingsExtended (0x180CF5D33): _isFullScreen = saved.FullScreen; SetResolutionLevel(PC ? max(saved level,
    	//     unavailable) : _resolutionLevel); TargetFpsLevel = saved; _isPostProcessingEnabled = saved.PostProcessingActivity
    	//   none saved (0x180CF5DBC): the same from Configuration.DefaultGraphicsOptions (+0x30), except the resolution level,
    	//     which is `unavailable` on PC (the largest size that fits the screen) and 0 elsewhere
    	// Annotated disassembly: work/title-path/disas/cme_15_LoadServiceState.txt
    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xCF5BD0", Offset = "0xCF45D0", VA = "0x180CF5BD0", Slot = "53")]
    	public override UniTask LoadServiceState(SettingsStateMap stateMap)
    	{
    		base.LoadServiceState(stateMap);
    		int unavailable = Configuration.ResolutionsForPc.Length - AvailableResolutionsForPc.Length;
    		SettingsExtended settings = stateMap.GetState<SettingsExtended>();
    		if (settings != null)
    		{
    			int resolutionLevel = Platform.IsPc ? Math.Max(settings.ResolutionLevel, unavailable) : _resolutionLevel;
    			_isFullScreen = settings.FullScreen;
    			SetResolutionLevel(resolutionLevel);
    			TargetFpsLevel = settings.TargetFpsLevel;
    			_isPostProcessingEnabled = settings.PostProcessingActivity;
    		}
    		else
    		{
    			_isFullScreen = Configuration.DefaultGraphicsOptions.FullScreen;
    			SetResolutionLevel(Platform.IsPc ? unavailable : 0);
    			TargetFpsLevel = Configuration.DefaultGraphicsOptions.TargetFpsLevel;
    			_isPostProcessingEnabled = Configuration.DefaultGraphicsOptions.PostProcessingActivity;
    		}
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xCF5E80", Offset = "0xCF4880", VA = "0x180CF5E80", Slot = "57")]
    	public override Texture2D CaptureThumbnail()
    	{
    		return base.CaptureThumbnail();
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCF5FB0", Offset = "0xCF49B0", VA = "0x180CF5FB0", Slot = "58")]
    	[AsyncStateMachine(typeof(_003CChangeOffset_003Ed__36))]
    	public override UniTask ChangeOffset(Vector3 offset, Tween tween, [Optional] AsyncToken asyncToken)
    	{
    		return base.ChangeOffset(offset, tween, asyncToken);
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCF6180", Offset = "0xCF4B80", VA = "0x180CF6180")]
    	private Vector3 ModifyOffset(Vector3 offset)
    	{
    		return offset;
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xCF63A0", Offset = "0xCF4DA0", VA = "0x180CF63A0", Slot = "61")]
    	[AsyncStateMachine(typeof(_003CInitializeMainCamera_003Ed__38))]
    	protected override UniTask<Camera> InitializeMainCamera(CameraConfiguration config, Transform parent, int uiLayer)
    	{
    		return InitializeMainCameraCore(config, parent, uiLayer);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF63A0 (kickoff) / 0xCF6DE0 (<InitializeMainCamera>d__38.MoveNext, 1331 B)
    	//   0x180CF6ECA  main = Camera.main; `if (main)` (UnityEngine.Object implicit bool: managed ref and native ptr +0x10)
    	//   0x180CF6F92  main.transform.SetParent(parent, true) and the result is `main` (0x180CF726E SetResult(rdi))
    	//   otherwise 0x180CF6FBB Debug.LogWarning("Main camera not found in the scene; creating a new one.") and
    	//   0x180CF718A await base.InitializeMainCamera(config, parent, uiLayer) (CameraManager's state machine started inline)
    	// Annotated disassembly: work/title-path/disas/cme_19_InitializeMainCamera_MoveNext.txt
    	private async UniTask<Camera> InitializeMainCameraCore(CameraConfiguration config, Transform parent, int uiLayer)
    	{
    		Camera main = Camera.main;
    		if (main)
    		{
    			main.transform.SetParent(parent, true);
    			return main;
    		}
    		UnityEngine.Debug.LogWarning("Main camera not found in the scene; creating a new one.");
    		return await base.InitializeMainCamera(config, parent, uiLayer);
    	}

    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xCF6600", Offset = "0xCF5000", VA = "0x180CF6600", Slot = "68")]
    	protected override void SetOffset(Vector3 value)
    	{
    		base.SetOffset(value);
    	}

    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xCF6660", Offset = "0xCF5060", VA = "0x180CF6660")]
    	private void CompleteOffsetTween()
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF52B0, 20 B -- store +0xC4, tail jump SetResolutionLevel(_resolutionLevel)
    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xCF52B0", Offset = "0xCF3CB0", VA = "0x180CF52B0")]
    	private void SetFullScreenEnabled(bool enable)
    	{
    		_isFullScreen = enable;
    		SetResolutionLevel(_resolutionLevel);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF6710, 352 B
    	//   _resolutionLevel (+0xC8) = level
    	//   Ios/Android (CurrentPlatform 2/3): size = (int)(_nativeResolution.width/height (+0xD4/+0xD8)
    	//     * ResolutionCoefficientsForMobile[level] (+0x20)); elsewhere size = ResolutionsForPc[level] (+0x18)
    	//   full screen is forced on Ios/Android/NintendoSwitch/PlayStation4 (2..5), else _isFullScreen
    	//   0x180CF6850  Screen.SetResolution(size.x, size.y, fullScreen ? FullScreenWindow (1) : Windowed (3),
    	//                new RefreshRate { numerator = 0, denominator = 1 })
    	// Annotated disassembly: work/title-path/disas/cme_12_SetResolutionLevel.txt
    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0xCF6710", Offset = "0xCF5110", VA = "0x180CF6710")]
    	private void SetResolutionLevel(int level)
    	{
    		_resolutionLevel = level;
    		PlatformKind platform = Platform.CurrentPlatform;
    		Vector2Int size;
    		if (platform == PlatformKind.Ios || platform == PlatformKind.Android)
    		{
    			// Android port: the shipped configuration has no mobile coefficients (ResolutionCoefficientsForMobile is empty,
    			// the game only ever ran on PC), so indexing it threw IndexOutOfRangeException, the engine's start-up task
    			// faulted unobserved and the player sat on a black screen.  Without coefficients run at the native resolution.
    			float[] coefficients = Configuration.ResolutionCoefficientsForMobile;
    			float coefficient = coefficients != null && coefficients.Length > 0 ? coefficients[Math.Clamp(level, 0, coefficients.Length - 1)] : 1f;
    			size = new Vector2Int((int)(_nativeResolution.width * coefficient), (int)(_nativeResolution.height * coefficient));
    		}
    		else
    		{
    			size = Configuration.ResolutionsForPc[level];
    		}
    		platform = Platform.CurrentPlatform;
    		bool fullScreen = platform == PlatformKind.Ios || platform == PlatformKind.Android
    		                  || platform == PlatformKind.NintendoSwitch || platform == PlatformKind.PlayStation4
    		                  || _isFullScreen;
    		Screen.SetResolution(size.x, size.y, fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed,
    			new RefreshRate { numerator = 0, denominator = 1 });
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF5300, 190 B -- _targetFpsLevel (+0xCC) = level; tail jump to the icall
    	//   'UnityEngine.Application::set_targetFrameRate(System.Int32)' with Configuration.TargetFpsValues (+0x28)[level]
    	// Annotated disassembly: work/title-path/disas/cme_06_set_TargetFpsLevel.txt
    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xCF5300", Offset = "0xCF3D00", VA = "0x180CF5300")]
    	private void SetTargetFpsLevel(int level)
    	{
    		_targetFpsLevel = level;
    		Application.targetFrameRate = Configuration.TargetFpsValues[level];
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF53D0, 7 B -- mov byte [rcx+0xD0], dl ; ret
    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0xCF53D0", Offset = "0xCF3DD0", VA = "0x180CF53D0")]
    	private void SetPostProcessingEnabled(bool enable)
    	{
    		_isPostProcessingEnabled = enable;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF6880, 133 B -- against the LAST (largest) screen resolution
    	//   (Resolution is 16 B; element width +0x20, height +0x24): size.x > width -> false, else size.y <= height
    	// Annotated disassembly: work/title-path/disas/cme_11_IsWithinResolutionRange.txt
    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xCF6880", Offset = "0xCF5280", VA = "0x180CF6880")]
    	private bool IsWithinResolutionRange(Vector2Int size)
    	{
    		if (size.x > ScreenResolutions[ScreenResolutions.Length - 1].width)
    		{
    			return false;
    		}
    		return size.y <= ScreenResolutions[ScreenResolutions.Length - 1].height;
    	}
    }
}
