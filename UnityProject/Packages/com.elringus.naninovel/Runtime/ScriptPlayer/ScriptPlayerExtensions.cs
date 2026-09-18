namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IScriptPlayer"/>.
    /// </summary>
    public static class ScriptPlayerExtensions
    {
        /// <summary>
        /// Loads a script with specified local resource path and starts executing if from the specified playlist index.
        /// </summary>
        /// <remarks>Load progress is reported by <see cref="IScriptLoader.OnLoadProgress"/> event.</remarks>
        /// <param name="scriptPath">Local resource path of the script to preload and execute.</param>
        /// <param name="playlistIndex">Playlist index of the executed script to start from.</param>
        public static async UniTask LoadAndPlay (this IScriptPlayer player, string scriptPath, int playlistIndex = 0)
        {
            await Engine.GetServiceOrErr<IScriptLoader>().Load(scriptPath, playlistIndex);
            player.Play(scriptPath, playlistIndex);
        }

        /// <summary>
        /// Loads a script with specified local resource path and starts executing at the specified line and inline indexes.
        /// </summary>
        /// <remarks>Load progress is reported by <see cref="IScriptLoader.OnLoadProgress"/> event.</remarks>
        /// <param name="scriptPath">Local resource path of the script to preload and execute.</param>
        /// <param name="lineIndex">Line index to start playback from.</param>
        /// <param name="inlineIndex">Command inline index to start playback from.</param>
        public static async UniTask LoadAndPlayAtLine (this IScriptPlayer player, string scriptPath, int lineIndex, int inlineIndex)
        {
            var script = (Script)await Engine.GetServiceOrErr<IScriptManager>().ScriptLoader.LoadOrErr(scriptPath, player);
            var playIdx = script.Playlist.GetIndexByLine(lineIndex, inlineIndex);
            if (playIdx == -1) throw new Error($"Failed to start '{scriptPath}' script playback at line #{lineIndex}.{inlineIndex}: no playable commands found at or after the line.");
            await player.LoadAndPlay(scriptPath, playIdx);
        }

        /// <summary>
        /// Loads a script with specified local resource path and starts executing from the specified label.
        /// </summary>
        /// <remarks>Load progress is reported by <see cref="IScriptLoader.OnLoadProgress"/> event.</remarks>
        /// <param name="scriptPath">Local resource path of the script to preload and execute.</param>
        /// <param name="label">Label within the script to start playback from.</param>
        public static async UniTask LoadAndPlayAtLabel (this IScriptPlayer player, string scriptPath, string label)
        {
            var script = (Script)await Engine.GetServiceOrErr<IScriptManager>().ScriptLoader.LoadOrErr(scriptPath, player);
            var lineIdx = script.GetLineIndexForLabel(label);
            if (lineIdx == -1) throw new Error($"Failed to start '{scriptPath}' script playback from '{label}' label: label not found.");
            await player.LoadAndPlayAtLine(scriptPath, lineIdx, 0);
        }

        /// <summary>
        /// Starts playback of a script with specified local resource path, starting at specified line and inline indexes.
        /// </summary>
        /// <remarks>
        /// Make sure script resource at the specified path is loaded via <see cref="IScriptManager.ScriptLoader"/> before attempting to play it.
        /// </remarks>
        public static void PlayAtLine (this IScriptPlayer player, string scriptPath, int lineIndex, int inlineIndex)
        {
            var script = Engine.GetServiceOrErr<IScriptManager>().ScriptLoader.GetLoadedOrErr(scriptPath);
            var playIdx = script.Playlist.GetIndexByLine(lineIndex, inlineIndex);
            if (playIdx == -1) throw new Error($"Failed to start '{scriptPath}' script playback at line #{lineIndex}.{inlineIndex}: no playable commands found at or after the line.");
            player.Play(scriptPath, playIdx);
        }

        /// <summary>
        /// Starts playback of a script with specified local resource path, starting at specified label.
        /// </summary>
        /// <remarks>
        /// Make sure script resource at the specified path is loaded via <see cref="IScriptManager.ScriptLoader"/> before attempting to play it.
        /// </remarks>
        public static void PlayAtLabel (this IScriptPlayer player, string scriptPath, string label)
        {
            var script = Engine.GetServiceOrErr<IScriptManager>().ScriptLoader.GetLoadedOrErr(scriptPath);
            var lineIdx = script.GetLineIndexForLabel(label);
            if (lineIdx == -1) throw new Error($"Failed to start '{scriptPath}' script playback from '{label}' label: label not found.");
            player.PlayAtLine(scriptPath, lineIdx, 0);
        }

        /// <summary>
        /// Resumes <see cref="PlayedScript"/> playback at specified line and inline indexes.
        /// </summary>
        /// <param name="lineIndex">Line index to start playback from.</param>
        /// <param name="inlineIndex">Command inline index to start playback from.</param>
        public static void ResumeAtLine (this IScriptPlayer player, int lineIndex, int inlineIndex = 0)
        {
            if (!player.PlayedScript) throw new Error("Failed resume playback: player doesn't have played script.");
            var playIdx = player.PlayedScript.Playlist.GetIndexByLine(lineIndex, inlineIndex);
            if (playIdx == -1) throw new Error($"Failed to resume '{player.PlayedScript.Path}' script playback at line #{lineIndex}.{inlineIndex}: no playable commands found at or after the line.");
            player.Resume(playIdx);
        }

        /// <summary>
        /// Resumes <see cref="PlayedScript"/> playback at specified label.
        /// </summary>
        /// <param name="label">Label within the script to start playback from.</param>
        public static void ResumeAtLabel (this IScriptPlayer player, string label)
        {
            if (!player.PlayedScript) throw new Error("Failed resume playback: player doesn't have played script.");
            var lineIdx = player.PlayedScript.GetLineIndexForLabel(label);
            if (lineIdx == -1) throw new Error($"Failed to start '{player.PlayedScript.Path}' script playback from '{label}' label: label not found.");
            player.ResumeAtLine(lineIdx);
        }

        /// <summary>
        /// Plays specified script independently of the current playback status; returns when all commands are executed.
        /// Will as well preload and release the associated resources before/after playing.
        /// </summary>
        /// <remarks>
        /// Use to additively play transient (runtime-only, non-resource) script without interrupting normal script playback.
        /// Be aware, that transient scripts can't use any features associated with playback state, such as gosub and localizable text.
        /// </remarks>
        /// <param name="playlist">The playlist to play.</param>
        public static async UniTask PlayTransient (this IScriptPlayer _, ScriptPlaylist playlist, AsyncToken token = default)
        {
            await playlist.LoadResources();
            foreach (var command in playlist)
            {
                if (!command.ShouldExecute) continue;
                try { await command.Execute(token); }
                catch (AsyncOperationCanceledException) { }
                token.ThrowIfCanceled();
            }
            playlist.ReleaseResources();
        }

        /// <param name="scriptName">Arbitrary script name to distinguish the script in error logs.</param>
        /// <param name="scriptText">The script text to play.</param>
        /// <inheritdoc cref="PlayTransient(Naninovel.IScriptPlayer,ScriptPlaylist,Naninovel.AsyncToken)"/>
        public static UniTask PlayTransient (this IScriptPlayer player, string scriptName, string scriptText, AsyncToken token = default)
        {
            var script = Script.FromTransient(scriptName, scriptText);
            return PlayTransient(player, script.Playlist, token);
        }

        /// <summary>
        /// Checks whether currently played command has lower indentation level
        /// than next one, ie playback would enter nested block.
        /// </summary>
        public static bool IsEnteringNested (this IScriptPlayer player)
        {
            return player.Playlist.IsEnteringNestedAt(player.PlayedIndex);
        }
    }
}
