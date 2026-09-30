// Restores local game font metadata, never downloads/relicenses proprietary assets.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.LowLevel;
using Object = UnityEngine.Object;

namespace Manosaba.EditorTools
{
    public static class RuntimeResourceRepair
    {
        public const string GeneratedRoot = "Assets/GameContent/RuntimeFonts";
        private const string GroupName = "Manosaba.RuntimeFonts";
        private static readonly string[] Names = { "TsukushiMincho", "SourceHanSerifSC", "SourceHanSansKR" };
        // Stable regeneration identities; group/TMP settings are tracked, font binaries are local-only.
        private static readonly string[] FontGuids = { "f9f75bb714f6db54fa3ce798316c9556", "94b38b030d93a5b4cac2545c4c72bdf7", "73bb5401af303c7498bc1bdbde28c2f9" };
        private static readonly string[] Samples = { "魔法少女ノ魔女裁判 日本語 あいうえお", "魔法少女的魔女审判 简体中文", "마법소녀의 마녀재판 한국어" };
        private static readonly string[] ShaderNames = { "UI/TrueShadow-Normal", "UI/TrueShadow-Additive", "UI/TrueShadow-Screen", "UI/TrueShadow-Multiply", "Hidden/TrueShadow/Generate", "Hidden/TrueShadow/GenerateHQ", "Hidden/TrueShadow/Cutout", "Hidden/TrueShadow/ImprintPostProcess", "Hidden/TrueShadow/PostProcess" };
        private static string Repo => Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
        private static string FontPath(string name) => GeneratedRoot + "/" + name + "/" + name + ".asset";
        private static string Key(string name) => "Naninovel/Fonts/" + name + "/Assets/" + name;

