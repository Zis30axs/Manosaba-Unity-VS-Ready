using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace Naninovel;

[Token(Token = "0x200047E")]
[InitializeAtRuntime(-2147483648, null)]
[Goto.DontReset]
public class StateManager : IStateManager, IEngineService<StateConfiguration>, IEngineService
{
	[Token(Token = "0x2000482")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass69_0
	{
		[Token(Token = "0x40010E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x40010E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameStateMap state;

		[Token(Token = "0x40010E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string slotId;

		[Token(Token = "0x6001683")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass69_0()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000485")]
	[CompilerGenerated]
	private struct _003CHandleRollbackInputStart_003Ed__90
{
		[Token(Token = "0x40010EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40010EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40010EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x40010ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600168A")]
		[Address(RVA = "0x5E1BC0", Offset = "0x5E05C0", VA = "0x1805E1BC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600168B")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000486")]
	[CompilerGenerated]
	private struct _003CLoadAllServicesFromStateAsync_003Ed__87<TService, TState> where TService : class, IStatefulService<TState> where TState : StateMap, new()
{
		[Token(Token = "0x40010EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40010EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40010F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TState state;

		[Token(Token = "0x40010F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<TService> _003C_003E7__wrap1;

		[Token(Token = "0x40010F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600168C")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600168D")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000487")]
	[CompilerGenerated]
	private struct _003CLoadGame_003Ed__71
{
		[Token(Token = "0x40010F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40010F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x40010F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string slotId;

		[Token(Token = "0x40010F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x40010F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private bool _003Cquick_003E5__2;

		[Token(Token = "0x40010F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private GameStateMap _003Cstate_003E5__3;

		[Token(Token = "0x40010F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40010FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600168E")]
		[Address(RVA = "0x5E1FA0", Offset = "0x5E09A0", VA = "0x1805E1FA0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600168F")]
		[Address(RVA = "0x5E2C60", Offset = "0x5E1660", VA = "0x1805E2C60", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000488")]
	[CompilerGenerated]
	private struct _003CPerformOnGameDeserializeTasks_003Ed__89
{
		[Token(Token = "0x40010FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40010FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40010FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x40010FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameStateMap state;

		[Token(Token = "0x40010FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private int _003Ci_003E5__2;

		[Token(Token = "0x4001100")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001690")]
		[Address(RVA = "0x5E2C90", Offset = "0x5E1690", VA = "0x1805E2C90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001691")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000489")]
	[CompilerGenerated]
	private struct _003CPerformPostEngineInitializationTasks_003Ed__92
{
		[Token(Token = "0x4001101")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001102")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001103")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001104")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001692")]
		[Address(RVA = "0x5E2FE0", Offset = "0x5E19E0", VA = "0x1805E2FE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001693")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200048A")]
	[CompilerGenerated]
	private struct _003CQuickLoad_003Ed__72
{
		[Token(Token = "0x4001105")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001106")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x4001107")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001108")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001694")]
		[Address(RVA = "0x5E3740", Offset = "0x5E2140", VA = "0x1805E3740", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001695")]
		[Address(RVA = "0x5E39D0", Offset = "0x5E23D0", VA = "0x1805E39D0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200048B")]
	[CompilerGenerated]
	private struct _003CQuickSave_003Ed__70
{
		[Token(Token = "0x4001109")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400110A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x400110B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x400110C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001696")]
		[Address(RVA = "0x5E3A00", Offset = "0x5E2400", VA = "0x1805E3A00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001697")]
		[Address(RVA = "0x5E4060", Offset = "0x5E2A60", VA = "0x1805E4060", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200048C")]
	[CompilerGenerated]
	private struct _003CResetState_003Ed__75
{
		[Token(Token = "0x400110D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400110E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400110F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001110")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Func<UniTask>[] tasks;

		[Token(Token = "0x4001111")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001698")]
		[Address(RVA = "0x5E4090", Offset = "0x5E2A90", VA = "0x1805E4090", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001699")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200048D")]
	[CompilerGenerated]
	private struct _003CResetState_003Ed__76
{
		[Token(Token = "0x4001112")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001113")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001114")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public IReadOnlyCollection<string> exclude;

		[Token(Token = "0x4001115")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001116")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Func<UniTask>[] tasks;

		[Token(Token = "0x4001117")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600169A")]
		[Address(RVA = "0x5E4350", Offset = "0x5E2D50", VA = "0x1805E4350", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600169B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200048E")]
	[CompilerGenerated]
	private struct _003CResetState_003Ed__77
{
		[Token(Token = "0x4001118")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001119")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400111A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x400111B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public IReadOnlyCollection<Type> exclude;

		[Token(Token = "0x400111C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Func<UniTask>[] tasks;

		[Token(Token = "0x400111D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private InteractionBlocker _003C_003E7__wrap1;

		[Token(Token = "0x400111E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400111F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Func<UniTask>[] _003C_003E7__wrap2;

		[Token(Token = "0x4001120")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private int _003C_003E7__wrap3;

		[Token(Token = "0x600169C")]
		[Address(RVA = "0x5E4950", Offset = "0x5E3350", VA = "0x1805E4950", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600169D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200048F")]
	[CompilerGenerated]
	private struct _003CRollback_003Ed__79
{
		[Token(Token = "0x4001121")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001122")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x4001123")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001124")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Predicate<GameStateMap> predicate;

		[Token(Token = "0x4001125")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600169E")]
		[Address(RVA = "0x5E5210", Offset = "0x5E3C10", VA = "0x1805E5210", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600169F")]
		[Address(RVA = "0x5E55F0", Offset = "0x5E3FF0", VA = "0x1805E55F0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000490")]
	[CompilerGenerated]
	private struct _003CRollbackToState_003Ed__85
{
		[Token(Token = "0x4001126")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001127")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001128")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001129")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameStateMap state;

		[Token(Token = "0x400112A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400112B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60016A0")]
		[Address(RVA = "0x5E5620", Offset = "0x5E4020", VA = "0x1805E5620", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60016A1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000491")]
	[CompilerGenerated]
	private struct _003CSaveGame_003Ed__69
{
		[Token(Token = "0x400112C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400112D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x400112E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x400112F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string slotId;

		[Token(Token = "0x4001130")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass69_0 _003C_003E8__1;

		[Token(Token = "0x4001131")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private bool _003Cquick_003E5__2;

		[Token(Token = "0x4001132")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60016A2")]
		[Address(RVA = "0x5E5DE0", Offset = "0x5E47E0", VA = "0x1805E5DE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60016A3")]
		[Address(RVA = "0x5E6850", Offset = "0x5E5250", VA = "0x1805E6850", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000492")]
	[CompilerGenerated]
	private struct _003CSaveGlobal_003Ed__73
{
		[Token(Token = "0x4001133")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001134")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001135")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x4001136")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60016A4")]
		[Address(RVA = "0x5E6880", Offset = "0x5E5280", VA = "0x1805E6880", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60016A5")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000493")]
	[CompilerGenerated]
	private struct _003CSaveSettings_003Ed__74
{
		[Token(Token = "0x4001137")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001138")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001139")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StateManager _003C_003E4__this;

		[Token(Token = "0x400113A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60016A6")]
		[Address(RVA = "0x5E6CE0", Offset = "0x5E56E0", VA = "0x1805E6CE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60016A7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40010CC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private readonly Queue<GameStateMap> rollbackTaskQueue;

	[Token(Token = "0x40010CD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private readonly List<Action<GameStateMap>> onGameSerializeTasks;

	[Token(Token = "0x40010CE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private readonly List<Func<GameStateMap, UniTask>> onGameDeserializeTasks;

	[Token(Token = "0x40010CF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private IInputSampler rollbackInput;

	[Token(Token = "0x40010D0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private IScriptPlayer scriptPlayer;

	[Token(Token = "0x40010D1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private ICameraManager cameraManager;

	[Token(Token = "0x17000325")]
	public virtual StateConfiguration Configuration
	{
		[Token(Token = "0x6001649")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "50")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000326")]
	public virtual GlobalStateMap GlobalState
	{
		[Token(Token = "0x600164A")]
		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "51")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600164B")]
		[Address(RVA = "0x458180", Offset = "0x456B80", VA = "0x180458180")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000327")]
	public virtual SettingsStateMap SettingsState
	{
		[Token(Token = "0x600164C")]
		[Address(RVA = "0x45A0C0", Offset = "0x458AC0", VA = "0x18045A0C0", Slot = "52")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600164D")]
		[Address(RVA = "0x4C0F30", Offset = "0x4BF930", VA = "0x1804C0F30")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000328")]
	public virtual ISaveSlotManager<GlobalStateMap> GlobalSlotManager
	{
		[Token(Token = "0x600164E")]
		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "53")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000329")]
	public virtual ISaveSlotManager<GameStateMap> GameSlotManager
	{
		[Token(Token = "0x600164F")]
		[Address(RVA = "0x478200", Offset = "0x476C00", VA = "0x180478200", Slot = "54")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700032A")]
	public virtual ISaveSlotManager<SettingsStateMap> SettingsSlotManager
	{
		[Token(Token = "0x6001650")]
		[Address(RVA = "0x478210", Offset = "0x476C10", VA = "0x180478210", Slot = "55")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700032B")]
	public virtual bool QuickLoadAvailable
	{
		[Token(Token = "0x6001651")]
		[Address(RVA = "0x5DD410", Offset = "0x5DBE10", VA = "0x1805DD410", Slot = "56")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700032C")]
	public virtual bool AnyGameSaveExists
	{
		[Token(Token = "0x6001652")]
		[Address(RVA = "0x5DD4A0", Offset = "0x5DBEA0", VA = "0x1805DD4A0", Slot = "57")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700032D")]
	public virtual bool RollbackInProgress
	{
		[Token(Token = "0x6001653")]
		[Address(RVA = "0x5DD510", Offset = "0x5DBF10", VA = "0x1805DD510", Slot = "58")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700032E")]
	protected virtual string LastQuickSaveSlotId
	{
		[Token(Token = "0x6001654")]
		[Address(RVA = "0x5DD560", Offset = "0x5DBF60", VA = "0x1805DD560", Slot = "59")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700032F")]
	protected virtual StateRollbackStack RollbackStack
	{
		[Token(Token = "0x6001655")]
		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "60")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1400007C")]
	public event Action<GameSaveLoadArgs> OnGameLoadStarted
	{
		[Token(Token = "0x6001639")]
		[Address(RVA = "0x5DC410", Offset = "0x5DAE10", VA = "0x1805DC410", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600163A")]
		[Address(RVA = "0x5DC520", Offset = "0x5DAF20", VA = "0x1805DC520", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400007D")]
	public event Action<GameSaveLoadArgs> OnGameLoadFinished
	{
		[Token(Token = "0x600163B")]
		[Address(RVA = "0x5DC630", Offset = "0x5DB030", VA = "0x1805DC630", Slot = "6")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600163C")]
		[Address(RVA = "0x5DC740", Offset = "0x5DB140", VA = "0x1805DC740", Slot = "7")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400007E")]
	public event Action<GameSaveLoadArgs> OnGameSaveStarted
	{
		[Token(Token = "0x600163D")]
		[Address(RVA = "0x5DC850", Offset = "0x5DB250", VA = "0x1805DC850", Slot = "8")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600163E")]
		[Address(RVA = "0x5DC960", Offset = "0x5DB360", VA = "0x1805DC960", Slot = "9")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400007F")]
	public event Action<GameSaveLoadArgs> OnGameSaveFinished
	{
		[Token(Token = "0x600163F")]
		[Address(RVA = "0x5DCA70", Offset = "0x5DB470", VA = "0x1805DCA70", Slot = "10")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001640")]
		[Address(RVA = "0x5DCB80", Offset = "0x5DB580", VA = "0x1805DCB80", Slot = "11")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000080")]
	public event Action OnResetStarted
	{
		[Token(Token = "0x6001641")]
		[Address(RVA = "0x5DCC90", Offset = "0x5DB690", VA = "0x1805DCC90", Slot = "12")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001642")]
		[Address(RVA = "0x5DCD80", Offset = "0x5DB780", VA = "0x1805DCD80", Slot = "13")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000081")]
	public event Action OnResetFinished
	{
		[Token(Token = "0x6001643")]
		[Address(RVA = "0x5DCE70", Offset = "0x5DB870", VA = "0x1805DCE70", Slot = "14")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001644")]
		[Address(RVA = "0x5DCF60", Offset = "0x5DB960", VA = "0x1805DCF60", Slot = "15")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000082")]
	public event Action OnRollbackStarted
	{
		[Token(Token = "0x6001645")]
		[Address(RVA = "0x5DD050", Offset = "0x5DBA50", VA = "0x1805DD050", Slot = "16")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001646")]
		[Address(RVA = "0x5DD140", Offset = "0x5DBB40", VA = "0x1805DD140", Slot = "17")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000083")]
	public event Action OnRollbackFinished
	{
		[Token(Token = "0x6001647")]
		[Address(RVA = "0x5DD230", Offset = "0x5DBC30", VA = "0x1805DD230", Slot = "18")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001648")]
		[Address(RVA = "0x5DD320", Offset = "0x5DBD20", VA = "0x1805DD320", Slot = "19")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6001656")]
	[Address(RVA = "0x5DD600", Offset = "0x5DC000", VA = "0x1805DD600")]
	public StateManager(StateConfiguration cfg = default)
	{
	}

	[Token(Token = "0x6001657")]
	[Address(RVA = "0x5DDFA0", Offset = "0x5DC9A0", VA = "0x1805DDFA0", Slot = "61")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001658")]
	[Address(RVA = "0x5DE100", Offset = "0x5DCB00", VA = "0x1805DE100", Slot = "62")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x6001659")]
	[Address(RVA = "0x5DE170", Offset = "0x5DCB70", VA = "0x1805DE170", Slot = "63")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x600165A")]
	[Address(RVA = "0x5DE310", Offset = "0x5DCD10", VA = "0x1805DE310", Slot = "64")]
	public virtual void AddOnGameSerializeTask(Action<GameStateMap> task)
	{
	}

	[Token(Token = "0x600165B")]
	[Address(RVA = "0x5DE370", Offset = "0x5DCD70", VA = "0x1805DE370", Slot = "65")]
	public virtual void RemoveOnGameSerializeTask(Action<GameStateMap> task)
	{
	}

	[Token(Token = "0x600165C")]
	[Address(RVA = "0x5DE3D0", Offset = "0x5DCDD0", VA = "0x1805DE3D0", Slot = "66")]
	public virtual void AddOnGameDeserializeTask(Func<GameStateMap, UniTask> task)
	{
	}

	[Token(Token = "0x600165D")]
	[Address(RVA = "0x5DE430", Offset = "0x5DCE30", VA = "0x1805DE430", Slot = "67")]
	public virtual void RemoveOnGameDeserializeTask(Func<GameStateMap, UniTask> task)
	{
	}

	[Token(Token = "0x600165E")]
	[Address(RVA = "0x5DE490", Offset = "0x5DCE90", VA = "0x1805DE490", Slot = "68")]
	[AsyncStateMachine(typeof(_003CSaveGame_003Ed__69))]
	public virtual UniTask<GameStateMap> SaveGame(string slotId)
	{
		return default(UniTask<GameStateMap>);
	}

	[Token(Token = "0x600165F")]
	[Address(RVA = "0x5DE680", Offset = "0x5DD080", VA = "0x1805DE680", Slot = "69")]
	[AsyncStateMachine(typeof(_003CQuickSave_003Ed__70))]
	public virtual UniTask<GameStateMap> QuickSave()
	{
		return default(UniTask<GameStateMap>);
	}

	[Token(Token = "0x6001660")]
	[Address(RVA = "0x5DE810", Offset = "0x5DD210", VA = "0x1805DE810", Slot = "70")]
	[AsyncStateMachine(typeof(_003CLoadGame_003Ed__71))]
	public virtual UniTask<GameStateMap> LoadGame(string slotId)
	{
		return default(UniTask<GameStateMap>);
	}

	[Token(Token = "0x6001661")]
	[Address(RVA = "0x5DEA10", Offset = "0x5DD410", VA = "0x1805DEA10", Slot = "71")]
	[AsyncStateMachine(typeof(_003CQuickLoad_003Ed__72))]
	public virtual UniTask<GameStateMap> QuickLoad()
	{
		return default(UniTask<GameStateMap>);
	}

	[Token(Token = "0x6001662")]
	[Address(RVA = "0x5DEBA0", Offset = "0x5DD5A0", VA = "0x1805DEBA0", Slot = "72")]
	[AsyncStateMachine(typeof(_003CSaveGlobal_003Ed__73))]
	public virtual UniTask SaveGlobal()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001663")]
	[Address(RVA = "0x5DECC0", Offset = "0x5DD6C0", VA = "0x1805DECC0", Slot = "73")]
	[AsyncStateMachine(typeof(_003CSaveSettings_003Ed__74))]
	public virtual UniTask SaveSettings()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001664")]
	[Address(RVA = "0x5DEDE0", Offset = "0x5DD7E0", VA = "0x1805DEDE0", Slot = "74")]
	[AsyncStateMachine(typeof(_003CResetState_003Ed__75))]
	public virtual UniTask ResetState(params Func<UniTask>[] tasks)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001665")]
	[Address(RVA = "0x5DEF60", Offset = "0x5DD960", VA = "0x1805DEF60", Slot = "75")]
	[AsyncStateMachine(typeof(_003CResetState_003Ed__76))]
	public virtual UniTask ResetState(IReadOnlyCollection<string> exclude, params Func<UniTask>[] tasks)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001666")]
	[Address(RVA = "0x5DF140", Offset = "0x5DDB40", VA = "0x1805DF140", Slot = "76")]
	[AsyncStateMachine(typeof(_003CResetState_003Ed__77))]
	public virtual UniTask ResetState(IReadOnlyCollection<Type> exclude, params Func<UniTask>[] tasks)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001667")]
	[Address(RVA = "0x5DF330", Offset = "0x5DDD30", VA = "0x1805DF330", Slot = "77")]
	public virtual void PushRollbackSnapshot(bool allowPlayerRollback)
	{
	}

	[Token(Token = "0x6001668")]
	[Address(RVA = "0x5DF450", Offset = "0x5DDE50", VA = "0x1805DF450", Slot = "78")]
	[AsyncStateMachine(typeof(_003CRollback_003Ed__79))]
	public virtual UniTask<bool> Rollback(Predicate<GameStateMap> predicate)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x6001669")]
	[Address(RVA = "0x5DF630", Offset = "0x5DE030", VA = "0x1805DF630", Slot = "79")]
	public virtual GameStateMap PeekRollbackStack()
	{
		return null;
	}

	[Token(Token = "0x600166A")]
	[Address(RVA = "0x5DF6C0", Offset = "0x5DE0C0", VA = "0x1805DF6C0", Slot = "80")]
	public virtual bool CanRollbackTo(Predicate<GameStateMap> predicate)
	{
		return default(bool);
	}

	[Token(Token = "0x600166B")]
	[Address(RVA = "0x5DF7A0", Offset = "0x5DE1A0", VA = "0x1805DF7A0", Slot = "81")]
	public virtual void PurgeRollbackData()
	{
	}

	[Token(Token = "0x600166C")]
	[Address(RVA = "0x5DF910", Offset = "0x5DE310", VA = "0x1805DF910", Slot = "82")]
	protected virtual string SerializeRollbackStack()
	{
		return null;
	}

	[Token(Token = "0x600166D")]
	[Address(RVA = "0x5DFA10", Offset = "0x5DE410", VA = "0x1805DFA10", Slot = "83")]
	protected virtual bool ShouldSerializeSnapshot(GameStateMap state)
	{
		return default(bool);
	}

	[Token(Token = "0x600166E")]
	[Address(RVA = "0x5DFA40", Offset = "0x5DE440", VA = "0x1805DFA40", Slot = "84")]
	[AsyncStateMachine(typeof(_003CRollbackToState_003Ed__85))]
	protected virtual UniTask RollbackToState(GameStateMap state)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600166F")]
	protected virtual void SaveAllServicesToState<TService, TState>(TState state) where TService : class, IStatefulService<TState> where TState : StateMap, new()
	{
	}

	[Token(Token = "0x6001670")]
	[AsyncStateMachine(typeof(_003CLoadAllServicesFromStateAsync_003Ed__87<, >))]
	protected virtual UniTask LoadAllServicesFromStateAsync<TService, TState>(TState state) where TService : class, IStatefulService<TState> where TState : StateMap, new()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001671")]
	[Address(RVA = "0x5DFBD0", Offset = "0x5DE5D0", VA = "0x1805DFBD0", Slot = "87")]
	protected virtual void PerformOnGameSerializeTasks(GameStateMap state)
	{
	}

	[Token(Token = "0x6001672")]
	[Address(RVA = "0x5DFC90", Offset = "0x5DE690", VA = "0x1805DFC90", Slot = "88")]
	[AsyncStateMachine(typeof(_003CPerformOnGameDeserializeTasks_003Ed__89))]
	protected virtual UniTask PerformOnGameDeserializeTasks(GameStateMap state)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001673")]
	[Address(RVA = "0x5DFE10", Offset = "0x5DE810", VA = "0x1805DFE10")]
	[AsyncStateMachine(typeof(_003CHandleRollbackInputStart_003Ed__90))]
	private void HandleRollbackInputStart()
	{
	}

	[Token(Token = "0x6001674")]
	[Address(RVA = "0x5DFF40", Offset = "0x5DE940", VA = "0x1805DFF40")]
	private UniTask HandleCommandPreExecution(Command _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001675")]
	[Address(RVA = "0x5DFFA0", Offset = "0x5DE9A0", VA = "0x1805DFFA0")]
	[AsyncStateMachine(typeof(_003CPerformPostEngineInitializationTasks_003Ed__92))]
	private UniTask PerformPostEngineInitializationTasks()
	{
		return default(UniTask);
	}
}
