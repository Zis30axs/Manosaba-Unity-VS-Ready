using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x200070A")]
[CommandAlias("despawn")]
public class DestroySpawned : Command
{
	[Token(Token = "0x200070B")]
	public interface IParameterized
	{
		[Token(Token = "0x6002189")]
		void SetDestroyParameters(IReadOnlyList<string> parameters);
	}

	[Token(Token = "0x200070C")]
	public interface IAwaitable
	{
		[Token(Token = "0x600218A")]
		UniTask AwaitDestroy([Optional] AsyncToken token);
	}

	[StructLayout(3)]
	[Token(Token = "0x200070D")]
	[CompilerGenerated]
	private struct _003CDestroy_003Ed__8
{
		[Token(Token = "0x4001AEC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001AED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001AEE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DestroySpawned _003C_003E4__this;

		[Token(Token = "0x4001AEF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001AF0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600218B")]
		[Address(RVA = "0x508AB0", Offset = "0x5074B0", VA = "0x180508AB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600218C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001AE9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	public StringParameter Path;

	[Token(Token = "0x4001AEA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	public StringListParameter Params;

	[Token(Token = "0x4001AEB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	public BooleanParameter Wait;

	[Token(Token = "0x170004E7")]
	protected virtual ISpawnManager SpawnManager
	{
		[Token(Token = "0x6002185")]
		[Address(RVA = "0x508820", Offset = "0x507220", VA = "0x180508820", Slot = "16")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6002186")]
	[Address(RVA = "0x508880", Offset = "0x507280", VA = "0x180508880", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002187")]
	[Address(RVA = "0x508930", Offset = "0x507330", VA = "0x180508930", Slot = "17")]
	[AsyncStateMachine(typeof(_003CDestroy_003Ed__8))]
	protected virtual UniTask Destroy(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002188")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public DestroySpawned()
	{
	}
}
