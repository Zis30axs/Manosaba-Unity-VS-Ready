using System.IO;
using System.Text;
using JetBrains.Annotations;
using Naninovel.Bridging;
using Naninovel.UI;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public static class BridgingService
    {
        private static readonly JsonSerializer serde = new();
        private static string bridgingDir;
        private static string metadataFile;
        [CanBeNull] private static Server server;
        [CanBeNull] private static IOFiles files;

        public static void RestartServer ()
        {
            ResolvePaths();
            StopServer();
            var cfg = Configuration.GetOrDefault<EngineConfiguration>();
            if (!cfg.EnableBridging) return;
            if (cfg.AutoGenerateMetadata) EditorApplication.delayCall += UpdateMetadata;
            StartServer();
        }

        [MenuItem("Naninovel/Update Metadata %#u", priority = 3)]
        public static void UpdateMetadata ()
        {
            ResolvePaths();
            var meta = MetadataGenerator.GenerateProjectMetadata();
            var json = serde.Serialize(meta);
            File.WriteAllText(metadataFile, json, Encoding.UTF8);
            server?.NotifyMetadataUpdated();
        }

        private static void ResolvePaths ()
        {
            var root = Path.GetFullPath(PackagePath.TransientDataPath);
            bridgingDir = $"{root}/Bridging";
            metadataFile = $"{root}/Metadata.json";
            if (!Directory.Exists(bridgingDir)) Directory.CreateDirectory(bridgingDir);
        }

        private static void StartServer ()
        {
            files?.Dispose();
            server = new(files = new(bridgingDir), new JsonSerializer());
            server.Start(new() {
                Name = $"{Application.productName} (Unity)",
                Version = EngineVersion.LoadFromResources().BuildVersionTag()
            });
            server.OnGotoRequested += HandleGotoRequest;
            Engine.OnInitializationFinished += AttachServiceListeners;
            Engine.OnDestroyed += NotifyPlaybackStopped;
        }

        private static void StopServer ()
        {
            Engine.OnInitializationFinished -= AttachServiceListeners;
            Engine.OnDestroyed -= NotifyPlaybackStopped;
            if (server != null) server.OnGotoRequested -= HandleGotoRequest;
            server = null;
            files?.Dispose();
        }

        private static void AttachServiceListeners ()
        {
            if (Engine.Behaviour is not RuntimeBehaviour) return;
            Engine.GetServiceOrErr<IScriptPlayer>().OnCommandExecutionStart += NotifyPlayedCommand;
        }

        private static void HandleGotoRequest (Bridging.PlaybackSpot spot)
        {
            var scriptPath = spot.ScriptPath;
            var lineIdx = spot.LineIndex;
            if (!Application.isPlaying) EditorApplication.EnterPlaymode();
            if (Engine.Initialized) OnEngineInit();
            else Engine.OnInitializationFinished += OnEngineInit;

            void OnEngineInit ()
            {
                Engine.OnInitializationFinished -= OnEngineInit;
                var player = Engine.GetServiceOrErr<IScriptPlayer>();
                if (player.PlayedScript && player.PlayedScript.Path == scriptPath)
                    player.Rewind(lineIdx).Forget();
                else
                    Engine.GetServiceOrErr<IStateManager>().ResetState()
                        .ContinueWith(() => player.LoadAndPlay(scriptPath))
                        .ContinueWith(() => Engine.GetServiceOrErr<IUIManager>().GetUI<ITitleUI>()?.Hide())
                        .ContinueWith(() => player.Rewind(lineIdx)).Forget();
            }
        }

        private static void NotifyPlayedCommand (Command command)
        {
            server?.NotifyPlaybackStatusChanged(new() {
                Playing = true,
                PlayedSpot = new() {
                    ScriptPath = command.PlaybackSpot.ScriptPath,
                    LineIndex = command.PlaybackSpot.LineIndex,
                    InlineIndex = command.PlaybackSpot.InlineIndex
                }
            });
        }

        private static void NotifyPlaybackStopped ()
        {
            server?.NotifyPlaybackStatusChanged(new() { Playing = false });
        }
    }
}
