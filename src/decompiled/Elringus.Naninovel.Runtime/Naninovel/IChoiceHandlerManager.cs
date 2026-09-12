using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000053")]
public interface IChoiceHandlerManager : IActorManager<IChoiceHandlerActor, ChoiceHandlerState, ChoiceHandlerMetadata, ChoiceHandlersConfiguration>, IActorManager, IEngineService, IEngineService<ChoiceHandlersConfiguration>, IStatefulService<GameStateMap>
{
	[Token(Token = "0x1700002E")]
	IResourceLoader<GameObject> ChoiceButtonLoader
	{
		[Token(Token = "0x600013E")]
		get;
	}

	[Token(Token = "0x600013F")]
	void PushPickedChoice(PlaybackSpot hostedAt, PlaybackSpot continueAt);

	[Token(Token = "0x6000140")]
	PlaybackSpot PopPickedChoice(PlaybackSpot hostedAt);
}
