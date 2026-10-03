using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common
{
    // NEW IN THE 2026-10-02 GAME UPDATE.  The shipped Naninovel StateConfiguration points
    // `GameStateHandler` at `GigaCreation.NaninovelExtender.Common.UniversalGameStateSerializerExtended`,
    // which implements this interface.  This checkout's pre-update reconstruction has neither, so
    // `Naninovel.StateManager..ctor` (Runtime/State/StateManager.cs:55) called
    // `Activator.CreateInstance(Type.GetType(cfg.GameStateHandler), cfg, saveDir)` with a *null* type
    // and threw ArgumentNullException("type") inside RuntimeInitializer.Initialize -- Naninovel never
    // finished initializing, `Engine.GetServiceOrErr<ICustomVariableManager>()` threw from
    // EntrySequence.ResetDebugVariables, and both G4 and G5 failed with `services=0`.
    //
    // Shape and member order come from the updated dump (work/il2cpp_out/dump.cs: TypeDefIndex 17008)
    // and the tokens from its DummyDll.
    [Token(Token = "0x2000028")]
    public interface ISaveSlotManagerExtended<TData> : ISaveSlotManager<TData>, ISaveSlotManager where TData : new()
    {
    	[Token(Token = "0x1700000D")]
    	string[] AutoSaveSlotNames
    	{
    		[Token(Token = "0x6000063")]
    		get;
    	}

    	[Token(Token = "0x6000064")]
    	void EnsureCapacityForAutoSaveSlot(int capacity);
    }
}
