using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000164")]
public abstract class WitchBookDataBase<TItem> : ScriptableObject
{
	[Token(Token = "0x170000D8")]
	public abstract IList<VersionedItem<TItem>> Items
	{
		[Token(Token = "0x60005C7")]
		get;
	}

	[Token(Token = "0x60005C8")]
	protected WitchBookDataBase()
	{
	}
}
