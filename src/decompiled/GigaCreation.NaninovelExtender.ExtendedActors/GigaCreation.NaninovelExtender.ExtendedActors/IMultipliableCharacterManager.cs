using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200001A")]
public interface IMultipliableCharacterManager : IMultipliableActorManager<ICharacterActor>, IActorManager, IEngineService
{
}
