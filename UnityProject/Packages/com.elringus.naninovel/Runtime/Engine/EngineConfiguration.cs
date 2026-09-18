using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class EngineConfiguration : Configuration
    {
        public const string DefaultGeneratedDataPath = "NaninovelData";

        [Tooltip("Whether to assign a specific layer to all the engine objects. Engine's camera will use the layer for the culling mask. Use this to isolate Naninovel objects from being rendered by other cameras.")]
        public bool OverrideObjectsLayer;
        [Tooltip("When `Override Objects Layer` is enabled, the specified layer will be assigned to all the engine objects.")]
        public int ObjectsLayer;
        [Tooltip("Log type to use for UniTask-related exceptions.")]
        public LogType AsyncExceptionLogType = LogType.Error;
        [Tooltip("Whether to use `Object.InstantiateAsync` for instantiating engine objects, which moves most associated work off the main thread. Keep enabled unless experiencing issues.")]
        public bool AsyncInstantiation = true;

        [Header("Initialization")]
        [Tooltip("Whether to automatically initialize the engine when application starts.")]
        public bool InitializeOnApplicationLoad = true;
        [Tooltip("Whether to apply `DontDestroyOnLoad` to the engine objects, making their lifetime independent of any loaded scenes. When disabled, the objects will be part of the Unity scene where the engine was initialized and will be destroyed when the scene is unloaded.")]
        public bool SceneIndependent = true;
        [Tooltip("Whether to show a loading UI while the engine is initializing.")]
        public bool ShowInitializationUI = true;
        [Tooltip("UI to show while the engine is initializing (when enabled). Will use a default one when not specified.")]
        public ScriptableUIBehaviour CustomInitializationUI;

        [Header("Bridging")]
        [Tooltip("Whether to automatically start the bridging server to communicate with external Naninovel tools: IDE extension, web editor, etc.")]
        public bool EnableBridging = true;
        [Tooltip("Whether to automatically generate project metadata when Unity editor is started and after compiling C# scripts.")]
        public bool AutoGenerateMetadata = true;

        [Header("Development Console")]
        [Tooltip("Whether to enable development console.")]
        public bool EnableDevelopmentConsole = true;
        [Tooltip("When enabled, development console will only be available in development (debug) builds.")]
        public bool DebugOnlyConsole;
    }
}
