using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000030")]
public interface IBackgroundManager : IActorManager<IBackgroundActor, BackgroundState, BackgroundMetadata, BackgroundsConfiguration>, IActorManager, IEngineService, IEngineService<BackgroundsConfiguration>, IStatefulService<GameStateMap>
{
}
