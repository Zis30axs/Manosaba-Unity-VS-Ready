using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Manosaba.ThreeDPrototype;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Manosaba.EditorTools
{
    public static class CellPrototypeBuilder
    {
        private const string Root = "Assets/ThreeDPrototype";
        private const string ScenePath = Root + "/Scenes/CellWakeUp.unity";
        private static readonly string Repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
        private static System.Random Random = new System.Random(490);
        private static readonly Dictionary<string, Material> Materials = new Dictionary<string, Material>();

        [MenuItem("Manosaba/3D Prototype/Generate prison scene")]
        public static void Generate()
        {
            if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            try { CreateScene(); }
            catch (Exception error) { Debug.LogException(error); if (Application.isBatchMode) EditorApplication.Exit(1); }
        }

        public static void CreateAndBuild()
        {
            int code = 1;
            try { CreateScene(); Build(); code = 0; }
            catch (Exception error) { Debug.LogException(error); }
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static void BuildOnly()
        {
            int code = 1;
            try { EditorSceneManager.OpenScene(ScenePath); Build(); code = 0; }
            catch (Exception error) { Debug.LogException(error); }
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static void RenderModelProbe()
        {
            try
            {
                EditorSceneManager.OpenScene(ScenePath);
                var controller = UnityEngine.Object.FindFirstObjectByType<CellPrototypeController>();
                controller.ema.position = new Vector3(0, 0, 0);
                controller.ema.rotation = Quaternion.Euler(0, 180, 0);
                var camera = controller.sceneCamera;
                camera.transform.position = new Vector3(0, 1.45f, -1.0f);
                camera.transform.LookAt(new Vector3(0, 1.43f, 0));
                camera.fieldOfView = 37;
                var renderers = controller.ema.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var renderer in renderers)
                    for (int i = 0; i < renderer.sharedMesh.blendShapeCount; i++) renderer.SetBlendShapeWeight(i, 0);
                RenderPreview(camera, Path.Combine(Repo, ".3d-model/preview/ema-rest.png"));
                foreach (var renderer in renderers)
                {
                    renderer.sharedMaterials = renderer.sharedMaterials.Select(m => new Material(m)).ToArray();
                    foreach (var material in renderer.sharedMaterials) material.SetFloat("_Cull", 0);
                }
                RenderPreview(camera, Path.Combine(Repo, ".3d-model/preview/ema-cull-off.png"));
                foreach (var renderer in renderers)
                    for (int i = 0; i < renderer.sharedMesh.blendShapeCount; i++)
                    {
                        string name = renderer.sharedMesh.GetBlendShapeName(i);
                        if (name == "びっくり") renderer.SetBlendShapeWeight(i, 18);
                        if (name == "困る") renderer.SetBlendShapeWeight(i, 16);
                    }
                RenderPreview(camera, Path.Combine(Repo, ".3d-model/preview/ema-expression.png"));
                if (Application.isBatchMode) EditorApplication.Exit(0);
            }
            catch (Exception error) { Debug.LogException(error); if (Application.isBatchMode) EditorApplication.Exit(1); }
        }

        [MenuItem("Manosaba/3D Prototype/Open prison scene")]
        public static void Open()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) EditorSceneManager.OpenScene(ScenePath);
        }

        private static void CreateScene()
        {
            Materials.Clear();
            Random = new System.Random(490);
            foreach (string folder in new[] { "Materials", "Geometry", "Media", "Scenes" })
                Directory.CreateDirectory(Path.Combine(Application.dataPath, "ThreeDPrototype", folder));
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            GameObject emaPrefab = PrototypeModelImporter.Import(Path.Combine(Repo, ".3d-model/generated/Ema"), Root + "/Models/Ema");
            GameObject tankPrefab = PrototypeModelImporter.Import(Path.Combine(Repo, ".3d-model/generated/BMPT72"), Root + "/Models/BMPT72");
            TextAsset storyAsset = CopyAsset<TextAsset>(".3d-model/generated/story.json", "Media/story.json");
            Font font = CopyAsset<Font>("res/Assets/#WitchTrials/Fonts/SourceHanSerifSC/SourceHanSerifSC.otf", "Media/SourceHanSerifSC.otf");
            Texture reference = CopyAsset<Texture2D>("res/Assets/#WitchTrials/Textures/Naninovel/Backgrounds/MainBackground/RGB/Background_001_001.png", "Media/ReferenceCell.png");
            CellPrototypeController.Story story = JsonUtility.FromJson<CellPrototypeController.Story>(storyAsset.text);
            var voices = new AudioClip[story.lines.Length];
            for (int i = 0; i < voices.Length; i++)
            {
                string voice = story.lines[i].voice;
                if (!string.IsNullOrEmpty(voice))
                    voices[i] = CopyAsset<AudioClip>("res/" + voice, "Media/Voice/" + Path.GetFileName(voice));
            }
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(.27f, .23f, .34f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(.07f, .056f, .11f);
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = .018f;

            Material stone = Mat("Stone", new Color(.29f, .26f, .34f), 0, .06f);
            Material mortar = Mat("Mortar", new Color(.10f, .088f, .13f));
            Material iron = Mat("WornIron", new Color(.16f, .17f, .20f), .68f, .32f);
            Material cloth = Mat("Mattress", new Color(.29f, .25f, .27f), 0, .05f);
            Material pillow = Mat("Pillow", new Color(.40f, .37f, .39f), 0, .05f);
            GameObject room = new GameObject("Prison · inferred depth 6 m");
            Cube("Foundation", new Vector3(0, -.18f, .45f), new Vector3(6.5f, .28f, 8.5f), mortar, room.transform);
            Cube("Dark ceiling", new Vector3(0, 3.76f, 0), new Vector3(6.6f, .16f, 6.6f), mortar, room.transform);
            Floor(room.transform, stone);
            Wall(room.transform, stone, mortar, 0);
            Wall(room.transform, stone, mortar, 1);
            Wall(room.transform, stone, mortar, 2);
            Door(room.transform, stone, iron);
            Bed(room.transform, iron, cloth, pillow);
            Chain(room.transform, iron, new Vector3(-2.65f, 2.13f, 2.48f));
            Chain(room.transform, iron, new Vector3(.20f, 2.13f, 2.48f));
            Bowl(room.transform, iron, new Vector3(1.05f, .08f, -.3f), .24f);
            Bucket(room.transform, iron);
            Debris(room.transform, stone);

            var screenMat = Mat("MonitorGlass", new Color(.018f, .025f, .025f), .2f, .8f);
            Transform monitor = new GameObject("Wall monitor").transform;
            monitor.SetParent(room.transform);
            monitor.position = new Vector3(-.30f, 2.78f, 2.43f);
            Cube("Frame", monitor.position, new Vector3(.91f, .66f, .12f), iron, monitor);
            GameObject screen = Cube("Screen", monitor.position + new Vector3(0, 0, -.073f), new Vector3(.77f, .51f, .018f), screenMat, monitor);
            for (int i = 0; i < 3; i++) Cylinder("Cable", new Vector3(-.15f + i * .025f, 2.15f, 2.47f), new Vector3(.006f, .28f, .006f), iron, room.transform);

            Lamp(room.transform, iron);
            Light main = Point("Doorway violet light", new Vector3(-1.55f, 2.0f, 3.55f), new Color(.66f, .44f, 1), 3.0f, 8);
            main.shadows = LightShadows.Soft;
            main.shadowStrength = .75f;
            Point("Weak overhead lamp", new Vector3(1.4f, 2.94f, .8f), new Color(.93f, .79f, .66f), .78f, 5);
            var fill = new GameObject("Soft camera fill").AddComponent<Light>();
            fill.type = LightType.Directional;
            fill.color = new Color(.68f, .60f, .83f);
            fill.intensity = .30f;
            fill.transform.rotation = Quaternion.Euler(45, 25, 0);

            GameObject actors = new GameObject("3D actors");
            Transform ema = Actor(emaPrefab, "樱羽艾玛 · rig and blend shapes", actors.transform, 1.62f, true);
            Transform hiro = Actor(tankPrefab, "二阶堂希罗 · BMPT72 placeholder", actors.transform, .65f, false);
            Transform jailer = Actor(tankPrefab, "看守 · BMPT72 placeholder", actors.transform, 1.3f, false);
            jailer.position = new Vector3(-1.55f, .03f, 3.5f);
            jailer.rotation = Quaternion.Euler(0, 90, 0);
            jailer.gameObject.SetActive(false);
            Transform warden = Actor(tankPrefab, "典狱长 · monitor BMPT72 placeholder", monitor, .22f, false);
            warden.position = new Vector3(-.30f, 2.61f, 2.26f);
            warden.rotation = Quaternion.Euler(0, 90, 0);
            warden.gameObject.SetActive(false);

            var camera = new GameObject("Prototype Camera").AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(.045f, .039f, .073f);
            camera.nearClipPlane = .04f;
            camera.farClipPlane = 30;
            camera.fieldOfView = 57;
            camera.transform.position = new Vector3(-.45f, 1.9f, -4.7f);
            camera.transform.LookAt(new Vector3(.05f, 1.45f, 1.65f));
            camera.gameObject.AddComponent<AudioListener>();
            var controller = new GameObject("Prison original dialogue player").AddComponent<CellPrototypeController>();
            controller.ema = ema;
            controller.hiro = hiro;
            controller.jailer = jailer;
            controller.monitorActor = warden;
            controller.monitorScreen = screen.GetComponent<Renderer>();
            controller.sceneCamera = camera;
            controller.storyAsset = storyAsset;
            controller.chineseFont = font;
            controller.voices = voices;
            controller.referenceBackground = reference;
            AssetDatabase.SaveAssets();
            if (!EditorSceneManager.SaveScene(scene, ScenePath)) throw new IOException("Could not save prison scene.");
            string preview = Path.Combine(Repo, ".3d-model/preview");
            Directory.CreateDirectory(preview);
            RenderPreview(camera, Path.Combine(preview, "scene-editor.png"));
            Debug.Log("[3D prototype] Scene generated with " + story.lines.Length + " original lines and " + voices.Count(v => v) + " voice clips.");
        }

        private static T CopyAsset<T>(string source, string target) where T : UnityEngine.Object
        {
            string sourcePath = Path.GetFullPath(Path.Combine(Repo, source));
            if (!sourcePath.StartsWith(Repo + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Source escaped the workspace.");
            string assetPath = Root + "/" + target;
            string absolute = Path.Combine(Directory.GetParent(Application.dataPath).FullName, assetPath);
            Directory.CreateDirectory(Path.GetDirectoryName(absolute));
            File.Copy(sourcePath, absolute, true);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (!asset) throw new InvalidDataException("Could not import " + assetPath);
            return asset;
        }

        private static Transform Actor(GameObject prefab, string name, Transform parent, float height, bool human)
        {
            Transform wrapper = new GameObject(name).transform;
            wrapper.SetParent(parent, false);
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.transform.SetParent(wrapper, false);
            Renderer[] renderers = instance.GetComponentsInChildren<Renderer>();
            Bounds bounds = renderers[0].bounds;
            foreach (Renderer renderer in renderers.Skip(1)) bounds.Encapsulate(renderer.bounds);
            float scale = height / bounds.size.y;
            if (!human) scale = Mathf.Min(scale, 1.48f / Mathf.Max(bounds.size.x, bounds.size.z));
            instance.transform.localScale = Vector3.one * scale;
            instance.transform.localPosition = new Vector3(-bounds.center.x * scale, -bounds.min.y * scale, -bounds.center.z * scale);
            Animator animator = instance.GetComponent<Animator>();
            if (animator) animator.enabled = false;
            return wrapper;
        }

        private static Material Mat(string name, Color color, float metallic = 0, float gloss = .12f)
        {
            if (Materials.TryGetValue(name, out Material cached)) return cached;
            string path = Root + "/Materials/" + name + ".mat";
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (!material) { material = new Material(Shader.Find("Standard")); AssetDatabase.CreateAsset(material, path); }
            material.color = color;
            material.SetFloat("_Metallic", metallic);
            material.SetFloat("_Glossiness", gloss);
            EditorUtility.SetDirty(material);
            Materials.Add(name, material);
            return material;
        }

        private static GameObject Cube(string name, Vector3 position, Vector3 size, Material material, Transform parent)
        {
            GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            item.name = name;
            item.transform.SetParent(parent, false);
            item.transform.position = position;
            item.transform.localScale = size;
            item.GetComponent<Renderer>().sharedMaterial = material;
            return item;
        }

        private static GameObject Cylinder(string name, Vector3 position, Vector3 size, Material material, Transform parent)
        {
            GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            item.name = name;
            item.transform.SetParent(parent, false);
            item.transform.position = position;
            item.transform.localScale = size;
            item.GetComponent<Renderer>().sharedMaterial = material;
            UnityEngine.Object.DestroyImmediate(item.GetComponent<Collider>());
            return item;
        }

        private static void Beam(string name, Vector3 from, Vector3 to, float radius, Material material, Transform parent)
        {
            GameObject item = Cylinder(name, (from + to) * .5f, new Vector3(radius, Vector3.Distance(from, to) * .5f, radius), material, parent);
            item.transform.up = to - from;
        }

        private static void Wall(Transform parent, Material stone, Material mortar, int side)
        {
            var holder = new GameObject(side == 0 ? "Door stone wall" : side == 1 ? "Right stone wall" : "Left stone wall").transform;
            holder.SetParent(parent, false);
            if (side == 0)
            {
                Cube("Right wall mortar backing", new Vector3(1.39f, 1.85f, 2.98f), new Vector3(3.99f, 3.70f, .12f), mortar, holder);
                Cube("Left wall mortar backing", new Vector3(-2.97f, 1.85f, 2.98f), new Vector3(.86f, 3.70f, .12f), mortar, holder);
                Cube("Arch upper mortar backing", new Vector3(-1.55f, 3.44f, 2.98f), new Vector3(2.03f, .61f, .12f), mortar, holder);
            }
            else Cube("Mortar backing", new Vector3(side == 1 ? 3.38f : -3.38f, 1.85f, 0), new Vector3(.12f, 3.70f, 6.8f), mortar, holder);
            for (int row = 0; row < 11; row++)
                for (int col = 0; col < 9; col++)
                {
                    float horizontal = -3.1f + col * .74f + (row % 2) * .35f;
                    float y = .16f + row * .34f;
                    if (horizontal > 3.16f) continue;
                    if (side == 0)
                    {
                        float dx = Mathf.Abs(horizontal + 1.55f);
                        if (dx < 1.04f && y < 2.27f) continue;
                        if (dx < 1.04f && y < 3.08f && dx * dx + (y - 2.12f) * (y - 2.12f) < 1.02f) continue;
                    }
                    Vector3 p = side == 0 ? new Vector3(horizontal, y, 2.78f) :
                        side == 1 ? new Vector3(3.2f, y, horizontal) : new Vector3(-3.2f, y, horizontal);
                    Vector3 size = side == 0 ? new Vector3(.70f, .31f, .28f) : new Vector3(.28f, .31f, .70f);
                    string tone = "StoneTone" + Random.Next(8);
                    float shade = .81f + (tone[tone.Length - 1] - '0') * .047f;
                    GameObject block = Cube("Masonry", p, size, Mat(tone, stone.color * shade), holder);
                    block.transform.rotation = Quaternion.Euler(0, 0, (float)(Random.NextDouble() - .5) * 1.8f);
                }
            // Near-camera ends remain open in the first vertical slice.
        }

        private static void Floor(Transform parent, Material stone)
        {
            for (int x = 0; x < 10; x++) for (int z = 0; z < 13; z++)
            {
                float tone = .68f + (float)Random.NextDouble() * .20f;
                Material material = Mat("FloorTone" + ((x + z * 3) % 6), stone.color * tone);
                GameObject slab = Cube("Cracked paving seam", new Vector3(-3.05f + x * .68f, -.024f, -3.8f + z * .7f),
                    new Vector3(.64f, .065f, .66f), material, parent);
                slab.transform.rotation = Quaternion.Euler(0, (float)(Random.NextDouble() - .5) * 3.5f, 0);
            }
        }

        private static void Door(Transform parent, Material stone, Material iron)
        {
            const float center = -1.55f, radius = .77f, spring = 2.12f;
            var gate = new GameObject("Locked arched gate · remains closed until Adv03").transform;
            gate.SetParent(parent, false);
            for (int i = 0; i < 15; i++)
            {
                float a = Mathf.PI * i / 15, b = Mathf.PI * (i + 1) / 15;
                Mesh mesh = ArcStone(radius + .04f, radius + .29f, a + .012f, b - .012f, .30f);
                mesh = SaveGeometry(mesh, "Arch" + i);
                var obj = new GameObject("Arch keystone " + i);
                obj.transform.SetParent(parent, false);
                obj.transform.position = new Vector3(center, spring, 2.67f);
                obj.AddComponent<MeshFilter>().sharedMesh = mesh;
                obj.AddComponent<MeshRenderer>().sharedMaterial = stone;
                float mid = (a + b) * .5f;
                Beam("Curved gate rim", new Vector3(center + radius * Mathf.Cos(a), spring + radius * Mathf.Sin(a), 2.54f),
                    new Vector3(center + radius * Mathf.Cos(b), spring + radius * Mathf.Sin(b), 2.54f), .024f, iron, gate);
            }
            Cube("Left door pier", new Vector3(center - radius - .16f, spring * .5f, 2.67f), new Vector3(.31f, spring, .36f), stone, parent);
            Cube("Right door pier", new Vector3(center + radius + .16f, spring * .5f, 2.67f), new Vector3(.31f, spring, .36f), stone, parent);
            for (int i = 0; i < 9; i++)
            {
                float x = -.72f + i * .18f;
                float top = spring + Mathf.Sqrt(Mathf.Max(0, radius * radius - x * x));
                Beam("Vertical prison bar", new Vector3(center + x, .03f, 2.54f), new Vector3(center + x, top, 2.54f), .020f, iron, gate);
            }
            foreach (float y in new[] { .20f, .95f, 1.68f, 2.12f })
                Beam("Gate crossbar", new Vector3(center - radius, y, 2.53f), new Vector3(center + radius, y, 2.53f), .025f, iron, gate);
            Cube("Latch", new Vector3(center + .6f, 1.12f, 2.48f), new Vector3(.12f, .19f, .09f), iron, gate);
            Cube("Corridor wall behind gate", new Vector3(-1.5f, 1.8f, 5.0f), new Vector3(3.2f, 3.6f, .25f), stone, parent);
        }

        private static Mesh ArcStone(float inner, float outer, float a, float b, float depth)
        {
            var vertices = new Vector3[8];
            for (int k = 0; k < 2; k++) for (int angle = 0; angle < 2; angle++) for (int r = 0; r < 2; r++)
            {
                float rad = r == 0 ? inner : outer, theta = angle == 0 ? a : b;
                vertices[k * 4 + angle * 2 + r] = new Vector3(Mathf.Cos(theta) * rad, Mathf.Sin(theta) * rad, (k - .5f) * depth);
            }
            int[] triangles = { 0,2,1, 1,2,3, 4,5,6, 5,7,6, 0,1,4, 1,5,4, 2,6,3, 3,6,7, 0,4,2, 2,4,6, 1,3,5, 3,7,5 };
            var mesh = new Mesh { vertices = vertices, triangles = triangles };
            mesh.RecalculateNormals(); mesh.RecalculateBounds(); return mesh;
        }

        private static void Bed(Transform parent, Material iron, Material cloth, Material pillow)
        {
            Transform bed = new GameObject("Double bunk · Ema lower / Hiro upper").transform;
            bed.SetParent(parent, false);
            foreach (float x in new[] { 1.13f, 2.47f }) foreach (float z in new[] { -.02f, 2.50f })
                Cylinder("Bed post", new Vector3(x, 1.23f, z), new Vector3(.035f, 1.21f, .035f), iron, bed);
            foreach (float y in new[] { .46f, 1.91f })
            {
                Cube("Bed frame", new Vector3(1.80f, y - .08f, 1.24f), new Vector3(1.38f, .075f, 2.58f), iron, bed);
                Cube("Thin mattress", new Vector3(1.80f, y, 1.24f), new Vector3(1.27f, .13f, 2.44f), cloth, bed);
                var cover = Cube("Uneven folded blanket", new Vector3(1.8f, y + .09f, .61f), new Vector3(1.31f, .08f, 1.18f), cloth, bed);
                cover.transform.rotation = Quaternion.Euler(0, -2, 1.4f);
                Cube("Pillow", new Vector3(1.80f, y + .11f, 2.17f), new Vector3(.82f, .13f, .40f), pillow, bed);
                foreach (float z in new[] { -.03f, 2.50f }) foreach (float offset in new[] { .15f, .40f })
                    Beam("End rail", new Vector3(1.13f, y + offset, z), new Vector3(2.47f, y + offset, z), .023f, iron, bed);
            }
            foreach (float x in new[] { 1.53f, 2.08f })
                Beam("Ladder side", new Vector3(x, .08f, -.12f), new Vector3(x, 2.31f, -.12f), .023f, iron, bed);
            for (int i = 0; i < 6; i++) Beam("Ladder rung", new Vector3(1.53f, .24f + i * .34f, -.12f), new Vector3(2.08f, .24f + i * .34f, -.12f), .019f, iron, bed);
        }

        private static void Chain(Transform parent, Material iron, Vector3 position)
        {
            Cube("Chain wall bracket", position, new Vector3(.17f, .18f, .08f), iron, parent);
            for (int i = 0; i < 11; i++) Ring("Alternating chain link", position + new Vector3(0, -.11f * i, -.07f), .06f, .008f, i % 2 == 0 ? 0 : 90, iron, parent);
            Ring("Handcuff", position + new Vector3(0, -1.25f, -.1f), .115f, .015f, 0, iron, parent);
        }

        private static void Ring(string name, Vector3 position, float radius, float thickness, float rotation, Material material, Transform parent)
        {
            const int count = 20;
            var ring = new GameObject(name).transform; ring.SetParent(parent, false); ring.position = position;
            for (int i = 0; i < count; i++)
            {
                float a = i * Mathf.PI * 2 / count, b = (i + 1) * Mathf.PI * 2 / count;
                Beam("Link segment", position + new Vector3(Mathf.Cos(a) * radius, Mathf.Sin(a) * radius, 0),
                    position + new Vector3(Mathf.Cos(b) * radius, Mathf.Sin(b) * radius, 0), thickness, material, ring);
            }
            ring.rotation = Quaternion.Euler(0, rotation, 0);
        }

        private static void Bowl(Transform parent, Material iron, Vector3 position, float radius)
        {
            GameObject bowl = Cylinder("Metal food plate", position, new Vector3(radius, .018f, radius), iron, parent);
            Ring("Plate rim", position + new Vector3(0, .018f, 0), radius * .5f, .008f, 0, iron, parent);
            Transform rim = parent.GetChild(parent.childCount - 1); rim.rotation = Quaternion.Euler(90, 0, 0);
        }

        private static void Bucket(Transform parent, Material iron)
        {
            Vector3 position = new Vector3(-2.6f, .27f, -.2f);
            Cylinder("Bucket", position, new Vector3(.28f, .25f, .28f), iron, parent);
            Ring("Bucket handle", position + new Vector3(0, .3f, 0), .19f, .012f, 0, iron, parent);
        }

        private static void Debris(Transform parent, Material stone)
        {
            for (int i = 0; i < 24; i++)
            {
                float x = (float)Random.NextDouble() * 5.7f - 2.85f, z = (float)Random.NextDouble() * 5.4f - 2.7f;
                var piece = Cube("Loose rubble", new Vector3(x, .03f, z), new Vector3(.06f + (float)Random.NextDouble() * .10f, .06f, .08f), stone, parent);
                piece.transform.rotation = Quaternion.Euler(15, Random.Next(180), 23);
            }
        }

        private static void Lamp(Transform parent, Material iron)
        {
            Beam("Lamp cord", new Vector3(1.45f, 3.60f, .9f), new Vector3(1.45f, 3.08f, .9f), .009f, iron, parent);
            Cylinder("Pendant lamp shade", new Vector3(1.45f, 3.07f, .9f), new Vector3(.26f, .07f, .26f), iron, parent);
            Material bulb = Mat("LampGlow", new Color(.93f, .82f, .67f));
            bulb.EnableKeyword("_EMISSION"); bulb.SetColor("_EmissionColor", new Color(.93f, .75f, .48f) * .7f);
            Cylinder("Lamp diffuser", new Vector3(1.45f, 2.99f, .9f), new Vector3(.22f, .012f, .22f), bulb, parent);
        }

        private static Light Point(string name, Vector3 position, Color color, float intensity, float range)
        {
            var light = new GameObject(name).AddComponent<Light>();
            light.type = LightType.Point; light.transform.position = position; light.color = color; light.intensity = intensity; light.range = range; return light;
        }

        private static Mesh SaveGeometry(Mesh mesh, string name)
        {
            mesh.name = name;
            string path = Root + "/Geometry/" + name + ".asset";
            var old = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (old) { EditorUtility.CopySerialized(mesh, old); UnityEngine.Object.DestroyImmediate(mesh); return old; }
            else AssetDatabase.CreateAsset(mesh, path);
            return mesh;
        }

        private static void RenderPreview(Camera camera, string output)
        {
            var target = new RenderTexture(1440, 900, 24);
            var pixels = new Texture2D(1440, 900, TextureFormat.RGB24, false);
            RenderTexture previous = RenderTexture.active;
            try
            {
                camera.targetTexture = target; camera.Render(); RenderTexture.active = target;
                pixels.ReadPixels(new Rect(0, 0, 1440, 900), 0, 0); pixels.Apply(); File.WriteAllBytes(output, pixels.EncodeToPNG());
            }
            finally { camera.targetTexture = null; RenderTexture.active = previous; UnityEngine.Object.DestroyImmediate(target); UnityEngine.Object.DestroyImmediate(pixels); }
        }

        private static void Build()
        {
            string output = Path.Combine(Repo, "dist/3d-prototype/Manosaba-Cell3D.exe");
            Directory.CreateDirectory(Path.GetDirectoryName(output));
            var target = NamedBuildTarget.Standalone;
            var previous = PlayerSettings.GetManagedStrippingLevel(target);
            var addresses = AddressableAssetSettingsDefaultObject.Settings;
            var previousAddressBuild = addresses.BuildAddressablesWithPlayerBuild;
            try
            {
                PlayerSettings.SetManagedStrippingLevel(target, ManagedStrippingLevel.Disabled);
                // This isolated scene references its assets directly and does not
                // need a rebuild of the full game's Addressables catalogue.
                addresses.BuildAddressablesWithPlayerBuild = AddressableAssetSettings.PlayerBuildOption.DoNotBuildWithPlayer;
                BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions {
                    scenes = new[] { ScenePath }, locationPathName = output, target = BuildTarget.StandaloneWindows64, options = BuildOptions.None
                });
                File.WriteAllText(Path.Combine(Repo, ".3d-model/preview/build-verification.json"),
                    "{\"result\":\"" + report.summary.result + "\",\"errors\":" + report.summary.totalErrors + ",\"bytes\":" + report.summary.totalSize + "}");
                if (report.summary.result != BuildResult.Succeeded) throw new InvalidOperationException("3D prototype Player build failed.");
                Debug.Log("[3D prototype] Standalone ready: " + output);
            }
            finally
            {
                addresses.BuildAddressablesWithPlayerBuild = previousAddressBuild;
                PlayerSettings.SetManagedStrippingLevel(target, previous);
            }
        }
    }
}
