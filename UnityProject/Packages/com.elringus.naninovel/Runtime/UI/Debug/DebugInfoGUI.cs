using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.UI
{
    public class DebugInfoGUI : MonoBehaviour
    {
        private const int windowId = 0;

        private static DebugInfoGUI instance;
        private Rect windowRect = new(20, 20, 300, 100);
        private Vector2 voiceScroll = Vector2.zero;
        private bool show;
        private EngineVersion version;
        private IScriptPlayer player;
        private IAudioManager audioManager;
        private IStateManager stateManager;
        private string lastCommandInfo, lastAutoVoiceName;

        public static void Toggle ()
        {
            if (!instance)
                instance = Engine.CreateObject<DebugInfoGUI>(nameof(DebugInfoGUI));

            instance.show = !instance.show;

            if (instance.show && instance.player != null)
                instance.HandleActionExecuted(instance.player.PlayedCommand);
        }

        private void Awake ()
        {
            version = EngineVersion.LoadFromResources();
            player = Engine.GetServiceOrErr<IScriptPlayer>();
            audioManager = Engine.GetServiceOrErr<IAudioManager>();
            stateManager = Engine.GetServiceOrErr<IStateManager>();
        }

        private void OnEnable ()
        {
            player.OnCommandExecutionStart += HandleActionExecuted;
            stateManager.OnRollbackFinished += HandleRollbackFinished;
        }

        private void OnDisable ()
        {
            player.OnCommandExecutionStart -= HandleActionExecuted;
            stateManager.OnRollbackFinished -= HandleRollbackFinished;
        }

        private void OnGUI ()
        {
            if (!show) return;

            windowRect = GUI.Window(windowId, windowRect, DrawWindow,
                string.IsNullOrEmpty(lastCommandInfo) ? $"Naninovel ver. {version.Version}" : lastCommandInfo);
        }

        private void DrawWindow (int windowID)
        {
            if (player.PlayedCommand != null)
            {
                if (!string.IsNullOrEmpty(lastAutoVoiceName))
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Auto Voice: ");
                    GUILayout.Label(lastAutoVoiceName, GUILayout.MaxWidth(150));
                    if (GUILayout.Button("COPY"))
                        GUIUtility.systemCopyBuffer = lastAutoVoiceName;
                    GUILayout.EndHorizontal();
                }

                GUILayout.FlexibleSpace();
                GUI.enabled = !player.Playing;
                if (!player.Playing && GUILayout.Button("Play")) player.Resume();
                GUI.enabled = player.Playing;
                if (player.Playing && GUILayout.Button("Stop")) player.Stop();
                GUI.enabled = true;
                if (GUILayout.Button("Close Window")) show = false;
            }

            GUI.DragWindow();
        }

        private void HandleActionExecuted (Command command)
        {
            if (player is null || command is null) return;

            lastCommandInfo = player.PlayedCommand.PlaybackSpot.ToString();

            if (audioManager != null && audioManager.Configuration.EnableAutoVoicing && command is PrintText print)
                lastAutoVoiceName = AutoVoiceResolver.Resolve(print.Text);
        }

        private void HandleRollbackFinished () => HandleActionExecuted(player.PlayedCommand);
    }
}
