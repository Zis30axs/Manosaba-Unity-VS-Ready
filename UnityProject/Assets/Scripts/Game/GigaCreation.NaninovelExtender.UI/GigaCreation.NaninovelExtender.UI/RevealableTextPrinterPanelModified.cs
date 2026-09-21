using System.Linq;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000027")]
    public class RevealableTextPrinterPanelModified : RevealableTextPrinterPanel
    {
    	[Token(Token = "0x40000A1")]
    	[FieldOffset(Offset = "0x1F0")]
    	[Header("References for Modified Printer")]
    	[SerializeField]
    	private CanvasGroup _skipModeIndicatorCanvasGroup;

    	[Token(Token = "0x40000A2")]
    	[FieldOffset(Offset = "0x1F8")]
    	[SerializeField]
    	private FontMaterialChanger _fontMaterialChanger;

    	[Token(Token = "0x40000A3")]
    	[FieldOffset(Offset = "0x200")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x40000A4")]
    	[FieldOffset(Offset = "0x208")]
    	private string _activeAppearance;

    	[Token(Token = "0x17000018")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD40520 -- lazy: [this+0x200], else Engine.GetServiceOrErr<IScriptPlayer>()
    		[Token(Token = "0x60000A5")]
    		[Address(RVA = "0xD40520", Offset = "0xD3EF20", VA = "0x180D40520")]
    		get
    		{
    			if (_naniScriptPlayer == null)
    			{
    				_naniScriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
    			}
    			return _naniScriptPlayer;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD405F0 -- RevealableTextPrinterPanel.Awake (the stub skipped it, so
    	//   UITextPrinterPanel.scriptPlayer stayed null: NRE at UITextPrinterPanel.Initialize:63, G4b round 18), then
    	//   _skipModeIndicatorCanvasGroup (+0x1F0) alpha from IScriptPlayer slot 16 SkipActive
    	[Token(Token = "0x60000A6")]
    	[Address(RVA = "0xD405F0", Offset = "0xD3EFF0", VA = "0x180D405F0", Slot = "4")]
    	protected override void Awake()
    	{
    		base.Awake();
    		_skipModeIndicatorCanvasGroup.alpha = (NaniScriptPlayer.SkipActive ? 1f : 0f);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40690 -- RevealableTextPrinterPanel.OnEnable inlined (CustomUI.OnEnable +
    	//   CharacterManager slot 104 OnCharacterAvatarChanged += HandleAvatarChanged), then IScriptPlayer slot 10
    	//   add_OnAutoPlay(HandleAutoModeChanged) and slot 8 add_OnSkip(HandleSkipModeChanged)
    	// Annotated disassembly: work/title-path/disas/rtp_03_OnEnable.txt
    	[Token(Token = "0x60000A7")]
    	[Address(RVA = "0xD40690", Offset = "0xD3F090", VA = "0x180D40690", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		NaniScriptPlayer.OnAutoPlay += HandleAutoModeChanged;
    		NaniScriptPlayer.OnSkip += HandleSkipModeChanged;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40840 -- RevealableTextPrinterPanel.OnDisable inlined (CustomUI.OnDisable,
    	//   avatar handler removed when CharacterManager is set), then slot 11 remove_OnAutoPlay and slot 9 remove_OnSkip
    	// Annotated disassembly: work/title-path/disas/rtp_04_OnDisable.txt
    	[Token(Token = "0x60000A8")]
    	[Address(RVA = "0xD40840", Offset = "0xD3F240", VA = "0x180D40840", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		NaniScriptPlayer.OnAutoPlay -= HandleAutoModeChanged;
    		NaniScriptPlayer.OnSkip -= HandleSkipModeChanged;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40A10 -- the base body (InputIndicator slot 119, IInputIndicator slot 2
    	//   Show / slot 3 Hide, slots 122 / 129) with one addition: nothing is shown while IScriptPlayer slot 17
    	//   AutoPlayActive
    	// Annotated disassembly: work/title-path/disas/rtp_05_SetWaitForInputIndicatorVisible.txt
    	[Token(Token = "0x60000A9")]
    	[Address(RVA = "0xD40A10", Offset = "0xD3F410", VA = "0x180D40A10", Slot = "112")]
    	public override void SetWaitForInputIndicatorVisible(bool visible)
    	{
    		if (visible)
    		{
    			if (!NaniScriptPlayer.AutoPlayActive)
    			{
    				InputIndicator.Show();
    				if (PositionIndicatorOverText)
    				{
    					PlaceInputIndicatorOverText();
    				}
    			}
    		}
    		else
    		{
    			InputIndicator.Hide();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40B30 -- RevealableTextPrinterPanel.SetFont inlined (FontChanger.ChangeFont over
    	//   slot 79 FontChangeConfigurations, then the indicator re-placed); every text of those configurations
    	//   (<>c.<SetFont>b__10_0 = config.Components, SelectMany) gets _fontMaterialChanger (+0x1F8)
    	//   .ChangeFontMaterialAsync(text, destroyCancellationToken) with Cysharp Forget()
    	// Annotated disassembly: work/title-path/disas/rtp_06_SetFont.txt
    	[Token(Token = "0x60000AA")]
    	[Address(RVA = "0xD40B30", Offset = "0xD3F530", VA = "0x180D40B30", Slot = "84")]
    	public override void SetFont(TMP_FontAsset font)
    	{
    		base.SetFont(font);
    		foreach (TMP_Text text in FontChangeConfigurations.SelectMany((FontChangeConfiguration config) => config.Components))
    		{
    			_fontMaterialChanger.ChangeFontMaterialAsync(text, base.destroyCancellationToken).Forget();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40EF0 -- [this+0x208], set to "Default" when null, returned
    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0xD40EF0", Offset = "0xD3F8F0", VA = "0x180D40EF0", Slot = "130")]
    	protected override string GetActiveAppearance()
    	{
    		if (_activeAppearance == null)
    		{
    			_activeAppearance = "Default";
    		}
    		return _activeAppearance;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40F90
    	// - string.IsNullOrWhiteSpace inlined (Char.IsWhiteSpace loop); a blank appearance becomes "Default" (+0x208).
    	// - Appearances (vtable +0x8E8, slot 123, List<CanvasGroup>) empty -> nothing else.
    	// - Each group: alpha 1.0 when any '-'-separated part of its name (Object.name) equals the active appearance
    	//   (<SetActiveAppearance>b__12_0), else 0.
    	// Annotated disassembly: work/title-path/disas/rtp_08_SetActiveAppearance.txt
    	[Token(Token = "0x60000AC")]
    	[Address(RVA = "0xD40F90", Offset = "0xD3F990", VA = "0x180D40F90", Slot = "131")]
    	protected override void SetActiveAppearance(string appearance)
    	{
    		_activeAppearance = (string.IsNullOrWhiteSpace(appearance) ? "Default" : appearance);
    		if (Appearances.Count == 0)
    		{
    			return;
    		}
    		foreach (CanvasGroup appearanceGroup in Appearances)
    		{
    			appearanceGroup.alpha = (appearanceGroup.name.Split('-').Any((string part) => part == _activeAppearance) ? 1f : 0f);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD41330 -- enable: IScriptPlayer slot 34 SetSkipEnabled(false); disable while
    	//   slot 18 WaitingForInput: vtable +0x838 (slot 112) SetWaitForInputIndicatorVisible(true)
    	[Token(Token = "0x60000AD")]
    	[Address(RVA = "0xD41330", Offset = "0xD3FD30", VA = "0x180D41330")]
    	private void HandleAutoModeChanged(bool enable)
    	{
    		if (enable)
    		{
    			NaniScriptPlayer.SetSkipEnabled(false);
    		}
    		else if (NaniScriptPlayer.WaitingForInput)
    		{
    			SetWaitForInputIndicatorVisible(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD413E0 -- +0x1F0 alpha 1.0 or 0, tail jump set_alpha
    	[Token(Token = "0x60000AE")]
    	[Address(RVA = "0xD413E0", Offset = "0xD3FDE0", VA = "0x180D413E0")]
    	private void HandleSkipModeChanged(bool enable)
    	{
    		_skipModeIndicatorCanvasGroup.alpha = (enable ? 1f : 0f);
    	}

    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0x33FA80", Offset = "0x33E480", VA = "0x18033FA80")]
    	public RevealableTextPrinterPanelModified()
    	{
    	}
    }
}
