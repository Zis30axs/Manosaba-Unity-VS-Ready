using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to handle <see cref="IEngineService"/>-related and other engine persistent data de-/serialization,
    /// provide API to save/load game state and handle state rollback feature.
    /// </summary>
    public interface IStateManager : IEngineService<StateConfiguration>
    {
        /// <summary>
        /// Invoked when a game load operation (<see cref="LoadGame"/> or <see cref="QuickLoad"/>) is started.
        /// </summary>
        event Action<GameSaveLoadArgs> OnGameLoadStarted;
        /// <summary>
        /// Invoked when a game load operation (<see cref="LoadGame"/> or <see cref="QuickLoad"/>) is finished.
        /// </summary>
        event Action<GameSaveLoadArgs> OnGameLoadFinished;
        /// <summary>
        /// Invoked when a game save operation (<see cref="SaveGame"/> or <see cref="QuickSave"/>) is started.
        /// </summary>
        event Action<GameSaveLoadArgs> OnGameSaveStarted;
        /// <summary>
        /// Invoked when a game save operation (<see cref="SaveGame"/> or <see cref="QuickSave"/>) is finished.
        /// </summary>
        event Action<GameSaveLoadArgs> OnGameSaveFinished;
        /// <summary>
        /// Invoked when a state reset operation (<see cref="ResetState"/>) is started.
        /// </summary>
        event Action OnResetStarted;
        /// <summary>
        /// Invoked when a state reset operation (<see cref="ResetState"/>) is finished.
        /// </summary>
        event Action OnResetFinished;
        /// <summary>
        /// Invoked when a state rollback operation is started.
        /// </summary>
        event Action OnRollbackStarted;
        /// <summary>
        /// Invoked when a state rollback operation is finished.
        /// </summary>
        event Action OnRollbackFinished;

        /// <summary>
        /// Current global state of the engine.
        /// </summary>
        GlobalStateMap GlobalState { get; }
        /// <summary>
        /// Current settings state of the engine.
        /// </summary>
        SettingsStateMap SettingsState { get; }
        /// <summary>
        /// Save slots manager for global engine state.
        /// </summary>
        ISaveSlotManager<GlobalStateMap> GlobalSlotManager { get; }
        /// <summary>
        /// Save slots manager for local engine state.
        /// </summary>
        ISaveSlotManager<GameStateMap> GameSlotManager { get; }
        /// <summary>
        /// Save slots manager for game settings.
        /// </summary>
        ISaveSlotManager<SettingsStateMap> SettingsSlotManager { get; }
        /// <summary>
        /// Whether at least one quick save slot exists.
        /// </summary>
        bool QuickLoadAvailable { get; }
        /// <summary>
        /// Whether any game save slots exist.
        /// </summary>
        bool AnyGameSaveExists { get; }
        /// <summary>
        /// Whether a state rollback is in progress.
        /// </summary>
        bool RollbackInProgress { get; }

        /// <summary>
        /// Adds a task to invoke when serializing (saving) game state.
        /// Use <see cref="GameStateMap"/> to serialize arbitrary custom objects to the game save slot.
        /// </summary>
        void AddOnGameSerializeTask (Action<GameStateMap> task);
        /// <summary>
        /// Removes a task assigned via <see cref="AddOnGameSerializeTask(Action{GameStateMap})"/>.
        /// </summary>
        void RemoveOnGameSerializeTask (Action<GameStateMap> task);
        /// <summary>
        /// Adds an async task to invoke when de-serializing (loading) game state.
        /// Use <see cref="GameStateMap"/> to deserialize previously serialized custom objects from the loaded game save slot.
        /// </summary>
        void AddOnGameDeserializeTask (Func<GameStateMap, UniTask> task);
        /// <summary>
        /// Removes a task assigned via <see cref="AddOnGameDeserializeTask(Func{GameStateMap, UniTask})"/>.
        /// </summary>
        void RemoveOnGameDeserializeTask (Func<GameStateMap, UniTask> task);
        /// <summary>
        /// Saves current game state to the specified save slot.
        /// </summary>
        UniTask<GameStateMap> SaveGame (string slotId);
        /// <summary>
        /// Saves current game state to the first quick save slot.
        /// Will shift the quick save slots chain by one index before saving.
        /// </summary>
        UniTask<GameStateMap> QuickSave ();
        /// <summary>
        /// Loads game state from the specified save slot.
        /// </summary>
        UniTask<GameStateMap> LoadGame (string slotId);
        /// <summary>
        /// Loads game state from the most recent quick save slot.
        /// </summary>
        UniTask<GameStateMap> QuickLoad ();
        /// <summary>
        /// Persists current global state of the engine.
        /// </summary>
        UniTask SaveGlobal ();
        /// <summary>
        /// Persists current settings state of the engine.
        /// </summary>
        UniTask SaveSettings ();
        /// <summary>
        /// Resets engine services and unloads unused assets; will basically revert to an empty initial engine state.
        /// This will also invoke all tasks added with <see cref="AddOnGameDeserializeTask"/> with empty game state.
        /// </summary>
        /// <param name="tasks">Additional tasks to perform during the reset (will be performed in order after the engine reset).</param>
        UniTask ResetState (params Func<UniTask>[] tasks);
        /// <inheritdoc cref="ResetState"/>
        /// <param name="exclude">Type names of the engine services (interfaces) to exclude from reset.</param>
        UniTask ResetState (IReadOnlyCollection<string> exclude, params Func<UniTask>[] tasks);
        /// <inheritdoc cref="ResetState"/>
        /// <param name="exclude">Types of the engine services (interfaces) to exclude from reset.</param>
        UniTask ResetState (IReadOnlyCollection<Type> exclude, params Func<UniTask>[] tasks);
        /// <summary>
        /// Takes a snapshot of the current game state and adds it to the rollback stack.
        /// </summary>
        /// <param name="allowPlayerRollback">Whether player is allowed rolling back to the snapshot; see <see cref="GameStateMap.PlayerRollbackAllowed"/> for more info.</param>
        void PushRollbackSnapshot (bool allowPlayerRollback = true);
        /// <summary>
        /// Returns topmost element in the rollback stack (if any), or null.
        /// </summary>
        GameStateMap PeekRollbackStack ();
        /// <summary>
        /// Attempts to rollback (revert) all the engine services to a state evaluated with the specified predicate.
        /// Be aware, that this will discard all the state snapshots in the rollback stack until the suitable one is found.
        /// </summary>
        /// <param name="predicate">The predicate to use when finding a suitable state snapshot.</param>
        /// <returns>Whether a suitable snapshot was found and the operation succeeded.</returns>
        UniTask<bool> Rollback (Predicate<GameStateMap> predicate);
        /// <summary>
        /// Checks whether a state snapshot evaluated by the specified predicate exists in the rollback stack.
        /// </summary>
        bool CanRollbackTo (Predicate<GameStateMap> predicate);
        /// <summary>
        /// Modifies existing state snapshots to prevent player from rolling back to them.
        /// </summary>
        void PurgeRollbackData ();
    }
}
