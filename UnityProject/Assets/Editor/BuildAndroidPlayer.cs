// Builds the first Android port as a development APK with an expansion (OBB) file.
//
// This entry point is deliberately separate from BuildPlayer.cs: the existing Windows
// smoke build remains the source of truth for the desktop player.  Run it with an Android
// active target, for example:
//
//   Unity -batchmode -nographics -quit -buildTarget Android -projectPath UnityProject \
//       -executeMethod Manosaba.EditorTools.BuildAndroidPlayer.Run -logFile -
//
// The method records the requested and effective Android settings in
// tools/unity-rebuild/out/unity/build_android_player.json.  It requires a successful,
// full Android Addressables build before invoking BuildPipeline: a Windows catalog or
// stale Android content is never allowed into this player.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Manosaba.EditorTools
{
    public static class BuildAndroidPlayer
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/build_android_player.json";
        private const string PlayerRelativePath = "../dist/player-android/manosaba-rebuild.apk";
        private const string ReleaseOutputRelativePath = "../tools/unity-rebuild/out/unity/build_android_player_release.json";
        private const string ReleasePlayerRelativePath = "../dist/player-android-release/manosaba-rebuild.apk";
        private const string AndroidContentReceiptRelativePath = "../tools/unity-rebuild/out/unity/g3_android_content_build.json";
        private const string ReceiptJsonCheckRelativePath = "../tools/unity-rebuild/out/unity/android_content_receipt_json_check.json";
        private const string AndroidContentRelativePath = "Library/com.unity.addressables/aa/Android";
        private const string PackageIdentifier = "com.acacia.manosaba";
        private const string BootScenePath = "Assets/Scenes/Boot.unity";
        // Bundles set aside next to the content root while the player is built (see StashBundles).
        private const string SidecarStashSuffix = ".bundles-sidecar";
        private const string SidecarManifestRelativePath = "../tools/unity-rebuild/out/unity/android_content_manifest.json";
        // Unity's Android expansion-file documentation states that each OBB may be up to 2 GB.
        private const long MaxObbBytes = 2L * 1024L * 1024L * 1024L;

        [MenuItem("Manosaba/Build Android Player (debug APK + OBB)")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        [MenuItem("Manosaba/Build Android Player (release preview, local debug signature)")]
        public static void RunRelease()
        {
            int code = Execute(true);
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute() => Execute(false);

        public static int Execute(bool release)
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string receiptPath = Path.GetFullPath(Path.Combine(projectRoot, release ? ReleaseOutputRelativePath : OutputRelativePath));
            BuildOptions buildOptions = release ? BuildOptions.None : BuildOptions.Development | BuildOptions.AllowDebugging;
            bool previousDevelopment = EditorUserBuildSettings.development;
#if UNITY_ANDROID
            bool previousManifestDevelopment = AndroidDevelopmentManifest.DevelopmentBuild;
#endif
            var result = new AndroidBuildResult
            {
                generated = DateTime.UtcNow.ToString("o"),
                unity_version = Application.unityVersion,
                requested_target = BuildTarget.Android.ToString(),
                active_build_target = EditorUserBuildSettings.activeBuildTarget.ToString(),
                output_path = Path.GetFullPath(Path.Combine(projectRoot, release ? ReleasePlayerRelativePath : PlayerRelativePath)),
                build_options = buildOptions.ToString(),
                release = release,
                signing_mode = "Unity local debug keystore; preview sideload only, not a production signing identity",
                package_identifier = PackageIdentifier,
                obb_limit_bytes = MaxObbBytes,
            };

            try
            {
                result.android_module_supported = BuildPipeline.IsBuildTargetSupported(
                    BuildTargetGroup.Android, BuildTarget.Android);
                if (!result.android_module_supported)
                {
                    // Capture what Unity can report even when the module is absent.  Avoid calling
                    // SetGraphicsAPIs/BuildPipeline on an unsupported target: some Editor versions
                    // throw before they can produce the intended blocked receipt.
                    result.settings_before = CaptureSettings();
                    result.settings_after = result.settings_before;
                    result.status = "BLOCKED_NO_ANDROID_MODULE";
                    result.error = "Unity Android Build Support is not installed for this Editor.";
                    return Finish(receiptPath, result);
                }

                // These are Android-scoped settings except for the orientation flags, which are
                // Unity's project-wide interface orientation settings.  LandscapeRight is already
                // the project's default; writing it explicitly makes the first Android build
                // reproducible and disabling autorotation prevents portrait fallback on devices.
                result.settings_before = CaptureSettings();
                if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                {
                    // Do not mutate project-wide orientation or Android settings when this method
                    // is accidentally invoked while the Windows target is active.
                    result.settings_after = result.settings_before;
                    result.status = "BLOCKED_WRONG_ACTIVE_BUILD_TARGET";
                    result.error = "Run Unity with -buildTarget Android (or switch the Editor target before invoking this method).";
                    return Finish(receiptPath, result);
                }

                // The manifest callback also reads this Editor flag; apply the actual build mode temporarily.
                EditorUserBuildSettings.development = !release;
                ApplySettings(release);
                result.settings_after = CaptureSettings();

                // A previous run that died mid-build left its bundles in the stash; put them back before validating.
                RestoreStashedBundles(Path.GetFullPath(Path.Combine(projectRoot, AndroidContentRelativePath)));

                var story = SwitchStoryContentBuilder.EnsureApplied();
                result.switch_story_status = (string)story["status"];
                result.switch_story_signature = (string)story["signature"];
                result.switch_story_output_fingerprint = (string)story["output_fingerprint"];

                if (!RequireAndroidContent(projectRoot, result))
                {
                    return Finish(receiptPath, result);
                }

                string[] scenes = EditorBuildSettings.scenes
                    .Where(s => s.enabled)
                    .Select(s => s.path)
                    .ToArray();
                if (scenes.Length == 0 && File.Exists(Path.Combine(projectRoot, BootScenePath)))
                {
                    // Making a scene an Addressables entry clears its "in build" flag (the Boot scene is one in the
                    // Default Local Group), so the build list can be empty although the player still needs its first
                    // scene.  Pass it explicitly instead of rewriting the shared EditorBuildSettings.
                    scenes = new[] { BootScenePath };
                }
                result.scenes = scenes.ToList();
                if (scenes.Length == 0)
                {
                    result.status = "BLOCKED_NO_SCENES";
                    result.error = "EditorBuildSettings has no enabled scene; run ProjectBootstrap first.";
                    return Finish(receiptPath, result);
                }

                // Keep the shared generated SpriteSkin repair in the Android build path as
                // BuildPlayer does for Windows.  The recipe is tracked; GameContent itself is not.
                RestoreOriginalCurtainSkin.EnsureApplied();

                string apk = result.output_path;
                Directory.CreateDirectory(Path.GetDirectoryName(apk));
                if (File.Exists(apk)) File.Delete(apk);
                DateTime buildStartedUtc = DateTime.UtcNow;
                var options = new BuildPlayerOptions
                {
                    scenes = scenes,
                    locationPathName = apk,
                    target = BuildTarget.Android,
                    targetGroup = BuildTargetGroup.Android,
                    options = buildOptions,
                };

                // The content was built and validated against the G3 receipt above.  With the default "preferences"
                // option Unity rebuilds Addressables inside the player build; if that rebuild fails (path validation,
                // a profile change) the content folder has already been cleaned and the player ships without any
                // content.  Keep the shared setting as it was once the build is over.
                AddressableAssetSettings addressables = AddressableAssetSettingsDefaultObject.Settings;
                var previousBuildOption = addressables == null ? default : addressables.BuildAddressablesWithPlayerBuild;
                if (addressables != null)
                    addressables.BuildAddressablesWithPlayerBuild = AddressableAssetSettings.PlayerBuildOption.DoNotBuildWithPlayer;
                // The expansion file cannot hold content this large (Unity reads it as a zip with 32-bit offsets and
                // Google Play caps it at 2 GB), so such content ships beside the app: the bundles are set aside while the
                // player is built, leaving only settings.json and the catalog for the OBB, and AndroidAddressablesSidecar
                // loads them from <persistentDataPath>/aa/Android (the bundles sit in its Android subfolder), where g5_android_player_smoke.ps1 pushes them.
                string contentRoot = Path.GetFullPath(Path.Combine(projectRoot, AndroidContentRelativePath));
                bool sidecar = result.android_content_bundle_bytes > MaxObbBytes;
                result.sidecar = sidecar;
                // With the bundles outside the player the expansion file would hold only ~55 MB of engine data, and an OBB is
                // one more thing a player must put in Android/obb by hand (not possible without adb on Android 11+; a stale or
                // missing one gives "Application OBB has mismatching GUID").  Fold it into the APK instead.
                PlayerSettings.Android.splitApplicationBinary = !sidecar;
                result.settings_after = CaptureSettings();
                if (sidecar)
                {
                    result.sidecar_source = contentRoot;
                    result.sidecar_bundles = result.android_content_bundles;
                    result.sidecar_bytes = result.android_content_bundle_bytes;
                    result.sidecar_device_dir = "/sdcard/Android/data/" + PackageIdentifier + "/files/aa/Android";
                    result.sidecar_manifest = Path.GetFullPath(Path.Combine(projectRoot, SidecarManifestRelativePath));
                    result.sidecar_build_id = WriteContentManifest(contentRoot, result.sidecar_manifest);
                }
                else
                {
#if UNITY_ANDROID
                    AndroidContentGate.ManifestJson = null;
#endif
                }
#if UNITY_ANDROID
                // Gradle callbacks are compiled only for Android; keep this Editor menu available on other targets.
                AndroidDevelopmentManifest.DevelopmentBuild = (options.options & BuildOptions.Development) != 0;
#endif
                BuildReport report;
                try
                {
                    if (sidecar) StashBundles(contentRoot);
                    report = BuildPipeline.BuildPlayer(options);
                }
                finally
                {
                    if (sidecar) RestoreStashedBundles(contentRoot);
                    if (addressables != null) addressables.BuildAddressablesWithPlayerBuild = previousBuildOption;
                }
                result.report_result = report.summary.result.ToString();
                result.report_error = report.summary.result == BuildResult.Succeeded ? null : report.summary.ToString();
                result.total_size_bytes = (long)report.summary.totalSize;
                result.total_time_seconds = Math.Round(report.summary.totalTime.TotalSeconds, 2);
                result.output_path = report.summary.outputPath;
                result.status = report.summary.result == BuildResult.Succeeded ? "SUCCEEDED" : "FAILED";
                result.apk_exists = File.Exists(apk);
                // The flag is read back from the generated manifest, not assumed from the setting that should have produced it.
                string generatedManifest = Path.Combine(projectRoot, "Library/Bee/Android/Prj/IL2CPP/Gradle/unityLibrary/src/main/AndroidManifest.xml");
                result.show_when_locked = File.Exists(generatedManifest) && File.ReadAllText(generatedManifest).Contains("android:showWhenLocked=\"true\"");
                result.obb_expected = PlayerSettings.Android.splitApplicationBinary;
                CollectArtifacts(result, Path.GetDirectoryName(apk), buildStartedUtc, NewestContentWriteUtc(projectRoot));
                result.apk_fresh = result.artifacts.Any(a => a.extension == ".apk" && a.fresh && a.bytes > 0 &&
                    Path.GetFullPath(a.path).Equals(Path.GetFullPath(apk), StringComparison.OrdinalIgnoreCase));
                result.obb_exists = result.artifacts.Any(a => a.extension == ".obb" && a.fresh && a.bytes > 0);
                ArtifactRecord freshObb = result.artifacts.FirstOrDefault(a => a.extension == ".obb" && a.fresh && a.bytes > 0);
                result.obb_bytes = freshObb == null ? 0 : freshObb.bytes;
                if (result.status == "SUCCEEDED" && (!result.apk_fresh || (result.obb_expected && !result.obb_exists)))
                {
                    result.status = "FAILED_MISSING_FRESH_ARTIFACTS";
                    result.error = "Build succeeded but did not produce both a fresh APK and a fresh OBB.";
                }
                else if (result.status == "SUCCEEDED" && result.obb_expected && result.obb_bytes > MaxObbBytes)
                {
                    result.status = "FAILED_OBB_TOO_LARGE";
                    result.error = "Fresh OBB exceeds Unity/Android's 2 GB expansion-file limit.";
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[BuildAndroidPlayer] " + ex);
            }

            finally
            {
                EditorUserBuildSettings.development = previousDevelopment;
#if UNITY_ANDROID
                AndroidDevelopmentManifest.DevelopmentBuild = previousManifestDevelopment;
#endif
            }

            return Finish(receiptPath, result);
        }

        private static void ApplySettings(bool release)
        {
            NamedBuildTarget android = NamedBuildTarget.FromBuildTargetGroup(BuildTargetGroup.Android);
            PlayerSettings.SetApplicationIdentifier(android, PackageIdentifier);
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
            PlayerSettings.Android.targetSdkVersion = RequestedTargetSdk();
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.Android.splitApplicationBinary = true;
            PlayerSettings.Android.startInFullscreen = true;
            PlayerSettings.Android.useCustomKeystore = false;

            // Android's first port intentionally uses the broadly supported GLES3 path.
            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.OpenGLES3 });

            // IL2CPP is required for the ARM64 Android player.  Keep the setting target-scoped.
            PlayerSettings.SetScriptingBackend(android, ScriptingImplementation.IL2CPP);

            // Both modes use the existing local debug signing identity for sideload previews.
            // Release removes the game's DebugPresenter/UI; it is not a production signing setup.
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.androidBuildType = release ? AndroidBuildType.Release : AndroidBuildType.Debug;
            EditorUserBuildSettings.buildAppBundle = false;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;

            // Landscape only (the UI is laid out for 16:9), but both landscape directions follow the sensor, so flipping the
            // phone 180 degrees works; Unity writes android:screenOrientation="sensorLandscape" for this combination.
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;
        }

        /// <summary>
        /// Target API level: Android 15 (35) unless MANOSABA_ANDROID_TARGET_SDK names another level, e.g. 36 for Android 16.
        /// minSdk stays 29 (Android 10), so the same build runs on every release from 10 up to the target.
        /// </summary>
        private static AndroidSdkVersions RequestedTargetSdk()
        {
            string requested = Environment.GetEnvironmentVariable("MANOSABA_ANDROID_TARGET_SDK");
            if (string.IsNullOrWhiteSpace(requested)) return AndroidSdkVersions.AndroidApiLevel35;
            if (!int.TryParse(requested, out int level) || level < (int)PlayerSettings.Android.minSdkVersion ||
                !Enum.IsDefined(typeof(AndroidSdkVersions), level))
                throw new ArgumentException("MANOSABA_ANDROID_TARGET_SDK must be an Android API level of at least minSdk and known to this Unity; got '" + requested + "'.");
            return (AndroidSdkVersions)level;
        }

        private static AndroidSettingsSnapshot CaptureSettings()
        {
            var snapshot = new AndroidSettingsSnapshot
            {
                application_identifier = "unknown",
                min_sdk = PlayerSettings.Android.minSdkVersion.ToString(),
                target_sdk = PlayerSettings.Android.targetSdkVersion.ToString(),
                architectures = PlayerSettings.Android.targetArchitectures.ToString(),
                split_application_binary = PlayerSettings.Android.splitApplicationBinary,
                default_interface_orientation = PlayerSettings.defaultInterfaceOrientation.ToString(),
                autorotate_portrait = PlayerSettings.allowedAutorotateToPortrait,
                autorotate_portrait_upside_down = PlayerSettings.allowedAutorotateToPortraitUpsideDown,
                autorotate_landscape_left = PlayerSettings.allowedAutorotateToLandscapeLeft,
                autorotate_landscape_right = PlayerSettings.allowedAutorotateToLandscapeRight,
                use_custom_keystore = PlayerSettings.Android.useCustomKeystore,
                scripting_backend = "unknown",
                graphics_apis = new List<string>(),
                use_default_graphics_apis = true,
                android_build_system = EditorUserBuildSettings.androidBuildSystem.ToString(),
                android_build_type = EditorUserBuildSettings.androidBuildType.ToString(),
                build_app_bundle = EditorUserBuildSettings.buildAppBundle,
            };
            try
            {
                NamedBuildTarget android = NamedBuildTarget.FromBuildTargetGroup(BuildTargetGroup.Android);
                snapshot.application_identifier = PlayerSettings.GetApplicationIdentifier(android);
                snapshot.scripting_backend = PlayerSettings.GetScriptingBackend(android).ToString();
            }
            catch (Exception ex) { snapshot.scripting_backend_error = ex.GetType().Name + ": " + ex.Message; }
            try
            {
                snapshot.use_default_graphics_apis = PlayerSettings.GetUseDefaultGraphicsAPIs(BuildTarget.Android);
                snapshot.graphics_apis = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android)
                    .Select(api => api.ToString()).ToList();
            }
            catch (Exception ex) { snapshot.graphics_apis_error = ex.GetType().Name + ": " + ex.Message; }
            return snapshot;
        }

        private static bool RequireAndroidContent(string projectRoot, AndroidBuildResult result)
        {
            string receiptPath = Path.GetFullPath(Path.Combine(projectRoot, AndroidContentReceiptRelativePath));
            string expectedRoot = Path.GetFullPath(Path.Combine(projectRoot, AndroidContentRelativePath));
            result.android_content_receipt_path = receiptPath;
            result.android_content_expected_root = expectedRoot;
            if (!File.Exists(receiptPath))
            {
                result.status = "BLOCKED_NO_ANDROID_CONTENT_RECEIPT";
                result.error = "Missing full Android G3 receipt; run g3_android_content_build.ps1 -Group all first.";
                return false;
            }

            try
            {
                AndroidContentReceipt receipt = ReadAndroidContentReceipt(File.ReadAllText(receiptPath), out JObject receiptDocument);
                result.android_content_status = receipt == null ? null : receipt.status;
                result.android_content_group = receipt == null ? null : receipt.group;
                result.android_content_staging = receipt == null ? null : receipt.staging;
                string reportedRoot = receipt == null || receipt.staging == null ? null : receipt.staging.player_streaming_source;
                bool rootMatches = !string.IsNullOrEmpty(reportedRoot) &&
                    Path.GetFullPath(reportedRoot).Equals(expectedRoot, StringComparison.OrdinalIgnoreCase);
                bool receiptPass = receipt != null && receipt.status == "PASS" && receipt.group == "all" &&
                    receipt.staging != null && receipt.staging.android_build_root && rootMatches;
                if (!receiptPass)
                {
                    result.status = "BLOCKED_ANDROID_CONTENT_NOT_FULL_PASS";
                    result.error = "Android player requires a PASS receipt for G3 group=all with the expected Android content root.";
                    return false;
                }

                if (result.switch_story_status == "OK")
                {
                    var built = receiptDocument["build"]?["result"];
                    if ((string)built?["switch_story_status"] != "OK" ||
                        (string)built?["switch_story_signature"] != result.switch_story_signature ||
                        (string)built?["switch_story_output_fingerprint"] != result.switch_story_output_fingerprint)
                    {
                        result.status = "BLOCKED_ANDROID_STORY_CONTENT_STALE";
                        result.error = "Switch story assets no longer match the full Android G3 build; rebuild Android content before packaging the player.";
                        return false;
                    }
                }

                string[] files = Directory.Exists(expectedRoot)
                    ? Directory.GetFiles(expectedRoot, "*", SearchOption.AllDirectories)
                    : Array.Empty<string>();
                string[] catalogs = files.Where(IsCatalog).ToArray();
                string[] bundles = files.Where(p => Path.GetExtension(p).Equals(".bundle", StringComparison.OrdinalIgnoreCase)).ToArray();
                long contentBytes = files.Sum(p => new FileInfo(p).Length);
                long bundleBytes = bundles.Sum(p => new FileInfo(p).Length);
                result.android_content_files = files.Length;
                result.android_content_bytes = contentBytes;
                result.android_content_catalogs = catalogs.Length;
                result.android_content_bundles = bundles.Length;
                result.android_content_bundle_bytes = bundleBytes;
                bool contentMatchesReceipt = files.Length > 0 && files.Length == receipt.staging.files &&
                    contentBytes == receipt.staging.bytes && catalogs.Length == receipt.staging.catalogs &&
                    catalogs.Any(p => new FileInfo(p).Length > 0) &&
                    bundles.Any(p => new FileInfo(p).Length > 0) &&
                    receipt.staging.catalog_fresh &&
                    receipt.staging.catalog_matches_build_receipt &&
                    receipt.staging.bundle_count_matches_build_receipt &&
                    receipt.staging.bundle_bytes_matches_build_receipt &&
                    bundles.Length == receipt.staging.bundles && bundleBytes == receipt.staging.bundle_bytes;
                if (!contentMatchesReceipt)
                {
                    result.status = "BLOCKED_ANDROID_CONTENT_MISSING_OR_STALE";
                    result.error = "G3 passed but the expected Android catalog/bundles are missing or no longer match its receipt.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                result.status = "BLOCKED_ANDROID_CONTENT_RECEIPT_INVALID";
                result.error = "Unable to validate the Android G3 receipt/content: " + ex;
                return false;
            }
        }

        // PowerShell 7 can emit Measure-Object totals as decimal JSON tokens (for example 4444884778.0).
        // JsonUtility truncates these to 32 bits even for long fields; read this local G3 receipt with Newtonsoft.
        private static AndroidContentReceipt ReadAndroidContentReceipt(string json, out JObject document)
        {
            using (var textReader = new StringReader(json))
            using (var reader = new JsonTextReader(textReader)
            {
                FloatParseHandling = FloatParseHandling.Decimal,
                DateParseHandling = DateParseHandling.None
            })
            {
                document = JObject.Load(reader, new JsonLoadSettings
                {
                    DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error
                });
                if (reader.Read()) throw new InvalidDataException("Unexpected data after the Android G3 receipt.");
            }

            var staging = document["staging"] as JObject;
            if (staging == null) throw new InvalidDataException("Android G3 receipt requires a staging object.");
            ValidateReceiptCount(staging, "files", int.MaxValue);
            ValidateReceiptCount(staging, "catalogs", int.MaxValue);
            ValidateReceiptCount(staging, "bundles", int.MaxValue);
            ValidateReceiptCount(staging, "bytes", long.MaxValue);
            ValidateReceiptCount(staging, "bundle_bytes", long.MaxValue);
            return document.ToObject<AndroidContentReceipt>(JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            }));
        }

        private static void ValidateReceiptCount(JObject staging, string name, decimal maximum)
        {
            JToken token = staging[name];
            if (token == null || (token.Type != JTokenType.Integer && token.Type != JTokenType.Float))
                throw new InvalidDataException("Android G3 staging." + name + " must be a numeric integer.");
            decimal value = token.Value<decimal>();
            if (value < 0 || value > maximum || value != decimal.Truncate(value))
                throw new InvalidDataException("Android G3 staging." + name + " is outside its non-negative integer range.");
        }

        [MenuItem("Manosaba/Verify Android content receipt JSON")]
        public static void VerifyAndroidContentReceiptJson()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string output = Path.GetFullPath(Path.Combine(projectRoot, ReceiptJsonCheckRelativePath));
            var cases = new JArray();
            var report = new JObject
            {
                ["generated"] = DateTime.UtcNow.ToString("o"),
                ["unity_version"] = Application.unityVersion,
                ["status"] = "FAIL",
                ["scope"] = "Local G3 numeric decoding and receipt-to-disk gate; no player build or Story freshness test.",
                ["cases"] = cases
            };
            bool passed = true;
            try
            {
                void Check(string name, string json, bool reject, long expectedBytes = 4444884778L,
                    long expectedBundleBytes = 4439250123L, int expectedFiles = 342)
                {
                    AndroidContentReceipt receipt = null;
                    Exception error = null;
                    try { receipt = ReadAndroidContentReceipt(json, out _); }
                    catch (Exception ex) { error = ex; }
                    bool ok = reject
                        ? error is InvalidDataException || error is JsonException || error is OverflowException
                        : error == null && receipt.staging.files == expectedFiles && receipt.staging.catalogs == 1 &&
                          receipt.staging.bundles == 338 && receipt.staging.bytes == expectedBytes &&
                          receipt.staging.bundle_bytes == expectedBundleBytes;
                    cases.Add(new JObject
                    {
                        ["name"] = name,
                        ["passed"] = ok,
                        ["expected_rejection"] = reject,
                        ["files"] = receipt?.staging?.files,
                        ["bytes"] = receipt?.staging?.bytes,
                        ["bundle_bytes"] = receipt?.staging?.bundle_bytes,
                        ["error"] = error == null ? null : error.GetType().Name + ": " + error.Message
                    });
                    passed &= ok;
                }

                string Fixture(string bytes, string bundleBytes = "4439250123.0", string files = "342") =>
                    "{\"status\":\"PASS\",\"group\":\"all\",\"staging\":{\"files\":" + files +
                    ",\"catalogs\":1,\"bundles\":338,\"bytes\":" + bytes + ",\"bundle_bytes\":" + bundleBytes + "}}";
                Check("small_integer_counts", Fixture("123", "100"), false, 123, 100);
                Check("powershell7_actual_decimal_totals", Fixture("4444884778.0"), false);
                Check("int_and_long_max_decimal", Fixture("9223372036854775807.0", "9223372036854775807.0", "2147483647.0"),
                    false, long.MaxValue, long.MaxValue, int.MaxValue);
                Check("zero_decimal_bytes", Fixture("0.0", "0.0"), false, 0, 0);
                Check("fractional_bytes", Fixture("4444884778.5"), true);
                Check("fractional_file_count", Fixture("4444884778.0", files: "342.5"), true);
                Check("negative_bytes", Fixture("-1"), true);
                Check("long_overflow", Fixture("9223372036854775808.0"), true);
                Check("int_overflow", Fixture("4444884778.0", files: "2147483648"), true);
                Check("numeric_string", Fixture("\"4444884778\""), true);
                Check("null_bytes", Fixture("null"), true);
                Check("boolean_bytes", Fixture("true"), true);
                Check("missing_bytes", Fixture("4444884778.0").Replace("\"bytes\":4444884778.0,", ""), true);
                Check("duplicate_bytes", Fixture("4444884778.0").Replace("\"bytes\":", "\"bytes\":1,\"bytes\":"), true);
                Check("additional_root", Fixture("4444884778.0") + " {}", true);

                var gate = new AndroidBuildResult();
                bool diskGate = RequireAndroidContent(projectRoot, gate);
                passed &= diskGate;
                report["receipt_to_disk_gate"] = new JObject
                {
                    ["passed"] = diskGate,
                    ["result"] = JObject.FromObject(gate)
                };
                report["status"] = passed ? "PASS" : "FAIL";
            }
            catch (Exception ex)
            {
                passed = false;
                report["error"] = ex.ToString();
            }
            Directory.CreateDirectory(Path.GetDirectoryName(output));
            File.WriteAllText(output, report.ToString(Formatting.Indented), new UTF8Encoding(false));
            Debug.Log("[BuildAndroidPlayer] receipt JSON check " + report["status"] + " report=" + output);
            if (Application.isBatchMode) EditorApplication.Exit(passed ? 0 : 1);
        }

        /// <summary>
        /// Lists the bundles the player needs (name and size) for the content gate and for package_android_content.py, which cuts
        /// the zip parts from the same list.  The build id is a hash of that list, so a pack made for another build cannot be
        /// mistaken for this one.  Returns the build id and hands the JSON to the manifest post-processor.
        /// </summary>
        private static string WriteContentManifest(string contentRoot, string manifestPath)
        {
            string[] bundles = Directory.GetFiles(contentRoot, "*.bundle", SearchOption.AllDirectories)
                .OrderBy(path => Path.GetFileName(path), StringComparer.Ordinal).ToArray();
            var listing = new StringBuilder();
            foreach (string bundle in bundles)
                listing.Append(Path.GetFileName(bundle)).Append('\t').Append(new FileInfo(bundle).Length).Append('\n');
            string buildId;
            using (var sha = SHA256.Create())
                buildId = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(listing.ToString()))).Replace("-", "").Substring(0, 12).ToLowerInvariant();

            var json = new StringBuilder();
            json.Append("{\"buildId\":\"").Append(buildId).Append("\",\"dir\":\"aa/Android/Android\",\"files\":[");
            for (int i = 0; i < bundles.Length; i++)
            {
                if (i > 0) json.Append(',');
                json.Append("{\"n\":\"").Append(Path.GetFileName(bundles[i])).Append("\",\"s\":").Append(new FileInfo(bundles[i]).Length).Append('}');
            }
            json.Append("]}");
            Directory.CreateDirectory(Path.GetDirectoryName(manifestPath));
            File.WriteAllText(manifestPath, json.ToString(), new UTF8Encoding(false));
