// Builds a real standalone Windows x64 Player from this project (G5 infrastructure).
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.BuildPlayer.Run -logFile -
//
// Output: dist/player/manosaba-rebuild.exe (+ result JSON in
// tools/unity-rebuild/out/unity/build_player.json).
//
// `-executeMethod Manosaba.EditorTools.BuildPlayer.RunRelease` builds the same scenes without
// BuildOptions.Development into dist/player-release/manosaba-rebuild.exe (result: build_player_release.json).
// That is the build the shipped game is: Debug.isDebugBuild is false, so DebugPresenter registers no
// IDebugManager and DestroySelfIfReleaseBuild removes the debug canvas - the shipped boot scene starts its
// DebugPresenter with _isDebugMode = true, so a development build shows the game's own debug panel.
//
// Development build on the Mono scripting backend: fast to produce and enough to prove that the
// reconstructed assemblies can be packaged and started.  IL2CPP is the fidelity target for the real
// G5 pass and is tracked separately (the module is installed, the managed stripping/AOT/link
// configuration is not yet defined).
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class BuildPlayer
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/build_player.json";
        private const string PlayerRelativePath = "../dist/player/manosaba-rebuild.exe";
        private const string ReleaseOutputRelativePath = "../tools/unity-rebuild/out/unity/build_player_release.json";
        private const string ReleasePlayerRelativePath = "../dist/player-release/manosaba-rebuild.exe";

        [MenuItem("Manosaba/Build Windows Player (reconstruction smoke)")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        [MenuItem("Manosaba/Build Windows Player (release, as shipped)")]
        public static void RunRelease()
        {
            int code = Execute(true);
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            return Execute(false);
        }

        public static int Execute(bool release)
        {
            var result = new PlayerBuildResult { generated = DateTime.UtcNow.ToString("o"), release = release };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, release ? ReleaseOutputRelativePath : OutputRelativePath));

            try
            {
                string[] scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
                result.scenes = scenes.ToList();
                if (scenes.Length == 0)
                {
                    result.status = "BLOCKED_NO_SCENES";
                    result.error = "EditorBuildSettings has no enabled scene; run ProjectBootstrap first";
                    Write(outPath, result);
                    Debug.LogError("[BuildPlayer] " + result.error);
                    return 2;
                }

                string exe = Path.GetFullPath(Path.Combine(projectRoot, release ? ReleasePlayerRelativePath : PlayerRelativePath));
                Directory.CreateDirectory(Path.GetDirectoryName(exe));

                var options = new BuildPlayerOptions
                {
                    scenes = scenes,
                    locationPathName = exe,
                    target = BuildTarget.StandaloneWindows64,
                    targetGroup = BuildTargetGroup.Standalone,
                    options = release ? BuildOptions.None : BuildOptions.Development,
                };
                result.player_path = exe;

                // The managed linker cannot resolve `System.Runtime.CompilerServices.Unsafe` for the genuine
                // third-party `Assets/Plugins/FastEnum/FastEnum.dll`, and it fails the whole build on it:
                //   Fatal error in Unity CIL Linker -- IL1010: Assembly 'WitchTrials.Game' cannot be loaded due
                //   to failure in processing 'FastEnum' -> Mono.Cecil.AssemblyResolutionException: Failed to
                //   resolve assembly: 'System.Runtime.CompilerServices.Unsafe, Version=4.0.4.0,
                //   PublicKeyToken=b03f5f7f11d50a3a'
                // The reference is real (FastEnum needs it, which is why the editor gates set
                // `validateReferences: 0` for it), so the link step -- not the assembly -- is what has to give.
                // The Mono scripting backend honours ManagedStrippingLevel.Disabled by skipping the linker
                // entirely; IL2CPP, which runs its own link stage, is unaffected by this setting and is still
                // the fidelity target for a later pass.
                NamedBuildTarget namedTarget = NamedBuildTarget.FromBuildTargetGroup(options.targetGroup);
                ManagedStrippingLevel previousStripping = PlayerSettings.GetManagedStrippingLevel(namedTarget);
                result.stripping_level_before = previousStripping.ToString();
                result.stripping_level_applied = ManagedStrippingLevel.Disabled.ToString();
                PlayerSettings.SetManagedStrippingLevel(namedTarget, ManagedStrippingLevel.Disabled);

                BuildReport report = BuildPipeline.BuildPlayer(options);
                result.result = report.summary.result.ToString();
                result.total_size_bytes = (long)report.summary.totalSize;
                result.total_time_seconds = Math.Round(report.summary.totalTime.TotalSeconds, 2);
                result.output_path = report.summary.outputPath;
                try
                {
                    foreach (BuildStep step in report.steps)
                    {
                        if (step.duration.TotalSeconds >= 0.5)
                        {
                            result.steps.Add(step.name + " (" + Math.Round(step.duration.TotalSeconds, 2) + "s)");
                        }
                    }
                }
                catch (Exception stepEx)
                {
                    result.steps.Add("(step list unavailable: " + stepEx.GetType().Name + ")");
                }

                result.status = report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded ? "SUCCEEDED" : "FAILED";
                result.exe_exists = File.Exists(exe);
                if (result.exe_exists) result.exe_bytes = new FileInfo(exe).Length;
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[BuildPlayer] " + ex);
            }

            Write(outPath, result);
            Debug.Log("[BuildPlayer] " + result.status + " result=" + result.result + " player=" + result.player_path);
            return result.status == "SUCCEEDED" ? 0 : 1;
        }

        private static void Write(string path, PlayerBuildResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class PlayerBuildResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public bool release;
            public string error;
            public string result;
            public string player_path;
            public string output_path;
            public bool exe_exists;
            public long exe_bytes;
            public long total_size_bytes;
            public double total_time_seconds;
            public string stripping_level_before;
            public string stripping_level_applied;
            public List<string> scenes = new List<string>();
            public List<string> steps = new List<string>();
        }
    }
}

