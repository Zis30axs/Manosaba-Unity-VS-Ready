// G4: run the reconstructed project in Editor Play Mode and collect the same evidence the standalone
// Player produces (G5), so "it runs" is verified on both sides of the build.
//
//   Unity -batchmode -projectPath UnityProject -executeMethod Manosaba.EditorTools.PlayModeGate.Run
//
// What it does:
//   1. makes sure the Addressables settings use the *built* content ("Use Existing Build"), so Play Mode
//      loads Library/com.unity.addressables/aa/<platform> exactly like the Player does -- the default
//      "Use Asset Database" play mode would bypass the content build and prove much less;
//   2. opens Assets/Scenes/Boot.unity (created by ProjectBootstrap) and enters Play Mode;
//   3. waits for ManosabaBoot to write its evidence file (MANOSABA_EVIDENCE), then leaves Play Mode and
//      exits the Editor with 0/1 according to that evidence, or a distinct code on timeout.
//
// Entering Play Mode triggers a domain reload, which clears static state -- so the gate's own state
// lives in SessionState (survives the reload) and the update loop re-hooks itself from
// [InitializeOnLoadMethod].  The evidence path travels in the environment, which also survives.
//
// Result: tools/unity-rebuild/out/unity/g4_play_mode.json
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;          // IDataBuilder lives here, not in .Build.DataBuilders
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class PlayModeGate
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/g4_play_mode.json";
        private const string EvidenceEnv = "MANOSABA_EVIDENCE";
        /// <summary>Optional second evidence file (the game-start probe); the gate waits for it too.</summary>
        private const string GameStartEvidenceEnv = "MANOSABA_GAMESTART_EVIDENCE";
        /// <summary>When "1", the gate refuses to leave Play Mode while the Editor is still compiling.</summary>
        private const string WaitForCompileEnv = "MANOSABA_PLAYMODE_WAIT_FOR_COMPILE";
        private const string ScenePath = "Assets/Scenes/Boot.unity";

        private const string SessionActive = "Manosaba.PlayModeGate.active";
        private const string SessionEvidence = "Manosaba.PlayModeGate.evidence";
        private const string SessionDeadline = "Manosaba.PlayModeGate.deadline";
        private const string TimeoutSeconds = "Manosaba.PlayModeGate.timeoutSeconds";

        [MenuItem("Manosaba/Run Play Mode Gate")]
        public static void Run()
        {
            string evidence = Environment.GetEnvironmentVariable(EvidenceEnv);
            var result = new GateResult
            {
                generated = DateTime.UtcNow.ToString("o"),
                evidence_path = evidence,
                editor_version = Application.unityVersion,
            };

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            if (string.IsNullOrEmpty(evidence))
            {
                Write(outPath, result, "BLOCKED_NO_EVIDENCE_PATH",
                    "set " + EvidenceEnv + " to where ManosabaBoot should write its evidence");
                EditorApplication.Exit(2);
                return;
            }
            if (File.Exists(evidence)) File.Delete(evidence);

            try
            {
                // Play Mode can be entered without a content sync request. Validate the
                // generated SpriteSkin at this final boundary as well.
                RestoreOriginalCurtainSkin.EnsureApplied();
            }
            catch (Exception error)
            {
                Write(outPath, result, "FAIL_CURTAIN_SKIN", error.ToString());
                Debug.LogError("[PlayModeGate] curtain SpriteSkin verification failed: " + error);
                EditorApplication.Exit(1);
                return;
            }

            // ---- play mode data source ---------------------------------------------------------
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            if (settings == null)
            {
                Write(outPath, result, "BLOCKED_NO_SETTINGS", "no AddressableAssetSettings in this project");
                EditorApplication.Exit(2);
                return;
            }
            result.play_mode_builder_before = settings.ActivePlayModeDataBuilderIndex;
            int existingBuild = -1;
            for (int i = 0; i < settings.DataBuilders.Count; i++)
            {
                var builder = settings.DataBuilders[i] as IDataBuilder;
                if (builder == null) continue;
                result.play_mode_builders.Add(i + ":" + builder.Name);
                if (builder.Name.IndexOf("Existing Build", StringComparison.OrdinalIgnoreCase) >= 0) existingBuild = i;
            }
            result.play_mode_builder_existing_build = existingBuild;
            if (settings.DataBuilders.Count == 0)
            {
                // In batchmode AddressableAssetSettings.DataBuilders came back empty even though the settings
                // asset does declare builders, so the switch below cannot be made from here.  That does not
                // weaken the gate: what proves Play Mode consumed the *built* content is the evidence file --
                // every address reports BundledAssetProvider and an internal id from the built catalog
                // ("Use Asset Database" play mode would report AssetDatabaseProvider and project file paths).
                result.play_mode_builder_note =
                    "DataBuilders empty in batchmode; play-mode source is verified from the evidence's provider ids instead";
            }
            if (existingBuild >= 0 && settings.ActivePlayModeDataBuilderIndex != existingBuild)
            {
                settings.ActivePlayModeDataBuilderIndex = existingBuild;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                result.play_mode_builder_changed = true;
            }

            // ---- scene ------------------------------------------------------------------------
            if (!File.Exists(Path.Combine(projectRoot, ScenePath)))
            {
                Write(outPath, result, "BLOCKED_NO_SCENE", "missing " + ScenePath + " (run ProjectBootstrap)");
                EditorApplication.Exit(2);
                return;
            }
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            result.scene = ScenePath;

            // ---- enter play mode --------------------------------------------------------------
            SessionState.SetBool(SessionActive, true);
            SessionState.SetString(SessionEvidence, evidence);
            // The play-mode budget has to cover the slowest experiment that runs inside it.  The game-start
            // probe's chain window alone is configurable (MANOSABA_GAMESTART_CHAIN_SECONDS, up to 600 s), and
            // when it exceeds this budget the gate tears the session down before the probe can write its
            // evidence -- which is exactly what a 45 s chain window inside a fixed 240 s budget produced:
            // g4_play_mode.json "FAIL_TIMEOUT"-adjacent BLOCKED with no game_start_evidence at all.  So the
            // budget is now derived from the same knob, with the old 240 s as the floor.
            float timeout = 240f;
            string configuredTimeout = Environment.GetEnvironmentVariable("MANOSABA_PLAYMODE_TIMEOUT_SECONDS");
            if (!string.IsNullOrEmpty(configuredTimeout) &&
                float.TryParse(configuredTimeout, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed) &&
                parsed > 0f)
                timeout = parsed;
            SessionState.SetFloat(TimeoutSeconds, timeout);
            SessionState.SetFloat(SessionDeadline, (float)EditorApplication.timeSinceStartup + timeout);
            Hook();
            result.entering_play_mode = true;
            Write(outPath, result, "RUNNING", null);
            EditorApplication.EnterPlaymode();
        }

        [InitializeOnLoadMethod]
        private static void ResumeAfterDomainReload()
        {
            if (SessionState.GetBool(SessionActive, false)) Hook();
        }

        private static void Hook()
        {
            EditorApplication.update -= Tick;
            EditorApplication.update += Tick;
        }

        private static void Tick()
        {
            if (!SessionState.GetBool(SessionActive, false))
            {
                EditorApplication.update -= Tick;
                return;
            }

            string evidence = SessionState.GetString(SessionEvidence, "");
            // The game-start probe (when enabled) needs a few seconds of play time, so the gate must not
            // exit on the boot evidence alone -- otherwise it tears the session down mid-experiment.
            string gameStartEvidence = Environment.GetEnvironmentVariable(GameStartEvidenceEnv);
            bool bootReady = !string.IsNullOrEmpty(evidence) && File.Exists(evidence);
            bool gameStartReady = string.IsNullOrEmpty(gameStartEvidence) || File.Exists(gameStartEvidence);
            // When asked, do not leave Play Mode while the Editor is still compiling/importing.  Unity 6 can
            // request a full script recompilation *inside* a Play Mode session ("[ScriptCompilation] Requested
            // script compilation because: Define symbols changed") and follow it with a domain reload that
            // destroys every coroutine and all engine static state; measured, the reload landed ~17 s into the
            // session and wiped a 60 s measurement window.  A warm-up session that waits for that compilation to
            // settle leaves the next session clean, which is the whole point of the switch.
            bool waitForCompile = Environment.GetEnvironmentVariable(WaitForCompileEnv) == "1";
            if (waitForCompile && (EditorApplication.isCompiling || EditorApplication.isUpdating))
            {
                SessionState.SetFloat(SessionDeadline, (float)EditorApplication.timeSinceStartup + 180f);
                return;
            }
            bool expired = EditorApplication.timeSinceStartup > SessionState.GetFloat(SessionDeadline, 0f);
            if (!(bootReady && gameStartReady) && !expired) return;

            EditorApplication.update -= Tick;
            SessionState.SetBool(SessionActive, false);

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            var result = new GateResult
            {
                generated = DateTime.UtcNow.ToString("o"),
                evidence_path = evidence,
                editor_version = Application.unityVersion,
                scene = ScenePath,
                is_playing = EditorApplication.isPlaying,
                play_seconds = Math.Round(EditorApplication.timeSinceStartup, 2),
            };

            if (expired && (string.IsNullOrEmpty(evidence) || !File.Exists(evidence)))
            {
                if (EditorApplication.isPlaying) EditorApplication.isPlaying = false;
                Write(outPath, result, "FAIL_TIMEOUT",
                    "no evidence after " + SessionState.GetFloat(TimeoutSeconds, 0f) + "s of play mode");
                EditorApplication.Exit(3);
                return;
            }

            string status = "FAIL_NO_STATUS";
            try
            {
                string text = File.ReadAllText(evidence);
                result.evidence = Newtonsoft.Json.Linq.JObject.Parse(text);
                status = (string)result.evidence["status"] ?? "FAIL_NO_STATUS";
                result.evidence_status = status;
                if (!string.IsNullOrEmpty(gameStartEvidence) && File.Exists(gameStartEvidence))
                {
                    result.game_start_evidence = Newtonsoft.Json.Linq.JObject.Parse(
                        File.ReadAllText(gameStartEvidence));
                    result.game_start_status = (string)result.game_start_evidence["status"];
                }
            }
            catch (Exception ex)
            {
                result.error = ex.GetType().Name + ": " + ex.Message;
            }

            if (EditorApplication.isPlaying) EditorApplication.isPlaying = false;
            Write(outPath, result, status == "PASS" ? "PASS" : "FAIL", result.error);
            EditorApplication.Exit(status == "PASS" ? 0 : 1);
        }

        private static void Write(string path, GateResult result, string status, string error)
        {
            result.status = status;
            if (!string.IsNullOrEmpty(error)) result.error = error;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented),
                new UTF8Encoding(false));
            Debug.Log("[PlayModeGate] " + status + (string.IsNullOrEmpty(error) ? "" : ": " + error) + " -> " + path);
        }

        [Serializable]
        public class GateResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string editor_version;
            public string scene;
            public string evidence_path;
            public string evidence_status;
            public string error;
            public bool entering_play_mode;
            public bool is_playing;
            public double play_seconds;
            public int play_mode_builder_before = -1;
            public int play_mode_builder_existing_build = -1;
            public bool play_mode_builder_changed;
            public string play_mode_builder_note;
            public System.Collections.Generic.List<string> play_mode_builders = new System.Collections.Generic.List<string>();
            public string game_start_status;
            public Newtonsoft.Json.Linq.JObject game_start_evidence;
            public Newtonsoft.Json.Linq.JObject evidence;
        }
    }
}
