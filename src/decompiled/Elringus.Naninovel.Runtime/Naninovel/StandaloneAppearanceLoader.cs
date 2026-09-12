using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000023")]
public class StandaloneAppearanceLoader<TResource> : LocalizableResourceLoader<TResource> where TResource : Object
{
	[Token(Token = "0x600009C")]
	public StandaloneAppearanceLoader(string actorId, ActorMetadata meta, IResourceProviderManager resources, ILocalizationManager l10n)
	{
	}
}
