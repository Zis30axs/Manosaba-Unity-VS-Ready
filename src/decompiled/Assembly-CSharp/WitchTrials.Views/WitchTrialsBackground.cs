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

	[Token(Token = "0x600008F")]
	[Address(RVA = "0x313970", Offset = "0x312370", VA = "0x180313970")]
	public WitchTrialsBackground(string id, BackgroundMetadata meta, StandaloneAppearanceLoader<Texture2D> loader)
	{
	}

	[Token(Token = "0x6000090")]
	[Address(RVA = "0x313980", Offset = "0x312380", VA = "0x180313980")]
	public WitchTrialsBackground(string actorId, string subId, BackgroundMetadata meta, StandaloneAppearanceLoader<Texture2D> loader)
	{
	}

	[Token(Token = "0x6000091")]
	[Address(RVA = "0x313A70", Offset = "0x312470", VA = "0x180313A70", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__3))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000092")]
	[Address(RVA = "0x313B90", Offset = "0x312590", VA = "0x180313B90", Slot = "48")]
	public override void Dispose()
	{
	}
}
