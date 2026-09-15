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

	// RECONSTRUCTED | Confidence: A | RVA: 0x317300 (stub), MoveNext 0x3177D0, 3200 B
	// Same shape as Naninovel PlaySfx.Play, but calling this class's instance PlayOrModifyTrack:
	// - duration = Assigned(Duration) (+0x58, ICommandParameter slot 0) ? Duration.Value : AudioManager
	//   (AudioCommand.get_AudioManager 0x4DE6D0 = Engine.GetServiceOrErr<IAudioManager>()) Configuration.DefaultFadeDuration (+0x50).
	// - Assigned(SfxPath): await PlayOrModifyTrack(AudioManager, SfxPath, Volume, Loop, duration, FadeInDuration, GroupPath, token).
	// - Otherwise `<>5__2` (+0x30) = CollectionPool<HashSet<string>, string>.Rent(out paths), `<>5__3` (+0x40) =
	//   CollectionPool<List<UniTask>, UniTask>.Rent(out tasks); IAudioManager slot 28 GetPlayedSfx(paths); per path
	//   tasks.Add(PlayOrModifyTrack(AudioManager (getter inlined here), path, Volume, Loop, duration, FadeInDuration, null, token));
	//   await UniTask.WhenAll(tasks); both pooled objects disposed in reverse order.
	[Token(Token = "0x60000A7")]
	[Address(RVA = "0x317300", Offset = "0x315D00", VA = "0x180317300", Slot = "18")]
	[AsyncStateMachine(typeof(_003CPlay_003Ed__0))]
	protected override async UniTask Play(AsyncToken token)
	{
		float duration = Assigned(Duration) ? Duration.Value : AudioManager.Configuration.DefaultFadeDuration;
		if (Assigned(SfxPath))
		{
			await PlayOrModifyTrack(AudioManager, SfxPath, Volume, Loop, duration, FadeInDuration, GroupPath, token);
			return;
		}
		using (CollectionPool<HashSet<string>, string>.Rent(out HashSet<string> paths))
		{
			using (CollectionPool<List<UniTask>, UniTask>.Rent(out List<UniTask> tasks))
			{
				AudioManager.GetPlayedSfx(paths);
				foreach (string path in paths)
				{
					tasks.Add(PlayOrModifyTrack(AudioManager, path, Volume, Loop, duration, FadeInDuration, null, token));
				}
				await UniTask.WhenAll(tasks);
			}
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3174A0, 469 B
	// Instance variant of PlaySfx.PlayOrModifyTrack (the base one is static):
	// - IAudioManager slot 25 IsSfxPlaying(path) -> slot 14 ModifySfx(path, volume, loop, time, token).
	// - Else slot 11 AudioLoader, IResourceLoader slot 5 IsLoaded(path) -> slot 19 PlaySfx(path, volume, fade, loop, group, token).
	// - Else Command vtable slot 7 Warn("効果音がロードされていません：" + path) and a completed task.
	[Token(Token = "0x60000A8")]
	[Address(RVA = "0x3174A0", Offset = "0x315EA0", VA = "0x1803174A0")]
	private UniTask PlayOrModifyTrack(IAudioManager manager, string path, float volume, bool loop, float time, float fade, string group, AsyncToken token)
	{
		if (manager.IsSfxPlaying(path))
		{
			return manager.ModifySfx(path, volume, loop, time, token);
		}
		if (manager.AudioLoader.IsLoaded(path))
		{
			return manager.PlaySfx(path, volume, fade, loop, group, token);
		}
		Warn("効果音がロードされていません：" + path);
		return UniTask.CompletedTask;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x317680, 335 B
	// Folded with Naninovel.Commands.PlaySfx..ctor: that base ctor is inlined (Volume +0x38 = 1f, Loop +0x40 = false,
	// FadeInDuration +0x48 = 0f, then Command..ctor). This class declares no fields of its own, so the body is empty.
	[Token(Token = "0x60000A9")]
	[Address(RVA = "0x317680", Offset = "0x316080", VA = "0x180317680")]
	public PlaySfxModified()
	{
	}
}
