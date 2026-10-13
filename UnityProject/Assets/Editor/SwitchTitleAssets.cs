// Only the opt-in SwitchTitle recipe uses these adapters. Shader Graph binaries from
// Switch cannot run in the built-in PC pipeline; Fog/Noise are parameter-driven approximations.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manosaba.EditorTools
{
    internal sealed class SwitchTitleAssets
    {
        private const string Root = "Assets/GameContent/#WitchTrials/SwitchTitle";
        private readonly Dictionary<long, Object> pointers = new Dictionary<long, Object>();
        private JObject controllerRecipe;
        private AnimatorController controller;
        private readonly Dictionary<string, ParticleTextureBinding> particleTextures = new Dictionary<string, ParticleTextureBinding>();

        private sealed class ParticleTextureBinding
        {
            public Sprite sprite;
            public Material sourceMaterial;
            public string materialPath;
        }

        public static SwitchTitleAssets Prepare(JToken job, string target)
        {
            if (job["title_materials"] == null && job["title_controller"] == null) return null;
            if (!target.StartsWith(Root + "/Prefabs/", StringComparison.Ordinal) ||
                target.Contains("..") || !target.EndsWith(".prefab", StringComparison.Ordinal))
                throw new InvalidOperationException("Switch title recipe must target its dedicated prefab folder");
            var result = new SwitchTitleAssets();
            foreach (JObject recipe in (job["title_materials"] as JArray ?? new JArray()).OfType<JObject>())
            {
                long id = recipe.Value<long>("pid");
                if (id == 0 || result.pointers.ContainsKey(id))
                    throw new InvalidOperationException("Duplicate/zero title material path id");
                result.pointers.Add(id, BuildMaterial(recipe));
            }
            result.controllerRecipe = job["title_controller"] as JObject;
            if (result.controllerRecipe != null)
            {
                string name = AssetName(result.controllerRecipe.Value<string>("name"));
                if (name != "TitleEma" && name != "TitleHiro" && name != "TitleED3")
                    throw new InvalidOperationException("Only the three simple title controllers are supported");
                string path = Root + "/Controllers/" + name + ".controller";
                EnsureFolder(Root + "/Controllers");
                result.controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
                if (result.controller == null)
                    result.controller = AnimatorController.CreateAnimatorControllerAtPath(path);
                result.pointers.Add(result.controllerRecipe.Value<long>("pid"), result.controller);
            }
            return result;
        }

        public Object Resolve(JToken pointer)
        {
            // Planner normalizes recipe objects to fileID 0 so a coincidental path id in
            // another serialized file cannot shadow an unrelated external object.
            if ((pointer?["m_FileID"]?.Value<int>() ?? -1) != 0) return null;
            return pointers.TryGetValue(pointer["m_PathID"]?.Value<long>() ?? 0, out Object asset) ? asset : null;
        }

        public int BindParticleTextures(GameObject root)
        {
            foreach (ParticleSystem particle in root.GetComponentsInChildren<ParticleSystem>(true))
            {
                string hierarchyPath = AnimationUtility.CalculateTransformPath(particle.transform, root.transform);
                var sheet = particle.textureSheetAnimation;
                if (!sheet.enabled || sheet.mode != ParticleSystemAnimationMode.Sprites || sheet.spriteCount != 1)
                    throw new InvalidOperationException("Title particle requires one enabled UV sprite: " + hierarchyPath);
                // The generic pointer mapper can resolve a PNG's main Texture2D for this nested
                // sprite field. Resolve its single Sprite subasset explicitly before using the API.
                var serialized = new SerializedObject(particle);
                var spriteReference = serialized.FindProperty("UVModule.sprites").GetArrayElementAtIndex(0).FindPropertyRelative("sprite");
                string spritePath = AssetDatabase.GetAssetPath(spriteReference.objectReferenceValue);
                if (!spritePath.StartsWith(Root + "/Textures/", StringComparison.Ordinal))
                    throw new InvalidOperationException("Title particle sprite is missing/outside recipe textures: " + hierarchyPath);
                Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spritePath).OfType<Sprite>().ToArray();
                if (sprites.Length != 1 || sprites[0].texture == null)
                    throw new InvalidOperationException("Title particle requires an unambiguous imported Sprite: " + spritePath);
                Sprite sprite = sprites[0];
                sheet.SetSprite(0, sprite);

                ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
                if (renderer == null || renderer.sharedMaterials.Length != 1 || renderer.sharedMaterial == null ||
                    renderer.sharedMaterial.shader.name != "Manosaba/SwitchTitle/Additive")
                    throw new InvalidOperationException("Title particle requires its single additive material: " + hierarchyPath);
                Material source = renderer.sharedMaterial;
                // SpriteRenderer supplies _MainTex itself; ParticleSystemRenderer needs a material
                // binding. Never mutate the shared additive material used by the title's light layers.
                string name = AssetName(source.name + "__Particle_" + sprite.name);
                string materialPath = Root + "/Materials/" + name + ".mat";
                Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                if (material == null)
                {
                    material = new Material(source) { name = name };
                    AssetDatabase.CreateAsset(material, materialPath);
                }
                else
                {
                    material.shader = source.shader;
                    material.CopyPropertiesFromMaterial(source);
                }
                material.SetTexture("_MainTex", sprite.texture);
                EditorUtility.SetDirty(material);
                renderer.sharedMaterial = material;
                EditorUtility.SetDirty(renderer);
                EditorUtility.SetDirty(particle);
                particleTextures.Add(hierarchyPath, new ParticleTextureBinding
                {
                    sprite = sprite, sourceMaterial = source, materialPath = materialPath
                });
            }
            return particleTextures.Count;
        }

        public void VerifyParticleTextures(GameObject saved)
        {
            ParticleSystem[] particles = saved.GetComponentsInChildren<ParticleSystem>(true);
            if (particles.Length != particleTextures.Count)
                throw new InvalidOperationException("Saved title particle count changed");
            foreach (ParticleSystem particle in particles)
            {
                string path = AnimationUtility.CalculateTransformPath(particle.transform, saved.transform);
                if (!particleTextures.TryGetValue(path, out ParticleTextureBinding expected))
                    throw new InvalidOperationException("Unexpected saved title particle: " + path);
                var sheet = particle.textureSheetAnimation;
                var renderer = particle.GetComponent<ParticleSystemRenderer>();
                Material material = AssetDatabase.LoadAssetAtPath<Material>(expected.materialPath);
                if (!sheet.enabled || sheet.mode != ParticleSystemAnimationMode.Sprites || sheet.spriteCount != 1 ||
                    sheet.GetSprite(0) != expected.sprite || renderer == null || renderer.sharedMaterials.Length != 1 ||
                    material == null || renderer.sharedMaterial != material || material == expected.sourceMaterial ||
                    material.GetTexture("_MainTex") != expected.sprite.texture)
                    throw new InvalidOperationException("Saved title particle Sprite/material/texture mismatch: " + path);
                if (saved.GetComponentsInChildren<SpriteRenderer>(true).Any(r => r.sharedMaterials.Contains(material)))
                    throw new InvalidOperationException("Particle texture material leaked into title light layers: " + path);
            }
        }

        public void CompleteController(Dictionary<string, Object> objects)
        {
            if (controller == null) return;
            if (!(controllerRecipe["states"] is JArray states) || states.Count < 1 || states.Count > 8)
                throw new InvalidOperationException("Title controller requires 1..8 simple states");
            if (controller.layers.Length != 1 || controller.parameters.Length != 0)
                throw new InvalidOperationException("Title controller must have one layer and no parameters");
            string defaultName = controllerRecipe.Value<string>("default_state");
            if (controller.name == "TitleED3" && defaultName != "ED3")
                throw new InvalidOperationException("TitleED3 must select the current ED3 state");
            AnimatorStateMachine machine = controller.layers[0].stateMachine;
            if (machine.stateMachines.Length != 0 || machine.anyStateTransitions.Length != 0 ||
                machine.entryTransitions.Length != 0 || machine.states.Any(s => s.state.transitions.Length != 0))
                throw new InvalidOperationException("Title controller supports only transition-free, non-nested states");
            var requested = states.Select(s => s.Value<string>("name")).ToList();
            if (requested.Any(string.IsNullOrEmpty) || requested.Distinct(StringComparer.Ordinal).Count() != requested.Count ||
                !requested.Contains(defaultName)) throw new InvalidOperationException("Invalid title controller state names/default");
            foreach (ChildAnimatorState existing in machine.states)
                if (!requested.Contains(existing.state.name)) machine.RemoveState(existing.state);
            AnimatorState defaultState = null;
            foreach (JObject recipe in states.OfType<JObject>())
            {
                string name = recipe.Value<string>("name"), clipId = recipe.Value<string>("clip_pid");
                if (!objects.TryGetValue(clipId, out Object clipObject) || !(clipObject is AnimationClip clip) ||
                    !EditorUtility.IsPersistent(clip) || AnimationUtility.GetCurveBindings(clip).Length == 0)
                    throw new InvalidOperationException("Title controller clip is missing/empty: " + clipId);
                float speed = recipe["speed"]?.Value<float>() ?? 1f;
                if (float.IsNaN(speed) || float.IsInfinity(speed) || speed <= 0)
                    throw new InvalidOperationException("Invalid title state speed");
                AnimatorState state = machine.states.Select(s => s.state).FirstOrDefault(s => s.name == name);
                if (state == null) state = machine.AddState(name);
                state.motion = clip;
                state.speed = speed;
                state.writeDefaultValues = true;
                var settings = AnimationUtility.GetAnimationClipSettings(clip);
                settings.loopTime = recipe["loop"]?.Value<bool>() ?? true;
                AnimationUtility.SetAnimationClipSettings(clip, settings);
                EditorUtility.SetDirty(clip);
                EditorUtility.SetDirty(state);
                if (name == defaultName) defaultState = state;
            }
            machine.defaultState = defaultState;
            EditorUtility.SetDirty(machine);
            EditorUtility.SetDirty(controller);
            string animatorId = controllerRecipe.Value<string>("animator_pid");
            if (!objects.TryGetValue(animatorId, out Object animatorObject) || !(animatorObject is Animator animator))
                throw new InvalidOperationException("Title controller Animator is missing: " + animatorId);
            animator.runtimeAnimatorController = controller;
            EditorUtility.SetDirty(animator);
        }

        public void RebindSavedClips(string prefabPath, Dictionary<string, string> clipNames)
        {
            if (controller == null) return;
            // SaveAsPrefabAsset can remap the local IDs initially assigned by AddObjectToAsset.
            // An external .controller must reference the imported clips, not the temporary objects
            // used while building the prefab. Resolve by the recipe's unambiguous source clip name.
            AnimationClip[] imported = AssetDatabase.LoadAllAssetsAtPath(prefabPath).OfType<AnimationClip>().ToArray();
            AnimatorStateMachine machine = controller.layers[0].stateMachine;
            foreach (JObject stateRecipe in ((JArray)controllerRecipe["states"]).OfType<JObject>())
            {
                string clipId = stateRecipe.Value<string>("clip_pid");
                if (!clipNames.TryGetValue(clipId, out string name))
                    throw new InvalidOperationException("No source clip name for title clip " + clipId);
                AnimationClip[] matches = imported.Where(c => c.name == name).ToArray();
                if (matches.Length != 1)
                    throw new InvalidOperationException("Expected exactly one imported title clip named " + name + ", found " + matches.Length);
                var state = machine.states.Select(s => s.state).Single(s => s.name == stateRecipe.Value<string>("name"));
                state.motion = matches[0];
                EditorUtility.SetDirty(state);
            }
            EditorUtility.SetDirty(controller);
            AssetDatabase.SaveAssets();
            string controllerPath = AssetDatabase.GetAssetPath(controller);
            AssetDatabase.ImportAsset(controllerPath, ImportAssetOptions.ForceUpdate);
            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        }

        public void VerifyController(GameObject saved)
        {
            if (controller == null) return;
            Animator[] animators = saved.GetComponentsInChildren<Animator>(true);
            Animator written = animators.SingleOrDefault(a => a.runtimeAnimatorController == controller);
            if (written == null) throw new InvalidOperationException("Saved title prefab has no matching AnimatorController");
            var loaded = AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GetAssetPath(controller));
            if (loaded == null || loaded.layers.Length != 1)
                throw new InvalidOperationException("Saved title controller missing its single layer");
            AnimatorStateMachine machine = loaded.layers[0].stateMachine;
            if (machine.defaultState == null || machine.defaultState.name != controllerRecipe.Value<string>("default_state"))
                throw new InvalidOperationException("Saved title controller default state mismatch");
            foreach (JObject stateRecipe in ((JArray)controllerRecipe["states"]).OfType<JObject>())
            {
                var state = machine.states.Select(s => s.state).SingleOrDefault(s => s.name == stateRecipe.Value<string>("name"));
                if (state == null || !(state.motion is AnimationClip clip) || !EditorUtility.IsPersistent(clip) ||
                    AnimationUtility.GetCurveBindings(clip).Length == 0 ||
                    AnimationUtility.GetAnimationClipSettings(clip).loopTime != (stateRecipe["loop"]?.Value<bool>() ?? true))
                    throw new InvalidOperationException("Saved title controller clip/loop mismatch: " + stateRecipe["name"]);
            }
        }

        private static Material BuildMaterial(JObject recipe)
        {
            string sourceShader = recipe.Value<string>("shader");
            string shaderName;
            switch (sourceShader)
            {
                case "Mobile/Particles/Additive": shaderName = "Manosaba/SwitchTitle/Additive"; break;
                case "Universal Render Pipeline/2D/Sprite-Unlit-Default": shaderName = "Sprites/Default"; break;
                case "Sprites/Mask": shaderName = "Sprites/Mask"; break;
                case "Shader Graphs/Fog":
                case "Shader Graphs/Fog_Hiro": shaderName = "Manosaba/SwitchTitle/Fog"; break;
                case "Shader Graphs/Noise": shaderName = "Manosaba/SwitchTitle/Noise"; break;
                default: throw new InvalidOperationException("Unsupported title shader: " + sourceShader);
            }
            Shader shader = Shader.Find(shaderName);
            if (shader == null) throw new InvalidOperationException("Title compatibility shader missing: " + shaderName);
            if (ShaderUtil.ShaderHasError(shader))
                throw new InvalidOperationException("Title compatibility shader has compilation errors: " + shaderName);
            string name = AssetName(recipe.Value<string>("name"));
            string path = Root + "/Materials/" + name + ".mat";
            EnsureFolder(Root + "/Materials");
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material == null)
            {
                material = new Material(shader) { name = name };
                AssetDatabase.CreateAsset(material, path);
            }
            else if (material.shader != shader) material.shader = shader;
            JObject properties = recipe["properties"] as JObject ?? throw new InvalidOperationException("Title material lacks source properties");
            foreach (var pair in Pairs(properties["m_Floats"]).Concat(Pairs(properties["m_Ints"])))
            {
                string property = pair.Key == "_Time" ? "_SourceTime" : pair.Key;
                if (material.HasProperty(property)) material.SetFloat(property, pair.Value.Value<float>());
            }
            foreach (var pair in Pairs(properties["m_Colors"]))
            {
                if (!material.HasProperty(pair.Key)) continue;
                var v = new Vector4(Number(pair.Value, "r"), Number(pair.Value, "g"), Number(pair.Value, "b"), Number(pair.Value, "a"));
                if (pair.Key == "_Scale" || pair.Key == "_Flip") material.SetVector(pair.Key, v);
                else material.SetColor(pair.Key, new Color(v.x, v.y, v.z, v.w));
            }
            JObject textures = recipe["textures"] as JObject ?? new JObject();
            foreach (var pair in Pairs(properties["m_TexEnvs"]))
            {
                if (!material.HasProperty(pair.Key)) continue;
                string texturePath = textures.Value<string>(pair.Key);
                if (!string.IsNullOrEmpty(texturePath))
                {
                    if (!texturePath.StartsWith(Root + "/Textures/", StringComparison.Ordinal) || texturePath.Contains("..") || texturePath.Contains("\\"))
                        throw new InvalidOperationException("Title material texture is outside its recipe folder");
                    Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
                    if (texture == null) throw new InvalidOperationException("Title material texture missing: " + texturePath);
                    material.SetTexture(pair.Key, texture);
                }
                else material.SetTexture(pair.Key, null);
                material.SetTextureScale(pair.Key, new Vector2(Number(pair.Value["m_Scale"], "x", 1), Number(pair.Value["m_Scale"], "y", 1)));
                material.SetTextureOffset(pair.Key, new Vector2(Number(pair.Value["m_Offset"], "x"), Number(pair.Value["m_Offset"], "y")));
            }
            EditorUtility.SetDirty(material);
            return material;
        }

        private static IEnumerable<KeyValuePair<string, JToken>> Pairs(JToken token)
        {
            if (token is JObject obj)
                foreach (JProperty property in obj.Properties()) yield return new KeyValuePair<string, JToken>(property.Name, property.Value);
            else if (token is JArray array)
                foreach (JToken item in array)
                {
                    if (!(item is JArray pair) || pair.Count != 2 || pair[0].Type != JTokenType.String)
                        throw new InvalidOperationException("Malformed title material property pair");
                    yield return new KeyValuePair<string, JToken>(pair[0].Value<string>(), pair[1]);
                }
        }

        private static float Number(JToken token, string key, float fallback = 0f) => token?[key]?.Value<float>() ?? fallback;

        private static string AssetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name == "." || name == ".." ||
                name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || name.Contains("/") || name.Contains("\\"))
                throw new InvalidOperationException("Invalid title asset name");
            return name;
        }

        private static void EnsureFolder(string folder)
        {
            if (AssetDatabase.IsValidFolder(folder)) return;
            string parent = folder.Substring(0, folder.LastIndexOf('/'));
            EnsureFolder(parent);
            AssetDatabase.CreateFolder(parent, folder.Substring(parent.Length + 1));
        }
    }
}
