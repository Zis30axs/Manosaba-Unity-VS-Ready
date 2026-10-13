using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using Shared = Manosaba.EditorTools.SwitchTitleContentBuilder;

namespace Manosaba.EditorTools
{
    /// <summary>Imports reviewed transparent portraits as independent sprites, retaining the original cut-in assets.</summary>
    public static class CorrectedCutInPortraitContentBuilder
    {
        public const string ManifestPath = "content-overrides/corrected-cutin-portraits/manifest.json";
        public const string TargetDirectory = "Assets/GameContent/#WitchTrials/CorrectedCutInPortraits/";
        public const string AddressPrefix = "Naninovel/CorrectedCutInPortraits/";
        private const string InputDirectory = "content-overrides/corrected-cutin-portraits/";
        private const string OriginalDirectory = "Assets/GameContent/#WitchTrials/SwitchStory/Textures/";
        private const string PrefabDirectory = "Assets/GameContent/#WitchTrials/SwitchStory/Prefabs/";
        private static string Project => Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        private static string Repo => Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
        private static readonly Dictionary<string, Vector2Int> Canvases = new Dictionary<string, Vector2Int> {
            ["Alisa"] = new Vector2Int(784,1496), ["AnAn"] = new Vector2Int(1801,1469), ["Coco"] = new Vector2Int(1233,1588),
            ["Ema"] = new Vector2Int(1223,1758), ["Hanna"] = new Vector2Int(1782,1634), ["Hiro"] = new Vector2Int(1297,1635),
            ["Leia"] = new Vector2Int(1261,1338), ["Margo"] = new Vector2Int(668,1597), ["Meruru"] = new Vector2Int(1299,1583),
            ["Miria"] = new Vector2Int(910,1556), ["Nanoka"] = new Vector2Int(1516,1575), ["Noah"] = new Vector2Int(1574,1604),
            ["Sherry"] = new Vector2Int(1311,1547)
        };
        private static readonly string[] CodeInputs = {
            "content-overrides/corrected-cutin-portraits/manifest.schema.json",
            "UnityProject/Assets/Editor/CorrectedCutInPortraitContentBuilder.cs",
            "UnityProject/Assets/Editor/CorrectedCutInPortraitContentBuilder.cs.meta",
            "UnityProject/Assets/Editor/SwitchTitleContentBuilder.cs",
            "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/CorrectedCutInPortraitAdapter.cs",
            "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/CorrectedCutInPortraitAdapter.cs.meta",
            "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/ObjectionCutIn.cs",
            "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/ObjectionCutIn.cs.meta",
            "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Models/EnhancementSettings.cs",
            "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Models/EnhancementSettings.cs.meta"
        };

        private sealed class PngInfo
        {
            public Vector2Int Size;
            public RectInt AlphaBounds; // Top-left origin, alpha >= 16.
            public float TransparentFraction;
            public string RgbaSha256; // Unmodified RGBA8 pixels, rows from top to bottom.
        }

        private sealed class Prepared
        {
            public JObject Row;
            public string Character, Source, Target, OriginalPath;
            public Sprite Original;
            public PngInfo OriginalPng, CorrectedPng;
            public Vector2 OriginalAnchor, CorrectedAnchor, Pivot;
            public float Ppu;
        }

