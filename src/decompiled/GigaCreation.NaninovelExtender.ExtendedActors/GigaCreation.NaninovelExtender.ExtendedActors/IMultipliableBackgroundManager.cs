using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200000D")]
public interface IMultipliableBackgroundManager : IMultipliableActorManager<IBackgroundActor>, IActorManager, IEngineService
{
}
