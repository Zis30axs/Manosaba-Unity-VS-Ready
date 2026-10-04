using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Manosaba.EditorTools
{
    /// <summary>
    /// Read-only diagnostic for generated reenactment prefabs. It loads S01..S05 into
    /// an empty preview scene, evaluates every PlayableDirector at time zero and at
    /// the director's end, then writes a compact evidence JSON for the curve repair.
    /// </summary>
    public static class ReenactmentProbe
    {
        private const string Root = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/Spawn/Reenactment/Ch02_E06";
        private const string Output = "../../tools/unity-rebuild/out/unity/reenactment_probe.json";

        public static void Run()
        {
            int exitCode = 0;
            try
            {
                JObject report = Execute();
                WriteReport(report);
                Debug.Log("[ReenactmentProbe] wrote " + OutputPath());
            }
            catch (Exception ex)
            {
                exitCode = 1;
                var failure = new JObject
                {
                    ["status"] = "FAIL",
                    ["generatedUtc"] = DateTime.UtcNow.ToString("o"),
                    ["error"] = ex.ToString()
                };
                try { WriteReport(failure); } catch { /* preserve the original diagnostic error */ }
                Debug.LogException(ex);
            }

            if (Application.isBatchMode)
                EditorApplication.Exit(exitCode);
        }

        private static JObject Execute()
        {
            var report = new JObject
            {
                ["status"] = "PASS",
                ["generatedUtc"] = DateTime.UtcNow.ToString("o"),
                ["targetScenario"] = "Act02_Chapter06_Trial10",
                ["root"] = Root,
                ["previewScene"] = "empty preview scene; generated prefabs instantiated with PrefabUtility",
                ["renderAttempted"] = false,
                ["samples"] = new JArray()
            };

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            if (!scene.IsValid()) throw new InvalidOperationException("Could not create the empty preview scene.");

            var samples = (JArray)report["samples"];
            for (int index = 1; index <= 5; index++)
            {
                string path = Root + "/Ch02_E06_S0" + index + "/Reenact_Ch02_E06_S0" + index + ".prefab";
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                var sample = new JObject { ["index"] = index, ["path"] = path };
                samples.Add(sample);
                if (!prefab)
                {
                    sample["status"] = "MISSING_PREFAB";
                    continue;
                }

                GameObject instance = null;
                try
                {
                    instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    if (!instance) throw new InvalidOperationException("PrefabUtility.InstantiatePrefab returned null.");
                    instance.name = "Probe_Ch02_E06_S0" + index;
                    sample["status"] = "LOADED";
                    sample["assetObjectCount"] = AssetDatabase.LoadAllAssetsAtPath(path).Length;
                    sample["hierarchy"] = HierarchyReport(instance.transform);
                    sample["directors"] = DirectorReport(instance, path);
                    EvaluateAndCapture(instance, sample);
                }
                finally
                {
                    if (instance) UnityEngine.Object.DestroyImmediate(instance);
                }
            }

            return report;
        }

        private static JArray DirectorReport(GameObject instance, string prefabPath)
        {
            var result = new JArray();
            var directors = instance.GetComponentsInChildren<PlayableDirector>(true);
            var embeddedClips = AssetDatabase.LoadAllAssetsAtPath(prefabPath).OfType<AnimationClip>().ToArray();
            foreach (PlayableDirector director in directors)
            {
                var item = new JObject
                {
                    ["name"] = director.name,
                    ["hierarchyPath"] = RelativePath(instance.transform, director.transform),
                    ["playableAsset"] = director.playableAsset ? director.playableAsset.name : null,
                    ["playableAssetType"] = director.playableAsset ? director.playableAsset.GetType().FullName : null,
                    ["duration"] = director.playableAsset ? director.playableAsset.duration : 0d,
                    ["initialGraphValid"] = director.playableGraph.IsValid(),
                    ["sceneBindingCount"] = 0,
                    ["tracks"] = new JArray(),
                    ["embeddedAnimationClipCount"] = embeddedClips.Length
                };

                var bindings = new JArray();
                var tracks = (JArray)item["tracks"];
                TimelineAsset timeline = director.playableAsset as TimelineAsset;
                if (timeline)
                {
                    foreach (TrackAsset track in timeline.GetOutputTracks())
                    {
                        UnityEngine.Object bound = director.GetGenericBinding(track);
                        bindings.Add(new JObject
                        {
                            ["track"] = track.name,
                            ["trackType"] = track.GetType().FullName,
                            ["binding"] = bound ? bound.name : null,
                            ["bindingType"] = bound ? bound.GetType().FullName : null,
                            ["bindingPath"] = bound is Component c ? RelativePath(instance.transform, c.transform) : null
                        });

                        AnimationClip infiniteClip = track is AnimationTrack animationTrack
                            ? animationTrack.infiniteClip : null;
                        var trackItem = new JObject
                        {
                            ["name"] = track.name,
                            ["type"] = track.GetType().FullName,
                            ["clipCount"] = track.GetClips().Count(),
                            ["clips"] = new JArray()
                        };
                        if (infiniteClip)
                        {
                            var infiniteEvidence = new JObject { ["name"] = infiniteClip.name };
                            AddAnimationEvidence(infiniteEvidence, infiniteClip);
                            trackItem["infiniteClip"] = infiniteEvidence;
                        }
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            var clipItem = new JObject
                            {
                                ["start"] = clip.start,
                                ["duration"] = clip.duration,
                                ["assetType"] = clip.asset ? clip.asset.GetType().FullName : null,
                                ["asset"] = clip.asset ? clip.asset.name : null
                            };
                            AnimationClip animation = clip.asset is AnimationPlayableAsset playable ? playable.clip : null;
                            if (animation != null) AddAnimationEvidence(clipItem, animation);
                            ((JArray)trackItem["clips"]).Add(clipItem);
                        }
                        tracks.Add(trackItem);
                    }
                }

                item["sceneBindingCount"] = bindings.Count;
                item["sceneBindings"] = bindings;
                foreach (AnimationClip clip in embeddedClips)
                {
                    var clipItem = new JObject { ["name"] = clip.name };
                    AddAnimationEvidence(clipItem, clip);
                    ((JArray)item["tracks"]).Add(new JObject
                    {
                        ["embeddedAnimationClip"] = clipItem
                    });
                }
                result.Add(item);
            }
            return result;
        }

        private static void AddAnimationEvidence(JObject item, AnimationClip clip)
        {
            EditorCurveBinding[] curves = AnimationUtility.GetCurveBindings(clip);
            EditorCurveBinding[] objectCurves = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            item["length"] = clip.length;
            item["frameRate"] = clip.frameRate;
            item["curveBindingCount"] = curves.Length;
            item["objectReferenceBindingCount"] = objectCurves.Length;
            item["curvePaths"] = StringsArray(curves.Select(c => c.path + ":" + c.type.FullName + ":" + c.propertyName));
            item["objectReferencePaths"] = StringsArray(objectCurves.Select(c => c.path + ":" + c.type.FullName + ":" + c.propertyName));
        }

        private static void EvaluateAndCapture(GameObject instance, JObject sample)
        {
            var directors = instance.GetComponentsInChildren<PlayableDirector>(true);
            foreach (PlayableDirector director in directors)
            {
                if (!director.playableAsset) continue;
                director.time = 0d;
                director.Evaluate();
            }
            JArray startSprites = Renderers(instance, "start");
            JArray startTransforms = Transforms(instance, "start");

            foreach (PlayableDirector director in directors)
            {
                if (!director.playableAsset) continue;
                double end = director.playableAsset.duration;
                if (double.IsNaN(end) || double.IsInfinity(end) || end <= 0d) end = 1d;
                director.time = end;
                director.Evaluate();
            }
            var perDirector = new JArray();
            foreach (PlayableDirector target in directors)
            {
                if (!target.playableAsset) continue;
                foreach (PlayableDirector director in directors)
                {
                    if (!director.playableAsset) continue;
                    director.time = 0d;
                    director.Evaluate();
                }
                JArray targetStartSprites = Renderers(instance, "start");
                JArray targetStartTransforms = Transforms(instance, "start");
                double targetEnd = target.playableAsset.duration;
                if (double.IsNaN(targetEnd) || double.IsInfinity(targetEnd) || targetEnd <= 0d) targetEnd = 1d;
                // CurtainOpen is the user-visible transition and the shipped timeline
                // is 1.1667 seconds long; sample its documented 0.75-second midpoint.
                double sampleTime = target.name.IndexOf("CurtainOpen", StringComparison.OrdinalIgnoreCase) >= 0
                    ? Math.Min(.75d, targetEnd) : targetEnd;
                target.time = sampleTime;
                target.Evaluate();
                JArray targetEndSprites = Renderers(instance, "end");
                JArray targetEndTransforms = Transforms(instance, "end");
                perDirector.Add(new JObject
                {
                    ["director"] = target.name,
                    ["duration"] = target.playableAsset.duration,
                    ["sampleTime"] = sampleTime,
                    ["startSprites"] = targetStartSprites,
                    ["endSprites"] = targetEndSprites,
                    ["spriteDifferences"] = RendererDifferences(targetStartSprites, targetEndSprites),
                    ["startTransforms"] = targetStartTransforms,
                    ["endTransforms"] = targetEndTransforms,
                    ["transformDifferences"] = TransformDifferences(targetStartTransforms, targetEndTransforms)
                });
            }

            sample["evaluation"] = new JObject
            {
                ["directorsWithPlayableAsset"] = directors.Count(d => d.playableAsset),
                ["startSpriteRenderers"] = startSprites,
                ["endSpriteRenderers"] = Renderers(instance, "end"),
                ["startTransforms"] = startTransforms,
                ["endTransforms"] = Transforms(instance, "end"),
                ["transformDifferences"] = TransformDifferences(startTransforms, Transforms(instance, "end")),
                ["perDirector"] = perDirector
            };
        }

        private static JArray Renderers(GameObject instance, string phase)
        {
            var result = new JArray();
            foreach (SpriteRenderer renderer in instance.GetComponentsInChildren<SpriteRenderer>(true))
            {
                Material material = renderer.sharedMaterial;
                result.Add(new JObject
                {
                    ["phase"] = phase,
                    ["path"] = RelativePath(instance.transform, renderer.transform),
                    ["enabled"] = renderer.enabled,
                    ["alpha"] = renderer.color.a,
                    ["sprite"] = renderer.sprite ? renderer.sprite.name : null,
                    ["material"] = material ? material.name : null,
                    ["shader"] = material && material.shader ? material.shader.name : null,
                    ["materialCount"] = renderer.sharedMaterials == null ? 0 : renderer.sharedMaterials.Length
                });
            }
            return result;
        }

        private static JArray Transforms(GameObject instance, string phase)
        {
            var result = new JArray();
            foreach (Transform transform in instance.GetComponentsInChildren<Transform>(true))
            {
                Vector3 position = transform.localPosition;
                Vector3 scale = transform.localScale;
                Vector3 euler = transform.localEulerAngles;
                result.Add(new JObject
                {
                    ["phase"] = phase,
                    ["path"] = RelativePath(instance.transform, transform),
                    ["localPosition"] = Vector3Json(position),
                    ["localScale"] = Vector3Json(scale),
                    ["localEulerAngles"] = Vector3Json(euler)
                });
            }
            return result;
        }

        private static JArray TransformDifferences(JArray start, JArray end)
        {
            var byPath = start.OfType<JObject>().ToDictionary(x => (string)x["path"]);
            var result = new JArray();
            foreach (JObject item in end.OfType<JObject>())
            {
                string path = (string)item["path"];
                if (!byPath.TryGetValue(path, out JObject before)) continue;
                Vector3 bp = ReadVector(before["localPosition"]);
                Vector3 ep = ReadVector(item["localPosition"]);
                Vector3 bs = ReadVector(before["localScale"]);
                Vector3 es = ReadVector(item["localScale"]);
                Vector3 br = ReadVector(before["localEulerAngles"]);
                Vector3 er = ReadVector(item["localEulerAngles"]);
                if ((bp - ep).sqrMagnitude < 1e-10f && (bs - es).sqrMagnitude < 1e-10f && (br - er).sqrMagnitude < 1e-8f) continue;
                result.Add(new JObject
                {
                    ["path"] = path,
                    ["positionDelta"] = Vector3Json(ep - bp),
                    ["scaleDelta"] = Vector3Json(es - bs),
                    ["eulerDelta"] = Vector3Json(er - br)
                });
            }
            return result;
        }

        private static JArray RendererDifferences(JArray start, JArray end)
        {
            var before = start.OfType<JObject>().ToDictionary(x => (string)x["path"]);
            var result = new JArray();
            foreach (JObject item in end.OfType<JObject>())
            {
                string path = (string)item["path"];
                if (!before.TryGetValue(path, out JObject prior)) continue;
                float oldAlpha = (float)prior["alpha"];
                float newAlpha = (float)item["alpha"];
                bool oldEnabled = (bool)prior["enabled"];
                bool newEnabled = (bool)item["enabled"];
                string oldSprite = (string)prior["sprite"];
                string newSprite = (string)item["sprite"];
                if (Mathf.Abs(oldAlpha - newAlpha) < 1e-5f && oldEnabled == newEnabled && oldSprite == newSprite) continue;
                result.Add(new JObject
                {
                    ["path"] = path,
                    ["alphaBefore"] = oldAlpha,
                    ["alphaAfter"] = newAlpha,
                    ["enabledBefore"] = oldEnabled,
                    ["enabledAfter"] = newEnabled,
                    ["spriteBefore"] = oldSprite,
                    ["spriteAfter"] = newSprite
                });
            }
            return result;
        }

        private static JObject HierarchyReport(Transform root)
        {
            return new JObject
            {
                ["transformCount"] = root.GetComponentsInChildren<Transform>(true).Length,
                ["componentCount"] = root.GetComponentsInChildren<Component>(true).Length,
                ["spriteRendererCount"] = root.GetComponentsInChildren<SpriteRenderer>(true).Length,
                ["playableDirectorCount"] = root.GetComponentsInChildren<PlayableDirector>(true).Length
            };
        }

        private static JObject Vector3Json(Vector3 value)
        {
            return new JObject { ["x"] = value.x, ["y"] = value.y, ["z"] = value.z };
        }

        private static JArray StringsArray(IEnumerable<string> values)
        {
            var result = new JArray();
            foreach (string value in values) result.Add(value);
            return result;
        }

        private static Vector3 ReadVector(JToken token)
        {
            return new Vector3((float)token["x"], (float)token["y"], (float)token["z"]);
        }

        private static string RelativePath(Transform root, Transform target)
        {
            if (target == root) return ".";
            var names = new List<string>();
            Transform current = target;
            while (current != null && current != root)
            {
                names.Add(current.name);
                current = current.parent;
            }
            names.Reverse();
            return current == root ? string.Join("/", names) : target.name;
        }

        private static string OutputPath()
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, Output));
        }

        private static void WriteReport(JObject report)
        {
            string path = OutputPath();
            string directory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directory)) throw new InvalidOperationException("Invalid probe output directory.");
            Directory.CreateDirectory(directory);
            File.WriteAllText(path, report.ToString(Newtonsoft.Json.Formatting.Indented), new UTF8Encoding(false));
        }
    }
}
