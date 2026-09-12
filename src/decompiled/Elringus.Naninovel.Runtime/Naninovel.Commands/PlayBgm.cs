using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x200068E")]
[CommandAlias("bgm")]
public class PlayBgm : AudioCommand, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x200068F")]
	[CompilerGenerated]
	private struct _003CPlay_003Ed__11
{
		[Token(Token = "0x400190C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400190D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400190E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlayBgm _003C_003E4__this;

		[Token(Token = "0x400190F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001910")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001911")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private PooledObject<HashSet<string>> _003C__003E5__2;

		[Token(Token = "0x4001912")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private PooledObject<List<UniTask>> _003C___003E5__3;

		[Token(Token = "0x6002009")]
		[Address(RVA = "0x4DEED0", Offset = "0x4DD8D0", VA = "0x1804DEED0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600200A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000690")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__8
{
		[Token(Token = "0x4001913")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001914")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001915")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlayBgm _003C_003E4__this;

		[Token(Token = "0x4001916")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600200B")]
		[Address(RVA = "0x4DFC40", Offset = "0x4DE640", VA = "0x1804DFC40", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600200C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001904")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ResourceContext("Audio", -1, null)]
	public StringParameter BgmPath;

	[Token(Token = "0x4001905")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("intro")]
	[ResourceContext("Audio", -1, null)]
	public StringParameter IntroBgmPath;

	[Token(Token = "0x4001906")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterDefaultValue("1")]
	public DecimalParameter Volume;

	[Token(Token = "0x4001907")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[ParameterDefaultValue("true")]
	public BooleanParameter Loop;

	[Token(Token = "0x4001908")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("fade")]
	[ParameterDefaultValue("0")]
	public DecimalParameter FadeInDuration;

	[Token(Token = "0x4001909")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ParameterAlias("group")]
	public StringParameter GroupPath;

	[Token(Token = "0x400190A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x400190B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	public BooleanParameter Wait;

	[Token(Token = "0x6002003")]
	[Address(RVA = "0x4DE730", Offset = "0x4DD130", VA = "0x1804DE730", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__8))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002004")]
	[Address(RVA = "0x4DE850", Offset = "0x4DD250", VA = "0x1804DE850", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x6002005")]
	[Address(RVA = "0x4DEA10", Offset = "0x4DD410", VA = "0x1804DEA10", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002006")]
	[Address(RVA = "0x4DEAC0", Offset = "0x4DD4C0", VA = "0x1804DEAC0", Slot = "18")]
	[AsyncStateMachine(typeof(_003CPlay_003Ed__11))]
	protected virtual UniTask Play(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002007")]
	[Address(RVA = "0x4DEC60", Offset = "0x4DD660", VA = "0x1804DEC60", Slot = "19")]
	protected virtual UniTask PlayOrModifyTrack(IAudioManager manager, string path, float volume, bool loop, float time, float fade, string introPath, string group, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002008")]
	[Address(RVA = "0x4DED80", Offset = "0x4DD780", VA = "0x1804DED80")]
	public PlayBgm()
	{
	}
}
