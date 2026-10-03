using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
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
        private const string PreviousBuilderKey = "Manosaba.IdeaPlayModeBridge.previousBuilder";
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
            public bool useAssetDatabase = true;
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

            // EnterPlaymode can force a domain reload before EnteredPlayMode is
            // delivered to this static class.  In that case the request has
            // already been consumed and the pending id is the only durable
            // hand-off left for the newly loaded editor assembly.
            if (EditorApplication.isPlaying)
            {
                string pendingId = SessionState.GetString(PendingIdKey, string.Empty);
                if (!string.IsNullOrEmpty(pendingId))
                {
                    SessionState.EraseString(PendingIdKey);
                    WriteResult(pendingId, "PLAYING", "Manosaba is running in Unity Play Mode.");
                }
                return;
            }

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

                ConfigurePlayModeBuilder(request.useAssetDatabase);

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
            if (state == PlayModeStateChange.EnteredEditMode)
                RestorePlayModeBuilder();

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

        private static void ConfigurePlayModeBuilder(bool useAssetDatabase)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            if (settings == null || settings.DataBuilders == null || settings.DataBuilders.Count == 0)
                throw new InvalidOperationException("Addressables settings have no Play Mode data builders. Reimport the project before launching from IDEA.");

            int selected = -1;
            for (int i = 0; i < settings.DataBuilders.Count; i++)
            {
                if (!(settings.DataBuilders[i] is IDataBuilder builder)) continue;
                bool isAssetDatabase = builder.Name.IndexOf("Asset Database", StringComparison.OrdinalIgnoreCase) >= 0;
                bool isExistingBuild = builder.Name.IndexOf("Existing Build", StringComparison.OrdinalIgnoreCase) >= 0;
                if ((useAssetDatabase && isAssetDatabase) || (!useAssetDatabase && isExistingBuild))
                {
                    selected = i;
                    break;
                }
            }

            if (selected < 0)
            {
                string mode = useAssetDatabase ? "Use Asset Database" : "Use Existing Build";
                throw new InvalidOperationException("Could not find the Addressables Play Mode builder: " + mode + ".");
            }

            if (!SessionState.GetBool(PreviousBuilderKey + ".set", false))
            {
                SessionState.SetInt(PreviousBuilderKey, settings.ActivePlayModeDataBuilderIndex);
                SessionState.SetBool(PreviousBuilderKey + ".set", true);
            }
            settings.ActivePlayModeDataBuilderIndex = selected;
            Debug.Log("[IdeaPlayModeBridge] Addressables Play Mode builder: " +
                      (useAssetDatabase ? "Use Asset Database" : "Use Existing Build"));
        }

        private static void RestorePlayModeBuilder()
        {
            if (!SessionState.GetBool(PreviousBuilderKey + ".set", false)) return;
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            if (settings != null)
            {
                int previous = SessionState.GetInt(PreviousBuilderKey, settings.ActivePlayModeDataBuilderIndex);
                if (previous >= 0 && previous < settings.DataBuilders.Count)
                    settings.ActivePlayModeDataBuilderIndex = previous;
            }
            SessionState.EraseString(PreviousBuilderKey);
            SessionState.SetBool(PreviousBuilderKey + ".set", false);
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
            IOException lastError = null;
            for (int attempt = 0; attempt < 20; attempt++)
            {
                try
                {
                    if (File.Exists(path)) File.Replace(temporary, path, null);
                    else File.Move(temporary, path);
                    lastError = null;
                    break;
                }
                catch (IOException error)
                {
                    lastError = error;
                    System.Threading.Thread.Sleep(50);
                }
            }
            if (lastError != null) throw lastError;
            lastStatus = status;
        }
    }
}
