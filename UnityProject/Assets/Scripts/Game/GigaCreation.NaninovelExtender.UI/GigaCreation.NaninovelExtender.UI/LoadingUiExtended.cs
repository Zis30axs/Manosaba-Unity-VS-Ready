using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GigaCreation.NaninovelExtender.Common;
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
    			[Token(Token = "0x600006C")]
    			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    			get
    			{
    				return default(LoadingMode);
    			}
    		}

    		[Token(Token = "0x1700000D")]
    		public SettingsItem Item
    		{
    			[Token(Token = "0x600006D")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return null;
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
    			[Token(Token = "0x600006F")]
    			[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x1700000F")]
    		public bool ShowUnderlay
    		{
    			[Token(Token = "0x6000070")]
    			[Address(RVA = "0x542940", Offset = "0x541340", VA = "0x180542940")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x17000010")]
    		public LoadingIndicatorBase Indicator
    		{
    			[Token(Token = "0x6000071")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x17000011")]
    		public FloatUnityEvent OnScriptLoadProgressChanged
    		{
    			[Token(Token = "0x6000072")]
    			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    			get
    			{
    				return null;
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
    		[Token(Token = "0x6000062")]
    		[Address(RVA = "0xD3BC00", Offset = "0xD3A600", VA = "0x180D3BC00")]
    		[CompilerGenerated]
    		get
    		{
    			return default(LoadingMode);
    		}
    		[Token(Token = "0x6000063")]
    		[Address(RVA = "0xD3BC10", Offset = "0xD3A610", VA = "0x180D3BC10", Slot = "95")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000009")]
    	public override bool BlockInputWhenVisible
    	{
    		[Token(Token = "0x6000064")]
    		[Address(RVA = "0xD3BC20", Offset = "0xD3A620", VA = "0x180D3BC20", Slot = "74")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	private IScriptLoader NaniScriptLoader
    	{
    		[Token(Token = "0x6000065")]
    		[Address(RVA = "0xD3BC80", Offset = "0xD3A680", VA = "0x180D3BC80")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	private IReadOnlyDictionary<LoadingMode, SettingsItem> Settings
    	{
    		[Token(Token = "0x6000066")]
    		[Address(RVA = "0xD3BD50", Offset = "0xD3A750", VA = "0x180D3BD50")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000067")]
    	[Address(RVA = "0xD3C080", Offset = "0xD3AA80", VA = "0x180D3C080", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6000068")]
    	[Address(RVA = "0xD3C140", Offset = "0xD3AB40", VA = "0x180D3C140", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6000069")]
    	[Address(RVA = "0xD3C210", Offset = "0xD3AC10", VA = "0x180D3C210", Slot = "55")]
    	protected override void HandleVisibilityChanged(bool visible)
    	{
    	}

    	[Token(Token = "0x600006A")]
    	[Address(RVA = "0xD3C5A0", Offset = "0xD3AFA0", VA = "0x180D3C5A0")]
    	private void HandleProgressChanged(float value)
    	{
    	}

    	[Token(Token = "0x600006B")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public LoadingUiExtended()
    	{
    	}
    }
}
