using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn;

[Token(Token = "0x2000006")]
[UsedImplicitly]
[CommandAlias("spawn")]
public class SpawnExtended : Spawn
{
	[StructLayout(3)]
	[Token(Token = "0x2000007")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__6
{
		[Token(Token = "0x4000010")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000011")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000012")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnExtended _003C_003E4__this;

		[Token(Token = "0x4000013")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000014")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000015")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600000F")]
		[Address(RVA = "0xD2BBD0", Offset = "0xD2A5D0", VA = "0x180D2BBD0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000010")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400000E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[UsedImplicitly]
	public StringParameter SubId;

	[Token(Token = "0x400000F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[UsedImplicitly]
	[ParameterAlias("match")]
	public BooleanParameter MatchScreenWidth;

	[Token(Token = "0x17000002")]
	private string AssignedSubId
	{
		[Token(Token = "0x600000A")]
		[Address(RVA = "0xD2B440", Offset = "0xD29E40", VA = "0x180D2B440")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000003")]
	private IMultipliableSpawnManager MultipliableManager
	{
		[Token(Token = "0x600000B")]
		[Address(RVA = "0xD2B4D0", Offset = "0xD29ED0", VA = "0x180D2B4D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0xD2B530", Offset = "0xD29F30", VA = "0x180D2B530", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__6))]
	public override UniTask Execute([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600000D")]
	[Address(RVA = "0xD2B6C0", Offset = "0xD2A0C0", VA = "0x180D2B6C0", Slot = "22")]
	protected override void ApplyScenePosition(SpawnedObject spawnedObject)
	{
	}

	[Token(Token = "0x600000E")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SpawnExtended()
	{
	}
}
