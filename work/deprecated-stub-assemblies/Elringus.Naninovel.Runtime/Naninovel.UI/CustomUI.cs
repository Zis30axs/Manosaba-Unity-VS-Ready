#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x200051B")]
    public class CustomUI : ScriptableUIBehaviour, IManagedUI
    {
    	[Serializable]
    	[Token(Token = "0x200051C")]
    	public class GameState
    	{
    		[Token(Token = "0x40013D4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public bool Visible;

    		[Token(Token = "0x6001988")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200051D")]
    	public class FontChangeConfiguration
    	{
    		[Token(Token = "0x40013D5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[Tooltip("The container game object with text components, which should be affected by font changes.")]
    		public GameObject Object;

    		[Token(Token = "0x40013D6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[Tooltip("Whether to affect container children game objects; when disabled only text component on the specified container object will be affected.")]
    		public bool IncludeChildren;

    		[Token(Token = "0x40013D7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x19")]
    		[Tooltip("Whether to allow changing font of the text component.")]
    		public bool AllowFontChange;

    		[Token(Token = "0x40013D8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x1A")]
    		[Tooltip("Whether to allow changing font size of the text component.")]
    		public bool AllowFontSizeChange;

    		[Token(Token = "0x40013D9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		[Tooltip("Sizes list should contain actual font sizes to apply for text component. Each element in the list corresponds to font size dropdown list index: Small -> 0, Default -> 1, Large -> 2, Extra Large -> 3 (can be changed via SettingsUI). Default value will be ignored and font size initially set in the prefab will be used instead.")]
    		public FontSizes FontSizes;

    		[NonSerialized]
    		[Token(Token = "0x40013DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public List<TMP_Text> Components;

    		[NonSerialized]
    		[Token(Token = "0x40013DB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public Dictionary<TMP_Text, int> DefaultSizes;

    		[NonSerialized]
    		[Token(Token = "0x40013DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public Dictionary<TMP_Text, TMP_FontAsset> DefaultFonts;

    		[Token(Token = "0x6001989")]
    		[Address(RVA = "0x61B780", Offset = "0x61A180", VA = "0x18061B780")]
    		public FontChangeConfiguration()
    		{
    		}
    	}

    	[Token(Token = "0x200051E")]
    	public struct SelectableData
    	{
    		[Token(Token = "0x40013DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Selectable Selectable;

    		[Token(Token = "0x40013DE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Navigation Navigation;
    	}

    	[Token(Token = "0x200051F")]
    	public struct BindInputOptions
    	{
    		[Token(Token = "0x40013DF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public bool OnEnd;

    		[Token(Token = "0x40013E0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x1")]
    		public bool WhenHidden;

    		[Token(Token = "0x40013E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2")]
    		public bool WhenBlocked;
    	}

    	[Token(Token = "0x2000520")]
    	public struct BindInputChangeOptions
    	{
    		[Token(Token = "0x40013E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public bool WhenHidden;

    		[Token(Token = "0x40013E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x1")]
    		public bool WhenBlocked;
    	}

    	[Token(Token = "0x40013C4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	[Tooltip("Whether to automatically hide the UI when loading game or resetting state.")]
    	[SerializeField]
    	private bool hideOnLoad;

    	[Token(Token = "0x40013C5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x81")]
    	[Tooltip("Whether to hide the UI when capturing thumbnail for save-load slots.")]
    	[SerializeField]
    	private bool hideInThumbnail;

    	[Token(Token = "0x40013C6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x82")]
    	[Tooltip("Whether to preserve visibility of the UI when saving/loading game.")]
    	[SerializeField]
    	private bool saveVisibilityState;

    	[Token(Token = "0x40013C7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x83")]
    	[Tooltip("Whether to halt user input processing while the UI is visible. Will also exit auto read and skip script player modes when the UI becomes visible.")]
    	[SerializeField]
    	private bool blockInputWhenVisible;

    	[Token(Token = "0x40013C8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x84")]
    	[Tooltip("Whether to modify the UI based on current input mode (mouse and keyboard, gamepad, touch, etc). Will take control of focus mode and navigation of the underlying selectables.")]
    	[SerializeField]
    	private bool adaptToInputMode;

    	[Token(Token = "0x40013C9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	[Tooltip("Which input samplers should still be allowed in case the input is blocked while the UI is visible.")]
    	[SerializeField]
    	private string[] allowedSamplers;

    	[Token(Token = "0x40013CA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[Tooltip("Whether to make all the other managed UIs not interactable while the UI is visible.")]
    	[SerializeField]
    	private bool modalUI;

    	[Token(Token = "0x40013CB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	[Tooltip("When assigned, will not yield the UI from being modal when another UI is made modal with the same group. Assign `*` to never yield the UI from being modal.")]
    	[SerializeField]
    	private string modalGroup;

    	[Token(Token = "0x40013CC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	[Tooltip("Control buttons associated with the UI. Will be hidden when input mode is gamepad.")]
    	[SerializeField]
    	private GameObject buttonControls;

    	[Token(Token = "0x40013CD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	[Tooltip("Labels indicating controls associated with the UI. Will be hidden when input mode is not gamepad.")]
    	[SerializeField]
    	private GameObject controlsLegend;

    	[Token(Token = "0x40013CE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	[Tooltip("Setup which game objects should be affected by font and text size changes (set in game settings).")]
    	[SerializeField]
    	private List<FontChangeConfiguration> fontChangeConfiguration;

    	[Token(Token = "0x40013CF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private readonly List<SelectableData> selectables;

    	[Token(Token = "0x40013D0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	private IStateManager stateManager;

    	[Token(Token = "0x40013D1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	private IInputManager inputManager;

    	[Token(Token = "0x40013D2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	private IUIManager uiManager;

    	[Token(Token = "0x40013D3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private IScriptPlayer scriptPlayer;

    	[Token(Token = "0x1700039F")]
    	public override GameObject FocusObject
    	{
    		[Token(Token = "0x6001965")]
    		[Address(RVA = "0x619300", Offset = "0x617D00", VA = "0x180619300", Slot = "26")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001966")]
    		[Address(RVA = "0x5405E0", Offset = "0x53EFE0", VA = "0x1805405E0", Slot = "27")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170003A0")]
    	public virtual bool HideOnLoad
    	{
    		[Token(Token = "0x6001967")]
    		[Address(RVA = "0x492EF0", Offset = "0x4918F0", VA = "0x180492EF0", Slot = "71")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003A1")]
    	public virtual bool HideInThumbnail
    	{
    		[Token(Token = "0x6001968")]
    		[Address(RVA = "0x6193F0", Offset = "0x617DF0", VA = "0x1806193F0", Slot = "72")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003A2")]
    	public virtual bool SaveVisibilityState
    	{
    		[Token(Token = "0x6001969")]
    		[Address(RVA = "0x619400", Offset = "0x617E00", VA = "0x180619400", Slot = "73")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003A3")]
    	public virtual bool BlockInputWhenVisible
    	{
    		[Token(Token = "0x600196A")]
    		[Address(RVA = "0x619410", Offset = "0x617E10", VA = "0x180619410", Slot = "74")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003A4")]
    	public virtual bool AdaptToInputMode
    	{
    		[Token(Token = "0x600196B")]
    		[Address(RVA = "0x619420", Offset = "0x617E20", VA = "0x180619420", Slot = "75")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003A5")]
    	public virtual bool ModalUI
    	{
    		[Token(Token = "0x600196C")]
    		[Address(RVA = "0x619430", Offset = "0x617E30", VA = "0x180619430", Slot = "76")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003A6")]
    	public virtual string ModalGroup
    	{
    		[Token(Token = "0x600196D")]
    		[Address(RVA = "0x492EE0", Offset = "0x4918E0", VA = "0x180492EE0", Slot = "77")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003A7")]
    	public virtual IReadOnlyList<SelectableData> Selectables
    	{
    		[Token(Token = "0x600196E")]
    		[Address(RVA = "0x492CE0", Offset = "0x4916E0", VA = "0x180492CE0", Slot = "78")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003A8")]
    	protected virtual List<FontChangeConfiguration> FontChangeConfigurations
    	{
    		[Token(Token = "0x600196F")]
    		[Address(RVA = "0x4717B0", Offset = "0x4701B0", VA = "0x1804717B0", Slot = "79")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003A9")]
    	protected virtual string[] AllowedSamplers
    	{
    		[Token(Token = "0x6001970")]
    		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "80")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003AA")]
    	protected virtual GameObject ButtonControls
    	{
    		[Token(Token = "0x6001971")]
    		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470", Slot = "81")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003AB")]
    	protected virtual GameObject ControlsLegend
    	{
    		[Token(Token = "0x6001972")]
    		[Address(RVA = "0x471780", Offset = "0x470180", VA = "0x180471780", Slot = "82")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001973")]
    	[Address(RVA = "0x3483B0", Offset = "0x346DB0", VA = "0x1803483B0", Slot = "83")]
    	public virtual UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001974")]
    	[Address(RVA = "0x619440", Offset = "0x617E40", VA = "0x180619440", Slot = "84")]
    	public virtual void SetFont(TMP_FontAsset font)
    	{
    	}

    	[Token(Token = "0x6001975")]
    	[Address(RVA = "0x619470", Offset = "0x617E70", VA = "0x180619470", Slot = "85")]
    	public virtual void SetFontSize(int dropdownIndex)
    	{
    	}

    	[Token(Token = "0x6001976")]
    	[Address(RVA = "0x6194A0", Offset = "0x617EA0", VA = "0x1806194A0", Slot = "86")]
    	public virtual UniTask HandleLocalizationChanged(LocaleChangedArgs _)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001977")]
    	[Address(RVA = "0x619530", Offset = "0x617F30", VA = "0x180619530", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001978")]
    	[Address(RVA = "0x619770", Offset = "0x618170", VA = "0x180619770", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001979")]
    	[Address(RVA = "0x619C90", Offset = "0x618690", VA = "0x180619C90", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x600197A")]
    	[Address(RVA = "0x619F40", Offset = "0x618940", VA = "0x180619F40", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x600197B")]
    	[Address(RVA = "0x61A010", Offset = "0x618A10", VA = "0x18061A010", Slot = "87")]
    	protected virtual void SerializeState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x600197C")]
    	[Address(RVA = "0x61A0E0", Offset = "0x618AE0", VA = "0x18061A0E0", Slot = "88")]
    	protected virtual UniTask DeserializeState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600197D")]
    	[Address(RVA = "0x61A1B0", Offset = "0x618BB0", VA = "0x18061A1B0", Slot = "55")]
    	protected override void HandleVisibilityChanged(bool visible)
    	{
    	}

    	[Token(Token = "0x600197E")]
    	[Address(RVA = "0x61A410", Offset = "0x618E10", VA = "0x18061A410", Slot = "54")]
    	protected override void SetVisibilityOnAwake()
    	{
    	}

    	[Token(Token = "0x600197F")]
    	[Address(RVA = "0x61A4B0", Offset = "0x618EB0", VA = "0x18061A4B0", Slot = "89")]
    	protected virtual void InitializeFontChangeConfiguration()
    	{
    	}

    	[Token(Token = "0x6001980")]
    	[Address(RVA = "0x61A4E0", Offset = "0x618EE0", VA = "0x18061A4E0", Slot = "90")]
    	protected virtual void HandleInputModeChanged(InputMode mode)
    	{
    	}

    	[Token(Token = "0x6001981")]
    	[Address(RVA = "0x61AAA0", Offset = "0x6194A0", VA = "0x18061AAA0", Slot = "91")]
    	protected virtual bool IsFocusInside()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001982")]
    	[Address(RVA = "0x61AE80", Offset = "0x619880", VA = "0x18061AE80", Slot = "92")]
    	protected virtual GameObject FindFocusObject()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001983")]
    	[Address(RVA = "0x61B000", Offset = "0x619A00", VA = "0x18061B000", Slot = "93")]
    	protected virtual void BindInput(string inputName, Action handler, [Optional] BindInputOptions? options)
    	{
    	}

    	[Token(Token = "0x6001984")]
    	[Address(RVA = "0x61B2C0", Offset = "0x619CC0", VA = "0x18061B2C0", Slot = "94")]
    	protected virtual void BindInput(string inputName, Action<float> handler, [Optional] BindInputChangeOptions? options)
    	{
    	}

    	[Token(Token = "0x6001985")]
    	[Address(RVA = "0x61B520", Offset = "0x619F20", VA = "0x18061B520")]
    	protected bool ShouldHandleBoundInput(bool whenHidden, bool whenBlocked)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001986")]
    	[Address(RVA = "0x61B680", Offset = "0x61A080", VA = "0x18061B680")]
    	private void HandleGameLoadStarted(GameSaveLoadArgs args)
    	{
    	}

    	[Token(Token = "0x6001987")]
    	[Address(RVA = "0x61B6A0", Offset = "0x61A0A0", VA = "0x18061B6A0")]
    	public CustomUI()
    	{
    	}
    }
}
