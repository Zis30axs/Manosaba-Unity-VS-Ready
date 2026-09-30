using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000086")]
    [ActorResources(typeof(GenericBackgroundBehaviour), false)]
    public class GenericBackground : GenericActor<GenericBackgroundBehaviour, BackgroundMetadata>, IBackgroundActor, IActor
    {
    	[Token(Token = "0x6000258")]
    	[Address(RVA = "0x45C840", Offset = "0x45B240", VA = "0x18045C840")]
    	public GenericBackground(string id, BackgroundMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
    	{
    	}
    }
}
