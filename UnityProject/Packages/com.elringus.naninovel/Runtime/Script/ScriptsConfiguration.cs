using Naninovel.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Naninovel
{
    [EditInProjectSettings]
    public class ScriptsConfiguration : Configuration
    {
        public enum GraphOrientationType
        {
            Vertical,
            Horizontal
        }

        public const string DefaultPathPrefix = "Scripts";

        [Tooltip("Configuration of the resource loader used with naninovel script resources.")]
        public ResourceLoaderConfiguration Loader = new() { PathPrefix = DefaultPathPrefix };
        [Tooltip(nameof(IScriptParser) + " implementation to use for creating script assets from text. Reimport script assets after modifying this setting for changes to take effect.")]
        public string ScriptParser = typeof(ScriptAssetParser).AssemblyQualifiedName;
        [Tooltip("Locale-specific NaniScript compiler options. Will propagate to IDE extension on metadata sync. Restart Unity editor and reimport script assets for changes to take effect.")]
        public CompilerLocalization CompilerLocalization;
        [Tooltip("Whether to automatically write identifiers to all the localizable text parameters in imported scripts. Enable to persist associations (eg, localization and voiceover) while editing text content. Re-import the scripts for the change to take effect.")]
        public bool StableIdentification;
        [Tooltip("Local resource path of the script to play right after the engine initialization.")]
        [ResourcePopup(DefaultPathPrefix)]
        public string InitializationScript;
        [Tooltip("Local resource path of the script to play when showing the Title UI. Can be used to setup the title screen scene (background, music, etc).")]
        [ResourcePopup(DefaultPathPrefix)]
        public string TitleScript;
        [Tooltip("Local resource path of the script to play when starting a new game. Will use first available when not specified.")]
        [ResourcePopup(DefaultPathPrefix)]
        public string StartGameScript;
        [Tooltip("Whether to automatically add created naninovel scripts to the resources.")]
        public bool AutoAddScripts = true;
        [Tooltip("Whether to automatically resolve and update resource paths whenever scripts are created, renamed or moved.")]
        public bool AutoResolvePath = true;
        [Tooltip("Whether to reload modified (both via visual and external editors) scripts and apply changes during play mode without restarting the playback.")]
        public bool HotReloadScripts = true;
        [Tooltip("Whether to run a file system watcher over '.nani' files. Required to register script changes when edited with an external application.")]
        public bool WatchScripts = true;
        [Tooltip("Whether to auto-show script navigator UI after engine is initialized (requires '" + nameof(IScriptNavigatorUI) + "' available in UI resources).")]
        public bool ShowScriptNavigator;

        [Header("Visual Editor")]
        [Tooltip("Whether to show visual script editor when a script is selected.")]
        public bool EnableVisualEditor = true;
        [Tooltip("Whether to hide un-assigned parameters of the command lines when the line is not hovered or focused.")]
        public bool HideUnusedParameters = true;
        [Tooltip("Whether to automatically select currently played script when visual editor is open.")]
        public bool SelectPlayedScript = true;
        [Tooltip("Hot key used to show 'Insert Line' window when the visual editor is in focus. Set to 'None' to disable.")]
        public KeyCode InsertLineKey = KeyCode.Space;
        [Tooltip("Modifier for the 'Insert Line Key'. Set to 'None' to disable.")]
        public EventModifiers InsertLineModifier = EventModifiers.Control;
        [Tooltip("Hot key used to indent lines. Set to 'None' to disable.")]
        public KeyCode IndentLineKey = KeyCode.RightArrow;
        [Tooltip("Modifier for the 'Indent Line Key'. Set to 'None' to disable.")]
        public EventModifiers IndentLineModifier = EventModifiers.Control;
        [Tooltip("Hot key used to un-indent lines. Set to 'None' to disable.")]
        public KeyCode UnindentLineKey = KeyCode.LeftArrow;
        [Tooltip("Modifier for the 'Unindent Line Key'. Set to 'None' to disable.")]
        public EventModifiers UnindentLineModifier = EventModifiers.Control;
        [Tooltip("Hot key used to save (serialize) the edited script when the visual editor is in focus. Set to 'None' to disable.")]
        public KeyCode SaveScriptKey = KeyCode.S;
        [Tooltip("Modifier for the 'Save Script Key'. Set to 'None' to disable.")]
        public EventModifiers SaveScriptModifier = EventModifiers.Control;
        [Tooltip("When clicked a line in visual editor, which mouse button should activate rewind: '0' is left, '1' right, '2' middle; set to '-1' to disable.")]
        public int RewindMouseButton;
        [Tooltip("Modifier for 'Rewind Mouse Button'. Set to 'None' to disable.")]
        public EventModifiers RewindModifier = EventModifiers.Shift;
        [Tooltip("How many script lines should be rendered per visual editor page.")]
        public int EditorPageLength = 300;
        [Tooltip("Allows modifying default style of the visual editor.")]
        public StyleSheet EditorCustomStyleSheet;

        [Header("Script Graph")]
        [Tooltip("Whether to build the graph vertically or horizontally.")]
        public GraphOrientationType GraphOrientation = GraphOrientationType.Horizontal;
        [Tooltip("Padding to add for each node when performing auto align.")]
        public Vector2 GraphAutoAlignPadding = new(10, 0);
        [Tooltip("Whether to show fist comment lines of the script inside the graph node.")]
        public bool ShowSynopsis = true;
        [Tooltip("Allows modifying default style of the script graph.")]
        public StyleSheet GraphCustomStyleSheet;

        [Header("Community Modding")]
        [Tooltip("Whether to allow adding external naninovel scripts to the build.")]
        public bool EnableCommunityModding;
        [Tooltip("Configuration of the resource loader used with external naninovel script resources.")]
        public ResourceLoaderConfiguration ExternalLoader = new() {
            ProviderTypes = new() { ResourceProviderConfiguration.LocalTypeName },
            PathPrefix = DefaultPathPrefix
        };

        private void OnEnable ()
        {
            if (!CompilerLocalization)
                CompilerLocalization = CreateInstance<CompilerLocalization>();
        }
    }
}
