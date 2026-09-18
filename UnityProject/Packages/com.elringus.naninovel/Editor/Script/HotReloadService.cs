using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Handles script hot reload feature.
    /// </summary>
    public static class HotReloadService
    {
        private static ScriptsConfiguration configuration;
        private static IScriptPlayer player;
        private static IStateManager state;
        private static string[] playedLineHashes;

        /// <summary>
        /// Performs hot-reload of the currently played script.
        /// </summary>
        public static async UniTask ReloadPlayedScript ()
        {
            if (player?.Playlist is null || player.Playlist.Count == 0 || !player.PlayedScript)
            {
                Engine.Err("Failed to perform hot reload: script player is not available or no script is currently played.");
                return;
            }

            var lastPlayedLineIndex = (player.PlayedCommand ?? player.Playlist.Last()).PlaybackSpot.LineIndex;

            // Find the first modified line in the updated script (before the played line).
            var rollbackIndex = -1;
            for (int i = 0; i < lastPlayedLineIndex; i++)
            {
                if (!player.PlayedScript.Lines.IsIndexValid(i)) // The updated script ends before the currently played line.
                {
                    rollbackIndex = player.Playlist.GetCommandBeforeLine(i - 1, 0)?.PlaybackSpot.LineIndex ?? 0;
                    break;
                }

                if (playedLineHashes?.IsIndexValid(i) ?? false)
                {
                    var oldLineHash = playedLineHashes[i];
                    var newLine = player.PlayedScript.Lines[i];
                    if (oldLineHash.EqualsFast(newLine.LineHash)) continue;
                }

                rollbackIndex = player.Playlist.GetCommandBeforeLine(i, 0)?.PlaybackSpot.LineIndex ?? 0;
                break;
            }

            // Find the playlist index to resume with.
            var playlist = player.PlayedScript.Playlist;
            var playIdx = playlist.FindIndex(c => c.PlaybackSpot.LineIndex == lastPlayedLineIndex);
            playIdx = Mathf.Clamp(playIdx, 0, playlist.Count - 1);
            await playlist.LoadResources(playIdx, playlist.Count - 1);

            if (rollbackIndex > -1) // Script has changed before the played line.
                // Rollback to the line before the first modified one.
                await state.Rollback(s => s.PlaybackSpot.LineIndex == rollbackIndex);
            else player.Resume(playIdx); // Script has changed after the played line, just resume.
        }

        internal static void Initialize ()
        {
            ScriptFileWatcher.OnModified -= HandleScriptModified;
            ScriptFileWatcher.OnModified += HandleScriptModified;

            Engine.OnInitializationFinished -= HandleEngineInitialized;
            Engine.OnInitializationFinished += HandleEngineInitialized;

            void HandleEngineInitialized ()
            {
                if (Engine.Behaviour is not RuntimeBehaviour) return;

                configuration ??= Configuration.GetOrDefault<ScriptsConfiguration>();
                player = Engine.GetServiceOrErr<IScriptPlayer>();
                state = Engine.GetServiceOrErr<IStateManager>();
                if (configuration.HotReloadScripts)
                    player.OnPlay += UpdateLineHashes;
            }
        }

        private static void UpdateLineHashes (Script script)
        {
            playedLineHashes = script.Lines.Select(l => l.LineHash).ToArray();
        }

        private static async void HandleScriptModified (string assetPath)
        {
            if (!Engine.Initialized || Engine.Behaviour is not RuntimeBehaviour || !configuration.HotReloadScripts ||
                !player.PlayedScript || player.Playlist?.Count == 0) return;

            var scriptAsset = AssetDatabase.LoadAssetAtPath<Script>(assetPath);
            if (!scriptAsset) return;

            if (player.PlayedScript.Path != scriptAsset.Path) return;

            await ReloadPlayedScript();
        }
    }
}
