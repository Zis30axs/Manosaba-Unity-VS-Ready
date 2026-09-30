using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UIElements;

namespace Naninovel
{
    [Token(Token = "0x2000425")]
    [EditInProjectSettings]
    public class ScriptsConfiguration : Configuration
    {
    	[Token(Token = "0x2000426")]
    	public enum GraphOrientationType
    	{
    		[Token(Token = "0x4000F49")]
    		Vertical,
    		[Token(Token = "0x4000F4A")]
    		Horizontal
    	}

    	[Token(Token = "0x4000F26")]
    	public const string DefaultPathPrefix = "Scripts";

    	[Token(Token = "0x4000F27")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Configuration of the resource loader used with naninovel script resources.")]
    	public ResourceLoaderConfiguration Loader;

    	[Token(Token = "0x4000F28")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("IScriptParser implementation to use for creating script assets from text. Reimport script assets after modifying this setting for changes to take effect.")]
    	public string ScriptParser;

    	[Token(Token = "0x4000F29")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("Locale-specific NaniScript compiler options. Will propagate to IDE extension on metadata sync. Restart Unity editor and reimport script assets for changes to take effect.")]
    	public CompilerLocalization CompilerLocalization;

    	[Token(Token = "0x4000F2A")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Whether to automatically write identifiers to all the localizable text parameters in imported scripts. Enable to persist associations (eg, localization and voiceover) while editing text content. Re-import the scripts for the change to take effect.")]
    	public bool StableIdentification;

    	[Token(Token = "0x4000F2B")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("Local resource path of the script to play right after the engine initialization.")]
    	[ResourcePopup("Scripts")]
    	public string InitializationScript;

    	[Token(Token = "0x4000F2C")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Local resource path of the script to play when showing the Title UI. Can be used to setup the title screen scene (background, music, etc).")]
    	[ResourcePopup("Scripts")]
    	public string TitleScript;

    	[Token(Token = "0x4000F2D")]
    	[FieldOffset(Offset = "0x48")]
    	[Tooltip("Local resource path of the script to play when starting a new game. Will use first available when not specified.")]
    	[ResourcePopup("Scripts")]
    	public string StartGameScript;

    	[Token(Token = "0x4000F2E")]
    	[FieldOffset(Offset = "0x50")]
    	[Tooltip("Whether to automatically add created naninovel scripts to the resources.")]
    	public bool AutoAddScripts;

    	[Token(Token = "0x4000F2F")]
    	[FieldOffset(Offset = "0x51")]
    	[Tooltip("Whether to automatically resolve and update resource paths whenever scripts are created, renamed or moved.")]
    	public bool AutoResolvePath;

    	[Token(Token = "0x4000F30")]
    	[FieldOffset(Offset = "0x52")]
    	[Tooltip("Whether to reload modified (both via visual and external editors) scripts and apply changes during play mode without restarting the playback.")]
    	public bool HotReloadScripts;

    	[Token(Token = "0x4000F31")]
    	[FieldOffset(Offset = "0x53")]
    	[Tooltip("Whether to run a file system watcher over '.nani' files. Required to register script changes when edited with an external application.")]
    	public bool WatchScripts;

    	[Token(Token = "0x4000F32")]
    	[FieldOffset(Offset = "0x54")]
    	[Tooltip("Whether to auto-show script navigator UI after engine is initialized (requires 'IScriptNavigatorUI' available in UI resources).")]
    	public bool ShowScriptNavigator;

    	[Token(Token = "0x4000F33")]
    	[FieldOffset(Offset = "0x55")]
    	[Header("Visual Editor")]
    	[Tooltip("Whether to show visual script editor when a script is selected.")]
    	public bool EnableVisualEditor;

    	[Token(Token = "0x4000F34")]
    	[FieldOffset(Offset = "0x56")]
    	[Tooltip("Whether to hide un-assigned parameters of the command lines when the line is not hovered or focused.")]
    	public bool HideUnusedParameters;

    	[Token(Token = "0x4000F35")]
    	[FieldOffset(Offset = "0x57")]
    	[Tooltip("Whether to automatically select currently played script when visual editor is open.")]
    	public bool SelectPlayedScript;

    	[Token(Token = "0x4000F36")]
    	[FieldOffset(Offset = "0x58")]
    	[Tooltip("Hot key used to show 'Insert Line' window when the visual editor is in focus. Set to 'None' to disable.")]
    	public KeyCode InsertLineKey;

    	[Token(Token = "0x4000F37")]
    	[FieldOffset(Offset = "0x5C")]
    	[Tooltip("Modifier for the 'Insert Line Key'. Set to 'None' to disable.")]
    	public EventModifiers InsertLineModifier;

    	[Token(Token = "0x4000F38")]
    	[FieldOffset(Offset = "0x60")]
    	[Tooltip("Hot key used to indent lines. Set to 'None' to disable.")]
    	public KeyCode IndentLineKey;

    	[Token(Token = "0x4000F39")]
    	[FieldOffset(Offset = "0x64")]
    	[Tooltip("Modifier for the 'Indent Line Key'. Set to 'None' to disable.")]
    	public EventModifiers IndentLineModifier;

    	[Token(Token = "0x4000F3A")]
    	[FieldOffset(Offset = "0x68")]
    	[Tooltip("Hot key used to un-indent lines. Set to 'None' to disable.")]
    	public KeyCode UnindentLineKey;

    	[Token(Token = "0x4000F3B")]
    	[FieldOffset(Offset = "0x6C")]
    	[Tooltip("Modifier for the 'Unindent Line Key'. Set to 'None' to disable.")]
    	public EventModifiers UnindentLineModifier;

    	[Token(Token = "0x4000F3C")]
    	[FieldOffset(Offset = "0x70")]
    	[Tooltip("Hot key used to save (serialize) the edited script when the visual editor is in focus. Set to 'None' to disable.")]
    	public KeyCode SaveScriptKey;

    	[Token(Token = "0x4000F3D")]
    	[FieldOffset(Offset = "0x74")]
    	[Tooltip("Modifier for the 'Save Script Key'. Set to 'None' to disable.")]
    	public EventModifiers SaveScriptModifier;

    	[Token(Token = "0x4000F3E")]
    	[FieldOffset(Offset = "0x78")]
    	[Tooltip("When clicked a line in visual editor, which mouse button should activate rewind: '0' is left, '1' right, '2' middle; set to '-1' to disable.")]
    	public int RewindMouseButton;

    	[Token(Token = "0x4000F3F")]
    	[FieldOffset(Offset = "0x7C")]
    	[Tooltip("Modifier for 'Rewind Mouse Button'. Set to 'None' to disable.")]
    	public EventModifiers RewindModifier;

    	[Token(Token = "0x4000F40")]
    	[FieldOffset(Offset = "0x80")]
    	[Tooltip("How many script lines should be rendered per visual editor page.")]
    	public int EditorPageLength;

    	[Token(Token = "0x4000F41")]
    	[FieldOffset(Offset = "0x88")]
    	[Tooltip("Allows modifying default style of the visual editor.")]
    	public StyleSheet EditorCustomStyleSheet;

    	[Token(Token = "0x4000F42")]
    	[FieldOffset(Offset = "0x90")]
    	[Header("Script Graph")]
    	[Tooltip("Whether to build the graph vertically or horizontally.")]
    	public GraphOrientationType GraphOrientation;

    	[Token(Token = "0x4000F43")]
    	[FieldOffset(Offset = "0x94")]
    	[Tooltip("Padding to add for each node when performing auto align.")]
    	public Vector2 GraphAutoAlignPadding;

    	[Token(Token = "0x4000F44")]
    	[FieldOffset(Offset = "0x9C")]
    	[Tooltip("Whether to show fist comment lines of the script inside the graph node.")]
    	public bool ShowSynopsis;

    	[Token(Token = "0x4000F45")]
    	[FieldOffset(Offset = "0xA0")]
    	[Tooltip("Allows modifying default style of the script graph.")]
    	public StyleSheet GraphCustomStyleSheet;

    	[Token(Token = "0x4000F46")]
    	[FieldOffset(Offset = "0xA8")]
    	[Header("Community Modding")]
    	[Tooltip("Whether to allow adding external naninovel scripts to the build.")]
    	public bool EnableCommunityModding;

    	[Token(Token = "0x4000F47")]
    	[FieldOffset(Offset = "0xB0")]
    	[Tooltip("Configuration of the resource loader used with external naninovel script resources.")]
    	public ResourceLoaderConfiguration ExternalLoader;

    	[Token(Token = "0x6001451")]
    	[Address(RVA = "0x5BAFB0", Offset = "0x5B99B0", VA = "0x1805BAFB0")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001452")]
    	[Address(RVA = "0x5BB0E0", Offset = "0x5B9AE0", VA = "0x1805BB0E0")]
    	public ScriptsConfiguration()
    	{
    	}
    }
}
