using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common;

[Token(Token = "0x2000026")]
public interface IStateManagerExtended : IStateManager, IEngineService<StateConfiguration>, IEngineService
{
	[Token(Token = "0x1700000C")]
	bool IsSoftResetting
	{
		[Token(Token = "0x6000060")]
		get;
	}

	[Token(Token = "0x6000061")]
	UniTask ResetStateAsync(IReadOnlyCollection<string> exclude, bool soft = false, params Func<UniTask>[] tasks);

	[Token(Token = "0x6000062")]
	UniTask ResetStateAsync(IReadOnlyCollection<Type> exclude, bool soft = false, params Func<UniTask>[] tasks);

	[Token(Token = "0x6000063")]
	UniTask<GameStateMap> QuickSaveAsync(int slotNumber);

	[Token(Token = "0x6000064")]
	UniTask<GameStateMap> QuickLoadAsync(int slotNumber);
}
