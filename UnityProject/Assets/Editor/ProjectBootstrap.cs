// Creates the runnable skeleton of the reconstruction: a Boot scene registered in the build
// settings, holding the integration-layer boot component.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.ProjectBootstrap.Run -logFile -
//
// Writes tools/unity-rebuild/out/unity/bootstrap_result.json and exits 0 on success.
//
// The scene is authored *by Unity* (EditorSceneManager) rather than by hand: a hand-written .unity
// file would have to guess every serialised field of every built-in component.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Manosaba.Integration;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manosaba.EditorTools
{
    public static class ProjectBootstrap
    {
        private const string ScenePath = "Assets/Scenes/Boot.unity";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/bootstrap_result.json";

        [MenuItem("Manosaba/Bootstrap Runnable Project")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            var result = new BootstrapResult { generated = DateTime.UtcNow.ToString("o") };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            try
            {
                // --- scene -----------------------------------------------------------------
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(ScenePath)));
                Scene scene = File.Exists(ScenePath)
                    ? EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single)
                    : EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                result.scene_created = !File.Exists(ScenePath) || scene.name != "Boot";

                GameObject boot = scene.GetRootGameObjects().FirstOrDefault(go => go.name == "ManosabaBoot");
                if (boot == null)
                {
                    boot = new GameObject("ManosabaBoot");
                    result.boot_object_created = true;
                }
                if (boot.GetComponent<ManosabaBoot>() == null)
                {
                    boot.AddComponent<ManosabaBoot>();
                    result.boot_component_added = true;
                }

                // --- cameras: a URP scene wants the additional camera data component ---------
                foreach (Camera camera in scene.GetRootGameObjects()
                             .SelectMany(go => go.GetComponentsInChildren<Camera>(true)))
                {
                    result.cameras.Add(camera.name);
                }

                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene, ScenePath);
                AssetDatabase.SaveAssets();

                // --- build settings ---------------------------------------------------------
                var scenes = EditorBuildSettings.scenes.ToList();
                if (!scenes.Any(s => s.path == ScenePath))
                {
                    scenes.Insert(0, new EditorBuildSettingsScene(ScenePath, true));
                    EditorBuildSettings.scenes = scenes.ToArray();
                    result.build_settings_updated = true;
                }
                result.build_scenes = EditorBuildSettings.scenes
                    .Select(s => (s.enabled ? "+" : "-") + s.path).ToList();

                // --- TextMeshPro essential resources -----------------------------------------
                // TMPro's global settings live in a Resources asset that the shipped project had and this rebuild
                // did not; without it every TMP_Text throws on `LoadDefaultSettings`.  Only PROBED here: the import
                // itself is asynchronous and needs an Editor that is allowed to keep running, so the gates run
                // `TmpEssentialsSetup.Run` (without -quit) as its own step before this bootstrap.
                TmpEssentialsSetup.Result tmp = TmpEssentialsSetup.Probe();
                result.tmp_essentials_status = tmp.status;
                result.tmp_essentials_action = tmp.action;
                result.tmp_essentials_error = tmp.error;

                result.status = "OK";
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[ProjectBootstrap] " + ex);
            }

            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            Debug.Log("[ProjectBootstrap] " + result.status + " scene=" + ScenePath + " wrote " + outPath);
            return result.status == "OK" ? 0 : 1;
        }

        [Serializable]
        public class BootstrapResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string error;
            public bool scene_created;
            public bool boot_object_created;
            public bool boot_component_added;
            public bool build_settings_updated;
            public string tmp_essentials_status;
            public string tmp_essentials_action;
            public string tmp_essentials_error;
            public List<string> cameras = new List<string>();
            public List<string> build_scenes = new List<string>();
        }
    }
}
