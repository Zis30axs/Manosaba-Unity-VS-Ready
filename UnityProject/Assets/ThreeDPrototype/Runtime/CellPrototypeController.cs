using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Manosaba.ThreeDPrototype
{
    // A local vertical slice. It reads the original dialogue, but owns its own
    // camera, staging and UI; no Naninovel save or scenario asset is changed.
    public sealed class CellPrototypeController : MonoBehaviour
    {
        [Serializable] public class Line
        {
            public string speaker, text, stage, authorId, key, voice;
            public int scriptLine;
        }
        [Serializable] public class Story { public string script; public Line[] lines; }
        [Serializable] public class Receipt
        {
            public string status, error;
            public int lines, visited, stages, voices, blendShapes, bones;
            public bool emaRendered, allMaterialsSupported, sceneCaptureNonBlank;
        }
        public TextAsset storyAsset;
        public Font chineseFont;
        public AudioClip[] voices;
        public Camera sceneCamera;
        public Transform ema, hiro, jailer, monitorActor;
        public Renderer monitorScreen;
        public Texture referenceBackground;

        private Story story;
        private int current;
        private bool finished, overview, reference, freeCamera, pausedVoice;
        private AudioSource audioSource;
        private GUIStyle titleStyle, bodyStyle, buttonStyle, smallStyle;
        private readonly Dictionary<string, Transform> bones = new Dictionary<string, Transform>();
        private readonly Dictionary<Transform, Quaternion> rest = new Dictionary<Transform, Quaternion>();
        private SkinnedMeshRenderer[] faceRenderers;
        private Vector3 cameraTarget, cameraLook, emaTarget;
        private Quaternion emaRotation;
        private float stageStarted;
        private string activeStage;
        private Vector2 orbit = new Vector2(0, 12);
        private float orbitDistance = 5.8f;
        private bool smoke;
        private string capturedError;
        private bool capturesNonBlank = true;

        public int LineCount => story == null ? 0 : story.lines.Length;
        public Line CurrentLine => story.lines[current];

        private void Awake()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
            story = JsonUtility.FromJson<Story>(storyAsset.text);
            if (story == null || story.lines == null || story.lines.Length == 0)
                throw new InvalidDataException("The original prison dialogue is missing.");
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0;
            audioSource.volume = .85f;
            foreach (Transform bone in ema.GetComponentsInChildren<Transform>())
            {
                if (!bones.ContainsKey(bone.name)) bones.Add(bone.name, bone);
                rest[bone] = bone.localRotation;
            }
            faceRenderers = ema.GetComponentsInChildren<SkinnedMeshRenderer>();
            smoke = Environment.GetCommandLineArgs().Contains("-prototypeSmoke");
            Application.logMessageReceived += ObserveLog;
            SetLine(0, true);
        }

        private void Start()
        {
            if (!Application.isEditor && !smoke) Screen.SetResolution(1440, 900, false);
            if (smoke) StartCoroutine(VerifyPlayer());
        }

        private void OnDestroy() { Application.logMessageReceived -= ObserveLog; }
        private void ObserveLog(string message, string stack, LogType type)
        {
            if (type == LogType.Exception || type == LogType.Error) capturedError = message + "\n" + stack;
        }

        public void SetLine(int index, bool instant = false)
        {
            current = Mathf.Clamp(index, 0, story.lines.Length - 1);
            finished = false;
            Line line = CurrentLine;
            audioSource.Stop();
            audioSource.clip = voices != null && current < voices.Length ? voices[current] : null;
            if (audioSource.clip && !pausedVoice && !smoke) audioSource.Play();
            if (activeStage != line.stage) { activeStage = line.stage; stageStarted = Time.time; }
            bool wake = line.stage == "wake";
            bool bunk = wake || line.stage == "bars" || line.stage == "hiro_bunk";
            bool broadcast = line.stage == "monitor";
            bool guards = new[] { "jailer", "cling", "rejection", "threshold" }.Contains(line.stage);
            jailer.gameObject.SetActive(guards);
            monitorActor.gameObject.SetActive(broadcast);
            monitorScreen.sharedMaterial.color = broadcast ? new Color(.17f, .22f, .3f) : new Color(.018f, .025f, .025f);
            hiro.position = bunk ? new Vector3(1.8f, 2.07f, 1.65f) : new Vector3(.8f, .03f, 1.1f);
            hiro.rotation = Quaternion.Euler(0, bunk ? 90 : -25, 0);
            emaTarget = wake ? new Vector3(1.45f, -.36f, .45f) :
                line.stage == "bars" || line.stage == "hiro_bunk" ? new Vector3(-1.55f, 0, 1.8f) :
                line.stage == "cling" ? new Vector3(.1f, 0, .8f) : new Vector3(-.35f, 0, .75f);
            emaRotation = Quaternion.Euler(0, wake ? 255 : line.stage == "bars" || broadcast || line.stage == "jailer" ? 0 : 155, 0);
            if (current == 0)
            {
                emaTarget = new Vector3(1.78f, .58f, .30f);
                emaRotation = Quaternion.Euler(-90, 180, 0);
            }
            Pose(wake && current > 0, line.stage == "cling", guards);
            ChooseShot();
            if (instant)
            {
                ema.position = emaTarget;
                ema.rotation = emaRotation;
                sceneCamera.transform.position = cameraTarget;
                sceneCamera.transform.LookAt(cameraLook);
            }
        }

        private void ChooseShot()
        {
            string stage = CurrentLine.stage;
            if (overview || stage == "threshold")
            {
                cameraTarget = new Vector3(-.45f, 1.9f, -4.7f);
                cameraLook = new Vector3(.05f, 1.45f, 1.65f);
                sceneCamera.fieldOfView = 57;
            }
            else if (stage == "wake")
            {
                cameraTarget = current == 0 ? new Vector3(.25f, 1.28f, 1.2f) : new Vector3(.15f, 1.26f, .1f);
                cameraLook = current == 0 ? new Vector3(1.78f, .67f, 1.45f) : new Vector3(1.45f, .98f, .58f);
                sceneCamera.fieldOfView = 53;
            }
            else if (stage == "monitor")
            {
                cameraTarget = new Vector3(-1.85f, 1.7f, -.55f);
                cameraLook = new Vector3(-.18f, 2.7f, 2.54f);
                sceneCamera.fieldOfView = 38;
            }
            else if (stage == "jailer")
            {
                cameraTarget = new Vector3(.2f, 1.65f, -1.65f);
                cameraLook = new Vector3(-1.3f, 1.45f, 3.0f);
                sceneCamera.fieldOfView = 49;
            }
            else
            {
                cameraTarget = new Vector3(-.55f, 1.75f, -3.15f);
                cameraLook = new Vector3(.15f, 1.15f, 1.25f);
                sceneCamera.fieldOfView = 53;
            }
        }

        private void Pose(bool sitting, bool cling, bool afraid)
        {
            foreach (var pair in rest) pair.Key.localRotation = pair.Value;
            // Direct PMX joint posing: this is deliberately not sold as a finished
            // retargeted animation or MMD physics simulation.
            Rotate("左腕", new Vector3(0, 0, -62));
            Rotate("右腕", new Vector3(0, 0, 62));
            Rotate("左ひじ", new Vector3(0, -18, -7));
            Rotate("右ひじ", new Vector3(0, 18, 7));
            if (sitting)
            {
                Rotate("左足", new Vector3(-90, 0, -4));
                Rotate("右足", new Vector3(-90, 0, 4));
                Rotate("左ひざ", new Vector3(90, 0, 0));
                Rotate("右ひざ", new Vector3(90, 0, 0));
                // This PMX skins the legs to the separate D chains. MMD normally
                // copies rotations using inheritance; this first importer uses FK.
                Rotate("左足D", new Vector3(-90, 0, -4));
                Rotate("右足D", new Vector3(-90, 0, 4));
                Rotate("左ひざD", new Vector3(90, 0, 0));
                Rotate("右ひざD", new Vector3(90, 0, 0));
                Rotate("上半身", new Vector3(10, 0, 0));
                // Fan the first skirt segment over the lap/bed; keep the waist
                // chain upright. Descendant segments inherit this coarse fold.
                float[] skirtX = { -80, -78.5f, -70.6f, 0, 70.6f, 78.5f, 80, 78.5f, 70.6f, 0, -70.6f, -78.5f };
                float[] skirtZ = { 0, 29.5f, 58.5f, 80, 58.5f, 29.5f, 0, -29.5f, -58.5f, -80, -58.5f, -29.5f };
                for (int i = 0; i < 12; i++) Rotate("Skt_1_" + i, new Vector3(skirtX[i], 0, skirtZ[i]));
                // Fold only the free front hem down; earlier segments must stay
                // over the mattress until cloth constraints are implemented.
                Rotate("Skt_4_0", new Vector3(50, 0, 0));
                Rotate("Skt_4_1", new Vector3(49, 0, -18.5f));
                Rotate("Skt_5_11", new Vector3(35, 0, 13));
            }
            if (cling)
            {
                Rotate("左腕", new Vector3(-35, 0, -20));
                Rotate("左ひじ", new Vector3(0, -65, 0));
            }
            if (afraid) Rotate("首", new Vector3(7, 0, 0));
        }

        private void Rotate(string name, Vector3 angles)
        {
            if (bones.TryGetValue(name, out Transform bone)) bone.localRotation = rest[bone] * Quaternion.Euler(angles);
        }

        private void Update()
        {
            float t = 1f - Mathf.Exp(-Time.deltaTime * 6);
            ema.position = Vector3.Lerp(ema.position, emaTarget, t);
            ema.rotation = Quaternion.Slerp(ema.rotation, emaRotation, t);
            if (freeCamera)
            {
                if (Input.GetMouseButton(1))
                {
                    orbit.x += Input.GetAxis("Mouse X") * 3;
                    orbit.y = Mathf.Clamp(orbit.y - Input.GetAxis("Mouse Y") * 3, -10, 65);
                }
                orbitDistance = Mathf.Clamp(orbitDistance - Input.mouseScrollDelta.y * .4f, 1, 8);
                Vector3 focus = new Vector3(0, 1.2f, .8f);
                sceneCamera.transform.position = focus + Quaternion.Euler(orbit.y, orbit.x, 0) * new Vector3(0, 0, -orbitDistance);
                sceneCamera.transform.LookAt(focus);
            }
            else
            {
                sceneCamera.transform.position = Vector3.Lerp(sceneCamera.transform.position, cameraTarget, t);
                sceneCamera.transform.rotation = Quaternion.Slerp(sceneCamera.transform.rotation,
                    Quaternion.LookRotation(cameraLook - sceneCamera.transform.position), t);
            }
            AnimateFace();
            if (smoke) return;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.RightArrow)) Next();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) SetLine(current - 1);
            if (Input.GetKeyDown(KeyCode.R)) SetLine(0);
            if (Input.GetKeyDown(KeyCode.V)) ToggleOverview();
            if (Input.GetKeyDown(KeyCode.F)) freeCamera = !freeCamera;
        }

        private void AnimateFace()
        {
            float age = Time.time - stageStarted;
            float blinkPhase = Time.time % 4.5f;
            float blink = !smoke && blinkPhase < .18f ? Mathf.Sin(blinkPhase / .18f * Mathf.PI) * 100 : 0;
            bool speaking = CurrentLine.authorId == "Ema" && audioSource.isPlaying;
            bool sorrow = CurrentLine.stage == "rejection" || CurrentLine.stage == "threshold";
            foreach (var renderer in faceRenderers)
            {
                Mesh mesh = renderer.sharedMesh;
                for (int i = 0; i < mesh.blendShapeCount; i++)
                {
                    string name = mesh.GetBlendShapeName(i);
                    float weight = 0;
                    if (name == "まばたき") weight = blink;
                    else if (name == "あ") weight = speaking ? (Mathf.Sin(Time.time * 14) * .5f + .5f) * 30 : 0;
                    else if (name == "びっくり") weight = sorrow ? 0 : 18;
                    else if (name == "困る") weight = sorrow ? 50 : 16;
                    renderer.SetBlendShapeWeight(i, weight);
                }
            }
            if (bones.TryGetValue("上半身2", out Transform chest))
                chest.localRotation = rest[chest] * Quaternion.Euler(Mathf.Sin(age * 2.1f) * .7f, 0, 0);
        }

        private void Next()
        {
            if (current + 1 < story.lines.Length) SetLine(current + 1);
            else { finished = true; audioSource.Stop(); }
        }
        private void ToggleOverview() { overview = !overview; freeCamera = false; ChooseShot(); }

        private void OnGUI()
        {
            if (story == null) return;
            float scale = Mathf.Min(Screen.width / 1440f, Screen.height / 900f);
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(scale, scale, 1));
            float width = Screen.width / scale, height = Screen.height / scale;
            if (bodyStyle == null)
            {
                titleStyle = new GUIStyle(GUI.skin.label) { font = chineseFont, fontSize = 25, normal = { textColor = new Color(.97f, .84f, .76f) } };
                bodyStyle = new GUIStyle(GUI.skin.label) { font = chineseFont, fontSize = 26, wordWrap = true, richText = false, normal = { textColor = new Color(.93f, .92f, .97f) } };
                smallStyle = new GUIStyle(bodyStyle) { fontSize = 16 };
                buttonStyle = new GUIStyle(GUI.skin.button) { font = chineseFont, fontSize = 17, padding = new RectOffset(12, 12, 7, 7) };
            }
            Panel(new Rect(0, 0, width, 60), new Color(.035f, .032f, .06f, .9f));
            GUI.Label(new Rect(26, 12, 850, 34), "樱羽艾玛 · 牢房初醒    /    3D 场景测试", titleStyle);
            if (GUI.Button(new Rect(width - 360, 12, 100, 34), overview ? "剧情镜头" : "全景 V", buttonStyle)) ToggleOverview();
            if (GUI.Button(new Rect(width - 250, 12, 110, 34), freeCamera ? "固定镜头" : "自由视角 F", buttonStyle)) freeCamera = !freeCamera;
            if (GUI.Button(new Rect(width - 130, 12, 100, 34), "原图对照", buttonStyle)) reference = !reference;
            if (reference && referenceBackground)
            {
                Rect rect = new Rect(24, 76, 570, 285);
                GUI.DrawTexture(rect, referenceBackground, ScaleMode.ScaleToFit);
                GUI.Label(new Rect(26, 365, 600, 28), "游戏原图：用于核对铁门、双层床和显示器的位置", smallStyle);
            }
            float top = height - 220;
            Panel(new Rect(0, top, width, 220), new Color(.026f, .022f, .048f, .95f));
            Panel(new Rect(28, top + 17, 4, 157), new Color(.74f, .43f, .61f));
            GUI.Label(new Rect(50, top + 17, width - 100, 36), CurrentLine.speaker, titleStyle);
            GUI.Label(new Rect(50, top + 63, width - 100, 102), finished ? "测试片段结束：停在牢房出门前。\n可重播，或使用自由视角检查场景。" : CurrentLine.text, bodyStyle);
            GUI.Label(new Rect(50, top + 181, 800, 28), (current + 1) + " / " + story.lines.Length + "    空格 / 回车继续 · ← 返回 · R 重播 · 右键拖动自由视角", smallStyle);
            if (GUI.Button(new Rect(width - 380, top + 176, 95, 34), "上一句", buttonStyle)) SetLine(current - 1);
            if (GUI.Button(new Rect(width - 275, top + 176, 95, 34), pausedVoice ? "开启配音" : "静音", buttonStyle))
            {
                pausedVoice = !pausedVoice;
                if (pausedVoice) audioSource.Stop();
                else if (audioSource.clip) audioSource.Play();
            }
            if (GUI.Button(new Rect(width - 170, top + 176, 140, 34), finished ? "重播片段" : "继续 →", buttonStyle))
            { if (finished) SetLine(0); else Next(); }
        }

        private static void Panel(Rect rect, Color color)
        {
            Color previous = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = previous;
        }

        private IEnumerator VerifyPlayer()
        {
            string output = Path.Combine(Application.dataPath, "../preview");
            string[] args = Environment.GetCommandLineArgs();
            int flag = Array.IndexOf(args, "-prototypeEvidence");
            if (flag >= 0 && flag + 1 < args.Length) output = Path.GetFullPath(args[flag + 1]);
            Directory.CreateDirectory(output);
            var receipt = new Receipt { lines = story.lines.Length, voices = voices.Count(v => v),
                blendShapes = faceRenderers.Sum(r => r.sharedMesh.blendShapeCount), bones = bones.Count };
            var visitedStages = new HashSet<string>();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < story.lines.Length; i++)
            {
                SetLine(i, true);
                yield return null;
                receipt.visited++;
                if (visitedStages.Add(CurrentLine.stage))
                {
                    yield return new WaitForSeconds(.2f);
                    yield return new WaitForEndOfFrame();
                    CaptureView(Path.Combine(output, CurrentLine.stage + ".png"));
                    yield return null;
                }
            }
            SetLine(4, true);
            yield return new WaitForSeconds(.2f);
            yield return new WaitForEndOfFrame();
            CaptureView(Path.Combine(output, "sitting.png"));
            yield return null;
            overview = true;
            SetLine(4, true);
            yield return new WaitForSeconds(.2f);
            yield return new WaitForEndOfFrame();
            CaptureView(Path.Combine(output, "overview.png"));
            yield return new WaitForSeconds(.5f);
            receipt.stages = visitedStages.Count;
            Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(sceneCamera);
            receipt.emaRendered = faceRenderers.Length > 0 && faceRenderers.All(r => r.sharedMesh.vertexCount > 0) &&
                faceRenderers.Any(r => GeometryUtility.TestPlanesAABB(frustum, r.bounds));
            receipt.allMaterialsSupported = FindObjectsByType<Renderer>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .SelectMany(r => r.sharedMaterials).All(m => m && m.shader && m.shader.isSupported);
            receipt.error = capturedError;
            receipt.sceneCaptureNonBlank = capturesNonBlank;
            receipt.status = receipt.visited == receipt.lines && receipt.stages == 10 && receipt.voices == 33 &&
                receipt.blendShapes > 0 && receipt.emaRendered && receipt.allMaterialsSupported && capturesNonBlank && capturedError == null ? "PASS" : "FAIL";
            File.WriteAllText(Path.Combine(output, "runtime-verification.json"), JsonUtility.ToJson(receipt, true));
            Debug.Log("[3D prototype] Runtime verification " + receipt.status);
            Application.Quit(receipt.status == "PASS" ? 0 : 1);
        }

        private void CaptureView(string output)
        {
            // Windows suppresses presentation for a hidden test window. Explicit
            // camera rendering exercises the real Player shaders and skinning
            // without depending on window visibility or desktop screenshots.
            var target = new RenderTexture(1440, 900, 24);
            var pixels = new Texture2D(1440, 900, TextureFormat.RGB24, false);
            RenderTexture previous = RenderTexture.active;
            try
            {
                sceneCamera.targetTexture = target;
                sceneCamera.Render();
                RenderTexture.active = target;
                pixels.ReadPixels(new Rect(0, 0, 1440, 900), 0, 0);
                pixels.Apply();
                float min = 1, max = 0;
                for (int x = 50; x < 1440; x += 80) for (int y = 50; y < 900; y += 80)
                {
                    float value = pixels.GetPixel(x, y).grayscale;
                    min = Mathf.Min(min, value); max = Mathf.Max(max, value);
                }
                capturesNonBlank &= max - min > .05f;
                File.WriteAllBytes(output, pixels.EncodeToPNG());
            }
            finally
            {
                sceneCamera.targetTexture = null;
                RenderTexture.active = previous;
                target.Release(); Destroy(target); Destroy(pixels);
            }
        }
    }
}
