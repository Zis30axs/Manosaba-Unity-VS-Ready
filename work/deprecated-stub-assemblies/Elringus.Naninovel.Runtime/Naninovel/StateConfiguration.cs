using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200047D")]
    [EditInProjectSettings]
    public class StateConfiguration : Configuration
    {
    	[Token(Token = "0x40010AE")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("The folder will be created in the game data folder.")]
    	public string SaveFolderName;

    	[Token(Token = "0x40010AF")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("The name of the settings save file.")]
    	public string DefaultSettingsSlotId;

    	[Token(Token = "0x40010B0")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("The name of the global save file.")]
    	public string DefaultGlobalSlotId;

    	[Token(Token = "0x40010B1")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Mask used to name save slots.")]
    	public string SaveSlotMask;

    	[Token(Token = "0x40010B2")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("Mask used to name quick save slots.")]
    	public string QuickSaveSlotMask;

    	[Token(Token = "0x40010B3")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Maximum number of save slots.")]
    	[Range(1f, 999f)]
    	public int SaveSlotLimit;

    	[Token(Token = "0x40010B4")]
    	[FieldOffset(Offset = "0x44")]
    	[Tooltip("Maximum number of quick save slots.")]
    	[Range(1f, 999f)]
    	public int QuickSaveSlotLimit;

    	[Token(Token = "0x40010B5")]
    	[FieldOffset(Offset = "0x48")]
    	[Tooltip("Whether to compress and store the saves as binary files (.nson) instead of text files (.json). This will significantly reduce the files size and make them harder to edit (to prevent cheating), but will consume more memory and CPU time when saving and loading.")]
    	public bool BinarySaveFiles;

    	[Token(Token = "0x40010B6")]
    	[FieldOffset(Offset = "0x49")]
    	[Tooltip("Whether to reset state of the engine services when loading another script via [@goto] command. Can be used instead of [@resetState] command to automatically unload all the resources on each goto.")]
    	public bool ResetOnGoto;

    	[Token(Token = "0x40010B7")]
    	[FieldOffset(Offset = "0x4A")]
    	[Header("State Rollback")]
    	[Tooltip("Whether to enable state rollback feature allowing player to rewind the script backwards.")]
    	public bool EnableStateRollback;

    	[Token(Token = "0x40010B8")]
    	[FieldOffset(Offset = "0x4C")]
    	[Tooltip("The number of state snapshots to keep at runtime; determines how far back the rollback (rewind) can be performed. Increasing this value will consume more memory.")]
    	public int StateRollbackSteps;

    	[Token(Token = "0x40010B9")]
    	[FieldOffset(Offset = "0x50")]
    	[Tooltip("The number of state snapshots to serialize (save) under the save game slots; determines how far back the rollback can be performed after loading a saved game. Increasing this value will enlarge save game files.")]
    	public int SavedRollbackSteps;

    	[Token(Token = "0x40010BA")]
    	[FieldOffset(Offset = "0x58")]
    	[Header("Serialization Handlers")]
    	[Tooltip("Implementation responsible for de-/serializing local (session-specific) game state; see `State Management` guide on how to add custom serialization handlers.")]
    	public string GameStateHandler;

    	[Token(Token = "0x40010BB")]
    	[FieldOffset(Offset = "0x60")]
    	[Tooltip("Implementation responsible for de-/serializing global game state; see `State Management` guide on how to add custom serialization handlers.")]
    	public string GlobalStateHandler;

    	[Token(Token = "0x40010BC")]
    	[FieldOffset(Offset = "0x68")]
    	[Tooltip("Implementation responsible for de-/serializing game settings; see `State Management` guide on how to add custom serialization handlers.")]
    	public string SettingsStateHandler;

    	[Token(Token = "0x6001636")]
    	[Address(RVA = "0x5DBE50", Offset = "0x5DA850", VA = "0x1805DBE50")]
    	public string IndexToSaveSlotId(int index)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001637")]
    	[Address(RVA = "0x5DBEC0", Offset = "0x5DA8C0", VA = "0x1805DBEC0")]
    	public string IndexToQuickSaveSlotId(int index)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001638")]
    	[Address(RVA = "0x5DBF30", Offset = "0x5DA930", VA = "0x1805DBF30")]
    	public StateConfiguration()
    	{
    	}
    }
}
