using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000099")]
    [ActorResources(typeof(LayeredBackgroundBehaviour), false)]
    public class LayeredBackground : LayeredActor<LayeredBackgroundBehaviour, BackgroundMetadata>, IBackgroundActor, IActor
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200009A")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__2
    {
    		[Token(Token = "0x400022E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400022F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000230")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LayeredBackground _003C_003E4__this;

    		[Token(Token = "0x4000231")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60002C7")]
    		[Address(RVA = "0x461780", Offset = "0x460180", VA = "0x180461780", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60002C8")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400022D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private BackgroundMatcher matcher;

    	[Token(Token = "0x60002C3")]
    	[Address(RVA = "0x461530", Offset = "0x45FF30", VA = "0x180461530")]
    	public LayeredBackground(string id, BackgroundMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
    	{
    	}

    	[Token(Token = "0x60002C4")]
    	[Address(RVA = "0x4615B0", Offset = "0x45FFB0", VA = "0x1804615B0", Slot = "41")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__2))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60002C5")]
    	[Address(RVA = "0x4616D0", Offset = "0x4600D0", VA = "0x1804616D0", Slot = "48")]
    	public override void Dispose()
    	{
    	}
    }
}
