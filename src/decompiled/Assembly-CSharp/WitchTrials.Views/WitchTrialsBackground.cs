using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000027")]
[UsedImplicitly]
[ActorResources(typeof(Texture2D), true)]
public class WitchTrialsBackground : SpriteBackgroundExtended
{
	[StructLayout(3)]
	[Token(Token = "0x2000028")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__3
{
		[Token(Token = "0x4000063")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000064")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000065")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchTrialsBackground _003C_003E4__this;

		[Token(Token = "0x4000066")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000094")]
		[Address(RVA = "0x313E50", Offset = "0x312850", VA = "0x180313E50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000095")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000062")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
	private BackgroundMatcherExtended _matcher;

	// RECONSTRUCTED | Confidence: A | RVA: 0x313970, 14 B (tail jump to SpriteBackground..ctor 0x46C8C0 through the
	// inlined, empty SpriteBackgroundExtended(id, metadata, loader) constructor)
	[Token(Token = "0x600008F")]
	[Address(RVA = "0x313970", Offset = "0x312370", VA = "0x180313970")]
	public WitchTrialsBackground(string id, BackgroundMetadata meta, StandaloneAppearanceLoader<Texture2D> loader)
		: base(id, meta, loader)
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x313980, 230 B (ICF-folded with SpriteBackgroundExtended..ctor(actorId, subId,
	// metadata, loader): `actorId + "-" + subId` when subId is non-empty, SpriteBackground ctor, subId stored at +0xB8)
	[Token(Token = "0x6000090")]
	[Address(RVA = "0x313980", Offset = "0x312380", VA = "0x180313980")]
	public WitchTrialsBackground(string actorId, string subId, BackgroundMetadata meta, StandaloneAppearanceLoader<Texture2D> loader)
		: base(actorId, subId, meta, loader)
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x313A70 (builder stub); MoveNext 0x313E50, 1161 B
	// `<>n__0` (0x313D30, folded with SpriteBackground.Initialize) is the base call. After it completes, the matcher is
	// built from MonoBehaviourActor slot 26 `ActorMeta` and SpriteActor slot 64 `TransitionalRenderer`, stored at +0xC0.
	[Token(Token = "0x6000091")]
	[Address(RVA = "0x313A70", Offset = "0x312470", VA = "0x180313A70", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__3))]
	public override async UniTask Initialize()
	{
		await base.Initialize();
		_matcher = BackgroundMatcherExtended.CreateForBackground(ActorMeta, TransitionalRenderer);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x313B90, 404 B
	// The body up to `+0xB0 matcher?.Stop()` is SpriteBackground.Dispose (0x46C940) inlined: destroy the GameObject
	// (slot 39), unsubscribe OnLocalized from the appearance loader, release it, and stop the base matcher.
	// It then stops our own matcher through CameraMatcher slot 6 `Stop`.
	[Token(Token = "0x6000092")]
	[Address(RVA = "0x313B90", Offset = "0x312590", VA = "0x180313B90", Slot = "48")]
	public override void Dispose()
	{
		base.Dispose();
		_matcher?.Stop();
	}
}
