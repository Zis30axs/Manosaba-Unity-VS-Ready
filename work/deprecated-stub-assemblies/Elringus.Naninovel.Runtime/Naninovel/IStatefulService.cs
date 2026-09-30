using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200034A")]
    public interface IStatefulService<TState> : IEngineService where TState : StateMap
    {
    	[Token(Token = "0x6000FD4")]
    	void SaveServiceState(TState stateMap);

    	[Token(Token = "0x6000FD5")]
    	UniTask LoadServiceState(TState stateMap);
    }
}
