using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine.Video;

namespace Naninovel;

[Token(Token = "0x20000CF")]
[ActorResources(typeof(VideoClip), true)]
public class VideoBackground : VideoActor<BackgroundMetadata>, IBackgroundActor, IActor
{
	[StructLayout(3)]
	[Token(Token = "0x20000D0")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__4
{
		[Token(Token = "0x4000335")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000336")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000337")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public VideoBackground _003C_003E4__this;

		[Token(Token = "0x4000338")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000407")]
		[Address(RVA = "0x4768B0", Offset = "0x4752B0", VA = "0x1804768B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000408")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000334")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private BackgroundMatcher matcher;

	[Token(Token = "0x170000AB")]
	protected override string MixerGroup
	{
		[Token(Token = "0x6000402")]
		[Address(RVA = "0x476520", Offset = "0x474F20", VA = "0x180476520", Slot = "67")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000403")]
	[Address(RVA = "0x476570", Offset = "0x474F70", VA = "0x180476570")]
	public VideoBackground(string id, BackgroundMetadata meta, StandaloneAppearanceLoader<VideoClip> loader)
	{
	}

	[Token(Token = "0x6000404")]
	[Address(RVA = "0x4765F0", Offset = "0x474FF0", VA = "0x1804765F0", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__4))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000405")]
	[Address(RVA = "0x476710", Offset = "0x475110", VA = "0x180476710", Slot = "48")]
	public override void Dispose()
	{
	}
}
