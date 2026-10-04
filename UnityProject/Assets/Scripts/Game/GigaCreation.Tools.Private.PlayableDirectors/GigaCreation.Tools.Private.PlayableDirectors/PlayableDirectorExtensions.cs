using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine.Playables;

namespace GigaCreation.Tools.Private.PlayableDirectors
{
    [Token(Token = "0x2000004")]
    public static class PlayableDirectorExtensions
    {
    	[Token(Token = "0x2000005")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass3_0
    	{
    		[Token(Token = "0x4000006")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PlayableDirector self;

    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass3_0()
    		{
    		}

    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xD51F30", Offset = "0xD50930", VA = "0x180D51F30")]
    		internal void _003CWaitForFinishAsync_003Eb__0()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000006")]
    	[CompilerGenerated]
    	private struct _003CPlayAsync_003Ed__0
    {
    		[Token(Token = "0x4000007")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PlayableDirector self;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken token;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xD52040", Offset = "0xD50A40", VA = "0x180D52040", Slot = "4")]
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

    	[StructLayout(3)]
    	[Token(Token = "0x2000007")]
    	[CompilerGenerated]
    	private struct _003CResumeAsync_003Ed__1
    {
    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PlayableDirector self;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken token;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000B")]
    		[Address(RVA = "0xD52310", Offset = "0xD50D10", VA = "0x180D52310", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000008")]
    	[CompilerGenerated]
    	private struct _003CWaitForFinishAsync_003Ed__3
    {
    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PlayableDirector self;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken token;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private _003C_003Ec__DisplayClass3_0 _003C_003E8__1;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenRegistration _003C_003E7__wrap1;

    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private object _003C_003E7__wrap2;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private int _003C_003E7__wrap3;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    		private ValueTaskAwaiter _003C_003Eu__2;

    		[Token(Token = "0x600000D")]
    		[Address(RVA = "0xD52660", Offset = "0xD51060", VA = "0x180D52660", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0xD517C0", Offset = "0xD501C0", VA = "0x180D517C0")]
    	[AsyncStateMachine(typeof(_003CPlayAsync_003Ed__0))]
	public static UniTask PlayAsync(this PlayableDirector self, [Optional] CancellationToken token)
	{
		return PlayCoreAsync(self, token);
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD51960", Offset = "0xD50360", VA = "0x180D51960")]
    	[AsyncStateMachine(typeof(_003CResumeAsync_003Ed__1))]
	public static UniTask ResumeAsync(this PlayableDirector self, [Optional] CancellationToken token)
	{
		return ResumeCoreAsync(self, token);
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD51B00", Offset = "0xD50500", VA = "0x180D51B00")]
	public static bool IsFinished(this PlayableDirector self)
	{
		if (self == null || self.playableAsset == null)
			return true;

		var graph = self.playableGraph;
		if (!graph.IsValid())
			return true;

		int rootCount = graph.GetRootPlayableCount();
		if (rootCount == 0)
			return true;

		for (int index = 0; index < rootCount; index++)
		{
			if (!graph.GetRootPlayable(index).IsDone())
				return false;
		}

		return true;
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD51D70", Offset = "0xD50770", VA = "0x180D51D70")]
    	[AsyncStateMachine(typeof(_003CWaitForFinishAsync_003Ed__3))]
	private static UniTask WaitForFinishAsync(this PlayableDirector self, [Optional] CancellationToken token)
	{
		return WaitForFinishCoreAsync(self, token);
	}

	private static async UniTask PlayCoreAsync(PlayableDirector self, CancellationToken token)
	{
		if (self == null)
			return;

		self.Play();
		await WaitForFinishCoreAsync(self, token);
	}

	private static async UniTask ResumeCoreAsync(PlayableDirector self, CancellationToken token)
	{
		if (self == null)
			return;

		self.Resume();
		await WaitForFinishCoreAsync(self, token);
	}

	private static async UniTask WaitForFinishCoreAsync(PlayableDirector self, CancellationToken token)
	{
		// Cancelling a command must stop the graph as well as release the awaiter;
		// otherwise a destroyed reenactment can keep driving its director after the
		// spawned object has been removed.
		using (token.Register(() =>
		{
			if (self != null)
				self.Stop();
		}))
		{
			await UniTask.WaitUntil(() => IsFinished(self), PlayerLoopTiming.Update, token);
		}
	}
}
}
