using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002A0")]
public interface ISaveSlotManager
{
	[Token(Token = "0x170001A7")]
	bool Loading
	{
		[Token(Token = "0x6000C07")]
		get;
	}

	[Token(Token = "0x170001A8")]
	bool Saving
	{
		[Token(Token = "0x6000C08")]
		get;
	}

	[Token(Token = "0x14000028")]
	event Action<string> OnBeforeSave;

	[Token(Token = "0x14000029")]
	event Action<string> OnSaved;

	[Token(Token = "0x1400002A")]
	event Action<string> OnBeforeLoad;

	[Token(Token = "0x1400002B")]
	event Action<string> OnLoaded;

	[Token(Token = "0x1400002C")]
	event Action<string> OnBeforeDelete;

	[Token(Token = "0x1400002D")]
	event Action<string> OnDeleted;

	[Token(Token = "0x1400002E")]
	event Action<string, string> OnBeforeRename;

	[Token(Token = "0x1400002F")]
	event Action<string, string> OnRenamed;

	[Token(Token = "0x6000C09")]
	bool SaveSlotExists(string slotId);

	[Token(Token = "0x6000C0A")]
	bool AnySaveExists();

	[Token(Token = "0x6000C0B")]
	void DeleteSaveSlot(string slotId);

	[Token(Token = "0x6000C0C")]
	void RenameSaveSlot(string sourceSlotId, string destSlotId);
}
[Token(Token = "0x20002A1")]
public interface ISaveSlotManager<TData> : ISaveSlotManager where TData : new()
{
	[Token(Token = "0x6000C0D")]
	UniTask Save(string slotId, TData data);

	[Token(Token = "0x6000C0E")]
	UniTask<TData> Load(string slotId);

	[Token(Token = "0x6000C0F")]
	UniTask<TData> LoadOrDefault(string slotId);
}
