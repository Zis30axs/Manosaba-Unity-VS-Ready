using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x2000697")]
[CommandAlias("voice")]
public class PlayVoice : AudioCommand, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x2000698")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__6
{
		[Token(Token = "0x400193E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400193F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001940")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlayVoice _003C_003E4__this;

		[Token(Token = "0x4001941")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002024")]
		[Address(RVA = "0x4E2BC0", Offset = "0x4E15C0", VA = "0x1804E2BC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002025")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000699")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__4
{
		[Token(Token = "0x4001942")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001943")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001944")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlayVoice _003C_003E4__this;

		[Token(Token = "0x4001945")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002026")]
		[Address(RVA = "0x4E30D0", Offset = "0x4E1AD0", VA = "0x1804E30D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002027")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400193A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[ResourceContext("Voice", -1, null)]
	public StringParameter VoicePath;

	[Token(Token = "0x400193B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterDefaultValue("1")]
	public DecimalParameter Volume;

	[Token(Token = "0x400193C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterAlias("group")]
	public StringParameter GroupPath;

	[Token(Token = "0x400193D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	public StringParameter AuthorId;

	[Token(Token = "0x6002020")]
	[Address(RVA = "0x4E27F0", Offset = "0x4E11F0", VA = "0x1804E27F0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__4))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002021")]
	[Address(RVA = "0x4E2910", Offset = "0x4E1310", VA = "0x1804E2910", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x6002022")]
	[Address(RVA = "0x4E2A20", Offset = "0x4E1420", VA = "0x1804E2A20", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__6))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002023")]
	[Address(RVA = "0x4E2B40", Offset = "0x4E1540", VA = "0x1804E2B40")]
	public PlayVoice()
	{
	}
}
