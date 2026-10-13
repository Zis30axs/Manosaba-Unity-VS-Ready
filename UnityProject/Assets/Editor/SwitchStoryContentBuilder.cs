using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.SceneManagement;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Shared = Manosaba.EditorTools.SwitchTitleContentBuilder;

namespace Manosaba.EditorTools
{
    /// <summary>Applies the optional, already-merged Switch story recipe through the normal content pipeline.</summary>
    public static class SwitchStoryContentBuilder
    {
        private const string PlanPath = "res/.manosaba/switch-story/plan.json";
        private const string SourceRoot = "res/.manosaba/switch-story/";
        private const string TargetRoot = "Assets/GameContent/#WitchTrials/SwitchStory/";
        private const string ScenarioRoot = "Assets/GameContent/#WitchTrials/Scenarios/";
        private const string PcRoot = "res/Assets/#WitchTrials/Scenarios/";
        private const string GalleryTarget = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/GalleryUI.prefab";
        private const string StillAddress = "Naninovel/Backgrounds/Stills/";
        private static string Project => Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        private static string Repo => Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));

        internal static JObject ReadValidatedPlan()
        {
            var plan = JObject.Parse(File.ReadAllText(Shared.ResolveBelow(Repo, PlanPath)));
            ValidatePlan(Repo, Project, plan);
            return plan;
        }

        public static bool HandlesScenario(string source, string target, string typeName, string assembly)
        {
            if (typeName != "Naninovel.Script" || assembly != "Elringus.Naninovel.Runtime") return false;
            string path = Shared.ResolveBelow(Repo, PlanPath);
            if (!File.Exists(path)) return false;
            JObject plan = JObject.Parse(File.ReadAllText(path));
            RequireShape(plan);
            var matches = ((JArray)plan["scenarios"]).OfType<JObject>().Where(row => (string)row["target"] == target).ToArray();
            if (matches.Length == 0) return false;
            Require(matches.Length == 1, "Duplicate story scenario target: " + target);
            JObject scenario = matches[0];
            if (!string.Equals(Path.GetFullPath(source), Shared.ResolveBelow(Repo, (string)scenario["pc_source"]), StringComparison.OrdinalIgnoreCase))
                return false;
            // Validate only this delegation here. EnsureApplied validates every source before writing;
            // hashing all large textures once per scenario would make normal sync quadratic.
            ValidateScenario(Repo, Project, plan, scenario);
            return true;
        }

        public static JObject EnsureApplied()
        {
            string repo = Repo, project = Project, input = Shared.ResolveBelow(repo, PlanPath);
            if (!File.Exists(input)) return new JObject { ["status"] = "disabled", ["reason"] = "No local Switch story plan" };
            string statePath = Path.Combine(project, "Library/ManosabaContent/switch-story-state.json");
            var report = new JObject { ["status"] = "ERROR", ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["plan"] = PlanPath, ["render_verified"] = false };
            try
            {
                Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Switch story content must be synchronized outside Play Mode.");
                JObject plan = JObject.Parse(File.ReadAllText(input));
                ValidatePlan(repo, project, plan);
                report["objection_voice_dependencies"] = VerifyObjectionVoiceDependencies(plan);
                report["signature"] = plan["signature"].DeepClone();
                report["plan_sha256"] = Shared.Hash(input);
                var previous = Shared.ReadState(statePath);
                var hashes = previous?["output_sha256"] as JObject;
                bool signatureMatches = (string)previous?["signature"] == (string)plan["signature"];
                bool planMatches = (string)previous?["plan_sha256"] == (string)report["plan_sha256"];
                var mismatches = new JArray(hashes?.Properties().Where(row =>
                    !File.Exists(Shared.ResolveBelow(project, row.Name)) ||
                    Shared.Hash(Shared.ResolveBelow(project, row.Name)) != (string)row.Value).Select(row => row.Name) ?? Enumerable.Empty<string>());
                bool outputsMatch = hashes != null && hashes.HasValues && mismatches.Count == 0;
                bool targetsPresent = hashes != null && Targets(plan).All(target =>
                    File.Exists(Shared.ResolveBelow(project, target)) && !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(target)) && hashes[target] != null);
                bool cached = (string)previous?["status"] == "OK" && signatureMatches && planMatches && outputsMatch && targetsPresent;
                report["cache"] = new JObject { ["hit"] = cached, ["previous_status"] = previous?["status"]?.DeepClone(),
                    ["signature_matches"] = signatureMatches, ["plan_hash_matches"] = planMatches,
                    ["output_hashes_match"] = outputsMatch, ["targets_present"] = targetsPresent,
                    ["output_hash_mismatches"] = mismatches };
                if (!cached) Shared.RequireCleanScenes();
                var leaves = (JArray)plan["leaves"];
                var leafResults = new JArray();
                bool copied = false;
                foreach (JObject leaf in leaves)
                {
                    string source = Shared.ResolveBelow(repo, (string)leaf["source"]);
                    string target = (string)leaf["target"], destination = Shared.ResolveBelow(project, target);
                    string hash = Shared.Hash(source);
                    bool changed = !File.Exists(destination) || Shared.Hash(destination) != hash;
                    var row = new JObject { ["target"] = target, ["sha256"] = hash, ["copied"] = changed,
                        ["guid_before"] = AssetDatabase.AssetPathToGUID(target) };
                    if (changed)
                    {
                        Shared.RequireCleanScenes();
                        Directory.CreateDirectory(Path.GetDirectoryName(destination));
                        File.Copy(source, destination, true); // Existing .meta is never replaced.
                        copied = true;
                    }
                    leafResults.Add(row);
                }
                if (copied) AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
                bool importerChanged = false;
                for (int i = 0; i < leaves.Count; i++)
                    importerChanged |= (string)leaves[i]["kind"] == "audio"
                        ? EnsureAudio((JObject)leaves[i], (JObject)leafResults[i])
                        : EnsureStoryTexture(repo, (JObject)leaves[i], (JObject)leafResults[i]);
                report["leaves"] = leafResults;
                bool rebuild = !cached || copied || importerChanged;
                report["rebuilt"] = rebuild;
                report["rebuild_reasons"] = new JArray(new[] { !cached ? "cache_miss" : null,
                    copied ? "leaf_bytes_changed" : null, importerChanged ? "import_settings_changed" : null }.Where(reason => reason != null));
                if (rebuild && ((JArray)plan["jobs"]).Count > 0)
                    report["jobs"] = RebuildPrefabs(repo, (JArray)plan["jobs"]);
                else report["jobs"] = previous?["jobs"]?.DeepClone() ?? new JArray();
                var prefabChecks = new JArray();
                foreach (JObject job in (JArray)plan["jobs"])
                    prefabChecks.Add(VerifyPrefab(AssetDatabase.LoadAssetAtPath<GameObject>((string)job["target"]), job));
                report["verified_prefabs"] = prefabChecks;
                var results = new JArray();
                foreach (JObject scenario in (JArray)plan["scenarios"])
                {
                    string target = (string)scenario["target"];
                    JObject converted;
                    if (rebuild)
                    {
                        var result = ScriptableObjectConverter.RebuildScenario(Shared.ResolveBelow(repo, (string)scenario["source"]), target);
                        Require(result.status == "CONVERTED" && result.managed_references_failed == 0 && result.needs_binding == 0 &&
                            result.params_with_value_dump == result.params_with_value_written,
                            "Switch story scenario failed: " + target + ": " + result.error);
                        converted = JObject.FromObject(result);
                    }
                    else
                    {
                        converted = ((JArray)previous["scenarios"]).OfType<JObject>().Single(row => (string)row["target"] == target);
                        converted = (JObject)converted.DeepClone();
                    }
                    var script = AssetDatabase.LoadAssetAtPath<Naninovel.Script>(target);
                    var source = JObject.Parse(File.ReadAllText(Shared.ResolveBelow(repo, (string)scenario["source"])));
                    Require(script && script.Path == (string)source["path"] && script.Playlist != null && script.Lines != null &&
                        script.Playlist.Count == ((JArray)source["playlist"]["commands"]).Count &&
                        script.Lines.Count == ((JArray)source["lines"]).Count &&
                        script.Playlist.All(command => command != null) && script.Lines.All(line => line != null),
                        "Story scenario failed actual graph readback: " + target);
                    converted["pc_source"] = scenario["pc_source"].DeepClone();
                    converted["pc_sha256"] = scenario["pc_sha256"].DeepClone();
                    converted["commands"] = script.Playlist.Count;
                    converted["dynamic_objection_calls"] = VerifyDynamicObjectionCalls(script, source, scenario);
                    converted["objection_voice_pairs"] = VerifyObjectionVoicePairs(script, source, scenario);
                    converted["conversion_diagnostics_cached"] = !rebuild;
                    results.Add(converted);
                }
                report["scenarios"] = results;
                report["gallery"] = ApplyGallery(plan);
                report["addresses"] = Shared.EnsureAddresses((JArray)plan["addresses"], "Manosaba.SwitchStory");
                var output = new JObject();
                var dependencies = AssetDatabase.GetDependencies(((JArray)plan["jobs"]).Select(job => (string)job["target"]).ToArray(), true)
                    .Where(path => path.StartsWith(TargetRoot, StringComparison.Ordinal));
                // Fingerprint the durable asset state after all recipe work. Importer
                // policy conflicts are diagnosed separately via rebuild_reasons above.
                AssetDatabase.SaveAssets();
                foreach (string target in Targets(plan).Concat(dependencies).Distinct(StringComparer.Ordinal).OrderBy(value => value, StringComparer.Ordinal))
                {
                    string full = Shared.ResolveBelow(project, target);
                    output[target] = Shared.Hash(full);
                    Require(File.Exists(full + ".meta"), "Generated asset has no stable metadata: " + target);
                    output[target + ".meta"] = Shared.Hash(full + ".meta");
                }
                report["output_sha256"] = output;
                using (var sha = SHA256.Create())
                    report["output_fingerprint"] = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(output.ToString(Newtonsoft.Json.Formatting.None))))
                        .Replace("-", "").ToLowerInvariant();
                report["status"] = "OK";
                Shared.WriteState(statePath, report);
                return report;
            }
            catch (Exception error)
            {
                report["error"] = error.ToString();
                try { Shared.WriteState(statePath, report); }
                catch (Exception writeError) { Debug.LogError("Could not save Switch story failure: " + writeError.Message); }
                throw;
            }
        }

        private sealed class GlassMesh
        {
            public Vector2[] localVertices;
            public Vertex2DMetaData[] importerVertices;
            public int[] indices;
            public string sourcePid;
        }

        // This adapter intentionally covers only the three reviewed W crack stages.
        // Their PNGs retain the full source canvas; vertices are not atlas coordinates.
        private static GlassMesh ReadGlassMesh(JObject leaf)
        {
            string target = (string)leaf["target"];
            string name = null, pid = null;
            int vertexCount = 0, indexCount = 0;
            string[] pids = { "2912700173187937077", "1974279541955088290", "-6803846092496667308" };
            int[] vertices = { 211, 207, 230 }, indices = { 627, 615, 708 };
            for (int i = 0; i < pids.Length; i++)
            {
                string candidate = "CutIn_Normal_W_Fx_StainedGlass_00" + (i + 1);
                string prefix = TargetRoot + "Textures/" + candidate + "_" + pids[i] + "_";
                if (target != null && target.StartsWith(prefix, StringComparison.Ordinal))
                {
                    Require(Regex.IsMatch(target.Substring(prefix.Length), "^[0-9a-f]{8}_sprite\\.png$"), "Invalid W glass target identity.");
                    name = candidate; pid = pids[i]; vertexCount = vertices[i]; indexCount = indices[i];
                    break;
                }
            }
            if (pid == null)
            {
                Require(leaf["sprite_geometry"] == null, "Unreviewed story sprite geometry: " + target);
                return null;
            }
            var mesh = leaf["sprite_geometry"] as JObject;
            Require(mesh != null && (int?)mesh["version"] == 1 && (string)mesh["coordinate_space"] == "sprite_local_units" &&
                (string)mesh["source_bundle"] == "general-sprites-cutin_normal_w_assets_all.bundle" &&
                (string)mesh["source_path_id"] == pid && (string)mesh["source_name"] == name &&
                (bool?)leaf["sprite"] == true && (int?)mesh["atlas_settings_raw"] == 67,
                "Missing or unreviewed W glass geometry recipe: " + target);
            Require(MeshVector(mesh["source_rect"], 4).SequenceEqual(new[] { 0f, 0f, 1895f, 817f }) &&
                MeshVector(mesh["source_pivot"], 2).SequenceEqual(new[] { .5f, .5f }) &&
                MeshVector(leaf["size"], 2).SequenceEqual(new[] { 1895f, 817f }) &&
                MeshVector(leaf["pivot"], 2).SequenceEqual(new[] { .5f, .5f }) &&
                MeshNumber(mesh["source_ppu"]) == 100f && MeshNumber(leaf["ppu"]) == 100f &&
                MeshVector(mesh["canvas_uv_offset"], 2).SequenceEqual(new[] { 947.5f, 408.5f }),
                "W glass geometry canvas/UV proof differs: " + target);
            float[] cropOffset = MeshVector(mesh["texture_rect_offset"], 2);
            Require(Mathf.Abs(cropOffset[0] - 49.076122f) < .00001f && Mathf.Abs(cropOffset[1] - 50.056374f) < .00001f,
                "W glass source crop offset differs: " + target);
            var vertexData = mesh["vertices"] as JArray;
            var indexData = mesh["indices"] as JArray;
            Require(vertexData != null && vertexData.Count == vertexCount && indexData != null && indexData.Count == indexCount,
                "W glass geometry count differs: " + target);
            var result = new GlassMesh { sourcePid = pid, localVertices = new Vector2[vertexCount],
                importerVertices = new Vertex2DMetaData[vertexCount], indices = new int[indexCount] };
            for (int i = 0; i < vertexCount; i++)
            {
                float[] point = MeshVector(vertexData[i], 2);
                var local = new Vector2(point[0], point[1]);
                Vector2 pixel = local * 100f + new Vector2(947.5f, 408.5f);
                Require(pixel.x >= 0 && pixel.x <= 1895 && pixel.y >= 0 && pixel.y <= 817, "W glass vertex leaves its canvas.");
                result.localVertices[i] = local;
                result.importerVertices[i] = new Vertex2DMetaData { position = pixel };
            }
            for (int i = 0; i < indexCount; i++)
            {
                Require(indexData[i].Type == JTokenType.Integer, "W glass triangle index must be an integer.");
                long index = (long)indexData[i];
                Require(index >= 0 && index < vertexCount, "W glass triangle index is outside its vertex array.");
                result.indices[i] = (int)index;
            }
            int degenerate = 0;
            for (int i = 0; i < indexCount; i += 3)
            {
                int a = result.indices[i], b = result.indices[i + 1], c = result.indices[i + 2];
                Require(a != b && b != c && a != c, "W glass triangle repeats an index.");
                Vector2 ab = result.localVertices[b] - result.localVertices[a], ac = result.localVertices[c] - result.localVertices[a];
                if (Math.Abs((double)ab.x * ac.y - (double)ab.y * ac.x) < 1e-10) degenerate++;
            }
            // Source stage003 includes two collinear triangles; keep that topology.
            Require(degenerate == (pid == pids[2] ? 2 : 0) && (int?)mesh["source_degenerate_triangles"] == degenerate,
                "W glass source degenerate triangle count differs.");
            return result;
        }

        private static float MeshNumber(JToken token)
        {
            Require(token != null && (token.Type == JTokenType.Float || token.Type == JTokenType.Integer), "Mesh coordinate must be numeric.");
            double value = (double)token;
            Require(!double.IsNaN(value) && !double.IsInfinity(value) && Math.Abs(value) <= 100000, "Mesh coordinate is not finite or bounded.");
            return (float)value;
        }

        private static float[] MeshVector(JToken token, int count)
        {
            Require(token is JArray array && array.Count == count, "Invalid mesh coordinate vector.");
            return ((JArray)token).Select(MeshNumber).ToArray();
        }

        private static bool MeshMatches(GlassMesh expected, Vertex2DMetaData[] vertices, int[] indices)
        {
            return vertices != null && indices != null && vertices.Length == expected.importerVertices.Length &&
                indices.SequenceEqual(expected.indices) && vertices.Select((vertex, i) =>
                    Vector2.Distance(vertex.position, expected.importerVertices[i].position) <= .0001f &&
                    vertex.boneWeight.weight0 == 0 && vertex.boneWeight.weight1 == 0 &&
                    vertex.boneWeight.weight2 == 0 && vertex.boneWeight.weight3 == 0).All(value => value);
        }

        private static bool EnsureStoryTexture(string repo, JObject leaf, JObject report)
        {
            bool textureChanged = Shared.EnsureTexture(repo, leaf, report);
            GlassMesh expected = ReadGlassMesh(leaf);
            if (expected == null) return textureChanged;
            string path = (string)leaf["target"];
            var spriteBefore = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            Require(AssetDatabase.TryGetGUIDAndLocalFileIdentifier(spriteBefore, out string guidBefore, out long fileIdBefore),
                "W glass sprite has no persistent identity: " + path);
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            var factories = new SpriteDataProviderFactories(); factories.Init();
            var provider = factories.GetSpriteEditorDataProviderFromObject(importer);
            Require(provider != null, "W glass Sprite Editor provider is missing.");
            provider.InitSpriteEditorDataProvider();
            var rects = provider.GetSpriteRects();
            Require(rects != null && rects.Length == 1, "W glass must remain a single Sprite.");
            var spriteId = rects[0].spriteID;
            var meshProvider = provider.GetDataProvider<ISpriteMeshDataProvider>();
            var bonesProvider = provider.GetDataProvider<ISpriteBoneDataProvider>();
            Require(meshProvider != null && bonesProvider != null && bonesProvider.GetBones(spriteId).Count == 0,
                "W glass mesh cannot overwrite an unreviewed Sprite skin.");
            var settings = new TextureImporterSettings(); importer.ReadTextureSettings(settings);
            bool meshChanged = settings.spriteMeshType != SpriteMeshType.Tight ||
                !MeshMatches(expected, meshProvider.GetVertices(spriteId), meshProvider.GetIndices(spriteId));
            if (meshChanged)
            {
                Shared.RequireCleanScenes();
                meshProvider.SetVertices(spriteId, expected.importerVertices);
                meshProvider.SetIndices(spriteId, expected.indices);
                meshProvider.SetEdges(spriteId, Array.Empty<Vector2Int>());
                settings.spriteMeshType = SpriteMeshType.Tight;
                importer.SetTextureSettings(settings);
                provider.Apply();
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
            }
            importer = (TextureImporter)AssetImporter.GetAtPath(path);
            provider = factories.GetSpriteEditorDataProviderFromObject(importer); provider.InitSpriteEditorDataProvider();
            rects = provider.GetSpriteRects();
            Require(rects.Length == 1 && rects[0].spriteID == spriteId, "W glass Sprite ID changed during mesh import.");
            meshProvider = provider.GetDataProvider<ISpriteMeshDataProvider>();
            importer.ReadTextureSettings(settings);
            Require(settings.spriteMeshType == SpriteMeshType.Tight &&
                MeshMatches(expected, meshProvider.GetVertices(spriteId), meshProvider.GetIndices(spriteId)), "W glass importer mesh readback failed: " + path);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            Require(sprite && AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sprite, out string guidAfter, out long fileIdAfter) &&
                guidAfter == guidBefore && fileIdAfter == fileIdBefore, "W glass persistent identity changed.");
            Vector2[] actualVertices = sprite.vertices;
            Require(actualVertices.Length == expected.localVertices.Length &&
                sprite.triangles.Select(index => (int)index).SequenceEqual(expected.indices) &&
                actualVertices.Select((vertex, i) => Vector2.Distance(vertex, expected.localVertices[i]) <= .00002f).All(value => value) &&
                sprite.rect.size == new Vector2(1895, 817) && sprite.pixelsPerUnit == 100 &&
                Vector2.Distance(sprite.pivot, new Vector2(947.5f, 408.5f)) <= .001f,
                "W glass runtime Sprite geometry readback failed: " + path);
            report["sprite_geometry"] = new JObject { ["source_path_id"] = expected.sourcePid, ["vertices"] = actualVertices.Length,
                ["indices"] = expected.indices.Length, ["changed"] = meshChanged, ["verified"] = true, ["identity_preserved"] = true };
            return textureChanged || meshChanged;
        }

        private static JArray RebuildPrefabs(string repo, JArray jobs)
        {
            Shared.RequireCleanScenes();
            Scene previous = SceneManager.GetActiveScene();
            bool replace = Shared.CanReplaceEmptyBatchScene();
            Scene temporary = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, replace ? NewSceneMode.Single : NewSceneMode.Additive);
            var results = new JArray();
            try
            {
                SceneManager.SetActiveScene(temporary);
                foreach (JObject job in jobs)
                {
                    var sources = ((JArray)job["objects"]).Select(row => Shared.ResolveBelow(repo, (string)row["dump"]));
                    var result = PrefabBuilder.RebuildEditable(repo, job, sources);
                    var row = JObject.FromObject(result);
                    results.Add(row);
                    Require(result.status == "REBUILT", "Switch story prefab failed: " + job["target"] + ": " + result.error);
                    if (IsDoubleObjectionJob(job)) row["compatibility_sorting"] = EnsureDoubleObjectionSorting(job);
                }
                return results;
            }
            finally
            {
                if (replace) EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                else
                {
                    if (previous.IsValid() && previous.isLoaded) SceneManager.SetActiveScene(previous);
                    if (temporary.IsValid() && temporary.isLoaded) EditorSceneManager.CloseScene(temporary, true);
                }
            }
        }

        internal static JObject VerifyPrefab(GameObject prefab, JObject job)
        {
            Require(prefab, "Switch story prefab did not load.");
            var transforms = prefab.GetComponentsInChildren<Transform>(true);
            Require(transforms.Sum(t => GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)) == 0,
                prefab.name + " has missing scripts.");
            var directors = prefab.GetComponentsInChildren<PlayableDirector>(true);
            Require(directors.Length > 0, prefab.name + " has no Timeline director.");
            var timelines = new JArray();
            var emptyUnboundTracks = new JArray();
            int curves = 0, timelineCurves = 0;
            foreach (var director in directors)
            {
                var timeline = director.playableAsset as TimelineAsset;
                Require(timeline && timeline.duration > 0 && !double.IsInfinity(timeline.duration), director.name + " has no valid Timeline.");
                foreach (var track in timeline.GetOutputTracks())
                {
                    Transform animationRoot = null;
                    if (track is AnimationTrack animationTrack)
                    {
                        var binding = director.GetGenericBinding(track);
                        animationRoot = binding is Component component ? component.transform : (binding as GameObject)?.transform;
                        Require(animationRoot, prefab.name + ": unbound animation track " + track.name);
                        if (animationTrack.infiniteClip) timelineCurves += VerifyAnimationPaths(animationRoot, animationTrack.infiniteClip, prefab.name);
                    }
                    if (track is ActivationTrack && !director.GetGenericBinding(track))
                    {
                        // The source ObjectionCutIn_w contains an unused, unbound activation
                        // track under CutIn, with no clips, curves, markers or child tracks.
                        // Preserve that inert track; an output with content must still bind.
                        Require(!track.GetClips().Any() && !track.curves && track.GetMarkerCount() == 0 && !track.GetChildTracks().Any(),
                            prefab.name + ": unbound activation track with content " + track.name);
                        emptyUnboundTracks.Add(new JObject { ["director"] = director.name, ["track"] = track.name,
                            ["reason"] = "Empty activation track has no target and produces no effect; no target was invented." });
                    }
                    if (track is AudioTrack)
                        Require(director.GetGenericBinding(track), prefab.name + ": unbound audio track " + track.name);
                    foreach (var clip in track.GetClips())
                    {
                        Require(clip.asset, prefab.name + ": missing Timeline clip asset.");
                        if (clip.asset is AnimationPlayableAsset animation)
                        {
                            Require(animationRoot && animation.clip, prefab.name + ": missing Timeline animation or binding.");
                            timelineCurves += VerifyAnimationPaths(animationRoot, animation.clip, prefab.name);
                        }
                        if (clip.asset is AudioPlayableAsset audio) Require(audio.clip, prefab.name + ": missing Timeline audio clip.");
                        if (clip.asset is ControlPlayableAsset control)
                        {
                            GameObject controlled = control.sourceGameObject.Resolve(director);
                            Require(controlled && controlled.transform.IsChildOf(prefab.transform) && !control.prefabGameObject,
                                prefab.name + ": missing or external ControlTrack object.");
                        }
                    }
                }
                timelines.Add(new JObject { ["director"] = director.name, ["asset"] = timeline.name, ["duration"] = timeline.duration });
            }
            foreach (var animator in prefab.GetComponentsInChildren<Animator>(true))
            {
                if (!animator.runtimeAnimatorController) continue; // Timeline can drive an Animator directly.
                foreach (var clip in animator.runtimeAnimatorController.animationClips)
                {
                    Require(clip, prefab.name + ": missing controller AnimationClip.");
                    curves += VerifyAnimationPaths(animator.transform, clip, prefab.name);
                }
            }
            var cutIns = prefab.GetComponentsInChildren<MonoBehaviour>(true).Where(component =>
                component && component.GetType().FullName == "WitchTrials.Views.ObjectionCutIn").ToArray();
            Require(cutIns.Length == 1, prefab.name + ": expected one ObjectionCutIn runtime component.");
            var runtime = new SerializedObject(cutIns[0]);
            Require(runtime.FindProperty("_playableDirector")?.objectReferenceValue is PlayableDirector runtimeDirector &&
                directors.Contains(runtimeDirector), prefab.name + ": ObjectionCutIn director reference is missing or external.");
            Require(runtime.FindProperty("_audioSource")?.objectReferenceValue is AudioSource audioSource &&
                audioSource.transform.IsChildOf(prefab.transform), prefab.name + ": ObjectionCutIn audio source reference is missing or external.");
            var library = runtime.FindProperty("_objectLibrary");
            Require(library != null && library.isArray, prefab.name + ": missing ObjectionCutIn object library.");
            for (int i = 0; i < library.arraySize; i++)
            {
                var objects = library.GetArrayElementAtIndex(i).FindPropertyRelative("Objects");
                Require(objects != null && objects.isArray, prefab.name + ": invalid cut-in activation group.");
                for (int j = 0; j < objects.arraySize; j++)
                    Require(objects.GetArrayElementAtIndex(j).objectReferenceValue is GameObject item && item.transform.IsChildOf(prefab.transform),
                        prefab.name + ": missing or external cut-in activation object.");
            }
            foreach (var renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {
                Require(renderer.sharedMaterials.All(material => material && material.shader), renderer.name + ": missing material/shader.");
            }
            JObject spriteBindings = VerifySourceSprites(prefab, job);
            JObject compatibilitySorting = IsDoubleObjectionJob(job) ? VerifyDoubleObjectionSorting(prefab, job, false, out _) : null;
            return new JObject { ["name"] = prefab.name, ["missing_scripts"] = 0,
                ["timelines"] = timelines, ["controller_curve_bindings"] = curves, ["timeline_curve_bindings"] = timelineCurves,
                ["empty_unbound_activation_tracks"] = emptyUnboundTracks,
                ["source_sprite_bindings"] = spriteBindings, ["compatibility_sorting"] = compatibilitySorting,
                ["runtime_spawn_links_verified"] = true, ["render_verified"] = false };
        }

        private static bool IsDoubleObjectionJob(JObject job) =>
            (string)job?["target"] == TargetRoot + "Prefabs/ObjectionCutIn_w.prefab";

        private static JObject EnsureDoubleObjectionSorting(JObject job)
        {
            string target = (string)job["target"];
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(target);
            VerifyDoubleObjectionSorting(prefab, job, true, out SpriteRenderer glow);
            bool changed = glow.sortingOrder != 199;
            string guid = AssetDatabase.AssetPathToGUID(target);
            Require(AssetDatabase.TryGetGUIDAndLocalFileIdentifier(glow, out string rendererGuid, out long rendererId) &&
                rendererGuid == guid, "W glow has no persistent prefab identity.");
            if (changed)
            {
                // The source deliberately gives glow and all three glass stages order200.
                // GPU isolation and the user's original-console reference establish that
                // the built-in renderer must draw glow behind the glass. Its same-order
                // material tie-break differs from the source URP result. This is an
                // explicit cross-pipeline compatibility order, NOT a recovered NS order.
                // Keep source dumps intact, and save only this generated field through
                // the existing prefab pipeline; shared PC cut-ins are never touched.
                glow.sortingOrder = 199;
                EditorUtility.SetDirty(glow);
                PrefabUtility.SavePrefabAsset(prefab, out bool saved);
                Require(saved, "Could not save the W compatibility sorting order.");
            }
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(target);
            JObject verified = VerifyDoubleObjectionSorting(prefab, job, false, out SpriteRenderer persistedGlow);
            Require(AssetDatabase.AssetPathToGUID(target) == guid &&
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(persistedGlow, out string savedGuid, out long savedId) &&
                savedGuid == rendererGuid && savedId == rendererId, "W compatibility sorting changed prefab/renderer identity.");
            verified["changed"] = changed;
            verified["identity_preserved"] = true;
            return verified;
        }

        private static JObject VerifyDoubleObjectionSorting(GameObject prefab, JObject job, bool allowSourceOrder, out SpriteRenderer glow)
        {
            Require(IsDoubleObjectionJob(job) && prefab && prefab.name == "ObjectionCutIn_w" &&
                (string)job["adapter"] == "switch-story-cutin-v1" &&
                (string)job["bundle"] == "naninovel-spawn_assets_naninovel_spawn_objectioncutin_w.bundle",
                "Unreviewed prefab identity for W compatibility sorting.");
            var rows = ((JArray)job["objects"]).OfType<JObject>().ToDictionary(row => (string)row["pid"]);
            var source = rows.Where(pair => new[] { "GameObject", "Transform", "RectTransform", "SpriteRenderer", "SortingGroup" }
                    .Contains((string)pair.Value["type"]))
                .ToDictionary(pair => pair.Key, pair => JObject.Parse(File.ReadAllText(Shared.ResolveBelow(Repo, (string)pair.Value["dump"]))));
            var transforms = source.Where(pair => pair.Value["m_Father"] != null)
                .ToDictionary(pair => (string)pair.Value["m_GameObject"]["m_PathID"], pair => pair.Value);
            string SourcePath(JObject component)
            {
                string go = (string)component["m_GameObject"]["m_PathID"];
                var names = new List<string>(); var visited = new HashSet<string>();
                while (true)
                {
                    Require(visited.Add(go) && source.ContainsKey(go) && transforms.ContainsKey(go), "Invalid W source sorting hierarchy.");
                    string name = (string)source[go]["m_Name"], parent = (string)transforms[go]["m_Father"]["m_PathID"];
                    if (parent == "0") { Require(name == prefab.name, "W source sorting root changed."); break; }
                    names.Add(name);
                    Require(source.ContainsKey(parent), "W source sorting parent is missing.");
                    go = (string)source[parent]["m_GameObject"]["m_PathID"];
                }
                names.Reverse(); return string.Join("/", names);
            }
            string[] pids = { "2373995515728115643", "-4388307456139315269", "6469868542123849659", "7669146499963500475" };
            string[] names = { "RefuteCutIn_StainedGlass_luminescence", "RefuteCutIn_StainedGlass_001",
                "RefuteCutIn_StainedGlass_002", "RefuteCutIn_StainedGlass_003" };
            var selected = new Dictionary<string, SpriteRenderer>();
            var rendererRows = rows.Where(pair => (string)pair.Value["type"] == "SpriteRenderer").ToArray();
            Require(rendererRows.Length == 18 && prefab.GetComponentsInChildren<SpriteRenderer>(true).Length == 18,
                "W source/generated SpriteRenderer count changed before sorting adaptation.");
            var seen = new HashSet<string>();
            foreach (var pair in rendererRows)
            {
                JObject data = source[pair.Key]; string path = SourcePath(data);
                Require(seen.Add(path), "Ambiguous W source sorting path: " + path);
                Transform node = path.Length == 0 ? prefab.transform : prefab.transform.Find(path);
                Require(node && node.GetComponents<SpriteRenderer>().Length == 1, "Missing W sorting renderer: " + path);
                SpriteRenderer renderer = node.GetComponent<SpriteRenderer>();
                int index = Array.IndexOf(pids, pair.Key);
                int sourceOrder = (int)data["m_SortingOrder"];
                int expectedOrder = index == 0 ? 199 : sourceOrder;
                Require(renderer.sortingLayerID == (int)data["m_SortingLayerID"] &&
                    renderer.rendererPriority == (int)data["m_RendererPriority"] &&
                    (int)renderer.spriteSortPoint == (int)data["m_SpriteSortPoint"] &&
                    (int)renderer.maskInteraction == (int)data["m_MaskInteraction"] &&
                    (renderer.sortingOrder == expectedOrder || (allowSourceOrder && index == 0 && renderer.sortingOrder == 200)),
                    "W renderer sorting differs from its source/explicit compatibility order: " + path);
                if (index < 0) continue; // Every other source sorting field must remain exact.
                Require(path == "CutIN_2/CutIN/" + names[index] && sourceOrder == 200 &&
                    (int)data["m_SortingLayerID"] == 0 && (int)data["m_RendererPriority"] == 0 &&
                    (int)data["m_SpriteSortPoint"] == 0 && (int)data["m_MaskInteraction"] == 0,
                    "The reviewed W layer/source order changed: " + path);
                string materialPid = index == 0 ? "-2505398188983732251" : "-458464065555864305";
                string sourceShader = index == 0 ? "Shader Graphs/Iuminescence_dezolve_0Fix" : "Shader Graphs/Glasses_0Fix";
                string shader = index == 0 ? "Manosaba/SwitchStory/LuminescenceDissolve" : "Manosaba/SwitchStory/Glasses";
                string materialName = index == 0 ? "Iuminescence_dezolve_W_-2505398188983732251" : "Glasses_Fix_W_-458464065555864305";
                var materials = (JArray)data["m_Materials"];
                var materialRecipe = ((JArray)job["story_materials"]).OfType<JObject>().Single(row => (string)row["pid"] == materialPid);
                Require(materials.Count == 1 && (int)materials[0]["m_FileID"] == 0 && (string)materials[0]["m_PathID"] == materialPid &&
                    (string)materialRecipe["name"] == materialName && (string)materialRecipe["shader"] == sourceShader &&
                    renderer.sharedMaterials.Length == 1 && renderer.sharedMaterial && renderer.sharedMaterial.name == materialName &&
                    renderer.sharedMaterial.shader && renderer.sharedMaterial.shader.name == shader && renderer.sharedMaterial.renderQueue == 3000,
                    "The reviewed W sorting layer material/shader changed: " + path);
                selected.Add(pair.Key, renderer);
            }
            Require(selected.Count == 4, "W compatibility sorting did not locate all four reviewed layers.");
            var sourceGroups = rows.Where(pair => (string)pair.Value["type"] == "SortingGroup").ToArray();
            var groups = prefab.GetComponentsInChildren<UnityEngine.Rendering.SortingGroup>(true);
            Require(sourceGroups.Length == 1 && sourceGroups[0].Key == "2021943833855932347" && groups.Length == 1 &&
                groups[0].transform == prefab.transform && groups[0].enabled && groups[0].sortingLayerID == 0 &&
                groups[0].sortingOrder == 1000 && !groups[0].sortAtRoot, "W root SortingGroup changed.");
            JObject groupSource = source[sourceGroups[0].Key];
            Require(SourcePath(groupSource).Length == 0 && (int)groupSource["m_Enabled"] == 1 &&
                (int)groupSource["m_SortingLayerID"] == 0 && (int)groupSource["m_SortingOrder"] == 1000 &&
                !(bool)groupSource["m_SortAtRoot"], "W source SortingGroup proof changed.");
            glow = selected[pids[0]];
            var layers = new JArray();
            for (int i = 0; i < pids.Length; i++) layers.Add(new JObject { ["path"] = "CutIN_2/CutIN/" + names[i],
                ["source_renderer_pid"] = pids[i], ["source_order"] = 200, ["actual_order"] = selected[pids[i]].sortingOrder,
                ["compatibility_order"] = i == 0 ? 199 : 200 });
            return new JObject { ["status"] = "PASS", ["adaptation"] = "W glow behind glass across rendering pipelines",
                ["source_order_preserved_in_recipe"] = true, ["source_original_draw_order_claimed"] = false,
                ["source_sprite_renderers_checked"] = rendererRows.Length, ["other_sorting_fields_preserved"] = true,
                ["layers"] = layers, ["evidence"] = "Original source layers all use order200. GPU layer isolation plus the original-console reference require glow behind the visible glass. Built-in same-order material tie-break obscured the glass; only the generated W glow uses199." };
        }

        private static JObject VerifySourceSprites(GameObject prefab, JObject job)
        {
            // A null source sprite is meaningful (the W stained-glass mask is enabled
            // but intentionally has no drawable sprite). Identify every source slot by
            // its hierarchy and compare it; never waive missing sprites globally.
            Require(job != null && job["objects"] is JArray, "A verified source job is required for sprite readback.");
            var source = ((JArray)job["objects"]).OfType<JObject>()
                .Where(row => new[] { "GameObject", "Transform", "RectTransform", "SpriteRenderer" }.Contains((string)row["type"]))
                .ToDictionary(row => (string)row["pid"], row => JObject.Parse(File.ReadAllText(Shared.ResolveBelow(Repo, (string)row["dump"]))));
            var transforms = source.Where(pair => pair.Value["m_Father"] != null).ToDictionary(
                pair => (string)pair.Value["m_GameObject"]["m_PathID"], pair => pair.Value);
            string RelativePath(string gameObjectId)
            {
                var names = new List<string>();
                var visited = new HashSet<string>();
                while (true)
                {
                    Require(visited.Add(gameObjectId), "Source transform hierarchy contains a cycle.");
                    string name = (string)source[gameObjectId]["m_Name"];
                    string parent = (string)transforms[gameObjectId]["m_Father"]["m_PathID"];
                    if (parent == "0")
                    {
                        Require(name == prefab.name, "Source and generated prefab roots differ.");
                        break;
                    }
                    names.Add(name);
                    gameObjectId = (string)source[parent]["m_GameObject"]["m_PathID"];
                }
                names.Reverse(); return string.Join("/", names);
            }
            var sprites = ((JArray)job["objects"]).OfType<JObject>().Where(row => (string)row["type"] == "SpriteRenderer").ToArray();
            Require(prefab.GetComponentsInChildren<SpriteRenderer>(true).Length == sprites.Length, prefab.name + ": source SpriteRenderer count changed.");
            var paths = new HashSet<string>();
            var empty = new JArray();
            foreach (JObject row in sprites)
            {
                string pid = (string)row["pid"];
                JObject data = source[pid];
                string path = RelativePath((string)data["m_GameObject"]["m_PathID"]);
                Require(paths.Add(path), "Source SpriteRenderer hierarchy is ambiguous: " + path);
                Transform target = path.Length == 0 ? prefab.transform : prefab.transform.Find(path);
                Require(target && target.GetComponents<SpriteRenderer>().Length == 1, prefab.name + ": source sprite slot is missing: " + path);
                var renderer = target.GetComponent<SpriteRenderer>();
                Require(renderer.sharedMaterials.Length == ((JArray)data["m_Materials"]).Count, prefab.name + ": material slot count changed: " + path);
                string spritePid = (string)data["m_Sprite"]["m_PathID"];
                if (spritePid == "0")
                {
                    Require(!renderer.sprite, prefab.name + ": an empty source sprite slot was populated: " + path);
                    empty.Add(new JObject { ["path"] = path, ["source_renderer_pid"] = pid,
                        ["reason"] = "Source m_Sprite is null; preserve the non-drawing renderer." });
                    continue;
                }
                string key = pid + ":" + (string)data["m_Sprite"]["m_FileID"] + ":" + spritePid;
                string expectedPath = (string)job["references"]?[key];
                Require(!string.IsNullOrEmpty(expectedPath), prefab.name + ": source sprite reference is absent from the recipe: " + key);
                var expected = AssetDatabase.LoadAssetAtPath<Sprite>(expectedPath);
                var actual = renderer.sprite;
                Require(expected && actual && actual.name == expected.name && actual.rect == expected.rect &&
                    actual.pivot == expected.pivot && Mathf.Approximately(actual.pixelsPerUnit, expected.pixelsPerUnit) &&
                    actual.texture && expected.texture && actual.texture.name == expected.texture.name &&
                    actual.texture.width == expected.texture.width && actual.texture.height == expected.texture.height,
                    prefab.name + ": source sprite identity/geometry changed: " + path);
            }
            return new JObject { ["count"] = sprites.Length, ["nonempty_verified"] = sprites.Length - empty.Count, ["source_empty"] = empty };
        }

        private static int VerifyAnimationPaths(Transform root, AnimationClip clip, string prefabName)
        {
            var bindings = AnimationUtility.GetCurveBindings(clip).Concat(AnimationUtility.GetObjectReferenceCurveBindings(clip)).ToArray();
            foreach (var binding in bindings)
                Require(string.IsNullOrEmpty(binding.path) || root.Find(binding.path),
                    prefabName + ": unresolved animation path " + binding.path + " in " + clip.name);
            return bindings.Length;
        }

        // Counts alone cannot detect a correctly sized but stale Ema/2 command where
        // the reviewed Switch merge calls ObjectionCutIn_w/0 instead. Check the actual
        // managed references, including raw text used by CommandParameter.ToString().
        internal static JArray VerifyDynamicObjectionCalls(Naninovel.Script script, JObject source, JObject scenario)
        {
            const string commandType = "WitchTrials.Views.GosubToObjectionCutIn";
            var references = ((JArray)source["references"]["RefIds"]).OfType<JObject>()
                .ToDictionary(row => (long)row["rid"]);
            var playlist = (JArray)source["playlist"]["commands"];
            Require(script.Playlist.Count == playlist.Count, "Objection command playlist changed: " + script.Path);
            var calls = new JArray();
            for (int i = 0; i < playlist.Count; i++)
            {
                JObject expected = references[(long)playlist[i]["rid"]];
                var actual = script.Playlist[i];
                bool isExpected = (string)expected["type"]["ns"] + "." + (string)expected["type"]["class"] == commandType;
                bool isActual = actual?.GetType().FullName == commandType;
                Require(isExpected == isActual, "Objection command type/position changed: " + script.Path + ":" + i);
                if (!isExpected) continue;
                var data = (JObject)expected["data"];
                Require(actual.Indent == (int)data["indent"] &&
                    JToken.DeepEquals(JObject.Parse(JsonUtility.ToJson(actual.PlaybackSpot)), data["playbackSpot"]),
                    "Objection command playback position changed: " + script.Path + ":" + i);
                var parameters = new JObject();
                foreach (string field in new[] { "CutInKind", "Index", "VoicePath", "Volume", "GroupPath", "AuthorId" })
                {
                    if (field != "CutInKind" && field != "Index" && data[field] == null) continue;
                    var parameter = actual.GetType().GetField(field)?.GetValue(actual) as Naninovel.ICommandParameter;
                    Require(parameter != null && data[field] is JObject, "Objection parameter is missing: " + field);
                    var serialized = JObject.Parse(JsonUtility.ToJson(parameter));
                    var wanted = (JObject)data[field];
                    Require(parameter.HasValue == ((int)wanted["hasValue"] != 0) &&
                        new[] { "value", "raw", "spot" }.All(key => JToken.DeepEquals(serialized[key], wanted[key])),
                        "Objection parameter readback changed: " + script.Path + ":" + i + "." + field);
                    parameters[field] = serialized;
                }
                var kind = (Naninovel.StringParameter)actual.GetType().GetField("CutInKind").GetValue(actual);
                var index = (Naninovel.IntegerParameter)actual.GetType().GetField("Index").GetValue(actual);
                bool literal = kind.HasValue && index.HasValue && !kind.DynamicValue && !index.DynamicValue;
                var call = new JObject { ["playlist_index"] = i, ["line_index"] = actual.PlaybackSpot.LineIndex,
                    ["literal_parameters"] = literal, ["parameters"] = parameters };
                if (literal)
                {
                    Require(kind.ToString() == kind.Value, "Objection raw text disagrees with its serialized value.");
                    call["spawn_path"] = "ObjectionCutIn_" + kind.Value;
                    call["address"] = "Naninovel/Spawn/ObjectionCutIn_" + kind.Value;
                    call["index"] = index.Value;
                }
                calls.Add(call);
            }
            // Only newly ported calls are declared; existing PC objection calls remain.
            if (scenario["merge"]?["dynamic_objection_calls"] is JObject declared)
                foreach (var declaration in declared.Properties())
                    Require(calls.Count(call => (string)call["spawn_path"] == declaration.Name) == (int)declaration.Value,
                        "Declared objection call count changed: " + script.Path + ": " + declaration.Name);
            foreach (JObject declaration in scenario["merge"]?["objection_voice_calls"] as JArray ?? new JArray())
            {
                var call = calls.OfType<JObject>().SingleOrDefault(row => (int)row["playlist_index"] == (int)declaration["merged_command"]);
                Require(call != null && (int)call["line_index"] == (int)declaration["line_index"] &&
                    (string)call["parameters"]?["VoicePath"]?["value"] == (string)declaration["voice_path"],
                    "Declared objection voice did not survive conversion: " + script.Path);
                foreach (var parameter in ((JObject)declaration["parameters"]).Properties())
                    Require(new[] { "value", "raw", "spot" }.All(key =>
                            JToken.DeepEquals(call["parameters"]?[parameter.Name]?[key], parameter.Value[key])),
                        "Declared objection voice parameter changed: " + script.Path + ": " + parameter.Name);
            }
            return calls;
        }

        internal static JArray VerifyObjectionVoicePairs(Naninovel.Script script, JObject source, JObject scenario)
        {
            const string typeName = "WitchTrials.Views.PlayObjectionVoicePair";
            var references = ((JArray)source["references"]["RefIds"]).OfType<JObject>().ToDictionary(row => (long)row["rid"]);
            var playlist = (JArray)source["playlist"]["commands"];
            Require(script.Playlist.Count == playlist.Count, "Objection voice pair playlist changed.");
            var result = new JArray();
            var declarations = scenario["merge"]?["objection_voice_pairs"] as JArray ?? new JArray();
            for (int i = 0; i < playlist.Count; i++)
            {
                var expected = references[(long)playlist[i]["rid"]];
                bool wanted = (string)expected["type"]["ns"] + "." + (string)expected["type"]["class"] == typeName;
                var actual = script.Playlist[i];
                Require(wanted == (actual?.GetType().FullName == typeName), "Objection voice pair type/position changed.");
                if (!wanted) continue;
                var data = (JObject)expected["data"];
                Require(actual.Indent == (int)data["indent"] &&
                    JToken.DeepEquals(JObject.Parse(JsonUtility.ToJson(actual.PlaybackSpot)), data["playbackSpot"]),
                    "Objection voice pair playback position changed.");
                var declaration = declarations.OfType<JObject>().SingleOrDefault(row => (int)row["merged_command"] == i);
                Require(declaration != null && (int)declaration["line_index"] == actual.PlaybackSpot.LineIndex,
                    "Objection voice pair has no exact merge declaration.");
                var parameters = new JObject();
                foreach (string prefix in new[] { "", "Second" })
                    foreach (string name in new[] { "VoicePath", "Volume", "GroupPath", "AuthorId" })
                    {
                        string field = prefix + name;
                        var parameter = actual.GetType().GetField(field)?.GetValue(actual) as Naninovel.ICommandParameter;
                        Require(parameter != null && data[field] is JObject, "Objection voice pair parameter is missing: " + field);
                        var serialized = JObject.Parse(JsonUtility.ToJson(parameter));
                        var expectedParameter = (JObject)data[field];
                        Require(parameter.HasValue == ((int)expectedParameter["hasValue"] != 0) &&
                            new[] { "value", "raw", "spot" }.All(key => JToken.DeepEquals(serialized[key], expectedParameter[key])) &&
                            JToken.DeepEquals(expectedParameter, declaration["parameters"]?[field]),
                            "Objection voice pair parameter readback changed: " + field);
                        parameters[field] = serialized;
                    }
                result.Add(new JObject { ["playlist_index"] = i, ["line_index"] = actual.PlaybackSpot.LineIndex,
                    ["parameters"] = parameters, ["addresses"] = declaration["addresses"].DeepClone() });
            }
            Require(result.Count == declarations.Count, "Declared objection voice pair count changed.");
            return result;
        }

        private static JArray VerifyObjectionVoiceDependencies(JObject plan)
        {
            // Reuse existing audio groups. Checking them on every gate also catches
            // missing clips or disabled/duplicate addresses when the recipe is cached.
            var addresses = new HashSet<string>(StringComparer.Ordinal);
            foreach (JObject scenario in (JArray)plan["scenarios"])
            {
                foreach (JObject call in scenario["merge"]?["objection_voice_calls"] as JArray ?? new JArray())
                    addresses.Add((string)call["address"]);
                foreach (JObject pair in scenario["merge"]?["objection_voice_pairs"] as JArray ?? new JArray())
                    foreach (string address in ((JArray)pair["addresses"]).Values<string>()) addresses.Add(address);
            }
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            Require(settings, "Missing Addressables settings for objection voices.");
            var result = new JArray();
            foreach (string address in addresses.OrderBy(value => value, StringComparer.Ordinal))
            {
                Require(address != null && Regex.IsMatch(address, "^Naninovel/Voice/CommonTrial_(Ema|Hiro)[0-9]+$"),
                    "Unexpected objection voice dependency: " + address);
                var matches = settings.groups.Where(group => group).SelectMany(group => group.entries)
                    .Where(entry => entry != null && entry.address == address).ToArray();
                Require(matches.Length == 1, "Objection voice must have one existing Addressable entry: " + address);
                var entry = matches[0];
                var schema = entry.parentGroup.GetSchema<BundledAssetGroupSchema>();
                Require(schema && schema.IncludeInBuild, "Objection voice group is excluded from the build: " + address);
                string path = AssetDatabase.GUIDToAssetPath(entry.guid);
                Require(!string.IsNullOrEmpty(path) && File.Exists(Shared.ResolveBelow(Project, path)), "Objection voice file is missing: " + address);
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
                Require(clip && clip.samples > 0 && clip.channels > 0 && clip.frequency > 0, "Objection voice AudioClip is invalid: " + address);
                result.Add(new JObject { ["address"] = address, ["path"] = path, ["guid"] = entry.guid,
                    ["group"] = entry.parentGroup.Name, ["samples"] = clip.samples, ["channels"] = clip.channels,
                    ["frequency"] = clip.frequency, ["length"] = clip.length,
                    ["sha256"] = Shared.Hash(Shared.ResolveBelow(Project, path)), ["verified"] = true });
            }
            return result;
        }

        private static bool EnsureAudio(JObject leaf, JObject report)
        {
            string target = (string)leaf["target"];
            var importer = AssetImporter.GetAtPath(target) as AudioImporter;
            Require(importer, "Missing story audio importer: " + target);
            var settings = importer.defaultSampleSettings;
            // AudioImportPolicy owns compression, load type and quality project-wide.
            // Fighting it with PCM/DecompressOnLoad caused every sync to reimport all
            // five WAVs, then rebuild every story prefab and scenario indefinitely.
            bool changed = importer.forceToMono || settings.sampleRateSetting != AudioSampleRateSetting.PreserveSampleRate;
            if (changed)
            {
                importer.forceToMono = false;
                settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;
                importer.defaultSampleSettings = settings;
                importer.SaveAndReimport();
            }
            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(target);
            Require(clip && clip.samples == (int)leaf["samples"] && clip.channels == (int)leaf["channels"] &&
                clip.frequency == (int)leaf["frequency"] && Math.Abs(clip.length - (double)leaf["length"]) < 0.01,
                "Story audio readback changed samples/channels/rate: " + target);
            string guid = AssetDatabase.AssetPathToGUID(target);
            Require(string.IsNullOrEmpty((string)report["guid_before"]) || guid == (string)report["guid_before"], "Story audio GUID changed.");
            report["guid"] = guid;
            report["importer_changed"] = changed;
            settings = importer.defaultSampleSettings;
            report["audio_policy_mode"] = AudioImportPolicy.Enabled ? AudioImportPolicy.Mode : "disabled";
            report["compression_format"] = settings.compressionFormat.ToString();
            report["load_type"] = settings.loadType.ToString();
            report["quality"] = settings.quality;
            report["verified"] = true;
            return changed;
        }

        private static JObject ApplyGallery(JObject plan)
        {
            if (!(plan["gallery"] is JObject recipe)) return new JObject { ["status"] = "not_requested" };
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(GalleryTarget);
            Require(prefab, "Existing GalleryUI prefab is missing; refusing to rebuild it.");
            var grids = prefab.GetComponentsInChildren<MonoBehaviour>(true)
                .Where(component => component && component.GetType().FullName == "WitchTrials.Views.StillGrid").ToArray();
            Require(grids.Length == 1, "GalleryUI must contain exactly one StillGrid.");
            var serialized = new SerializedObject(grids[0]);
            var property = serialized.FindProperty("_order");
            Require(property != null && property.isArray, "StillGrid ordering field is missing.");
            var before = Enumerable.Range(0, property.arraySize).Select(i => property.GetArrayElementAtIndex(i).stringValue).ToList();
            var after = new List<string>(before);
            string[] sourceOrder = ((JArray)recipe["source_order"]).Values<string>().ToArray();
            var candidates = new HashSet<string>(((JArray)plan["addresses"]).OfType<JObject>()
                .Select(row => (string)row["address"]).Where(address => address.StartsWith(StillAddress, StringComparison.Ordinal))
                .Select(address => address.Substring(StillAddress.Length).Split('_')[0]), StringComparer.Ordinal);
            var inserted = new JArray();
            foreach (string id in sourceOrder.Where(candidates.Contains))
            {
                if (after.Contains(id)) continue;
                string next = sourceOrder.Skip(Array.IndexOf(sourceOrder, id) + 1).FirstOrDefault(after.Contains);
                int index = next == null ? after.Count : after.IndexOf(next);
                after.Insert(index, id);
                inserted.Add(id);
            }
            Require(candidates.All(after.Contains), "One or more new CG IDs are absent from the source gallery order.");
            if (inserted.Count > 0)
            {
                Shared.RequireCleanScenes();
                string guid = AssetDatabase.AssetPathToGUID(GalleryTarget);
                property.arraySize = after.Count;
                for (int i = 0; i < after.Count; i++) property.GetArrayElementAtIndex(i).stringValue = after[i];
                serialized.ApplyModifiedPropertiesWithoutUndo();
                PrefabUtility.SavePrefabAsset(prefab);
                Require(AssetDatabase.AssetPathToGUID(GalleryTarget) == guid, "GalleryUI GUID changed.");
                serialized.Update();
                Require(Enumerable.Range(0, property.arraySize).Select(i => property.GetArrayElementAtIndex(i).stringValue).SequenceEqual(after),
                    "Gallery ordering did not survive prefab save.");
            }
            return new JObject { ["status"] = "OK", ["target"] = GalleryTarget,
                ["before"] = new JArray(before), ["after"] = new JArray(after), ["inserted"] = inserted,
                ["preserved_existing_order"] = true, ["saved_unlock_state_changed"] = false };
        }

        private static void RequireShape(JObject plan)
        {
            Require((int?)plan["version"] == 1 && IsHash((string)plan["signature"]) &&
                plan["leaves"] is JArray && plan["jobs"] is JArray && plan["scenarios"] is JArray &&
                plan["addresses"] is JArray && plan["generated_hashes"] is JObject,
                "Unsupported or incomplete Switch story plan.");
        }

        private static void ValidatePlan(string repo, string project, JObject plan)
        {
            RequireShape(plan);
            var owned = new HashSet<string>(StringComparer.Ordinal);
            foreach (JObject leaf in (JArray)plan["leaves"])
            {
                bool audio = (string)leaf["kind"] == "audio";
                RequireScoped(project, (string)leaf["target"], TargetRoot, audio ? ".wav" : ".png");
                Require(owned.Add((string)leaf["target"]), "Duplicate story target.");
                ValidateGenerated(repo, plan, (string)leaf["source"]);
                if (audio)
                    Require((int?)leaf["samples"] > 0 && (int?)leaf["channels"] > 0 && (int?)leaf["frequency"] > 0 &&
                        (double?)leaf["length"] > 0, "Invalid story audio recipe.");
                else
                {
                    Shared.ValidateTextureRecipe(repo, project, leaf);
                    ReadGlassMesh(leaf); // Fail before copying any input if the geometry proof is malformed.
                }
            }
            foreach (JObject job in (JArray)plan["jobs"])
            {
                RequireScoped(project, (string)job["target"], TargetRoot, ".prefab");
                Require((string)job["adapter"] == "switch-story-cutin-v1" && owned.Add((string)job["target"]) &&
                    job["objects"] is JArray objects && objects.Count > 0, "Unsupported or duplicate story prefab job.");
                foreach (JObject row in (JArray)job["objects"]) ValidateGenerated(repo, plan, (string)row["dump"]);
                foreach (JProperty reference in (job["references"] as JObject ?? new JObject()).Properties())
                {
                    string referencePath = (string)reference.Value;
                    Shared.ResolveBelow(project, referencePath);
                    Require(referencePath.StartsWith("Assets/", StringComparison.Ordinal) || referencePath.StartsWith("Packages/", StringComparison.Ordinal),
                        "Unexpected story prefab reference: " + referencePath);
                    RequireNoLinks(project, referencePath);
                }
            }
            foreach (JObject scenario in (JArray)plan["scenarios"])
            {
                ValidateScenario(repo, project, plan, scenario);
                Require(owned.Add((string)scenario["target"]), "Duplicate story scenario target.");
            }
            var addresses = new HashSet<string>(StringComparer.Ordinal);
            var addressTargets = new HashSet<string>(StringComparer.Ordinal);
            foreach (JObject row in (JArray)plan["addresses"])
            {
                string path = (string)row["path"], address = (string)row["address"];
                Require(owned.Contains(path) && !string.IsNullOrEmpty(address) && address.StartsWith("Naninovel/", StringComparison.Ordinal) &&
                    addresses.Add(address) && addressTargets.Add(path) && row["labels"] is JArray labels &&
                    labels.Values<string>().Contains("Naninovel") && labels.All(label => !string.IsNullOrWhiteSpace((string)label)),
                    "Invalid or duplicate Switch story address: " + address);
                if (address.StartsWith(StillAddress, StringComparison.Ordinal))
                    Require(Regex.IsMatch(address.Substring(StillAddress.Length), @"^\d+_\d+$"), "Invalid still address.");
            }
            Require(((JArray)plan["scenarios"]).Concat((JArray)plan["jobs"]).All(row => addressTargets.Contains((string)row["target"])),
                "Every Switch story scenario/prefab must have exactly one address.");
            if (plan["gallery"] is JObject gallery)
            {
                Require((string)gallery["target"] == GalleryTarget && gallery["source_order"] is JArray order && order.Count > 0 &&
                    order.Values<string>().Distinct(StringComparer.Ordinal).Count() == order.Count &&
                    order.All(value => Regex.IsMatch((string)value ?? "", @"^\d+$")), "Invalid Switch gallery ordering recipe.");
                RequireNoLinks(project, GalleryTarget);
            }
        }

        private static void ValidateScenario(string repo, string project, JObject plan, JObject scenario)
        {
            string source = (string)scenario["source"], target = (string)scenario["target"], pc = (string)scenario["pc_source"];
            RequireScoped(project, target, ScenarioRoot, ".asset");
            RequireScoped(repo, pc, PcRoot, ".asset.json");
            Require(target != ScenarioRoot + "System/System_Title.asset" &&
                target.Substring(ScenarioRoot.Length) + ".json" == pc.Substring(PcRoot.Length) &&
                (string)scenario["type"] == "Naninovel.Script" && (string)scenario["assembly"] == "Elringus.Naninovel.Runtime",
                "Story scenario identity must match its PC source and existing converter.");
            Require(IsHash((string)scenario["pc_sha256"]) && File.Exists(Shared.ResolveBelow(repo, pc)) &&
                Shared.Hash(Shared.ResolveBelow(repo, pc)) == (string)scenario["pc_sha256"],
                "Switch story recipe is stale for " + pc + "; run normal content sync before applying it.");
            ValidateGenerated(repo, plan, source);
        }

        private static void ValidateGenerated(string repo, JObject plan, string source)
        {
            RequireScoped(repo, source, SourceRoot, null);
            string hash = (string)plan["generated_hashes"]?[source];
            Require(IsHash(hash) && File.Exists(Shared.ResolveBelow(repo, source)) && Shared.Hash(Shared.ResolveBelow(repo, source)) == hash,
                "Switch story generated input is missing or changed: " + source);
        }

        private static void RequireScoped(string root, string path, string prefix, string extension)
        {
            Shared.ResolveBelow(root, path);
            Require(path.StartsWith(prefix, StringComparison.Ordinal) &&
                (extension == null || path.EndsWith(extension, StringComparison.Ordinal)), "Unexpected story recipe path: " + path);
            RequireNoLinks(root, path);
        }

        private static void RequireNoLinks(string root, string relative)
        {
            string full = Shared.ResolveBelow(root, relative);
            for (string current = full; current != null && current.Length > root.Length; current = Path.GetDirectoryName(current))
                if ((File.Exists(current) || Directory.Exists(current)) && (File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0)
                    throw new InvalidDataException("Story recipe cannot traverse a symlink/junction: " + relative);
        }

        private static IEnumerable<string> Targets(JObject plan) =>
            ((JArray)plan["leaves"]).Select(row => (string)row["target"])
                .Concat(((JArray)plan["jobs"]).Select(row => (string)row["target"]))
                .Concat(((JArray)plan["scenarios"]).Select(row => (string)row["target"]))
                .Concat(plan["gallery"] is JObject ? new[] { GalleryTarget } : Array.Empty<string>());

        private static bool IsHash(string value) => value != null && Regex.IsMatch(value, "^[0-9a-f]{64}$");
        private static void Require(bool condition, string message)
        { if (!condition) throw new InvalidDataException(message); }
    }
}
