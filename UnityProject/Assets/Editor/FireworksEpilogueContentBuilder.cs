using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Naninovel;
using Naninovel.ManagedText;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using Shared = Manosaba.EditorTools.SwitchTitleContentBuilder;

namespace Manosaba.EditorTools
{
    /// <summary>Applies the tracked epilogue through the existing sync/build extension gate.</summary>
    public static class FireworksEpilogueContentBuilder
    {
        public const string ScriptPath = "System/System_FireworksEpilogue";
        public const string ScriptTarget = "Assets/GameContent/#WitchTrials/Scenarios/" + ScriptPath + ".asset";
        public const string EndingPath = "Act02_Chapter05/Act02_Chapter05_Adv09";
        public const string EndingTarget = "Assets/GameContent/#WitchTrials/Scenarios/" + EndingPath + ".asset";
        public const string EmptyTarget = "Assets/GameContent/#WitchTrials/FireworksEpilogue/Textures/EpilogueEmpty.png";
        public const string PosterTarget = "Assets/GameContent/#WitchTrials/FireworksEpilogue/Textures/SourcePoster.png";
        public const string TextTarget = "Assets/GameContent/#WitchTrials/ManagedText/Scripts/" + ScriptPath + ".txt";
        public const string LocalizedTextTarget = "Assets/GameContent/#WitchTrials/Localization/zh-Hans/Text/Scripts/" + ScriptPath + ".txt";
        private const string PcSource = "res/Assets/#WitchTrials/Scenarios/" + EndingPath + ".asset.json";
        private const string PlanPath = "res/.manosaba/fireworks-epilogue/plan.json";
        private static string Project => Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        private static string Repo => Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));

        public static bool HandlesScenario(string source, string target, string typeName, string assembly)
        {
            if (target != EndingTarget || typeName != "Naninovel.Script" || assembly != "Elringus.Naninovel.Runtime" ||
                !string.Equals(Path.GetFullPath(source), Shared.ResolveBelow(Repo, PcSource), StringComparison.OrdinalIgnoreCase))
                return false;
            if (!File.Exists(Shared.ResolveBelow(Repo, PlanPath))) return false;
            ReadValidatedPlan();
            return true;
        }

        internal static JObject ReadValidatedPlan()
        {
            var plan = JObject.Parse(File.ReadAllText(Shared.ResolveBelow(Repo, PlanPath)));
            Require((int?)plan["version"] == 1 && plan["signature"]?.ToString().Length == 64,
                "Unsupported fireworks epilogue plan; run normal content sync.");
            Require(plan["input_sha256"] is JObject && plan["generated_hashes"] is JObject,
                "Epilogue plan has no input provenance.");
            foreach (var hashes in new[] { (JObject)plan["input_sha256"], (JObject)plan["generated_hashes"] })
                foreach (var item in hashes.Properties())
                {
                    string path = Shared.ResolveBelow(Repo, item.Name);
                    Require(File.Exists(path) && Shared.Hash(path) == (string)item.Value,
                        "Epilogue recipe input changed or is missing: " + item.Name + "; run normal content sync.");
                }
            var scenarios = plan["scenarios"] as JArray;
            Require(scenarios?.Count == 1 && (string)scenarios[0]["pc_source"] == PcSource &&
                (string)scenarios[0]["target"] == EndingTarget &&
                (string)scenarios[0]["source"] == "res/.manosaba/fireworks-epilogue/Act02_Chapter05_Adv09.asset.json" &&
                (string)scenarios[0]["pc_sha256"] == Shared.Hash(Shared.ResolveBelow(Repo, PcSource)),
                "Epilogue ending ownership/source no longer matches the current PC input.");
            Require((string)plan["scenario"]?["source"] == "res/.manosaba/fireworks-epilogue/System_FireworksEpilogue.nani" &&
                (string)plan["scenario"]?["target"] == ScriptTarget && (string)plan["scenario"]?["script_path"] == ScriptPath &&
                plan["scenario"]?["text_map"] is JObject map && map.Count > 0 && plan["scenario"]?["pages"] is JArray,
                "Epilogue compiled script recipe is invalid.");
            var leaves = plan["leaves"] as JArray;
            Require(leaves?.Count == 2 && (string)leaves[0]["target"] == EmptyTarget && (bool?)leaves[0]["sprite"] == false &&
                (string)leaves[0]["source"] == "content-overrides/fireworks-epilogue/empty-rooftop.png" &&
                (string)leaves[0]["npot_scale"] == "ToNearest" && (int?)leaves[0]["max_texture_size"] == 2048 &&
                JToken.DeepEquals(leaves[0]["imported_size"], new JArray(2048, 1024)) &&
                (string)leaves[1]["target"] == PosterTarget && (bool?)leaves[1]["sprite"] == false &&
                (string)leaves[1]["source"] == "content-overrides/fireworks-epilogue/source-poster.png",
                "Epilogue texture targets are outside the reviewed scope.");
            foreach (var leaf in leaves.OfType<JObject>()) Shared.ValidateTextureRecipe(Repo, Project, leaf);
            var documents = plan["text_documents"] as JArray;
            Require(documents?.Count == 2 && documents.Select(d => (string)d["target"]).OrderBy(p => p, StringComparer.Ordinal)
                .SequenceEqual(new[] { TextTarget, LocalizedTextTarget }.OrderBy(p => p, StringComparer.Ordinal)) &&
                documents.All(d => (string)d["script_path"] == ScriptPath && (string)d["content_locale"] == "zh-Hans" &&
                    JToken.DeepEquals(d["records"], plan["scenario"]["text_map"])),
                "Epilogue managed text documents must preserve every authored page and use the reviewed paths.");
            var expected = new Dictionary<string, string> {
                [ScriptTarget] = "Naninovel/Scripts/" + ScriptPath,
                [EndingTarget] = "Naninovel/Scripts/" + EndingPath,
                [EmptyTarget] = "Naninovel/Backgrounds/Stills/EpilogueEmpty",
                [PosterTarget] = "Naninovel/FireworksEpilogue/SourcePoster",
                [TextTarget] = "Naninovel/Text/Scripts/" + ScriptPath,
                [LocalizedTextTarget] = "Naninovel/Localization/zh-Hans/Text/Scripts/" + ScriptPath };
            var addresses = plan["addresses"] as JArray;
            Require(addresses?.Count == expected.Count && addresses.OfType<JObject>().All(row =>
                expected.TryGetValue((string)row["path"] ?? "", out var address) && address == (string)row["address"] &&
                row["labels"] is JArray) && addresses.Select(row => (string)row["path"]).Distinct().Count() == expected.Count,
                "Epilogue address plan differs from the reviewed six targets.");
            return plan;
        }

        public static JObject EnsureApplied()
        {
            string input = Shared.ResolveBelow(Repo, PlanPath);
            if (!File.Exists(input)) return new JObject { ["status"] = "disabled", ["reason"] = "No local epilogue plan" };
            string statePath = Path.Combine(Project, "Library/ManosabaContent/fireworks-epilogue-state.json");
            var report = new JObject { ["status"] = "ERROR", ["plan"] = PlanPath,
                ["generated_utc"] = DateTime.UtcNow.ToString("o"), ["saved_state_changed"] = false };
            Script staged = null;
            try
            {
                Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Synchronize epilogue content outside Play mode.");
                JObject plan = ReadValidatedPlan();
                report["signature"] = plan["signature"].DeepClone();
                report["plan_sha256"] = Shared.Hash(input);
                JObject previous = Shared.ReadState(statePath);
                bool cached = (string)previous?["status"] == "OK" &&
                    (string)previous["signature"] == (string)plan["signature"] &&
                    previous["output_sha256"] is JObject hashes && hashes.Count == 12 && hashes.Properties().All(item =>
                        File.Exists(Shared.ResolveBelow(Project, item.Name)) && Shared.Hash(Shared.ResolveBelow(Project, item.Name)) == (string)item.Value);
                report["cache"] = new JObject { ["hit"] = cached };
                // Compile and validate every command and page before replacing any source-derived asset.
                staged = Compile((JObject)plan["scenario"]);
                VerifyDependencies();
                // Use the same serializer/parser as the project's localized scenarios.
                // Validate both documents before replacing any generated asset.
                var documents = ((JArray)plan["text_documents"]).OfType<JObject>()
                    .Select(recipe => (Recipe: recipe, Text: SerializeTextDocument(recipe))).ToArray();
                bool textChanged = false;
                var textReport = new JArray();
                foreach (var document in documents)
                {
                    string target = (string)document.Recipe["target"];
                    string path = Shared.ResolveBelow(Project, target);
                    string before = AssetDatabase.AssetPathToGUID(target);
                    bool changed = !File.Exists(path) || File.ReadAllText(path) != document.Text;
                    if (changed)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.WriteAllText(path, document.Text, new UTF8Encoding(false));
                        AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceSynchronousImport);
                        textChanged = true;
                    }
                    var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(target);
                    VerifyTextDocument(asset, document.Recipe);
                    Require(string.IsNullOrEmpty(before) || before == AssetDatabase.AssetPathToGUID(target), "Epilogue managed text GUID changed.");
                    textReport.Add(new JObject { ["target"] = target, ["guid"] = AssetDatabase.AssetPathToGUID(target),
                        ["records"] = ((JObject)document.Recipe["records"]).Count, ["values_verified"] = true, ["changed"] = changed });
                }
                report["text_documents"] = textReport;
                bool texturesChanged = false;
                var textureReport = new JArray();
                foreach (var leaf in ((JArray)plan["leaves"]).OfType<JObject>())
                {
                    string target = (string)leaf["target"];
                    string before = AssetDatabase.AssetPathToGUID(target);
                    var leafReport = new JObject { ["target"] = target, ["guid_before"] = before };
                    string destination = Shared.ResolveBelow(Project, target);
                    string image = Shared.ResolveBelow(Repo, (string)leaf["source"]);
                    bool copied = !File.Exists(destination) || Shared.Hash(destination) != Shared.Hash(image);
                    if (copied)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destination));
                        File.Copy(image, destination, true); // Keep the collaborator's existing .meta/GUID.
                        AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceSynchronousImport);
                    }
                    bool imported = target == EmptyTarget ? EnsureEmptyTexture(leaf, leafReport) : Shared.EnsureTexture(Repo, leaf, leafReport);
                    Require(string.IsNullOrEmpty(before) || before == AssetDatabase.AssetPathToGUID(target), "Epilogue texture GUID changed.");
                    leafReport["copied"] = copied;
                    textureReport.Add(leafReport);
                    texturesChanged |= copied || imported;
                }
                report["leaves"] = textureReport;
                bool rebuild = !cached || texturesChanged || textChanged;
                report["rebuilt"] = rebuild;
                var scenario = (JObject)plan["scenarios"][0];
                JObject endingResult;
                if (rebuild)
                {
                    string guid = AssetDatabase.AssetPathToGUID(ScriptTarget);
                    var existing = AssetDatabase.LoadAssetAtPath<Script>(ScriptTarget);
                    Require(!File.Exists(Shared.ResolveBelow(Project, ScriptTarget)) || existing,
                        "Existing epilogue target is not a readable Script; it was retained.");
                    if (existing)
                    {
                        Require(existing.Path == ScriptPath, "Existing script belongs to a different logical path.");
                        EditorUtility.CopySerialized(staged, existing);
                        EditorUtility.SetDirty(existing);
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Shared.ResolveBelow(Project, ScriptTarget)));
                        AssetDatabase.CreateAsset(staged, ScriptTarget);
                        staged = null;
                    }
                    AssetDatabase.SaveAssets();
                    Require(string.IsNullOrEmpty(guid) || guid == AssetDatabase.AssetPathToGUID(ScriptTarget), "Epilogue Script GUID changed.");
                    var result = ScriptableObjectConverter.RebuildScenario(Shared.ResolveBelow(Repo, (string)scenario["source"]), EndingTarget);
                    Require(result.status == "CONVERTED" && result.managed_references_failed == 0 && result.needs_binding == 0 &&
                        result.params_with_value_dump == result.params_with_value_written,
                        "Epilogue ending conversion failed: " + result.error);
                    endingResult = JObject.FromObject(result);
                }
                else endingResult = (JObject)previous["scenarios"][0].DeepClone();
                endingResult["pc_source"] = scenario["pc_source"].DeepClone();
                endingResult["pc_sha256"] = scenario["pc_sha256"].DeepClone();
                endingResult["conversion_diagnostics_cached"] = !rebuild;
                report["scenarios"] = new JArray(endingResult);
                var actual = AssetDatabase.LoadAssetAtPath<Script>(ScriptTarget);
                VerifyScript(actual, (JObject)plan["scenario"]);
                report["compiled_script"] = new JObject { ["target"] = ScriptTarget, ["guid"] = AssetDatabase.AssetPathToGUID(ScriptTarget),
                    ["commands"] = actual.Playlist.Count, ["pages"] = ((JArray)plan["scenario"]["pages"]).Count,
                    ["text_entries"] = actual.TextMap.Map.Count, ["text_verified"] = true };
                report["ending_route"] = VerifyEnding(AssetDatabase.LoadAssetAtPath<Script>(EndingTarget),
                    JObject.Parse(File.ReadAllText(Shared.ResolveBelow(Repo, (string)scenario["source"]))));
                report["ending_merge"] = scenario["merge"].DeepClone();
                report["addresses"] = Shared.EnsureAddresses((JArray)plan["addresses"], "Manosaba.FireworksEpilogue");
                AssetDatabase.SaveAssets();
                var output = new JObject();
                foreach (string path in new[] { ScriptTarget, EndingTarget, EmptyTarget, PosterTarget, TextTarget, LocalizedTextTarget }.OrderBy(p => p, StringComparer.Ordinal))
                {
                    output[path] = Shared.Hash(Shared.ResolveBelow(Project, path));
                    output[path + ".meta"] = Shared.Hash(Shared.ResolveBelow(Project, path + ".meta"));
                }
                report["output_sha256"] = output;
                using (var sha = SHA256.Create()) report["output_fingerprint"] = BitConverter.ToString(sha.ComputeHash(
                    Encoding.UTF8.GetBytes(output.ToString(Newtonsoft.Json.Formatting.None)))).Replace("-", "").ToLowerInvariant();
                report["status"] = "OK";
                Shared.WriteState(statePath, report);
                return report;
            }
            catch (Exception error)
            {
                report["error"] = error.ToString();
                try { Shared.WriteState(statePath, report); }
                catch (Exception publishError) { Debug.LogError("Could not publish epilogue error: " + publishError); }
                throw;
            }
            finally { if (staged) UnityEngine.Object.DestroyImmediate(staged); }
        }

        private static bool EnsureEmptyTexture(JObject leaf, JObject report)
        {
            // SpriteBackground renders Texture2D; differing texture dimensions
            // change TransitionalSpriteRenderer's mesh/UV calculation on reveal.
            // Use Unity's importer to normalize this one generated texture, not
            // the authored PNG. A Sprite sub-asset and its PPU are not involved.
            Require((string)leaf["target"] == EmptyTarget, "Only the epilogue empty background may be normalized.");
            var importer = AssetImporter.GetAtPath(EmptyTarget) as TextureImporter;
            Require(importer, "Missing epilogue empty background importer.");
            bool changed = importer.textureType != TextureImporterType.Default || importer.maxTextureSize != 2048 ||
                importer.npotScale != TextureImporterNPOTScale.ToNearest || importer.mipmapEnabled ||
                importer.textureCompression != TextureImporterCompression.Uncompressed ||
                importer.wrapModeU != TextureWrapMode.Clamp || importer.wrapModeV != TextureWrapMode.Clamp ||
                importer.wrapModeW != TextureWrapMode.Clamp;
            if (changed)
            {
                importer.textureType = TextureImporterType.Default;
                importer.maxTextureSize = 2048;
                importer.npotScale = TextureImporterNPOTScale.ToNearest;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.mipmapEnabled = false;
                importer.wrapMode = TextureWrapMode.Clamp;
                importer.SaveAndReimport();
            }
            importer = AssetImporter.GetAtPath(EmptyTarget) as TextureImporter;
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(EmptyTarget);
            Require(importer && importer.textureType == TextureImporterType.Default &&
                importer.npotScale == TextureImporterNPOTScale.ToNearest && importer.maxTextureSize == 2048 &&
                texture && texture.width == 2048 && texture.height == 1024,
                "Epilogue empty texture did not import as 2048x1024 with ToNearest; check importer/platform overrides.");
            importer.GetSourceTextureWidthAndHeight(out int sourceWidth, out int sourceHeight);
            Require(sourceWidth == (int)leaf["size"][0] && sourceHeight == (int)leaf["size"][1] &&
                Shared.Hash(Shared.ResolveBelow(Project, EmptyTarget)) == Shared.Hash(Shared.ResolveBelow(Repo, (string)leaf["source"])),
                "Epilogue normalization changed or lost the original source image.");
            string guid = AssetDatabase.AssetPathToGUID(EmptyTarget);
            Require(string.IsNullOrEmpty((string)report["guid_before"]) || (string)report["guid_before"] == guid,
                "Epilogue normalized texture GUID changed.");
            report["guid"] = guid;
            report["importer_changed"] = changed;
            report["source_width"] = sourceWidth;
            report["source_height"] = sourceHeight;
            report["width"] = texture.width;
            report["height"] = texture.height;
            report["normalization"] = "Unity TextureImporter.ToNearest";
            report["source_bytes_preserved"] = true;
            report["verified"] = true;
            return changed;
        }

        private static Script Compile(JObject recipe)
        {
            string source = Shared.ResolveBelow(Repo, (string)recipe["source"]);
            var errors = new List<ScriptParseError>();
            Script parsed = Compiler.ScriptAssetParser.ParseText(ScriptPath, File.ReadAllText(source), new ParseOptions(errors, false));
            try
            {
                Require(errors.Count == 0, "Epilogue parse failed: " + string.Join("; ", errors.Select(e => e.ToString(source))));
                var text = ((JObject)recipe["text_map"]).Properties().ToDictionary(p => p.Name, p => (string)p.Value, StringComparer.Ordinal);
                var script = Script.Create(ScriptPath, parsed.Lines.ToArray(), new ScriptTextMap(text));
                try { VerifyScript(script, recipe); return script; }
                catch { UnityEngine.Object.DestroyImmediate(script); throw; }
            }
            finally { if (parsed) UnityEngine.Object.DestroyImmediate(parsed); }
        }

        private static string SerializeTextDocument(JObject recipe)
        {
            var records = ((JObject)recipe["records"]).Properties()
                .Select(p => new ManagedTextRecord(p.Name, (string)p.Value, null));
            string text = ManagedTextUtils.SerializeMultiline(new ManagedTextDocument(records), 1);
            VerifyTextRecords(text, recipe);
            return text;
        }

        internal static void VerifyTextDocument(TextAsset asset, JObject recipe)
        {
            Require(asset, "Epilogue managed text asset is missing: " + recipe["target"]);
            VerifyTextRecords(asset.text, recipe);
        }

        private static void VerifyTextRecords(string text, JObject recipe)
        {
            var expected = (JObject)recipe["records"];
            var records = new ScriptLocalizationParser().Parse(text).Records.ToArray();
            Require(records.Length == expected.Count && records.Select(r => r.Key).SequenceEqual(expected.Properties().Select(p => p.Name)) &&
                records.All(r => (string)expected[r.Key] == r.Value),
                "Epilogue managed text lost page IDs, order or authored values during serialization.");
        }

        internal static void VerifyScript(Script script, JObject recipe)
        {
            Require(script && script.Path == ScriptPath && script.Playlist.Count > 0 && script.Playlist.All(c => c != null), "Missing compiled epilogue graph.");
            var map = (JObject)recipe["text_map"];
            Require(script.TextMap.Map.Count == map.Count && map.Properties().All(p => script.TextMap.GetTextOrNull(p.Name) == (string)p.Value),
                "Epilogue text did not survive compilation/readback exactly.");
            var commands = script.Playlist.ToArray();
            Require(commands[0].GetType().Name == "BeginFireworksEpilogue" && commands[^2].GetType().Name == "FinishFireworksEpilogue" &&
                commands[^1].GetType().Name == "ReturnToTitle", "Epilogue session boundaries are invalid.");
            Require(!commands.Any(c => c.GetType().Name is "AutoSave" or "QuickSave" or "SetCustomVariable" or "GosubResetWitchBook"),
                "Epilogue contains a forbidden main-story state/save command.");
            var prints = commands.OfType<Naninovel.Commands.PrintText>().ToArray();
            Require(prints.Length == map.Count && prints.SelectMany(p => p.Text.RawValue.Value.Parts)
                .Where(p => !string.IsNullOrEmpty(p.Id)).Select(p => p.Id).SequenceEqual(map.Properties().Select(p => p.Name)),
                "Epilogue page IDs/order were changed by the compiler.");
            int finalPrint = Array.FindLastIndex(commands, c => c is Naninovel.Commands.PrintText);
            Require(finalPrint > 0 && commands[finalPrint - 1].GetType().Name == "ShowFireworksEpilogueCredits" &&
                commands.Count(c => c.GetType().Name == "ShowFireworksEpilogueCredits") == 1,
                "Epilogue must show the original poster credits immediately before its final body page.");
        }

        internal static JObject VerifyEnding(Script script, JObject source)
        {
            Require(script && script.Path == EndingPath && script.Playlist.Count > 0, "Patched PC ending did not load.");
            var last = script.Playlist.Last();
            long rid = (long)source["playlist"]["commands"].Last()["rid"];
            var expected = (JObject)((JArray)source["references"]["RefIds"]).Single(row => (long)row["rid"] == rid);
            var data = (JObject)expected["data"];
            const string routeType = "WitchTrials.Views.RouteFireworksEpilogue";
            Require(last.GetType().FullName == routeType &&
                (string)expected["type"]["ns"] + "." + (string)expected["type"]["class"] == routeType &&
                script.Playlist.Count == ((JArray)source["playlist"]["commands"]).Count &&
                last.Indent == (int)data["indent"] &&
                JToken.DeepEquals(JObject.Parse(JsonUtility.ToJson(last.PlaybackSpot)), data["playbackSpot"]),
                "PC ending did not preserve its settings-aware route and playback position.");
            // The Editor assembly cannot directly reference the game's asmdef.
            // Check the actual deserialized public parameters through Naninovel's API.
            var parameters = new JObject();
            foreach (string field in new[] { "FadeDuration", "MinDelayDuration", "Wait", "ConditionalExpression" })
            {
                var parameter = last.GetType().GetField(field)?.GetValue(last) as ICommandParameter;
                var wanted = data[field] as JObject;
                Require(parameter != null && wanted != null, "Ending route parameter is missing: " + field);
                var serialized = JObject.Parse(JsonUtility.ToJson(parameter));
                bool sameValue = parameter is DecimalParameter number
                    ? number.Value == (float)wanted["value"]
                    : parameter is BooleanParameter boolean
                        ? boolean.Value == ((int)wanted["value"] != 0)
                        : JToken.DeepEquals(serialized["value"], wanted["value"]);
                Require(parameter.HasValue == ((int)wanted["hasValue"] != 0) && sameValue &&
                    new[] { "raw", "spot" }.All(key => JToken.DeepEquals(serialized[key], wanted[key])),
                    "Ending route changed its original return parameter: " + field);
                parameters[field] = serialized;
            }
            return new JObject { ["command"] = routeType, ["enabled_destination"] = ScriptPath,
                ["disabled_destination"] = "ReturnToTitle", ["return_parameters_verified"] = true,
                ["parameters"] = parameters };
        }

        private static void VerifyDependencies()
        {
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            Require(settings, "Core Addressables settings are missing.");
            foreach (string address in new[] { "Naninovel/Backgrounds/Stills/3_1", "Naninovel/Spawn/TitleScreenEffect_3" })
            {
                var matches = settings.groups.Where(g => g).SelectMany(g => g.entries).Where(e => e.address == address).ToArray();
                Require(matches.Length == 1 && AssetDatabase.LoadMainAssetAtPath(matches[0].AssetPath),
                    "Missing/ambiguous epilogue dependency: " + address + "; prepare the Switch title content first.");
                if (address == "Naninovel/Backgrounds/Stills/3_1")
                {
                    var full = AssetDatabase.LoadAssetAtPath<Texture2D>(matches[0].AssetPath);
                    Require(full && full.width == 2048 && full.height == 1024,
                        "Epilogue full CG must have the same 2048x1024 imported canvas as the normalized empty background.");
                }
            }
        }

        private static void Require(bool condition, string message)
        { if (!condition) throw new InvalidOperationException(message); }
    }
}
