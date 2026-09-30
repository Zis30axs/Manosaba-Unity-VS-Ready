using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace Naninovel
{
    [Token(Token = "0x2000435")]
    [InitializeAtRuntime(0, null)]
    public class ScriptLoader : IStatefulService<GameStateMap>, IEngineService, IScriptLoader
    {
    	[Serializable]
    	[Token(Token = "0x2000436")]
    	public class GameState
    	{
    		[Token(Token = "0x4000F71")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public string[] LoadedScriptPaths;

    		[Token(Token = "0x60014C9")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000437")]
    	[CompilerGenerated]
    	private struct _003CLoad_003Ed__22
    {
    		[Token(Token = "0x4000F72")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000F73")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000F74")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptLoader _003C_003E4__this;

    		[Token(Token = "0x4000F75")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string scriptPath;

    		[Token(Token = "0x4000F76")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int startIndex;

    		[Token(Token = "0x4000F77")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private ScriptPlaylist _003Cplaylist_003E5__2;

    		[Token(Token = "0x4000F78")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private ScriptPlaylist[] _003CprevLists_003E5__3;

    		[Token(Token = "0x4000F79")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<ScriptPlaylist>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000F7A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60014CA")]
    		[Address(RVA = "0x5C1220", Offset = "0x5BFC20", VA = "0x1805C1220", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60014CB")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000438")]
    	[CompilerGenerated]
    	private struct _003CLoadAndHoldScript_003Ed__34
    {
    		[Token(Token = "0x4000F7B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000F7C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ScriptPlaylist> _003C_003Et__builder;

    		[Token(Token = "0x4000F7D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ScriptLoader _003C_003E4__this;

    		[Token(Token = "0x4000F7E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string scriptPath;

    		[Token(Token = "0x4000F7F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<Resource<Script>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60014CC")]
    		[Address(RVA = "0x5C1EA0", Offset = "0x5C08A0", VA = "0x1805C1EA0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60014CD")]
    		[Address(RVA = "0x5C21D0", Offset = "0x5C0BD0", VA = "0x1805C21D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000439")]
    	[CompilerGenerated]
    	private struct _003CLoadDependency_003Ed__27
    {
    		[Token(Token = "0x4000F80")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000F81")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000F82")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptLoader _003C_003E4__this;

    		[Token(Token = "0x4000F83")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string scriptPath;

    		[Token(Token = "0x4000F84")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private ScriptPlaylist _003Cplaylist_003E5__2;

    		[Token(Token = "0x4000F85")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ScriptPlaylist>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000F86")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60014CE")]
    		[Address(RVA = "0x5C2200", Offset = "0x5C0C00", VA = "0x1805C2200", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60014CF")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200043A")]
    	[CompilerGenerated]
    	private struct _003CLoadSaved_003Ed__25
    {
    		[Token(Token = "0x4000F87")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000F88")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000F89")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptLoader _003C_003E4__this;

    		[Token(Token = "0x4000F8A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string scriptPath;

    		[Token(Token = "0x4000F8B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public PlaybackSpot playedSpot;

    		[Token(Token = "0x4000F8C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<ScriptPlaylist>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000F8D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60014D0")]
    		[Address(RVA = "0x5C29A0", Offset = "0x5C13A0", VA = "0x1805C29A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60014D1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x170002E6")]
    	protected virtual IScriptManager ScriptManager
    	{
    		[Token(Token = "0x60014B1")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "12")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002E7")]
    	protected virtual ResourcePolicy Policy
    	{
    		[Token(Token = "0x60014B2")]
    		[Address(RVA = "0x440360", Offset = "0x43ED60", VA = "0x180440360", Slot = "13")]
    		[CompilerGenerated]
    		get
    		{
    			return default(ResourcePolicy);
    		}
    	}

    	[Token(Token = "0x170002E8")]
    	protected virtual bool ShouldRemoveActors
    	{
    		[Token(Token = "0x60014B3")]
    		[Address(RVA = "0x5BEFE0", Offset = "0x5BD9E0", VA = "0x1805BEFE0", Slot = "14")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170002E9")]
    	protected virtual Dictionary<string, ScriptPlaylist> listByLoadedScriptPath
    	{
    		[Token(Token = "0x60014B4")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "15")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1400006C")]
    	public event Action<float> OnLoadProgress
    	{
    		[Token(Token = "0x60014AF")]
    		[Address(RVA = "0x5BEDC0", Offset = "0x5BD7C0", VA = "0x1805BEDC0", Slot = "9")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60014B0")]
    		[Address(RVA = "0x5BEED0", Offset = "0x5BD8D0", VA = "0x1805BEED0", Slot = "10")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60014B5")]
    	[Address(RVA = "0x5BEFF0", Offset = "0x5BD9F0", VA = "0x1805BEFF0")]
    	public ScriptLoader(ResourceProviderConfiguration config, IScriptManager scriptManager)
    	{
    	}

    	[Token(Token = "0x60014B6")]
    	[Address(RVA = "0x5BF140", Offset = "0x5BDB40", VA = "0x1805BF140", Slot = "16")]
    	public virtual UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014B7")]
    	[Address(RVA = "0x5BF190", Offset = "0x5BDB90", VA = "0x1805BF190", Slot = "17")]
    	public virtual void ResetService()
    	{
    	}

    	[Token(Token = "0x60014B8")]
    	[Address(RVA = "0x5BF190", Offset = "0x5BDB90", VA = "0x1805BF190", Slot = "18")]
    	public virtual void DestroyService()
    	{
    	}

    	[Token(Token = "0x60014B9")]
    	[Address(RVA = "0x5BF1B0", Offset = "0x5BDBB0", VA = "0x1805BF1B0", Slot = "19")]
    	public virtual void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60014BA")]
    	[Address(RVA = "0x5BF3D0", Offset = "0x5BDDD0", VA = "0x1805BF3D0", Slot = "20")]
    	public virtual UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014BB")]
    	[Address(RVA = "0x5BF8E0", Offset = "0x5BE2E0", VA = "0x1805BF8E0", Slot = "21")]
    	[AsyncStateMachine(typeof(_003CLoad_003Ed__22))]
    	public virtual UniTask Load(string scriptPath, int startIndex = 0)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014BC")]
    	[Address(RVA = "0x5BFA80", Offset = "0x5BE480", VA = "0x1805BFA80", Slot = "22")]
    	protected virtual bool IsLoaded(string scriptPath)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60014BD")]
    	[Address(RVA = "0x5BFB70", Offset = "0x5BE570", VA = "0x1805BFB70", Slot = "23")]
    	protected virtual UniTask LoadList(ScriptPlaylist list, int startIndex)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014BE")]
    	[Address(RVA = "0x5BFCA0", Offset = "0x5BE6A0", VA = "0x1805BFCA0", Slot = "24")]
    	[AsyncStateMachine(typeof(_003CLoadSaved_003Ed__25))]
    	protected virtual UniTask LoadSaved(string scriptPath, PlaybackSpot playedSpot)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014BF")]
    	[Address(RVA = "0x5BFE90", Offset = "0x5BE890", VA = "0x1805BFE90", Slot = "25")]
    	protected virtual UniTask LoadDependencies(ScriptPlaylist list)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014C0")]
    	[Address(RVA = "0x5C0210", Offset = "0x5BEC10", VA = "0x1805C0210", Slot = "26")]
    	[AsyncStateMachine(typeof(_003CLoadDependency_003Ed__27))]
    	protected virtual UniTask LoadDependency(string scriptPath)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60014C1")]
    	[Address(RVA = "0x5C03A0", Offset = "0x5BEDA0", VA = "0x1805C03A0", Slot = "27")]
    	protected virtual bool TryGetDependency(Command command, out string scriptPath)
    	{
    		scriptPath = default;
    		scriptPath = default;
    		return default(bool);
    	}

    	[Token(Token = "0x60014C2")]
    	[Address(RVA = "0x5C04F0", Offset = "0x5BEEF0", VA = "0x1805C04F0", Slot = "28")]
    	protected virtual bool TryGetScriptDependency(NamedStringParameter path, out string scriptPath)
    	{
    		scriptPath = default;
    		scriptPath = default;
    		return default(bool);
    	}

    	[Token(Token = "0x60014C3")]
    	[Address(RVA = "0x5C07B0", Offset = "0x5BF1B0", VA = "0x1805C07B0", Slot = "29")]
    	protected virtual bool IsDependency(Goto go)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60014C4")]
    	[Address(RVA = "0x5C0920", Offset = "0x5BF320", VA = "0x1805C0920", Slot = "30")]
    	protected virtual void UnloadScript(string scriptPath)
    	{
    	}

    	[Token(Token = "0x60014C5")]
    	[Address(RVA = "0x5C09F0", Offset = "0x5BF3F0", VA = "0x1805C09F0", Slot = "31")]
    	protected virtual void UnloadList(ScriptPlaylist playlist)
    	{
    	}

    	[Token(Token = "0x60014C6")]
    	[Address(RVA = "0x5C0AE0", Offset = "0x5BF4E0", VA = "0x1805C0AE0", Slot = "32")]
    	protected virtual void UnloadAll()
    	{
    	}

    	[Token(Token = "0x60014C7")]
    	[Address(RVA = "0x5C0D50", Offset = "0x5BF750", VA = "0x1805C0D50", Slot = "33")]
    	[AsyncStateMachine(typeof(_003CLoadAndHoldScript_003Ed__34))]
    	protected virtual UniTask<ScriptPlaylist> LoadAndHoldScript(string scriptPath)
    	{
    		return default(UniTask<ScriptPlaylist>);
    	}

    	[Token(Token = "0x60014C8")]
    	[Address(RVA = "0x5C0F40", Offset = "0x5BF940", VA = "0x1805C0F40", Slot = "34")]
    	protected virtual void RemoveUnusedActors()
    	{
    	}
    }
}
