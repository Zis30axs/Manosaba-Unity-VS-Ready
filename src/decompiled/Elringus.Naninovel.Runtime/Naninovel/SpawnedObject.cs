using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200045D")]
public class SpawnedObject
{
	[StructLayout(3)]
	[Token(Token = "0x200045E")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__19
{
		[Token(Token = "0x4001042")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001043")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001044")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnedObject _003C_003E4__this;

		[Token(Token = "0x4001045")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001046")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001595")]
		[Address(RVA = "0x5D4550", Offset = "0x5D2F50", VA = "0x1805D4550", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001596")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200045F")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__17
{
		[Token(Token = "0x4001047")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001048")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001049")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnedObject _003C_003E4__this;

		[Token(Token = "0x400104A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400104B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001597")]
		[Address(RVA = "0x5D47F0", Offset = "0x5D31F0", VA = "0x1805D47F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001598")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400103D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly List<string> parameters;

	[Token(Token = "0x400103E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly Spawn.IParameterized spawnParameterized;

	[Token(Token = "0x400103F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly Spawn.IAwaitable spawnAwaitable;

	[Token(Token = "0x4001040")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly DestroySpawned.IParameterized destroyParameterized;

	[Token(Token = "0x4001041")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly DestroySpawned.IAwaitable destroyAwaitable;

	[Token(Token = "0x170002FD")]
	public string Path
	{
		[Token(Token = "0x600158C")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002FE")]
	public GameObject GameObject
	{
		[Token(Token = "0x600158D")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002FF")]
	public Transform Transform
	{
		[Token(Token = "0x600158E")]
		[Address(RVA = "0x5D3EB0", Offset = "0x5D28B0", VA = "0x1805D3EB0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000300")]
	public IReadOnlyList<string> Parameters
	{
		[Token(Token = "0x600158F")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001590")]
	[Address(RVA = "0x5D3ED0", Offset = "0x5D28D0", VA = "0x1805D3ED0")]
	public SpawnedObject(string path, GameObject gameObject)
	{
	}

	[Token(Token = "0x6001591")]
	[Address(RVA = "0x5D40F0", Offset = "0x5D2AF0", VA = "0x1805D40F0")]
	public void SetSpawnParameters(IReadOnlyList<string> value, bool asap)
	{
	}

	[Token(Token = "0x6001592")]
	[Address(RVA = "0x5D41F0", Offset = "0x5D2BF0", VA = "0x1805D41F0")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__17))]
	public UniTask AwaitSpawn([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001593")]
	[Address(RVA = "0x5D4370", Offset = "0x5D2D70", VA = "0x1805D4370")]
	public void SetDestroyParameters(IReadOnlyList<string> value)
	{
	}

	[Token(Token = "0x6001594")]
	[Address(RVA = "0x5D43D0", Offset = "0x5D2DD0", VA = "0x1805D43D0")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__19))]
	public UniTask AwaitDestroy([Optional] AsyncToken token)
	{
		return default(UniTask);
	}
}