        public static JObject ReadValidatedManifest()
        {
            string path = Shared.ResolveBelow(Repo, ManifestPath);
            var manifest = JObject.Parse(File.ReadAllText(path));
            Require((int?)manifest["version"] == 2 && manifest["portraits"] is JArray, "Unsupported corrected portrait manifest; version 2 requires reviewed decoded original pixels.");
            var rows = ((JArray)manifest["portraits"]).OfType<JObject>().ToArray();
            Require(rows.Length == 13 && rows.Select(row => (string)row["character"]).OrderBy(n => n, StringComparer.Ordinal)
                .SequenceEqual(Canvases.Keys.OrderBy(n => n, StringComparer.Ordinal)), "Corrected portraits require exactly the reviewed thirteen characters.");
            foreach (JObject row in rows)
            {
                string character = (string)row["character"], source = (string)row["source"], original = (string)row["original_sprite"];
                Require(source == InputDirectory + character + ".png", "Unexpected corrected portrait input: " + source);
                Require(original != null && original.StartsWith(OriginalDirectory + "CutIn_Small_Character_" + character + "_", StringComparison.Ordinal)
                    && original.EndsWith("_sprite.png", StringComparison.Ordinal) && Path.GetDirectoryName(original).Replace('\\','/') == OriginalDirectory.TrimEnd('/'),
                    "Unexpected original sprite ownership: " + original);
                VerifyFile(Shared.ResolveBelow(Repo, source), (string)row["sha256"]);
                Require(IsSha256((string)row["original_sha256"]), "Original PNG encoding provenance is missing: " + character);
                VerifyOriginalPixels(row);
                Require(row["alignment"] == null || (row["alignment"] is JObject alignment &&
                    alignment["original_anchor_px"] is JArray originalAnchor && originalAnchor.Count == 2 &&
                    alignment["corrected_anchor_px"] is JArray correctedAnchor && correctedAnchor.Count == 2),
                    "Explicit portrait alignment must provide both anchors: " + character);
            }
            return manifest;
        }

