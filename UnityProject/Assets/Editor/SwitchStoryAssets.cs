// Opt-in adapter for the reviewed Switch story cut-ins. Serialized graphs still
// go through PrefabBuilder; only platform materials/controllers need translation.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace Manosaba.EditorTools
{
    internal sealed class SwitchStoryAssets
    {
        private const string Root = "Assets/GameContent/#WitchTrials/SwitchStory";
        private readonly Dictionary<long, Object> pointers = new Dictionary<long, Object>();
        private readonly List<ControllerBinding> controllers = new List<ControllerBinding>();
        private readonly Dictionary<string, Sprite> particleSprites = new Dictionary<string, Sprite>();
        private readonly Dictionary<string, List<ExposedBinding>> exposedBindings = new Dictionary<string, List<ExposedBinding>>();
        private sealed class ExposedBinding
        {
            public PropertyName name;
            public string targetPath;
        }
        private sealed class ControllerBinding
        {
            public JObject recipe;
            public AnimatorController controller;
        }

        public static SwitchStoryAssets Prepare(JToken job, string target)
        {
            if (job["adapter"] == null && job["story_materials"] == null && job["story_controllers"] == null) return null;
            if (job.Value<string>("adapter") != "switch-story-cutin-v1" ||
                !target.StartsWith(Root + "/Prefabs/", StringComparison.Ordinal) || target.Contains("..") ||
                !target.EndsWith(".prefab", StringComparison.Ordinal))
                throw new InvalidOperationException("Unsupported/unsafe Switch story prefab adapter");
            var result = new SwitchStoryAssets();
            foreach (JObject recipe in (job["story_materials"] as JArray ?? new JArray()).OfType<JObject>())
                result.Add(recipe.Value<long>("pid"), BuildMaterial(recipe));
            foreach (JObject recipe in (job["story_controllers"] as JArray ?? new JArray()).OfType<JObject>())
            {
                // Each prefab owns its controller: motions are subassets of that prefab,
                // so a shared external controller would be rebound to the last generated job.
                string name = AssetName(Path.GetFileNameWithoutExtension(target) + "_" + recipe.Value<long>("pid"));
                string path = Root + "/Controllers/" + name + ".controller";
                EnsureFolder(Root + "/Controllers");
                var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
                if (controller == null) controller = AnimatorController.CreateAnimatorControllerAtPath(path);
                result.controllers.Add(new ControllerBinding { recipe = recipe, controller = controller });
                result.Add(recipe.Value<long>("pid"), controller);
            }
            return result;
        }

        private void Add(long pid, Object asset)
        {
            if (pid == 0 || pointers.ContainsKey(pid)) throw new InvalidOperationException("Duplicate/zero story asset identity");
            pointers.Add(pid, asset);
        }

        public Object Resolve(JToken pointer)
        {
            if ((pointer?["m_FileID"]?.Value<int>() ?? -1) != 0) return null;
            return pointers.TryGetValue(pointer["m_PathID"]?.Value<long>() ?? 0, out Object asset) ? asset : null;
        }

        public HashSet<string> IgnoredKeys(Object target, IEnumerable<string> baseline)
        {
            var keys = new HashSet<string>(baseline, StringComparer.Ordinal);
            if (target is PlayableDirector) keys.Add("m_ExposedReferences");
            if (target is ControlPlayableAsset) keys.Add("sourceGameObject");
            return keys;
        }

        private static PropertyName ExposedName(JToken token)
        {
            string source = token?.Value<string>();
            if (string.IsNullOrEmpty(source)) return default(PropertyName);
            // The compiled dump retains a numeric PropertyName identity, not its editor
            // text. Register a deterministic name on BOTH sides so Unity can persist it.
            if (!int.TryParse(source, out _)) throw new InvalidOperationException("Unreviewed story exposed-reference identity");
            return new PropertyName("SwitchStory_" + source);
        }

        public void ApplyTypedFields(Object target, JObject tree, Func<JToken, Object> resolve)
        {
            if (target is PlayableDirector director)
            {
                var recipes = tree["m_ExposedReferences"]?["m_References"] as JArray ?? new JArray();
                var used = new HashSet<PropertyName>();
                var expected = new List<ExposedBinding>();
                foreach (JToken row in recipes)
                {
                    if (!(row is JArray pair) || pair.Count != 2) throw new InvalidOperationException("Invalid story exposed-reference pair");
                    PropertyName name = ExposedName(pair[0]);
                    if (PropertyName.IsNullOrEmpty(name) || !used.Add(name)) throw new InvalidOperationException("Duplicate/empty story exposed name");
                    Object value = pair[1].Value<long>("m_PathID") == 0 ? null : resolve(pair[1]);
                    if (pair[1].Value<long>("m_PathID") != 0 && !(value is GameObject))
                        throw new InvalidOperationException("Story exposed reference must resolve to its GameObject");
                    var gameObject = value as GameObject;
                    if (gameObject != null && !gameObject.transform.IsChildOf(director.transform.root))
                        throw new InvalidOperationException("Story exposed reference leaves prefab hierarchy");
                    director.SetReferenceValue(name, value);
                    expected.Add(new ExposedBinding { name = name,
                        targetPath = gameObject == null ? null : AnimationUtility.CalculateTransformPath(gameObject.transform, director.transform.root) });
                }
                exposedBindings[AnimationUtility.CalculateTransformPath(director.transform, director.transform.root)] = expected;
                EditorUtility.SetDirty(director);
            }
            else if (target is ControlPlayableAsset control && tree["sourceGameObject"] is JObject source)
            {
                JToken pointer = source["defaultValue"];
                Object value = pointer?.Value<long>("m_PathID") == 0 ? null : resolve(pointer);
                if (pointer?.Value<long>("m_PathID") != 0 && !(value is GameObject))
                    throw new InvalidOperationException("Story ControlPlayable default reference is missing");
                control.sourceGameObject = new ExposedReference<GameObject>
                    { exposedName = ExposedName(source["exposedName"]), defaultValue = value as GameObject };
                EditorUtility.SetDirty(control);
            }
        }

        public HashSet<string> AnimationPaths(AnimationClip clip, GameObject root, Dictionary<string, Object> objects)
        {
            var roots = new HashSet<Transform>();
            foreach (var director in root.GetComponentsInChildren<PlayableDirector>(true))
            {
                if (!(director.playableAsset is TimelineAsset timeline)) continue;
                foreach (var track in timeline.GetOutputTracks().OfType<AnimationTrack>())
                {
                    if (track.infiniteClip != clip && !track.GetClips().Any(c => c.asset is AnimationPlayableAsset a && a.clip == clip)) continue;
                    Object binding = director.GetGenericBinding(track);
                    Transform target = binding is Component component ? component.transform : (binding as GameObject)?.transform;
                    if (target == null) throw new InvalidOperationException("Story animation track lacks its actual root: " + track.name);
                    roots.Add(target);
                }
            }
            foreach (var binding in controllers)
            {
                bool usesClip = binding.recipe["states"].OfType<JObject>().Any(s =>
                    s.Value<string>("clip_pid") is string id && objects.TryGetValue(id, out Object value) && value == clip);
                if (!usesClip) continue;
                foreach (string id in binding.recipe["animator_pids"].Values<string>())
                {
                    if (!objects.TryGetValue(id, out Object value) || !(value is Animator animator))
                        throw new InvalidOperationException("Story controller lacks actual Animator root");
                    roots.Add(animator.transform);
                }
            }
            // Old, detached Timeline subassets may remain in the shipped dependency
            // graph. They are never executed by this director; keep the generic handling.
            if (roots.Count == 0) return null;
            HashSet<string> allowed = null;
            foreach (Transform animationRoot in roots)
            {
                var paths = new HashSet<string>(animationRoot.GetComponentsInChildren<Transform>(true)
                    .Select(t => AnimationUtility.CalculateTransformPath(t, animationRoot)), StringComparer.Ordinal);
                if (allowed != null && !allowed.SetEquals(paths))
                    throw new InvalidOperationException("Unreviewed story clip shared across different hierarchy shapes: " + clip.name);
                allowed = paths;
            }
            return allowed;
        }

        public void CompleteControllers(Dictionary<string, Object> objects)
        {
            foreach (var binding in controllers)
            {
                var controller = binding.controller;
                if (!(binding.recipe["states"] is JArray states) || states.Count == 0 ||
                    controller.layers.Length != 1 || controller.parameters.Length != 0)
                    throw new InvalidOperationException("Story controller must have one parameter-free layer");
                var machine = controller.layers[0].stateMachine;
                if (machine.stateMachines.Length != 0 || machine.anyStateTransitions.Length != 0 ||
                    machine.entryTransitions.Length != 0 || machine.states.Any(s => s.state.transitions.Length != 0))
                    throw new InvalidOperationException("Unreviewed story controller transitions/nesting");
                var requested = states.Select(s => s.Value<string>("name")).ToList();
                string defaultName = binding.recipe.Value<string>("default_state");
                if (requested.Any(string.IsNullOrEmpty) || requested.Distinct(StringComparer.Ordinal).Count() != requested.Count ||
                    !requested.Contains(defaultName)) throw new InvalidOperationException("Invalid story state names/default");
                foreach (var child in machine.states)
                    if (!requested.Contains(child.state.name)) machine.RemoveState(child.state);
                foreach (JObject recipe in states.OfType<JObject>())
                {
                    string name = recipe.Value<string>("name"), clipId = recipe.Value<string>("clip_pid");
                    var state = machine.states.Select(s => s.state).SingleOrDefault(s => s.name == name) ?? machine.AddState(name);
                    AnimationClip clip = null;
                    if (clipId != null && (!objects.TryGetValue(clipId, out Object value) || (clip = value as AnimationClip) == null ||
                        !EditorUtility.IsPersistent(clip))) throw new InvalidOperationException("Story controller clip missing: " + clipId);
                    state.motion = clip;
                    state.speed = recipe.Value<float>("speed");
                    state.cycleOffset = recipe.Value<float>("cycle_offset");
                    state.mirror = recipe.Value<bool>("mirror");
                    state.iKOnFeet = recipe.Value<bool>("ik_on_feet");
                    state.writeDefaultValues = recipe.Value<bool>("write_default_values");
                    if (float.IsNaN(state.speed) || float.IsInfinity(state.speed)) throw new InvalidOperationException("Nonfinite story state speed");
                    if (clip != null)
                    {
                        var settings = AnimationUtility.GetAnimationClipSettings(clip);
                        settings.loopTime = recipe.Value<bool>("loop");
                        AnimationUtility.SetAnimationClipSettings(clip, settings);
                        EditorUtility.SetDirty(clip);
                    }
                    if (name == defaultName) machine.defaultState = state;
                    EditorUtility.SetDirty(state);
                }
                foreach (string id in binding.recipe["animator_pids"].Values<string>())
                {
                    if (!objects.TryGetValue(id, out Object value) || !(value is Animator animator))
                        throw new InvalidOperationException("Story controller has no Animator: " + id);
                    animator.runtimeAnimatorController = controller;
                    EditorUtility.SetDirty(animator);
                }
                EditorUtility.SetDirty(machine);
                EditorUtility.SetDirty(controller);
            }
        }

        public void RebindSavedClips(string prefabPath, Dictionary<string, string> clipNames)
        {
            var imported = AssetDatabase.LoadAllAssetsAtPath(prefabPath).OfType<AnimationClip>().ToArray();
            foreach (var binding in controllers)
            {
                var machine = binding.controller.layers[0].stateMachine;
                foreach (JObject recipe in binding.recipe["states"].OfType<JObject>())
                {
                    string id = recipe.Value<string>("clip_pid");
                    if (id == null) continue; // Background's default state deliberately has no motion.
                    if (!clipNames.TryGetValue(id, out string name)) throw new InvalidOperationException("Unknown story clip ID " + id);
                    var matches = imported.Where(c => c.name == name).ToArray();
                    if (matches.Length != 1) throw new InvalidOperationException("Ambiguous imported story clip " + name);
                    var state = machine.states.Select(s => s.state).Single(s => s.name == recipe.Value<string>("name"));
                    state.motion = matches[0];
                    EditorUtility.SetDirty(state);
                }
                EditorUtility.SetDirty(binding.controller);
            }
            AssetDatabase.SaveAssets();
        }

        public void BindParticleTextures(GameObject root)
        {
            foreach (var particle in root.GetComponentsInChildren<ParticleSystem>(true))
            {
                var sheet = particle.textureSheetAnimation;
                if (!sheet.enabled) continue;
                string hierarchy = AnimationUtility.CalculateTransformPath(particle.transform, root.transform);
                if (sheet.mode != ParticleSystemAnimationMode.Sprites || sheet.spriteCount != 1)
                    throw new InvalidOperationException("Unreviewed story particle sheet: " + hierarchy);
                var serialized = new SerializedObject(particle);
                var pointer = serialized.FindProperty("UVModule.sprites").GetArrayElementAtIndex(0).FindPropertyRelative("sprite");
                string path = AssetDatabase.GetAssetPath(pointer.objectReferenceValue);
                if (!path.StartsWith(Root + "/Textures/", StringComparison.Ordinal))
                    throw new InvalidOperationException("Story particle Sprite not in recipe: " + hierarchy);
                var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
                if (sprites.Length != 1) throw new InvalidOperationException("Ambiguous story particle Sprite: " + path);
                sheet.SetSprite(0, sprites[0]);
                var renderer = particle.GetComponent<ParticleSystemRenderer>();
                if (renderer == null || renderer.sharedMaterials.Length != 1 || renderer.sharedMaterial == null ||
                    !renderer.sharedMaterial.HasProperty("_MainTex")) throw new InvalidOperationException("Invalid story particle material");
                Material source = renderer.sharedMaterial;
                string name = AssetName(source.name + "__Particle_" + sprites[0].name);
                string materialPath = Root + "/Materials/" + name + ".mat";
                var material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                if (material == null) { material = new Material(source) { name = name }; AssetDatabase.CreateAsset(material, materialPath); }
                else { material.shader = source.shader; material.CopyPropertiesFromMaterial(source); }
                material.SetTexture("_MainTex", sprites[0].texture);
                renderer.sharedMaterial = material;
                particleSprites.Add(hierarchy, sprites[0]);
                EditorUtility.SetDirty(material); EditorUtility.SetDirty(renderer); EditorUtility.SetDirty(particle);
            }
        }

        public void Verify(GameObject saved)
        {
            foreach (var pair in exposedBindings)
            {
                Transform transform = pair.Key.Length == 0 ? saved.transform : saved.transform.Find(pair.Key);
                var director = transform != null ? transform.GetComponent<PlayableDirector>() : null;
                if (director == null) throw new InvalidOperationException("Saved story director missing: " + pair.Key);
                foreach (var entry in pair.Value)
                {
                    Object actual = director.GetReferenceValue(entry.name, out bool valid);
                    Transform expected = entry.targetPath == null ? null : entry.targetPath.Length == 0 ? saved.transform : saved.transform.Find(entry.targetPath);
                    if (!valid || (entry.targetPath != null && expected == null) || actual != (expected == null ? null : expected.gameObject))
                        throw new InvalidOperationException("Saved story exposed-reference identity changed: " + entry.name);
                }
                if (director.playableAsset is TimelineAsset timeline)
                    foreach (var track in timeline.GetOutputTracks().SelectMany(Descendants))
                        foreach (var clip in track.GetClips())
                            if (clip.asset is ControlPlayableAsset control)
                            {
                                var entry = pair.Value.SingleOrDefault(e => e.name == control.sourceGameObject.exposedName);
                                if (entry == null || entry.targetPath == null)
                                    throw new InvalidOperationException("Saved story ControlPlayable has no source identity");
                                Transform expected = entry.targetPath.Length == 0 ? saved.transform : saved.transform.Find(entry.targetPath);
                                if (expected == null || control.sourceGameObject.Resolve(director) != expected.gameObject)
                                    throw new InvalidOperationException("Saved story ControlPlayable resolves to wrong object: " + entry.name);
                            }
            }
            foreach (var binding in controllers)
            {
                var machine = binding.controller.layers[0].stateMachine;
                if (machine.defaultState == null || machine.defaultState.name != binding.recipe.Value<string>("default_state") ||
                    saved.GetComponentsInChildren<Animator>(true).Count(a => a.runtimeAnimatorController == binding.controller) !=
                    binding.recipe["animator_pids"].Count()) throw new InvalidOperationException("Saved story controller/default/binding mismatch");
                foreach (JObject recipe in binding.recipe["states"].OfType<JObject>())
                {
                    var state = machine.states.Select(s => s.state).Single(s => s.name == recipe.Value<string>("name"));
                    bool empty = recipe.Value<string>("clip_pid") == null;
                    if ((empty && state.motion != null) || (!empty && (!(state.motion is AnimationClip clip) ||
                        !EditorUtility.IsPersistent(clip) || AnimationUtility.GetAnimationClipSettings(clip).loopTime != recipe.Value<bool>("loop"))))
                        throw new InvalidOperationException("Saved story controller motion/loop mismatch: " + state.name);
                    if (state.speed != recipe.Value<float>("speed") || state.cycleOffset != recipe.Value<float>("cycle_offset") ||
                        state.mirror != recipe.Value<bool>("mirror") || state.iKOnFeet != recipe.Value<bool>("ik_on_feet") ||
                        state.writeDefaultValues != recipe.Value<bool>("write_default_values"))
                        throw new InvalidOperationException("Saved story controller state parameters mismatch: " + state.name);
                }
            }
            int found = 0;
            foreach (var particle in saved.GetComponentsInChildren<ParticleSystem>(true))
            {
                string path = AnimationUtility.CalculateTransformPath(particle.transform, saved.transform);
                var sheet = particle.textureSheetAnimation;
                if (!sheet.enabled) continue;
                var material = particle.GetComponent<ParticleSystemRenderer>().sharedMaterial;
                if (!particleSprites.TryGetValue(path, out Sprite expected) || sheet.spriteCount != 1 || sheet.GetSprite(0) != expected ||
                    material == null || material.GetTexture("_MainTex") != expected.texture)
                    throw new InvalidOperationException("Saved story particle Sprite/texture mismatch: " + path);
                found++;
            }
            if (found != particleSprites.Count) throw new InvalidOperationException("Story particle bindings changed during save");
        }

        private static IEnumerable<TrackAsset> Descendants(TrackAsset track)
        {
            yield return track;
            foreach (var child in track.GetChildTracks())
                foreach (var nested in Descendants(child)) yield return nested;
        }

        private static readonly Dictionary<string, string> Shaders = new Dictionary<string, string>
        {
            {"Shader Graphs/GreyScale", "Manosaba/SwitchStory/GreyScale"},
            {"Shader Graphs/GlassBreakRadial", "Manosaba/SwitchStory/GlassBreakRadial"},
            {"Shader Graphs/Chara_Shadow", "Manosaba/SwitchStory/CharaShadow"},
            {"Shader Graphs/Background_Moya", "Manosaba/SwitchStory/BackgroundMoya"},
            {"Shader Graphs/Background_Moya_w", "Manosaba/SwitchStory/BackgroundMoyaW"},
            {"Shader Graphs/Iuminescence_Silhouette_W", "Manosaba/SwitchStory/LuminescenceW"},
            {"Shader Graphs/Background_0Fix_Performant", "Manosaba/SwitchStory/BackgroundPerformant"},
            {"Shader Graphs/Background_0Fix", "Manosaba/CutIn/Background"},
            {"Shader Graphs/Glasses_0Fix", "Manosaba/SwitchStory/Glasses"},
            {"Shader Graphs/Iuminescence_dezolve_0Fix", "Manosaba/SwitchStory/LuminescenceDissolve"},
            {"Shader Graphs/Iuminescence_Silhouette_0Fix", "Manosaba/CutIn/LuminescenceSilhouette"},
            {"Shader Graphs/Shadow_Fix", "Manosaba/CutIn/Shadow"},
            {"Shader Graphs/Sprites_Default_Graph", "Sprites/Default"},
            {"Universal Render Pipeline/2D/Sprite-Unlit-Default", "Sprites/Default"},
            {"Sprites/Mask", "Sprites/Mask"},
            {"Mobile/Particles/Additive", "Manosaba/SwitchStory/Particles"},
            {"Mobile/Particles/Alpha Blended", "Manosaba/SwitchStory/Particles"},
            {"Universal Render Pipeline/Particles/Unlit", "Manosaba/SwitchStory/Particles"}
        };

        private static Material BuildMaterial(JObject recipe)
        {
            string sourceShader = recipe.Value<string>("shader");
            if (!Shaders.TryGetValue(sourceShader, out string name)) throw new InvalidOperationException("Unreviewed story shader: " + sourceShader);
            Shader shader = Shader.Find(name);
            if (shader == null || ShaderUtil.ShaderHasError(shader)) throw new InvalidOperationException("Missing/broken story shader: " + name);
            string materialName = AssetName(recipe.Value<string>("name")), path = Root + "/Materials/" + materialName + ".mat";
            EnsureFolder(Root + "/Materials");
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material == null) { material = new Material(shader) { name = materialName }; AssetDatabase.CreateAsset(material, path); }
            else material.shader = shader;
            JObject properties = recipe["properties"] as JObject ?? throw new InvalidOperationException("Missing story material properties");
            bool particles = name == "Manosaba/SwitchStory/Particles";
            string Property(string key) => particles && key == "_BaseMap" ? "_MainTex" :
                particles && key == "_BaseColor" ? "_Color" : key;
            foreach (var pair in Pairs(properties["m_Floats"]).Concat(Pairs(properties["m_Ints"])))
                if (material.HasProperty(Property(pair.Key))) material.SetFloat(Property(pair.Key), pair.Value.Value<float>());
            foreach (var pair in Pairs(properties["m_Colors"]))
                if (material.HasProperty(Property(pair.Key))) material.SetVector(Property(pair.Key),
                    new Vector4(Number(pair.Value, "r"), Number(pair.Value, "g"), Number(pair.Value, "b"), Number(pair.Value, "a")));
            if (sourceShader == "Mobile/Particles/Additive")
            {
                material.SetFloat("_SrcBlend", 5); material.SetFloat("_DstBlend", 1);
                material.SetFloat("_SrcBlendAlpha", 5); material.SetFloat("_DstBlendAlpha", 1);
            }
            else if (sourceShader == "Mobile/Particles/Alpha Blended")
            {
                material.SetFloat("_SrcBlend", 5); material.SetFloat("_DstBlend", 10);
                material.SetFloat("_SrcBlendAlpha", 5); material.SetFloat("_DstBlendAlpha", 10);
            }
            var textures = recipe["textures"] as JObject ?? new JObject();
            foreach (var pair in Pairs(properties["m_TexEnvs"]))
            {
                string property = Property(pair.Key);
                string texturePath = textures.Value<string>(pair.Key);
                if (!material.HasProperty(property))
                {
                    if (texturePath != null) throw new InvalidOperationException("Story shader lacks source texture property: " + sourceShader + ":" + pair.Key);
                    continue;
                }
                Texture2D texture = null;
                if (texturePath != null)
                {
                    if (!texturePath.StartsWith(Root + "/Textures/", StringComparison.Ordinal) || texturePath.Contains(".."))
                        throw new InvalidOperationException("Unsafe story material texture path");
                    texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
                    if (texture == null) throw new InvalidOperationException("Story material texture missing: " + texturePath);
                }
                material.SetTexture(property, texture);
                material.SetTextureScale(property, new Vector2(Number(pair.Value["m_Scale"], "x", 1), Number(pair.Value["m_Scale"], "y", 1)));
                material.SetTextureOffset(property, new Vector2(Number(pair.Value["m_Offset"], "x"), Number(pair.Value["m_Offset"], "y")));
            }
            EditorUtility.SetDirty(material);
            return material;
        }

        private static IEnumerable<KeyValuePair<string, JToken>> Pairs(JToken token) =>
            (token as JArray ?? new JArray()).OfType<JArray>().Select(p => new KeyValuePair<string, JToken>((string)p[0], p[1]));
        private static float Number(JToken token, string key, float fallback = 0) => token?[key]?.Value<float>() ?? fallback;
        private static string AssetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name == "." || name == ".." || name.Contains("/") || name.Contains("\\") ||
                name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) throw new InvalidOperationException("Invalid story asset name");
            return name;
        }
        private static void EnsureFolder(string folder)
        {
            if (AssetDatabase.IsValidFolder(folder)) return;
            string parent = folder.Substring(0, folder.LastIndexOf('/'));
            EnsureFolder(parent); AssetDatabase.CreateFolder(parent, folder.Substring(parent.Length + 1));
        }
    }
}
