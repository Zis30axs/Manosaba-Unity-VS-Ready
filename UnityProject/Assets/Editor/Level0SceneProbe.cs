// Experiment: can the Unity *editor* read the shipped player-build boot scene (`manosaba_Data/level0`)?
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.Level0SceneProbe.Run -logFile -
//
// Why: the boot scene is the real entry point of the game, but it ships inside the built Player, so it is not
// an AssetBundle asset and no extraction pass covered it.  Its bytes *are* readable (see
// tools/unity-rebuild/decode_level0_boot.py, which recovers the entry component's references), and if Unity's
// own importer can deserialize the file, the scene can be re-serialized with this project's script GUIDs and
// used as the boot scene directly - far better than hand-authoring an equivalent.
//
// MEASURED RESULT (2026-... , this project, Unity 6000.0.48f1): it cannot.  Importing the shipped file makes
// the editor abort with
//     "Assets/__BootSceneProbe/level0.unity is corrupted! [Position out of bounds!]"
// (exit 1073741845, batchmode aborted; log: out/logs/level0_import_probe_aborted.log).  A player-build scene
// has no type tree, and the editor requires one, so the boot scene can only be *reconstructed* - the decoded
// inventory in out/unity/level0_scene_inventory.json is the input for that.  Keep this probe as the recorded
// experiment, but do not expect a different outcome.
//
// Safety: a corrupted scene left in Assets/ makes the whole project unopenable, so a failed import is cleaned
// up here.  A *fatal* abort kills the process natively, before any managed cleanup can run - after such a run
// delete the scratch folder by hand:
//     Remove-Item UnityProject/Assets/__BootSceneProbe -Recurse -Force; Remove-Item UnityProject/Temp/UnityLockfile
//
// The probe is deliberately non-destructive about the outcome: it copies the shipped file to a scratch folder
// outside the content plan (Assets/__BootSceneProbe), imports it, and reports exactly what survived.  Nothing
// is saved back, so a failed read leaves no broken scene in the project.
//
// Writes tools/unity-rebuild/out/unity/level0_import_probe.json.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manosaba.EditorTools
{
    public static class Level0SceneProbe
    {
        private const string ScratchFolder = "Assets/__BootSceneProbe";
        private const string ScenePath = ScratchFolder + "/level0.unity";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/level0_import_probe.json";

        [MenuItem("Manosaba/Probe Shipped Boot Scene Import")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            var result = new ProbeResult { generated = DateTime.UtcNow.ToString("o") };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            // Unity logs the importer's complaints; they are the most informative part of the result.
            var messages = new List<string>();
            Application.LogCallback capture = (condition, stackTrace, type) =>
            {
                if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert ||
                    type == LogType.Warning)
                {
                    if (messages.Count < 200) messages.Add(type + ": " + condition);
                }
            };
            Application.logMessageReceived += capture;

            try
            {
                string source = Environment.GetEnvironmentVariable("MANOSABA_LEVEL0_SOURCE");
                if (string.IsNullOrEmpty(source))
                {
                    result.status = "BLOCKED";
                    result.error = "MANOSABA_LEVEL0_SOURCE is not set (path to the shipped 'level0' file)";
                }
                else if (!File.Exists(source))
                {
                    result.status = "BLOCKED";
                    result.error = "shipped scene not found: " + source;
                }
                else
                {
                    result.source = source;
                    result.source_bytes = new FileInfo(source).Length;

                    Directory.CreateDirectory(ScratchFolder);
                    File.Copy(source, ScenePath, true);
                    result.copied_to = ScenePath;
                    AssetDatabase.Refresh();
                    AssetDatabase.ImportAsset(ScenePath, ImportAssetOptions.ForceUpdate);

                    var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
                    result.imported_as_scene_asset = asset != null;

                    Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
                    result.open_ok = scene.IsValid();
                    result.scene_name = scene.name;
                    result.root_count = scene.rootCount;

                    var rootObjects = scene.GetRootGameObjects().ToList();
                    result.root_names = rootObjects.Select(go => go.name).ToList();

                    var all = rootObjects.SelectMany(go => go.GetComponentsInChildren<Component>(true)).ToList();
                    result.component_count = all.Count;
                    result.missing_script_count = all.Count(c => c == null);
                    result.component_histogram = all.Where(c => c != null)
                        .GroupBy(c => c.GetType().FullName)
                        .OrderByDescending(g => g.Count())
                        .ToDictionary(g => g.Key, g => g.Count());

                    var gameObjects = rootObjects.SelectMany(go => go.GetComponentsInChildren<Transform>(true))
                        .Select(t => t.gameObject).ToList();
                    result.gameobject_count = gameObjects.Count;
                    result.gameobject_names = gameObjects.Select(go => go.name).ToList();

                    // The entry component is the concrete question: if its serialized references survived, the
                    // scene is usable as a boot scene rather than merely "loadable".
                    result.entry_components = gameObjects
                        .SelectMany(go => go.GetComponents<Component>())
                        .Where(c => c != null && c.GetType().Name == "GameInitializer")
                        .Select(c => c.GetType().FullName).ToList();

                    result.status = result.open_ok ? "READ" : "OPEN_FAILED";
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[Level0SceneProbe] " + ex);
            }
            finally
            {
                Application.logMessageReceived -= capture;

                // Do not leave an unreadable scene in the project: Unity cannot open a project whose Assets/
                // holds an asset it fails to deserialize.  (A *fatal* import abort never reaches this code;
                // the header comment says how to clean up after that case.)
                if (result.status != "READ" && Directory.Exists(ScratchFolder))
                {
                    try
                    {
                        AssetDatabase.DeleteAsset(ScratchFolder);
                        result.scratch_removed = true;
                    }
                    catch (Exception cleanupEx)
                    {
                        Debug.LogWarning("[Level0SceneProbe] scratch cleanup failed: " + cleanupEx.Message);
                    }
                }
            }

            result.log_messages = messages;
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            Debug.Log("[Level0SceneProbe] " + result.status + " imported=" + result.imported_as_scene_asset +
                      " open=" + result.open_ok + " roots=" + result.root_count +
                      " components=" + result.component_count + " missing=" + result.missing_script_count +
                      " wrote " + outPath);
            return result.status == "READ" ? 0 : 1;
        }

        [Serializable]
        public class ProbeResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string error;
            public string source;
            public long source_bytes;
            public string copied_to;
            public bool imported_as_scene_asset;
            public bool scratch_removed;
            public bool open_ok;
            public string scene_name;
            public int root_count;
            public int gameobject_count;
            public int component_count;
            public int missing_script_count;
            public List<string> root_names = new List<string>();
            public List<string> gameobject_names = new List<string>();
            public Dictionary<string, int> component_histogram = new Dictionary<string, int>();
            public List<string> entry_components = new List<string>();
            public List<string> log_messages = new List<string>();
        }
    }
}
