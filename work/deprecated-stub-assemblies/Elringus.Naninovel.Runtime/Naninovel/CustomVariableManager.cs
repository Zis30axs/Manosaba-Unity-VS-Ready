#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace Naninovel
{
    [Token(Token = "0x2000320")]
    [InitializeAtRuntime(-2147483647, null)]
    [Goto.DontReset]
    public class CustomVariableManager : IStatefulService<GameStateMap>, IEngineService, IStatefulService<GlobalStateMap>, ICustomVariableManager, IEngineService<CustomVariablesConfiguration>
    {
    	[Serializable]
    	[Token(Token = "0x2000321")]
    	public class GlobalState
    	{
    		[Token(Token = "0x4000BCB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public CustomVariable[] GlobalVariables;

    		[Token(Token = "0x6000F14")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GlobalState()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000322")]
    	public class GameState
    	{
    		[Token(Token = "0x4000BCC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public CustomVariable[] LocalVariables;

    		[Token(Token = "0x6000F15")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000326")]
    	[CompilerGenerated]
    	private struct _003CInitializeService_003Ed__14
    {
    		[Token(Token = "0x4000BD6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000BD7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000BD8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CustomVariableManager _003C_003E4__this;

    		[Token(Token = "0x4000BD9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<Resource>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000F1E")]
    		[Address(RVA = "0x5623B0", Offset = "0x560DB0", VA = "0x1805623B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000F1F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000BC8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly Dictionary<string, CustomVariable> variablesByName;

    	[Token(Token = "0x4000BC9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly Dictionary<string, string> predefineByName;

    	[Token(Token = "0x4000BCA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private readonly ITextManager docs;

    	[Token(Token = "0x17000203")]
    	public virtual CustomVariablesConfiguration Configuration
    	{
    		[Token(Token = "0x6000F01")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "20")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000204")]
    	public virtual IReadOnlyCollection<CustomVariable> Variables
    	{
    		[Token(Token = "0x6000F02")]
    		[Address(RVA = "0x55FA70", Offset = "0x55E470", VA = "0x18055FA70", Slot = "21")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x14000047")]
    	public event Action<CustomVariableUpdatedArgs> OnVariableUpdated
    	{
    		[Token(Token = "0x6000EFF")]
    		[Address(RVA = "0x55F850", Offset = "0x55E250", VA = "0x18055F850", Slot = "11")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000F00")]
    		[Address(RVA = "0x55F960", Offset = "0x55E360", VA = "0x18055F960", Slot = "12")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000F03")]
    	[Address(RVA = "0x55FAC0", Offset = "0x55E4C0", VA = "0x18055FAC0")]
    	public CustomVariableManager(CustomVariablesConfiguration config, ITextManager docs)
    	{
    	}

    	[Token(Token = "0x6000F04")]
    	[Address(RVA = "0x55FF60", Offset = "0x55E960", VA = "0x18055FF60", Slot = "22")]
    	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__14))]
    	public virtual UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000F05")]
    	[Address(RVA = "0x560080", Offset = "0x55EA80", VA = "0x180560080", Slot = "23")]
    	public virtual void ResetService()
    	{
    	}

    	[Token(Token = "0x6000F06")]
    	[Address(RVA = "0x5600F0", Offset = "0x55EAF0", VA = "0x1805600F0", Slot = "24")]
    	public virtual void DestroyService()
    	{
    	}

    	[Token(Token = "0x6000F07")]
    	[Address(RVA = "0x5601F0", Offset = "0x55EBF0", VA = "0x1805601F0", Slot = "25")]
    	public virtual void SaveServiceState(GlobalStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x6000F08")]
    	[Address(RVA = "0x560500", Offset = "0x55EF00", VA = "0x180560500", Slot = "26")]
    	public virtual UniTask LoadServiceState(GlobalStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000F09")]
    	[Address(RVA = "0x5606D0", Offset = "0x55F0D0", VA = "0x1805606D0", Slot = "27")]
    	public virtual void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x6000F0A")]
    	[Address(RVA = "0x5609E0", Offset = "0x55F3E0", VA = "0x1805609E0", Slot = "28")]
    	public virtual UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000F0B")]
    	[Address(RVA = "0x560BB0", Offset = "0x55F5B0", VA = "0x180560BB0", Slot = "29")]
    	public virtual bool VariableExists(string name)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F0C")]
    	[Address(RVA = "0x560C10", Offset = "0x55F610", VA = "0x180560C10", Slot = "30")]
    	public virtual CustomVariableValue GetVariableValue(string name)
    	{
    		return default(CustomVariableValue);
    	}

    	[Token(Token = "0x6000F0D")]
    	[Address(RVA = "0x560C50", Offset = "0x55F650", VA = "0x180560C50", Slot = "31")]
    	public virtual void SetVariableValue(string name, CustomVariableValue value)
    	{
    	}

    	[Token(Token = "0x6000F0E")]
    	[Address(RVA = "0x561240", Offset = "0x55FC40", VA = "0x180561240", Slot = "32")]
    	public virtual void ResetVariable(string name)
    	{
    	}

    	[Token(Token = "0x6000F0F")]
    	[Address(RVA = "0x561440", Offset = "0x55FE40", VA = "0x180561440", Slot = "33")]
    	public virtual void ResetAllVariables([Optional] CustomVariableScope? scope)
    	{
    	}

    	[Token(Token = "0x6000F10")]
    	[Address(RVA = "0x5618D0", Offset = "0x5602D0", VA = "0x1805618D0", Slot = "34")]
    	protected virtual UniTask InitializeLocalVariables()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000F11")]
    	[Address(RVA = "0x561970", Offset = "0x560370", VA = "0x180561970", Slot = "35")]
    	protected virtual CustomVariable GetVariable(string name)
    	{
    		return default(CustomVariable);
    	}

    	[Token(Token = "0x6000F12")]
    	[Address(RVA = "0x561A60", Offset = "0x560460", VA = "0x180561A60", Slot = "36")]
    	protected virtual void ResetPredefinedVariable(string name, string predefine)
    	{
    	}
    }
}
