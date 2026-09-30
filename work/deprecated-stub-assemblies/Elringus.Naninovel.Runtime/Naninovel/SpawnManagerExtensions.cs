using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x200046C")]
    public static class SpawnManagerExtensions
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200046D")]
    	[CompilerGenerated]
    	private struct _003CGetOrSpawn_003Ed__0
    {
    		[Token(Token = "0x400107B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400107C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

    		[Token(Token = "0x400107D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ISpawnManager manager;

    		[Token(Token = "0x400107E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string path;

    		[Token(Token = "0x400107F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001080")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60015CD")]
    		[Address(RVA = "0x5D9270", Offset = "0x5D7C70", VA = "0x1805D9270", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60015CE")]
    		[Address(RVA = "0x5D9590", Offset = "0x5D7F90", VA = "0x1805D9590", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200046E")]
    	[CompilerGenerated]
    	private struct _003CSpawnWithParameters_003Ed__1
    {
    		[Token(Token = "0x4001081")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001082")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

    		[Token(Token = "0x4001083")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ISpawnManager manager;

    		[Token(Token = "0x4001084")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string path;

    		[Token(Token = "0x4001085")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001086")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public IReadOnlyList<string> parameters;

    		[Token(Token = "0x4001087")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60015CF")]
    		[Address(RVA = "0x5D95C0", Offset = "0x5D7FC0", VA = "0x1805D95C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60015D0")]
    		[Address(RVA = "0x5D98F0", Offset = "0x5D82F0", VA = "0x1805D98F0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200046F")]
    	[CompilerGenerated]
    	private struct _003CSpawnWithParametersAndWait_003Ed__2
    {
    		[Token(Token = "0x4001088")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001089")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

    		[Token(Token = "0x400108A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ISpawnManager manager;

    		[Token(Token = "0x400108B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string path;

    		[Token(Token = "0x400108C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public IReadOnlyList<string> parameters;

    		[Token(Token = "0x400108D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public AsyncToken token;

    		[Token(Token = "0x400108E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private SpawnedObject _003CspawnedObject_003E5__2;

    		[Token(Token = "0x400108F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001090")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60015D1")]
    		[Address(RVA = "0x5D9920", Offset = "0x5D8320", VA = "0x1805D9920", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60015D2")]
    		[Address(RVA = "0x5D9E80", Offset = "0x5D8880", VA = "0x1805D9E80", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x60015CA")]
    	[Address(RVA = "0x5D8AB0", Offset = "0x5D74B0", VA = "0x1805D8AB0")]
    	[AsyncStateMachine(typeof(_003CGetOrSpawn_003Ed__0))]
    	public static UniTask<SpawnedObject> GetOrSpawn(this ISpawnManager manager, string path, [Optional] AsyncToken token)
    	{
    		return default(UniTask<SpawnedObject>);
    	}

    	[Token(Token = "0x60015CB")]
    	[Address(RVA = "0x5D8D10", Offset = "0x5D7710", VA = "0x1805D8D10")]
    	[AsyncStateMachine(typeof(_003CSpawnWithParameters_003Ed__1))]
    	public static UniTask<SpawnedObject> SpawnWithParameters(this ISpawnManager manager, string path, IReadOnlyList<string> parameters, [Optional] AsyncToken token)
    	{
    		return default(UniTask<SpawnedObject>);
    	}

    	[Token(Token = "0x60015CC")]
    	[Address(RVA = "0x5D8FC0", Offset = "0x5D79C0", VA = "0x1805D8FC0")]
    	[AsyncStateMachine(typeof(_003CSpawnWithParametersAndWait_003Ed__2))]
    	public static UniTask<SpawnedObject> SpawnWithParametersAndWait(this ISpawnManager manager, string path, IReadOnlyList<string> parameters, [Optional] AsyncToken token)
    	{
    		return default(UniTask<SpawnedObject>);
    	}
    }
}
