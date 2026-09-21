using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GigaCreation.NaninovelExtender.Common;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x200001E")]
    public class LoadingUiExtended : CustomUI, ILoadingUiExtended, ILoadingUI, IManagedUI
    {
    	[Serializable]
    	[Token(Token = "0x200001F")]
    	public class SettingsData
    	{
    		[Token(Token = "0x400006F")]
    		[FieldOffset(Offset = "0x10")]
    		[SerializeField]
    		private LoadingMode _mode;

    		[Token(Token = "0x4000070")]
    		[FieldOffset(Offset = "0x18")]
    		[SerializeField]
    		private SettingsItem _item;

    		[Token(Token = "0x1700000C")]
    		public LoadingMode Mode
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x331650 -- shared trivial getter: mov eax, [rcx+0x10] (_mode)
    			[Token(Token = "0x600006C")]
    			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    			get
    			{
    				return _mode;
    			}
    		}

    		[Token(Token = "0x1700000D")]
    		public SettingsItem Item
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 -- shared trivial getter: mov rax, [rcx+0x18] (_item)
    			[Token(Token = "0x600006D")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _item;
    			}
    		}

    		[Token(Token = "0x600006E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public SettingsData()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000020")]
    	public class SettingsItem
    	{
    		[Token(Token = "0x4000071")]
    		[FieldOffset(Offset = "0x10")]
    		[SerializeField]
    		private bool _blockInputWhenVisible;

    		[Token(Token = "0x4000072")]
    		[FieldOffset(Offset = "0x11")]
    		[SerializeField]
    		private bool _showUnderlay;

    		[Token(Token = "0x4000073")]
    		[FieldOffset(Offset = "0x18")]
    		[SerializeField]
    		private LoadingIndicatorBase _indicator;

    		[Token(Token = "0x4000074")]
    		[FieldOffset(Offset = "0x20")]
    		[SerializeField]
    		private FloatUnityEvent _onScriptLoadProgressChanged;

    		[Token(Token = "0x1700000E")]
    		public bool BlockInputWhenVisible
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x44D320 -- shared trivial getter: movzx eax, byte [rcx+0x10]
    			[Token(Token = "0x600006F")]
    			[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
    			get
    			{
    				return _blockInputWhenVisible;
    			}
    		}

    		[Token(Token = "0x1700000F")]
    		public bool ShowUnderlay
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x542940 -- shared trivial getter: movzx eax, byte [rcx+0x11]
    			[Token(Token = "0x6000070")]
    			[Address(RVA = "0x542940", Offset = "0x541340", VA = "0x180542940")]
    			get
    			{
    				return _showUnderlay;
    			}
    		}

    		[Token(Token = "0x17000010")]
    		public LoadingIndicatorBase Indicator
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 -- shared trivial getter: mov rax, [rcx+0x18] (_indicator)
    			[Token(Token = "0x6000071")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _indicator;
    			}
    		}

    		[Token(Token = "0x17000011")]
    		public FloatUnityEvent OnScriptLoadProgressChanged
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x377910 -- shared trivial getter: mov rax, [rcx+0x20]
    			[Token(Token = "0x6000072")]
    			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    			get
    			{
    				return _onScriptLoadProgressChanged;
    			}
    		}

    		[Token(Token = "0x6000073")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public SettingsItem()
    		{
    		}
    	}

    	[Token(Token = "0x4000069")]
    	[FieldOffset(Offset = "0xE0")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _underlay;

    	[Token(Token = "0x400006A")]
    	[FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private LoadingIndicatorBase[] _indicators;

    	[Token(Token = "0x400006B")]
    	[FieldOffset(Offset = "0xF0")]
    	[Header("Parameters")]
    	[SerializeField]
    	private SettingsData[] _settingsData;

    	[Token(Token = "0x400006C")]
    	[FieldOffset(Offset = "0xF8")]
    	private IScriptLoader _naniScriptLoader;

    	[Token(Token = "0x400006D")]
    	[FieldOffset(Offset = "0x100")]
    	private Dictionary<LoadingMode, SettingsItem> _settings;

    	[Token(Token = "0x17000008")]
    	public LoadingMode CurrentMode
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3BC00 / 0xD3BC10 -- [CompilerGenerated] auto-property, backing
    		//   field +0x108 (read by BlockInputWhenVisible, HandleVisibilityChanged and HandleProgressChanged)
    		[Token(Token = "0x6000062")]
    		[Address(RVA = "0xD3BC00", Offset = "0xD3A600", VA = "0x180D3BC00")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x6000063")]
    		[Address(RVA = "0xD3BC10", Offset = "0xD3A610", VA = "0x180D3BC10", Slot = "95")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x17000009")]
    	public override bool BlockInputWhenVisible
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3BC20 -- get_Settings, IReadOnlyDictionary indexer with [this+0x108],
    		//   movzx eax, byte [item+0x10]
    		// Annotated disassembly: work/title-path/disas/lue_03_get_BlockInputWhenVisible.txt
    		[Token(Token = "0x6000064")]
    		[Address(RVA = "0xD3BC20", Offset = "0xD3A620", VA = "0x180D3BC20", Slot = "74")]
    		get
    		{
    			return Settings[CurrentMode].BlockInputWhenVisible;
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	private IScriptLoader NaniScriptLoader
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3BC80 -- lazy: [this+0xF8] tested, else 0x180D3BCE2
    		//   Engine.GetService<IScriptLoader>() stored back to +0xF8
    		// Annotated disassembly: work/title-path/disas/lue_04_get_NaniScriptLoader.txt
    		[Token(Token = "0x6000065")]
    		[Address(RVA = "0xD3BC80", Offset = "0xD3A680", VA = "0x180D3BC80")]
    		get
    		{
    			if (_naniScriptLoader == null)
    			{
    				_naniScriptLoader = Engine.GetService<IScriptLoader>();
    			}
    			return _naniScriptLoader;
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	private IReadOnlyDictionary<LoadingMode, SettingsItem> Settings
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3BD50 -- lazy: [this+0x100] tested, else Enumerable.ToDictionary over
    		//   _settingsData (+0xF0) with the two cached <>c lambdas (<>9__14_0 key = Mode, <>9__14_1 element = Item)
    		// Annotated disassembly: work/title-path/disas/lue_05_get_Settings.txt
    		[Token(Token = "0x6000066")]
    		[Address(RVA = "0xD3BD50", Offset = "0xD3A750", VA = "0x180D3BD50")]
    		get
    		{
    			if (_settings == null)
    			{
    				_settings = _settingsData.ToDictionary((SettingsData data) => data.Mode, (SettingsData data) => data.Item);
    			}
    			return _settings;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3C080 -- CustomUI.OnEnable, then IScriptLoader slot 0 add_OnLoadProgress
    	//   with a new Action<float>(HandleProgressChanged)
    	// Annotated disassembly: work/title-path/disas/lue_06_OnEnable.txt
    	[Token(Token = "0x6000067")]
    	[Address(RVA = "0xD3C080", Offset = "0xD3AA80", VA = "0x180D3C080", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		NaniScriptLoader.OnLoadProgress += HandleProgressChanged;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3C140 -- CustomUI.OnDisable, then IScriptLoader slot 1 remove_OnLoadProgress
    	// Annotated disassembly: work/title-path/disas/lue_07_OnDisable.txt
    	[Token(Token = "0x6000068")]
    	[Address(RVA = "0xD3C140", Offset = "0xD3AB40", VA = "0x180D3C140", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		NaniScriptLoader.OnLoadProgress -= HandleProgressChanged;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3C210, 904 B
    	// - Loop over _indicators (+0xE8): SetOpacity inlined (store to [indicator+0x20]._canvasGroup.alpha) with 1.0
    	//   (data 0x2AE2820) only when visible and the indicator is Settings[CurrentMode]._indicator (+0x18, inlined
    	//   UnityEngine.Object ==), else 0.
    	// - _underlay (+0xE0): CanvasGroupExtensions.Present / Dismiss (0xD53430 / 0xD53480) inlined into both branches of
    	//   `visible && Settings[CurrentMode]._showUnderlay` (+0x11) and tail-merged (alpha 1/0, interactable, blocksRaycasts).
    	// - CustomUI.HandleVisibilityChanged(visible) at 0x180D3C498.
    	// - The mode's indicator: vtable +0x188 = slot 5 OnShow when visible, +0x198 = slot 6 OnHide otherwise.
    	// - The mode's _onScriptLoadProgressChanged (+0x20) is reset with Invoke(0f) (r13d = 0) behind the shipped null test.
    	// Annotated disassembly: work/title-path/disas/lue_08_HandleVisibilityChanged.txt
    	[Token(Token = "0x6000069")]
    	[Address(RVA = "0xD3C210", Offset = "0xD3AC10", VA = "0x180D3C210", Slot = "55")]
    	protected override void HandleVisibilityChanged(bool visible)
    	{
    		LoadingIndicatorBase[] indicators = _indicators;
    		foreach (LoadingIndicatorBase indicator in indicators)
    		{
    			indicator.SetOpacity((visible && indicator == Settings[CurrentMode].Indicator) ? 1f : 0f);
    		}
    		if (visible && Settings[CurrentMode].ShowUnderlay)
    		{
    			_underlay.Present();
    		}
    		else
    		{
    			_underlay.Dismiss();
    		}
    		base.HandleVisibilityChanged(visible);
    		if (visible)
    		{
    			Settings[CurrentMode].Indicator.OnShow();
    		}
    		else
    		{
    			Settings[CurrentMode].Indicator.OnHide();
    		}
    		FloatUnityEvent onScriptLoadProgressChanged = Settings[CurrentMode].OnScriptLoadProgressChanged;
    		// Shipped null test: 0x180D3C551 test rcx, rcx / je past the Invoke.
    		if (onScriptLoadProgressChanged != null)
    		{
    			onScriptLoadProgressChanged.Invoke(0f);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3C5A0 -- Settings[CurrentMode]._onScriptLoadProgressChanged (+0x20),
    	//   UnityEvent<float>.Invoke(value) behind the shipped null test
    	// Annotated disassembly: work/title-path/disas/lue_09_HandleProgressChanged.txt
    	[Token(Token = "0x600006A")]
    	[Address(RVA = "0xD3C5A0", Offset = "0xD3AFA0", VA = "0x180D3C5A0")]
    	private void HandleProgressChanged(float value)
    	{
    		FloatUnityEvent onScriptLoadProgressChanged = Settings[CurrentMode].OnScriptLoadProgressChanged;
    		// Shipped null test: 0x180D3C60A test rcx, rcx / je past the Invoke.
    		if (onScriptLoadProgressChanged != null)
    		{
    			onScriptLoadProgressChanged.Invoke(value);
    		}
    	}

    	[Token(Token = "0x600006B")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public LoadingUiExtended()
    	{
    	}
    }
}
