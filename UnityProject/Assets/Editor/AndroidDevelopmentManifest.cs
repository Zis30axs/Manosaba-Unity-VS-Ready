// Development players are allowed to show over the lock screen.
//
// An unattended device run (tools/unity-rebuild/g5_android_player_smoke.ps1) starts the game while the phone is locked; a
// normal activity is then paused before its first frame and killed a few seconds later, so nothing about the game is
// learned.  android:showWhenLocked / turnScreenOn let the Development player run on top of the keyguard.  Release players are
// left untouched.
#if UNITY_ANDROID
using System.IO;
using UnityEditor;
using UnityEditor.Android;

namespace Manosaba.EditorTools
{
    public class AndroidDevelopmentManifest : IPostGenerateGradleAndroidProject
    {
        private const string Activity = "android:name=\"com.unity3d.player.UnityPlayerGameActivity\"";

        /// <summary>
        /// Set by BuildAndroidPlayer for the build it starts: that entry point passes BuildOptions.Development to
        /// BuildPlayer, which does not touch EditorUserBuildSettings.development, so the editor checkbox cannot be trusted here.
        /// </summary>
        public static bool DevelopmentBuild;

        public int callbackOrder => 0;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            if (!DevelopmentBuild && !EditorUserBuildSettings.development) return;
            string manifest = Path.Combine(path, "src", "main", "AndroidManifest.xml");
            string text = File.ReadAllText(manifest);
            if (!text.Contains(Activity) || text.Contains("android:showWhenLocked")) return;
            File.WriteAllText(manifest, text.Replace(Activity, Activity + " android:showWhenLocked=\"true\" android:turnScreenOn=\"true\""));
        }
    }
}
#endif
