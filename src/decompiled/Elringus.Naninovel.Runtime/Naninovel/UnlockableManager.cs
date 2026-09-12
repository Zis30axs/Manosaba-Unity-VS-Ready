using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x20004DD")]
[InitializeAtRuntime(0, null)]
public class UnlockableManager : IUnlockableManager, IEngineService<UnlockablesConfiguration>, IEngineService, IStatefulService<GlobalStateMap>
{
	[Serializable]
	[Token(Token = "0x20004DE")]
	public class UnlockablesMap : SerializableMap<string, bool>
	{
		[Token(Token = "0x6001869")]
		[Address(RVA = "0x606D20", Offset = "0x605720", VA = "0x180606D20")]
		public UnlockablesMap()
		{
		}

		[Token(Token = "0x600186A")]
		[Address(RVA = "0x606DD0", Offset = "0x6057D0", VA = "0x180606DD0")]
		public UnlockablesMap(UnlockablesMap map)
		{
		}
	}

	[Serializable]
	[Token(Token = "0x20004DF")]
	public class GlobalState
	{
		[Token(Token = "0x4001296")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public UnlockablesMap UnlockablesMap;

		[Token(Token = "0x600186B")]
		[Address(RVA = "0x606E90", Offset = "0x605890", VA = "0x180606E90")]
		public GlobalState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004E1")]
	[CompilerGenerated]
	private struct _003CInitializeService_003Ed__14
{
		[Token(Token = "0x4001299")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400129A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400129B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UnlockableManager _003C_003E4__this;

		[Token(Token = "0x400129C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400129D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600186F")]
		[Address(RVA = "0x607020", Offset = "0x605A20", VA = "0x180607020", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001870")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001293")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly UnlockablesMap map;

	[Token(Token = "0x4001294")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly IResourceProviderManager resources;

	[Token(Token = "0x4001295")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly ITextManager docs;

	[Token(Token = "0x17000377")]
	public virtual UnlockablesConfiguration Configuration
	{
		[Token(Token = "0x6001859")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "19")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000378")]
	public virtual IReadOnlyCollection<string> ItemIds
	{
		[Token(Token = "0x600185A")]
		[Address(RVA = "0x6059D0", Offset = "0x6043D0", VA = "0x1806059D0", Slot = "20")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x14000089")]
	public event Action<UnlockableItemUpdatedArgs> OnItemUpdated
	{
		[Token(Token = "0x6001857")]
		[Address(RVA = "0x6057B0", Offset = "0x6041B0", VA = "0x1806057B0", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001858")]
		[Address(RVA = "0x6058C0", Offset = "0x6042C0", VA = "0x1806058C0", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x600185B")]
	[Address(RVA = "0x605A40", Offset = "0x604440", VA = "0x180605A40")]
	public UnlockableManager(UnlockablesConfiguration config, IResourceProviderManager resources, ITextManager docs)
	{
	}

	[Token(Token = "0x600185C")]
	[Address(RVA = "0x605C10", Offset = "0x604610", VA = "0x180605C10", Slot = "21")]
	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__14))]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600185D")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "22")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x600185E")]
	[Address(RVA = "0x605D40", Offset = "0x604740", VA = "0x180605D40", Slot = "23")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x600185F")]
	[Address(RVA = "0x605DC0", Offset = "0x6047C0", VA = "0x180605DC0", Slot = "24")]
	public virtual void SaveServiceState(GlobalStateMap stateMap)
	{
	}

	[Token(Token = "0x6001860")]
	[Address(RVA = "0x606080", Offset = "0x604A80", VA = "0x180606080", Slot = "25")]
	public virtual UniTask LoadServiceState(GlobalStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001861")]
	[Address(RVA = "0x6063A0", Offset = "0x604DA0", VA = "0x1806063A0", Slot = "26")]
	public virtual bool ItemUnlocked(string itemId)
	{
		return default(bool);
	}

	[Token(Token = "0x6001862")]
	[Address(RVA = "0x606470", Offset = "0x604E70", VA = "0x180606470", Slot = "27")]
	public virtual void SetItemUnlocked(string itemId, bool unlocked)
	{
	}

	[Token(Token = "0x6001863")]
	[Address(RVA = "0x606680", Offset = "0x605080", VA = "0x180606680", Slot = "28")]
	public virtual void UnlockItem(string itemId)
	{
	}

	[Token(Token = "0x6001864")]
	[Address(RVA = "0x6066A0", Offset = "0x6050A0", VA = "0x1806066A0", Slot = "29")]
	public virtual void LockItem(string itemId)
	{
	}

	[Token(Token = "0x6001865")]
	[Address(RVA = "0x6066C0", Offset = "0x6050C0", VA = "0x1806066C0", Slot = "30")]
	public virtual void UnlockAllItems()
	{
	}

	[Token(Token = "0x6001866")]
	[Address(RVA = "0x606840", Offset = "0x605240", VA = "0x180606840", Slot = "31")]
	public virtual void LockAllItems()
	{
	}

	[Token(Token = "0x6001867")]
	[Address(RVA = "0x6069C0", Offset = "0x6053C0", VA = "0x1806069C0", Slot = "32")]
	protected virtual UniTask<IReadOnlyCollection<string>> LocateUnlockableResources()
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x6001868")]
	[Address(RVA = "0x606A70", Offset = "0x605470", VA = "0x180606A70", Slot = "33")]
	protected virtual IReadOnlyCollection<string> GetAllTips()
	{
		return null;
	}
}
