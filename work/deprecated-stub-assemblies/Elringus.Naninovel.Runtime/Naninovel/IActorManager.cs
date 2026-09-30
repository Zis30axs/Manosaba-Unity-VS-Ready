using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using JetBrains.Annotations;

namespace Naninovel
{
    [Token(Token = "0x200005A")]
    public interface IActorManager : IEngineService
    {
    	[Token(Token = "0x17000036")]
    	ActorManagerConfiguration ActorManagerConfiguration
    	{
    		[Token(Token = "0x6000160")]
    		get;
    	}

    	[Token(Token = "0x17000037")]
    	IEnumerable<IActor> Actors
    	{
    		[Token(Token = "0x6000161")]
    		get;
    	}

    	[Token(Token = "0x14000005")]
    	event Action<string> OnActorAdded;

    	[Token(Token = "0x14000006")]
    	event Action<string> OnActorRemoved;

    	[Token(Token = "0x6000162")]
    	bool ActorExists(string actorId);

    	[Token(Token = "0x6000163")]
    	IActor GetActor(string actorId);

    	[Token(Token = "0x6000164")]
    	UniTask<IActor> AddActor(string actorId);

    	[Token(Token = "0x6000165")]
    	void RemoveActor(string actorId);

    	[Token(Token = "0x6000166")]
    	void RemoveAllActors();

    	[Token(Token = "0x6000167")]
    	ActorState GetActorState(string actorId);

    	[Token(Token = "0x6000168")]
    	[CanBeNull]
    	IResourceLoader GetAppearanceLoader(string actorId);
    }
    [Token(Token = "0x200005B")]
    public interface IActorManager<TActor, TState, TMeta, TConfig> : IActorManager, IEngineService, IEngineService<TConfig>, IStatefulService<GameStateMap> where TActor : IActor where TState : ActorState<TActor>, new() where TMeta : ActorMetadata where TConfig : ActorManagerConfiguration<TMeta>
    {
    	[Token(Token = "0x17000038")]
    	new IReadOnlyCollection<TActor> Actors
    	{
    		[Token(Token = "0x6000169")]
    		get;
    	}

    	[Token(Token = "0x600016A")]
    	new UniTask<TActor> AddActor(string actorId);

    	[Token(Token = "0x600016B")]
    	UniTask<TActor> AddActor(string actorId, TState state);

    	[Token(Token = "0x600016C")]
    	new TActor GetActor(string actorId);

    	[Token(Token = "0x600016D")]
    	new TState GetActorState(string actorId);
    }
}
