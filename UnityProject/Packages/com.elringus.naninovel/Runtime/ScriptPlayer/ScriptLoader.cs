using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.Commands;

namespace Naninovel
{
    /// <inheritdoc cref="IScriptLoader"/>
    [InitializeAtRuntime]
    public class ScriptLoader : IStatefulService<GameStateMap>, IScriptLoader
    {
        [Serializable]
        public class GameState
        {
            public string[] LoadedScriptPaths;
        }

        public event Action<float> OnLoadProgress;

        protected virtual IScriptManager ScriptManager { get; }
        protected virtual ResourcePolicy Policy { get; }
        protected virtual bool ShouldRemoveActors { get; }

        private readonly Dictionary<string, ScriptPlaylist> listByLoadedScriptPath = new();
        private readonly HashSet<string> loadingScriptPaths = new();

        public ScriptLoader (ResourceProviderConfiguration config, IScriptManager scriptManager)
        {
            ScriptManager = scriptManager;
            Policy = config.ResourcePolicy;
            ShouldRemoveActors = config.RemoveActors;
        }

        public virtual UniTask InitializeService () => UniTask.CompletedTask;

        public virtual void ResetService () => UnloadAll();

        public virtual void DestroyService () => UnloadAll();

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var state = new GameState {
                LoadedScriptPaths = listByLoadedScriptPath.Keys.ToArray()
            };
            stateMap.SetState(state);
        }

        public virtual UniTask LoadServiceState (GameStateMap stateMap)
        {
            var state = stateMap.GetState<GameState>() ?? new GameState {
                LoadedScriptPaths = Array.Empty<string>()
            };
            if (listByLoadedScriptPath.Count > 0)
                foreach (var orphan in listByLoadedScriptPath.Keys.Except(state.LoadedScriptPaths).ToArray())
                    UnloadScript(orphan);
            if (state.LoadedScriptPaths.Length == 0) return UniTask.CompletedTask;
            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var scriptPath in state.LoadedScriptPaths)
                if (!IsLoaded(scriptPath))
                    tasks.Add(LoadSaved(scriptPath, stateMap.PlaybackSpot));
            return UniTask.WhenAll(tasks);
        }

        public virtual async UniTask Load (string scriptPath, int startIndex = 0)
        {
            // Script being already loaded means it was loaded as dependency, so do nothing.
            if (IsLoaded(scriptPath)) return;
            loadingScriptPaths.Clear();

            // In conservative unload after loading to prevent re-loading shared resources.
            if (Policy == ResourcePolicy.Conservative)
            {
                var playlist = await LoadAndHoldScript(scriptPath);
                var prevLists = listByLoadedScriptPath.Values.ToArray();
                listByLoadedScriptPath.Clear();
                await LoadList(playlist, startIndex);
                await LoadDependencies(playlist);
                foreach (var prevList in prevLists)
                    if (!IsLoaded(prevList.ScriptPath))
                        UnloadList(prevList);
                if (ShouldRemoveActors) RemoveUnusedActors();
                return;
            }

            // In optimistic loads are sparse, so prefer re-loading shared resources
            // instead of keeping resources from both previous and next script batches
            // while loading.
            if (Policy == ResourcePolicy.Optimistic)
            {
                UnloadAll();
                if (ShouldRemoveActors) RemoveUnusedActors();
                var playlist = await LoadAndHoldScript(scriptPath);
                await LoadList(playlist, startIndex);
                await LoadDependencies(playlist);
            }
        }

        protected virtual bool IsLoaded (string scriptPath)
        {
            if (string.IsNullOrWhiteSpace(scriptPath)) return false;
            return listByLoadedScriptPath.ContainsKey(scriptPath);
        }

        protected virtual UniTask LoadList (ScriptPlaylist list, int startIndex)
        {
            if (IsLoaded(list.ScriptPath)) return UniTask.CompletedTask;
            listByLoadedScriptPath.Add(list.ScriptPath, list);
            return list.LoadResources(startIndex, list.Count - 1, OnLoadProgress);
        }

        protected virtual async UniTask LoadSaved (string scriptPath, PlaybackSpot playedSpot)
        {
            var playlist = await LoadAndHoldScript(scriptPath);
            var startIndex = playlist.ScriptPath == playedSpot.ScriptPath
                ? playlist.GetIndexByLine(playedSpot.LineIndex, playedSpot.InlineIndex) : 0;
            await LoadList(playlist, startIndex);
        }

        protected virtual UniTask LoadDependencies (ScriptPlaylist list)
        {
            using var _ = ListPool<UniTask>.Rent(out var tasks);
            loadingScriptPaths.Add(list.ScriptPath);
            foreach (var command in list)
                if (TryGetDependency(command, out var scriptPath) &&
                    !IsLoaded(scriptPath) && loadingScriptPaths.Add(scriptPath))
                    tasks.Add(LoadDependency(scriptPath));
            return UniTask.WhenAll(tasks);
        }

        protected virtual async UniTask LoadDependency (string scriptPath)
        {
            var playlist = await LoadAndHoldScript(scriptPath);
            await LoadDependencies(playlist);
            await LoadList(playlist, 0);
        }

        protected virtual bool TryGetDependency (Command command, out string scriptPath)
        {
            scriptPath = null;
            if (command is Gosub sub) return TryGetScriptDependency(sub.Path, out scriptPath);
            if (command is Goto go && IsDependency(go)) return TryGetScriptDependency(go.Path, out scriptPath);
            return false;
        }

        protected virtual bool TryGetScriptDependency (NamedStringParameter path, out string scriptPath)
        {
            scriptPath = path.Name;
            if (string.IsNullOrWhiteSpace(scriptPath)) return false;
            if (path.DynamicValue) return false;
            if (path.PlaybackSpot.HasValue && scriptPath == path.PlaybackSpot.Value.ScriptPath) return false;
            return true;
        }

        protected virtual bool IsDependency (Goto go)
        {
            if (Policy == ResourcePolicy.Optimistic) return !Command.Assigned(go.Release) || !go.Release.Value;
            if (Policy == ResourcePolicy.Conservative) return Command.Assigned(go.Hold) && go.Hold.Value;
            return false;
        }

        protected virtual void UnloadScript (string scriptPath)
        {
            UnloadList(listByLoadedScriptPath[scriptPath]);
            listByLoadedScriptPath.Remove(scriptPath);
        }

        protected virtual void UnloadList (ScriptPlaylist playlist)
        {
            playlist.ReleaseResources();
            ScriptManager.ScriptLoader.Release(playlist.ScriptPath, this);
        }

        protected virtual void UnloadAll ()
        {
            foreach (var list in listByLoadedScriptPath.Values)
                list.ReleaseResources();
            listByLoadedScriptPath.Clear();
            ScriptManager.ScriptLoader.ReleaseAll(this);
        }

        protected virtual async UniTask<ScriptPlaylist> LoadAndHoldScript (string scriptPath)
        {
            var script = await ScriptManager.ScriptLoader.LoadOrErr(scriptPath, this);
            return script.Object.Playlist;
        }

        protected virtual void RemoveUnusedActors ()
        {
            foreach (var manager in Engine.Services)
                if (manager is IActorManager actorManager)
                    foreach (var actor in actorManager.Actors.ToArray())
                        // Single holder means the actor is holding its own resources and is effectively unused.
                        if (actorManager.GetAppearanceLoader(actor.Id) is { } loader && loader.CountHolders() == 1)
                            actorManager.RemoveActor(actor.Id);
        }
    }
}
