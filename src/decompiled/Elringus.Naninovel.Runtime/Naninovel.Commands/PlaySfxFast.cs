using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x2000694")]
[CommandAlias("sfxFast")]
public class PlaySfxFast : AudioCommand, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x2000695")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__8
{
		[Token(Token = "0x400192F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001930")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001931")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlaySfxFast _003C_003E4__this;

		[Token(Token = "0x4001932")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private string _003Cpath_003E5__2;

		[Token(Token = "0x4001933")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private bool _003Cwait_003E5__3;

		[Token(Token = "0x4001934")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001935")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600201C")]
		[Address(RVA = "0x4E1D40", Offset = "0x4E0740", VA = "0x1804E1D40", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600201D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000696")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__6
{
		[Token(Token = "0x4001936")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001937")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001938")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlaySfxFast _003C_003E4__this;

		[Token(Token = "0x4001939")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600201E")]
		[Address(RVA = "0x4E2500", Offset = "0x4E0F00", VA = "0x1804E2500", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600201F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001929")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ResourceContext("Audio", -1, null)]
	public StringParameter SfxPath;

	[Token(Token = "0x400192A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterDefaultValue("1")]
	public DecimalParameter Volume;

	[Token(Token = "0x400192B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterDefaultValue("true")]
	public BooleanParameter Restart;

	[Token(Token = "0x400192C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[ParameterDefaultValue("true")]
	public BooleanParameter Additive;

	[Token(Token = "0x400192D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("group")]
	public StringParameter GroupPath;

	[Token(Token = "0x400192E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	public BooleanParameter Wait;

	[Token(Token = "0x6002018")]
	[Address(RVA = "0x4E1890", Offset = "0x4E0290", VA = "0x1804E1890", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__6))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002019")]
	[Address(RVA = "0x4E19B0", Offset = "0x4E03B0", VA = "0x1804E19B0", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x600201A")]
	[Address(RVA = "0x4E1AC0", Offset = "0x4E04C0", VA = "0x1804E1AC0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__8))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600201B")]
	[Address(RVA = "0x4E1BF0", Offset = "0x4E05F0", VA = "0x1804E1BF0")]
	public PlaySfxFast()
	{
	}
}
