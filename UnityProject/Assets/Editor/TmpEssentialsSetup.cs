// Imports TextMeshPro's "Essential Resources" into this project when they are missing.
//
// Why this is part of the reconstruction and not a convenience: the shipped game's project contained TMP's
// essential resources (they are created the first time TextMeshPro is used), and this rebuild does not -- a
// search of Assets/ finds no `TMP Settings.asset` and no `Assets/TextMesh Pro/` at all.  TMPro reads its
// global settings through `TMP_Settings.instance`, which resolves `Resources.Load<TMP_Settings>("TMP Settings")`
// and returns null when the asset is absent, so every `TMP_Text.LoadDefaultSettings()` throws
//     NullReferenceException
//       TMPro.TMP_Settings.get_autoSizeTextContainer ()   [TMP_Settings.cs:240]
//       <- TMPro.TMP_Text.LoadDefaultSettings ()          [TMP_Text.cs:5919]
// Measured in the standalone Player: 28 such exceptions before the engine built its UI hierarchy, and 330 after
// the engine instantiated its UIs (one per TMP_Text).  Those ~5400 UI objects are what the title screen and the
// ADV views are made of, so this sits on the critical path to "playable", not on the cosmetic edge.
//
// TWO ENTRY POINTS, because `AssetDatabase.ImportPackage` is asynchronous and `-quit` would kill it:
//   * `Probe()`  - pure report, no side effects; called from ProjectBootstrap so every gate records TMP state.
//   * `Run()`    - performs the import and WAITS for it.  It must be invoked WITHOUT `-quit`: it registers an
//                  `EditorApplication.update` hook, polls for the settings asset, writes its result and only
//                  then calls `EditorApplication.Exit`.  An earlier revision called `ImportPackage` and relied on
//                  `AssetDatabase.Refresh(ForceSynchronousImport)`, and the measured outcome was
//                  `status=FAILED action=imported ... still does not exist` -- the Editor exited first.
//
//   Unity -batchmode -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.TmpEssentialsSetup.Run -logFile -        # no -quit
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class TmpEssentialsSetup
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/tmp_essentials.json";
        /// <summary>The asset TMPro resolves at runtime; its absence is the condition this tool repairs.</summary>
        private const string SettingsAssetPath = "Assets/TextMesh Pro/Resources/TMP Settings.asset";
        private const string PackageFile = "Package Resources/TMP Essential Resources.unitypackage";
        /// <summary>Generous but bounded: the package is ~0.9 MB of fonts/shaders/sprites.</summary>
        private const double ImportTimeoutSeconds = 180.0;

        private static Result pending;
        private static double pendingDeadline;

        [MenuItem("Manosaba/Import TMP Essential Resources (if missing)")]
        public static void Run()
        {
            if (File.Exists(SettingsAssetPath))
            {
                Finish(new Result { status = "OK", action = "already present", settings_before = true, settings_after = true });
                return;
            }

            var result = new Result { generated = DateTime.UtcNow.ToString("o") };
            result.settings_before = false;
            try
            {
                // The package cache folder carries the resolved package version in its name, so it is looked up
                // rather than hard-coded.
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                string cacheRoot = Path.Combine(projectRoot, "Library", "PackageCache");
                string source = Directory.Exists(cacheRoot)
                    ? Directory.GetDirectories(cacheRoot, "com.unity.ugui*")
                        .Select(d => Path.Combine(d, PackageFile))
                        .FirstOrDefault(File.Exists)
                    : null;
                result.package_source = source;
                if (source == null)
                {
                    result.status = "BLOCKED";
                    result.error = "TMP Essential Resources.unitypackage not found under Library/PackageCache/com.unity.ugui*";
                    Finish(result);
                    return;
                }
                result.bytes = new FileInfo(source).Length;
                Debug.Log("[TmpEssentialsSetup] importing " + source + " (" + result.bytes + " bytes)");
                AssetDatabase.ImportPackage(source, false);
                result.action = "import started";
                result.status = "IMPORTING";
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[TmpEssentialsSetup] " + ex);
                Finish(result);
                return;
            }

            // ImportPackage is asynchronous: it kicks off an import that completes on a later editor update, so the
            // result can only be observed from the update loop.  `-quit` must therefore NOT be passed to this
            // method's invocation; the exit happens here instead.
            pending = result;
            pendingDeadline = EditorApplication.timeSinceStartup + ImportTimeoutSeconds;
            EditorApplication.update -= Tick;
            EditorApplication.update += Tick;
        }

        private static void Tick()
        {
            if (pending == null)
            {
                EditorApplication.update -= Tick;
                return;
            }
            if (File.Exists(SettingsAssetPath))
            {
                pending.action = "imported";
                pending.status = "OK";
                pending.settings_after = true;
                Finish(pending);
                return;
            }
            if (EditorApplication.timeSinceStartup > pendingDeadline)
            {
                pending.status = "FAILED";
                pending.error = "import did not produce " + SettingsAssetPath + " within " +
                                ImportTimeoutSeconds + "s";
                Finish(pending);
            }
        }

        private static void Finish(Result result)
        {
            EditorApplication.update -= Tick;
            pending = null;
            try
            {
                result.settings_after = File.Exists(SettingsAssetPath);
                if (result.status == "OK" && !result.settings_after)
                {
                    result.status = "FAILED";
                    result.error = "import completed but " + SettingsAssetPath + " still does not exist";
                }
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
                Debug.Log("[TmpEssentialsSetup] " + result.status + " action=" + result.action +
                          " settings=" + result.settings_after + " -> " + outPath);
            }
            catch (Exception ex)
            {
                Debug.LogError("[TmpEssentialsSetup] could not write result: " + ex);
                result.status = "ERROR";
                result.error = ex.ToString();
            }
            if (Application.isBatchMode) EditorApplication.Exit(result.status == "OK" ? 0 : 1);
        }

        /// <summary>
        /// Pure report, no side effects, never throws: called from ProjectBootstrap so every gate records the TMP
        /// state next to its own result without the bootstrap having to wait for an asynchronous import.
        /// </summary>
        public static Result Probe()
        {
            var result = new Result { generated = DateTime.UtcNow.ToString("o") };
            try
            {
                result.settings_before = File.Exists(SettingsAssetPath);
                result.settings_after = result.settings_before;
                result.status = result.settings_before ? "OK" : "MISSING";
                result.action = result.settings_before ? "present" : "run TmpEssentialsSetup.Run to import";
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
            }
            return result;
        }

        [Serializable]
        public class Result
        {
            public string status = "NOT_RUN";
            public string generated;
            public string action;
            public string error;
            public bool settings_before;
            public bool settings_after;
            public string package_source;
            public long bytes;
        }
    }
}
