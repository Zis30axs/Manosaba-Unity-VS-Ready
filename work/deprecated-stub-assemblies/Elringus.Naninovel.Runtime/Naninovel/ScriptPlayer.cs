using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x200043B")]
    [InitializeAtRuntime(0, null)]
    public class ScriptPlayer : IStatefulService<SettingsStateMap>, IEngineService, IStatefulService<GlobalStateMap>, IStatefulService<GameStateMap>, IScriptPlayer, IEngineService<ScriptPlayerConfiguration>
    {
    	[Serializable]
    	[Token(Token = "0x200043C")]
    	public class Settings
    	{
    		[Token(Token = "0x4000FB0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PlayerSkipMode SkipMode;

    		[Token(Token = "0x6001521")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Settings()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200043D")]
    	public class GlobalState
    	{
    		[Token(Token = "0x4000FB1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PlayedScriptRegister PlayedScriptRegister;

    		[Token(Token = "0x6001522")]
    		[Address(RVA = "0x5C82B0", Offset = "0x5C6CB0", VA = "0x1805C82B0")]
    		public GlobalState()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200043E")]
    	public class GameState
    	{
    		[Token(Token = "0x4000FB2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public bool Playing;

    		[Token(Token = "0x4000FB3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x11")]
    		public bool ExecutedPlayedCommand;

    		[Token(Token = "0x4000FB4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x12")]
    		public bool WaitingForInput;

    		[Token(Token = "0x4000FB5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public List<PlaybackSpot> GosubReturnSpots;

    		[Token(Token = "0x6001523")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000442")]
    	[CompilerGenerated]
    	private struct _003CComplete_003Ed__109
    {
    		[Token(Token = "0x4000FBE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FBF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FC0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Func<UniTask> onComplete;

    		[Token(Token = "0x4000FC1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FC2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000FC3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private InteractionBlocker _003C_003E7__wrap1;

    		[Token(Token = "0x600152D")]
    		[Address(RVA = "0x5C90E0", Offset = "0x5C7AE0", VA = "0x1805C90E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600152E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000443")]
    	[CompilerGenerated]
    	private struct _003CExecuteIgnoringCancellation_003Ed__125
    {
    		[Token(Token = "0x4000FC4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FC5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FC6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Command command;

    		[Token(Token = "0x4000FC7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000FC8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FC9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600152F")]
    		[Address(RVA = "0x5C9B80", Offset = "0x5C8580", VA = "0x1805C9B80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001530")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000444")]
    	[CompilerGenerated]
    	private struct _003CExecutePlayedCommand_003Ed__124
    {
    		[Token(Token = "0x4000FCA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FCB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FCC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FCD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000FCE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private int _003Ci_003E5__2;

    		[Token(Token = "0x4000FCF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000FD0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<bool>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001531")]
    		[Address(RVA = "0x5CA020", Offset = "0x5C8A20", VA = "0x1805CA020", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001532")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000445")]
    	[CompilerGenerated]
    	private struct _003CFastForwardRoutine_003Ed__127
    {
    		[Token(Token = "0x4000FD1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FD2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000FD3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FD4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool executePlayedCommand;

    		[Token(Token = "0x4000FD5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4000FD6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public int targetPlaylistIndex;

    		[Token(Token = "0x4000FD7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    		private bool _003CreachedLine_003E5__2;

    		[Token(Token = "0x4000FD8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000FD9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<bool>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001533")]
    		[Address(RVA = "0x5CB080", Offset = "0x5C9A80", VA = "0x1805CB080", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001534")]
    		[Address(RVA = "0x5CB8D0", Offset = "0x5CA2D0", VA = "0x1805CB8D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000446")]
    	[CompilerGenerated]
    	private struct _003CLoadServiceState_003Ed__97
    {
    		[Token(Token = "0x4000FDA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FDB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FDC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FDD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x4000FDE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001535")]
    		[Address(RVA = "0x5CB900", Offset = "0x5CA300", VA = "0x1805CB900", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001536")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000447")]
    	[CompilerGenerated]
    	private struct _003CPlayRoutine_003Ed__126
    {
    		[Token(Token = "0x4000FDF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FE0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FE1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FE2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000FE3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<int>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000FE4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<bool>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x4000FE5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x6001537")]
    		[Address(RVA = "0x5CC1C0", Offset = "0x5CABC0", VA = "0x1805CC1C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001538")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000448")]
    	[CompilerGenerated]
    	private struct _003CRewind_003Ed__105
    {
    		[Token(Token = "0x4000FE6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FE7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000FE8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FE9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int lineIndex;

    		[Token(Token = "0x4000FEA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001539")]
    		[Address(RVA = "0x5CCCE0", Offset = "0x5CB6E0", VA = "0x1805CCCE0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600153A")]
    		[Address(RVA = "0x5CD450", Offset = "0x5CBE50", VA = "0x1805CD450", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000449")]
    	[CompilerGenerated]
    	private struct _003CWaitCompletion_003Ed__111
    {
    		[Token(Token = "0x4000FEB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FEC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000FED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000FEE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FEF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600153B")]
    		[Address(RVA = "0x5CD480", Offset = "0x5CBE80", VA = "0x1805CD480", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600153C")]
    		[Address(RVA = "0x5CD6E0", Offset = "0x5CC0E0", VA = "0x1805CD6E0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200044A")]
    	[CompilerGenerated]
    	private struct _003CWaitForInputInAutoPlay_003Ed__123
    {
    		[Token(Token = "0x4000FF0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FF1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FF2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FF3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000FF4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private YieldAwaitable.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600153D")]
    		[Address(RVA = "0x5CD710", Offset = "0x5CC110", VA = "0x1805CD710", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600153E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200044B")]
    	[CompilerGenerated]
    	private struct _003CWaitForWaitForInputDisabled_003Ed__122
    {
    		[Token(Token = "0x4000FF5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000FF6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000FF7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptPlayer _003C_003E4__this;

    		[Token(Token = "0x4000FF8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600153F")]
    		[Address(RVA = "0x5CDF00", Offset = "0x5CC900", VA = "0x1805CDF00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001540")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000F9D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private readonly List<Func<Command, UniTask>> preExecutionTasks;

    	[Token(Token = "0x4000F9E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private readonly List<Func<Command, UniTask>> postExecutionTasks;

    	[Token(Token = "0x4000F9F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private readonly Queue<Func<UniTask>> onCompleteTasks;

    	[Token(Token = "0x4000FA0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	private readonly HashSet<Command> playedCommands;

    	[Token(Token = "0x4000FA1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	private readonly IInputManager input;

    	[Token(Token = "0x4000FA2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	private readonly IScriptManager scripts;

    	[Token(Token = "0x4000FA3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	private readonly IStateManager state;

    	[Token(Token = "0x4000FA4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	private bool executedPlayedCommand;

    	[Token(Token = "0x4000FA5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA9")]
    	private bool shouldCompleteNextCommand;

    	[Token(Token = "0x4000FA6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private PlayedScriptRegister playedRegister;

    	[Token(Token = "0x4000FA7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private CancellationTokenSource playRoutineCTS;

    	[Token(Token = "0x4000FA8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	private CancellationTokenSource commandExecutionCTS;

    	[Token(Token = "0x4000FA9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	private CancellationTokenSource completionCTS;

    	[Token(Token = "0x4000FAA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	private UniTaskCompletionSource waitForWaitForInputDisabledTCS;

    	[Token(Token = "0x4000FAB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private UniTaskCompletionSource completeTCS;

    	[Token(Token = "0x4000FAC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	private IInputSampler continueInput;

    	[Token(Token = "0x4000FAD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private IInputSampler skipInput;

    	[Token(Token = "0x4000FAE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	private IInputSampler toggleSkipInput;

    	[Token(Token = "0x4000FAF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	private IInputSampler autoPlayInput;

    	[Token(Token = "0x170002EA")]
    	public virtual ScriptPlayerConfiguration Configuration
    	{
    		[Token(Token = "0x60014E0")]
    		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "56")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002EB")]
    	public virtual bool Playing
    	{
    		[Token(Token = "0x60014E1")]
    		[Address(RVA = "0x5C3DA0", Offset = "0x5C27A0", VA = "0x1805C3DA0", Slot = "57")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170002EC")]
    	public virtual bool Completing
    	{
    		[Token(Token = "0x60014E2")]
    		[Address(RVA = "0x5C3DB0", Offset = "0x5C27B0", VA = "0x1805C3DB0", Slot = "58")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170002ED")]
    	public virtual bool SkipActive
    	{
    		[Token(Token = "0x60014E3")]
    		[Address(RVA = "0x456370", Offset = "0x454D70", VA = "0x180456370", Slot = "59")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60014E4")]
    		[Address(RVA = "0x539F80", Offset = "0x538980", VA = "0x180539F80")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002EE")]
    	public virtual bool AutoPlayActive
    	{
    		[Token(Token = "0x60014E5")]
    		[Address(RVA = "0x539F90", Offset = "0x538990", VA = "0x180539F90", Slot = "60")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60014E6")]
    		[Address(RVA = "0x539FA0", Offset = "0x5389A0", VA = "0x180539FA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002EF")]
    	public virtual bool WaitingForInput
    	{
    		[Token(Token = "0x60014E7")]
    		[Address(RVA = "0x5C3DC0", Offset = "0x5C27C0", VA = "0x1805C3DC0", Slot = "61")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60014E8")]
    		[Address(RVA = "0x5C3DD0", Offset = "0x5C27D0", VA = "0x1805C3DD0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002F0")]
    	public virtual PlayerSkipMode SkipMode
    	{
    		[Token(Token = "0x60014E9")]
    		[Address(RVA = "0x5C3DE0", Offset = "0x5C27E0", VA = "0x1805C3DE0", Slot = "62")]
    		[CompilerGenerated]
    		get
    		{
    			return default(PlayerSkipMode);
    		}
    		[Token(Token = "0x60014EA")]
    		[Address(RVA = "0x5C3DF0", Offset = "0x5C27F0", VA = "0x1805C3DF0", Slot = "63")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170002F1")]
    	public virtual Script PlayedScript
    	{
    		[Token(Token = "0x60014EB")]
    		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "64")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60014EC")]
    		[Address(RVA = "0x458180", Offset = "0x456B80", VA = "0x180458180")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002F2")]
    	public virtual Command PlayedCommand
    	{
    		[Token(Token = "0x60014ED")]
    		[Address(RVA = "0x5C3E00", Offset = "0x5C2800", VA = "0x1805C3E00", Slot = "65")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002F3")]
    	public virtual IReadOnlyCollection<Command> ExecutingCommands
    	{
    		[Token(Token = "0x60014EE")]
    		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "66")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002F4")]
    	public virtual PlaybackSpot PlaybackSpot
    	{
    		[Token(Token = "0x60014EF")]
    		[Address(RVA = "0x5C3E70", Offset = "0x5C2870", VA = "0x1805C3E70", Slot = "67")]
    		get
    		{
    			return default(PlaybackSpot);
    		}
    	}

    	[Token(Token = "0x170002F5")]
    	public virtual ScriptPlaylist Playlist
    	{
    		[Token(Token = "0x60014F0")]
    		[Address(RVA = "0x5C3EC0", Offset = "0x5C28C0", VA = "0x1805C3EC0", Slot = "68")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002F6")]
    	public virtual int PlayedIndex
    	{
    		[Token(Token = "0x60014F1")]
    		[Address(RVA = "0x5C3EF0", Offset = "0x5C28F0", VA = "0x1805C3EF0", Slot = "69")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x60014F2")]
    		[Address(RVA = "0x5C3F00", Offset = "0x5C2900", VA = "0x1805C3F00")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002F7")]
    	public virtual Stack<PlaybackSpot> GosubReturnSpots
    	{
    		[Token(Token = "0x60014F3")]
    		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "70")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60014F4")]
    		[Address(RVA = "0x5C3F10", Offset = "0x5C2910", VA = "0x1805C3F10")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002F8")]
    	public virtual int PlayedCommandsCount
    	{
    		[Token(Token = "0x60014F5")]
    		[Address(RVA = "0x5C3F70", Offset = "0x5C2970", VA = "0x1805C3F70", Slot = "71")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1400006D")]
    	public event Action<Script> OnPlay
    	{
    		[Token(Token = "0x60014D2")]
    		[Address(RVA = "0x5C2EC0", Offset = "0x5C18C0", VA = "0x1805C2EC0", Slot = "13")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014D3")]
    		[Address(RVA = "0x5C2FD0", Offset = "0x5C19D0", VA = "0x1805C2FD0", Slot = "14")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400006E")]
    	public event Action<Script> OnStop
    	{
    		[Token(Token = "0x60014D4")]
    		[Address(RVA = "0x5C30E0", Offset = "0x5C1AE0", VA = "0x1805C30E0", Slot = "15")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014D5")]
    		[Address(RVA = "0x5C31F0", Offset = "0x5C1BF0", VA = "0x1805C31F0", Slot = "16")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400006F")]
    	public event Action<Command> OnCommandExecutionStart
    	{
    		[Token(Token = "0x60014D6")]
    		[Address(RVA = "0x5C3300", Offset = "0x5C1D00", VA = "0x1805C3300", Slot = "17")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014D7")]
    		[Address(RVA = "0x5C3410", Offset = "0x5C1E10", VA = "0x1805C3410", Slot = "18")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000070")]
    	public event Action<Command> OnCommandExecutionFinish
    	{
    		[Token(Token = "0x60014D8")]
    		[Address(RVA = "0x5C3520", Offset = "0x5C1F20", VA = "0x1805C3520", Slot = "19")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014D9")]
    		[Address(RVA = "0x5C3630", Offset = "0x5C2030", VA = "0x1805C3630", Slot = "20")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000071")]
    	public event Action<bool> OnSkip
    	{
    		[Token(Token = "0x60014DA")]
    		[Address(RVA = "0x5C3740", Offset = "0x5C2140", VA = "0x1805C3740", Slot = "21")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014DB")]
    		[Address(RVA = "0x5C3850", Offset = "0x5C2250", VA = "0x1805C3850", Slot = "22")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000072")]
    	public event Action<bool> OnAutoPlay
    	{
    		[Token(Token = "0x60014DC")]
    		[Address(RVA = "0x5C3960", Offset = "0x5C2360", VA = "0x1805C3960", Slot = "23")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014DD")]
    		[Address(RVA = "0x5C3A70", Offset = "0x5C2470", VA = "0x1805C3A70", Slot = "24")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000073")]
    	public event Action<bool> OnWaitingForInput
    	{
    		[Token(Token = "0x60014DE")]
    		[Address(RVA = "0x5C3B80", Offset = "0x5C2580", VA = "0x1805C3B80", Slot = "25")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014DF")]
    		[Address(RVA = "0x5C3C90", Offset = "0x5C2690", VA = "0x1805C3C90", Slot = "26")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60014F6")]
    	[Address(RVA = "0x5C3FA0", Offset = "0x5C29A0", VA = "0x1805C3FA0")]
    	public ScriptPlayer(ScriptPlayerConfiguration cfg = default, IInputManager input = default, IScriptManager scripts = default, IStateManager state = default)
    	{
    	}

    	[Token(Token = "0x60014F7")]
    	[Address(RVA = "0x5C45A0", Offset = "0x5C2FA0", VA = "0x1805C45A0", Slot = "72")]
    	public virtual UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014F8")]
    	[Address(RVA = "0x5C4AE0", Offset = "0x5C34E0", VA = "0x1805C4AE0", Slot = "73")]
    	public virtual void ResetService()
    	{
    	}

    	[Token(Token = "0x60014F9")]
    	[Address(RVA = "0x5C4C60", Offset = "0x5C3660", VA = "0x1805C4C60", Slot = "74")]
    	public virtual void DestroyService()
    	{
    	}

    	[Token(Token = "0x60014FA")]
    	[Address(RVA = "0x5C4EE0", Offset = "0x5C38E0", VA = "0x1805C4EE0", Slot = "75")]
    	public virtual void SaveServiceState(SettingsStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60014FB")]
    	[Address(RVA = "0x5C4FF0", Offset = "0x5C39F0", VA = "0x1805C4FF0", Slot = "76")]
    	public virtual UniTask LoadServiceState(SettingsStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014FC")]
    	[Address(RVA = "0x5C50E0", Offset = "0x5C3AE0", VA = "0x1805C50E0", Slot = "77")]
    	public virtual void SaveServiceState(GlobalStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60014FD")]
    	[Address(RVA = "0x5C5240", Offset = "0x5C3C40", VA = "0x1805C5240", Slot = "78")]
    	public virtual UniTask LoadServiceState(GlobalStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014FE")]
    	[Address(RVA = "0x5C5360", Offset = "0x5C3D60", VA = "0x1805C5360", Slot = "79")]
    	public virtual void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60014FF")]
    	[Address(RVA = "0x5C5600", Offset = "0x5C4000", VA = "0x1805C5600", Slot = "80")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__97))]
    	public virtual UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001500")]
    	[Address(RVA = "0x5C5780", Offset = "0x5C4180", VA = "0x1805C5780", Slot = "81")]
    	public virtual void AddPreExecutionTask(Func<Command, UniTask> task)
    	{
    	}

    	[Token(Token = "0x6001501")]
    	[Address(RVA = "0x5C57E0", Offset = "0x5C41E0", VA = "0x1805C57E0", Slot = "82")]
    	public virtual void RemovePreExecutionTask(Func<Command, UniTask> task)
    	{
    	}

    	[Token(Token = "0x6001502")]
    	[Address(RVA = "0x5C5840", Offset = "0x5C4240", VA = "0x1805C5840", Slot = "83")]
    	public virtual void AddPostExecutionTask(Func<Command, UniTask> task)
    	{
    	}

    	[Token(Token = "0x6001503")]
    	[Address(RVA = "0x5C58A0", Offset = "0x5C42A0", VA = "0x1805C58A0", Slot = "84")]
    	public virtual void RemovePostExecutionTask(Func<Command, UniTask> task)
    	{
    	}

    	[Token(Token = "0x6001504")]
    	[Address(RVA = "0x5C5900", Offset = "0x5C4300", VA = "0x1805C5900", Slot = "85")]
    	public virtual void Play(string scriptPath, int playlistIndex = 0)
    	{
    	}

    	[Token(Token = "0x6001505")]
    	[Address(RVA = "0x5C5AA0", Offset = "0x5C44A0", VA = "0x1805C5AA0", Slot = "86")]
    	public virtual void Resume([Optional] int? playlistIndex)
    	{
    	}

    	[Token(Token = "0x6001506")]
    	[Address(RVA = "0x5C5ED0", Offset = "0x5C48D0", VA = "0x1805C5ED0", Slot = "87")]
    	public virtual void Stop()
    	{
    	}

    	[Token(Token = "0x6001507")]
    	[Address(RVA = "0x5C5FF0", Offset = "0x5C49F0", VA = "0x1805C5FF0", Slot = "88")]
    	[AsyncStateMachine(typeof(_003CRewind_003Ed__105))]
    	public virtual UniTask<bool> Rewind(int lineIndex)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6001508")]
    	[Address(RVA = "0x5C61A0", Offset = "0x5C4BA0", VA = "0x1805C61A0", Slot = "89")]
    	public virtual void SetSkipEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x6001509")]
    	[Address(RVA = "0x5C6370", Offset = "0x5C4D70", VA = "0x1805C6370", Slot = "90")]
    	public virtual void SetAutoPlayEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x600150A")]
    	[Address(RVA = "0x5C6400", Offset = "0x5C4E00", VA = "0x1805C6400", Slot = "91")]
    	public virtual void SetWaitingForInputEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x600150B")]
    	[Address(RVA = "0x5C6570", Offset = "0x5C4F70", VA = "0x1805C6570", Slot = "92")]
    	[AsyncStateMachine(typeof(_003CComplete_003Ed__109))]
    	public virtual UniTask Complete([Optional] Func<UniTask> onComplete)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600150C")]
    	[Address(RVA = "0x5C66F0", Offset = "0x5C50F0", VA = "0x1805C66F0", Slot = "93")]
    	public virtual bool HasPlayed(string scriptPath, [Optional] int? playlistIndex)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600150D")]
    	[Address(RVA = "0x5C67F0", Offset = "0x5C51F0", VA = "0x1805C67F0", Slot = "94")]
    	[AsyncStateMachine(typeof(_003CWaitCompletion_003Ed__111))]
    	protected virtual UniTask<bool> WaitCompletion(AsyncToken token)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x600150E")]
    	[Address(RVA = "0x5C69E0", Offset = "0x5C53E0", VA = "0x1805C69E0", Slot = "95")]
    	protected virtual int FindPlayableIndex(PlaybackSpot spot)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600150F")]
    	[Address(RVA = "0x5C7080", Offset = "0x5C5A80", VA = "0x1805C7080", Slot = "96")]
    	protected virtual void EnableSkip()
    	{
    	}

    	[Token(Token = "0x6001510")]
    	[Address(RVA = "0x5C70A0", Offset = "0x5C5AA0", VA = "0x1805C70A0", Slot = "97")]
    	protected virtual void DisableSkip()
    	{
    	}

    	[Token(Token = "0x6001511")]
    	[Address(RVA = "0x5C70C0", Offset = "0x5C5AC0", VA = "0x1805C70C0", Slot = "98")]
    	protected virtual void ToggleSkip()
    	{
    	}

    	[Token(Token = "0x6001512")]
    	[Address(RVA = "0x5C7100", Offset = "0x5C5B00", VA = "0x1805C7100", Slot = "99")]
    	protected virtual void EnableAutoPlay()
    	{
    	}

    	[Token(Token = "0x6001513")]
    	[Address(RVA = "0x5C7120", Offset = "0x5C5B20", VA = "0x1805C7120", Slot = "100")]
    	protected virtual void DisableAutoPlay()
    	{
    	}

    	[Token(Token = "0x6001514")]
    	[Address(RVA = "0x5C7140", Offset = "0x5C5B40", VA = "0x1805C7140", Slot = "101")]
    	protected virtual void ToggleAutoPlay()
    	{
    	}

    	[Token(Token = "0x6001515")]
    	[Address(RVA = "0x5C7180", Offset = "0x5C5B80", VA = "0x1805C7180", Slot = "102")]
    	protected virtual void EnableWaitingForInput()
    	{
    	}

    	[Token(Token = "0x6001516")]
    	[Address(RVA = "0x5C71A0", Offset = "0x5C5BA0", VA = "0x1805C71A0", Slot = "103")]
    	protected virtual void DisableWaitingForInput()
    	{
    	}

    	[Token(Token = "0x6001517")]
    	[Address(RVA = "0x5C71C0", Offset = "0x5C5BC0", VA = "0x1805C71C0", Slot = "104")]
    	protected virtual bool GetSkipAllowed()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001518")]
    	[Address(RVA = "0x5C72D0", Offset = "0x5C5CD0", VA = "0x1805C72D0", Slot = "105")]
    	[AsyncStateMachine(typeof(_003CWaitForWaitForInputDisabled_003Ed__122))]
    	protected virtual UniTask WaitForWaitForInputDisabled()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001519")]
    	[Address(RVA = "0x5C73F0", Offset = "0x5C5DF0", VA = "0x1805C73F0", Slot = "106")]
    	[AsyncStateMachine(typeof(_003CWaitForInputInAutoPlay_003Ed__123))]
    	protected virtual UniTask WaitForInputInAutoPlay()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600151A")]
    	[Address(RVA = "0x5C7520", Offset = "0x5C5F20", VA = "0x1805C7520", Slot = "107")]
    	[AsyncStateMachine(typeof(_003CExecutePlayedCommand_003Ed__124))]
    	protected virtual UniTask ExecutePlayedCommand(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600151B")]
    	[Address(RVA = "0x5C76B0", Offset = "0x5C60B0", VA = "0x1805C76B0", Slot = "108")]
    	[AsyncStateMachine(typeof(_003CExecuteIgnoringCancellation_003Ed__125))]
    	protected virtual UniTask ExecuteIgnoringCancellation(Command command, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600151C")]
    	[Address(RVA = "0x5C78A0", Offset = "0x5C62A0", VA = "0x1805C78A0", Slot = "109")]
    	[AsyncStateMachine(typeof(_003CPlayRoutine_003Ed__126))]
    	protected virtual UniTask PlayRoutine(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600151D")]
    	[Address(RVA = "0x5C7A40", Offset = "0x5C6440", VA = "0x1805C7A40", Slot = "110")]
    	[AsyncStateMachine(typeof(_003CFastForwardRoutine_003Ed__127))]
    	protected virtual UniTask<bool> FastForwardRoutine(AsyncToken token, int targetPlaylistIndex, bool executePlayedCommand)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x600151E")]
    	[Address(RVA = "0x5C7C50", Offset = "0x5C6650", VA = "0x1805C7C50", Slot = "111")]
    	protected virtual bool SelectNextCommand()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600151F")]
    	[Address(RVA = "0x5C8130", Offset = "0x5C6B30", VA = "0x1805C8130", Slot = "112")]
    	protected virtual void CancelCommands()
    	{
    	}
    }
}
