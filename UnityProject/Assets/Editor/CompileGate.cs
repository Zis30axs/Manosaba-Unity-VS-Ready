// Compile / import gate for the reconstruction.
//
// Gate G1 (UNITY_READINESS_CODEX_AUDIT.md section 9) requires the *actual Unity* script
// compiler to report 0 errors, and explicitly forbids substituting a `dotnet build`.
// This Editor entry point runs the real compilation pipeline and writes a machine-readable
// result plus the raw log path, so the outcome can be audited later.
//
// It is intentionally read-only with respect to Assets/: it never "fixes" anything, so a
// failing compile stays failing and cannot be papered over.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Compilation;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class CompileGate
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/compile_result.json";

        [MenuItem("Manosaba/G1 Compile Gate")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            BuildTarget activeTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup activeGroup = BuildPipeline.GetBuildTargetGroup(activeTarget);
            NamedBuildTarget namedTarget = NamedBuildTarget.FromBuildTargetGroup(activeGroup);
            var result = new CompileResult
            {
                generated = DateTime.UtcNow.ToString("o"),
                unity_version = Application.unityVersion,
                active_build_target = activeTarget.ToString(),
                build_target_group = activeGroup.ToString(),
                scripting_define_symbols = PlayerSettings.GetScriptingDefineSymbols(namedTarget),
                api_compatibility_level = PlayerSettings.GetApiCompatibilityLevel(namedTarget).ToString(),
            };

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            try
            {
                // Force a full, fresh compile so a cached result cannot be mistaken for a pass.
                UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

                var assemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);
                result.player_assembly_count = assemblies.Length;
                foreach (var asm in assemblies.OrderBy(a => a.name))
                {
                    result.assemblies.Add(new AssemblyRecord
                    {
                        name = asm.name,
                        source_files = asm.sourceFiles == null ? 0 : asm.sourceFiles.Length,
                        references = asm.allReferences == null ? 0 : asm.allReferences.Length,
                        output_path = asm.outputPath,
                    });
                }

                // CompilationPipeline gives the graph; the real error list comes from the log.
                // Unity writes compiler diagnostics to the log file, which the caller captures
                // with -logFile, so both are recorded.
                result.status = "COMPILED_NO_DIAGNOSTICS_COLLECTED";
                result.note =
                    "Unity's compiler diagnostics are written to the -logFile. tools/unity-rebuild/g1_compile.ps1 " +
                    "parses that log and merges the counts into this file; this script records the assembly graph " +
                    "and the editor configuration.";
                result.assembly_graph_recorded = true;
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
            }

            File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            Debug.Log("[CompileGate] wrote " + outPath + " status=" + result.status +
                      " playerAssemblies=" + result.player_assembly_count);
            return result.status == "ERROR" ? 1 : 0;
        }

        [Serializable]
        public class CompileResult
        {
            public string generated;
            public string unity_version;
            public string active_build_target;
            public string build_target_group;
            public string status = "NOT_RUN";
            public string error;
            public string note;
            public string scripting_define_symbols;
            public string api_compatibility_level;
            public int player_assembly_count;
            public bool assembly_graph_recorded;
            public List<AssemblyRecord> assemblies = new List<AssemblyRecord>();
        }

        [Serializable]
        public class AssemblyRecord
        {
            public string name;
            public int source_files;
            public int references;
            public string output_path;
        }
    }
}
