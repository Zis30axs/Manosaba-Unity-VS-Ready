// Android: the Addressables bundles ship beside the app instead of inside the expansion file.
//
// Unity reads the OBB/APK as a zip with 32-bit offsets (Google Play also caps an expansion file at 2 GB), and the Android
// content is about 4.3 GB: a player whose OBB held it logged "Unknown compression method" for every entry beyond the first
// 4 GiB and never loaded anything.  BuildAndroidPlayer therefore keeps only settings.json and the catalog in the OBB and
// tools/unity-rebuild/g5_android_player_smoke.ps1 pushes the bundles to <persistentDataPath>/aa/Android.  This hook points
// every bundle location at that copy when it exists; without the folder the player behaves as before.
#if UNITY_ANDROID && !UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Manosaba.Integration
{
    public static class AndroidAddressablesSidecar
    {
        public const string RelativeRoot = "aa/Android";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Install()
        {
            string root = Path.Combine(Application.persistentDataPath, RelativeRoot);
            if (!Directory.Exists(root))
            {
                Debug.Log("[AndroidAddressablesSidecar] no sidecar folder at " + root + "; bundles load from the player.");
                return;
            }

            // Addressables writes the bundles to <BuildPath>/<BuildTarget>, i.e. aa/Android/Android/*.bundle, and the whole tree is
            // pushed; file names are unique, so index them wherever they sit.
            var bundles = new Dictionary<string, string>();
            foreach (string path in Directory.GetFiles(root, "*.bundle", SearchOption.AllDirectories)) bundles[Path.GetFileName(path)] = path;
            Debug.Log("[AndroidAddressablesSidecar] " + bundles.Count + " bundles in " + root);

            Addressables.InternalIdTransformFunc = location =>
            {
                string id = location.InternalId;
                if (id != null && id.EndsWith(".bundle"))
                {
                    if (bundles.TryGetValue(Path.GetFileName(id), out string local)) return local;
                }
                return id;
            };
        }
    }
}
#endif
