using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.U2D;

namespace Manosaba.EditorTools
{
    /// <summary>
    /// Restores and verifies the Sprite Editor mesh that was present in the shipped bundle.
    /// GameContent is deliberately generated and ignored by Git, so this tracked recipe is
    /// the source of truth. Build/sync gates call EnsureApplied instead of relying on a
    /// hand-edited generated .meta file.
    /// </summary>
    [InitializeOnLoad]
    public static class RestoreOriginalCurtainSkin
    {
        private const int DataVersion = 1;
        private const string DataRelativePath = "Assets/Editor/OriginalCurtainSpriteData.json";
        private const string ReportRelativePath = "../work/tmp/curtain_skin_import.report.json";
        private static readonly string[] Names =
            { "Curtain_Open_L", "Curtain_Open_R", "Curtain_Close_L", "Curtain_Close_R", "Curtain_Top", "Curtain_Top_Ribon" };
        private static int autoAttempts;
        private static bool playGuard;

        static RestoreOriginalCurtainSkin()
        {
            // This is a convenience for an already-open editor. The authoritative callers
            // are ContentSyncBridge, AddressablesBuild, BuildPlayer, and PlayModeGate.
            EditorApplication.delayCall += AutoEnsure;
            EditorApplication.playModeStateChanged += EnsureBeforePlay;
        }

        [MenuItem("Manosaba/Restore Original Curtain Sprite Skin")]
        public static void Run()
        {
            int code = 0;
            try
            {
                EnsureApplied();
            }
            catch (Exception error)
            {
                WriteFailure(error);
                Debug.LogException(error);
                code = 1;
            }
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        /// <summary>Apply the tracked recipe and fail if Unity cannot read back the exact data.</summary>
        public static JObject EnsureApplied()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string dataPath = Path.GetFullPath(Path.Combine(projectRoot, DataRelativePath));
            if (!File.Exists(dataPath))
                throw new FileNotFoundException("Tracked curtain SpriteSkin recipe is missing", dataPath);

            JObject all = JObject.Parse(File.ReadAllText(dataPath));
            var report = new JObject
            {
                ["data"] = dataPath,
                ["data_version"] = DataVersion,
                ["data_sha256"] = HashFile(dataPath),
                ["assets"] = new JArray()
            };

            var factories = new SpriteDataProviderFactories();
            factories.Init();
            string assetRoot = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/Spawn/Reenactment/Ch01_E01/Ch01_E01_S01/Reenact_Ch01_E01_S01.sprites/";
            foreach (string name in Names)
            {
                JObject source = all[name] as JObject;
                if (source == null) throw new InvalidDataException("Sprite missing from tracked recipe: " + name);
                string assetPath = assetRoot + name + ".png";
                JObject item = ApplyAndVerify(factories, assetPath, source);
                ((JArray)report["assets"]).Add(item);
            }

            report["status"] = "OK";
            WriteReport(projectRoot, report);
            Debug.Log("[RestoreOriginalCurtainSkin] verified " + Names.Length + " curtain sprites; recipe " + report["data_sha256"]);
            return report;
        }

        private static void AutoEnsure()
        {
            try
            {
                EnsureApplied();
                autoAttempts = 0;
            }
            catch (Exception error)
            {
                // A fresh generated tree may still be importing when the first delayCall runs.
                // Retry a few times, while keeping the explicit build/sync gates authoritative.
                autoAttempts++;
                Debug.LogWarning("[RestoreOriginalCurtainSkin] startup verification deferred (" + autoAttempts + "/4): " + error.Message);
                if (autoAttempts < 4) EditorApplication.delayCall += AutoEnsure;
            }
        }

        private static void EnsureBeforePlay(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode || playGuard) return;
            playGuard = true;
            try
            {
                EnsureApplied();
            }
            catch (Exception error)
            {
                WriteFailure(error);
                Debug.LogError("[RestoreOriginalCurtainSkin] Play Mode blocked: " + error);
                EditorApplication.isPlaying = false;
            }
            finally
            {
                playGuard = false;
            }
        }

        private static JObject ApplyAndVerify(SpriteDataProviderFactories factories, string assetPath, JObject source)
        {
            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null) throw new InvalidDataException("Texture importer missing: " + assetPath);
            var provider = factories.GetSpriteEditorDataProviderFromObject(importer);
            provider.InitSpriteEditorDataProvider();
            SpriteRect[] rects = provider.GetSpriteRects();
            if (rects == null || rects.Length != 1)
                throw new InvalidDataException(assetPath + " has " + (rects == null ? 0 : rects.Length) + " sprite rects");
            GUID spriteId = rects[0].spriteID;
            var bonesProvider = provider.GetDataProvider<ISpriteBoneDataProvider>();
            var meshProvider = provider.GetDataProvider<ISpriteMeshDataProvider>();
            if (bonesProvider == null || meshProvider == null)
                throw new InvalidDataException("Sprite skin providers missing: " + assetPath);

