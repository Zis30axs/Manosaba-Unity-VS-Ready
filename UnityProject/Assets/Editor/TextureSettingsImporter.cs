// Applies the shipped Texture2D settings to the project's texture importers, for the textures in
// tools/unity-rebuild/out/unity/texture_settings_plan.json (written by tools/unity-rebuild/plan_texture_settings.py).
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.TextureSettingsImporter.Run -logFile -
//
// Why: an extracted PNG imports with Unity's defaults (Non-Power of 2 "To Nearest", mipmaps, sRGB, Repeat, max size
// 2048, normal compression).  For a lookup texture that changes the data itself - the shipped "ToneCurve" sampled by
// "Naninovel Extender/Maskable Transparent Toned" is 256x4 R8, one mip, linear, Clamp - and for every background it
// changes the shape: a shipped 2560x1440 still became 2048x1024, and Naninovel sizes a background from its texture, so
// every background was drawn at 2:1 (the title still stretched sideways, the black "Overlay" fader 1920x960 on a
// 1080-pixel screen).  Per plan entry this sets what the shipped object records and nothing else:
//   no power-of-two rescale (the shipped object keeps its own size); mipmaps = (mip count > 1); sRGB = (colour
//   space 1); wrap U/V, filter mode, aniso level, mip bias, readability;
//   max size = the smallest power of two that holds the shipped width and height (never a downscale);
//   Standalone format = the shipped TextureFormat when TextureImporterFormat has the same member (R8, DXT1, DXT5,
//   DXT1Crunched, BC7, RGBA32, ...), and uncompressed for the uncompressed formats.
// After the reimport the imported Texture2D's size is compared with the shipped one; a mismatch is FAILED, not APPLIED.
// Sprite crops (".sprite" in the path) are owned by SpriteImportPolicy and are reported, not touched.
// Writes tools/unity-rebuild/out/unity/texture_settings_import.json.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class TextureSettingsImporter
    {
        private const string InputRelativePath = "../tools/unity-rebuild/out/unity/texture_settings_plan.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/texture_settings_import.json";
        private const string CropMarker = ".sprite";

        [MenuItem("Manosaba/Apply Shipped Texture Settings (from plan)")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            var result = new ImportResult { generated = DateTime.UtcNow.ToString("o") };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string inputPath = Path.GetFullPath(Path.Combine(projectRoot, InputRelativePath));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            try
            {
                if (!File.Exists(inputPath))
                {
                    result.status = "BLOCKED";
                    result.error = "plan not found: " + inputPath + " (run tools/unity-rebuild/plan_texture_settings.py --write)";
                }
                else
                {
                    var plan = JObject.Parse(File.ReadAllText(inputPath));
                    // Settings first, one batched reimport, then the size check: StartAssetEditing defers every
                    // SaveAndReimport to StopAssetEditing, where the texture imports run together.
                    AssetDatabase.StartAssetEditing();
                    try
                    {
                        foreach (JObject entry in (JArray)plan["textures"])
                            result.textures.Add(ApplyOne(entry));
                    }
                    finally
                    {
                        AssetDatabase.StopAssetEditing();
                    }
                    foreach (TextureRow row in result.textures.FindAll(t => t.status == "PENDING"))
                        Verify(row);
                    result.applied = result.textures.FindAll(t => t.status == "APPLIED").Count;
                    result.status = result.textures.Exists(t => t.status == "FAILED") ? "PARTIAL" : "OK";
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[TextureSettingsImporter] " + ex);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            Debug.Log("[TextureSettingsImporter] " + result.status + " applied=" + result.applied + " of " +
                      result.textures.Count + " -> " + outPath);
            return result.status == "OK" ? 0 : 1;
        }

        private static TextureRow ApplyOne(JObject entry)
        {
            var row = new TextureRow
            {
                asset_path = (string)entry["asset_path"],
                shipped_status = (string)entry["status"],
            };
            if (row.shipped_status != "READ")
            {
                row.status = "SKIPPED";
                row.notes.Add("the plan could not read the shipped texture (" + row.shipped_status + ")");
                return row;
            }
            if (string.IsNullOrEmpty(row.asset_path) || row.asset_path.IndexOf(CropMarker, StringComparison.Ordinal) >= 0)
            {
                row.status = "SKIPPED";
                row.notes.Add("a sprite crop: its import settings belong to SpriteImportPolicy");
                return row;
            }
            var importer = AssetImporter.GetAtPath(row.asset_path) as TextureImporter;
            if (importer == null)
            {
                row.status = "FAILED";
                row.notes.Add("no TextureImporter at this path");
                return row;
            }

            int width = (int)entry["width"], height = (int)entry["height"];
            int mipCount = (int)entry["mip_count"], colorSpace = (int)entry["color_space"];
            int formatValue = (int)entry["format_value"];

            importer.npotScale = TextureImporterNPOTScale.None;
            importer.mipmapEnabled = mipCount > 1;
            importer.sRGBTexture = colorSpace == 1;
            importer.wrapModeU = (TextureWrapMode)(int)entry["wrap_u"];
            importer.wrapModeV = (TextureWrapMode)(int)entry["wrap_v"];
            importer.filterMode = (FilterMode)(int)entry["filter_mode"];
            importer.anisoLevel = (int)entry["aniso"];
            importer.mipMapBias = (float)entry["mip_bias"];
            importer.isReadable = (bool)entry["is_readable"];
            int maxSize = 32;
            while (maxSize < Math.Max(width, height)) maxSize *= 2;
            importer.maxTextureSize = maxSize;

            string formatName = Enum.GetName(typeof(TextureFormat), formatValue);
            row.shipped_format = formatName ?? formatValue.ToString();
            var standalone = importer.GetPlatformTextureSettings("Standalone");
            standalone.overridden = false;
            if (formatName != null && Enum.TryParse(formatName, out TextureImporterFormat importerFormat))
            {
                standalone.overridden = true;
                standalone.format = importerFormat;
                standalone.maxTextureSize = maxSize;
                row.notes.Add("Standalone format " + importerFormat + " (the shipped TextureFormat)");
            }
            else
            {
                row.notes.Add("shipped format " + row.shipped_format + " has no TextureImporterFormat member; " +
                              "the importer's automatic format is kept");
            }
            importer.SetPlatformTextureSettings(standalone);
            importer.textureCompression = IsUncompressed(formatName)
                ? TextureImporterCompression.Uncompressed : TextureImporterCompression.Compressed;
            importer.SaveAndReimport();

            row.shipped_width = width;
            row.shipped_height = height;
            row.status = "PENDING";
            row.applied = "mipmaps=" + importer.mipmapEnabled + " sRGB=" + importer.sRGBTexture + " wrap=" +
                          importer.wrapModeU + "/" + importer.wrapModeV + " filter=" + importer.filterMode +
                          " maxSize=" + maxSize + " format=" + (standalone.overridden ? standalone.format.ToString() : "auto");
            return row;
        }

        /// <summary>The imported texture must have the shipped size; anything else is FAILED, not APPLIED.</summary>
        private static void Verify(TextureRow row)
        {
            var imported = AssetDatabase.LoadAssetAtPath<Texture2D>(row.asset_path);
            row.imported_size = imported == null ? "none" : imported.width + "x" + imported.height;
            if (imported == null || imported.width != row.shipped_width || imported.height != row.shipped_height)
            {
                row.status = "FAILED";
                row.notes.Add("imported as " + row.imported_size + ", shipped " + row.shipped_width + "x" + row.shipped_height);
                return;
            }
            row.status = "APPLIED";
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

        [Serializable]
        public class TextureRow
        {
            public string asset_path;
            public string shipped_status;
            public string shipped_format;
            public string imported_size;
            public int shipped_width;
            public int shipped_height;
            public string status;
            public string applied;
            public List<string> notes = new List<string>();
        }

        [Serializable]
        public class ImportResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string error;
            public int applied;
            public List<TextureRow> textures = new List<TextureRow>();
        }
    }
}
