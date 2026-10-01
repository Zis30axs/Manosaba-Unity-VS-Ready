using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    // Fixed local request file; no arbitrary paths, commands or method names are accepted.
    [InitializeOnLoad]
    public static class LocalizationSyncBridge
    {
        private static readonly string Root = Path.GetFullPath(Path.Combine(Application.dataPath, "../Library/ManosabaLocalization"));
        private static double nextPoll;
        private static bool busy;
        private static string waitingId;

        [Serializable] private class Request { public string id = string.Empty; public string expiresUtc = string.Empty; }
        [Serializable] private class Result { public string id; public string status; public string message; }

        static LocalizationSyncBridge() { EditorApplication.update += Poll; }

        private static void WriteResult(string id, string status, string message)
        {
            Directory.CreateDirectory(Root);
            string path = Path.Combine(Root, "result.json");
            string temp = path + ".tmp";
            File.WriteAllText(temp, JsonUtility.ToJson(new Result { id = id, status = status, message = message }, true));
            if (File.Exists(path)) File.Replace(temp, path, null);
            else File.Move(temp, path);
        }

        private static void Poll()
        {
            if (busy || EditorApplication.timeSinceStartup < nextPoll ||
                EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            nextPoll = EditorApplication.timeSinceStartup + 0.5;
            string path = Path.Combine(Root, "request.json");
            if (!File.Exists(path)) return;
            Request request = null;
            try
            {
                request = JsonUtility.FromJson<Request>(File.ReadAllText(path));
                if (request == null || !Guid.TryParseExact(request.id, "N", out _) ||
                    !DateTime.TryParse(request.expiresUtc, CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind, out DateTime expiry) || expiry.ToUniversalTime() < DateTime.UtcNow)
                {
                    File.Delete(path);
                    return;
                }
                if (EditorApplication.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    if (waitingId != request.id)
                    {
                        WriteResult(request.id, "WAITING_FOR_EDIT_MODE", "Stop Play Mode in Unity; the build will start automatically.");
                        waitingId = request.id;
                    }
                    return;
                }
                busy = true;
                // Consume before importing/building so domain reloads cannot repeat the request.
                File.Delete(path);
                WriteResult(request.id, "BUILDING", "Importing edited text and rebuilding Addressables.");
                AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
                DateTime started = DateTime.UtcNow;
                int code = AddressablesBuild.Execute();
                string reportPath = Path.GetFullPath(Path.Combine(Application.dataPath,
                    "../../tools/unity-rebuild/out/unity/addressables_build_result.json"));
                var report = File.Exists(reportPath)
                    ? JsonUtility.FromJson<AddressablesBuild.BuildResult>(File.ReadAllText(reportPath)) : null;
                bool ready = code == 0 && report != null && report.status == "OK" &&
                    report.player_streaming_source_ready &&
                    DateTime.TryParse(report.generated, CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind, out DateTime generated) && generated.ToUniversalTime() >= started;
                WriteResult(request.id, ready ? "OK" : "ERROR",
                    ready ? "Content is ready. Start a new Play Mode session in Chinese."
                              : "Addressables build failed. See the Unity Console and addressables_build_result.json.");
                Debug.Log("[LocalizationSync] Build exit code: " + code);
            }
            catch (Exception error)
            {
                if (request != null && !string.IsNullOrEmpty(request.id))
                    WriteResult(request.id, "ERROR", error.Message);
                Debug.LogException(error);
            }
            finally { busy = false; }
        }
    }
}
