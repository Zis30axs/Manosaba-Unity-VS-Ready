using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20003C6")]
    [EditInProjectSettings]
    public class ResourceProviderConfiguration : Configuration
    {
    	[Token(Token = "0x4000E20")]
    	public const string ProjectTypeName = "Naninovel.ProjectResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

    	[Token(Token = "0x4000E21")]
    	public const string LocalTypeName = "Naninovel.LocalResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

    	[Token(Token = "0x4000E22")]
    	public const string GoogleDriveTypeName = "Naninovel.GoogleDriveResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

    	[Token(Token = "0x4000E23")]
    	public const string VirtualTypeName = "Naninovel.VirtualResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

    	[Token(Token = "0x4000E24")]
    	public const string AddressableTypeName = "Naninovel.AddressableResourceProvider, Elringus.Naninovel.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

    	[Token(Token = "0x4000E25")]
    	public const string AddressableId = "Naninovel";

    	[Token(Token = "0x4000E26")]
    	[FieldOffset(Offset = "0x0")]
    	public static IResourceProvider EditorProvider;

    	[Token(Token = "0x4000E27")]
    	[FieldOffset(Offset = "0x18")]
    	[Header("Resources Management")]
    	[Tooltip("Dictates when the resources are loaded and unloaded during script execution:\n • Conservative — The default mode with balanced memory utilization. All the resources required for script execution are preloaded when starting the playback and unloaded when the script has finished playing. Scripts referenced in [@gosub] commands are preloaded as well. Additional scripts can be preloaded by using `hold` parameter of [@goto] command.\n • Optimistic — All the resources required by the played script, as well all resources of all the scripts specified in [@goto] and [@gosub] commands are preloaded and not unloaded unless `release` parameter is specified in [@goto] command. This minimizes loading screens and allows smooth rollback, but requires manually specifying when the resources have to be unloaded, increasing risk of out of memory exceptions.")]
    	public ResourcePolicy ResourcePolicy;

    	[Token(Token = "0x4000E28")]
    	[FieldOffset(Offset = "0x1C")]
    	[Tooltip("Whether to automatically remove unused actors (characters, backgrounds, text printers and choice handlers) when unloading script resources. Note, that even when enabled, it's still possible to remove actors manually with `@remove` commands at any time.")]
    	public bool RemoveActors;

    	[Token(Token = "0x4000E29")]
    	[FieldOffset(Offset = "0x1D")]
    	[Tooltip("Whether to log resource un-/loading operations.")]
    	public bool LogResourceLoading;

    	[Token(Token = "0x4000E2A")]
    	[FieldOffset(Offset = "0x1E")]
    	[Header("Build Processing")]
    	[Tooltip("Whether to register a custom build player handle to process the assets assigned as Naninovel resources.\n\nWarning: In order for this setting to take effect, it's required to restart the Unity editor.")]
    	public bool EnableBuildProcessing;

    	[Token(Token = "0x4000E2B")]
    	[FieldOffset(Offset = "0x1F")]
    	[Tooltip("When the Addressable Asset System is installed, enabling this property will optimize asset processing step improving the build time.")]
    	public bool UseAddressables;

    	[Token(Token = "0x4000E2C")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("Whether to automatically build the addressable asset bundles when building the player. Has no effect when `Use Addressables` is disabled.")]
    	public bool AutoBuildBundles;

    	[Token(Token = "0x4000E2D")]
    	[FieldOffset(Offset = "0x21")]
    	[Header("Addressable Provider")]
    	[Tooltip("Whether to use addressable provider in editor. Enable if you're manually exposing resources via addressable address instead of assigning them with Naninovel's resource managers. Be aware, that enabling this could cause issues when resources are assigned both in resources manager and registered with an addressable address and then renamed or duplicated.")]
    	public bool AllowAddressableInEditor;

    	[Token(Token = "0x4000E2E")]
    	[FieldOffset(Offset = "0x22")]
    	[Tooltip("Whether to label all the Naninovel addressable assets by the scenario script path they're used in. When `Bundle Mode` is set to `Pack Together By Label` in the addressable group settings, will result in a more efficient bundle packing.\n\nNote that script labels will be assigned to all the assets with 'Naninovel' label, which includes assets manually exposed to the addressable resource provider (w/o using the resource editor menus).")]
    	public bool LabelByScripts;

    	[Token(Token = "0x4000E2F")]
    	[FieldOffset(Offset = "0x23")]
    	[Tooltip("Whether to create an addressable group per Naninovel resource category: scripts, characters, audio, etc. When disabled, will use single `Naninovel` group for all the resources.")]
    	public bool GroupByCategory;

    	[Token(Token = "0x4000E30")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("Addressable provider will only work with assets, that have the assigned labels in addition to `Naninovel` label. Can be used to filter assets used by the engine based on custom criteria (eg, HD vs SD textures).")]
    	public string[] ExtraLabels;

    	[Token(Token = "0x4000E31")]
    	[FieldOffset(Offset = "0x30")]
    	[Header("Local Provider")]
    	[Tooltip("Path root to use for the local resource provider. Can be an absolute path to the folder where the resources are located, or a relative path with one of the available origins:\n • %DATA% — Game data folder on the target device (UnityEngine.Application.dataPath).\n • %PDATA% — Persistent data directory on the target device (UnityEngine.Application.persistentDataPath).\n • %STREAM% — `StreamingAssets` folder (UnityEngine.Application.streamingAssetsPath).\n • %SPECIAL{F}% — An OS special folder (where F is value from System.Environment.SpecialFolder).")]
    	public string LocalRootPath;

    	[Token(Token = "0x4000E32")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("When streaming videos under WebGL (movies, video backgrounds), specify the extension of the video files.")]
    	public string VideoStreamExtension;

    	[Token(Token = "0x4000E33")]
    	[FieldOffset(Offset = "0x40")]
    	[Header("Project Provider")]
    	[Tooltip("Path relative to `Resources` folders, under which the naninovel-specific assets are located.")]
    	public string ProjectRootPath;

    	[Token(Token = "0x17000276")]
    	public virtual IResourceProvider MasterProvider
    	{
    		[Token(Token = "0x600123F")]
    		[Address(RVA = "0x59B7A0", Offset = "0x59A1A0", VA = "0x18059B7A0", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001240")]
    	[Address(RVA = "0x59B7E0", Offset = "0x59A1E0", VA = "0x18059B7E0")]
    	public ResourceProviderConfiguration()
    	{
    	}
    }
}
