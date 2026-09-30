using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x20002A2")]
    public abstract class PlayerPrefsSaveSlotManager : ISaveSlotManager
    {
    	[Token(Token = "0x170001A9")]
    	public bool Loading
    	{
    		[Token(Token = "0x6000C20")]
    		[Address(RVA = "0x456370", Offset = "0x454D70", VA = "0x180456370", Slot = "20")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000C21")]
    		[Address(RVA = "0x539F80", Offset = "0x538980", VA = "0x180539F80")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170001AA")]
    	public bool Saving
    	{
    		[Token(Token = "0x6000C22")]
    		[Address(RVA = "0x539F90", Offset = "0x538990", VA = "0x180539F90", Slot = "21")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000C23")]
    		[Address(RVA = "0x539FA0", Offset = "0x5389A0", VA = "0x180539FA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170001AB")]
    	protected abstract bool PrettifyJson
    	{
    		[Token(Token = "0x6000C28")]
    		get;
    	}

    	[Token(Token = "0x170001AC")]
    	protected abstract bool Binary
    	{
    		[Token(Token = "0x6000C29")]
    		get;
    	}

    	[Token(Token = "0x14000030")]
    	public event Action<string> OnBeforeSave
    	{
    		[Token(Token = "0x6000C10")]
    		[Address(RVA = "0x53A0B0", Offset = "0x538AB0", VA = "0x18053A0B0", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C11")]
    		[Address(RVA = "0x53A1C0", Offset = "0x538BC0", VA = "0x18053A1C0", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000031")]
    	public event Action<string> OnSaved
    	{
    		[Token(Token = "0x6000C12")]
    		[Address(RVA = "0x53A2D0", Offset = "0x538CD0", VA = "0x18053A2D0", Slot = "6")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C13")]
    		[Address(RVA = "0x53A3E0", Offset = "0x538DE0", VA = "0x18053A3E0", Slot = "7")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000032")]
    	public event Action<string> OnBeforeLoad
    	{
    		[Token(Token = "0x6000C14")]
    		[Address(RVA = "0x53A4F0", Offset = "0x538EF0", VA = "0x18053A4F0", Slot = "8")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C15")]
    		[Address(RVA = "0x53A600", Offset = "0x539000", VA = "0x18053A600", Slot = "9")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000033")]
    	public event Action<string> OnLoaded
    	{
    		[Token(Token = "0x6000C16")]
    		[Address(RVA = "0x53A710", Offset = "0x539110", VA = "0x18053A710", Slot = "10")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C17")]
    		[Address(RVA = "0x53A820", Offset = "0x539220", VA = "0x18053A820", Slot = "11")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000034")]
    	public event Action<string> OnBeforeDelete
    	{
    		[Token(Token = "0x6000C18")]
    		[Address(RVA = "0x53A930", Offset = "0x539330", VA = "0x18053A930", Slot = "12")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C19")]
    		[Address(RVA = "0x53AA40", Offset = "0x539440", VA = "0x18053AA40", Slot = "13")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000035")]
    	public event Action<string> OnDeleted
    	{
    		[Token(Token = "0x6000C1A")]
    		[Address(RVA = "0x53AB50", Offset = "0x539550", VA = "0x18053AB50", Slot = "14")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C1B")]
    		[Address(RVA = "0x53AC60", Offset = "0x539660", VA = "0x18053AC60", Slot = "15")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000036")]
    	public event Action<string, string> OnBeforeRename
    	{
    		[Token(Token = "0x6000C1C")]
    		[Address(RVA = "0x53AD70", Offset = "0x539770", VA = "0x18053AD70", Slot = "16")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C1D")]
    		[Address(RVA = "0x53AE80", Offset = "0x539880", VA = "0x18053AE80", Slot = "17")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000037")]
    	public event Action<string, string> OnRenamed
    	{
    		[Token(Token = "0x6000C1E")]
    		[Address(RVA = "0x53AF90", Offset = "0x539990", VA = "0x18053AF90", Slot = "18")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C1F")]
    		[Address(RVA = "0x53B0A0", Offset = "0x539AA0", VA = "0x18053B0A0", Slot = "19")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000C24")]
    	public abstract bool SaveSlotExists(string slotId);

    	[Token(Token = "0x6000C25")]
    	public abstract bool AnySaveExists();

    	[Token(Token = "0x6000C26")]
    	public abstract void DeleteSaveSlot(string slotId);

    	[Token(Token = "0x6000C27")]
    	public abstract void RenameSaveSlot(string sourceSlotId, string destSlotId);

    	[Token(Token = "0x6000C2A")]
    	[Address(RVA = "0x539FB0", Offset = "0x5389B0", VA = "0x180539FB0")]
    	protected void InvokeOnBeforeSave(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C2B")]
    	[Address(RVA = "0x539FD0", Offset = "0x5389D0", VA = "0x180539FD0")]
    	protected void InvokeOnSaved(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C2C")]
    	[Address(RVA = "0x539FF0", Offset = "0x5389F0", VA = "0x180539FF0")]
    	protected void InvokeOnBeforeLoad(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C2D")]
    	[Address(RVA = "0x53A010", Offset = "0x538A10", VA = "0x18053A010")]
    	protected void InvokeOnLoaded(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C2E")]
    	[Address(RVA = "0x53A030", Offset = "0x538A30", VA = "0x18053A030")]
    	protected void InvokeOnBeforeDelete(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C2F")]
    	[Address(RVA = "0x53A050", Offset = "0x538A50", VA = "0x18053A050")]
    	protected void InvokeOnDeleted(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C30")]
    	[Address(RVA = "0x53A070", Offset = "0x538A70", VA = "0x18053A070")]
    	protected void InvokeOnBeforeRename(string sourceSlotId, string destSlotId)
    	{
    	}

    	[Token(Token = "0x6000C31")]
    	[Address(RVA = "0x53A090", Offset = "0x538A90", VA = "0x18053A090")]
    	protected void InvokeOnRenamed(string sourceSlotId, string destSlotId)
    	{
    	}

    	[Token(Token = "0x6000C32")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	protected PlayerPrefsSaveSlotManager()
    	{
    	}
    }
    [Token(Token = "0x20002A3")]
    public class PlayerPrefsSaveSlotManager<TData> : PlayerPrefsSaveSlotManager, ISaveSlotManager<TData>, ISaveSlotManager where TData : class, new()
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20002A5")]
    	[CompilerGenerated]
    	private struct _003CDeserializeData_003Ed__22
    {
    		[Token(Token = "0x4000A4B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A4C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TData> _003C_003Et__builder;

    		[Token(Token = "0x4000A4D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public PlayerPrefsSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A4E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A4F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000C4A")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000C4B")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002A6")]
    	[CompilerGenerated]
    	private struct _003CLoad_003Ed__13
    {
    		[Token(Token = "0x4000A50")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A51")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TData> _003C_003Et__builder;

    		[Token(Token = "0x4000A52")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public PlayerPrefsSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A53")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A54")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<TData>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000C4C")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000C4D")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002A7")]
    	[CompilerGenerated]
    	private struct _003CLoadOrDefault_003Ed__14
    {
    		[Token(Token = "0x4000A55")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A56")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TData> _003C_003Et__builder;

    		[Token(Token = "0x4000A57")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public PlayerPrefsSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A58")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A59")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000A5A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<TData>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000C4E")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000C4F")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002A8")]
    	[CompilerGenerated]
    	private struct _003CSave_003Ed__11
    {
    		[Token(Token = "0x4000A5B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A5C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000A5D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public PlayerPrefsSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A5E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A5F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public TData data;

    		[Token(Token = "0x4000A60")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000A61")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000C50")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000C51")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002A9")]
    	[CompilerGenerated]
    	private struct _003CSerializeData_003Ed__20
    {
    		[Token(Token = "0x4000A62")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A63")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000A64")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public TData data;

    		[Token(Token = "0x4000A65")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public PlayerPrefsSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A66")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A67")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private string _003CslotKey_003E5__2;

    		[Token(Token = "0x4000A68")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<byte[]>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000C52")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000C53")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000A49")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool saveInProgress;

    	[Token(Token = "0x170001AD")]
    	protected virtual string KeyPrefix
    	{
    		[Token(Token = "0x6000C33")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001AE")]
    	protected virtual string IndexKey
    	{
    		[Token(Token = "0x6000C34")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001AF")]
    	protected virtual string IndexDelimiter
    	{
    		[Token(Token = "0x6000C35")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001B0")]
    	protected override bool PrettifyJson
    	{
    		[Token(Token = "0x6000C36")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170001B1")]
    	protected override bool Binary
    	{
    		[Token(Token = "0x6000C37")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6000C38")]
    	[AsyncStateMachine(typeof(PlayerPrefsSaveSlotManager<>._003CSave_003Ed__11))]
    	public virtual UniTask Save(string slotId, TData data)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000C39")]
    	public virtual void SaveBlocking(string slotId, TData data)
    	{
    	}

    	[Token(Token = "0x6000C3A")]
    	[AsyncStateMachine(typeof(PlayerPrefsSaveSlotManager<>._003CLoad_003Ed__13))]
    	public virtual UniTask<TData> Load(string slotId)
    	{
    		return default(UniTask<TData>);
    	}

    	[Token(Token = "0x6000C3B")]
    	[AsyncStateMachine(typeof(PlayerPrefsSaveSlotManager<>._003CLoadOrDefault_003Ed__14))]
    	public virtual UniTask<TData> LoadOrDefault(string slotId)
    	{
    		return default(UniTask<TData>);
    	}

    	[Token(Token = "0x6000C3C")]
    	public override bool SaveSlotExists(string slotId)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000C3D")]
    	public override bool AnySaveExists()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000C3E")]
    	public override void DeleteSaveSlot(string slotId)
    	{
    	}

    	[Token(Token = "0x6000C3F")]
    	public override void RenameSaveSlot(string sourceSlotId, string destSlotId)
    	{
    	}

    	[Token(Token = "0x6000C40")]
    	protected virtual string SlotIdToKey(string slotId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000C41")]
    	[AsyncStateMachine(typeof(PlayerPrefsSaveSlotManager<>._003CSerializeData_003Ed__20))]
    	protected virtual UniTask SerializeData(string slotId, TData data)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000C42")]
    	protected virtual void SerializeDataBlocking(string slotId, TData data)
    	{
    	}

    	[Token(Token = "0x6000C43")]
    	[AsyncStateMachine(typeof(PlayerPrefsSaveSlotManager<>._003CDeserializeData_003Ed__22))]
    	protected virtual UniTask<TData> DeserializeData(string slotId)
    	{
    		return default(UniTask<TData>);
    	}

    	[Token(Token = "0x6000C44")]
    	protected virtual void AddKeyIndexIfNotExist(string slotKey)
    	{
    	}

    	[Token(Token = "0x6000C45")]
    	protected virtual void RemoveKeyIndex(string slotKey)
    	{
    	}

    	[Token(Token = "0x6000C46")]
    	private List<string> ParseIndexList(string prefsValue)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000C47")]
    	public PlayerPrefsSaveSlotManager()
    	{
    	}
    }
}
