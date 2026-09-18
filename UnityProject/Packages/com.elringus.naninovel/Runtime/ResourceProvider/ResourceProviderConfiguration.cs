using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class ResourceProviderConfiguration : Configuration
    {
        /// <summary>
        /// Assembly-qualified type name of the built-in project resource provider.
        /// </summary>
        public const string ProjectTypeName = "Naninovel.ProjectResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        /// <summary>
        /// Assembly-qualified type name of the built-in local resource provider.
        /// </summary>
        public const string LocalTypeName = "Naninovel.LocalResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        /// <summary>
        /// Assembly-qualified type name of the built-in Google Drive resource provider.
        /// </summary>
        public const string GoogleDriveTypeName = "Naninovel.GoogleDriveResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        /// <summary>
        /// Assembly-qualified type name of the built-in virtual resource provider.
        /// </summary>
        public const string VirtualTypeName = "Naninovel.VirtualResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        /// <summary>
        /// Assembly-qualified type name of the built-in addressable resource provider.
        /// </summary>
        public const string AddressableTypeName = "Naninovel.AddressableResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        /// <summary>
        /// Unique identifier (group name, address prefix, label) used with assets managed by the Naninovel resource provider.
        /// </summary>
        public const string AddressableId = "Naninovel";
        /// <summary>
        /// Assigned from the editor assembly when the application is running under Unity editor.
        /// </summary>
        public static IResourceProvider EditorProvider = default;

        /// <summary>
        /// Used by the <see cref="IResourceProviderManager"/> before all the other providers.
        /// </summary>
        public virtual IResourceProvider MasterProvider => EditorProvider;

        [Header("Resources Management")]
        [Tooltip("Dictates when the resources are loaded and unloaded during script execution:" +
                 "\n • Conservative — The default mode with balanced memory utilization. All the resources required for script execution are preloaded when starting the playback and unloaded when the script has finished playing. Scripts referenced in [@gosub] commands are preloaded as well. Additional scripts can be preloaded by using `hold` parameter of [@goto] command." +
                 "\n • Optimistic — All the resources required by the played script, as well all resources of all the scripts specified in [@goto] and [@gosub] commands are preloaded and not unloaded unless `release` parameter is specified in [@goto] command. This minimizes loading screens and allows smooth rollback, but requires manually specifying when the resources have to be unloaded, increasing risk of out of memory exceptions.")]
        public ResourcePolicy ResourcePolicy = ResourcePolicy.Conservative;
        [Tooltip("Whether to automatically remove unused actors (characters, backgrounds, text printers and choice handlers) when unloading script resources. Note, that even when enabled, it's still possible to remove actors manually with `@remove` commands at any time.")]
        public bool RemoveActors = true;
        [Tooltip("Whether to log resource un-/loading operations.")]
        public bool LogResourceLoading;

        [Header("Build Processing")]
        [Tooltip("Whether to register a custom build player handle to process the assets assigned as Naninovel resources.\n\nWarning: In order for this setting to take effect, it's required to restart the Unity editor.")]
        public bool EnableBuildProcessing = true;
        [Tooltip("When the Addressable Asset System is installed, enabling this property will optimize asset processing step improving the build time.")]
        public bool UseAddressables = true;
        [Tooltip("Whether to automatically build the addressable asset bundles when building the player. Has no effect when `Use Addressables` is disabled.")]
        public bool AutoBuildBundles = true;

        [Header("Addressable Provider")]
        [Tooltip("Whether to use addressable provider in editor. Enable if you're manually exposing resources via addressable address instead of assigning them with Naninovel's resource managers. Be aware, that enabling this could cause issues when resources are assigned both in resources manager and registered with an addressable address and then renamed or duplicated.")]
        public bool AllowAddressableInEditor;
        [Tooltip("Whether to label all the Naninovel addressable assets by the scenario script path they're used in. When `Bundle Mode` is set to `Pack Together By Label` in the addressable group settings, will result in a more efficient bundle packing.\n\nNote that script labels will be assigned to all the assets with 'Naninovel' label, which includes assets manually exposed to the addressable resource provider (w/o using the resource editor menus).")]
        public bool LabelByScripts = true;
        [Tooltip("Whether to create an addressable group per Naninovel resource category: scripts, characters, audio, etc. When disabled, will use single `Naninovel` group for all the resources.")]
        public bool GroupByCategory;
        [Tooltip("Addressable provider will only work with assets, that have the assigned labels in addition to `Naninovel` label. Can be used to filter assets used by the engine based on custom criteria (eg, HD vs SD textures).")]
        public string[] ExtraLabels;

        [Header("Local Provider")]
        [Tooltip("Path root to use for the local resource provider. Can be an absolute path to the folder where the resources are located, or a relative path with one of the available origins:" +
                 "\n • %DATA% — Game data folder on the target device (UnityEngine.Application.dataPath)." +
                 "\n • %PDATA% — Persistent data directory on the target device (UnityEngine.Application.persistentDataPath)." +
                 "\n • %STREAM% — `StreamingAssets` folder (UnityEngine.Application.streamingAssetsPath)." +
                 "\n • %SPECIAL{F}% — An OS special folder (where F is value from System.Environment.SpecialFolder).")]
        public string LocalRootPath = "%DATA%/Resources";
        [Tooltip("When streaming videos under WebGL (movies, video backgrounds), specify the extension of the video files.")]
        public string VideoStreamExtension = ".mp4";

        [Header("Project Provider")]
        [Tooltip("Path relative to `Resources` folders, under which the naninovel-specific assets are located.")]
        public string ProjectRootPath = "Naninovel";

        #if UNITY_GOOGLE_DRIVE_AVAILABLE
        [Header("Google Drive Provider")]
        [Tooltip("Path root to use for the Google Drive resource provider.")]
        public string GoogleDriveRootPath = "Resources";
        [Tooltip("Maximum allowed concurrent requests when contacting Google Drive API.")]
        public int GoogleDriveRequestLimit = 2;
        [Tooltip("Cache policy to use when downloading resources. `Smart` will attempt to use Changes API to check for the modifications on the drive. `PurgeAllOnInit` will to re-download all the resources when the provider is initialized.")]
        public GoogleDriveResourceProvider.CachingPolicyType GoogleDriveCachingPolicy = GoogleDriveResourceProvider.CachingPolicyType.Smart;
        #endif
    }
}
