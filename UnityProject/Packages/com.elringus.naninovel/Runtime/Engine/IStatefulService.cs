
namespace Naninovel
{
    /// <summary>
    /// Implementation represents a <see cref="IEngineService"/> that has a persistent 
    /// state and is able to save/load it using <typeparamref name="TState"/>.
    /// </summary>
    public interface IStatefulService<TState> : IEngineService where TState : StateMap
    {
        /// <summary>
        /// Serializes service state via <see cref="StateMap.SetState{TState}(TState, string)"/>.
        /// </summary>
        void SaveServiceState (TState stateMap);
        /// <summary>
        /// De-serializes service state via <see cref="StateMap.GetState{TState}(string)"/>.
        /// </summary>
        UniTask LoadServiceState (TState stateMap);
    }
}
