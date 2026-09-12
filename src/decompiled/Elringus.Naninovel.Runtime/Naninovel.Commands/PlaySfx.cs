using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x2000691")]
[CommandAlias("sfx")]
public class PlaySfx : AudioCommand, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x2000692")]
	[CompilerGenerated]
	private struct _003CPlay_003Ed__10
{
		[Token(Token = "0x400191E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400191F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001920")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlaySfx _003C_003E4__this;

		[Token(Token = "0x4001921")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001922")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001923")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private PooledObject<HashSet<string>> _003C__003E5__2;

		[Token(Token = "0x4001924")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private PooledObject<List<UniTask>> _003C___003E5__3;

		[Token(Token = "0x6002014")]
		[Address(RVA = "0x4E0890", Offset = "0x4DF290", VA = "0x1804E0890", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002015")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000693")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__7
{
		[Token(Token = "0x4001925")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001926")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001927")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlaySfx _003C_003E4__this;

		[Token(Token = "0x4001928")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002016")]
		[Address(RVA = "0x4E15A0", Offset = "0x4DFFA0", VA = "0x1804E15A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002017")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001917")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ResourceContext("Audio", -1, null)]
	public StringParameter SfxPath;

	[Token(Token = "0x4001918")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterDefaultValue("1")]
	public DecimalParameter Volume;

	[Token(Token = "0x4001919")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterDefaultValue("false")]
	public BooleanParameter Loop;

	[Token(Token = "0x400191A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[ParameterAlias("fade")]
	[ParameterDefaultValue("0")]
	public DecimalParameter FadeInDuration;

	[Token(Token = "0x400191B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("group")]
	public StringParameter GroupPath;

	[Token(Token = "0x400191C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x400191D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	public BooleanParameter Wait;

	[Token(Token = "0x600200D")]
	[Address(RVA = "0x4E0120", Offset = "0x4DEB20", VA = "0x1804E0120", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__7))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600200E")]
	[Address(RVA = "0x4E0240", Offset = "0x4DEC40", VA = "0x1804E0240", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x600200F")]
	[Address(RVA = "0x4E0350", Offset = "0x4DED50", VA = "0x1804E0350", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002010")]
	[Address(RVA = "0x4E0440", Offset = "0x4DEE40", VA = "0x1804E0440", Slot = "18")]
	[AsyncStateMachine(typeof(_003CPlay_003Ed__10))]
	protected virtual UniTask Play(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002011")]
	[Address(RVA = "0x4E05E0", Offset = "0x4DEFE0", VA = "0x1804E05E0", Slot = "19")]
	protected virtual bool ShouldSkip()
	{
		return default(bool);
	}

	[Token(Token = "0x6002012")]
	[Address(RVA = "0x4E0780", Offset = "0x4DF180", VA = "0x1804E0780")]
	private static UniTask PlayOrModifyTrack(IAudioManager manager, string path, float volume, bool loop, float time, float fade, string group, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002013")]
	[Address(RVA = "0x317680", Offset = "0x316080", VA = "0x180317680")]
	public PlaySfx()
	{
	}
}
