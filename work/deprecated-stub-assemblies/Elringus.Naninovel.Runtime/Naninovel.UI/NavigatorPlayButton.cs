using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel.UI
{
    [Token(Token = "0x2000585")]
    public class NavigatorPlayButton : ScriptableButton
    {
    	[Serializable]
    	[Token(Token = "0x2000586")]
    	private class OnLabelChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6001B5D")]
    		[Address(RVA = "0x63B250", Offset = "0x639C50", VA = "0x18063B250")]
    		public OnLabelChangedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x4001522")]
    	[FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private OnLabelChangedEvent onLabelChanged;

    	[Token(Token = "0x4001523")]
    	[FieldOffset(Offset = "0x98")]
    	private NavigatorPanel navigator;

    	[Token(Token = "0x4001524")]
    	[FieldOffset(Offset = "0xA0")]
    	private string scriptPath;

    	[Token(Token = "0x4001525")]
    	[FieldOffset(Offset = "0xA8")]
    	private IScriptPlayer player;

    	[Token(Token = "0x4001526")]
    	[FieldOffset(Offset = "0xB0")]
    	private IStateManager stateManager;

    	[Token(Token = "0x4001527")]
    	[FieldOffset(Offset = "0xB8")]
    	private bool isInitialized;

    	[Token(Token = "0x6001B53")]
    	[Address(RVA = "0x63A6F0", Offset = "0x6390F0", VA = "0x18063A6F0", Slot = "63")]
    	public virtual void Initialize(NavigatorPanel navigator, string scriptPath, IScriptPlayer player)
    	{
    	}

    	[Token(Token = "0x6001B54")]
    	[Address(RVA = "0x63A8C0", Offset = "0x6392C0", VA = "0x18063A8C0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001B55")]
    	[Address(RVA = "0x63A9D0", Offset = "0x6393D0", VA = "0x18063A9D0", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001B56")]
    	[Address(RVA = "0x63AC10", Offset = "0x639610", VA = "0x18063AC10", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001B57")]
    	[Address(RVA = "0x63AE50", Offset = "0x639850", VA = "0x18063AE50", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6001B58")]
    	[Address(RVA = "0x63AF80", Offset = "0x639980", VA = "0x18063AF80", Slot = "64")]
    	protected virtual void SetLabel(string value)
    	{
    	}

    	[Token(Token = "0x6001B59")]
    	[Address(RVA = "0x63AFE0", Offset = "0x6399E0", VA = "0x18063AFE0", Slot = "65")]
    	protected virtual void PlayScript()
    	{
    	}

    	[Token(Token = "0x6001B5A")]
    	[Address(RVA = "0x63B130", Offset = "0x639B30", VA = "0x18063B130", Slot = "66")]
    	protected virtual void ControlInteractability(string _)
    	{
    	}

    	[Token(Token = "0x6001B5B")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public NavigatorPlayButton()
    	{
    	}
    }
}
