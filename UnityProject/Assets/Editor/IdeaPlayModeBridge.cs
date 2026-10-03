using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Manosaba.EditorTools
{
    // Handles the fixed request written by tools/unity-rebuild/run_unity_game_idea.ps1.
    // This keeps the IDEA entry point usable whether the Editor is already open or not.
    [InitializeOnLoad]
    public static class IdeaPlayModeBridge
    {
        private const string Root = "Library/ManosabaIdeaLauncher";
        private const string RequestName = "request.json";
        private const string ResultName = "result.json";
        private const string PendingIdKey = "Manosaba.IdeaPlayModeBridge.pendingId";
        private const string ScenePath = "Assets/Scenes/Boot.unity";

        private static readonly string RootPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../" + Root));
        private static double nextPoll;
        private static bool busy;
        private static string lastStatus;

        [Serializable]
        private sealed class Request
        {
            public string id = string.Empty;
            public string expiresUtc = string.Empty;
        }

        [Serializable]
        private sealed class Result
        {
            public string id;
            public string status;
            public string message;
        }

        static IdeaPlayModeBridge()
        {
            EditorApplication.update -= Poll;
            EditorApplication.update += Poll;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        [InitializeOnLoadMethod]
        private static void ReconnectAfterDomainReload()
        {
            EditorApplication.update -= Poll;
            EditorApplication.update += Poll;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void Poll()
        {
            if (busy || EditorApplication.timeSinceStartup < nextPoll) return;
            nextPoll = EditorApplication.timeSinceStartup + 0.5;

            string requestPath = Path.Combine(RootPath, RequestName);
            if (!File.Exists(requestPath)) return;

            Request request = null;
            try
            {
                request = JsonUtility.FromJson<Request>(File.ReadAllText(requestPath));
                if (request == null || !Guid.TryParseExact(request.id, "N", out _) ||
                    !DateTime.TryParse(request.expiresUtc, CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind, out DateTime expiry) ||
                    expiry.ToUniversalTime() < DateTime.UtcNow)
                {
                    File.Delete(requestPath);
                    return;
                }

                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    File.Delete(requestPath);
                    WriteResult(request.id, "ALREADY_PLAYING", "Unity is already in Play Mode.");
                    return;
                }

                if (EditorApplication.isCompiling || EditorApplication.isUpdating)
                {
                    WriteStatus(request.id, "WAITING", "Waiting for Unity to finish compiling or importing.");
                    return;
                }

                busy = true;
                File.Delete(requestPath);
                SceneAsset bootScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
                if (bootScene == null)
                    throw new InvalidOperationException("Missing Assets/Scenes/Boot.unity. Run the project bootstrap first.");

                // playModeStartScene changes the scene used by Play Mode without opening or overwriting
                // the scene currently being edited.
                EditorSceneManager.playModeStartScene = bootScene;
                SessionState.SetString(PendingIdKey, request.id);
                WriteResult(request.id, "STARTING", "Starting Manosaba Play Mode from Boot.unity.");
                EditorApplication.EnterPlaymode();
            }
            catch (Exception error)
            {
                if (request != null && !string.IsNullOrEmpty(request.id))
                {
                    SessionState.EraseString(PendingIdKey);
                    WriteResult(request.id, "ERROR", error.Message);
                }
                else
                {
                    try { File.Delete(requestPath); } catch { }
                }
                Debug.LogException(error);
            }
            finally { busy = false; }
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            string id = SessionState.GetString(PendingIdKey, string.Empty);
            if (string.IsNullOrEmpty(id)) return;

            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                SessionState.EraseString(PendingIdKey);
                WriteResult(id, "PLAYING", "Manosaba is running in Unity Play Mode.");
            }
            else if (state == PlayModeStateChange.ExitingEditMode)
            {
                WriteStatus(id, "ENTERING_PLAY_MODE", "Unity is entering Play Mode.");
            }
        }

        private static void WriteStatus(string id, string status, string message)
        {
            if (lastStatus == status) return;
            WriteResult(id, status, message);
        }

        private static void WriteResult(string id, string status, string message)
        {
            Directory.CreateDirectory(RootPath);
            string path = Path.Combine(RootPath, ResultName);
            string temporary = path + ".tmp";
            File.WriteAllText(temporary, JsonUtility.ToJson(new Result
            {
                id = id,
                status = status,
                message = message,
            }, true));
            if (File.Exists(path)) File.Replace(temporary, path, null);
            else File.Move(temporary, path);
            lastStatus = status;
        }
    }
}
