// Installs the content gate (Assets/Plugins/Android/ContentGateActivity.java) as the Android launcher activity.
//
// Only when BuildAndroidPlayer set ManifestJson, i.e. when the bundles ship outside the player as zip parts (see
// BuildAndroidPlayer.StashBundles).  Then the generated manifest gets the gate activity with the MAIN/LAUNCHER filter, the Unity
// activity loses that filter (the gate starts it), and the list of expected bundles goes into the APK as assets/manosaba-content.json.
#if UNITY_ANDROID
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.Android;

namespace Manosaba.EditorTools
{
    public class AndroidContentGate : IPostGenerateGradleAndroidProject
    {
        private const string GateActivity = "com.acacia.manosaba.gate.ContentGateActivity";

        /// <summary>Content manifest JSON (see BuildAndroidPlayer.BuildContentManifest); null leaves the manifest untouched.</summary>
        public static string ManifestJson;

        public int callbackOrder => 10;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            if (string.IsNullOrEmpty(ManifestJson)) return;
            string manifestPath = Path.Combine(path, "src", "main", "AndroidManifest.xml");
            string text = File.ReadAllText(manifestPath);

            // The launcher filter of the Unity activity: the first intent-filter after its android:name.
            var unity = new Regex("(<activity\\b[^>]*android:name=\"com\\.unity3d\\.player\\.UnityPlayerGameActivity\"[^>]*>)\\s*<intent-filter>.*?</intent-filter>", RegexOptions.Singleline);
            if (!unity.IsMatch(text)) throw new InvalidDataException("The Unity activity has no launcher intent-filter in " + manifestPath);
            text = unity.Replace(text, "$1", 1);

            const string gate =
                "    <activity android:name=\"" + GateActivity + "\" android:exported=\"true\" android:screenOrientation=\"sensorLandscape\"" +
                " android:theme=\"@android:style/Theme.Translucent.NoTitleBar\"" +
                " android:configChanges=\"mcc|mnc|locale|touchscreen|keyboard|keyboardHidden|navigation|orientation|screenLayout|uiMode|screenSize|smallestScreenSize|fontScale|layoutDirection|density\">\n" +
                "      <intent-filter>\n" +
                "        <category android:name=\"android.intent.category.LAUNCHER\" />\n" +
                "        <action android:name=\"android.intent.action.MAIN\" />\n" +
                "      </intent-filter>\n" +
                "    </activity>\n";
            int close = text.LastIndexOf("</application>", StringComparison.Ordinal);
            if (close < 0) throw new InvalidDataException("No </application> in " + manifestPath);
            text = text.Insert(close, gate);
            File.WriteAllText(manifestPath, text);

            string assets = Path.Combine(path, "src", "main", "assets");
            Directory.CreateDirectory(assets);
            File.WriteAllText(Path.Combine(assets, "manosaba-content.json"), ManifestJson);
        }
    }
}
#endif
