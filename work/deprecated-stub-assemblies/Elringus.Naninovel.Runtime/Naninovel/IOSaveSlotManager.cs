using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x2000299")]
    public abstract class IOSaveSlotManager : ISaveSlotManager
    {
    	[Token(Token = "0x1700019D")]
    	public bool Loading
    	{
    		[Token(Token = "0x6000BC8")]
    		[Address(RVA = "0x456370", Offset = "0x454D70", VA = "0x180456370", Slot = "20")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000BC9")]
    		[Address(RVA = "0x539F80", Offset = "0x538980", VA = "0x180539F80")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700019E")]
    	public bool Saving
    	{
    		[Token(Token = "0x6000BCA")]
    		[Address(RVA = "0x539F90", Offset = "0x538990", VA = "0x180539F90", Slot = "21")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000BCB")]
    		[Address(RVA = "0x539FA0", Offset = "0x5389A0", VA = "0x180539FA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700019F")]
    	protected abstract bool PrettifyJson
    	{
    		[Token(Token = "0x6000BD0")]
    		get;
    	}

    	[Token(Token = "0x170001A0")]
    	protected abstract bool Binary
    	{
    		[Token(Token = "0x6000BD1")]
    		get;
    	}

    	[Token(Token = "0x170001A1")]
    	protected abstract string Extension
    	{
    		[Token(Token = "0x6000BD2")]
    		get;
    	}

    	[Token(Token = "0x14000020")]
    	public event Action<string> OnBeforeSave
    	{
    		[Token(Token = "0x6000BB8")]
    		[Address(RVA = "0x538E80", Offset = "0x537880", VA = "0x180538E80", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BB9")]
    		[Address(RVA = "0x538F90", Offset = "0x537990", VA = "0x180538F90", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000021")]
    	public event Action<string> OnSaved
    	{
    		[Token(Token = "0x6000BBA")]
    		[Address(RVA = "0x5390A0", Offset = "0x537AA0", VA = "0x1805390A0", Slot = "6")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BBB")]
    		[Address(RVA = "0x5391B0", Offset = "0x537BB0", VA = "0x1805391B0", Slot = "7")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000022")]
    	public event Action<string> OnBeforeLoad
    	{
    		[Token(Token = "0x6000BBC")]
    		[Address(RVA = "0x5392C0", Offset = "0x537CC0", VA = "0x1805392C0", Slot = "8")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BBD")]
    		[Address(RVA = "0x5393D0", Offset = "0x537DD0", VA = "0x1805393D0", Slot = "9")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000023")]
    	public event Action<string> OnLoaded
    	{
    		[Token(Token = "0x6000BBE")]
    		[Address(RVA = "0x5394E0", Offset = "0x537EE0", VA = "0x1805394E0", Slot = "10")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BBF")]
    		[Address(RVA = "0x5395F0", Offset = "0x537FF0", VA = "0x1805395F0", Slot = "11")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000024")]
    	public event Action<string> OnBeforeDelete
    	{
    		[Token(Token = "0x6000BC0")]
    		[Address(RVA = "0x539700", Offset = "0x538100", VA = "0x180539700", Slot = "12")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BC1")]
    		[Address(RVA = "0x539810", Offset = "0x538210", VA = "0x180539810", Slot = "13")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000025")]
    	public event Action<string> OnDeleted
    	{
    		[Token(Token = "0x6000BC2")]
    		[Address(RVA = "0x539920", Offset = "0x538320", VA = "0x180539920", Slot = "14")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BC3")]
    		[Address(RVA = "0x539A30", Offset = "0x538430", VA = "0x180539A30", Slot = "15")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000026")]
    	public event Action<string, string> OnBeforeRename
    	{
    		[Token(Token = "0x6000BC4")]
    		[Address(RVA = "0x539B40", Offset = "0x538540", VA = "0x180539B40", Slot = "16")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BC5")]
    		[Address(RVA = "0x539C50", Offset = "0x538650", VA = "0x180539C50", Slot = "17")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000027")]
    	public event Action<string, string> OnRenamed
    	{
    		[Token(Token = "0x6000BC6")]
    		[Address(RVA = "0x539D60", Offset = "0x538760", VA = "0x180539D60", Slot = "18")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000BC7")]
    		[Address(RVA = "0x539E70", Offset = "0x538870", VA = "0x180539E70", Slot = "19")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000BCC")]
    	public abstract bool SaveSlotExists(string slotId);

    	[Token(Token = "0x6000BCD")]
    	public abstract bool AnySaveExists();

    	[Token(Token = "0x6000BCE")]
    	public abstract void DeleteSaveSlot(string slotId);

    	[Token(Token = "0x6000BCF")]
    	public abstract void RenameSaveSlot(string sourceSlotId, string destSlotId);

    	[Token(Token = "0x6000BD3")]
    	[Address(RVA = "0x539FB0", Offset = "0x5389B0", VA = "0x180539FB0")]
    	protected void InvokeOnBeforeSave(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BD4")]
    	[Address(RVA = "0x539FD0", Offset = "0x5389D0", VA = "0x180539FD0")]
    	protected void InvokeOnSaved(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BD5")]
    	[Address(RVA = "0x539FF0", Offset = "0x5389F0", VA = "0x180539FF0")]
    	protected void InvokeOnBeforeLoad(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BD6")]
    	[Address(RVA = "0x53A010", Offset = "0x538A10", VA = "0x18053A010")]
    	protected void InvokeOnLoaded(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BD7")]
    	[Address(RVA = "0x53A030", Offset = "0x538A30", VA = "0x18053A030")]
    	protected void InvokeOnBeforeDelete(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BD8")]
    	[Address(RVA = "0x53A050", Offset = "0x538A50", VA = "0x18053A050")]
    	protected void InvokeOnDeleted(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BD9")]
    	[Address(RVA = "0x53A070", Offset = "0x538A70", VA = "0x18053A070")]
    	protected void InvokeOnBeforeRename(string sourceSlotId, string destSlotId)
    	{
    	}

    	[Token(Token = "0x6000BDA")]
    	[Address(RVA = "0x53A090", Offset = "0x538A90", VA = "0x18053A090")]
    	protected void InvokeOnRenamed(string sourceSlotId, string destSlotId)
    	{
    	}

    	[Token(Token = "0x6000BDB")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	protected IOSaveSlotManager()
    	{
    	}
    }
    [Token(Token = "0x200029A")]
    public class IOSaveSlotManager<TData> : IOSaveSlotManager, ISaveSlotManager<TData>, ISaveSlotManager where TData : class, new()
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200029B")]
    	[CompilerGenerated]
    	private struct _003CDeserializeData_003Ed__20
    {
    		[Token(Token = "0x4000A1F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A20")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TData> _003C_003Et__builder;

    		[Token(Token = "0x4000A21")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public IOSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A22")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A23")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<byte[]>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000A24")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<string>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000BED")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000BEE")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200029C")]
    	[CompilerGenerated]
    	private struct _003CLoad_003Ed__12
    {
    		[Token(Token = "0x4000A25")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A26")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TData> _003C_003Et__builder;

    		[Token(Token = "0x4000A27")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public IOSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A28")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A29")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<TData>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000BEF")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000BF0")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200029D")]
    	[CompilerGenerated]
    	private struct _003CLoadOrDefault_003Ed__13
    {
    		[Token(Token = "0x4000A2A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A2B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TData> _003C_003Et__builder;

    		[Token(Token = "0x4000A2C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public IOSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A2D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A2E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000A2F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<TData>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000BF1")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000BF2")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200029E")]
    	[CompilerGenerated]
    	private struct _003CSave_003Ed__11
    {
    		[Token(Token = "0x4000A30")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A31")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000A32")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public IOSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A33")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A34")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public TData data;

    		[Token(Token = "0x4000A35")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000A36")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000BF3")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000BF4")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200029F")]
    	[CompilerGenerated]
    	private struct _003CSerializeData_003Ed__19
    {
    		[Token(Token = "0x4000A37")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000A38")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000A39")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public TData data;

    		[Token(Token = "0x4000A3A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public IOSaveSlotManager<TData> _003C_003E4__this;

    		[Token(Token = "0x4000A3B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string slotId;

    		[Token(Token = "0x4000A3C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private string _003CfilePath_003E5__2;

    		[Token(Token = "0x4000A3D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<byte[]>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000A3E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000BF5")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000BF6")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000A1E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool saveInProgress;

    	[Token(Token = "0x170001A2")]
    	protected virtual string GameDataPath
    	{
    		[Token(Token = "0x6000BDC")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001A3")]
    	protected virtual string SaveDataPath
    	{
    		[Token(Token = "0x6000BDD")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001A4")]
    	protected override bool PrettifyJson
    	{
    		[Token(Token = "0x6000BDE")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170001A5")]
    	protected override bool Binary
    	{
    		[Token(Token = "0x6000BDF")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170001A6")]
    	protected override string Extension
    	{
    		[Token(Token = "0x6000BE0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000BE1")]
    	[AsyncStateMachine(typeof(IOSaveSlotManager<>._003CSave_003Ed__11))]
    	public UniTask Save(string slotId, TData data)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000BE2")]
    	[AsyncStateMachine(typeof(IOSaveSlotManager<>._003CLoad_003Ed__12))]
    	public UniTask<TData> Load(string slotId)
    	{
    		return default(UniTask<TData>);
    	}

    	[Token(Token = "0x6000BE3")]
    	[AsyncStateMachine(typeof(IOSaveSlotManager<>._003CLoadOrDefault_003Ed__13))]
    	public UniTask<TData> LoadOrDefault(string slotId)
    	{
    		return default(UniTask<TData>);
    	}

    	[Token(Token = "0x6000BE4")]
    	public override bool SaveSlotExists(string slotId)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000BE5")]
    	public override bool AnySaveExists()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000BE6")]
    	public override void DeleteSaveSlot(string slotId)
    	{
    	}

    	[Token(Token = "0x6000BE7")]
    	public override void RenameSaveSlot(string sourceSlotId, string destSlotId)
    	{
    	}

    	[Token(Token = "0x6000BE8")]
    	protected virtual string SlotIdToFilePath(string slotId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000BE9")]
    	[AsyncStateMachine(typeof(IOSaveSlotManager<>._003CSerializeData_003Ed__19))]
    	protected virtual UniTask SerializeData(string slotId, TData data)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000BEA")]
    	[AsyncStateMachine(typeof(IOSaveSlotManager<>._003CDeserializeData_003Ed__20))]
    	protected virtual UniTask<TData> DeserializeData(string slotId)
    	{
    		return default(UniTask<TData>);
    	}

    	[Token(Token = "0x6000BEB")]
    	protected virtual string GetGameDataPath()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000BEC")]
    	public IOSaveSlotManager()
    	{
    	}
    }
}