        public static JObject EnsureApplied()
        {
            string manifestPath = Shared.ResolveBelow(Repo, ManifestPath);
            if (!File.Exists(manifestPath)) return new JObject { ["status"] = "disabled", ["reason"] = "No corrected portrait manifest" };
            string statePath = Path.Combine(Project, "Library/ManosabaContent/corrected-cutin-portraits-state.json");
            string errorPath = Path.Combine(Project, "Library/ManosabaContent/corrected-cutin-portraits-error.json");
            var report = new JObject { ["status"] = "ERROR", ["manifest"] = ManifestPath, ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["saved_state_changed"] = false, ["original_prefabs_modified"] = false, ["render_verified"] = false };
            try
            {
                Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Import corrected portraits outside Play mode.");
                JObject manifest = ReadValidatedManifest();
                // Validate all source PNGs, originals and slot identities before creating any output.
                Prepared[] inputs = ((JArray)manifest["portraits"]).OfType<JObject>().Select(Prepare).ToArray();
                var hashes = new JObject();
                AddHash(hashes, Repo, ManifestPath);
                foreach (string path in CodeInputs) AddHash(hashes, Repo, path);
                foreach (Prepared input in inputs)
                {
                    AddHash(hashes, Repo, input.Source);
                    AddHash(hashes, Repo, "UnityProject/" + input.OriginalPath);
                    AddHash(hashes, Repo, "UnityProject/" + input.OriginalPath + ".meta");
                    AddHash(hashes, Repo, "UnityProject/" + PrefabDirectory + PrefabName(input.Character) + ".prefab");
                }
                report["input_sha256"] = hashes;
                report["signature"] = HashText(hashes.ToString(Newtonsoft.Json.Formatting.None));
                JObject previous = Shared.ReadState(statePath);
                bool cacheHit = (string)previous?["status"] == "OK" && (string)previous["signature"] == (string)report["signature"] &&
                    previous["output_sha256"] is JObject oldHashes && oldHashes.Count == 26 && oldHashes.Properties().All(p =>
                        File.Exists(Shared.ResolveBelow(Project,p.Name)) && Shared.Hash(Shared.ResolveBelow(Project,p.Name)) == (string)p.Value);
                report["cache"] = new JObject { ["hit"] = cacheHit };
                var rows = new JArray();
                var addresses = new JArray();
                bool rebuilt = false;
                foreach (Prepared input in inputs)
                {
                    string target = Shared.ResolveBelow(Project, input.Target), source = Shared.ResolveBelow(Repo, input.Source);
                    string expected = ((string)input.Row["sha256"]).ToLowerInvariant();
                    string guidBefore = AssetDatabase.AssetPathToGUID(input.Target);
                    bool copied = !File.Exists(target) || Shared.Hash(target) != expected;
                    // Generated output can be updated from the previous known result; unrelated manual edits fail closed.
                    if (File.Exists(target) && copied)
                        Require((string)previous?["output_sha256"]?[input.Target] == Shared.Hash(target), "Corrected portrait target has an unacknowledged edit: " + input.Target);
                    if (copied)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(target));
                        File.Copy(source, target, true); // Preserve an existing .meta and its GUID.
                        AssetDatabase.ImportAsset(input.Target, ImportAssetOptions.ForceSynchronousImport);
                    }
                    else if (!AssetImporter.GetAtPath(input.Target))
                        AssetDatabase.ImportAsset(input.Target, ImportAssetOptions.ForceSynchronousImport);
                    var textureRecipe = new JObject { ["source"] = input.Source, ["target"] = input.Target, ["sprite"] = true,
                        ["ppu"] = input.Ppu, ["pivot"] = new JArray(input.Pivot.x,input.Pivot.y), ["border"] = new JArray(0,0,0,0), ["wrap_mode"] = 1 };
                    var item = new JObject { ["character"] = input.Character, ["source"] = input.Source, ["target"] = input.Target,
                        ["address"] = AddressPrefix + input.Character, ["prefab_address"] = PrefabAddress(input.Character),
                        ["renderer_paths"] = new JArray("CharaRig/Chara","CharaRig/Chara_Shadow"), ["guid_before"] = guidBefore, ["copied"] = copied };
                    bool importerChanged = Shared.EnsureTexture(Repo, textureRecipe, item);
                    var importer = (TextureImporter)AssetImporter.GetAtPath(input.Target);
                    var textureSettings = new TextureImporterSettings();
                    importer.ReadTextureSettings(textureSettings);
                    if (textureSettings.spriteMeshType != SpriteMeshType.FullRect || !importer.alphaIsTransparency || importer.filterMode != FilterMode.Bilinear)
                    {
                        textureSettings.spriteMeshType = SpriteMeshType.FullRect;
                        importer.SetTextureSettings(textureSettings);
                        importer.alphaIsTransparency = true;
                        importer.filterMode = FilterMode.Bilinear;
                        importer.SaveAndReimport();
                        importerChanged = true;
                    }
                    Sprite corrected = AssetDatabase.LoadAssetAtPath<Sprite>(input.Target);
                    VerifyImported(input, corrected, item);
                    Require(string.IsNullOrEmpty(guidBefore) || guidBefore == AssetDatabase.AssetPathToGUID(input.Target), "Corrected portrait GUID changed.");
                    Require(Shared.Hash(target) == expected, "Corrected portrait PNG bytes changed during import.");
                    item["guid"] = AssetDatabase.AssetPathToGUID(input.Target);
                    item["importer_changed"] = importerChanged;
                    rows.Add(item);
                    addresses.Add(new JObject { ["path"] = input.Target, ["address"] = AddressPrefix + input.Character, ["labels"] = new JArray("CorrectedCutInPortraits") });
                    rebuilt |= copied || importerChanged;
                }
                report["portraits"] = rows;
                report["addresses"] = Shared.EnsureAddresses(addresses, "Manosaba.CorrectedCutInPortraits");
                AssetDatabase.SaveAssets();
                // The original PNGs, metas and prefab serialization are protected inputs, including on a cache hit.
                foreach (var property in hashes.Properties())
                    Require(Shared.Hash(Shared.ResolveBelow(Repo, property.Name)) == (string)property.Value, "Portrait import changed a protected input: " + property.Name);
                var outputs = new JObject();
                foreach (Prepared input in inputs)
                    foreach (string path in new[] { input.Target,input.Target + ".meta" }) AddHash(outputs, Project, path);
                report["output_sha256"] = outputs;
                report["rebuilt"] = rebuilt;
                report["status"] = "OK";
                Shared.WriteState(statePath, report);
                return report;
            }
            catch (Exception error)
            {
                report["error"] = error.ToString();
                // Only completed imports may replace the confirmation used to
                // authorize later payload updates. A validation/import failure
                // must not erase the last successful output hashes and GUIDs.
                report["successful_state_preserved"] = File.Exists(statePath);
                try { Shared.WriteState(errorPath, report); } catch (Exception stateError) { Debug.LogError(stateError.Message); }
                throw;
            }
        }

        private static string PrefabName(string character)
        {
            Require(Canvases.ContainsKey(character), "Unknown corrected portrait character: " + character);
            return "CutinSmall_" + (character == "AnAn" ? "Anan" : character);
        }

        public static string PrefabAddress(string character) => "Naninovel/Spawn/" + PrefabName(character);

        private static Prepared Prepare(JObject row)
        {
            string character = (string)row["character"], originalPath = (string)row["original_sprite"];
            Sprite original = AssetDatabase.LoadAssetAtPath<Sprite>(originalPath);
            Require(original && original.rect.size == (Vector2)Canvases[character] && Mathf.Approximately(original.pixelsPerUnit,100) &&
                Vector2.Distance(original.pivot, original.rect.size * .5f) < .01f, "Original portrait geometry differs: " + character);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDirectory + PrefabName(character) + ".prefab");
            Require(prefab && prefab.name == PrefabName(character), "Original small cut-in prefab identity differs: " + character);
            foreach (string path in new[] { "CharaRig/Chara", "CharaRig/Chara_Shadow" })
            {
                var node = prefab.transform.Find(path);
                var renderer = node ? node.GetComponent<SpriteRenderer>() : null;
                Require(renderer && renderer.sprite == original && !renderer.flipX && !renderer.flipY,
                    "Original portrait slot/identity/orientation differs: " + character + "/" + path);
            }
            var input = new Prepared { Row=row, Character=character, Original=original, OriginalPath=originalPath,
                Source=(string)row["source"], Target=TargetDirectory + character + ".png",
                OriginalPng=ReadPng(Shared.ResolveBelow(Project,originalPath)), CorrectedPng=ReadPng(Shared.ResolveBelow(Repo,(string)row["source"])) };
            Require(input.OriginalPng.Size == Canvases[character], "Original PNG and imported sprite canvas differ: " + character);
            Require(string.Equals(input.OriginalPng.RgbaSha256, (string)row["original_rgba_sha256"], StringComparison.OrdinalIgnoreCase),
                "Original portrait pixels changed during import preparation: " + character);
            Require(input.CorrectedPng.TransparentFraction >= .02f, "The corrected portrait has no useful transparent background: " + character);
            input.OriginalAnchor = ReadAnchor(row["alignment"]?["original_anchor_px"], input.OriginalPng);
            input.CorrectedAnchor = ReadAnchor(row["alignment"]?["corrected_anchor_px"], input.CorrectedPng);
            input.Ppu = 100f * input.CorrectedPng.AlphaBounds.height / input.OriginalPng.AlphaBounds.height;
            Vector2 originalWorld = AnchorWorld(input.OriginalAnchor, input.OriginalPng.Size, original.pivot, original.pixelsPerUnit);
            Vector2 correctedBottom = new Vector2(input.CorrectedAnchor.x, input.CorrectedPng.Size.y - input.CorrectedAnchor.y);
            Vector2 pivotPixels = correctedBottom - originalWorld * input.Ppu;
            input.Pivot = new Vector2(pivotPixels.x / input.CorrectedPng.Size.x, pivotPixels.y / input.CorrectedPng.Size.y);
            Require(Finite(input.Ppu) && input.Ppu > 0 && Finite(input.Pivot.x) && Finite(input.Pivot.y) &&
                input.Pivot.x >= 0 && input.Pivot.x <= 1 && input.Pivot.y >= 0 && input.Pivot.y <= 1,
                "Uniform portrait alignment places its pivot outside the PNG; add transparent padding or review the anchors: " + character);
            return input;
        }

        private static void VerifyImported(Prepared input, Sprite corrected, JObject report)
        {
            Require(corrected && corrected.texture.width == input.CorrectedPng.Size.x && corrected.texture.height == input.CorrectedPng.Size.y &&
                corrected.rect.size == (Vector2)input.CorrectedPng.Size && Mathf.Abs(corrected.pixelsPerUnit-input.Ppu) < .001f &&
                Vector2.Distance(corrected.pivot, Vector2.Scale(input.Pivot,input.CorrectedPng.Size)) < .02f,
                "Corrected portrait import resized or moved the source: " + input.Character);
            Vector2 originalWorld = AnchorWorld(input.OriginalAnchor,input.OriginalPng.Size,input.Original.pivot,input.Original.pixelsPerUnit);
            Vector2 correctedWorld = AnchorWorld(input.CorrectedAnchor,input.CorrectedPng.Size,corrected.pivot,corrected.pixelsPerUnit);
            float originalHeight = input.OriginalPng.AlphaBounds.height / input.Original.pixelsPerUnit;
            float correctedHeight = input.CorrectedPng.AlphaBounds.height / corrected.pixelsPerUnit;
            Require(Vector2.Distance(originalWorld,correctedWorld) < .001f && Mathf.Abs(originalHeight-correctedHeight) < .001f,
                "Corrected portrait world alignment differs: " + input.Character);
            report["original"] = Describe(input.Original,input.OriginalPng,input.OriginalAnchor,originalWorld,input.OriginalPath);
            report["corrected"] = Describe(corrected,input.CorrectedPng,input.CorrectedAnchor,correctedWorld,input.Target);
            report["alignment"] = new JObject { ["method"] = "uniform-alpha-height-and-anchor", ["anchor_origin"] = "top-left",
                ["alpha_threshold"] = 16, ["explicit_anchors"] = input.Row["alignment"] != null,
                ["anchor_world_error"] = Vector2.Distance(originalWorld,correctedWorld), ["alpha_height_world_error"] = Mathf.Abs(originalHeight-correctedHeight),
                ["transform_changed"] = false, ["independent_axis_scaling"] = false };
        }

        private static JObject Describe(Sprite sprite, PngInfo png, Vector2 anchor, Vector2 world, string path) => new JObject {
            ["path"] = path, ["size"] = new JArray(png.Size.x,png.Size.y), ["rect"] = new JArray(sprite.rect.x,sprite.rect.y,sprite.rect.width,sprite.rect.height),
            ["pivot_px"] = new JArray(sprite.pivot.x,sprite.pivot.y), ["ppu"] = sprite.pixelsPerUnit,
            ["alpha_bounds_top_left"] = new JArray(png.AlphaBounds.x,png.AlphaBounds.y,png.AlphaBounds.width,png.AlphaBounds.height),
            ["alpha_height_world"] = png.AlphaBounds.height / sprite.pixelsPerUnit,
            ["anchor_px_top_left"] = new JArray(anchor.x,anchor.y), ["anchor_world"] = new JArray(world.x,world.y),
            ["transparent_fraction"] = png.TransparentFraction, ["guid"] = AssetDatabase.AssetPathToGUID(path),
            ["decoded_rgba_sha256"] = png.RgbaSha256
        };

        private static Vector2 AnchorWorld(Vector2 topLeft, Vector2Int size, Vector2 pivot, float ppu) =>
            (new Vector2(topLeft.x,size.y-topLeft.y)-pivot)/ppu;

        private static Vector2 ReadAnchor(JToken value, PngInfo png)
        {
            Vector2 anchor;
            if (value == null) anchor = new Vector2(png.AlphaBounds.x + png.AlphaBounds.width*.5f,png.AlphaBounds.y);
            else
            {
                Require(value is JArray points && points.Count == 2, "Portrait anchor must contain two pixel coordinates.");
                anchor = new Vector2((float)value[0],(float)value[1]);
            }
            Require(Finite(anchor.x) && Finite(anchor.y) && anchor.x >= 0 && anchor.x <= png.Size.x && anchor.y >= 0 && anchor.y <= png.Size.y,
                "Portrait anchor lies outside its PNG.");
            return anchor;
        }

        private static PngInfo ReadPng(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Require(bytes.Length >= 24 && bytes.Take(8).SequenceEqual(new byte[] {137,80,78,71,13,10,26,10}), "Portrait must be a PNG: " + path);
            int width = ReadInt(bytes,16), height = ReadInt(bytes,20);
            Require(width > 0 && height > 0 && width <= 8192 && height <= 8192, "Portrait PNG dimensions are outside the importer limit: " + path);
            Texture2D texture = new Texture2D(2,2,TextureFormat.RGBA32,false,true);
            try
            {
                Require(ImageConversion.LoadImage(texture,bytes,false) && texture.width == width && texture.height == height, "Portrait PNG decode failed: " + path);
                Color32[] pixels = texture.GetPixels32();
                int minX=width,minY=height,maxX=-1,maxY=-1,transparent=0;
                for (int y=0;y<height;y++) for (int x=0;x<width;x++)
                {
                    byte alpha=pixels[y*width+x].a;
                    if (alpha < 16) { transparent++; continue; }
                    minX=Math.Min(minX,x); maxX=Math.Max(maxX,x); minY=Math.Min(minY,y); maxY=Math.Max(maxY,y);
                }
                Require(maxX >= minX && maxY >= minY, "Portrait has no visible alpha pixels: " + path);
                return new PngInfo { Size=new Vector2Int(width,height), AlphaBounds=new RectInt(minX,height-1-maxY,maxX-minX+1,maxY-minY+1),
                    TransparentFraction=(float)transparent/pixels.Length, RgbaSha256=HashRgbaTopLeft(pixels,width,height) };
            }
            finally { UnityEngine.Object.DestroyImmediate(texture); }
        }

        private static int ReadInt(byte[] bytes,int offset) => (bytes[offset]<<24)|(bytes[offset+1]<<16)|(bytes[offset+2]<<8)|bytes[offset+3];
        private static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
        private static bool IsSha256(string value) => value != null && value.Length == 64 && value.All(c =>
            c >= '0' && c <= '9' || c >= 'a' && c <= 'f' || c >= 'A' && c <= 'F');
        internal static string VerifyOriginalPixels(JObject row)
        {
            string character = (string)row["character"], expected = (string)row["original_rgba_sha256"];
            Require(Canvases.ContainsKey(character) && IsSha256(expected), "Original portrait decoded pixel identity is missing: " + character);
            string path = Shared.ResolveBelow(Project, (string)row["original_sprite"]);
            Require(File.Exists(path), "Reviewed original portrait file is missing: " + path);
            var png = ReadPng(path);
            Require(png.Size == Canvases[character] && string.Equals(png.RgbaSha256,expected,StringComparison.OrdinalIgnoreCase),
                "Reviewed original portrait decoded pixels or canvas changed: " + character);
            return png.RgbaSha256;
        }
        private static string HashRgbaTopLeft(Color32[] pixels,int width,int height)
        {
            // GetPixels32 uses bottom-left row order. Hash exact byte channels
            // top-left first, matching the reviewed decoder-independent RGBA8 identity.
            // ReadPng's temporary texture is linear:true, so no color conversion is applied.
            using var sha=SHA256.Create();
            var row=new byte[checked(width*4)];
            for (int y=height-1;y>=0;y--)
            {
                for (int x=0;x<width;x++)
                {
                    Color32 color=pixels[y*width+x]; int offset=x*4;
                    row[offset]=color.r; row[offset+1]=color.g; row[offset+2]=color.b; row[offset+3]=color.a;
                }
                sha.TransformBlock(row,0,row.Length,null,0);
            }
            sha.TransformFinalBlock(Array.Empty<byte>(),0,0);
            return BitConverter.ToString(sha.Hash).Replace("-","").ToLowerInvariant();
        }
        private static void VerifyFile(string path,string expected) => Require(expected != null && expected.Length == 64 && File.Exists(path) &&
            string.Equals(Shared.Hash(path),expected,StringComparison.OrdinalIgnoreCase), "Missing or changed reviewed portrait input: " + path);
        private static void AddHash(JObject hashes,string root,string relative) => hashes[relative]=Shared.Hash(Shared.ResolveBelow(root,relative));
        private static string HashText(string value)
        {
            using (var hash=SHA256.Create()) return BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-","").ToLowerInvariant();
        }
        private static void Require(bool condition,string message) { if (!condition) throw new InvalidDataException(message); }
    }
}