        [MenuItem("Manosaba/Repair TrueShadow and TMP Resources")]
        public static void Run()
        {
            var report = new JObject { ["generated"] = DateTime.UtcNow.ToString("o"), ["mode"] = "REBUILT_FROM_LOCAL_ORIGINAL_FONT_AND_METADATA" };
            int code = 1;
            try
            {
                // Validate all inputs before changing any generated assets.
                foreach (string name in Names)
                {
                    string folder = Path.Combine(Repo, "res/Assets/#WitchTrials/Fonts", name);
                    if (!File.Exists(Path.Combine(folder, name + ".asset.json"))) throw new FileNotFoundException("Missing extracted TMP metadata", folder);
                    if (AssetDatabase.LoadAssetAtPath<Font>("Assets/GameContent/#WitchTrials/Fonts/" + name + "/" + name + ".otf") == null)
                        throw new FileNotFoundException("Import the locally extracted OTF first", name);
                    var data = JObject.Parse(File.ReadAllText(Path.Combine(folder, name + ".asset.json")));
                    // This recovery path intentionally does NOT discard a nonempty original atlas/table.
                    if ((int)data["m_AtlasPopulationMode"] != 1 || ((JArray)data["m_GlyphTable"]).Count != 0 || ((JArray)data["m_CharacterTable"]).Count != 0)
                        throw new InvalidDataException(name + ": not the expected empty dynamic font; requires static-table restoration instead");
                    if (((JArray)data["m_FallbackFontAssetTable"]).Count != 0)
                        throw new InvalidDataException(name + ": unresolved original fallback references; refusing to drop them");
                }
                var settings = AddressableAssetSettingsDefaultObject.Settings;
                if (!settings) throw new InvalidOperationException("Addressables settings missing");
                var group = settings.FindGroup(GroupName) ?? settings.CreateGroup(GroupName, false, false, false, settings.DefaultGroup.Schemas);
                var schema = group.GetSchema<BundledAssetGroupSchema>();
                if (!schema) schema = group.AddSchema<BundledAssetGroupSchema>();
                schema.BuildPath.SetVariableByName(settings, "Local.BuildPath");
                schema.LoadPath.SetVariableByName(settings, "Local.LoadPath");
                schema.IncludeInBuild = true;
                settings.AddLabel("Naninovel");
                var rows = new JArray();
                foreach (string name in Names)
                {
                    string source = Path.Combine(Repo, "res/Assets/#WitchTrials/Fonts", name);
                    string metadata = Path.Combine(source, name + ".asset.json");
                    JObject data = JObject.Parse(File.ReadAllText(metadata));
                    string fontPath = "Assets/GameContent/#WitchTrials/Fonts/" + name + "/" + name + ".otf";
                    var font = AssetDatabase.LoadAssetAtPath<Font>(fontPath);
                    string path = FontPath(name);
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    string expectedGuid = FontGuids[Array.IndexOf(Names, name)];
                    AssetDatabase.Refresh();
                    var asset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(path);
                    if (!asset)
                    {
                        asset = TMP_FontAsset.CreateFontAsset(font, (int)data["m_FaceInfo"]["m_PointSize"], (int)data["m_AtlasPadding"],
                            (GlyphRenderMode)(int)data["m_AtlasRenderMode"], (int)data["m_AtlasWidth"], (int)data["m_AtlasHeight"], AtlasPopulationMode.Dynamic, true);
                        if (!asset) throw new InvalidOperationException("FontEngine rejected " + fontPath);
                        asset.name = name;
                        // Keep original face metrics and style settings, but never copy serialized PPtr IDs.
                        var values = new JObject { ["m_FaceInfo"] = data["m_FaceInfo"].DeepClone() };
                        foreach (string field in new[] { "normalStyle", "normalSpacingOffset", "boldStyle", "boldSpacing", "italicStyle", "tabSize" }) values[field] = data[field]?.DeepClone();
                        JsonUtility.FromJsonOverwrite(values.ToString(), asset);
                        asset.material.shader = Shader.Find("TextMeshPro/Distance Field");
                        if (!asset.material.shader) throw new InvalidOperationException("TMP essentials shader missing");
                        // Extractor collision suffixes differ per bundle (SC uses ~2.json).
                        // Bind by the serialized material name, never by the guessed file basename.
                        var atlasMaterial = Directory.GetFiles(source, "*.json").Select(p => JObject.Parse(File.ReadAllText(p)))
                            .Single(j => (string)j["m_Name"] == name + " Atlas Material" && j["m_SavedProperties"] != null);
                        ApplyMaterial(asset.material, atlasMaterial, asset.atlasTexture);
                        // Write directly before CreateAsset: a preceding Refresh would delete an orphan meta.
                        if (!File.Exists(path + ".meta"))
                            File.WriteAllText(path + ".meta", "fileFormatVersion: 2\nguid: " + expectedGuid + "\nNativeFormatImporter:\n  externalObjects: {}\n  mainObjectFileID: 11400000\n");
                        AssetDatabase.CreateAsset(asset, path);
                        AssetDatabase.AddObjectToAsset(asset.material, asset);
                        foreach (var texture in asset.atlasTextures) { texture.name = name + " Atlas"; AssetDatabase.AddObjectToAsset(texture, asset); }
                    }
                    if (asset.atlasPopulationMode != AtlasPopulationMode.Dynamic || asset.sourceFontFile != font)
                        throw new InvalidDataException("Existing generated asset has unexpected source/mode: " + path);
                    if (AssetDatabase.AssetPathToGUID(path) != expectedGuid)
                        throw new InvalidDataException("Unexpected generated font GUID; preserve the tracked regeneration metadata: " + path);
                    asset.isMultiAtlasTexturesEnabled = true;
                    var serializedFont = new SerializedObject(asset);
                    serializedFont.FindProperty("m_ClearDynamicDataOnBuild").boolValue = true;
                    serializedFont.ApplyModifiedPropertiesWithoutUndo();
                    var entry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(path), group);
                    entry.address = Key(name); entry.SetLabel("Naninovel", true, true);
                    // Preserve original TMP material variants as native Unity assets; shader is Unity's TMP shader.
                    int variants = 0;
                    foreach (string variantPath in Directory.GetFiles(source, name + "@*.json"))
                    {
                        string variant = Path.GetFileNameWithoutExtension(variantPath);
                        string materialPath = GeneratedRoot + "/" + name + "/" + variant + ".mat";
                        var mat = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                        if (!mat) { mat = new Material(asset.material) { name = variant }; AssetDatabase.CreateAsset(mat, materialPath); }
                        ApplyMaterial(mat, JObject.Parse(File.ReadAllText(variantPath)), asset.atlasTexture);
                        EditorUtility.SetDirty(mat); variants++;
                    }
                    EditorUtility.SetDirty(asset);
                    rows.Add(new JObject { ["name"] = name, ["key"] = Key(name), ["path"] = path, ["source"] = fontPath,
                        ["source_sha256"] = Hash(fontPath), ["metadata_sha256"] = Hash(metadata), ["dynamic"] = true,
                        ["original_glyph_count"] = 0, ["original_character_count"] = 0, ["original_fallback_count"] = 0,
                        ["point_size"] = asset.faceInfo.pointSize, ["ascent_line"] = asset.faceInfo.ascentLine,
                        ["atlas_width"] = asset.atlasWidth, ["atlas_height"] = asset.atlasHeight, ["variants"] = variants });
                }
                // The language selector can render before Naninovel applies its configured font.
                // Add CJK fallbacks without replacing the existing default font or UIConfiguration.
                var tmpSettings = TMP_Settings.instance;
                foreach (string name in Names)
                {
                    var font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(FontPath(name));
                    if (!TMP_Settings.fallbackFontAssets.Contains(font)) TMP_Settings.fallbackFontAssets.Add(font);
                }
                EditorUtility.SetDirty(tmpSettings); EditorUtility.SetDirty(settings); EditorUtility.SetDirty(group);
                AssetDatabase.SaveAssets(); AssetDatabase.Refresh();
                report["fonts"] = rows; report["status"] = "OK"; code = 0;
            }
            catch (Exception ex) { report["status"] = "ERROR"; report["error"] = ex.ToString(); Debug.LogException(ex); }
            WriteReport(report, "runtime_resource_repair.json");
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        private static void ApplyMaterial(Material material, JObject source, Texture atlas)
        {
            var properties = source["m_SavedProperties"];
            foreach (var pair in properties["m_Floats"] ?? new JArray())
                if (material.HasProperty((string)pair[0])) material.SetFloat((string)pair[0], (float)pair[1]);
            foreach (var pair in properties["m_Colors"] ?? new JArray())
            {
                var c = pair[1];
                if (material.HasProperty((string)pair[0])) material.SetColor((string)pair[0], new Color((float)c["r"], (float)c["g"], (float)c["b"], (float)c["a"]));
            }
            material.mainTexture = atlas;
            material.shaderKeywords = ((JArray)source["m_ValidKeywords"] ?? new JArray()).Values<string>().ToArray();
        }

