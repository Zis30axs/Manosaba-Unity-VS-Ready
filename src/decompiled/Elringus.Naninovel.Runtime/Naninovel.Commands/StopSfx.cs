using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x200069C")]
public class StopSfx : AudioCommand
{
	[StructLayout(3)]
	[Token(Token = "0x200069D")]
	[CompilerGenerated]
	private struct _003CStop_003Ed__4
{
		[Token(Token = "0x4001951")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001952")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001953")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StopSfx _003C_003E4__this;

		[Token(Token = "0x4001954")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001955")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002030")]
		[Address(RVA = "0x4E3D70", Offset = "0x4E2770", VA = "0x1804E3D70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002031")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400194E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ResourceContext("Audio", -1, null)]
	public StringParameter SfxPath;

	[Token(Token = "0x400194F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("fade")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter FadeOutDuration;

	[Token(Token = "0x4001950")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	public BooleanParameter Wait;

	[Token(Token = "0x600202D")]
	[Address(RVA = "0x4E3B40", Offset = "0x4E2540", VA = "0x1804E3B40", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600202E")]
	[Address(RVA = "0x4E3BF0", Offset = "0x4E25F0", VA = "0x1804E3BF0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CStop_003Ed__4))]
	protected virtual UniTask Stop(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600202F")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public StopSfx()
	{
	}
}
