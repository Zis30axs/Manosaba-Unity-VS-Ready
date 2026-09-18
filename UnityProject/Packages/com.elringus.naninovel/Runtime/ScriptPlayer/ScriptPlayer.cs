using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Naninovel
{
    /// <inheritdoc cref="IScriptPlayer"/>
    [InitializeAtRuntime]
    public class ScriptPlayer : IStatefulService<SettingsStateMap>, IStatefulService<GlobalStateMap>, IStatefulService<GameStateMap>, IScriptPlayer
    {
        [Serializable]
        public class Settings
        {
            public PlayerSkipMode SkipMode;
        }

        [Serializable]
        public class GlobalState
        {
            public PlayedScriptRegister PlayedScriptRegister = new();
        }

        [Serializable]
        public class GameState
        {
            public bool Playing;
            public bool ExecutedPlayedCommand;
            public bool WaitingForInput;
            public List<PlaybackSpot> GosubReturnSpots;
        }

        public event Action<Script> OnPlay;
        public event Action<Script> OnStop;
        public event Action<Command> OnCommandExecutionStart;
        public event Action<Command> OnCommandExecutionFinish;
        public event Action<bool> OnSkip;
        public event Action<bool> OnAutoPlay;
        public event Action<bool> OnWaitingForInput;

        public virtual ScriptPlayerConfiguration Configuration { get; }
        public virtual bool Playing => playRoutineCTS != null;
        public virtual bool Completing => completeTCS != null;
        public virtual bool SkipActive { get; private set; }
        public virtual bool AutoPlayActive { get; private set; }
        public virtual bool WaitingForInput { get; private set; }
        public virtual PlayerSkipMode SkipMode { get; set; }
        public virtual Script PlayedScript { get; private set; }
        public virtual Command PlayedCommand => Playlist?.GetCommandByIndex(PlayedIndex);
        public virtual IReadOnlyCollection<Command> ExecutingCommands => playedCommands;
        public virtual PlaybackSpot PlaybackSpot => PlayedCommand?.PlaybackSpot ?? default;
        public virtual ScriptPlaylist Playlist => PlayedScript?.Playlist;
        public virtual int PlayedIndex { get; private set; }
        public virtual Stack<PlaybackSpot> GosubReturnSpots { get; private set; }
        public virtual int PlayedCommandsCount => playedRegister.CountPlayed();

        private readonly List<Func<Command, UniTask>> preExecutionTasks = new();
        private readonly List<Func<Command, UniTask>> postExecutionTasks = new();
        private readonly Queue<Func<UniTask>> onCompleteTasks = new();
        private readonly HashSet<Command> playedCommands = new();
        private readonly IInputManager input;
        private readonly IScriptManager scripts;
        private readonly IStateManager state;
        private bool executedPlayedCommand;
        private bool shouldCompleteNextCommand;
        private PlayedScriptRegister playedRegister;
        private CancellationTokenSource playRoutineCTS;
        private CancellationTokenSource commandExecutionCTS;
        private CancellationTokenSource completionCTS;
        private UniTaskCompletionSource waitForWaitForInputDisabledTCS;
        private UniTaskCompletionSource completeTCS;
        private IInputSampler continueInput, skipInput, toggleSkipInput, autoPlayInput;

        public ScriptPlayer (ScriptPlayerConfiguration cfg, IInputManager input, IScriptManager scripts, IStateManager state)
        {
            Configuration = cfg;
            this.input = input;
            this.scripts = scripts;
            this.state = state;

            GosubReturnSpots = new();
            playedRegister = new();
            commandExecutionCTS = new();
            completionCTS = new();
        }

        public virtual UniTask InitializeService ()
        {
            continueInput = input.GetContinue();
            skipInput = input.GetSkip();
            toggleSkipInput = input.GetToggleSkip();
            autoPlayInput = input.GetAutoPlay();

            if (continueInput != null)
            {
                continueInput.OnStart += DisableWaitingForInput;
                continueInput.OnStart += DisableSkip;
            }
            if (skipInput != null)
            {
                skipInput.OnStart += EnableSkip;
                skipInput.OnEnd += DisableSkip;
            }
            if (toggleSkipInput != null)
                toggleSkipInput.OnStart += ToggleSkip;
            if (autoPlayInput != null)
                autoPlayInput.OnStart += ToggleAutoPlay;

            if (Configuration.ShowDebugOnInit)
                UI.DebugInfoGUI.Toggle();

            return UniTask.CompletedTask;
        }

        public virtual void ResetService ()
        {
            Stop();
            CancelCommands();
            // Playlist?.ReleaseResources(); performed in StateManager; 
            // here it could be invoked after the actors are already destroyed.
            PlayedIndex = -1;
            PlayedScript = scripts.ScriptLoader.Juggle(PlayedScript, null, this);
            executedPlayedCommand = false;
            shouldCompleteNextCommand = false;
            DisableWaitingForInput();
            DisableAutoPlay();
            DisableSkip();
        }

        public virtual void DestroyService ()
        {
            ResetService();

            commandExecutionCTS?.Dispose();
            completionCTS?.Dispose();

            if (continueInput != null)
            {
                continueInput.OnStart -= DisableWaitingForInput;
                continueInput.OnStart -= DisableSkip;
            }
            if (skipInput != null)
            {
                skipInput.OnStart -= EnableSkip;
                skipInput.OnEnd -= DisableSkip;
            }
            if (toggleSkipInput != null)
                toggleSkipInput.OnStart -= ToggleSkip;
            if (autoPlayInput != null)
                autoPlayInput.OnStart -= ToggleAutoPlay;
        }

        public virtual void SaveServiceState (SettingsStateMap stateMap)
        {
            var settings = new Settings {
                SkipMode = SkipMode
            };
            stateMap.SetState(settings);
        }

        public virtual UniTask LoadServiceState (SettingsStateMap stateMap)
        {
            var settings = stateMap.GetState<Settings>() ?? new Settings {
                SkipMode = Configuration.DefaultSkipMode
            };
            SkipMode = settings.SkipMode;
            return UniTask.CompletedTask;
        }

        public virtual void SaveServiceState (GlobalStateMap stateMap)
        {
            var global = new GlobalState {
                PlayedScriptRegister = playedRegister
            };
            stateMap.SetState(global);
        }

        public virtual UniTask LoadServiceState (GlobalStateMap stateMap)
        {
            var global = stateMap.GetState<GlobalState>() ?? new GlobalState();
            playedRegister = global.PlayedScriptRegister;
            return UniTask.CompletedTask;
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var game = new GameState {
                Playing = Playing,
                ExecutedPlayedCommand = executedPlayedCommand,
                WaitingForInput = WaitingForInput,
                GosubReturnSpots = GosubReturnSpots.Count > 0 ? GosubReturnSpots.Reverse().ToList() : null // Stack is reversed on enum.
            };
            stateMap.PlaybackSpot = PlaybackSpot;
            stateMap.SetState(game);
        }

        public virtual async UniTask LoadServiceState (GameStateMap stateMap)
        {
            var game = stateMap.GetState<GameState>();
            if (game is null)
            {
                Playlist?.ReleaseResources();
                ResetService();
                return;
            }

            // Force stop and cancel all running commands to prevent state mutation while loading other services.
            Stop();
            CancelCommands();

            executedPlayedCommand = game.ExecutedPlayedCommand;

            if (game.Playing) // The playback is resumed (when necessary) after other services are loaded.
            {
                if (state.RollbackInProgress) state.OnRollbackFinished += PlayAfterRollback;
                else state.OnGameLoadFinished += PlayAfterLoad;
            }

            if (game.GosubReturnSpots != null && game.GosubReturnSpots.Count > 0)
                GosubReturnSpots = new(game.GosubReturnSpots);
            else GosubReturnSpots.Clear();

            if (string.IsNullOrEmpty(stateMap.PlaybackSpot.ScriptPath)) LoadStoppedState();
            else await LoadPlayingState(stateMap.PlaybackSpot);

            void LoadStoppedState ()
            {
                Playlist?.ReleaseResources();
                PlayedIndex = 0;
                if (PlayedScript) scripts.ScriptLoader.Release(PlayedScript.Path, this);
                PlayedScript = null;
            }

            async UniTask LoadPlayingState (PlaybackSpot spot)
            {
                if (Playlist == null || !PlayedScript || !stateMap.PlaybackSpot.ScriptPath.EqualsFast(PlayedScript.Path))
                {
                    var script = await scripts.ScriptLoader.LoadOrErr(stateMap.PlaybackSpot.ScriptPath, this);
                    PlayedScript = scripts.ScriptLoader.Juggle(PlayedScript, script, this);
                }
                PlayedIndex = FindPlayableIndex(stateMap.PlaybackSpot);
            }

            void PlayAfterRollback ()
            {
                state.OnRollbackFinished -= PlayAfterRollback;
                SetWaitingForInputEnabled(game.WaitingForInput);
                // Rollback snapshots are pushed before the currently played command is executed, so play it again.
                shouldCompleteNextCommand = true;
                Resume();
            }

            void PlayAfterLoad (GameSaveLoadArgs _)
            {
                state.OnGameLoadFinished -= PlayAfterLoad;
                SetWaitingForInputEnabled(game.WaitingForInput);
                // Game could be saved before or after the currently played command is executed.
                if (executedPlayedCommand)
                {
                    if (SelectNextCommand()) Resume();
                }
                else Resume();
            }
        }

        public virtual void AddPreExecutionTask (Func<Command, UniTask> task) => preExecutionTasks.Insert(0, task);

        public virtual void RemovePreExecutionTask (Func<Command, UniTask> task) => preExecutionTasks.Remove(task);

        public virtual void AddPostExecutionTask (Func<Command, UniTask> task) => postExecutionTasks.Insert(0, task);

        public virtual void RemovePostExecutionTask (Func<Command, UniTask> task) => postExecutionTasks.Remove(task);

        public virtual void Play (string scriptPath, int playlistIndex = 0)
        {
            var script = scripts.ScriptLoader.GetLoadedOrErr(scriptPath);
            PlayedScript = scripts.ScriptLoader.Juggle(PlayedScript, script, this);
            Resume(playlistIndex);
        }

        public virtual void Resume (int? playlistIndex = null)
        {
            if (!PlayedScript || Playlist is null)
                throw new Error("Failed to resume script playback: no currently played script.");

            if (Playing) Stop();

            if (playlistIndex.HasValue)
                PlayedIndex = playlistIndex.Value;

            if (Playlist.IsIndexValid(PlayedIndex) || SelectNextCommand())
            {
                playRoutineCTS = new();
                var playRoutineCancellationToken = playRoutineCTS.Token;
                PlayRoutine(playRoutineCancellationToken).Forget();
                if (!playRoutineCancellationToken.IsCancellationRequested)
                    OnPlay?.Invoke(PlayedScript);
            }
        }

        public virtual void Stop ()
        {
            playRoutineCTS?.Cancel();
            playRoutineCTS?.Dispose();
            playRoutineCTS = null;

            OnStop?.Invoke(PlayedScript);
        }

        public virtual async UniTask<bool> Rewind (int lineIndex)
        {
            if (PlayedCommand is null) throw new Error("Script player failed to rewind: played command is not valid.");

            var targetCommand = Playlist?.GetCommandAfterLine(lineIndex, 0);
            if (targetCommand is null) throw new Error($"Script player failed to rewind: target line index ({lineIndex}) is not valid for '{PlayedScript?.Path}' script.");

            var targetPlaylistIndex = Playlist.IndexOf(targetCommand);
            if (targetPlaylistIndex == PlayedIndex) return true;

            var wasWaitingInput = WaitingForInput;

            if (Playing) Stop();
            DisableAutoPlay();
            DisableSkip();
            DisableWaitingForInput();

            playRoutineCTS = new();
            var cancellationToken = playRoutineCTS.Token;

            bool result;
            if (targetPlaylistIndex > PlayedIndex)
            {
                // In case were waiting input, the current command wasn't executed; execute it now.
                result = await FastForwardRoutine(cancellationToken, targetPlaylistIndex, wasWaitingInput);
                Resume();
            }
            else
            {
                var targetSpot = targetCommand.PlaybackSpot;
                result = await state.Rollback(s => s.PlaybackSpot == targetSpot);
            }

            return result;
        }

        public virtual void SetSkipEnabled (bool enable)
        {
            if (SkipActive == enable) return;
            if (enable && !GetSkipAllowed()) return;

            SkipActive = enable;
            Engine.Time.TimeScale = enable ? Configuration.SkipTimeScale : 1f;
            OnSkip?.Invoke(enable);

            if (enable && WaitingForInput)
            {
                state.PeekRollbackStack()?.AllowPlayerRollback();
                SetWaitingForInputEnabled(false);
            }
            if (enable && AutoPlayActive) SetAutoPlayEnabled(false);
        }

        public virtual void SetAutoPlayEnabled (bool enable)
        {
            if (AutoPlayActive == enable) return;
            AutoPlayActive = enable;
            OnAutoPlay?.Invoke(enable);

            if (enable && WaitingForInput) SetWaitingForInputEnabled(false);
        }

        public virtual void SetWaitingForInputEnabled (bool enable)
        {
            if (WaitingForInput == enable) return;

            if (SkipActive && enable || (!enable && (continueInput.Active || AutoPlayActive)))
                state.PeekRollbackStack()?.AllowPlayerRollback();

            if (SkipActive && enable) return;

            WaitingForInput = enable;
            if (!enable)
            {
                waitForWaitForInputDisabledTCS?.TrySetResult();
                waitForWaitForInputDisabledTCS = null;
            }

            OnWaitingForInput?.Invoke(enable);
        }

        public virtual async UniTask Complete (Func<UniTask> onComplete = null)
        {
            if (onComplete != null)
                onCompleteTasks.Enqueue(onComplete);

            if (completeTCS != null)
            {
                await completeTCS.Task;
                return;
            }

            using (new InteractionBlocker())
            {
                completionCTS.Cancel();
                completeTCS = new();

                await UniTask.WaitWhile(() => playedCommands.Count > 0);

                while (onCompleteTasks.Count > 0)
                    await onCompleteTasks.Dequeue()();

                completionCTS.Dispose();
                completionCTS = new();
                completeTCS.TrySetResult();
                completeTCS = null;
            }
        }

        public virtual bool HasPlayed (string scriptPath, int? playlistIndex = null)
        {
            if (playlistIndex.HasValue) return playedRegister.IsIndexPlayed(scriptPath, playlistIndex.Value);
            return playedRegister.IsScriptPlayed(scriptPath);
        }

        /// <summary>
        /// In case <see cref="Complete"/> request is being handled, will wait until it's finished;
        /// returns true in case specified token has requested cancellation.
        /// </summary>
        /// <remarks>This should be awaited after any async operation in the playback routine.</remarks>
        protected virtual async UniTask<bool> WaitCompletion (AsyncToken token)
        {
            if (token.Canceled) return true;
            if (completeTCS != null)
                await completeTCS.Task;
            return token.Canceled;
        }

        protected virtual int FindPlayableIndex (PlaybackSpot spot)
        {
            if (Playlist?.IndexOf(spot) is { } index && index >= 0) return index;

            if (Configuration.ResolveMode == PlayerResolveMode.Error)
                throw new Error($"Failed to play '{spot}': the script has probably changed after the save was made.");

            if (Configuration.ResolveMode == PlayerResolveMode.Restart && Playlist?.GetCommandAfterLine(0, -1) is { } firstCommand)
                return Playlist.IndexOf(firstCommand.PlaybackSpot);

            if (Playlist?.GetCommandAfterLine(spot.LineIndex, -1) is { } nextCommand)
            {
                Engine.Warn($"Failed to play '{spot}': the script has probably changed after the save was made." +
                            " Will play next command instead; expect undefined behaviour.");
                return Playlist.IndexOf(nextCommand.PlaybackSpot);
            }
            if (Playlist?.GetCommandBeforeLine(spot.LineIndex, 0) is { } prevCommand)
            {
                Engine.Warn($"Failed to play '{spot}': the script has probably changed after the save was made." +
                            " Will play previous command instead; expect undefined behaviour.");
                return Playlist.IndexOf(prevCommand.PlaybackSpot);
            }
            Engine.Warn($"Failed to play '{spot}': neither the spot, nor playable commands after it were found.");

            throw new Error($"Failed to play '{spot}': the script has no playable commands.");
        }

        protected virtual void EnableSkip () => SetSkipEnabled(true);
        protected virtual void DisableSkip () => SetSkipEnabled(false);
        protected virtual void ToggleSkip () => SetSkipEnabled(!SkipActive);
        protected virtual void EnableAutoPlay () => SetAutoPlayEnabled(true);
        protected virtual void DisableAutoPlay () => SetAutoPlayEnabled(false);
        protected virtual void ToggleAutoPlay () => SetAutoPlayEnabled(!AutoPlayActive);
        protected virtual void EnableWaitingForInput () => SetWaitingForInputEnabled(true);
        protected virtual void DisableWaitingForInput () => SetWaitingForInputEnabled(false);

        protected virtual bool GetSkipAllowed ()
        {
            if (SkipMode == PlayerSkipMode.Everything) return true;
            if (PlayedScript is null) return false;
            return HasPlayed(PlayedScript.Path, PlayedIndex + 1);
        }

        protected virtual async UniTask WaitForWaitForInputDisabled ()
        {
            waitForWaitForInputDisabledTCS ??= new();
            await waitForWaitForInputDisabledTCS.Task;
        }

        protected virtual async UniTask WaitForInputInAutoPlay ()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Configuration.MinAutoPlayDelay), true);
            while (AutoPlayActive && WaitingForInput && Engine.GetService<IAudioManager>()?.GetPlayedVoice() != null)
                await AsyncUtils.WaitEndOfFrame();
            if (!AutoPlayActive) await WaitForWaitForInputDisabled(); // In case autoplay was disabled while waiting for delay.
        }

        protected virtual async UniTask ExecutePlayedCommand (AsyncToken token)
        {
            if (PlayedScript is null || PlayedCommand is null || !PlayedCommand.ShouldExecute) return;

            OnCommandExecutionStart?.Invoke(PlayedCommand);

            playedRegister.RegisterPlayedIndex(PlayedScript.Path, PlayedIndex);

            for (int i = preExecutionTasks.Count - 1; i >= 0; i--)
            {
                await preExecutionTasks[i](PlayedCommand);
                if (await WaitCompletion(token)) return;
            }

            if (await WaitCompletion(token)) return;

            var completionToken = shouldCompleteNextCommand ? new(true) : completionCTS.Token;
            shouldCompleteNextCommand = false;
            executedPlayedCommand = true;
            playedCommands.Add(PlayedCommand);

            var executionToken = new AsyncToken(commandExecutionCTS.Token, completionToken);
            await ExecuteIgnoringCancellation(PlayedCommand, executionToken);

            if (await WaitCompletion(token)) return;

            for (int i = postExecutionTasks.Count - 1; i >= 0; i--)
            {
                await postExecutionTasks[i](PlayedCommand);
                if (await WaitCompletion(token)) return;
            }

            if (await WaitCompletion(token)) return;

            OnCommandExecutionFinish?.Invoke(PlayedCommand);
        }

        protected virtual async UniTask ExecuteIgnoringCancellation (Command command, AsyncToken token)
        {
            try { await command.Execute(token); }
            catch (AsyncOperationCanceledException) { }
            finally { playedCommands.Remove(command); }
        }

        protected virtual async UniTask PlayRoutine (AsyncToken token)
        {
            while (Engine.Initialized && Playing)
            {
                if (WaitingForInput)
                {
                    if (AutoPlayActive)
                    {
                        await UniTask.WhenAny(WaitForInputInAutoPlay(), WaitForWaitForInputDisabled());
                        if (await WaitCompletion(token)) return;
                        DisableWaitingForInput();
                    }
                    else
                    {
                        await WaitForWaitForInputDisabled();
                        if (await WaitCompletion(token)) return;
                    }
                }

                await ExecutePlayedCommand(token);
                if (await WaitCompletion(token)) return;

                var nextActionAvailable = SelectNextCommand();
                if (!nextActionAvailable) break;

                if (SkipActive && !GetSkipAllowed()) SetSkipEnabled(false);
            }
        }

        protected virtual async UniTask<bool> FastForwardRoutine (AsyncToken token, int targetPlaylistIndex, bool executePlayedCommand)
        {
            SetSkipEnabled(true);

            if (executePlayedCommand)
            {
                await ExecutePlayedCommand(token);
                if (await WaitCompletion(token)) return false;
            }

            var reachedLine = true;
            while (Engine.Initialized && Playing)
            {
                var nextCommandAvailable = SelectNextCommand();
                if (!nextCommandAvailable)
                {
                    reachedLine = false;
                    break;
                }

                if (PlayedIndex >= targetPlaylistIndex)
                {
                    reachedLine = true;
                    break;
                }

                await ExecutePlayedCommand(token);
                if (await WaitCompletion(token)) return false;
                SetSkipEnabled(true); // Force skip mode to be always active while fast-forwarding.

                if (token.Canceled)
                {
                    reachedLine = false;
                    break;
                }
            }

            SetSkipEnabled(false);
            return reachedLine;
        }

        /// <summary>
        /// Attempts to select next <see cref="Command"/> in the current <see cref="Playlist"/>.
        /// </summary>
        /// <returns>Whether next command is available and was selected.</returns>
        protected virtual bool SelectNextCommand ()
        {
            if (Playlist is null) return false;

            var nextIndex = -1;
            var nextCommand = Playlist.GetCommandByIndex(PlayedIndex + 1);

            if (nextCommand == null)
            {
                if (PlayedCommand?.Indent > 0)
                    nextIndex = Playlist.GetNestedHost(PlayedIndex).GetNextPlaybackIndex(Playlist, PlayedIndex);
            }
            else if (PlayedCommand is Command.INestedHost && !PlayedCommand.ShouldExecute)
                nextIndex = Playlist.SkipNestedAt(PlayedIndex, PlayedCommand.Indent);
            else if (PlayedCommand is Command.INestedHost host && nextCommand.Indent > PlayedCommand.Indent)
                nextIndex = host.GetNextPlaybackIndex(Playlist, PlayedIndex);
            else if (PlayedCommand?.Indent == 0)
                nextIndex = PlayedIndex + 1;
            else nextIndex = Playlist.GetNestedHost(PlayedIndex).GetNextPlaybackIndex(Playlist, PlayedIndex);

            PlayedIndex = nextIndex;
            if (!Playlist.IsIndexValid(PlayedIndex))
            {
                // No commands left in the played script.
                Engine.Warn($"Script '{PlayedScript?.Path}' has finished playing, and there wasn't a follow-up goto command. " +
                            "Consider using stop command in case you wish to gracefully stop script execution.", PlaybackSpot);
                Stop();
                return false;
            }

            executedPlayedCommand = false;
            return true;
        }

        /// <summary>
        /// Cancels all the asynchronously-running commands.
        /// </summary>
        /// <remarks>
        /// Be aware that this could lead to an inconsistent state; only use when the current engine state is going to be discarded 
        /// (eg, when preparing to load a game or perform state rollback).
        /// </remarks>
        protected virtual void CancelCommands ()
        {
            commandExecutionCTS.Cancel();
            commandExecutionCTS.Dispose();
            commandExecutionCTS = new();
        }
    }
}
