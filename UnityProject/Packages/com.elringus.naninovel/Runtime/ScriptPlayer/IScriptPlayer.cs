using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Naninovel.Commands;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to handle <see cref="Script"/> execution (playback).
    /// </summary>
    public interface IScriptPlayer : IEngineService<ScriptPlayerConfiguration>
    {
        /// <summary>
        /// Event invoked when player starts playing a script.
        /// </summary>
        event Action<Script> OnPlay;
        /// <summary>
        /// Event invoked when player stops playing a script.
        /// </summary>
        event Action<Script> OnStop;
        /// <summary>
        /// Event invoked when player starts executing a <see cref="Command"/>.
        /// </summary>
        event Action<Command> OnCommandExecutionStart;
        /// <summary>
        /// Event invoked when player finishes executing a <see cref="Command"/>.
        /// </summary>
        event Action<Command> OnCommandExecutionFinish;
        /// <summary>
        /// Event invoked when skip mode changes.
        /// </summary>
        event Action<bool> OnSkip;
        /// <summary>
        /// Event invoked when auto play mode changes.
        /// </summary>
        event Action<bool> OnAutoPlay;
        /// <summary>
        /// Event invoked when waiting for input mode changes.
        /// </summary>
        event Action<bool> OnWaitingForInput;

        /// <summary>
        /// Whether script playback routine is currently running.
        /// </summary>
        bool Playing { get; }
        /// <summary>
        /// Whether player is currently handling <see cref="Complete"/> request,
        /// ie waiting for all the executing async commands to finish.
        /// </summary>
        bool Completing { get; }
        /// <summary>
        /// Whether skip mode is currently active.
        /// </summary>
        bool SkipActive { get; }
        /// <summary>
        /// Whether auto play mode is currently active.
        /// </summary>
        bool AutoPlayActive { get; }
        /// <summary>
        /// Whether user input is required to execute next script command.
        /// </summary>
        bool WaitingForInput { get; }
        /// <summary>
        /// Skip mode to use while <see cref="SkipActive"/>.
        /// </summary>
        PlayerSkipMode SkipMode { get; set; }
        /// <summary>
        /// Currently played <see cref="Script"/> or null when not playing any.
        /// </summary>
        Script PlayedScript { get; }
        /// <summary>
        /// Currently played <see cref="Command"/> or null when not playing any.
        /// </summary>
        Command PlayedCommand { get; }
        /// <summary>
        /// Commands that are currently being executed, including un-awaited playing
        /// at background in parallel with <see cref="PlayedCommand"/>.
        /// </summary>
        IReadOnlyCollection<Command> ExecutingCommands { get; }
        /// <summary>
        /// Currently played <see cref="Naninovel.PlaybackSpot"/> or default when not playing any.
        /// </summary>
        PlaybackSpot PlaybackSpot { get; }
        /// <summary>
        /// List of <see cref="Command"/> built upon the currently played <see cref="Script"/>.
        /// </summary>
        ScriptPlaylist Playlist { get; }
        /// <summary>
        /// Index of the currently played command within the <see cref="Playlist"/>.
        /// </summary>
        int PlayedIndex { get; }
        /// <summary>
        /// Last playback return spots stack registered by <see cref="Gosub"/> commands.
        /// </summary>
        Stack<PlaybackSpot> GosubReturnSpots { get; }
        /// <summary>
        /// Total number of unique commands ever played by the player (global state scope).
        /// </summary>
        int PlayedCommandsCount { get; }

        /// <summary>
        /// Starts executing script with specified local resource path.
        /// </summary>
        /// <remarks>
        /// Make sure script resource at the specified path is loaded via <see cref="IScriptManager.ScriptLoader"/> before attempting to play it.
        /// </remarks>
        /// <param name="scriptPath">Local resource path of the script to execute.</param>
        /// <param name="playlistIndex">Playlist index of the executed script to start from.</param>
        void Play (string scriptPath, int playlistIndex = 0);
        /// <summary>
        /// Resumes <see cref="PlayedScript"/> playback at <paramref name="playlistIndex"/> when specified, or at <see cref="PlayedIndex"/>.
        /// </summary>
        /// <param name="playlistIndex">The playback (command) index in <see cref="Playlist"/> to resume playback from.</param>
        void Resume (int? playlistIndex = null);
        /// <summary>
        /// Halts the playback of the currently played script.
        /// </summary>
        void Stop ();
        /// <summary>
        /// Depending on whether the specified <paramref name="lineIndex"/> being before or after currently played command' line index,
        /// performs a fast-forward playback or state rollback of the currently loaded script.
        /// </summary>
        /// <param name="lineIndex">The line index to rewind at.</param>
        /// <returns>Whether the <paramref name="lineIndex"/> has been reached.</returns>
        UniTask<bool> Rewind (int lineIndex);
        /// <summary>
        /// Whether the player has ever played a command at the specified script path and playlist index (global state).
        /// When index is not specified, will just check if the script has ever played, at any index.
        /// </summary>
        bool HasPlayed (string scriptPath, int? playlistIndex = null);
        /// <summary>
        /// Sets the player skip mode.
        /// </summary>
        void SetSkipEnabled (bool enabled);
        /// <summary>
        /// Sets the player auto play mode.
        /// </summary>
        void SetAutoPlayEnabled (bool enabled);
        /// <summary>
        /// Sets the player waiting for input mode.
        /// </summary>
        void SetWaitingForInputEnabled (bool enabled);
        /// <summary>
        /// Adds a task to perform before a command is executed.
        /// </summary>
        void AddPreExecutionTask (Func<Command, UniTask> task);
        /// <summary>
        /// Removes a task to perform before a command is executed.
        /// </summary>
        void RemovePreExecutionTask (Func<Command, UniTask> task);
        /// <summary>
        /// Adds a task to perform after a command is executed.
        /// </summary>
        void AddPostExecutionTask (Func<Command, UniTask> task);
        /// <summary>
        /// Removes a task to perform after a command is executed.
        /// </summary>
        void RemovePostExecutionTask (Func<Command, UniTask> task);
        /// <summary>
        /// Requests all the executing commands to be completed ASAP (via <see cref="AsyncToken.Completed"/>)
        /// and waits for them to finish before performing the specified task (if any) and executing next commands.
        /// </summary>
        UniTask Complete ([CanBeNull] Func<UniTask> onComplete = null);
    }
}
