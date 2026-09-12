using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200001F")]
public interface IMultipliableActorManager<TActor> : IActorManager, IEngineService where TActor : IActor
{
	[Token(Token = "0x600004E")]
	bool ActorExists(string actorId, string subId);

	[Token(Token = "0x600004F")]
	TActor GetActor(string actorId, string subId);

	[Token(Token = "0x6000050")]
	UniTask<TActor> AddActor(string actorId, string subId);
}
