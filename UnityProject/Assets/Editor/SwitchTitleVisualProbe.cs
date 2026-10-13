using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Naninovel;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Manosaba.EditorTools
{
    /// <summary>
    /// Renders imported title assets in an isolated preview scene. This does not
    /// run Boot, change saves, or claim to validate Naninovel's live title flow.
    /// </summary>
    public static class SwitchTitleVisualProbe
    {
        private const string Root = "Assets/GameContent/#WitchTrials/SwitchTitle";
        private const string ConfigRoot = "Assets/NaninovelData/Resources/Naninovel/Configuration/";
        private static readonly float[] SampleTimes = { 0f, 1f, 3f, 6f };

        [MenuItem("Manosaba/Probe Switch Title Visuals")]
        public static void Run()
        {
            var output = Path.GetFullPath(Path.Combine(Application.dataPath,
                "../../tools/unity-rebuild/out/unity/switch-title-visual",
                DateTime.UtcNow.ToString("yyyyMMddTHHmmssfffZ", CultureInfo.InvariantCulture)));
            JObject report;
            try { report = Execute(output); }
            catch (Exception ex)
            {
                report = new JObject { ["status"] = "FAIL", ["error"] = ex.ToString() };
                Debug.LogException(ex);
            }
            report["generatedUtc"] = DateTime.UtcNow.ToString("o");
            report["outputDirectory"] = output;
            Directory.CreateDirectory(output);
            File.WriteAllText(Path.Combine(output, "report.json"), report.ToString());
            Debug.Log("[SwitchTitleVisualProbe] " + report["status"] + ": " + output);
            if (Application.isBatchMode)
                EditorApplication.Exit((string)report["status"] == "PASS" ? 0 : 1);
        }

        public static JObject Execute(string outputDirectory)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
                throw new InvalidOperationException("End Play Mode before running the isolated title preview.");
            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
                throw new InvalidOperationException("A graphics device is required; do not use -nographics.");

            Directory.CreateDirectory(outputDirectory);
            var cameraConfig = Configuration("CameraConfiguration");
            var reference = cameraConfig.FindProperty("ReferenceResolution").vector2IntValue;
            var ppu = cameraConfig.FindProperty("ReferencePPU").floatValue;
            if (reference.x <= 0 || reference.y <= 0 || ppu <= 0)
                throw new InvalidOperationException("Invalid Naninovel camera reference resolution or PPU.");
            var backgroundScale = ReadBackgroundScale();
            var samples = new JArray();
            var report = new JObject
            {
                ["status"] = "PASS",
                ["assetRoot"] = Root,
                ["graphicsDevice"] = SystemInfo.graphicsDeviceType.ToString(),
                ["referenceResolution"] = new JArray(reference.x, reference.y),
                ["referencePPU"] = ppu,
                ["backgroundDefaultScale"] = backgroundScale,
                ["renderAttempted"] = true,
                ["liveRuntimeValidated"] = false,
                ["scope"] = "Prefab/default Animator/particles and offscreen background blur. " +
                    "Entrance scale is sampled from the known title timing; AwaitSpawn is not executed. " +
                    "No menu or black entrance overlay. Shader _UnscaledTime is sampled; engine _Time is not controlled.",
                ["samples"] = samples
            };

            var scene = EditorSceneManager.NewPreviewScene();
            var previousTime = Shader.GetGlobalFloat("_UnscaledTime");
            try
            {
                var cameraObject = new GameObject("SwitchTitleProbeCamera");
                SceneManager.MoveGameObjectToScene(cameraObject, scene);
                var camera = cameraObject.AddComponent<Camera>();
                camera.scene = scene;
                camera.enabled = false;
                camera.orthographic = true;
                camera.orthographicSize = reference.y / ppu / 2f;
                camera.aspect = (float)reference.x / reference.y;
                camera.transform.position = cameraConfig.FindProperty("InitialPosition").vector3Value;
                camera.nearClipPlane = .01f;
                camera.farClipPlane = 1000f;
                camera.backgroundColor = Color.black;
                camera.allowHDR = false;
                camera.allowMSAA = false;

                for (var index = 1; index <= 3; index++)
                foreach (var seconds in SampleTimes)
                {
                    var sample = new JObject { ["variant"] = index, ["seconds"] = seconds };
                    samples.Add(sample);
                    try { RenderSample(scene, camera, index, seconds, backgroundScale, outputDirectory, sample); }
                    catch (Exception ex)
                    {
                        sample["status"] = "FAIL";
                        sample["error"] = ex.ToString();
                        report["status"] = "FAIL";
                    }
                }
            }
            finally
            {
                Shader.SetGlobalFloat("_UnscaledTime", previousTime);
                EditorSceneManager.ClosePreviewScene(scene);
            }
            return report;
        }

        private static void RenderSample(Scene scene, Camera camera, int index, float seconds,
            float backgroundScale, string output, JObject sample)
        {
            var prefabPath = Root + "/Prefabs/TitleScreenEffect_" + index + ".prefab";
            var texturePath = Root + "/Textures/Still_00" + index + "_001.png";
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (!prefab) throw new InvalidOperationException("Missing prefab: " + prefabPath);
            var sprite = AssetDatabase.LoadAllAssetsAtPath(texturePath).OfType<Sprite>().SingleOrDefault();
            if (!sprite) throw new InvalidOperationException("Expected one imported background Sprite: " + texturePath);

            GameObject instance = null;
            GameObject background = null;
            RenderTexture target = null;
            Texture2D pixels = null;
            var previousActive = RenderTexture.active;
            try
            {
                instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
                var transforms = instance.GetComponentsInChildren<Transform>(true);
                foreach (var transform in transforms) transform.gameObject.layer = 30;
                var missing = transforms.Sum(t => GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject));
                if (missing != 0) throw new InvalidOperationException("Prefab contains " + missing + " missing scripts.");
                var component = instance.GetComponents<MonoBehaviour>()
                    .SingleOrDefault(c => c && c.GetType().FullName == "WitchTrials.Views.TitleScreenEffect");
                if (!component) throw new InvalidOperationException("TitleScreenEffect component is missing on the prefab root.");
                var effectRoot = new SerializedObject(component).FindProperty("_rootTransform")?.objectReferenceValue as Transform;
                if (!effectRoot) throw new InvalidOperationException("TitleScreenEffect._rootTransform is unbound.");
                var defaultScale = effectRoot.localScale;
                var animations = SampleAnimations(instance, seconds);
                var ratio = Mathf.Clamp01(seconds / 2.7f);
                var entrance = Mathf.Lerp(1.05f, 1f, 1f - (1f - ratio) * (1f - ratio));
                effectRoot.localScale = defaultScale * entrance;
                var particles = SampleParticles(instance, seconds);
                var renderers = ValidateRenderers(instance);

                background = new GameObject("TitleBackground");
                SceneManager.MoveGameObjectToScene(background, scene);
                background.layer = 31;
                var renderer = background.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                renderer.sortingOrder = short.MinValue;
                // BackgroundMatcherExtended derives its reference height from the texture width at 16:9.
                var cameraWidth = camera.orthographicSize * 2f * camera.aspect;
                var baseScale = cameraWidth / (sprite.texture.width / sprite.pixelsPerUnit) * backgroundScale;
                background.transform.localScale = Vector3.one * baseScale * entrance;
                background.transform.position = -Vector3.Scale(sprite.bounds.center, background.transform.localScale);

                target = RenderTexture.GetTemporary(1280, 720, 24, RenderTextureFormat.ARGB32);
                camera.targetTexture = target;
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.cullingMask = 1 << 31;
                camera.Render();
                var blur = 1f - Mathf.SmoothStep(0f, 1f, ratio);
                if (blur > 0f)
                {
                    var blurShader = Shader.Find("Hidden/Naninovel/BlurFilter");
                    if (!blurShader || !blurShader.isSupported || ShaderUtil.ShaderHasError(blurShader))
                        throw new InvalidOperationException("Naninovel blur shader is missing or unsupported.");
                    using (var filter = new BlurFilter(2, true)) filter.BlurTexture(target, blur);
                }
                Shader.SetGlobalFloat("_UnscaledTime", seconds);
                camera.clearFlags = CameraClearFlags.Nothing;
                camera.cullingMask = 1 << 30;
                camera.Render();

                RenderTexture.active = target;
                pixels = new Texture2D(target.width, target.height, TextureFormat.RGB24, false);
                pixels.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
                pixels.Apply();
                var colors = pixels.GetPixels32();
                var nonBlack = colors.Count(c => c.r > 3 || c.g > 3 || c.b > 3);
                if (nonBlack < colors.Length / 1000)
                    throw new InvalidOperationException("Rendered output is effectively black; preview did not validate visible content.");
                var png = pixels.EncodeToPNG();
                var file = "title_" + index + "_t" + seconds.ToString("0", CultureInfo.InvariantCulture) + ".png";
                File.WriteAllBytes(Path.Combine(output, file), png);
                using (var sha = SHA256.Create())
                    sample["pngSha256"] = BitConverter.ToString(sha.ComputeHash(png)).Replace("-", "").ToLowerInvariant();
                sample["status"] = "PASS";
                sample["image"] = file;
                sample["prefab"] = prefabPath;
                sample["background"] = texturePath;
                sample["backgroundSprite"] = new JObject
                {
                    ["rect"] = new JArray(sprite.rect.x, sprite.rect.y, sprite.rect.width, sprite.rect.height),
                    ["pivot"] = new JArray(sprite.pivot.x, sprite.pivot.y),
                    ["ppu"] = sprite.pixelsPerUnit,
                    ["worldScale"] = baseScale * entrance
                };
                sample["rootDefaultScale"] = Vector(defaultScale);
                sample["entranceMultiplier"] = entrance;
                sample["blurIntensity"] = blur;
                sample["animations"] = animations;
                sample["particles"] = particles;
                sample["renderers"] = renderers;
                sample["nonBlackPixels"] = nonBlack;
                sample["magentaPixels"] = colors.Count(c => c.r > 250 && c.g < 5 && c.b > 250);
            }
            finally
            {
                camera.targetTexture = null;
                RenderTexture.active = previousActive;
                if (target) RenderTexture.ReleaseTemporary(target);
                if (pixels) UnityEngine.Object.DestroyImmediate(pixels);
                if (background) UnityEngine.Object.DestroyImmediate(background);
                if (instance) UnityEngine.Object.DestroyImmediate(instance);
            }
        }

        private static JArray SampleAnimations(GameObject instance, float seconds)
        {
            var result = new JArray();
            var animators = instance.GetComponentsInChildren<Animator>(true);
            if (animators.Length == 0) throw new InvalidOperationException("Title effect has no Animator.");
            foreach (var animator in animators)
            {
                animator.enabled = false;
                var controller = animator.runtimeAnimatorController as AnimatorController;
                if (!controller || controller.layers.Length == 0)
                    throw new InvalidOperationException("Expected an imported AnimatorController for " + animator.name);
                var state = controller.layers[0].stateMachine.defaultState;
                var clip = state ? state.motion as AnimationClip : null;
                if (!clip) throw new InvalidOperationException("Default Animator state has no AnimationClip: " + animator.name);
                var bindings = AnimationUtility.GetCurveBindings(clip);
                if (bindings.Length == 0) throw new InvalidOperationException("Default title animation has no curves: " + clip.name);
                var at = clip.isLooping && clip.length > 0f ? seconds % clip.length : Mathf.Min(seconds, clip.length);
                clip.SampleAnimation(animator.gameObject, at);
                result.Add(new JObject
                {
                    ["animator"] = animator.name, ["controller"] = controller.name,
                    ["defaultState"] = state.name, ["clip"] = clip.name,
                    ["length"] = clip.length, ["loop"] = clip.isLooping,
                    ["sampleTime"] = at, ["curveCount"] = bindings.Length
                });
            }
            return result;
        }

        private static JArray SampleParticles(GameObject instance, float seconds)
        {
            var systems = instance.GetComponentsInChildren<ParticleSystem>(true);
            for (var i = 0; i < systems.Length; i++)
            {
                var sheet = systems[i].textureSheetAnimation;
                var renderer = systems[i].GetComponent<ParticleSystemRenderer>();
                if (!sheet.enabled || sheet.mode != ParticleSystemAnimationMode.Sprites || sheet.spriteCount != 1 ||
                    sheet.GetSprite(0) == null || renderer == null || renderer.sharedMaterials.Length != 1 ||
                    renderer.sharedMaterial == null || renderer.sharedMaterial.GetTexture("_MainTex") != sheet.GetSprite(0).texture)
                    throw new InvalidOperationException("Particle Sprite texture is not bound to its material: " + systems[i].name);
                systems[i].useAutoRandomSeed = false;
                systems[i].randomSeed = (uint)(1234 + i);
            }
            foreach (var system in systems)
            {
                var ancestor = system.transform.parent ? system.transform.parent.GetComponentInParent<ParticleSystem>() : null;
                if (ancestor && systems.Contains(ancestor)) continue;
                system.Simulate(seconds, true, true, true);
            }
            return new JArray(systems.Select(system => new JObject
            {
                ["name"] = system.name, ["particleCount"] = system.particleCount,
                ["seed"] = system.randomSeed, ["loop"] = system.main.loop,
                ["sprite"] = system.textureSheetAnimation.GetSprite(0).name,
                ["texture"] = AssetDatabase.GetAssetPath(system.textureSheetAnimation.GetSprite(0).texture),
                ["material"] = AssetDatabase.GetAssetPath(system.GetComponent<ParticleSystemRenderer>().sharedMaterial),
                ["spriteTextureBindingValidated"] = true
            }));
        }

        private static JArray ValidateRenderers(GameObject instance)
        {
            var result = new JArray();
            foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer is SpriteRenderer sprite && !sprite.sprite)
                    throw new InvalidOperationException("Missing Sprite on " + renderer.name);
                var materials = new JArray();
                foreach (var material in renderer.sharedMaterials)
                {
                    if (!material || !material.shader || !material.shader.isSupported || ShaderUtil.ShaderHasError(material.shader))
                        throw new InvalidOperationException("Missing, unsupported or erroneous material/shader on " + renderer.name);
                    materials.Add(new JObject { ["material"] = material.name, ["shader"] = material.shader.name });
                }
                if (materials.Count == 0) throw new InvalidOperationException("Renderer has no material: " + renderer.name);
                result.Add(new JObject { ["name"] = renderer.name, ["type"] = renderer.GetType().Name, ["materials"] = materials });
            }
            return result;
        }

        private static SerializedObject Configuration(string name)
        {
            var asset = AssetDatabase.LoadMainAssetAtPath(ConfigRoot + name + ".asset");
            if (!asset) throw new InvalidOperationException("Missing Naninovel configuration: " + name);
            return new SerializedObject(asset);
        }

        private static float ReadBackgroundScale()
        {
            var entries = Configuration("CustomVariablesConfiguration").FindProperty("PredefinedVariables");
            for (var i = 0; i < entries.arraySize; i++)
            {
                var entry = entries.GetArrayElementAtIndex(i);
                if (entry.FindPropertyRelative("name").stringValue != "g_backgroundDefaultScale") continue;
                var text = entry.FindPropertyRelative("value").stringValue;
                if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var scale) && scale > 0f)
                    return scale;
                throw new InvalidOperationException("Invalid g_backgroundDefaultScale: " + text);
            }
            throw new InvalidOperationException("g_backgroundDefaultScale is not configured.");
        }

        private static JArray Vector(Vector3 value) => new JArray(value.x, value.y, value.z);
    }
}
