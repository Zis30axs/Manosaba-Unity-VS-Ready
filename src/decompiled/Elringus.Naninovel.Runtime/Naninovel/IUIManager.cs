using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004C6")]
public interface IUIManager : IEngineService<UIConfiguration>, IEngineService
{
	[Token(Token = "0x1700036B")]
	string FontName
	{
		[Token(Token = "0x60017D2")]
		get;
		[Token(Token = "0x60017D3")]
		set;
	}

	[Token(Token = "0x1700036C")]
	int FontSize
	{
		[Token(Token = "0x60017D4")]
		get;
		[Token(Token = "0x60017D5")]
		set;
	}

	[Token(Token = "0x1700036D")]
	bool AnyModal
	{
		[Token(Token = "0x60017D6")]
		get;
	}

	[Token(Token = "0x14000084")]
	event Action<string> OnFontNameChanged;

	[Token(Token = "0x14000085")]
	event Action<int> OnFontSizeChanged;

	[Token(Token = "0x60017D7")]
	UniTask<IManagedUI> AddUI(GameObject prefab, [Optional] string name, [Optional] string group);

	[Token(Token = "0x60017D8")]
	void GetManagedUIs(ICollection<IManagedUI> managedUIs);

	[Token(Token = "0x60017D9")]
	bool HasUI<T>() where T : class, IManagedUI;

	[Token(Token = "0x60017DA")]
	bool HasUI(string name);

	[Token(Token = "0x60017DB")]
	T GetUI<T>() where T : class, IManagedUI;

	[Token(Token = "0x60017DC")]
	IManagedUI GetUI(string name);

	[Token(Token = "0x60017DD")]
	bool RemoveUI(IManagedUI managedUI);

	[Token(Token = "0x60017DE")]
	void SetUIVisibleWithToggle(bool visible, bool allowToggle = true);

	[Token(Token = "0x60017DF")]
	void AddModalUI(IManagedUI managedUI);

	[Token(Token = "0x60017E0")]
	void RemoveModalUI(IManagedUI managedUI);

	[Token(Token = "0x60017E1")]
	void GetModalUIs(ICollection<IManagedUI> modalUIs);

	[Token(Token = "0x60017E2")]
	bool IsActiveModalUI(IManagedUI managedUI);

	[Token(Token = "0x60017E3")]
	TMP_FontAsset GetFontAsset(string fontName);
}
