// Integration-layer diagnostic (not game code): what is actually on screen.
//
// Opt-in with MANOSABA_RENDER_DUMP=<seconds>[,<seconds>...] (times since startup).  At each time it writes, to
// MANOSABA_RENDER_DUMP_PATH (default <persistentDataPath>/manosaba_render_dump.txt), every enabled camera and every
// enabled root canvas with the graphics that can actually be seen: active and enabled, effective alpha (color alpha x
// every CanvasGroup above it) above 1 %, sorted by canvas order, with their screen coverage and sprite / texture.  A
// screenshot is saved next to it.  This is how "the whole screen is white" is turned into "this Image on that canvas".
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Manosaba.Integration
{
    public class ManosabaRenderDump : MonoBehaviour
    {
        private const string TimesEnv = "MANOSABA_RENDER_DUMP";
        private const string PathEnv = "MANOSABA_RENDER_DUMP_PATH";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Install()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(TimesEnv))) return;
            var host = new GameObject("ManosabaRenderDump");
            DontDestroyOnLoad(host);
            host.AddComponent<ManosabaRenderDump>();
        }

        private IEnumerator Start()
        {
            string path = Environment.GetEnvironmentVariable(PathEnv);
            if (string.IsNullOrEmpty(path)) path = Path.Combine(Application.persistentDataPath, "manosaba_render_dump.txt");
            var times = Environment.GetEnvironmentVariable(TimesEnv).Split(',')
                .Select(s => float.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float v) ? v : -1f)
                .Where(v => v >= 0f).OrderBy(v => v).ToList();
            var sb = new StringBuilder();
            foreach (float t in times)
            {
                while (Time.realtimeSinceStartup < t) yield return null;
                yield return new WaitForEndOfFrame();
                Dump(sb, t);
                string shot = Path.ChangeExtension(path, null) + "_t" + t.ToString("0", CultureInfo.InvariantCulture) + ".png";
                try { ScreenCapture.CaptureScreenshot(shot); } catch (Exception ex) { sb.AppendLine("screenshot failed: " + ex.Message); }
                File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
            }
        }

        private static void Dump(StringBuilder sb, float t)
        {
            sb.AppendLine("==== t=" + t.ToString("0.0", CultureInfo.InvariantCulture) + " frame=" + Time.frameCount +
                          " screen=" + Screen.width + "x" + Screen.height);
            string locale = LocaleState();
            sb.AppendLine(locale);
            Debug.Log("[ManosabaRenderDump] " + locale);
            foreach (Camera cam in Camera.allCameras.OrderBy(c => c.depth))
            {
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "camera {0} depth={1} clear={2} bg={3} mask=0x{4:X} rt={5} ortho={6} tag={7}",
                    PathOf(cam.transform), cam.depth, cam.clearFlags, cam.backgroundColor, cam.cullingMask,
                    cam.targetTexture ? cam.targetTexture.name : "-", cam.orthographic, cam.tag));
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "   pos={0} size={1:0.###} near={2:0.###} far={3:0.###} enabled={4}",
                    cam.transform.position, cam.orthographicSize, cam.nearClipPlane, cam.farClipPlane, cam.enabled));
                // Scene renderers (not UI) this camera draws: in its culling mask, enabled, and inside its frustum.
                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
                foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
                {
                    if (!r.enabled || !r.gameObject.activeInHierarchy || r.forceRenderingOff) continue;
                    if (r is CanvasRenderer || (cam.cullingMask & (1 << r.gameObject.layer)) == 0) continue;
                    if (!GeometryUtility.TestPlanesAABB(planes, r.bounds)) continue;
                    var mat = r.sharedMaterial;
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "   renderer {0} {1} layer={2} sort={3}/{4} center={5} size={6} shader={7} sprite={8}",
                        r.GetType().Name, PathOf(r.transform), r.gameObject.layer, r.sortingLayerName, r.sortingOrder,
                        r.bounds.center, r.bounds.size, mat ? mat.shader.name : "NONE",
                        r is SpriteRenderer sr && sr.sprite ? sr.sprite.name : "-"));
                }
            }
            var canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None).Where(c => c.isRootCanvas)
                .OrderBy(c => c.sortingLayerID).ThenBy(c => c.sortingOrder).ToList();
            float screenArea = Mathf.Max(1f, Screen.width * (float)Screen.height);
            foreach (Canvas canvas in canvases)
            {
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "canvas {0} enabled={1} active={2} mode={3} order={4} cam={5} scale={6:0.###}",
                    PathOf(canvas.transform), canvas.enabled, canvas.gameObject.activeInHierarchy, canvas.renderMode,
                    canvas.sortingOrder, canvas.worldCamera ? PathOf(canvas.worldCamera.transform) : "-", canvas.scaleFactor));
                if (!canvas.enabled || !canvas.gameObject.activeInHierarchy) continue;
                foreach (Graphic g in canvas.GetComponentsInChildren<Graphic>(false))
                {
                    if (!g.enabled) continue;
                    float alpha = g.color.a * GroupAlpha(g.transform);
                    if (alpha < 0.01f) continue;
                    Rect r = ScreenRect(g.rectTransform, canvas);
                    float cover = Mathf.Max(0f, r.width) * Mathf.Max(0f, r.height) / screenArea;
                    if (cover < 0.02f) continue;
                    // overrideSprite is what the Image draws (LocalizeImage swaps it per locale; it falls back to sprite)
                    string source = g is Image img ? "sprite=" + (img.overrideSprite ? img.overrideSprite.name : "NONE")
                        : g is RawImage raw ? "texture=" + (raw.texture ? raw.texture.name : "NONE")
                        : g is TMPro.TMP_Text tmp ? "text='" + Trim(tmp.text) + "'" : g.GetType().Name;
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "   {0,5:0.0}% a={1:0.00} {2} {3} color={4} {5} rect=({6:0},{7:0},{8:0},{9:0})",
                        cover * 100f, alpha, g.GetType().Name, PathOf(g.transform), g.color, source, r.x, r.y, r.width, r.height));
                }
            }
        }

        /// <summary>
        /// Which locale the game is in and why: the OS language Unity reports, the game's default-locale selector (while it is
        /// registered), the game's own localization manager and Naninovel's.  Read by reflection so this diagnostic adds no
        /// assembly references; anything unreadable is printed as such.
        /// </summary>
        private static string LocaleState()
        {
            var sb = new StringBuilder("locale systemLanguage=" + Application.systemLanguage);
            sb.Append(" selector=" + DescribeService("GigaCreation.Essentials.Localization.IDefaultLocaleSelector", "DefaultLocale", "AvailableTextLanguages"));
            sb.Append(" giga=" + DescribeService("GigaCreation.Essentials.Localization.ILocalizationManager", "CurrentLocale"));
            try
            {
                var naninovel = Naninovel.Engine.GetService<Naninovel.ILocalizationManager>();
                sb.Append(" naninovel=" + (naninovel == null ? "(no service)" :
                    naninovel.SelectedLocale + " available=[" + string.Join(",", naninovel.AvailableLocales) + "]"));
            }
            catch (Exception ex) { sb.Append(" naninovel=(" + ex.GetType().Name + ")"); }
            return sb.ToString();
        }

        private static string DescribeService(string interfaceName, params string[] properties)
        {
            try
            {
                Type type = AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetType(interfaceName, false)).FirstOrDefault(x => x != null);
                if (type == null) return "(type not loaded)";
                MethodInfo tryGet = typeof(GigaCreation.Tools.Service.ServiceLocator).GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(m => m.Name == "TryGet" && m.IsGenericMethodDefinition && m.GetParameters().Length == 2).MakeGenericMethod(type);
                var args = new object[] { null, true };
                if (!(bool)tryGet.Invoke(null, args)) return "(not registered)";
                return "{" + string.Join(" ", properties.Select(name =>
                {
                    // the concrete object's property: an interface type does not list properties it inherits
                    object value = args[0].GetType().GetProperty(name)?.GetValue(args[0]);
                    return name + "=" + (value is System.Collections.IEnumerable list && !(value is string)
                        ? "[" + string.Join(",", list.Cast<object>()) + "]" : value ?? "null");
                })) + "}";
            }
            catch (Exception ex) { return "(" + ex.GetType().Name + ": " + ex.Message + ")"; }
        }

        private static float GroupAlpha(Transform t)
        {
            float a = 1f;
            for (; t != null; t = t.parent)
            {
                foreach (CanvasGroup group in t.GetComponents<CanvasGroup>())
                {
                    if (group.enabled) a *= group.alpha;
                    if (group.enabled && group.ignoreParentGroups) return a;
                }
            }
            return a;
        }

        private static Rect ScreenRect(RectTransform rt, Canvas canvas)
        {
            var corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue), max = new Vector2(float.MinValue, float.MinValue);
            foreach (Vector3 c in corners)
            {
                Vector2 p = RectTransformUtility.WorldToScreenPoint(cam, c);
                min = Vector2.Min(min, p);
                max = Vector2.Max(max, p);
            }
            min = Vector2.Max(min, Vector2.zero);
            max = Vector2.Min(max, new Vector2(Screen.width, Screen.height));
            return new Rect(min, max - min);
        }

        private static string Trim(string s) => s == null ? "" : (s.Length > 24 ? s.Substring(0, 24) + "..." : s).Replace("\n", " ");

        private static string PathOf(Transform t)
        {
            var parts = new List<string>();
            for (; t != null; t = t.parent) parts.Add(t.name);
            parts.Reverse();
            return string.Join("/", parts);
        }
    }
}