            List<SpriteBone> expectedBones = ParseBones((JArray)source["spriteBone"]);
            Vertex2DMetaData[] expectedVertices = ParseVertices((JArray)source["vertices"]);
            int[] expectedIndices = ParseIndices((string)source["indices"]);
            bool repaired = false;
            if (!Matches(source, expectedBones, expectedVertices, expectedIndices, bonesProvider.GetBones(spriteId),
                         meshProvider.GetVertices(spriteId), meshProvider.GetIndices(spriteId)))
            {
                bonesProvider.SetBones(spriteId, expectedBones);
                meshProvider.SetVertices(spriteId, expectedVertices);
                meshProvider.SetIndices(spriteId, expectedIndices);
                meshProvider.SetEdges(spriteId, Array.Empty<Vector2Int>());
                var settings = new TextureImporterSettings();
                importer.ReadTextureSettings(settings);
                settings.spriteMeshType = SpriteMeshType.Tight;
                importer.SetTextureSettings(settings);
                provider.Apply();
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
                repaired = true;
            }

            importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            provider = factories.GetSpriteEditorDataProviderFromObject(importer);
            provider.InitSpriteEditorDataProvider();
            rects = provider.GetSpriteRects();
            if (rects == null || rects.Length != 1) throw new InvalidDataException(assetPath + " readback has invalid sprite rects");
            spriteId = rects[0].spriteID;
            bonesProvider = provider.GetDataProvider<ISpriteBoneDataProvider>();
            meshProvider = provider.GetDataProvider<ISpriteMeshDataProvider>();
            List<SpriteBone> actualBones = bonesProvider.GetBones(spriteId);
            Vertex2DMetaData[] actualVertices = meshProvider.GetVertices(spriteId);
            int[] actualIndices = meshProvider.GetIndices(spriteId);
            if (!Matches(source, expectedBones, expectedVertices, expectedIndices, actualBones, actualVertices, actualIndices))
                throw new InvalidDataException("Sprite skin readback mismatch: " + assetPath);

