using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20004DB")]
public interface IUnlockableManager : IEngineService<UnlockablesConfiguration>, IEngineService
{
	[Token(Token = "0x17000376")]
	IReadOnlyCollection<string> ItemIds
	{
		[Token(Token = "0x600184C")]
		get;
	}

	[Token(Token = "0x14000088")]
	event Action<UnlockableItemUpdatedArgs> OnItemUpdated;

	[Token(Token = "0x600184D")]
	bool ItemUnlocked(string itemId);

	[Token(Token = "0x600184E")]
	void SetItemUnlocked(string itemId, bool unlocked);

	[Token(Token = "0x600184F")]
	void UnlockItem(string itemId);

	[Token(Token = "0x6001850")]
	void LockItem(string itemId);

	[Token(Token = "0x6001851")]
	void UnlockAllItems();

	[Token(Token = "0x6001852")]
	void LockAllItems();
}
