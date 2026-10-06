// Sprite import policy for the reconstructed sprite crops.
//
// In the shipped build a Sprite is a sub-asset of its Texture2D (or lives on a SpriteAtlas page).  The extraction
// pipeline dumped the sprite *pixels* as one crop per sprite (files named "<texture>.sprite.png" /
// "<texture>.sprite~N.png"), and a13 writes each crop as "<original sprite name>.png" under "<texture>.sprites/", so
// the imported sprite keeps the name the game's prefabs refer to.  This postprocessor makes each crop import as a
// Sprite instead of a Texture2D.
//
// The shipped Sprite's own settings come from tools/unity-rebuild/out/unity/sprite_settings_plan.json
// (tools/unity-rebuild/plan_sprite_settings.py, read from the shipped Sprite objects): pixels per unit, pivot, 9-slice
// border, mesh type, and the settings of the texture its pixels live in (format, filter, wrap, mipmaps, colour space),
// with a max size that never downscales.  A crop the plan does not list keeps the placeholders this policy always used
// (100 PPU, centre pivot, max 2048, default compression) - recorded as a deviation, not as recovered data.
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public class SpriteImportPolicy : AssetPostprocessor
    {
        /// <summary>Only crops carry this marker; the owning sheet textures stay plain textures.</summary>
        private const string CropMarker = ".sprite";
        private const string PlanRelativePath = "../tools/unity-rebuild/out/unity/sprite_settings_plan.json";

        private static Dictionary<string, JObject> plan;
        private static DateTime planStamp;
        private static bool applyingPlannedSettings;

        void OnPreprocessTexture()
        {
            if (assetPath == null || assetPath.IndexOf(CropMarker, StringComparison.Ordinal) < 0) return;

            var importer = assetImporter as TextureImporter;
            if (importer == null) return;

            // A pixel edit must keep the existing .meta settings.  Reapplying the shipped plan here
            // would reset a user's pivot, border, filtering, compression, and other importer edits.
            // New crops still receive this policy; the explicit menu command can reapply the plan.
            if (!applyingPlannedSettings && !importer.importSettingsMissing &&
                importer.textureType == TextureImporterType.Sprite &&
                importer.spriteImportMode == SpriteImportMode.Single) return;

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.alphaIsTransparency = true;
            importer.isReadable = false;

            JObject entry = PlanEntry(assetPath);
            if (entry == null)
            {
                importer.spritePixelsPerUnit = 100f;
                importer.mipmapEnabled = false;
                importer.wrapMode = TextureWrapMode.Clamp;
                importer.filterMode = FilterMode.Bilinear;
                importer.maxTextureSize = 2048;
                importer.textureCompression = TextureImporterCompression.Compressed;
                return;
            }

            importer.spritePixelsPerUnit = (float)entry["pixels_per_unit"];
            var settings = new TextureImporterSettings();
            importer.ReadTextureSettings(settings);
            var pivot = new Vector2((float)entry["pivot"][0], (float)entry["pivot"][1]);
            settings.spriteAlignment = pivot == new Vector2(0.5f, 0.5f)
                ? (int)SpriteAlignment.Center : (int)SpriteAlignment.Custom;
            settings.spritePivot = pivot;
            settings.spriteBorder = new Vector4((float)entry["border"][0], (float)entry["border"][1],
                (float)entry["border"][2], (float)entry["border"][3]);
            settings.spriteMeshType = IsCutInCrop(assetPath)
                ? ShippedMeshType(entry)
                : (bool)entry["full_rect_mesh"] ? SpriteMeshType.FullRect : SpriteMeshType.Tight;
            importer.SetTextureSettings(settings);

            int rectW = (int)entry["rect_size"][0], rectH = (int)entry["rect_size"][1];
            int maxSize = 32;
            while (maxSize < Math.Max(rectW, rectH)) maxSize *= 2;
            importer.maxTextureSize = maxSize;

            var tex = entry["texture"] as JObject;
            if (tex == null)
            {
                importer.mipmapEnabled = false;
                importer.wrapMode = TextureWrapMode.Clamp;
                importer.filterMode = FilterMode.Bilinear;
                importer.textureCompression = TextureImporterCompression.Compressed;
                return;
            }
            importer.mipmapEnabled = (int)tex["mip_count"] > 1;
            importer.sRGBTexture = (int)tex["color_space"] == 1;
            importer.filterMode = (FilterMode)(int)tex["filter_mode"];
            importer.wrapModeU = (TextureWrapMode)(int)tex["wrap_u"];
            importer.wrapModeV = (TextureWrapMode)(int)tex["wrap_v"];
            importer.anisoLevel = (int)tex["aniso"];
            string formatName = Enum.GetName(typeof(TextureFormat), (int)tex["format_value"]);
            var standalone = importer.GetPlatformTextureSettings("Standalone");
            standalone.overridden = false;
            if (formatName != null && Enum.TryParse(formatName, out TextureImporterFormat importerFormat))
            {
                standalone.overridden = true;
                standalone.format = importerFormat;
                standalone.maxTextureSize = maxSize;
            }
            importer.SetPlatformTextureSettings(standalone);
            importer.textureCompression = IsUncompressed(formatName)
                ? TextureImporterCompression.Uncompressed : TextureImporterCompression.Compressed;
        }

        /// <summary>
        /// Reimports every crop the plan lists, so a new plan reaches assets that are already imported (the policy only
        /// runs when an asset imports).  Batch: Unity -batchmode -quit -projectPath UnityProject
        /// -executeMethod Manosaba.EditorTools.SpriteImportPolicy.ReimportPlanned -logFile -
        /// </summary>
        [MenuItem("Manosaba/Reimport Sprite Crops (shipped sprite settings)")]
        public static void ReimportPlanned()
        {
            PlanEntry("");
            int count = 0;
            applyingPlannedSettings = true;
            AssetDatabase.StartAssetEditing();
            try
            {
                foreach (string path in plan == null ? new List<string>() : new List<string>(plan.Keys))
                {
                    if (!File.Exists(Path.Combine(Directory.GetParent(Application.dataPath).FullName, path))) continue;
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                    count++;
                }
            }
            finally
            {
                try { AssetDatabase.StopAssetEditing(); }
                finally { applyingPlannedSettings = false; }
            }
            int mismatched = 0;
            foreach (var pair in plan ?? new Dictionary<string, JObject>())
            {
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(pair.Key);
                if (sprite == null) continue;
                if (Math.Abs(sprite.pixelsPerUnit - (float)pair.Value["pixels_per_unit"]) > 1e-3f ||
                    (int)sprite.rect.width != (int)pair.Value["rect_size"][0] ||
                    (int)sprite.rect.height != (int)pair.Value["rect_size"][1])
                {
                    mismatched++;
                    if (mismatched <= 20)
                        Debug.LogWarning("[SpriteImportPolicy] " + pair.Key + " imported ppu=" + sprite.pixelsPerUnit +
                                         " rect=" + sprite.rect.size + ", shipped ppu=" + pair.Value["pixels_per_unit"] +
                                         " rect=" + pair.Value["rect_size"]);
                }
            }
            Debug.Log("[SpriteImportPolicy] reimported " + count + " planned crops; " + mismatched +
                      " differ from the shipped pixels-per-unit / rect size");
            if (Application.isBatchMode) EditorApplication.Exit(mismatched == 0 ? 0 : 1);
        }

        /// <summary>
        /// The refutation cut-ins draw their sprites with Shader Graph materials whose alpha comes from procedural noise,
        /// not from the texture, so the sprite *mesh* is what cuts the character/glass shape out of its rectangle.
        /// </summary>
        public static bool IsCutInCrop(string assetPath) =>
            (assetPath ?? "").Replace('\\', '/').Contains("/Prefabs/Naninovel/Spawn/CutIn/ObjectionCutIn_");

        /// <summary>
        /// The mesh type the shipped Sprite had.  The plan key "full_rect_mesh" holds m_RD.settingsRaw bit 6, which Unity
        /// defines as SpriteMeshType (0 = FullRect, 1 = Tight): every shipped sprite with bit 6 = 0 has exactly 4 vertices
        /// and the ones with bit 6 = 1 have 26..258, so the key's name is inverted.  Only the cut-in crops read it
        /// correctly so far; the general mapping above still reads it as written.
        /// </summary>
        public static SpriteMeshType ShippedMeshType(JObject entry) =>
            (bool)entry["full_rect_mesh"] ? SpriteMeshType.Tight : SpriteMeshType.FullRect;

        /// <summary>The shipped mesh type of the crop at a path; throws when the plan has no row for it.</summary>
        public static SpriteMeshType ShippedMeshType(string assetPath)
        {
            JObject entry = PlanEntry(assetPath);
            if (entry == null) throw new InvalidDataException("No shipped sprite settings for " + assetPath);
            return ShippedMeshType(entry);
        }

        /// <summary>The plan row for a crop, reloading the plan when the file changed.</summary>
        private static JObject PlanEntry(string path)
        {
            string file = Path.GetFullPath(Path.Combine(Directory.GetParent(Application.dataPath).FullName, PlanRelativePath));
            if (!File.Exists(file)) return null;
            DateTime stamp = File.GetLastWriteTimeUtc(file);
            if (plan == null || stamp != planStamp)
            {
                plan = new Dictionary<string, JObject>(StringComparer.Ordinal);
                foreach (var property in ((JObject)JObject.Parse(File.ReadAllText(file))["sprites"]).Properties())
                    plan[property.Name] = (JObject)property.Value;
                planStamp = stamp;
            }
            return plan.TryGetValue(path, out JObject entry) ? entry : null;
        }

        private static bool IsUncompressed(string formatName)
        {
            switch (formatName)
            {
                case "Alpha8": case "R8": case "R16": case "RG16": case "RGB24": case "RGBA32": case "ARGB32":
                case "BGRA32": case "RHalf": case "RGHalf": case "RGBAHalf": case "RFloat": case "RGFloat":
                case "RGBAFloat": case "RGB565": case "ARGB4444": case "RGBA4444":
                    return true;
                default:
                    return false;
            }
        }
    }
}
