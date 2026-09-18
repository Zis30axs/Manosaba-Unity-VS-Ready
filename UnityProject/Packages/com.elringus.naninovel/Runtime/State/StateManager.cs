using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="IStateManager"/>
    [InitializeAtRuntime(int.MinValue), Goto.DontResetAttribute]
    public class StateManager : IStateManager
    {
        public event Action<GameSaveLoadArgs> OnGameLoadStarted;
        public event Action<GameSaveLoadArgs> OnGameLoadFinished;
        public event Action<GameSaveLoadArgs> OnGameSaveStarted;
        public event Action<GameSaveLoadArgs> OnGameSaveFinished;
        public event Action OnResetStarted;
        public event Action OnResetFinished;
        public event Action OnRollbackStarted;
        public event Action OnRollbackFinished;

        public virtual StateConfiguration Configuration { get; }
        public virtual GlobalStateMap GlobalState { get; private set; }
        public virtual SettingsStateMap SettingsState { get; private set; }
        public virtual ISaveSlotManager<GlobalStateMap> GlobalSlotManager { get; }
        public virtual ISaveSlotManager<GameStateMap> GameSlotManager { get; }
        public virtual ISaveSlotManager<SettingsStateMap> SettingsSlotManager { get; }
        public virtual bool QuickLoadAvailable => GameSlotManager.SaveSlotExists(LastQuickSaveSlotId);
        public virtual bool AnyGameSaveExists => GameSlotManager.AnySaveExists();
        public virtual bool RollbackInProgress => rollbackTaskQueue.Count > 0;

        protected virtual string LastQuickSaveSlotId => Configuration.IndexToQuickSaveSlotId(1);
        protected virtual StateRollbackStack RollbackStack { get; }

        private readonly Queue<GameStateMap> rollbackTaskQueue = new();
        private readonly List<Action<GameStateMap>> onGameSerializeTasks = new();
        private readonly List<Func<GameStateMap, UniTask>> onGameDeserializeTasks = new();
        private IInputSampler rollbackInput;
        private IScriptPlayer scriptPlayer;
        private ICameraManager cameraManager;

        // Remember to not reference any other engine services to make sure this service is always initialized first.
        // This is required for the post engine initialization tasks to be performed before any others.
        public StateManager (StateConfiguration cfg)
        {
            Configuration = cfg;

            if (cfg.EnableStateRollback)
            {
                var rollbackCapacity = Mathf.Max(1, cfg.StateRollbackSteps);
                RollbackStack = new(rollbackCapacity);
            }

            var saveDir = Application.isEditor ? $"{Engine.EditorTransientRoot.GetAfterFirst("Assets/")}/{cfg.SaveFolderName}" : cfg.SaveFolderName;
            GameSlotManager = (ISaveSlotManager<GameStateMap>)Activator.CreateInstance(Type.GetType(cfg.GameStateHandler), cfg, saveDir);
            GlobalSlotManager = (ISaveSlotManager<GlobalStateMap>)Activator.CreateInstance(Type.GetType(cfg.GlobalStateHandler), cfg, saveDir);
            SettingsSlotManager = (ISaveSlotManager<SettingsStateMap>)Activator.CreateInstance(Type.GetType(cfg.SettingsStateHandler), cfg, saveDir);

            Engine.AddPostInitializationTask(PerformPostEngineInitializationTasks);
        }

        public virtual UniTask InitializeService ()
        {
            scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
            cameraManager = Engine.GetServiceOrErr<ICameraManager>();

            return UniTask.CompletedTask;
        }

        public virtual void ResetService ()
        {
            RollbackStack?.Clear();
        }

        public virtual void DestroyService ()
        {
            scriptPlayer?.RemovePreExecutionTask(HandleCommandPreExecution);

            if (rollbackInput != null)
                rollbackInput.OnStart -= HandleRollbackInputStart;

            Engine.RemovePostInitializationTask(PerformPostEngineInitializationTasks);
        }

        public virtual void AddOnGameSerializeTask (Action<GameStateMap> task) => onGameSerializeTasks.Insert(0, task);

        public virtual void RemoveOnGameSerializeTask (Action<GameStateMap> task) => onGameSerializeTasks.Remove(task);

        public virtual void AddOnGameDeserializeTask (Func<GameStateMap, UniTask> task) => onGameDeserializeTasks.Insert(0, task);

        public virtual void RemoveOnGameDeserializeTask (Func<GameStateMap, UniTask> task) => onGameDeserializeTasks.Remove(task);

        public virtual async UniTask<GameStateMap> SaveGame (string slotId)
        {
            var quick = slotId.StartsWithFast(Configuration.QuickSaveSlotMask.GetBefore("{"));

            OnGameSaveStarted?.Invoke(new(slotId, quick));

            var state = new GameStateMap();
            await scriptPlayer.Complete(DoSaveAfterSync);

            OnGameSaveFinished?.Invoke(new(slotId, quick));

            return state;

            async UniTask DoSaveAfterSync ()
            {
                PeekRollbackStack()?.ForceSerialize();

                state.SaveDateTime = DateTime.Now;
                state.Thumbnail = cameraManager.CaptureThumbnail();

                SaveAllServicesToState<IStatefulService<GameStateMap>, GameStateMap>(state);
                PerformOnGameSerializeTasks(state);
                state.RollbackStackJson = SerializeRollbackStack();

                await GameSlotManager.Save(slotId, state);

                // Also save global state on every game save.
                await SaveGlobal();
            }
        }

        public virtual async UniTask<GameStateMap> QuickSave ()
        {
            // Free first quick save slot by shifting existing ones by one.
            for (int i = Configuration.QuickSaveSlotLimit; i > 0; i--)
            {
                var curSlotId = Configuration.IndexToQuickSaveSlotId(i);
                var prevSlotId = Configuration.IndexToQuickSaveSlotId(i + 1);
                GameSlotManager.RenameSaveSlot(curSlotId, prevSlotId);
            }

            // Delete the last slot in case it's out of the limit.
            var outOfLimitSlotId = Configuration.IndexToQuickSaveSlotId(Configuration.QuickSaveSlotLimit + 1);
            if (GameSlotManager.SaveSlotExists(outOfLimitSlotId))
                GameSlotManager.DeleteSaveSlot(outOfLimitSlotId);

            var firstSlotId = Configuration.IndexToQuickSaveSlotId(1);
            return await SaveGame(firstSlotId);
        }

        public virtual async UniTask<GameStateMap> LoadGame (string slotId)
        {
            if (string.IsNullOrEmpty(slotId) || !GameSlotManager.SaveSlotExists(slotId))
                throw new Error($"Slot '{slotId}' not found when loading '{typeof(GameStateMap)}' data.");

            var quick = slotId.EqualsFast(LastQuickSaveSlotId);

            OnGameLoadStarted?.Invoke(new(slotId, quick));

            Engine.Reset();
            var state = await GameSlotManager.Load(slotId);
            await LoadAllServicesFromStateAsync<IStatefulService<GameStateMap>, GameStateMap>(state);
            RollbackStack?.OverrideFromJson(state.RollbackStackJson);
            await PerformOnGameDeserializeTasks(state);

            OnGameLoadFinished?.Invoke(new(slotId, quick));

            return state;
        }

        public virtual async UniTask<GameStateMap> QuickLoad () => await LoadGame(LastQuickSaveSlotId);

        public virtual async UniTask SaveGlobal ()
        {
            SaveAllServicesToState<IStatefulService<GlobalStateMap>, GlobalStateMap>(GlobalState);
            await GlobalSlotManager.Save(Configuration.DefaultGlobalSlotId, GlobalState);
        }

        public virtual async UniTask SaveSettings ()
        {
            SaveAllServicesToState<IStatefulService<SettingsStateMap>, SettingsStateMap>(SettingsState);
            await SettingsSlotManager.Save(Configuration.DefaultSettingsSlotId, SettingsState);
        }

        public virtual async UniTask ResetState (params Func<UniTask>[] tasks)
        {
            await ResetState(default(Type[]), tasks);
        }

        public virtual async UniTask ResetState (IReadOnlyCollection<string> exclude, params Func<UniTask>[] tasks)
        {
            var serviceTypes = Engine.Services.Select(s => s.GetType());
            var excludeTypes = serviceTypes.Where(t => exclude.Contains(t.Name) || t.GetInterfaces().Any(i => exclude.Contains(i.Name))).ToArray();
            await ResetState(excludeTypes, tasks);
        }

        public virtual async UniTask ResetState (IReadOnlyCollection<Type> exclude, params Func<UniTask>[] tasks)
        {
            OnResetStarted?.Invoke();
            using (new InteractionBlocker())
            {
                scriptPlayer?.Playlist?.ReleaseResources();
                Engine.Reset(exclude);
                await PerformOnGameDeserializeTasks(new());
                if (tasks != null)
                    foreach (var task in tasks)
                        if (task != null)
                            await task.Invoke();
            }
            OnResetFinished?.Invoke();
        }

        public virtual void PushRollbackSnapshot (bool allowPlayerRollback)
        {
            if (RollbackStack is null) return;

            var state = new GameStateMap();
            state.SaveDateTime = DateTime.Now;
            state.PlayerRollbackAllowed = allowPlayerRollback;

            SaveAllServicesToState<IStatefulService<GameStateMap>, GameStateMap>(state);
            PerformOnGameSerializeTasks(state);
            RollbackStack.Push(state);
        }

        public virtual async UniTask<bool> Rollback (Predicate<GameStateMap> predicate)
        {
            var state = RollbackStack?.Pop(predicate);
            if (state is null) return false;
            await RollbackToState(state);
            return true;
        }

        public virtual GameStateMap PeekRollbackStack () => RollbackStack?.Peek();

        public virtual bool CanRollbackTo (Predicate<GameStateMap> predicate) => RollbackStack?.Contains(predicate) ?? false;

        public virtual void PurgeRollbackData () => RollbackStack?.ForEach(s => s.PlayerRollbackAllowed = false);

        protected virtual string SerializeRollbackStack ()
        {
            if (RollbackStack is null) return string.Empty;
            return RollbackStack.ToJson(Configuration.SavedRollbackSteps, ShouldSerializeSnapshot);
        }

        protected virtual bool ShouldSerializeSnapshot (GameStateMap state)
        {
            return state.ForcedSerialize || state.PlayerRollbackAllowed;
        }

        protected virtual async UniTask RollbackToState (GameStateMap state)
        {
            rollbackTaskQueue.Enqueue(state);
            OnRollbackStarted?.Invoke();

            while (rollbackTaskQueue.Peek() != state)
                await AsyncUtils.WaitEndOfFrame();

            await LoadAllServicesFromStateAsync<IStatefulService<GameStateMap>, GameStateMap>(state);

            await PerformOnGameDeserializeTasks(state);

            rollbackTaskQueue.Dequeue();
            OnRollbackFinished?.Invoke();
        }

        protected virtual void SaveAllServicesToState<TService, TState> (TState state)
            where TService : class, IStatefulService<TState>
            where TState : StateMap, new()
        {
            foreach (var service in Engine.Services.OfType<TService>())
                service.SaveServiceState(state);
        }

        protected virtual async UniTask LoadAllServicesFromStateAsync<TService, TState> (TState state)
            where TService : class, IStatefulService<TState>
            where TState : StateMap, new()
        {
            foreach (var service in Engine.Services.OfType<TService>())
                await service.LoadServiceState(state);
        }

        protected virtual void PerformOnGameSerializeTasks (GameStateMap state)
        {
            for (int i = onGameSerializeTasks.Count - 1; i >= 0; i--)
                onGameSerializeTasks[i](state);
        }

        protected virtual async UniTask PerformOnGameDeserializeTasks (GameStateMap state)
        {
            for (int i = onGameDeserializeTasks.Count - 1; i >= 0; i--)
                await onGameDeserializeTasks[i](state);
        }

        private async void HandleRollbackInputStart ()
        {
            if (!Configuration.EnableStateRollback || !CanRollbackTo(s => s.PlayerRollbackAllowed)) return;
            await Rollback(s => s.PlayerRollbackAllowed);
        }

        private UniTask HandleCommandPreExecution (Command _)
        {
            PushRollbackSnapshot(false);
            return UniTask.CompletedTask;
        }

        private async UniTask PerformPostEngineInitializationTasks ()
        {
            await LoadSettings();
            if (!Engine.Initializing) return;
            await LoadGlobal();
            if (!Engine.Initializing) return;

            if (Configuration.EnableStateRollback)
                InitializeRollback();

            async UniTask LoadSettings ()
            {
                SettingsState = await SettingsSlotManager.LoadOrDefault(Configuration.DefaultSettingsSlotId);
                await LoadAllServicesFromStateAsync<IStatefulService<SettingsStateMap>, SettingsStateMap>(SettingsState);
            }

            async UniTask LoadGlobal ()
            {
                GlobalState = await GlobalSlotManager.LoadOrDefault(Configuration.DefaultGlobalSlotId);
                await LoadAllServicesFromStateAsync<IStatefulService<GlobalStateMap>, GlobalStateMap>(GlobalState);
            }

            void InitializeRollback ()
            {
                scriptPlayer.AddPreExecutionTask(HandleCommandPreExecution);

                rollbackInput = Engine.GetService<IInputManager>().GetRollback();
                if (rollbackInput != null)
                    rollbackInput.OnStart += HandleRollbackInputStart;
            }
        }
    }
}
