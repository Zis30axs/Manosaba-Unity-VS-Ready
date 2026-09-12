using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.ExtendedSpawn;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Effects;

[Token(Token = "0x2000004")]
[UsedImplicitly]
public class HideMattes : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000005")]
	[CompilerGenerated]
	private struct _003CDestroyMattesAsync_003Ed__4
{
		[Token(Token = "0x4000009")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400000A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400000B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnedObject mattes;

		[Token(Token = "0x400000C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public HideMattes _003C_003E4__this;

		[Token(Token = "0x400000D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken asyncToken;

		[Token(Token = "0x400000E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000007")]
		[Address(RVA = "0xD04180", Offset = "0xD02B80", VA = "0x180D04180", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000008")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000006")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__3
{
		[Token(Token = "0x400000F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000010")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000011")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public HideMattes _003C_003E4__this;

		[Token(Token = "0x4000012")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000013")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ISpawnManager _003CspawnManager_003E5__2;

		[Token(Token = "0x4000014")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private IMultipliableSpawnManager _003CmultipliableSpawnManager_003E5__3;

		[Token(Token = "0x4000015")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000009")]
		[Address(RVA = "0xD04590", Offset = "0xD02F90", VA = "0x180D04590", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600000A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000006")]
	private const string MattesName = "Mattes";

	[Token(Token = "0x4000007")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("time")]
	public StringParameter Duration;

	[Token(Token = "0x4000008")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	public StringParameter SubId;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0xD03CD0", Offset = "0xD026D0", VA = "0x180D03CD0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__3))]
	public override UniTask Execute([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD03E60", Offset = "0xD02860", VA = "0x180D03E60")]
	[AsyncStateMachine(typeof(_003CDestroyMattesAsync_003Ed__4))]
	private UniTask DestroyMattesAsync(SpawnedObject mattes, [Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0xD04050", Offset = "0xD02A50", VA = "0x180D04050")]
	private static string BuildWarningMessage(string mattesName, [Optional] string subId)
	{
		return null;
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public HideMattes()
	{
	}
}
