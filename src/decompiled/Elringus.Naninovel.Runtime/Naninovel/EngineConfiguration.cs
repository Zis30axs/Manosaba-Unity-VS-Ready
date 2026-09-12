using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000341")]
[EditInProjectSettings]
public class EngineConfiguration : Configuration
{
	[Token(Token = "0x4000C26")]
	public const string DefaultGeneratedDataPath = "NaninovelData";

	[Token(Token = "0x4000C27")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Whether to assign a specific layer to all the engine objects. Engine's camera will use the layer for the culling mask. Use this to isolate Naninovel objects from being rendered by other cameras.")]
	public bool OverrideObjectsLayer;

	[Token(Token = "0x4000C28")]
	[FieldOffset(Offset = "0x1C")]
	[Tooltip("When `Override Objects Layer` is enabled, the specified layer will be assigned to all the engine objects.")]
	public int ObjectsLayer;

	[Token(Token = "0x4000C29")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Log type to use for UniTask-related exceptions.")]
	public LogType AsyncExceptionLogType;

	[Token(Token = "0x4000C2A")]
	[FieldOffset(Offset = "0x24")]
	[Tooltip("Whether to use `Object.InstantiateAsync` for instantiating engine objects, which moves most associated work off the main thread. Keep enabled unless experiencing issues.")]
	public bool AsyncInstantiation;

	[Token(Token = "0x4000C2B")]
	[FieldOffset(Offset = "0x25")]
	[Header("Initialization")]
	[Tooltip("Whether to automatically initialize the engine when application starts.")]
	public bool InitializeOnApplicationLoad;

	[Token(Token = "0x4000C2C")]
	[FieldOffset(Offset = "0x26")]
	[Tooltip("Whether to apply `DontDestroyOnLoad` to the engine objects, making their lifetime independent of any loaded scenes. When disabled, the objects will be part of the Unity scene where the engine was initialized and will be destroyed when the scene is unloaded.")]
	public bool SceneIndependent;

	[Token(Token = "0x4000C2D")]
	[FieldOffset(Offset = "0x27")]
	[Tooltip("Whether to show a loading UI while the engine is initializing.")]
	public bool ShowInitializationUI;

	[Token(Token = "0x4000C2E")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("UI to show while the engine is initializing (when enabled). Will use a default one when not specified.")]
	public ScriptableUIBehaviour CustomInitializationUI;

	[Token(Token = "0x4000C2F")]
	[FieldOffset(Offset = "0x30")]
	[Header("Bridging")]
	[Tooltip("Whether to automatically start the bridging server to communicate with external Naninovel tools: IDE extension, web editor, etc.")]
	public bool EnableBridging;

	[Token(Token = "0x4000C30")]
	[FieldOffset(Offset = "0x31")]
	[Tooltip("Whether to automatically generate project metadata when Unity editor is started and after compiling C# scripts.")]
	public bool AutoGenerateMetadata;

	[Token(Token = "0x4000C31")]
	[FieldOffset(Offset = "0x32")]
	[Header("Development Console")]
	[Tooltip("Whether to enable development console.")]
	public bool EnableDevelopmentConsole;

	[Token(Token = "0x4000C32")]
	[FieldOffset(Offset = "0x33")]
	[Tooltip("When enabled, development console will only be available in development (debug) builds.")]
	public bool DebugOnlyConsole;

	[Token(Token = "0x6000FA3")]
	[Address(RVA = "0x5692A0", Offset = "0x567CA0", VA = "0x1805692A0")]
	public EngineConfiguration()
	{
	}
}