            return new JObject
            {
                ["asset"] = assetPath,
                ["vertices"] = actualVertices.Length,
                ["indices"] = actualIndices.Length,
                ["bones"] = actualBones.Count,
                ["weights"] = CountWeightedVertices(actualVertices),
                ["repaired"] = repaired
            };
        }

        private static bool Matches(JObject source, List<SpriteBone> expectedBones, Vertex2DMetaData[] expectedVertices,
                                    int[] expectedIndices, List<SpriteBone> actualBones,
                                    Vertex2DMetaData[] actualVertices, int[] actualIndices)
        {
            if (actualBones == null || actualVertices == null || actualIndices == null ||
                actualBones.Count != expectedBones.Count || actualVertices.Length != expectedVertices.Length ||
                actualIndices.Length != expectedIndices.Length)
                return false;
            for (int i = 0; i < expectedIndices.Length; i++)
                if (actualIndices[i] != expectedIndices[i]) return false;
            for (int i = 0; i < expectedVertices.Length; i++)
            {
                Vertex2DMetaData expected = expectedVertices[i];
                Vertex2DMetaData actual = actualVertices[i];
                if (!Near(actual.position.x, expected.position.x) || !Near(actual.position.y, expected.position.y) ||
                    !Matches(actual.boneWeight, expected.boneWeight)) return false;
            }
            JArray sourceBones = (JArray)source["spriteBone"];
            for (int i = 0; i < expectedBones.Count; i++)
            {
                SpriteBone expected = expectedBones[i];
                SpriteBone actual = actualBones[i];
                if (actual.name != expected.name || actual.guid != expected.guid || actual.parentId != expected.parentId ||
                    !Near(actual.length, expected.length) || !Near(actual.position.x, expected.position.x) ||
                    !Near(actual.position.y, expected.position.y) || !Near(actual.position.z, expected.position.z) ||
                    !Near(actual.rotation.x, expected.rotation.x) || !Near(actual.rotation.y, expected.rotation.y) ||
                    !Near(actual.rotation.z, expected.rotation.z) || !Near(actual.rotation.w, expected.rotation.w))
                    return false;
                // Keep the JSON bone fields in the comparison so a malformed recipe cannot pass
                // merely because Unity returned the same default values.
                JObject json = (JObject)sourceBones[i];
                JObject position = (JObject)json["position"];
                JObject rotation = (JObject)json["rotation"];
                if (!Near(actual.position.x, (float)position["x"]) || !Near(actual.position.y, (float)position["y"]) ||
                    !Near(actual.position.z, (float)position["z"]) || !Near(actual.rotation.x, (float)rotation["x"]) ||
                    !Near(actual.rotation.y, (float)rotation["y"]) || !Near(actual.rotation.z, (float)rotation["z"]) ||
                    !Near(actual.rotation.w, (float)rotation["w"])) return false;
            }
            return true;
        }

        private static bool Matches(BoneWeight actual, BoneWeight expected)
        {
            return actual.boneIndex0 == expected.boneIndex0 && actual.boneIndex1 == expected.boneIndex1 &&
                   actual.boneIndex2 == expected.boneIndex2 && actual.boneIndex3 == expected.boneIndex3 &&
                   Near(actual.weight0, expected.weight0) && Near(actual.weight1, expected.weight1) &&
                   Near(actual.weight2, expected.weight2) && Near(actual.weight3, expected.weight3);
        }

        private static int CountWeightedVertices(Vertex2DMetaData[] vertices)
        {
            int count = 0;
            foreach (Vertex2DMetaData vertex in vertices)
                if (vertex.boneWeight.weight0 > 0 || vertex.boneWeight.weight1 > 0 ||
                    vertex.boneWeight.weight2 > 0 || vertex.boneWeight.weight3 > 0) count++;
            return count;
        }

        private static bool Near(float actual, float expected) { return Mathf.Abs(actual - expected) <= 0.00001f; }

        private static List<SpriteBone> ParseBones(JArray values)
        {
            if (values == null) throw new InvalidDataException("spriteBone is missing");
            var result = new List<SpriteBone>(values.Count);
            foreach (JObject bone in values)
            {
                JObject position = (JObject)bone["position"];
                JObject rotation = (JObject)bone["rotation"];
                uint rgba = (uint)bone["rgba"];
                result.Add(new SpriteBone
                {
                    name = (string)bone["name"], guid = (string)bone["guid"], parentId = (int)bone["parentId"],
                    length = (float)bone["length"], position = new Vector3((float)position["x"], (float)position["y"], (float)position["z"]),
                    rotation = new Quaternion((float)rotation["x"], (float)rotation["y"], (float)rotation["z"], (float)rotation["w"]),
                    color = new Color32((byte)(rgba >> 24), (byte)(rgba >> 16), (byte)(rgba >> 8), (byte)rgba)
                });
            }
            return result;
        }

        private static Vertex2DMetaData[] ParseVertices(JArray values)
        {
            if (values == null) throw new InvalidDataException("vertices are missing");
            var result = new Vertex2DMetaData[values.Count];
            for (int i = 0; i < result.Length; i++)
            {
                JObject vertex = (JObject)values[i];
                JObject position = (JObject)vertex["position"];
                JObject weight = (JObject)vertex["boneWeight"];
                result[i] = new Vertex2DMetaData
                {
                    position = new Vector2((float)position["x"], (float)position["y"]),
                    boneWeight = new BoneWeight
                    {
                        weight0 = (float)weight["weight0"], weight1 = (float)weight["weight1"],
                        weight2 = (float)weight["weight2"], weight3 = (float)weight["weight3"],
                        boneIndex0 = (int)weight["boneIndex0"], boneIndex1 = (int)weight["boneIndex1"],
                        boneIndex2 = (int)weight["boneIndex2"], boneIndex3 = (int)weight["boneIndex3"]
                    }
                };
            }
            return result;
        }

        private static int[] ParseIndices(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length % 8 != 0) throw new InvalidDataException("Invalid index buffer length");
            var result = new int[hex.Length / 8];
            for (int i = 0; i < result.Length; i++)
            {
                uint value = 0;
                for (int b = 0; b < 4; b++)
                    value |= (uint)byte.Parse(hex.Substring(i * 8 + b * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) << (8 * b);
                result[i] = checked((int)value);
            }
            return result;
        }

        private static string HashFile(string path)
        {
            using (var hash = SHA256.Create()) using (var stream = File.OpenRead(path))
                return BitConverter.ToString(hash.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
        }

        private static string ReportPath(string projectRoot)
        {
            return Path.GetFullPath(Path.Combine(projectRoot, ReportRelativePath));
        }

        private static void WriteReport(string projectRoot, JObject report)
        {
            string path = ReportPath(projectRoot);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, report.ToString(), new System.Text.UTF8Encoding(false));
        }

        private static void WriteFailure(Exception error)
        {
            try
            {
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                var report = new JObject { ["data_version"] = DataVersion, ["status"] = "ERROR", ["error"] = error.ToString() };
                WriteReport(projectRoot, report);
            }
            catch (Exception reportError) { Debug.LogError("[RestoreOriginalCurtainSkin] could not write failure report: " + reportError); }
        }
    }
}