#if UNITY_ANDROID
            AndroidContentGate.ManifestJson = json.ToString();
#endif
            return buildId;
        }

        private static void StashBundles(string contentRoot)
        {
            string stash = contentRoot + SidecarStashSuffix;
            foreach (string bundle in Directory.GetFiles(contentRoot, "*.bundle", SearchOption.AllDirectories))
            {
                string target = Path.Combine(stash, Path.GetRelativePath(contentRoot, bundle));
                Directory.CreateDirectory(Path.GetDirectoryName(target));
                File.Move(bundle, target);
            }
        }

        private static void RestoreStashedBundles(string contentRoot)
        {
            string stash = contentRoot + SidecarStashSuffix;
            if (!Directory.Exists(stash)) return;
            foreach (string bundle in Directory.GetFiles(stash, "*", SearchOption.AllDirectories))
            {
                string target = Path.Combine(contentRoot, Path.GetRelativePath(stash, bundle));
                Directory.CreateDirectory(Path.GetDirectoryName(target));
                File.Move(bundle, target);
            }
            Directory.Delete(stash, true);
        }

        private static bool IsCatalog(string path)
        {
            string name = Path.GetFileName(path);
            string extension = Path.GetExtension(path);
            return name.StartsWith("catalog", StringComparison.OrdinalIgnoreCase) &&
                (extension.Equals(".json", StringComparison.OrdinalIgnoreCase) ||
                 extension.Equals(".bin", StringComparison.OrdinalIgnoreCase));
        }

        private static DateTime NewestContentWriteUtc(string projectRoot)
        {
            string root = Path.GetFullPath(Path.Combine(projectRoot, AndroidContentRelativePath));
            return Directory.Exists(root)
                ? Directory.GetFiles(root, "*", SearchOption.AllDirectories).Select(File.GetLastWriteTimeUtc).DefaultIfEmpty(DateTime.MinValue).Max()
                : DateTime.MinValue;
        }

        private static void CollectArtifacts(AndroidBuildResult result, string outputDirectory, DateTime startedUtc, DateTime contentNewestUtc)
        {
            if (!Directory.Exists(outputDirectory)) return;
            foreach (string path in Directory.GetFiles(outputDirectory, "*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(path).ToLowerInvariant();
                if (extension != ".apk" && extension != ".obb") continue;
                var info = new FileInfo(path);
                // Unity keeps an up-to-date OBB between builds (7-Zip only reruns when the packaged assets changed), so an OBB
                // written after the newest Android content file is as current as one written during this build.
                bool fresh = info.LastWriteTimeUtc >= startedUtc ||
                             (extension == ".obb" && info.LastWriteTimeUtc >= contentNewestUtc);
                result.artifacts.Add(new ArtifactRecord
                {
                    path = path,
                    extension = extension,
                    bytes = info.Length,
                    fresh = fresh,
                });
            }
        }

        private static int Finish(string receiptPath, AndroidBuildResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(receiptPath));
            File.WriteAllText(receiptPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            Debug.Log("[BuildAndroidPlayer] " + result.status + " apk=" + result.output_path +
                      " module=" + result.android_module_supported + " obb=" + result.obb_exists +
                      " receipt=" + receiptPath);
            if (result.status == "SUCCEEDED") return 0;
            return result.status.StartsWith("BLOCKED_", StringComparison.Ordinal) ? 2 : 1;
        }

        [Serializable]
        public class AndroidSettingsSnapshot
        {
            public string application_identifier;
            public string min_sdk;
            public string target_sdk;
            public string architectures;
            public bool split_application_binary;
            public string default_interface_orientation;
            public bool autorotate_portrait;
            public bool autorotate_portrait_upside_down;
            public bool autorotate_landscape_left;
            public bool autorotate_landscape_right;
            public bool use_custom_keystore;
            public string scripting_backend;
            public string scripting_backend_error;
            public List<string> graphics_apis = new List<string>();
            public bool use_default_graphics_apis;
            public string graphics_apis_error;
            public string android_build_system;
            public string android_build_type;
            public bool build_app_bundle;
        }

        [Serializable]
        public class ArtifactRecord
        {
            public string path;
            public string extension;
            public long bytes;
            public bool fresh;
        }

        [Serializable]
        public class AndroidContentStaging
        {
            public string player_streaming_source;
            public bool android_build_root;
            public int files;
            public int catalogs;
            public bool catalog_fresh;
            public int bundles;
            public long bytes;
            public long bundle_bytes;
            public bool catalog_matches_build_receipt;
            public bool bundle_count_matches_build_receipt;
            public bool bundle_bytes_matches_build_receipt;
        }

        [Serializable]
        public class AndroidContentReceipt
        {
            public string status;
            public string group;
            public AndroidContentStaging staging;
        }

        [Serializable]
        public class AndroidBuildResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string unity_version;
            public string requested_target;
            public string active_build_target;
            public bool android_module_supported;
            public string error;
            public string report_result;
            public string report_error;
            public string output_path;
            public string build_options;
            public bool release;
            public string signing_mode;
            public string package_identifier;
            public long obb_limit_bytes;
            public bool apk_exists;
            public bool apk_fresh;
            public bool obb_expected;
            public bool obb_exists;
            public long obb_bytes;
            public string android_content_receipt_path;
            public string android_content_expected_root;
            public string android_content_status;
            public string android_content_group;
            public AndroidContentStaging android_content_staging;
            public int android_content_files;
            public long android_content_bytes;
            public int android_content_catalogs;
            public int android_content_bundles;
            public long android_content_bundle_bytes;
            public string switch_story_status;
            public string switch_story_signature;
            public string switch_story_output_fingerprint;
            // AndroidDevelopmentManifest lets Development players run over the lock screen.
            public bool show_when_locked;
            public bool sidecar;
            public string sidecar_source;
            public int sidecar_bundles;
            public long sidecar_bytes;
            public string sidecar_device_dir;
            public string sidecar_manifest;
            public string sidecar_build_id;
            public long total_size_bytes;
            public double total_time_seconds;
            public List<string> scenes = new List<string>();
            public List<ArtifactRecord> artifacts = new List<ArtifactRecord>();
            public AndroidSettingsSnapshot settings_before;
            public AndroidSettingsSnapshot settings_after;
        }
    }
}
