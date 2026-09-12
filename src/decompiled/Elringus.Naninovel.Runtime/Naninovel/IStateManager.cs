using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000477")]
public interface IStateManager : IEngineService<StateConfiguration>, IEngineService
{
	[Token(Token = "0x17000317")]
	GlobalStateMap GlobalState
	{
		[Token(Token = "0x600160E")]
		get;
	}

	[Token(Token = "0x17000318")]
	SettingsStateMap SettingsState
	{
		[Token(Token = "0x600160F")]
		get;
	}

	[Token(Token = "0x17000319")]
	ISaveSlotManager<GlobalStateMap> GlobalSlotManager
	{
		[Token(Token = "0x6001610")]
		get;
	}

	[Token(Token = "0x1700031A")]
	ISaveSlotManager<GameStateMap> GameSlotManager
	{
		[Token(Token = "0x6001611")]
		get;
	}

	[Token(Token = "0x1700031B")]
	ISaveSlotManager<SettingsStateMap> SettingsSlotManager
	{
		[Token(Token = "0x6001612")]
		get;
	}

	[Token(Token = "0x1700031C")]
	bool QuickLoadAvailable
	{
		[Token(Token = "0x6001613")]
		get;
	}

	[Token(Token = "0x1700031D")]
	bool AnyGameSaveExists
	{
		[Token(Token = "0x6001614")]
		get;
	}

	[Token(Token = "0x1700031E")]
	bool RollbackInProgress
	{
		[Token(Token = "0x6001615")]
		get;
	}

	[Token(Token = "0x14000074")]
	event Action<GameSaveLoadArgs> OnGameLoadStarted;

	[Token(Token = "0x14000075")]
	event Action<GameSaveLoadArgs> OnGameLoadFinished;

	[Token(Token = "0x14000076")]
	event Action<GameSaveLoadArgs> OnGameSaveStarted;

	[Token(Token = "0x14000077")]
	event Action<GameSaveLoadArgs> OnGameSaveFinished;

	[Token(Token = "0x14000078")]
	event Action OnResetStarted;

	[Token(Token = "0x14000079")]
	event Action OnResetFinished;

	[Token(Token = "0x1400007A")]
	event Action OnRollbackStarted;

	[Token(Token = "0x1400007B")]
	event Action OnRollbackFinished;

	[Token(Token = "0x6001616")]
	void AddOnGameSerializeTask(Action<GameStateMap> task);

	[Token(Token = "0x6001617")]
	void RemoveOnGameSerializeTask(Action<GameStateMap> task);

	[Token(Token = "0x6001618")]
	void AddOnGameDeserializeTask(Func<GameStateMap, UniTask> task);

	[Token(Token = "0x6001619")]
	void RemoveOnGameDeserializeTask(Func<GameStateMap, UniTask> task);

	[Token(Token = "0x600161A")]
	UniTask<GameStateMap> SaveGame(string slotId);

	[Token(Token = "0x600161B")]
	UniTask<GameStateMap> QuickSave();

	[Token(Token = "0x600161C")]
	UniTask<GameStateMap> LoadGame(string slotId);

	[Token(Token = "0x600161D")]
	UniTask<GameStateMap> QuickLoad();

	[Token(Token = "0x600161E")]
	UniTask SaveGlobal();

	[Token(Token = "0x600161F")]
	UniTask SaveSettings();

	[Token(Token = "0x6001620")]
	UniTask ResetState(params Func<UniTask>[] tasks);

	[Token(Token = "0x6001621")]
	UniTask ResetState(IReadOnlyCollection<string> exclude, params Func<UniTask>[] tasks);

	[Token(Token = "0x6001622")]
	UniTask ResetState(IReadOnlyCollection<Type> exclude, params Func<UniTask>[] tasks);

	[Token(Token = "0x6001623")]
	void PushRollbackSnapshot(bool allowPlayerRollback = true);

	[Token(Token = "0x6001624")]
	GameStateMap PeekRollbackStack();

	[Token(Token = "0x6001625")]
	UniTask<bool> Rollback(Predicate<GameStateMap> predicate);

	[Token(Token = "0x6001626")]
	bool CanRollbackTo(Predicate<GameStateMap> predicate);

	[Token(Token = "0x6001627")]
	void PurgeRollbackData();
}
