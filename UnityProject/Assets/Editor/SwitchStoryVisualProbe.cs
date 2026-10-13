using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace Manosaba.EditorTools
{
    /// <summary>Source-asset previews only; no game engine, scenario, audio playback, or saved progress.</summary>
    public static class SwitchStoryVisualProbe
    {
        public static void Run()
        {
            string output = SwitchStoryBuiltProbe.Output("switch-story-visual");
            JObject report;
            try { report = Execute(output); }
            catch (Exception error) { report = new JObject { ["status"] = "FAIL", ["error"] = error.ToString() }; Debug.LogException(error); }
            report["generated_utc"] = DateTime.UtcNow.ToString("o");
            File.WriteAllText(Path.Combine(output, "report.json"), report.ToString());
            Debug.Log("[SwitchStoryVisualProbe] " + report["status"] + " " + output);
            if (Application.isBatchMode) EditorApplication.Exit((string)report["status"] == "PASS" ? 0 : 1);
        }

        public static JObject Execute(string output, bool renderSamples = true)
        {
            Require(!EditorApplication.isPlayingOrWillChangePlaymode, "End Play Mode before previewing story assets.");
            Require(!Naninovel.Engine.Initialized && !Naninovel.Engine.Initializing, "The game engine must be stopped.");
            if (renderSamples) Require(SystemInfo.graphicsDeviceType != GraphicsDeviceType.Null, "Rendering requires a graphics device; do not use -nographics.");
            JObject plan = SwitchStoryContentBuilder.ReadValidatedPlan();
            string prefabFilter = Environment.GetEnvironmentVariable("MANOSABA_SWITCH_STORY_VISUAL_PREFAB") ?? "";
            string timeFilter = Environment.GetEnvironmentVariable("MANOSABA_SWITCH_STORY_VISUAL_TIME") ?? "";
            string layerFilter = Environment.GetEnvironmentVariable("MANOSABA_SWITCH_STORY_VISUAL_LAYERS") ?? "";
            Require(prefabFilter.Length == 0 || prefabFilter == "ObjectionCutIn_w", "The optional diagnostic filter only supports ObjectionCutIn_w.");
            Require(layerFilter.Length == 0 || layerFilter == "1", "VISUAL_LAYERS must be absent or 1.");
            Require((timeFilter.Length == 0 && layerFilter.Length == 0) || prefabFilter.Length > 0,
                "A fixed time or layer capture requires the explicit prefab diagnostic filter.");
            double? fixedTime = null;
            if (timeFilter.Length > 0)
            {
                Require(double.TryParse(timeFilter, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture,
                    out double parsed) && !double.IsNaN(parsed) && !double.IsInfinity(parsed) && parsed >= 0, "Invalid diagnostic sample time.");
                fixedTime = parsed;
            }
            bool diagnostic = prefabFilter.Length > 0, captureLayers = layerFilter == "1";
            JObject[] jobs = ((JArray)plan["jobs"]).OfType<JObject>()
                .Where(job => !diagnostic || Path.GetFileNameWithoutExtension((string)job["target"]) == prefabFilter).ToArray();
            Require(jobs.Length > 0, "The selected diagnostic prefab is absent from the reviewed plan.");
            Directory.CreateDirectory(output);
            var report = new JObject { ["status"] = "PASS", ["signature"] = plan["signature"].DeepClone(),
                ["source"] = "AssetDatabase", ["render_samples"] = renderSamples,
                ["live_scenario_verified"] = false, ["audio_played"] = false, ["save_state_accessed"] = false,
                ["visual_quality_reviewed"] = false,
                ["scope"] = "Isolated manual Timeline samples at source prefab transform, using the Naninovel reference camera. Transparent cut-in PNGs are overlays; CG contact sheet positions are listed in the report, not asserted as scene pairings.",
                ["prefabs"] = new JArray(), ["cgs"] = new JArray() };
            if (diagnostic)
            {
                report["scope"] = "Filtered AssetDatabase diagnostic, not the complete story visual gate. Renderer-only layer comparisons preserve masks and sampled Timeline state; no persistent assets are changed.";
                report["diagnostic_prefab"] = prefabFilter;
                report["fixed_sample_seconds"] = fixedTime;
                report["layer_captures"] = captureLayers;
                report["complete_visual_gate"] = false;
                report["unity_Time_controlled"] = false;
                report["time_scope"] = "Only _UnscaledTime follows the requested sample. Unity's built-in _Time is not overridden; shaders using it can differ in phase between captures/runs.";
            }
            if (!renderSamples)
            {
                foreach (JObject job in jobs)
                    ((JArray)report["prefabs"]).Add(SwitchStoryContentBuilder.VerifyPrefab(AssetDatabase.LoadAssetAtPath<GameObject>((string)job["target"]), job));
                return report;
            }
            Scene scene = EditorSceneManager.NewPreviewScene();
            float oldTime = Shader.GetGlobalFloat("_UnscaledTime");
            try
            {
                var cameraObject = new GameObject("SwitchStoryPreviewCamera");
                SceneManager.MoveGameObjectToScene(cameraObject, scene);
                Camera camera = cameraObject.AddComponent<Camera>();
                camera.scene = scene; camera.enabled = false; camera.orthographic = true;
                var config = new SerializedObject(AssetDatabase.LoadMainAssetAtPath("Assets/NaninovelData/Resources/Naninovel/Configuration/CameraConfiguration.asset"));
                Vector2Int reference = config.FindProperty("ReferenceResolution").vector2IntValue;
                camera.orthographicSize = reference.y / config.FindProperty("ReferencePPU").floatValue / 2f;
                camera.aspect = (float)reference.x / reference.y;
                camera.transform.position = config.FindProperty("InitialPosition").vector3Value;
                camera.clearFlags = CameraClearFlags.SolidColor; camera.backgroundColor = Color.clear;
                camera.nearClipPlane = 0.01f; camera.farClipPlane = 1000f;
                camera.allowHDR = false; camera.allowMSAA = false;
                report["reference_resolution"] = new JArray(reference.x, reference.y);
                foreach (JObject job in jobs)
                {
                    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>((string)job["target"]);
                    var row = SwitchStoryContentBuilder.VerifyPrefab(prefab, job);
                    row["target"] = job["target"].DeepClone();
                    row["samples"] = new JArray();
                    ((JArray)report["prefabs"]).Add(row);
                    double duration = prefab.GetComponentsInChildren<PlayableDirector>(true).Max(d => d.playableAsset.duration);
                    var times = new[] { 0d, .3d, .37d, .40d, .7d, 1.3d, duration * .5, Math.Max(0, duration - .02) }
                        .Concat(new[] { 2d, 2.5d }.Where(value => value < duration))
                        .Distinct().OrderBy(value => value).ToArray();
                    if (fixedTime.HasValue)
                    {
                        Require(fixedTime.Value <= duration, "Diagnostic sample exceeds the source Timeline duration.");
                        times = new[] { fixedTime.Value };
                    }
                    int visible = 0;
                    int nonemptySamples = 0;
                    var imageHashes = new HashSet<string>();
                    foreach (double time in times)
                    {
                        JObject sample = Sample(prefab, scene, camera, time, output, captureLayers, !diagnostic);
                        ((JArray)row["samples"]).Add(sample);
                        visible += (int)sample["visible_pixels"];
                        if ((int)sample["visible_pixels"] > 0) nonemptySamples++;
                        imageHashes.Add((string)sample["image_sha256"]);
                    }
                    Require(visible > 0, prefab.name + " produced no visible pixels at any sample.");
                    if (!fixedTime.HasValue)
                        Require(nonemptySamples > 1 && imageHashes.Count > 1, prefab.name + " did not produce multiple nonempty, changing samples.");
                    row["nonempty_samples"] = nonemptySamples;
                    row["distinct_sample_images"] = imageHashes.Count;
                    row["visual_review_required"] = true;
                    row["render_verified"] = true;
                }
                if (!diagnostic)
                {
                    MakeContactSheet(plan, output, (JArray)report["cgs"]);
                    report["cg_contact_sheet"] = "cg-contact-sheet.png";
                }
            }
            finally { Shader.SetGlobalFloat("_UnscaledTime", oldTime); EditorSceneManager.ClosePreviewScene(scene); }
            return report;
        }

        private static JObject Sample(GameObject prefab, Scene scene, Camera camera, double time, string output,
            bool captureLayers = false, bool strictActivation = true)
        {
            GameObject instance = null;
            var independentControllers = new List<PreviewController>();
            try
            {
                instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
                foreach (var transform in instance.GetComponentsInChildren<Transform>(true)) transform.gameObject.layer = 30;
                camera.cullingMask = 1 << 30;
                foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
                    foreach (var material in renderer.sharedMaterials)
                        Require(material && material.shader && material.shader.isSupported &&
                            material.shader.name != "Hidden/InternalErrorShader" && !ShaderUtil.ShaderHasError(material.shader),
                            "Missing, unsupported or broken preview shader: " + renderer.name);
                Shader.SetGlobalFloat("_UnscaledTime", (float)time);
                var directors = instance.GetComponentsInChildren<PlayableDirector>(true);
                var controlledDirectors = new HashSet<PlayableDirector>();
                var timelineAnimators = new HashSet<Animator>();
                foreach (var director in directors)
                {
                    var timeline = director.playableAsset as TimelineAsset;
                    Require(timeline, "Only verified Timeline assets may be evaluated.");
                    foreach (var track in timeline.GetOutputTracks())
                    {
                        Require(track is AnimationTrack || track is ActivationTrack || track is AudioTrack || track is ControlTrack,
                            "Preview refuses a script/signal or unknown Timeline track: " + track.GetType().FullName);
                        if (track is AnimationTrack)
                        {
                            var binding = director.GetGenericBinding(track);
                            Animator animator = binding as Animator;
                            if (!animator && binding is GameObject gameObject) animator = gameObject.GetComponent<Animator>();
                            if (animator) timelineAnimators.Add(animator);
                        }
                        foreach (var clip in track.GetClips())
                            if (clip.asset is ControlPlayableAsset control)
                            {
                                GameObject controlled = control.sourceGameObject.Resolve(director);
                                Require(!controlled || controlled.transform.IsChildOf(instance.transform), "ControlTrack references an external object.");
                                Require(!control.prefabGameObject, "Preview refuses ControlTrack prefab instantiation.");
                                if (controlled && control.updateDirector)
                                    foreach (var child in controlled.GetComponentsInChildren<PlayableDirector>(true))
                                        if (child != director) controlledDirectors.Add(child);
                            }
                    }
                    director.playOnAwake = false; director.timeUpdateMode = DirectorUpdateMode.Manual;
                }
                // A parent ControlTrack owns a nested director's clip-relative time.
                // Sampling that child again at the outer time would overwrite the real result.
                PlayableDirector[] roots = directors.Where(item => !controlledDirectors.Contains(item)).ToArray();
                foreach (var director in roots)
                {
                    director.RebuildGraph();
                    for (int i = 0; i < director.playableGraph.GetOutputCount(); i++)
                    {
                        PlayableOutput playableOutput = director.playableGraph.GetOutput(i);
                        if (playableOutput.GetPlayableOutputType() == typeof(AudioPlayableOutput)) playableOutput.SetWeight(0);
                    }
                    foreach (var source in instance.GetComponentsInChildren<AudioSource>(true)) { source.mute = true; source.volume = 0; }
                }
                foreach (var animator in instance.GetComponentsInChildren<Animator>(true))
                    if (animator.runtimeAnimatorController && !timelineAnimators.Contains(animator))
                        independentControllers.Add(new PreviewController(animator, instance.transform));
                // Seek-only Timeline previews leave independent Crack/Flash controllers at
                // their serialized defaults. Advance from zero in small steps, letting
                // Timeline activation run first, then advancing only independent controllers.
                const double step = 1d / 60d;
                double sampledTime = 0;
                int simulationSteps = 0;
                void EvaluateAt(double at, float delta)
                {
                    Shader.SetGlobalFloat("_UnscaledTime", (float)at);
                    foreach (var director in roots)
                    {
                        director.time = at; director.Evaluate();
                        UpdateLocalCoordinates(instance, "RefreshCoordinates");
                    }
                    foreach (var controller in independentControllers) controller.Advance(delta);
                    UpdateLocalCoordinates(instance, "RefreshCoordinates");
                    simulationSteps++;
                }
                EvaluateAt(0, 0);
                while (sampledTime < time)
                {
                    double next = Math.Min(time, sampledTime + step);
                    EvaluateAt(next, (float)(next - sampledTime));
                    sampledTime = next;
                }
                string filename = prefab.name + "-" + time.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture) + ".png";
                Texture2D pixels = Capture(camera, 960, 540);
                try
                {
                    Color32[] colors = pixels.GetPixels32();
                    int visible = colors.Count(color => color.a > 8);
                    int magenta = colors.Count(color => color.a > 128 && color.r > 220 && color.b > 220 && color.g < 30);
                    byte[] png = pixels.EncodeToPNG();
                    File.WriteAllBytes(Path.Combine(output, filename), png);
                    string hash;
                    using (var sha = System.Security.Cryptography.SHA256.Create())
                        hash = BitConverter.ToString(sha.ComputeHash(png)).Replace("-", "").ToLowerInvariant();
                    var row = new JObject { ["seconds"] = time, ["image"] = filename, ["visible_pixels"] = visible,
                        ["image_sha256"] = hash,
                        ["magenta_pixels"] = magenta, ["audio_muted"] = true,
                        ["directors_checked"] = directors.Length, ["directors_evaluated"] = directors.Length - controlledDirectors.Count,
                        ["directors_driven_by_control_tracks"] = controlledDirectors.Count,
                        ["timeline_bound_animators_excluded"] = timelineAnimators.Count,
                        ["simulation_step_seconds"] = step, ["simulation_steps"] = simulationSteps,
                        ["independent_controllers"] = new JArray(independentControllers.Select(controller => controller.Report())) };
                    if (prefab.name == "ObjectionCutIn_w" && new[] { .7d, 1.3d, 2.5d }.Any(at => Math.Abs(at - time) < .000001))
                        row["activation_curve_readback"] = CheckWActivation(sampledInstance: instance, time, strictActivation);
                    if (captureLayers)
                    {
                        row["layer_diagnostics"] = CaptureLayers(instance, camera, output, filename, colors, hash);
                        row["boolean_curve_ab"] = CheckBooleanCurveBindings(prefab, instance, scene, time);
                    }
                    return row;
                }
                finally { Object.DestroyImmediate(pixels); }
            }
            finally
            {
                foreach (var controller in independentControllers) controller.Dispose();
                if (instance)
                    try { UpdateLocalCoordinates(instance, "ReleaseCoordinates"); }
                    finally { Object.DestroyImmediate(instance); }
            }
        }

        private static JObject CheckWActivation(GameObject sampledInstance, double time, bool strict)
        {
            const string recordedName = "Recorded_8__7930763052336622142";
            var matches = (from director in sampledInstance.GetComponentsInChildren<PlayableDirector>(true)
                           from track in ((TimelineAsset)director.playableAsset).GetOutputTracks().OfType<AnimationTrack>()
                           from clip in track.GetClips()
                           where clip.asset is AnimationPlayableAsset animation && animation.clip && animation.clip.name == recordedName
                           select new { director, track, clip, animation = (AnimationPlayableAsset)clip.asset }).ToArray();
            Require(matches.Length == 1, "W activation readback needs the current Recorded_8 track exactly once.");
            var match = matches[0];
            var animator = match.director.GetGenericBinding(match.track) as Animator;
            Require(animator, "W activation track lost its Animator binding.");
            double localTime = (time - match.clip.start) * match.clip.timeScale + match.clip.clipIn;
            var bindings = AnimationUtility.GetCurveBindings(match.animation.clip)
                .Where(binding => binding.type == typeof(GameObject) && binding.propertyName == "m_IsActive").ToArray();
            Require(bindings.Length == 5, "W activation curve set changed.");
            var expected = new JObject();
            foreach (var binding in bindings)
                expected[binding.path] = AnimationUtility.GetEditorCurve(match.animation.clip, binding).Evaluate((float)localTime) > .5f;
            // Independently checked against the original Switch streamed clip (path
            // hashes 773931313 / 3072856203 / 3224313885): 001 until .75, 002 until
            // 1.5, 003 until 3.3333333 in clip time; track starts at .3333333383.
            string selected = time < 1 ? "001" : time < 2 ? "002" : "003";
            bool sourceStageMatches = new[] { "001", "002", "003" }.All(id =>
                expected.Value<bool>("RefuteCutIn_StainedGlass_" + id) == (id == selected));
            JObject actual = BooleanStates(animator.transform, bindings);
            bool floatBindings = bindings.All(binding => !binding.isDiscreteCurve && !binding.isPPtrCurve);
            bool matchesKeys = JToken.DeepEquals(actual, expected);
            var report = new JObject { ["clip"] = recordedName, ["clip_local_seconds"] = localTime,
                ["expected_from_saved_keys"] = expected, ["actual"] = actual, ["source_expected_stage"] = selected,
                ["saved_keys_match_source_stage"] = sourceStageMatches, ["float_bindings"] = floatBindings,
                ["matches_expected"] = matchesKeys, ["strict_assertion"] = strict,
                ["source_evidence"] = "Switch AnimationClip 7930763052336622142 Recorded_8, current W_CutIn_Timeline2/track2849755239698147902. Original streamed activation keys: 001=1@0,0@.75; 002=0@0,1@.75,0@1.5; 003=0@0,1@1.5,0@3.3333333. isIntCurve=0." };
            if (strict) Require(floatBindings && sourceStageMatches && matchesKeys,
                "W activation curve evaluation disagrees with source at " + time + " seconds: " + report.ToString(Newtonsoft.Json.Formatting.None));
            return report;
        }

        private static JObject CheckBooleanCurveBindings(GameObject prefab, GameObject sampledInstance, Scene scene, double time)
        {
            const string recordedName = "Recorded_8__7930763052336622142";
            var matches = (from director in sampledInstance.GetComponentsInChildren<PlayableDirector>(true)
                           from track in ((TimelineAsset)director.playableAsset).GetOutputTracks().OfType<AnimationTrack>()
                           from clip in track.GetClips()
                           where clip.asset is AnimationPlayableAsset animation && animation.clip && animation.clip.name == recordedName
                           select new { director, track, clip, animation = (AnimationPlayableAsset)clip.asset }).ToArray();
            Require(matches.Length == 1, "Expected the reviewed Recorded_8 activation clip exactly once.");
            var match = matches[0];
            var animator = match.director.GetGenericBinding(match.track) as Animator;
            Require(animator, "Recorded_8 must be bound to the actual CutIN Animator.");
            string rootPath = AnimationUtility.CalculateTransformPath(animator.transform, sampledInstance.transform);
            double localTime = (time - match.clip.start) * match.clip.timeScale + match.clip.clipIn;
            if (localTime < 0 || localTime > match.animation.clip.length)
                return new JObject { ["status"] = "SKIPPED", ["clip_local_seconds"] = localTime,
                    ["reason"] = "Direct clip A/B only samples inside Recorded_8; use Timeline time 2.5 seconds." };
            AnimationClip original = match.animation.clip;
            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(original)
                .Where(binding => binding.type == typeof(GameObject) && binding.propertyName == "m_IsActive").ToArray();
            Require(bindings.Length == 5, "The reviewed Recorded_8 must have five activation curves.");
            var expected = new JObject();
            var sourceBindings = new JArray();
            foreach (var binding in bindings)
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(original, binding);
                expected[binding.path] = curve.Evaluate((float)localTime) > .5f;
                sourceBindings.Add(new JObject { ["path"] = binding.path, ["property"] = binding.propertyName,
                    ["discrete"] = binding.isDiscreteCurve, ["pptr"] = binding.isPPtrCurve,
                    ["keys"] = new JArray(curve.keys.Select(key => new JObject { ["time"] = key.time, ["value"] = key.value })) });
            }
            AnimationClip floating = Object.Instantiate(original);
            floating.name = original.name + "_TransientFloatBindingProbe";
            floating.hideFlags = HideFlags.HideAndDontSave;
            try
            {
                foreach (var binding in bindings)
                {
                    AnimationCurve sourceCurve = AnimationUtility.GetEditorCurve(original, binding);
                    AnimationUtility.SetEditorCurve(floating, binding, null);
                    AnimationUtility.SetEditorCurve(floating, EditorCurveBinding.FloatCurve(binding.path, typeof(GameObject), "m_IsActive"),
                        new AnimationCurve(sourceCurve.keys) { preWrapMode = sourceCurve.preWrapMode, postWrapMode = sourceCurve.postWrapMode });
                }
                var rewritten = AnimationUtility.GetCurveBindings(floating)
                    .Where(binding => binding.type == typeof(GameObject) && binding.propertyName == "m_IsActive").ToArray();
                Require(rewritten.Length == bindings.Length && !AssetDatabase.Contains(floating), "A/B clip must remain a transient copy.");
                var current = BooleanStates(animator.transform, bindings);
                var a = SampleBooleanClip(prefab, scene, rootPath, original, (float)localTime, bindings, expected);
                var b = SampleBooleanClip(prefab, scene, rootPath, floating, (float)localTime, bindings, expected);
                return new JObject { ["source_clip"] = original.name, ["animator_path"] = rootPath,
                    ["timeline_seconds"] = time, ["clip_start"] = match.clip.start, ["clip_in"] = match.clip.clipIn,
                    ["time_scale"] = match.clip.timeScale, ["clip_local_seconds"] = localTime,
                    ["source_packed_is_int_curve"] = false, ["source_binding_flags"] = sourceBindings,
                    ["copied_binding_flags"] = new JArray(rewritten.Select(binding => new JObject { ["path"] = binding.path,
                        ["discrete"] = binding.isDiscreteCurve, ["pptr"] = binding.isPPtrCurve })),
                    ["expected_from_saved_keys"] = expected, ["actual_sampled_timeline"] = current,
                    ["timeline_matches_expected"] = JToken.DeepEquals(current, expected),
                    ["original_clip_sample_animation"] = a, ["float_copy_sample_animation"] = b,
                    ["persistent_clip_modified"] = false,
                    ["scope"] = "SampleAnimation A/B on separate temporary prefab instances; only the copy's five m_IsActive bindings are recreated as FloatCurve with identical keys. Ancestors of CutIN are enabled to match the active Timeline phase. The rewritten copy is not installed into the Timeline; this is a binding diagnostic, not a runtime-equivalence verdict." };
            }
            finally { Object.DestroyImmediate(floating); }
        }

        private static JObject SampleBooleanClip(GameObject prefab, Scene scene, string rootPath, AnimationClip clip, float time,
            EditorCurveBinding[] bindings, JObject expected)
        {
            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
            try
            {
                Transform root = instance.transform.Find(rootPath);
                Require(root, "Transient activation root is missing.");
                foreach (var source in instance.GetComponentsInChildren<AudioSource>(true)) { source.mute = true; source.volume = 0; }
                // The source's parent CutIN_2 starts inactive, then its ActivationTrack
                // enables it. Reproduce only that prerequisite for direct clip sampling.
                for (Transform item = root; item != null; item = item.parent) item.gameObject.SetActive(true);
                JObject before = BooleanStates(root, bindings);
                clip.SampleAnimation(root.gameObject, time);
                JObject after = BooleanStates(root, bindings);
                return new JObject { ["before"] = before, ["after"] = after,
                    ["matches_expected"] = JToken.DeepEquals(after, expected) };
            }
            finally { Object.DestroyImmediate(instance); }
        }

        private static JObject BooleanStates(Transform root, EditorCurveBinding[] bindings)
        {
            var states = new JObject();
            foreach (var binding in bindings)
            {
                Transform child = root.Find(binding.path);
                Require(child, "Activation target is missing: " + binding.path);
                states[binding.path] = child.gameObject.activeSelf;
            }
            return states;
        }

        private static JObject CaptureLayers(GameObject instance, Camera camera, string output, string fullImage, Color32[] fullPixels, string fullHash)
        {
            Renderer[] renderers = instance.GetComponentsInChildren<Renderer>(true);
            var enabled = renderers.ToDictionary(renderer => renderer, renderer => renderer.enabled);
            string PathOf(Renderer renderer) => AnimationUtility.CalculateTransformPath(renderer.transform, instance.transform);
            var groups = new Dictionary<string, Renderer[]>
            {
                ["background"] = renderers.Where(renderer => PathOf(renderer) == "BackGround").ToArray(),
                ["glass_band"] = renderers.Where(renderer => PathOf(renderer).StartsWith("CutIN_2/CutIN/", StringComparison.Ordinal) && !(renderer is SpriteMask)).ToArray(),
                ["glass_glow"] = renderers.Where(renderer => PathOf(renderer) == "CutIN_2/CutIN/RefuteCutIn_StainedGlass_luminescence").ToArray(),
                ["glass_texture"] = renderers.Where(renderer => renderer.sharedMaterials.Any(material =>
                    material && material.shader.name == "Manosaba/SwitchStory/Glasses")).ToArray(),
                ["glow"] = renderers.Where(renderer => !(renderer is SpriteMask) &&
                    renderer.sharedMaterials.Any(material => material && material.shader.name == "Manosaba/SwitchStory/LuminescenceW")).ToArray()
            };
            Require(groups.All(group => group.Value.Length > 0), "A requested double-objection diagnostic layer is missing.");
            var captures = new JArray();
            var full = PixelReport(fullPixels); full["mode"] = "full"; full["image"] = fullImage; full["image_sha256"] = fullHash;
            full["renderers"] = RendererReadback(renderers, instance.transform); captures.Add(full);
            try
            {
                foreach (var group in groups)
                    foreach (bool only in new[] { true, false })
                    {
                        foreach (Renderer renderer in renderers)
                            renderer.enabled = enabled[renderer] && (renderer is SpriteMask || (only ? group.Value.Contains(renderer) : !group.Value.Contains(renderer)));
                        string mode = (only ? "only_" : "without_") + group.Key;
                        string filename = Path.GetFileNameWithoutExtension(fullImage) + "-" + mode + ".png";
                        var readback = RendererReadback(renderers, instance.transform);
                        Texture2D pixels = Capture(camera, 960, 540);
                        try
                        {
                            byte[] png = pixels.EncodeToPNG(); File.WriteAllBytes(Path.Combine(output, filename), png);
                            var row = PixelReport(pixels.GetPixels32()); row["mode"] = mode; row["image"] = filename;
                            using (var sha = System.Security.Cryptography.SHA256.Create())
                                row["image_sha256"] = BitConverter.ToString(sha.ComputeHash(png)).Replace("-", "").ToLowerInvariant();
                            row["renderers"] = readback; captures.Add(row);
                        }
                        finally { Object.DestroyImmediate(pixels); }
                    }
                // A reversible renderer-order experiment, not a source-data repair.
                // The source gives both layers the same order; isolate that tie here.
                foreach (var item in enabled) item.Key.enabled = item.Value;
                Renderer glow = groups["glass_glow"].Single();
                var activeGlass = groups["glass_texture"].Where(renderer => renderer.enabled && renderer.gameObject.activeInHierarchy).ToArray();
                if (activeGlass.Length == 1)
                {
                    Renderer glass = activeGlass[0];
                    int originalOrder = glow.sortingOrder;
                    try
                    {
                        glow.sortingOrder = glass.sortingOrder - 1;
                        const string mode = "sorting_ab_glow_behind_glass";
                        string filename = Path.GetFileNameWithoutExtension(fullImage) + "-" + mode + ".png";
                        Texture2D pixels = Capture(camera, 960, 540);
                        try
                        {
                            byte[] png = pixels.EncodeToPNG(); File.WriteAllBytes(Path.Combine(output, filename), png);
                            var row = PixelReport(pixels.GetPixels32()); row["mode"] = mode; row["image"] = filename;
                            row["original_glow_order"] = originalOrder; row["temporary_glow_order"] = glow.sortingOrder;
                            row["glass_order"] = glass.sortingOrder; row["renderers"] = RendererReadback(renderers, instance.transform);
                            captures.Add(row);
                        }
                        finally { Object.DestroyImmediate(pixels); }
                    }
                    finally { glow.sortingOrder = originalOrder; }
                }
                else captures.Add(new JObject { ["mode"] = "sorting_ab_glow_behind_glass", ["status"] = "SKIPPED",
                    ["reason"] = "Sorting A/B requires exactly one active glass stage at the requested time." });
            }
            finally { foreach (var item in enabled) item.Key.enabled = item.Value; }
            return new JObject { ["groups"] = JObject.FromObject(groups.ToDictionary(group => group.Key, group => group.Value.Select(PathOf).ToArray())),
                ["glow_group_scope"] = "glow selects the actual LuminescenceW shader (character silhouettes); glass_glow separately selects the stained-glass luminescence strip. glass_band includes that strip plus its other renderer layers.",
                ["masks_preserved"] = true, ["groups_may_overlap"] = true, ["unity_Time_controlled"] = false,
                ["scope"] = "Temporary clone only: layer captures change Renderer.enabled; the explicitly named sorting A/B also puts glow one order behind active glass, then restores it. Existing inactive objects and masks remain intact. No Timeline re-evaluation or material mutation occurs.",
                ["captures"] = captures };
        }

        private static JObject PixelReport(Color32[] colors) => new JObject
        {
            ["visible_pixels"] = colors.Count(color => color.a > 8),
            ["opaque_black_pixels"] = colors.Count(color => color.a > 250 && color.r < 8 && color.g < 8 && color.b < 8),
            ["transparent_pixels"] = colors.Count(color => color.a == 0),
            ["alpha_min"] = colors.Min(color => color.a), ["alpha_max"] = colors.Max(color => color.a)
        };

        private static JArray RendererReadback(Renderer[] renderers, Transform root)
        {
            var rows = new JArray();
            foreach (Renderer renderer in renderers)
            {
                var globalBlock = new MaterialPropertyBlock(); renderer.GetPropertyBlock(globalBlock);
                var row = new JObject { ["path"] = AnimationUtility.CalculateTransformPath(renderer.transform, root),
                    ["type"] = renderer.GetType().Name, ["enabled"] = renderer.enabled,
                    ["active_self"] = renderer.gameObject.activeSelf, ["active_in_hierarchy"] = renderer.gameObject.activeInHierarchy,
                    ["sorting_layer"] = renderer.sortingLayerName, ["sorting_layer_id"] = renderer.sortingLayerID,
                    ["sorting_order"] = renderer.sortingOrder, ["position"] = VectorValues(renderer.transform.position),
                    ["lossy_scale"] = VectorValues(renderer.transform.lossyScale), ["property_block_empty"] = globalBlock.isEmpty };
                if (renderer is SpriteRenderer sprite)
                {
                    row["color"] = ColorValues(sprite.color); row["mask_interaction"] = sprite.maskInteraction.ToString();
                    row["sprite"] = sprite.sprite ? new JObject { ["name"] = sprite.sprite.name,
                        ["asset"] = AssetDatabase.GetAssetPath(sprite.sprite), ["texture"] = TextureInfo(sprite.sprite.texture) } : null;
                    row["sprite_texture_binding_scope"] = "Sprite texture is the SpriteRenderer per-draw source; shared material and user property blocks are reported separately, not asserted to be the GPU binding.";
                }
                var materials = new JArray(); row["materials"] = materials;
                for (int slot = 0; slot < renderer.sharedMaterials.Length; slot++)
                {
                    Material material = renderer.sharedMaterials[slot];
                    if (!material) { materials.Add(JValue.CreateNull()); continue; }
                    var slotBlock = new MaterialPropertyBlock(); renderer.GetPropertyBlock(slotBlock, slot);
                    var properties = new JObject();
                    for (int i = 0; i < material.shader.GetPropertyCount(); i++)
                    {
                        string name = material.shader.GetPropertyName(i); int id = Shader.PropertyToID(name);
                        ShaderPropertyType type = material.shader.GetPropertyType(i);
                        var value = new JObject { ["type"] = type.ToString(),
                            ["shared_material"] = PropertyValue(material, null, name, id, type),
                            ["renderer_block_present"] = globalBlock.HasProperty(id), ["slot_block_present"] = slotBlock.HasProperty(id) };
                        if (globalBlock.HasProperty(id)) value["renderer_block"] = PropertyValue(material, globalBlock, name, id, type);
                        if (slotBlock.HasProperty(id)) value["slot_block"] = PropertyValue(material, slotBlock, name, id, type);
                        if (type == ShaderPropertyType.Texture)
                        {
                            value["shared_texture_scale"] = VectorValues(material.GetTextureScale(name));
                            value["shared_texture_offset"] = VectorValues(material.GetTextureOffset(name));
                        }
                        properties[name] = value;
                    }
                    materials.Add(new JObject { ["slot"] = slot, ["name"] = material.name, ["asset"] = AssetDatabase.GetAssetPath(material),
                        ["shader"] = material.shader.name, ["render_queue"] = material.renderQueue,
                        ["keywords"] = new JArray(material.shaderKeywords), ["slot_property_block_empty"] = slotBlock.isEmpty,
                        ["declares_MainTex"] = material.HasProperty("_MainTex"), ["properties"] = properties });
                }
                rows.Add(row);
            }
            return rows;
        }

        private static JToken PropertyValue(Material material, MaterialPropertyBlock block, string name, int id, ShaderPropertyType type)
        {
            if (type == ShaderPropertyType.Color) return ColorValues(block == null ? material.GetColor(id) : block.GetColor(id));
            if (type == ShaderPropertyType.Vector) return VectorValues(block == null ? material.GetVector(id) : block.GetVector(id));
            if (type == ShaderPropertyType.Texture) return TextureInfo(block == null ? material.GetTexture(id) : block.GetTexture(id));
            if (type == ShaderPropertyType.Int) return new JValue(block == null ? material.GetInteger(id) : block.GetInteger(id));
            return new JValue(block == null ? material.GetFloat(id) : block.GetFloat(id));
        }

        private static JArray ColorValues(UnityEngine.Color value) => new JArray(value.r, value.g, value.b, value.a);
        private static JArray VectorValues(Vector4 value) => new JArray(value.x, value.y, value.z, value.w);
        private static JToken TextureInfo(UnityEngine.Texture texture) => texture ?
            new JObject { ["name"] = texture.name, ["asset"] = AssetDatabase.GetAssetPath(texture), ["width"] = texture.width, ["height"] = texture.height } : JValue.CreateNull();

        private sealed class PreviewController : IDisposable
        {
            private readonly Animator animator;
            private readonly string path;
            private PlayableGraph graph;
            private AnimatorControllerPlayable playable;
            private bool wasActive;
            private int activations, deactivations, advances;
            private double activeSeconds;

            public PreviewController(Animator animator, Transform root)
            {
                this.animator = animator;
                path = AnimationUtility.CalculateTransformPath(animator.transform, root);
                animator.fireEvents = false;
                animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            }

            public void Advance(float delta)
            {
                bool active = animator.isActiveAndEnabled;
                if (!active)
                {
                    if (wasActive)
                    {
                        deactivations++;
                        if (animator.keepAnimatorStateOnDisable) graph.Stop();
                        else Dispose();
                    }
                    wasActive = false;
                    return;
                }
                if (!wasActive)
                {
                    activations++;
                    if (!graph.IsValid())
                    {
                        graph = PlayableGraph.Create("SwitchStoryPreview_" + path);
                        graph.SetTimeUpdateMode(DirectorUpdateMode.Manual);
                        playable = AnimatorControllerPlayable.Create(graph, animator.runtimeAnimatorController);
                        AnimationPlayableOutput.Create(graph, "Controller", animator).SetSourcePlayable(playable);
                    }
                    graph.Play();
                    graph.Evaluate(0);
                }
                else
                {
                    graph.Evaluate(delta);
                    advances++;
                    activeSeconds += delta;
                }
                wasActive = true;
            }

            public JObject Report()
            {
                var row = new JObject { ["path"] = path, ["controller"] = animator.runtimeAnimatorController.name,
                    ["method"] = "Activation-aware AnimatorControllerPlayable; Timeline-bound Animators excluded",
                    ["activations"] = activations, ["deactivations"] = deactivations,
                    ["advance_calls"] = advances, ["active_seconds_advanced"] = activeSeconds,
                    ["active_at_capture"] = animator.isActiveAndEnabled };
                if (graph.IsValid() && playable.GetLayerCount() > 0)
                {
                    AnimatorStateInfo state = playable.GetCurrentAnimatorStateInfo(0);
                    row["state_hash"] = state.fullPathHash;
                    row["normalized_time"] = state.normalizedTime;
                }
                return row;
            }

            public void Dispose() { if (graph.IsValid()) graph.Destroy(); }
        }

        private static void UpdateLocalCoordinates(GameObject instance, string methodName)
        {
            // The game assembly is not auto-referenced by Editor tools. Invoke only this
            // explicitly reviewed preview API, including temporarily inactive Timeline targets.
            foreach (var component in instance.GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (!component || component.GetType().FullName != "WitchTrials.Views.SpriteLocalCoords") continue;
                var method = component.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance, null, Type.EmptyTypes, null);
                Require(method != null && method.ReturnType == typeof(void), "Missing SpriteLocalCoords preview API: " + methodName);
                method.Invoke(component, null);
            }
        }

        private static void MakeContactSheet(JObject plan, string output, JArray rows)
        {
            var cgs = ((JArray)plan["addresses"]).OfType<JObject>().Where(row =>
                ((string)row["address"]).StartsWith("Naninovel/Backgrounds/Stills/", StringComparison.Ordinal)).ToArray();
            if (cgs.Length == 0) return;
            const int columns = 4, width = 384, height = 216;
            int sheetRows = (cgs.Length + columns - 1) / columns;
            var sheet = new Texture2D(columns * width, sheetRows * height, TextureFormat.RGBA32, false);
            try
            {
                sheet.SetPixels32(Enumerable.Repeat(new Color32(32, 32, 32, 255), sheet.width * sheet.height).ToArray());
                for (int i = 0; i < cgs.Length; i++)
                {
                    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>((string)cgs[i]["path"]);
                    Require(texture, "CG texture is missing: " + cgs[i]["path"]);
                    float scale = Mathf.Min((float)width / texture.width, (float)height / texture.height);
                    int fittedWidth = Mathf.Max(1, Mathf.RoundToInt(texture.width * scale));
                    int fittedHeight = Mathf.Max(1, Mathf.RoundToInt(texture.height * scale));
                    Texture2D thumbnail = BlitReadable(texture, fittedWidth, fittedHeight);
                    try { sheet.SetPixels((i % columns) * width + (width - fittedWidth) / 2,
                        (sheetRows - 1 - i / columns) * height + (height - fittedHeight) / 2,
                        fittedWidth, fittedHeight, thumbnail.GetPixels()); }
                    finally { Object.DestroyImmediate(thumbnail); }
                    rows.Add(new JObject { ["address"] = cgs[i]["address"].DeepClone(), ["row"] = i / columns + 1,
                        ["column"] = i % columns + 1, ["width"] = texture.width, ["height"] = texture.height,
                        ["preview_note"] = "Thumbnail is letterboxed with its aspect ratio preserved; source dimensions remain unchanged." });
                }
                sheet.Apply();
                File.WriteAllBytes(Path.Combine(output, "cg-contact-sheet.png"), sheet.EncodeToPNG());
            }
            finally { Object.DestroyImmediate(sheet); }
        }

        private static Texture2D Capture(Camera camera, int width, int height)
        {
            var target = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
            RenderTexture previous = RenderTexture.active;
            try
            {
                camera.targetTexture = target; camera.Render(); RenderTexture.active = target;
                var result = new Texture2D(width, height, TextureFormat.RGBA32, false);
                result.ReadPixels(new Rect(0, 0, width, height), 0, 0); result.Apply(); return result;
            }
            finally { camera.targetTexture = null; RenderTexture.active = previous; RenderTexture.ReleaseTemporary(target); }
        }

        private static Texture2D BlitReadable(Texture2D source, int width, int height)
        {
            var target = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
            RenderTexture previous = RenderTexture.active;
            try
            {
                Graphics.Blit(source, target); RenderTexture.active = target;
                var result = new Texture2D(width, height, TextureFormat.RGBA32, false);
                result.ReadPixels(new Rect(0, 0, width, height), 0, 0); result.Apply(); return result;
            }
            finally { RenderTexture.active = previous; RenderTexture.ReleaseTemporary(target); }
        }

        private static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
    }
}
