using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn
{
    [Token(Token = "0x2000011")]
    public static class MultipliableSpawnManagerExtensions
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000012")]
    	[CompilerGenerated]
    	private struct _003CGetOrSpawnAsync_003Ed__0
    {
    		[Token(Token = "0x4000042")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000043")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

    		[Token(Token = "0x4000044")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public IMultipliableSpawnManager manager;

    		[Token(Token = "0x4000045")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string path;

    		[Token(Token = "0x4000046")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x4000047")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000048")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000039")]
    		[Address(RVA = "0xD30830", Offset = "0xD2F230", VA = "0x180D30830", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600003A")]
    		[Address(RVA = "0xD30B70", Offset = "0xD2F570", VA = "0x180D30B70", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x6000038")]
    	[Address(RVA = "0xD30580", Offset = "0xD2EF80", VA = "0x180D30580")]
    	[AsyncStateMachine(typeof(_003CGetOrSpawnAsync_003Ed__0))]
    	public static UniTask<SpawnedObject> GetOrSpawnAsync(this IMultipliableSpawnManager manager, string path, string subId, [Optional] AsyncToken asyncToken)
    	{
    		return default(UniTask<SpawnedObject>);
    	}
    }
}
