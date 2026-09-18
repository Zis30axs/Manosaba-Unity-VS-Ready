using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn
{
    [Token(Token = "0x2000004")]
    [UsedImplicitly]
    [CommandAlias("despawn")]
    public class DestroySpawnedExtended : DestroySpawned
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000005")]
    	[CompilerGenerated]
    	private struct _003CDestroy_003Ed__4
    {
    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DestroySpawnedExtended _003C_003E4__this;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IMultipliableSpawnManager _003CmultipliableSpawnManager_003E5__2;

    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xD2ABD0", Offset = "0xD295D0", VA = "0x180D2ABD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly]
    	public StringParameter SubId;

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[UsedImplicitly]
    	[ParameterAlias("warn")]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter LogWarning;

    	[Token(Token = "0x17000001")]
    	private string AssignedSubId
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x86C880", Offset = "0x86B280", VA = "0x18086C880")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD2A950", Offset = "0xD29350", VA = "0x180D2A950", Slot = "17")]
    	[AsyncStateMachine(typeof(_003CDestroy_003Ed__4))]
    	protected override UniTask Destroy(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD2AAD0", Offset = "0xD294D0", VA = "0x180D2AAD0", Slot = "7")]
    	public override void Warn(string message)
    	{
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x4E9A40", Offset = "0x4E8440", VA = "0x1804E9A40")]
    	public DestroySpawnedExtended()
    	{
    	}
    }
}
