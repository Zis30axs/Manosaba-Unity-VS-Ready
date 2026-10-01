// Loads TEXT/IMAGE/DATA/SCENARIO cases from the built catalog, plus optional
// MasterData from Resources, in Play Mode.
// Run after tools/content/roundtrip.py prepare + content sync/build. The journal
// supplies exact source hashes, GUIDs, pixel coordinates, and a serialized field.
// No research/shipped-game input is read by this probe.
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Manosaba.EditorTools
{
    public static class ContentRoundTripProbe
    {
        private const string PlanEnv = "MANOSABA_ROUNDTRIP_PLAN";
        private const string Active = "Manosaba.ContentRoundTrip.active";
        private const string Started = "Manosaba.ContentRoundTrip.started";
        private const string Done = "Manosaba.ContentRoundTrip.done";
        private const string Deadline = "Manosaba.ContentRoundTrip.deadline";
        private const string PreviousBuilder = "Manosaba.ContentRoundTrip.previousBuilder";

        [MenuItem("Manosaba/Verify Edited Built Content (Play Mode)")]
        public static void Run()
        {
            try
            {
                JObject plan = ReadPlan();
                var imported = new JArray();
                foreach (JObject item in (JArray)plan["cases"])
                    imported.Add(VerifyImported(item));
                Write("imported_result.json", new JObject { ["status"] = "PASS", ["cases"] = imported });
                SessionState.SetInt(PreviousBuilder, -1);
                if (((JArray)plan["cases"]).Any(item => (string)item["delivery"] != "Resources"))
                {
                    var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                    Require(settings != null, "no Addressables settings");
                    SessionState.SetInt(PreviousBuilder, settings.ActivePlayModeDataBuilderIndex);
                    for (int i = 0; i < settings.DataBuilders.Count; i++)
                    {
                        var builder = settings.DataBuilders[i] as IDataBuilder;
                        if (builder != null && builder.Name.IndexOf("Existing Build", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            settings.ActivePlayModeDataBuilderIndex = i;
                            EditorUtility.SetDirty(settings);
                            AssetDatabase.SaveAssets();
                            break;
                        }
                    }
                }
                // An empty scene isolates the content load from game boot side
                // effects. The existing G4/G5 gates separately exercise boot.
                EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                SessionState.SetBool(Active, true);
                SessionState.SetBool(Started, false);
                SessionState.SetBool(Done, false);
                SessionState.SetFloat(Deadline, (float)EditorApplication.timeSinceStartup + 180f);
                Hook();
                EditorApplication.EnterPlaymode();
            }
            catch (Exception ex)
            {
                Write("runtime_result.json", Failure(ex));
                Debug.LogError("[ContentRoundTripProbe] " + ex);
                if (Application.isBatchMode) EditorApplication.Exit(1);
            }
        }

        // Reimport the restored files in place, including pre-test generated
        // asset differences, without reconverting them from editable sources.
        public static void RestoreImports()
        {
            try
            {
                foreach (JObject item in (JArray)ReadPlan()["cases"])
                    AssetDatabase.ImportAsset((string)item["target"], ImportAssetOptions.ForceUpdate);
                AssetDatabase.SaveAssets();
                Write("restore_import_result.json", new JObject { ["status"] = "PASS" });
                if (Application.isBatchMode) EditorApplication.Exit(0);
            }
            catch (Exception ex)
            {
                Write("restore_import_result.json", Failure(ex));
                Debug.LogError("[ContentRoundTripProbe] " + ex);
                if (Application.isBatchMode) EditorApplication.Exit(1);
            }
        }

        [InitializeOnLoadMethod]
        private static void Resume()
        {
            if (!SessionState.GetBool(Active, false)) return;
            if (!SessionState.GetBool(Done, false)) SessionState.SetBool(Started, false);
            Hook();
        }

        private static void Hook()
        {
            EditorApplication.update -= Tick;
            EditorApplication.update += Tick;
        }

        private static void Tick()
        {
            if (!SessionState.GetBool(Active, false))
            {
                EditorApplication.update -= Tick;
                return;
            }
            if (SessionState.GetBool(Done, false) && !EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                SessionState.SetBool(Active, false);
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                if (settings != null && SessionState.GetInt(PreviousBuilder, -1) >= 0)
                {
                    settings.ActivePlayModeDataBuilderIndex = SessionState.GetInt(PreviousBuilder, settings.ActivePlayModeDataBuilderIndex);
                    EditorUtility.SetDirty(settings);
                    AssetDatabase.SaveAssets();
                }
                string status = (string)JObject.Parse(File.ReadAllText(ResultPath("runtime_result.json")))["status"];
                Debug.Log("[ContentRoundTripProbe] " + status);
                if (Application.isBatchMode) EditorApplication.Exit(status == "PASS" ? 0 : 1);
                return;
            }
            if (EditorApplication.timeSinceStartup > SessionState.GetFloat(Deadline, 0))
            {
                Write("runtime_result.json", Failure(new TimeoutException("built-content probe exceeded 180 seconds")));
                SessionState.SetBool(Done, true);
                if (EditorApplication.isPlaying) EditorApplication.ExitPlaymode();
                return;
            }
            if (!EditorApplication.isPlaying || EditorApplication.isCompiling || SessionState.GetBool(Started, false)) return;
            SessionState.SetBool(Started, true);
            CheckRuntime();
        }

        private static async void CheckRuntime()
        {
            var result = new JObject { ["generated"] = DateTime.UtcNow.ToString("o"), ["status"] = "RUNNING",
                                       ["play_mode"] = true, ["cases"] = new JArray() };
            try
            {
                JObject plan = ReadPlan();
                if (((JArray)plan["cases"]).Any(item => (string)item["delivery"] != "Resources"))
                {
                    var initialized = Addressables.InitializeAsync(false);
                    try
                    {
                        await initialized.Task;
                        Require(initialized.Status == AsyncOperationStatus.Succeeded, "Addressables initialization failed");
                    }
                    finally { Addressables.Release(initialized); }
                }
                foreach (JObject item in (JArray)plan["cases"])
                    ((JArray)result["cases"]).Add(await VerifyRuntime(item));
                result["status"] = "PASS";
                result["imported"] = JObject.Parse(File.ReadAllText(ResultPath("imported_result.json")));
            }
            catch (Exception ex)
            {
                result["status"] = "FAIL";
                result["error"] = ex.ToString();
                Debug.LogError("[ContentRoundTripProbe] " + ex);
            }
            Write("runtime_result.json", result);
            SessionState.SetBool(Done, true);
            EditorApplication.ExitPlaymode();
        }

        private static JObject VerifyImported(JObject item)
        {
            string target = (string)item["target"];
            Require(AssetDatabase.AssetPathToGUID(target) == (string)item["guid"], "GUID changed for " + target);
            string category = (string)item["category"];
            var row = new JObject { ["category"] = category, ["target"] = target, ["guid"] = item["guid"] };
            if (category == "TEXT")
            {
                var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(target);
                Require(asset != null, "no imported TextAsset at " + target);
                row["sha256"] = TextHash(asset);
                Require((string)row["sha256"] == (string)item["expected_sha256"], "imported text is stale");
            }
            else if (category == "IMAGE")
            {
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(target);
                var texture = sprite != null ? sprite.texture : AssetDatabase.LoadAssetAtPath<Texture2D>(target);
                Require(texture != null, "no imported Sprite/Texture2D at " + target);
                row["pixel"] = CheckPixel(texture, sprite, item);
                if (sprite != null)
                {
                    row["pixels_per_unit"] = sprite.pixelsPerUnit;
                    row["pivot"] = new JArray(sprite.pivot.x, sprite.pivot.y);
                    row["border"] = new JArray(sprite.border.x, sprite.border.y, sprite.border.z, sprite.border.w);
                }
            }
            else if ((string)item["delivery"] == "Resources")
            {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(target);
                Require(asset != null, "no imported Resources ScriptableObject at " + target);
                var property = new SerializedObject(asset).FindProperty((string)item["serialized_field"]);
                Require(property != null && property.propertyType == SerializedPropertyType.Float, "MasterData field is not an imported float");
                row["actual"] = property.floatValue;
                row["delivery"] = "Resources";
                Require(Math.Abs(property.floatValue - (double)item["expected"]) < 1e-5, "imported MasterData is stale");
            }
            else
            {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(target);
                Require(asset != null, "no imported ScriptableObject at " + target);
                var property = new SerializedObject(asset).FindProperty((string)item["serialized_field"]);
                Require(property != null && property.propertyType == SerializedPropertyType.String, "data field is not an imported string");
                row["actual"] = property.stringValue;
                Require(property.stringValue == (string)item["expected"], "imported data is stale");
                if (category == "SCENARIO") CheckScenario((Naninovel.Script)asset, item, row);
            }
            row["status"] = "PASS";
            return row;
        }

        private static async Task<JObject> VerifyRuntime(JObject item)
        {
            string category = (string)item["category"];
            string guid = (string)item["guid"];
            if ((string)item["delivery"] == "Resources")
            {
                string key = (string)item["resources_key"];
                var asset = Resources.Load<ScriptableObject>(key);
                Require(asset != null, "Resources.Load did not find " + key);
                string propertyName = (string)item["runtime_property"];
                var property = asset.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                Require(property != null, "runtime MasterData getter missing: " + propertyName);
                double actual = Convert.ToDouble(property.GetValue(asset), CultureInfo.InvariantCulture);
                Require(Math.Abs(actual - (double)item["expected"]) < 1e-5, "runtime MasterData getter is stale");
                return new JObject { ["category"] = category, ["guid"] = guid, ["delivery"] = "Resources",
                                     ["provider"] = "UnityEngine.Resources", ["resources_key"] = key,
                                     ["runtime_property"] = propertyName, ["type"] = asset.GetType().FullName,
                                     ["actual"] = actual, ["status"] = "PASS" };
            }
            Type type = category == "TEXT" ? typeof(TextAsset) : category == "DATA" || category == "SCENARIO" ? typeof(ScriptableObject) :
                        (string)item["asset_type"] == "Sprite" ? typeof(Sprite) : typeof(Texture2D);
            var locations = Addressables.LoadResourceLocationsAsync(guid, type);
            await locations.Task;
            try
            {
                Require(locations.Status == AsyncOperationStatus.Succeeded && locations.Result.Count == 1,
                        "no unique built location for GUID " + guid);
                var location = locations.Result[0];
                Require(location.ProviderId.IndexOf("BundledAssetProvider", StringComparison.Ordinal) >= 0,
                        "probe must consume built bundles; provider was " + location.ProviderId);
                var row = new JObject { ["category"] = category, ["guid"] = guid,
                                        ["provider"] = location.ProviderId, ["internal_id"] = location.InternalId,
                                        ["primary_key"] = location.PrimaryKey };
                // A sprite PNG has both Texture2D and Sprite sub-assets under
                // the same bundle path. Requesting Object can select the
                // texture; use the declared type to verify the actual Sprite.
                AsyncOperationHandle handle;
                if (type == typeof(Sprite)) handle = Addressables.LoadAssetAsync<Sprite>(location);
                else if (type == typeof(Texture2D)) handle = Addressables.LoadAssetAsync<Texture2D>(location);
                else if (type == typeof(TextAsset)) handle = Addressables.LoadAssetAsync<TextAsset>(location);
                else handle = Addressables.LoadAssetAsync<ScriptableObject>(location);
                await handle.Task;
                try
                {
                    Require(handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null,
                            "built asset load failed for " + guid);
                    if (category == "TEXT")
                    {
                        row["sha256"] = TextHash((TextAsset)handle.Result);
                        Require((string)row["sha256"] == (string)item["expected_sha256"], "runtime text is stale");
                    }
                    else if (category == "IMAGE")
                    {
                        var sprite = handle.Result as Sprite;
                        row["pixel"] = CheckPixel(sprite != null ? sprite.texture : (Texture2D)handle.Result, sprite, item);
                    }
                    else if (category == "SCENARIO")
                    {
                        CheckScenario((Naninovel.Script)handle.Result, item, row);
                    }
                    else
                    {
                        object value = handle.Result;
                        foreach (JToken part in (JArray)item["field"])
                        {
                            if (part.Type == JTokenType.Integer) value = ((IList)value)[(int)part];
                            else
                            {
                                FieldInfo field = FindField(value.GetType(), (string)part);
                                Require(field != null, "runtime field missing: " + part);
                                value = field.GetValue(value);
                            }
                        }
                        row["actual"] = Convert.ToString(value, CultureInfo.InvariantCulture);
                        Require((string)row["actual"] == (string)item["expected"], "runtime data is stale");
                    }
                    row["status"] = "PASS";
                    return row;
                }
                finally { Addressables.Release(handle); }
            }
            finally { Addressables.Release(locations); }
        }

        private static FieldInfo FindField(Type type, string name)
        {
            for (; type != null; type = type.BaseType)
            {
                var field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (field != null) return field;
            }
            return null;
        }

        private static void CheckScenario(Naninovel.Script script, JObject item, JObject row)
        {
            // This accessor is the Naninovel runtime's fallback text lookup.
            // Checking it catches JSON copies that never became a live Script.
            row["actual"] = script.TextMap.GetTextOrNull((string)item["text_id"]);
            row["playlist_count"] = script.Playlist.Count;
            Require((string)row["actual"] == (string)item["expected"], "Naninovel runtime text map is stale");
            Require(script.Playlist.Count > 0, "Naninovel playlist graph was lost during conversion");
        }

        private static JArray CheckPixel(Texture2D texture, Sprite sprite, JObject item)
        {
            // GPU readback keeps isReadable and all other importer settings
            // unchanged. Sprite crops can live in an atlas at runtime.
            var old = RenderTexture.active;
            var temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
            var readable = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false, true);
            try
            {
                Graphics.Blit(texture, temporary);
                RenderTexture.active = temporary;
                readable.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
                readable.Apply();
                float normalizedX = (float)item["sample_x"] / (int)item["width"];
                float normalizedY = 1f - (float)item["sample_y"] / (int)item["height"];
                int x, y;
                if (sprite == null)
                {
                    x = Mathf.Clamp((int)(normalizedX * texture.width), 0, texture.width - 1);
                    y = Mathf.Clamp((int)(normalizedY * texture.height), 0, texture.height - 1);
                }
                else
                {
                    Require(!sprite.packed || sprite.packingRotation == SpritePackingRotation.None,
                            "rotated atlas sprite requires a UV probe; no settings were changed");
                    Vector2 offset = sprite.textureRectOffset;
                    Rect rect = sprite.textureRect;
                    x = Mathf.Clamp((int)(rect.x + normalizedX * sprite.rect.width - offset.x), 0, texture.width - 1);
                    y = Mathf.Clamp((int)(rect.y + normalizedY * sprite.rect.height - offset.y), 0, texture.height - 1);
                }
                Color32 actual = readable.GetPixel(x, y);
                int[] channels = { actual.r, actual.g, actual.b, actual.a };
                int tolerance = (int)item["color_tolerance"];
                var expected = (JArray)item["expected_rgba"];
                Require(channels.Select((v, i) => Math.Abs(v - (int)expected[i]) <= tolerance).All(v => v),
                        "image marker missing; actual RGBA=" + string.Join(",", channels));
                return new JArray(channels);
            }
            finally
            {
                RenderTexture.active = old;
                RenderTexture.ReleaseTemporary(temporary);
                UnityEngine.Object.DestroyImmediate(readable);
            }
        }

        private static string TextHash(TextAsset asset)
        {
            string text = Encoding.UTF8.GetString(asset.bytes).TrimStart('\ufeff').Replace("\r\n", "\n");
            using (var digest = SHA256.Create())
                return BitConverter.ToString(digest.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "").ToLowerInvariant();
        }

        private static string RepoRoot => Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;

        private static string PlanPath()
        {
            string value = Environment.GetEnvironmentVariable(PlanEnv);
            Require(!string.IsNullOrEmpty(value), "set " + PlanEnv + " to the round-trip journal");
            string path = Path.GetFullPath(value);
            string root = Path.GetFullPath(RepoRoot) + Path.DirectorySeparatorChar;
            Require(path.StartsWith(root, StringComparison.OrdinalIgnoreCase), "round-trip journal must be inside the workspace");
            return path;
        }

        private static JObject ReadPlan() => JObject.Parse(File.ReadAllText(PlanPath()));
        private static string ResultPath(string name) => Path.Combine(Path.GetDirectoryName(PlanPath()), name);
        private static JObject Failure(Exception ex) => new JObject { ["status"] = "FAIL", ["generated"] = DateTime.UtcNow.ToString("o"), ["error"] = ex.ToString() };
        private static void Require(bool condition, string reason) { if (!condition) throw new InvalidOperationException(reason); }
        private static void Write(string name, JObject result) => File.WriteAllText(ResultPath(name), result.ToString(), new UTF8Encoding(false));
    }
}
