using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace WitchTrials.Models
{
    // ============================================================================
    // RECONSTRUCTED FROM NATIVE CODE — GameAssembly.dll (Steam CHS, PE timestamp
    // 0x68B45C56, imagebase 0x180000000). Managed skeleton + field offsets from
    // Il2CppDumper; method bodies recovered by disassembling the RVAs below.
    //
    // This is an equivalent behavioural model rebuilt from the compiled binary,
    // NOT the original author's source text. Identifier names come from IL2CPP
    // metadata (which preserves them); statement structure is inferred.
    //
    // Per-member confidence and evidence are noted at each method.
    // ============================================================================
    [Token(Token = "0x2000182")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, null)]
    public class TrialStateManager : ITrialStateManager, IEngineService, IStatefulService<GameStateMap>
    {
    	[Serializable]
    	[Token(Token = "0x2000183")]
    	public class GameState
    	{
    		[Token(Token = "0x4000734")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PerspectiveStageKind CurrentStageKind;

    		[Token(Token = "0x4000735")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
    		public bool IsLoaded;

    		[Token(Token = "0x4000736")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CameraState[] CameraStates;

    		[Token(Token = "0x4000737")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ScreenState[] ScreenStates;

    		// RECONSTRUCTED | Confidence: A | RVA: 0x30C000
    		// 0x30C000 is the shared empty-body thunk (`ret 0`, verified by
    		// disassembly). The constructor genuinely does nothing beyond the
    		// implicit zero-init, i.e. there are no field initialisers.
    		[Token(Token = "0x60006AC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000184")]
    	public class CameraState
    	{
    		[Token(Token = "0x4000738")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public float Height;

    		[Token(Token = "0x4000739")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
    		public Vector3 Rotation;

    		[Token(Token = "0x400073A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float Distance;

    		// RECONSTRUCTED | Confidence: A | RVA: 0x30C000
    		// Shared empty-body thunk — no field initialisers.
    		[Token(Token = "0x60006AD")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public CameraState()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000185")]
    	public class ScreenState
    	{
    		[Token(Token = "0x400073B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public bool Visible = true;

    		[Token(Token = "0x400073C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
    		public float NormalizedPosX;

    		[Token(Token = "0x400073D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public float Roll;

    		[Token(Token = "0x400073E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
    		public ScreenSplitDirection Direction = ScreenSplitDirection.Right;

    		// RECONSTRUCTED | Confidence: A | RVA: 0x39E320
    		// Native body is exactly three instructions:
    		//     mov byte  ptr [rcx+0x10], 1   ; Visible   (+0x10, bool)  = true
    		//     mov dword ptr [rcx+0x1C], 1   ; Direction (+0x1C, enum)  = 1
    		//     ret
    		// ScreenSplitDirection: Left = 0, Right = 1 -> Direction = Right.
    		// Expressed above as C# field initialisers, which is how the compiler
    		// produces this shape; the constructor body itself is empty.
    		[Token(Token = "0x60006AE")]
    		[Address(RVA = "0x39E320", Offset = "0x39CD20", VA = "0x18039E320")]
    		public ScreenState()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000187")]
    	[CompilerGenerated]
    	private struct _003CLoadServiceState_003Ed__9
    {
    		[Token(Token = "0x4000742")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000743")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000744")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x4000745")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public TrialStateManager _003C_003E4__this;

    		[Token(Token = "0x4000746")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		// NATIVE BODY NOT RECONSTRUCTED | RVA: 0x39E510 (0xD5C = 3420 bytes)
    		//
    		// Async state machine for LoadServiceState. Control flow has not been
    		// decoded yet, so no body is asserted here. What IS established from a
    		// static call/metadata inventory of the native function:
    		//
    		//   reads   Naninovel.StateMap.GetState<GameState>()
    		//   builds  List<CameraState> / List<ScreenState> via Enumerable.ToList
    		//   builds  List<UniTask>, awaits Cysharp UniTask.WhenAll(...)
    		//   touches ModifyCameraHeightTween, ModifyCameraRotationTween,
    		//           ModifyCameraDistanceTween
    		//   through IPerspectiveStageCameraModifier,
    		//           IPerspectiveStageScreenModifier, IPerspectiveStageLoader
    		//   uses    Vector3.get_Item, Nullable<float>..ctor
    		//
    		// i.e. it restores the saved GameState and replays it onto the stage by
    		// awaiting a batch of per-camera / per-screen tweens. The exact ordering,
    		// null handling and loop structure are UNVERIFIED — do not rely on the
    		// summary above as if it were the implementation.
    		[Token(Token = "0x60006B1")]
    		[Address(RVA = "0x39E510", Offset = "0x39CF10", VA = "0x18039E510", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		// RECONSTRUCTED | Confidence: A | RVA: 0x30C000
    		// Shared empty-body thunk — the usual no-op SetStateMachine.
    		[Token(Token = "0x60006B2")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400072F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private IPerspectiveStageManager _stageManager;

    	[Token(Token = "0x4000730")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private PerspectiveStageKind _currentStageKind;

    	[Token(Token = "0x4000731")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
    	private bool _isLoaded;

    	[Token(Token = "0x4000732")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private IList<CameraState> _cameraStates;

    	[Token(Token = "0x4000733")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private IList<ScreenState> _screenStates;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x39D3A0 (0x220 bytes)
    	//
    	// Native evidence:
    	//   0x39D439  ServiceLocator_TypeInfo cctor guard
    	//   0x39D44E  mov cl, 1                     ; includeDerivedType: true
    	//             call ServiceLocator$$Get<object>
    	//             MethodInfo = ServiceLocator.Get<IPerspectiveStageManager>()
    	//   0x39D46A  mov [rdi+0x10], rax           ; _stageManager = <result>
    	//   0x39D4B9  object_new List<CameraState> + ..ctor()
    	//   0x39D4DE  mov [rdi+0x20], rbx           ; _cameraStates = <new list>
    	//   0x39D529  object_new List<ScreenState> + ..ctor()
    	//   0x39D54E  mov [rdi+0x28], rbx           ; _screenStates = <new list>
    	//   0x39D5B3  xor eax, eax / ret            ; returns a zeroed UniTask
    	// (The interleaved `bts`/`lock cmpxchg` blocks are GC write barriers.)
    	//
    	// `default` vs `UniTask.CompletedTask`: observationally identical (both are
    	// an all-zero struct), so this is a spelling choice, not a behaviour claim.
    	// Mild evidence favours `UniTask.CompletedTask`: there is a UniTask_TypeInfo
    	// class-init guard at 0x39D599, and reading a static property is what would
    	// require the cctor to have run. Not enough to assert, so `default` stays.
    	[Token(Token = "0x60006A1")]
    	[Address(RVA = "0x39D3A0", Offset = "0x39BDA0", VA = "0x18039D3A0", Slot = "9")]
    	public Naninovel.UniTask InitializeService()
    	{
    		_stageManager = ServiceLocator.Get<IPerspectiveStageManager>(includeDerivedType: true);
    		_cameraStates = new List<CameraState>();
    		_screenStates = new List<ScreenState>();
    		return default(Naninovel.UniTask);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C000
    	// Bound to the shared empty-body thunk (`ret 0`) — genuinely a no-op.
    	[Token(Token = "0x60006A2")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "10")]
    	public void ResetService()
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C000
    	// Bound to the shared empty-body thunk (`ret 0`) — genuinely a no-op.
    	[Token(Token = "0x60006A3")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "11")]
    	public void DestroyService()
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x39D5C0 (0x2EA bytes)
    	//
    	// Native evidence:
    	//   0x39D63B  object_new GameState
    	//   0x39D64C  [rsi+0x18] -> [rbx+0x10]  ; gs.CurrentStageKind = _currentStageKind
    	//   0x39D652  [rsi+0x1C] -> [rbx+0x14]  ; gs.IsLoaded         = _isLoaded
    	//   0x39D673  Enumerable.ToArray<CameraState>(_cameraStates) -> [rbx+0x18]
    	//   0x39D719  Enumerable.ToArray<ScreenState>(_screenStates) -> [rbx+0x20]
    	//   0x39D804  StateMap.SetState<GameState>, inlined down to
    	//             Dictionary<string,object>.set_Item on the map's backing dict
    	//
    	// Note: ToArray throws ArgumentNullException("source") when a list is null
    	// (landing pads at 0x39D867 / 0x39D889) — that is inlined LINQ behaviour,
    	// not an explicit check written here.
    	[Token(Token = "0x60006A4")]
    	[Address(RVA = "0x39D5C0", Offset = "0x39BFC0", VA = "0x18039D5C0", Slot = "12")]
    	public void SaveServiceState(GameStateMap stateMap)
    	{
    		GameState gameState = new GameState
    		{
    			CurrentStageKind = _currentStageKind,
    			IsLoaded = _isLoaded,
    			CameraStates = _cameraStates.ToArray(),
    			ScreenStates = _screenStates.ToArray()
    		};
    		stateMap.SetState(gameState);
    	}

    	// PARTIALLY RECONSTRUCTED | Confidence: A for the shell, D for the body
    	// RVA: 0x39D8B0 (0x17E bytes)
    	//
    	// The native function here is only the async trampoline: it initialises
    	// <LoadServiceState>d__9 (stateMap, <>4__this, state = -1), calls
    	// AsyncUniTaskMethodBuilder.Start<d__9>(ref sm) and returns
    	// <>t__builder.Task. That much is Confidence A.
    	//
    	// The actual restore logic lives in d__9.MoveNext (RVA 0x39E510), which is
    	// NOT yet reconstructed — see the note on MoveNext above. The body is left
    	// as the original stub deliberately: writing an `async` body here would
    	// fabricate control flow that has not been verified.
    	[Token(Token = "0x60006A5")]
    	[Address(RVA = "0x39D8B0", Offset = "0x39C2B0", VA = "0x18039D8B0", Slot = "13")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__9))]
    	public Naninovel.UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(Naninovel.UniTask);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x39DA30 (0x10 bytes)
    	// Native body is exactly:
    	//     mov dword ptr [rcx+0x18], edx   ; _currentStageKind = kind
    	//     mov byte  ptr [rcx+0x1C], 1     ; _isLoaded = true
    	//     ret
    	[Token(Token = "0x60006A6")]
    	[Address(RVA = "0x39DA30", Offset = "0x39C430", VA = "0x18039DA30", Slot = "4")]
    	public void RegisterCurrentStage(PerspectiveStageKind kind)
    	{
    		_currentStageKind = kind;
    		_isLoaded = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x39DA40 (0x10 bytes)
    	// Native body is exactly:
    	//     mov dword ptr [rcx+0x18], 0     ; _currentStageKind = 0
    	//     mov byte  ptr [rcx+0x1C], 0     ; _isLoaded = false
    	//     ret
    	// PerspectiveStageKind's 0 value is written back as `default` because the
    	// native store is an immediate 0 and the enum's zero member name carries no
    	// extra evidence about intent.
    	[Token(Token = "0x60006A7")]
    	[Address(RVA = "0x39DA40", Offset = "0x39C440", VA = "0x18039DA40", Slot = "5")]
    	public void UnregisterCurrentStage()
    	{
    		_currentStageKind = default(PerspectiveStageKind);
    		_isLoaded = false;
    	}

    	// RECONSTRUCTED | Confidence: A (structure), B (the `??` spelling)
    	// RVA: 0x39DA50 (0x52B bytes) + closure lambda at RVA 0x39E330
    	//
    	// Native evidence:
    	//   0x39DB23  object_new <>c__DisplayClass12_0; captures
    	//             +0x10 = rotation, +0x18 = this, +0x20 = index
    	//   0x39DC04  edi = ICollection<CameraState>.get_Count(_cameraStates)
    	//   0x39DC10  loop, re-reading the captured index each pass:
    	//               for (int i = _cameraStates.Count; i < index + 1; i++)
    	//                   _cameraStates.Add(new CameraState());
    	//             get_Count is called ONCE, before the loop (0x39DC04), and the
    	//             counter is a local incremented at 0x39DC95 -- Roslyn cannot
    	//             hoist an interface property across Add(), so the hoist is the
    	//             source's own `for` initialiser, not an optimisation. A
    	//             `while (Count < index + 1)` would re-read Count each pass.
    	//   0x39DCBB  Nullable<float>.get_HasValue(height)
    	//             -> _cameraStates[index].Height = height.Value   ([rbx+0x10])
    	//   0x39DD59  if (rotation != null):
    	//     0x39DD84   new Enumerable.RangeIterator{ start = 0, count = 3 }
    	//     0x39DDCE   new Func<int,float>(closure.<RegisterCameraState>b__0)
    	//     0x39DDE0   Enumerable.Select<int,float>
    	//     0x39DE3B   Enumerable.ToArray<float>
    	//     0x39DE62   three bounds checks (len > 0, > 1, > 2), then
    	//     0x39DE9B   movsd [rax+0x14] / movss [rax+0x1C]
    	//                -> CameraState.Rotation = new Vector3(r[0], r[1], r[2])
    	//   0x39DEA5  Nullable<float>.get_HasValue(distance)
    	//             -> _cameraStates[index].Distance = distance.Value ([rbx+0x20])
    	//
    	// The lambda (RVA 0x39E330) is:
    	//     Enumerable.ElementAtOrDefault<float?>(rotation, i)  -> float?
    	//     if HasValue  -> GetValueOrDefault()
    	//     else         -> <>4__this._cameraStates[index].Rotation[i]
    	//                     (Vector3.get_Item on CameraState+0x14)
    	// which is written below as `??`. Confidence B only on that spelling: the
    	// native shape is HasValue/GetValueOrDefault, which `??` compiles to, but an
    	// explicit if/else in the original would produce the same code.
    	[Token(Token = "0x60006A8")]
    	[Address(RVA = "0x39DA50", Offset = "0x39C450", VA = "0x18039DA50", Slot = "6")]
    	public void RegisterCameraState(int index, float? height, float?[] rotation, float? distance)
    	{
    		for (int i = _cameraStates.Count; i < index + 1; i++)
    		{
    			_cameraStates.Add(new CameraState());
    		}
    		if (height.HasValue)
    		{
    			_cameraStates[index].Height = height.Value;
    		}
    		if (rotation != null)
    		{
    			float[] array = Enumerable.Range(0, 3)
    				.Select((int i) => rotation.ElementAtOrDefault(i) ?? _cameraStates[index].Rotation[i])
    				.ToArray();
    			_cameraStates[index].Rotation = new Vector3(array[0], array[1], array[2]);
    		}
    		if (distance.HasValue)
    		{
    			_cameraStates[index].Distance = distance.Value;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x39DF80 (0x191 bytes)
    	//
    	// Native evidence:
    	//   0x39DFD7  r8 = _screenStates ([rsi/rbp+0x28]); null -> NullReferenceException
    	//   0x39DFFD  esi = ICollection<ScreenState>.get_Count(_screenStates)
    	//             -- called ONCE, before the loop; esi is then a local counter
    	//             incremented at 0x39E0A5. Count is never re-read, so the source
    	//             is a `for` with Count as its initialiser, not a `while` on
    	//             Count (Roslyn cannot hoist that property across Add()).
    	//   0x39E011  cmp esi, index + 1 / jge -> skip loop
    	//   0x39E020  loop body: object_new ScreenState (ctor inlined: Visible = 1,
    	//             Direction = 1) then ICollection<ScreenState>.Add
    	//   0x39E0B0  rax = IList<ScreenState>.get_Item(_screenStates, index)
    	//   0x39E0DF  mov byte ptr [rax+0x10], r12b   ; item.Visible = visible
    	[Token(Token = "0x60006A9")]
    	[Address(RVA = "0x39DF80", Offset = "0x39C980", VA = "0x18039DF80", Slot = "7")]
    	public void RegisterScreenVisibilityState(int index, bool visible)
    	{
    		for (int i = _screenStates.Count; i < index + 1; i++)
    		{
    			_screenStates.Add(new ScreenState());
    		}
    		_screenStates[index].Visible = visible;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x39E120 (0x1F4 bytes)
    	//
    	// Native evidence: identical padding loop to the method above -- get_Count
    	// hoisted once into ebp at 0x39E1B0, incremented at 0x39E259 -- then three
    	// IList<ScreenState>.get_Item(index) calls, each storing one field:
    	//   0x39E286  movss dword ptr [rax+0x14], xmm6  ; NormalizedPosX
    	//   0x39E2A1  movss dword ptr [rax+0x18], xmm7  ; Roll
    	//   0x39E2E2  mov   dword ptr [rax+0x1C], ecx   ; Direction
    	// The element is re-fetched before each store (three separate get_Item
    	// calls at 0x39E274 / 0x39E293 / 0x39E2AE), which is what the repeated
    	// `_screenStates[index]` below reproduces.
    	[Token(Token = "0x60006AA")]
    	[Address(RVA = "0x39E120", Offset = "0x39CB20", VA = "0x18039E120", Slot = "8")]
    	public void RegisterScreenSplitState(int index, float normalizedPosX, float roll, ScreenSplitDirection direction)
    	{
    		for (int i = _screenStates.Count; i < index + 1; i++)
    		{
    			_screenStates.Add(new ScreenState());
    		}
    		_screenStates[index].NormalizedPosX = normalizedPosX;
    		_screenStates[index].Roll = roll;
    		_screenStates[index].Direction = direction;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C000
    	// Bound to the shared empty-body thunk (`ret 0`) — no field initialisers,
    	// which matches _cameraStates / _screenStates being created in
    	// InitializeService rather than in the constructor.
    	[Token(Token = "0x60006AB")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public TrialStateManager()
    	{
    	}
    }
}
