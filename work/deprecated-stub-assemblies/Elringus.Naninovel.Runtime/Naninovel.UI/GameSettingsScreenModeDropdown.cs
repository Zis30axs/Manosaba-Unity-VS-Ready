using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x2000554")]
    public class GameSettingsScreenModeDropdown : ScriptableDropdown
    {
    	[Token(Token = "0x4001473")]
    	[FieldOffset(Offset = "0x0")]
    	[ManagedText("DefaultUI")]
    	protected static string ExclusiveFullScreen;

    	[Token(Token = "0x4001474")]
    	[FieldOffset(Offset = "0x8")]
    	[ManagedText("DefaultUI")]
    	protected static string FullScreenWindow;

    	[Token(Token = "0x4001475")]
    	[FieldOffset(Offset = "0x10")]
    	[ManagedText("DefaultUI")]
    	protected static string MaximizedWindow;

    	[Token(Token = "0x4001476")]
    	[FieldOffset(Offset = "0x18")]
    	[ManagedText("DefaultUI")]
    	protected static string Windowed;

    	[Token(Token = "0x4001477")]
    	[FieldOffset(Offset = "0x90")]
    	private readonly Dictionary<int, FullScreenMode> indexToMode;

    	[Token(Token = "0x4001478")]
    	[FieldOffset(Offset = "0x98")]
    	private readonly List<string> options;

    	[Token(Token = "0x4001479")]
    	[FieldOffset(Offset = "0xA0")]
    	private bool allowApplySettings;

    	[Token(Token = "0x6001A6D")]
    	[Address(RVA = "0x62ACF0", Offset = "0x6296F0", VA = "0x18062ACF0", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001A6E")]
    	[Address(RVA = "0x62AEB0", Offset = "0x6298B0", VA = "0x18062AEB0", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001A6F")]
    	[Address(RVA = "0x62B060", Offset = "0x629A60", VA = "0x18062B060", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001A70")]
    	[Address(RVA = "0x62B070", Offset = "0x629A70", VA = "0x18062B070", Slot = "62")]
    	protected override void OnValueChanged(int value)
    	{
    	}

    	[Token(Token = "0x6001A71")]
    	[Address(RVA = "0x62B1E0", Offset = "0x629BE0", VA = "0x18062B1E0", Slot = "63")]
    	protected virtual void MapOptions()
    	{
    	}

    	[Token(Token = "0x6001A72")]
    	[Address(RVA = "0x62B500", Offset = "0x629F00", VA = "0x18062B500", Slot = "64")]
    	protected virtual void InitializeOptions()
    	{
    	}

    	[Token(Token = "0x6001A73")]
    	[Address(RVA = "0x628AB0", Offset = "0x6274B0", VA = "0x180628AB0", Slot = "65")]
    	protected virtual void HandleLocaleChanged(LocaleChangedArgs _)
    	{
    	}

    	[Token(Token = "0x6001A74")]
    	[Address(RVA = "0x62B620", Offset = "0x62A020", VA = "0x18062B620", Slot = "66")]
    	protected virtual void MapOption(string option, FullScreenMode mode)
    	{
    	}

    	[Token(Token = "0x6001A75")]
    	[Address(RVA = "0x62B6F0", Offset = "0x62A0F0", VA = "0x18062B6F0")]
    	public GameSettingsScreenModeDropdown()
    	{
    	}
    }
}