        [MenuItem("Manosaba/Verify Runtime Resource Assets")]
        public static void Verify() => Audit(false);
        public static void VerifyBuilt() => Audit(true);

        private static void Audit(bool built)
        {
            var report = new JObject { ["generated"] = DateTime.UtcNow.ToString("o"), ["built_catalog"] = built };
            var checks = new JArray(); int failures = 0;
            void Check(string name, Action action)
            {
                try { action(); checks.Add(new JObject { ["check"] = name, ["status"] = "PASS" }); }
                catch (Exception ex) { failures++; checks.Add(new JObject { ["check"] = name, ["status"] = "FAIL", ["error"] = ex.ToString() }); }
            }
            foreach (string name in ShaderNames) Check(name, () =>
            {
                var shader = Shader.Find(name);
                if (!shader) throw new InvalidOperationException("Shader.Find returned null");
                var errors = ShaderUtil.GetShaderMessages(shader).Where(m => m.severity.ToString() == "Error").ToArray();
                if (errors.Length > 0) throw new InvalidOperationException(string.Join("; ", errors.Select(m => m.message)));
                var material = new Material(shader);
                int expected = name.EndsWith("/Generate") ? 3 : name.EndsWith("/GenerateHQ") ? 2 : 1;
                if (material.passCount < expected) throw new InvalidOperationException("Missing shader passes");
                if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.Null)
                {
                    if (!shader.isSupported) throw new InvalidOperationException("Shader unsupported on active graphics device");
                    for (int pass = 0; pass < expected; pass++)
                        if (!material.SetPass(pass)) throw new InvalidOperationException("Shader pass failed: " + pass);
                }
                Object.DestroyImmediate(material);
            });
            Check("True Shadow Blue Noise", () => { if (!Resources.Load<Texture2D>("True Shadow Blue Noise")) throw new Exception("noise missing"); });
            Check("True Shadow Project Settings", () => { if (!Resources.Load<LeTai.TrueShadow.ProjectSettings>("True Shadow Project Settings")) throw new Exception("settings missing"); });
            for (int i = 0; i < Names.Length; i++)
            {
                string name = Names[i], sample = Samples[i];
                Check(Key(name), () =>
                {
                    AsyncOperationHandle<TMP_FontAsset> handle = default;
                    TMP_FontAsset font;
                    try
                    {
                        font = built ? (handle = Addressables.LoadAssetAsync<TMP_FontAsset>(Key(name))).WaitForCompletion() : AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(FontPath(name));
                        if (!font) throw new Exception("TMP_FontAsset missing");
                        if (!font.sourceFontFile || font.atlasPopulationMode != AtlasPopulationMode.Dynamic || !font.isMultiAtlasTexturesEnabled) throw new Exception("CJK dynamic source/atlas configuration missing");
                        if (!font.TryAddCharacters(sample, out string missing)) throw new Exception("Missing glyphs: " + missing);
                        var canvasRoot = new GameObject("FontVerificationCanvas", typeof(RectTransform), typeof(Canvas));
                        var go = new GameObject("FontVerification", typeof(RectTransform), typeof(CanvasRenderer));
                        go.transform.SetParent(canvasRoot.transform, false);
                        try
                        {
                            var text = go.AddComponent<TextMeshProUGUI>(); text.font = font; text.text = sample;
                            text.rectTransform.sizeDelta = new Vector2(1600, 300); text.fontSize = 48; text.ForceMeshUpdate(true, true);
                            if (text.textInfo.characterCount != sample.Length || text.textInfo.meshInfo[0].vertexCount == 0)
                                throw new Exception("CJK text mesh not generated: chars=" + text.textInfo.characterCount + " vertices=" + text.textInfo.meshInfo[0].vertexCount);
                        }
                        finally { Object.DestroyImmediate(canvasRoot); }
                    }
                    finally { if (handle.IsValid()) Addressables.Release(handle); }
                });
            }
            if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.Null)
                Check("CJK UI preview", RenderPreview);
            report["checks"] = checks; report["failures"] = failures; report["status"] = failures == 0 ? "PASS" : "FAIL";
            WriteReport(report, built ? "runtime_resources_built.json" : "runtime_resources_assets.json");
            Debug.Log("[RuntimeResourceRepair] " + report["status"] + " failures=" + failures);
            if (Application.isBatchMode) EditorApplication.Exit(failures == 0 ? 0 : 1);
        }

        private static void RenderPreview()
        {
            var root = new GameObject("RuntimeResourcePreview");
            var target = new RenderTexture(1200, 540, 24);
            var previous = RenderTexture.active;
            Texture2D pixels = null;
            try
            {
                var camera = root.AddComponent<Camera>(); camera.orthographic = true; camera.orthographicSize = 2.7f;
                camera.transform.position = new Vector3(0, 0, -10); camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = new Color(0.06f, 0.07f, 0.1f); camera.targetTexture = target;
                var canvasObject = new GameObject("CJK Canvas", typeof(RectTransform), typeof(Canvas));
                canvasObject.transform.SetParent(root.transform, false);
                var canvas = canvasObject.GetComponent<Canvas>(); canvas.renderMode = RenderMode.WorldSpace; canvas.worldCamera = camera;
                var rect = (RectTransform)canvas.transform; rect.sizeDelta = new Vector2(1200, 540); rect.localScale = Vector3.one * 0.01f;
                rect.localPosition = new Vector3(0, 0, 10);
                for (int i = 0; i < Names.Length; i++)
                {
                    var go = new GameObject(Names[i], typeof(RectTransform), typeof(CanvasRenderer)); go.transform.SetParent(canvas.transform, false);
                    var text = go.AddComponent<TextMeshProUGUI>(); text.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(FontPath(Names[i]));
                    text.text = Samples[i]; text.fontSize = 46; text.color = Color.white; text.alignment = TextAlignmentOptions.Center;
                    text.rectTransform.sizeDelta = new Vector2(1150, 130); text.rectTransform.anchoredPosition = new Vector2(0, 155 - i * 155);
                    text.ForceMeshUpdate(true, true);
                }
                Canvas.ForceUpdateCanvases(); camera.Render(); RenderTexture.active = target;
                pixels = new Texture2D(target.width, target.height, TextureFormat.RGB24, false);
                pixels.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0); pixels.Apply();
                string path = Path.Combine(Repo, "tools/unity-rebuild/out/runtime-resources/cjk-preview.png");
                Directory.CreateDirectory(Path.GetDirectoryName(path)); File.WriteAllBytes(path, pixels.EncodeToPNG());
                if (pixels.GetPixels32().Count(c => c.r > 160 && c.g > 160 && c.b > 160) < 1000) throw new Exception("Preview contains insufficient visible text pixels; saved failed preview for diagnosis");
            }
            finally
            {
                RenderTexture.active = previous;
                Object.DestroyImmediate(root); target.Release(); Object.DestroyImmediate(target);
                if (pixels) Object.DestroyImmediate(pixels);
            }
        }

        private static string Hash(string path) { using var sha = SHA256.Create(); return BitConverter.ToString(sha.ComputeHash(File.ReadAllBytes(path))).Replace("-", "").ToLowerInvariant(); }
        private static void WriteReport(JObject report, string filename)
        {
            string path = Environment.GetEnvironmentVariable("MANOSABA_RESOURCE_REPORT");
            if (string.IsNullOrEmpty(path)) path = Path.Combine(Repo, "tools/unity-rebuild/out/unity", filename);
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))); File.WriteAllText(path, report.ToString());
        }
    }
}
