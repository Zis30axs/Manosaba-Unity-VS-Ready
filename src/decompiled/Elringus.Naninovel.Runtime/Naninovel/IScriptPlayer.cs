using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;

namespace Naninovel;

[Token(Token = "0x200042E")]
public interface IScriptPlayer : IEngineService<ScriptPlayerConfiguration>, IEngineService
{
	[Token(Token = "0x170002D3")]
	bool Playing
	{
		[Token(Token = "0x6001478")]
		get;
	}

	[Token(Token = "0x170002D4")]
	bool Completing
	{
		[Token(Token = "0x6001479")]
		get;
	}

	[Token(Token = "0x170002D5")]
	bool SkipActive
	{
		[Token(Token = "0x600147A")]
		get;
	}

	[Token(Token = "0x170002D6")]
	bool AutoPlayActive
	{
		[Token(Token = "0x600147B")]
		get;
	}

	[Token(Token = "0x170002D7")]
	bool WaitingForInput
	{
		[Token(Token = "0x600147C")]
		get;
	}

	[Token(Token = "0x170002D8")]
	PlayerSkipMode SkipMode
	{
		[Token(Token = "0x600147D")]
		get;
		[Token(Token = "0x600147E")]
		set;
	}

	[Token(Token = "0x170002D9")]
	Script PlayedScript
	{
		[Token(Token = "0x600147F")]
		get;
	}

	[Token(Token = "0x170002DA")]
	Command PlayedCommand
	{
		[Token(Token = "0x6001480")]
		get;
	}

	[Token(Token = "0x170002DB")]
	IReadOnlyCollection<Command> ExecutingCommands
	{
		[Token(Token = "0x6001481")]
		get;
	}

	[Token(Token = "0x170002DC")]
	PlaybackSpot PlaybackSpot
	{
		[Token(Token = "0x6001482")]
		get;
	}

	[Token(Token = "0x170002DD")]
	ScriptPlaylist Playlist
	{
		[Token(Token = "0x6001483")]
		get;
	}

	[Token(Token = "0x170002DE")]
	int PlayedIndex
	{
		[Token(Token = "0x6001484")]
		get;
	}

	[Token(Token = "0x170002DF")]
	Stack<PlaybackSpot> GosubReturnSpots
	{
		[Token(Token = "0x6001485")]
		get;
	}

	[Token(Token = "0x170002E0")]
	int PlayedCommandsCount
	{
		[Token(Token = "0x6001486")]
		get;
	}

	[Token(Token = "0x14000065")]
	event Action<Script> OnPlay;

	[Token(Token = "0x14000066")]
	event Action<Script> OnStop;

	[Token(Token = "0x14000067")]
	event Action<Command> OnCommandExecutionStart;

	[Token(Token = "0x14000068")]
	event Action<Command> OnCommandExecutionFinish;

	[Token(Token = "0x14000069")]
	event Action<bool> OnSkip;

	[Token(Token = "0x1400006A")]
	event Action<bool> OnAutoPlay;

	[Token(Token = "0x1400006B")]
	event Action<bool> OnWaitingForInput;

	[Token(Token = "0x6001487")]
	void Play(string scriptPath, int playlistIndex = 0);

	[Token(Token = "0x6001488")]
	void Resume([Optional] int? playlistIndex);

	[Token(Token = "0x6001489")]
	void Stop();

	[Token(Token = "0x600148A")]
	UniTask<bool> Rewind(int lineIndex);

	[Token(Token = "0x600148B")]
	bool HasPlayed(string scriptPath, [Optional] int? playlistIndex);

	[Token(Token = "0x600148C")]
	void SetSkipEnabled(bool enabled);

	[Token(Token = "0x600148D")]
	void SetAutoPlayEnabled(bool enabled);

	[Token(Token = "0x600148E")]
	void SetWaitingForInputEnabled(bool enabled);

	[Token(Token = "0x600148F")]
	void AddPreExecutionTask(Func<Command, UniTask> task);

	[Token(Token = "0x6001490")]
	void RemovePreExecutionTask(Func<Command, UniTask> task);

	[Token(Token = "0x6001491")]
	void AddPostExecutionTask(Func<Command, UniTask> task);

	[Token(Token = "0x6001492")]
	void RemovePostExecutionTask(Func<Command, UniTask> task);

	[Token(Token = "0x6001493")]
	UniTask Complete([Optional][CanBeNull] Func<UniTask> onComplete);
}
