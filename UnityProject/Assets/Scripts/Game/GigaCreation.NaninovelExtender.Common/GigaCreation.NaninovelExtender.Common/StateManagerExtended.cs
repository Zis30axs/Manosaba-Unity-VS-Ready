using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000028")]
    [UsedImplicitly]
    [Goto.DontReset]
    [InitializeAtRuntime(-2147483648, typeof(StateManager))]
    public class StateManagerExtended : StateManager, IStateManagerExtended, IStateManager, IEngineService<StateConfiguration>, IEngineService
    {
    	[Token(Token = "0x200002B")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass9_0
    	{
    		[Token(Token = "0x4000082")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public StateManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000083")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string slotId;

    		[Token(Token = "0x4000084")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public GameStateMap state;

    		[Token(Token = "0x6000076")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass9_0()
    		{
    		}

    		[Token(Token = "0x6000077")]
    		[Address(RVA = "0xD00090", Offset = "0xCFEA90", VA = "0x180D00090")]
    		internal UniTask _003CQuickSaveAsync_003Eb__0()
    		{
    			return default(UniTask);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200002C")]
    	[CompilerGenerated]
    	private struct _003CDoSaveAfterSync_003Ed__11
    {
    		[Token(Token = "0x4000085")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000086")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000087")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public StateManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000088")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public GameStateMap state;

    		[Token(Token = "0x4000089")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string slotId;

    		[Token(Token = "0x400008A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000078")]
    		[Address(RVA = "0xD000C0", Offset = "0xCFEAC0", VA = "0x180D000C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000079")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200002D")]
    	[CompilerGenerated]
    	private struct _003CQuickLoadAsync_003Ed__10
    {
    		[Token(Token = "0x400008B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400008C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

    		[Token(Token = "0x400008D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public StateManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400008E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int slotNumber;

    		[Token(Token = "0x400008F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600007A")]
    		[Address(RVA = "0xD00680", Offset = "0xCFF080", VA = "0x180D00680", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600007B")]
    		[Address(RVA = "0xD00930", Offset = "0xCFF330", VA = "0x180D00930", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200002E")]
    	[CompilerGenerated]
    	private struct _003CQuickSaveAsync_003Ed__9
    {
    		[Token(Token = "0x4000090")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000091")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

    		[Token(Token = "0x4000092")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public StateManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000093")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int slotNumber;

    		[Token(Token = "0x4000094")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private _003C_003Ec__DisplayClass9_0 _003C_003E8__1;

    		[Token(Token = "0x4000095")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600007C")]
    		[Address(RVA = "0xD00960", Offset = "0xCFF360", VA = "0x180D00960", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600007D")]
    		[Address(RVA = "0xD00F20", Offset = "0xCFF920", VA = "0x180D00F20", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200002F")]
    	[CompilerGenerated]
    	private struct _003CResetStateAsync_003Ed__7
    {
    		[Token(Token = "0x4000096")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000097")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000098")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IReadOnlyCollection<string> exclude;

    		[Token(Token = "0x4000099")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public StateManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400009A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool soft;

    		[Token(Token = "0x400009B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public Func<UniTask>[] tasks;

    		[Token(Token = "0x400009C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600007E")]
    		[Address(RVA = "0xD00F50", Offset = "0xCFF950", VA = "0x180D00F50", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600007F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000030")]
    	[CompilerGenerated]
    	private struct _003CResetStateAsync_003Ed__8
    {
    		[Token(Token = "0x400009D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400009E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400009F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public bool soft;

    		[Token(Token = "0x40000A0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public StateManagerExtended _003C_003E4__this;

    		[Token(Token = "0x40000A1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public IReadOnlyCollection<Type> exclude;

    		[Token(Token = "0x40000A2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public Func<UniTask>[] tasks;

    		[Token(Token = "0x40000A3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40000A4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private NaninovelInteractionBlocker _003C_003E7__wrap1;

    		[Token(Token = "0x40000A5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private IEnumerator<Func<UniTask>> _003C_003E7__wrap2;

    		[Token(Token = "0x6000080")]
    		[Address(RVA = "0xD01550", Offset = "0xCFFF50", VA = "0x180D01550", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000081")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400007B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x1700000D")]
    	public bool IsSoftResetting
    	{
    		// RECONSTRUCTED | Confidence: A | auto-property, backing field +0xC0 (set around a soft reset)
    		[Token(Token = "0x6000065")]
    		[Address(RVA = "0xC54930", Offset = "0xC53330", VA = "0x180C54930", Slot = "89")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x6000066")]
    		[Address(RVA = "0xCC8360", Offset = "0xCC6D60", VA = "0x180CC8360")]
    		[CompilerGenerated]
    		private set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFF280, 8 B
    	// `xor r8d,r8d` (MethodInfo* = null) + `jmp 0x1805DD600` (Naninovel.StateManager$$.ctor,
    	// 2448 B). rcx (__this) and rdx (cfg) are passed through unchanged, so this is a pure
    	// tail call == `: base(cfg)`; no field initializer and no other statement is emitted.
    	[Token(Token = "0x6000067")]
    	[Address(RVA = "0xCFF280", Offset = "0xCFDC80", VA = "0x180CFF280")]
    	public StateManagerExtended(StateConfiguration cfg)
    		: base(cfg)
    	{
    	}

    	[Token(Token = "0x6000068")]
    	[Address(RVA = "0xCFF290", Offset = "0xCFDC90", VA = "0x180CFF290", Slot = "61")]
    	public override UniTask InitializeService()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCFF290, 432 B (0x1B0)
    		//
    		// The retained stub skipped `base.InitializeService()`, which is the only thing that fills
    		// `StateManager.scriptPlayer` (+0xA8) and `StateManager.cameraManager` (+0xB0) -- both `private` in the
    		// package, so the derived class cannot fill them itself.  The measured consequence: the last statement
    		// of `StateManager.PerformPostEngineInitializationTasks` is
    		//     if (Configuration.EnableStateRollback) InitializeRollback();   // StateManager.cs:306-307
    		// and `InitializeRollback` opens with `scriptPlayer.AddPreExecutionTask(...)` -> NullReferenceException
    		// on the null player -> the first post-init task faults -> `Engine.Initialize` faults -> `initializeTCS`
    		// is never signalled, so the engine reports `Initializing = true` for ever while the game plays on
    		// (`RuntimeInitializer` polls that task with `IsCompleted` alone, RuntimeInitializer.cs:93, so the
    		// exception is never observed).  Measured state that pins this exact position:
    		//   engine_settings_state = SettingsStateMap   and   engine_global_state = GlobalStateMap
    		// i.e. BOTH state loads inside the task returned, and no further progress event was ever raised.
    		//
    		// Recovered sequence -- three stores, no other statement, then a CompletedTask return:
    		//   0x180CFF33F  call 0x180682840  METHODINFO Engine.GetServiceOrErr<IScriptPlayer>()
    		//   0x180CFF352  mov [rbx+0xA8],rax (+ GC write barrier)  -> StateManager.scriptPlayer
    		//                (dump.cs: `private IScriptPlayer scriptPlayer; // 0xA8`)
    		//   0x180CFF3B0  call 0x180682840  METHODINFO Engine.GetServiceOrErr<ICameraManager>()
    		//   0x180CFF3BC  mov [rbx+0xB0],rax (+ GC write barrier)  -> StateManager.cameraManager
    		//                (dump.cs: `private ICameraManager cameraManager; // 0xB0`)
    		//   0x180CFF43A  call 0x180682840  METHODINFO Engine.GetServiceOrErr<IScriptPlayer>()
    		//   0x180CFF446  mov [rbx+0xB8],rax (+ GC write barrier)  -> StateManagerExtended._naniScriptPlayer
    		//   0x180CFF49E  xor eax,eax / epilogue / ret              -> an all-zero UniTask = CompletedTask
    		// The first two stores are the whole body of the base implementation
    		// (StateManager.cs:64-66: `scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>(); cameraManager =
    		// Engine.GetServiceOrErr<ICameraManager>(); return UniTask.CompletedTask;`), in that order and with no
    		// call instruction anywhere in these 432 bytes -- so `base.InitializeService()` was inlined here and the
    		// source called it.  The third store is the override's own statement.
    		//
    		// Log: tools/unity-rebuild/out/logs/StateManagerExtended_InitializeService_0xCFF290.txt
    		base.InitializeService();
    		_naniScriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
    		return UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFF4B0 (builder stub); MoveNext 0xD00F50, one await state.
    	// The body lives in ResetStateAsyncCore (XxxCore convention; <ResetStateAsync>d__7 metadata stays), the same
    	// shape as Naninovel's StateManager.ResetState(IReadOnlyCollection<string>): Engine.Services.Select(<>c.b__7_0
    	// GetType).Where(<>c__DisplayClass7_0.b__1: name or any interface name excluded).ToArray(), then the Type overload
    	// (0xCFF6B0) with the same soft flag and tasks.
    	// Annotated disassembly: work/title-path/disas/sme_01_ResetStateAsync_str_MoveNext.txt
    	[Token(Token = "0x6000069")]
    	[Address(RVA = "0xCFF4B0", Offset = "0xCFDEB0", VA = "0x180CFF4B0", Slot = "90")]
    	[AsyncStateMachine(typeof(_003CResetStateAsync_003Ed__7))]
    	public UniTask ResetStateAsync(IReadOnlyCollection<string> exclude, bool soft = false, params Func<UniTask>[] tasks)
    	{
    		return ResetStateAsyncCore(exclude, soft, tasks);
    	}

    	private async UniTask ResetStateAsyncCore(IReadOnlyCollection<string> exclude, bool soft, Func<UniTask>[] tasks)
    	{
    		IEnumerable<Type> serviceTypes = Engine.Services.Select((IEngineService service) => service.GetType());
    		Type[] excludeTypes = serviceTypes.Where((Type t) => exclude.Contains(t.Name) || t.GetInterfaces().Any((Type i) => exclude.Contains(i.Name))).ToArray();
    		await ResetStateAsync(excludeTypes, soft, tasks);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFF6B0 (builder stub); MoveNext 0xD01550, two await states.
    	// The body lives in ResetStateAsyncCore (XxxCore convention; <ResetStateAsync>d__8 metadata stays):
    	// - Not soft: Naninovel.StateManager.ResetState(exclude, tasks) (0x1805DF140) awaited, nothing else.
    	// - Soft: IsSoftResetting (+0xC0) = true; a NaninovelInteractionBlocker(Array.Empty, ["Skip"]) held (<>7__wrap1);
    	//   _naniScriptPlayer (+0xB8)?.Playlist?.ReleaseResources(); Engine.Reset(exclude); each non-null task (<>c.b__8_0)
    	//   awaited in turn; IsSoftResetting = false.  Unlike the base there is no OnResetStarted/Finished and no
    	//   PerformOnGameDeserializeTasks.
    	// Annotated disassembly: work/title-path/disas/sme_02_ResetStateAsync_type_MoveNext.txt
    	[Token(Token = "0x600006A")]
    	[Address(RVA = "0xCFF6B0", Offset = "0xCFE0B0", VA = "0x180CFF6B0", Slot = "91")]
    	[AsyncStateMachine(typeof(_003CResetStateAsync_003Ed__8))]
    	public UniTask ResetStateAsync(IReadOnlyCollection<Type> exclude, bool soft = false, params Func<UniTask>[] tasks)
    	{
    		return ResetStateAsyncCore(exclude, soft, tasks);
    	}

    	private async UniTask ResetStateAsyncCore(IReadOnlyCollection<Type> exclude, bool soft, Func<UniTask>[] tasks)
    	{
    		if (!soft)
    		{
    			await base.ResetState(exclude, tasks);
    			return;
    		}
    		IsSoftResetting = true;
    		using (new NaninovelInteractionBlocker(Array.Empty<ICancelEventBlockable>(), "Skip"))
    		{
    			// Shipped null tests: 0x180D017EF test r8 (player) / 0x180D017F9 test rax (Playlist), each skipping the release.
    			if (_naniScriptPlayer != null)
    			{
    				ScriptPlaylist playlist = _naniScriptPlayer.Playlist;
    				if (playlist != null)
    				{
    					playlist.ReleaseResources();
    				}
    			}
    			Engine.Reset(exclude);
    			if (tasks != null)
    			{
    				foreach (Func<UniTask> task in tasks.Where((Func<UniTask> t) => t != null))
    				{
    					await task();
    				}
    			}
    		}
    		IsSoftResetting = false;
    	}

    	[Token(Token = "0x600006B")]
    	[Address(RVA = "0xCFF8B0", Offset = "0xCFE2B0", VA = "0x180CFF8B0", Slot = "92")]
    	[AsyncStateMachine(typeof(_003CQuickSaveAsync_003Ed__9))]
    	public UniTask<GameStateMap> QuickSaveAsync(int slotNumber)
    	{
    		return default(UniTask<GameStateMap>);
    	}

    	[Token(Token = "0x600006C")]
    	[Address(RVA = "0xCFFA60", Offset = "0xCFE460", VA = "0x180CFFA60", Slot = "93")]
    	[AsyncStateMachine(typeof(_003CQuickLoadAsync_003Ed__10))]
    	public UniTask<GameStateMap> QuickLoadAsync(int slotNumber)
    	{
    		return default(UniTask<GameStateMap>);
    	}

    	[Token(Token = "0x600006D")]
    	[Address(RVA = "0xCFFC10", Offset = "0xCFE610", VA = "0x180CFFC10")]
    	[AsyncStateMachine(typeof(_003CDoSaveAfterSync_003Ed__11))]
    	private UniTask DoSaveAfterSync(string slotId, GameStateMap state)
    	{
    		return default(UniTask);
    	}
    }
}
