using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace WitchTrials.Views;

[Token(Token = "0x200002F")]
[UsedImplicitly]
[CommandAlias("sfx")]
public class PlaySfxModified : PlaySfx
{
	[StructLayout(3)]
	[Token(Token = "0x2000030")]
	[CompilerGenerated]
	private struct _003CPlay_003Ed__0
{
		[Token(Token = "0x4000080")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000081")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000082")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlaySfxModified _003C_003E4__this;

		[Token(Token = "0x4000083")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4000084")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private PooledObject<HashSet<string>> _003C__003E5__2;

		[Token(Token = "0x4000085")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private PooledObject<List<UniTask>> _003C___003E5__3;

		[Token(Token = "0x4000086")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000AA")]
		[Address(RVA = "0x3177D0", Offset = "0x3161D0", VA = "0x1803177D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000AB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x60000A7")]
	[Address(RVA = "0x317300", Offset = "0x315D00", VA = "0x180317300", Slot = "18")]
	[AsyncStateMachine(typeof(_003CPlay_003Ed__0))]
	protected override UniTask Play(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000A8")]
	[Address(RVA = "0x3174A0", Offset = "0x315EA0", VA = "0x1803174A0")]
	private UniTask PlayOrModifyTrack(IAudioManager manager, string path, float volume, bool loop, float time, float fade, string group, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000A9")]
	[Address(RVA = "0x317680", Offset = "0x316080", VA = "0x180317680")]
	public PlaySfxModified()
	{
	}
}
