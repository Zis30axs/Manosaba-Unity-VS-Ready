using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using SpriteDicing;

namespace Naninovel
{
    [Token(Token = "0x200007D")]
    [ActorResources(typeof(DicedSpriteAtlas), false)]
    public class DicedSpriteBackground : DicedSpriteActor<BackgroundMetadata>, IBackgroundActor, IActor
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200007E")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__2
    {
    		[Token(Token = "0x40001C6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001C7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001C8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DicedSpriteBackground _003C_003E4__this;

    		[Token(Token = "0x40001C9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000233")]
    		[Address(RVA = "0x45B8B0", Offset = "0x45A2B0", VA = "0x18045B8B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000234")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40001C5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	private BackgroundMatcher matcher;

    	[Token(Token = "0x600022F")]
    	[Address(RVA = "0x45B570", Offset = "0x459F70", VA = "0x18045B570")]
    	public DicedSpriteBackground(string id, BackgroundMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
    	{
    	}

    	[Token(Token = "0x6000230")]
    	[Address(RVA = "0x45B5F0", Offset = "0x459FF0", VA = "0x18045B5F0", Slot = "41")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__2))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000231")]
    	[Address(RVA = "0x45B710", Offset = "0x45A110", VA = "0x18045B710", Slot = "48")]
    	public override void Dispose()
    	{
    	}
    }
}
