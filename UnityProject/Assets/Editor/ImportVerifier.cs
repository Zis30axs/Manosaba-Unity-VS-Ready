// G2 verification: check that every file a13_import_assets.py wrote really became a Unity asset.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.ImportVerifier.Run -logFile -
//
// Reads tools/unity-rebuild/out/unity/a13_imported_paths.txt (one Unity asset path per line), forces
// an AssetDatabase refresh, then reports for each path whether LoadMainAssetAtPath resolves, what the
// main asset type is, and its GUID.  The GUID map is what makes the "close the Editor, reopen, verify
// again" requirement checkable: GUIDs must be identical on the second run, otherwise the import is
// still churning.
//
// Output: tools/unity-rebuild/out/unity/import_verify.json
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class ImportVerifier
    {
        private const string PathsDirectoryRelative = "../tools/unity-rebuild/out/unity";
        private const string PathsPattern = "*_imported_paths*.txt";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/import_verify.json";

        /// <summary>Unload resident assets every N checks (see the loop for why).</summary>
        private const int UnloadInterval = 500;

        [MenuItem("Manosaba/G2 Verify Imported Assets")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string pathsDir = Path.GetFullPath(Path.Combine(projectRoot, PathsDirectoryRelative));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            var result = new VerifyResult { generated = DateTime.UtcNow.ToString("o"), input = pathsDir };

            try
            {
                // Import anything still pending before measuring.
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                AssetDatabase.SaveAssets();

                if (!Directory.Exists(pathsDir))
                {
                    result.status = "BLOCKED_NO_INPUT";
                    result.error = "missing directory " + pathsDir;
                    Write(outPath, result);
                    Debug.LogError("[ImportVerifier] " + result.error);
                    return 2;
                }

                // Every producer of assets writes its own list; the verifier checks the union, so one
                // gate run covers imported leaf assets and converted composites alike.
                string[] listFiles = Directory.GetFiles(pathsDir, PathsPattern);
                result.input_files = listFiles.Select(Path.GetFileName).OrderBy(n => n, StringComparer.Ordinal).ToList();
                if (listFiles.Length == 0)
                {
                    result.status = "BLOCKED_NO_INPUT";
                    result.error = "no " + PathsPattern + " under " + pathsDir;
                    Write(outPath, result);
                    Debug.LogError("[ImportVerifier] " + result.error);
                    return 2;
                }

                var paths = new List<string>();
                using (var sha = System.Security.Cryptography.SHA256.Create())
                {
                    var sb = new StringBuilder();
                    foreach (string listFile in listFiles.OrderBy(f => f, StringComparer.Ordinal))
                    {
                        byte[] content = File.ReadAllBytes(listFile);
                        foreach (byte b in sha.ComputeHash(content)) sb.Append(b.ToString("x2"));
                        foreach (string line in File.ReadAllLines(listFile))
                        {
                            string trimmed = line.Trim();
                            if (trimmed.Length > 0) paths.Add(trimmed);
                        }
                    }
                    result.input_sha256 = sb.ToString();
                }
                string[] distinct = paths.Distinct().ToArray();
                result.requested = distinct.Length;

                int checkedSoFar = 0;
                foreach (string path in distinct)
                {
                    // Loading tens of thousands of assets (906 large textures among them) keeps them
                    // resident and the Editor dies with an access violation rather than a managed
                    // exception -- observed 2026-09-17 on the first run over all 30,749 paths.  Unload
                    // periodically, and log progress so a crash can be localised to a range.
                    if (++checkedSoFar % UnloadInterval == 0)
                    {
                        EditorUtility.UnloadUnusedAssetsImmediate();
                        Debug.Log($"[ImportVerifier] checked {checkedSoFar}/{distinct.Length}");
                    }

                    UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(path);
                    if (asset == null)
                    {
                        result.missing.Add(path);
                        // Sub-asset? The sprite/atlas converters will produce those; report it apart.
                        string[] subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(path)
                            .Where(a => a != null).Select(a => a.name).ToArray();
                        if (subAssets.Length > 0) result.sub_asset_only.Add(path + " [" + string.Join(", ", subAssets) + "]");
                        continue;
                    }

                    string type = asset.GetType().Name;
                    result.found++;
                    result.by_type.Add(type);
                    result.guid_map[path] = AssetDatabase.AssetPathToGUID(path);
                }

                result.status = result.found == result.requested ? "OK" : "INCOMPLETE";
                result.missing_count = result.missing.Count;
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[ImportVerifier] " + ex);
            }

            Write(outPath, result);
            Debug.Log("[ImportVerifier] " + result.status + " found=" + result.found + "/" + result.requested +
                      " missing=" + result.missing_count + " wrote " + outPath);
            return result.status == "OK" ? 0 : 1;
        }

        private static void Write(string path, VerifyResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class VerifyResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string input;
            public string input_sha256;
            public List<string> input_files = new List<string>();
            public string error;
            public int requested;
            public int found;
            public int missing_count;
            public List<string> missing = new List<string>();
            public List<string> sub_asset_only = new List<string>();
            public IntDictionary by_type = new IntDictionary();
            public SerializableDictionary guid_map = new SerializableDictionary();
        }

        /// <summary>JsonUtility cannot serialise Dictionary, so histograms use a key/value pair list.</summary>
        [Serializable]
        public class IntDictionary
        {
            public List<string> keys = new List<string>();
            public List<int> values = new List<int>();

            [NonSerialized] private Dictionary<string, int> _map = new Dictionary<string, int>();

            public void Add(string key)
            {
                _map.TryGetValue(key, out int n);
                _map[key] = n + 1;
                Sync();
            }

            private void Sync()
            {
                keys = _map.Keys.OrderBy(k => k, StringComparer.Ordinal).ToList();
                values = keys.Select(k => _map[k]).ToList();
            }
        }

        /// <summary>JsonUtility cannot serialise Dictionary, and G2 needs both histograms and a GUID map.</summary>
        [Serializable]
        public class SerializableDictionary : ISerializationCallbackReceiver
        {
            [SerializeField] private List<string> keys = new List<string>();
            [SerializeField] private List<string> values = new List<string>();

            [NonSerialized] private Dictionary<string, string> _map = new Dictionary<string, string>();

            public int Count => _map.Count;
            public bool ContainsKey(string key) => _map.ContainsKey(key);
            public bool TryGetValue(string key, out string value) => _map.TryGetValue(key, out value);
            public string this[string key] { get => _map[key]; set => _map[key] = value; }
            public Dictionary<string, string> Map => _map;

            public void OnBeforeSerialize()
            {
                keys = _map.Keys.OrderBy(k => k, StringComparer.Ordinal).ToList();
                values = keys.Select(k => _map[k]).ToList();
            }

            public void OnAfterDeserialize()
            {
                _map = new Dictionary<string, string>();
                for (int i = 0; i < keys.Count && i < values.Count; i++) _map[keys[i]] = values[i];
            }
        }
    }
}
