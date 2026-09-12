using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x200069A")]
public class StopBgm : AudioCommand
{
	[StructLayout(3)]
	[Token(Token = "0x200069B")]
	[CompilerGenerated]
	private struct _003CStop_003Ed__4
{
		[Token(Token = "0x4001949")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400194A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400194B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StopBgm _003C_003E4__this;

		[Token(Token = "0x400194C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400194D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600202B")]
		[Address(RVA = "0x4E35F0", Offset = "0x4E1FF0", VA = "0x1804E35F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600202C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001946")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ResourceContext("Audio", -1, null)]
	public StringParameter BgmPath;

	[Token(Token = "0x4001947")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("fade")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter FadeOutDuration;

	[Token(Token = "0x4001948")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	public BooleanParameter Wait;

	[Token(Token = "0x6002028")]
	[Address(RVA = "0x4E33C0", Offset = "0x4E1DC0", VA = "0x1804E33C0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002029")]
	[Address(RVA = "0x4E3470", Offset = "0x4E1E70", VA = "0x1804E3470", Slot = "16")]
	[AsyncStateMachine(typeof(_003CStop_003Ed__4))]
	protected virtual UniTask Stop(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600202A")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public StopBgm()
	{
	}
}
