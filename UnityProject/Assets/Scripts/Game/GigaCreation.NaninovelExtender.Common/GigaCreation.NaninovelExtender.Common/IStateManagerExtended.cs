using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000026")]
    public interface IStateManagerExtended : IStateManager, IEngineService<StateConfiguration>, IEngineService
    {
    	[Token(Token = "0x1700000C")]
    	bool IsSoftResetting
    	{
    		[Token(Token = "0x6000060")]
    		get;
    	}

    	// NEW IN THE 2026-10-02 GAME UPDATE: absent from this checkout's pre-update dump (src/il2cpp),
    	// present in the updated game's metadata and needed by `WitchTrials.Views.AutoSave`.
    	[Token(Token = "0x1700000F")]
    	IReadOnlyList<string> AutoSaveSlotNames
    	{
    		[Token(Token = "0x6000066")]
    		get;
    	}

    	[Token(Token = "0x6000061")]
    	UniTask ResetStateAsync(IReadOnlyCollection<string> exclude, bool soft = false, params Func<UniTask>[] tasks);

    	[Token(Token = "0x6000062")]
    	UniTask ResetStateAsync(IReadOnlyCollection<Type> exclude, bool soft = false, params Func<UniTask>[] tasks);

    	// NEW IN THE 2026-10-02 GAME UPDATE.  The interface dump records AutoSaveAsync at slot 4 and
    	// QuickSaveAsync at slot 5; `WitchTrials.Views.AutoSave` passes exactly those indices to the
    	// interface-invoke helper at RVA 0x3270.
    	[Token(Token = "0x6000069")]
    	UniTask<GameStateMap> AutoSaveAsync();

    	[Token(Token = "0x6000063")]
    	UniTask<GameStateMap> QuickSaveAsync(int slotNumber);

    	[Token(Token = "0x6000064")]
    	UniTask<GameStateMap> QuickLoadAsync(int slotNumber);
    }
}
