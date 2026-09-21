using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x200000B")]
    public class GameStateSlotExtended : GameStateSlot
    {
    	[Token(Token = "0x4000019")]
    	[FieldOffset(Offset = "0x120")]
    	[Space]
    	[SerializeField]
    	private CanvasGroup _thumbnailCanvasGroup;

    	[Token(Token = "0x400001A")]
    	[FieldOffset(Offset = "0x128")]
    	[SerializeField]
    	private CanvasGroup _loadingIconCanvasGroup;

    	[Token(Token = "0x400001B")]
    	[FieldOffset(Offset = "0x130")]
    	[SerializeField]
    	private Transform _loadingIconTransform;

    	[Token(Token = "0x400001C")]
    	[FieldOffset(Offset = "0x138")]
    	[SerializeField]
    	private CanvasGroup _noDataLabel;

    	[Token(Token = "0x400001D")]
    	[FieldOffset(Offset = "0x140")]
    	[Space]
    	[SerializeField]
    	private float _fadeDuration;

    	[Token(Token = "0x400001E")]
    	[FieldOffset(Offset = "0x144")]
    	[SerializeField]
    	private float _rollDuration;

    	[Token(Token = "0x400001F")]
    	[FieldOffset(Offset = "0x148")]
    	private MotionHandle _fadeMotion;

    	[Token(Token = "0x4000020")]
    	[FieldOffset(Offset = "0x154")]
    	private MotionHandle _rollMotion;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD36A30 -- GameStateSlot.Awake, then alpha 0 on +0x120, +0x128, +0x138
    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xD36A30", Offset = "0xD35430", VA = "0x180D36A30", Slot = "4")]
    	protected override void Awake()
    	{
    		base.Awake();
    		_thumbnailCanvasGroup.alpha = 0f;
    		_loadingIconCanvasGroup.alpha = 0f;
    		_noDataLabel.alpha = 0f;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD36A90 -- CancelAllMotions, then GameStateSlot.OnDestroy inlined
    	//   (DeleteButton slot 79 onClick.RemoveListener(HandleDeleteButtonClicked, slot 89); l10n (+0x118) tested,
    	//   OnLocaleChanged -= HandleLocaleChanged (slot 91))
    	// Annotated disassembly: work/title-path/disas/gse_02_0xD36A90.txt
    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xD36A90", Offset = "0xD35490", VA = "0x180D36A90", Slot = "8")]
    	protected override void OnDestroy()
    	{
    		CancelAllMotions();
    		base.OnDestroy();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD36BF0 -- GameStateSlot.SetEmptyState inlined (DeleteButton inactive,
    	//   TitleTemplate {N}/{D} -> SetTitleText slot 88, ThumbnailImage.texture = EmptySlotThumbnail), then alpha 0 on
    	//   +0x120 and 1.0 on +0x138
    	// Annotated disassembly: work/title-path/disas/gse_03_0xD36BF0.txt
    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xD36BF0", Offset = "0xD355F0", VA = "0x180D36BF0", Slot = "86")]
    	protected override void SetEmptyState(int slotNumber)
    	{
    		base.SetEmptyState(slotNumber);
    		_thumbnailCanvasGroup.alpha = 0f;
    		_noDataLabel.alpha = 1f;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD36DB0 -- GameStateSlot.SetNonEmptyState, then alpha 1.0 on +0x120 and 0 on +0x138
    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xD36DB0", Offset = "0xD357B0", VA = "0x180D36DB0", Slot = "87")]
    	protected override void SetNonEmptyState(int slotNumber, GameStateMap state)
    	{
    		base.SetNonEmptyState(slotNumber, state);
    		_thumbnailCanvasGroup.alpha = 1f;
    		_noDataLabel.alpha = 0f;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD36E00 -- CancelAllMotions; alpha 0 on +0x120 / +0x138; DeleteButton (slot 79)
    	//   inactive; LMotion 0 -> 1 over _fadeDuration (+0x140) BindToAlpha(+0x128) AddTo(this) -> +0x148; LMotion 0 -> 360
    	//   over _rollDuration (+0x144) WithLoops(-1, Restart) WithScheduler(MotionScheduler static +0x60,
    	//   UpdateIgnoreTimeScale) Bind(SetRoll) AddTo(this) -> +0x154
    	// Annotated disassembly: work/title-path/disas/gse_05_0xD36E00.txt
    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0xD36E00", Offset = "0xD35800", VA = "0x180D36E00")]
    	public void OnBeginToLoad()
    	{
    		CancelAllMotions();
    		_thumbnailCanvasGroup.alpha = 0f;
    		_noDataLabel.alpha = 0f;
    		DeleteButton.gameObject.SetActive(false);
    		_fadeMotion = LMotion.Create(0f, 1f, _fadeDuration).BindToAlpha(_loadingIconCanvasGroup).AddTo(this);
    		_rollMotion = LMotion.Create(0f, 360f, _rollDuration).WithLoops(-1).WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
    			.Bind(SetRoll)
    			.AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD371B0 -- CancelAllMotions; LMotion 1 -> 0 over +0x140 BindToAlpha(+0x128)
    	//   AddTo(this) -> +0x148
    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xD371B0", Offset = "0xD35BB0", VA = "0x180D371B0")]
    	public void OnEndToLoad()
    	{
    		CancelAllMotions();
    		_fadeMotion = LMotion.Create(1f, 0f, _fadeDuration).BindToAlpha(_loadingIconCanvasGroup).AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD372C0 -- as LoadingIndicatorRolling.SetRoll on _loadingIconTransform (+0x130):
    	//   180.0 (0x2AE2C08), 360.0 (0x2AE2C40), sign mask 0x2AE3A40; only the z Euler angle is replaced
    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0xD372C0", Offset = "0xD35CC0", VA = "0x180D372C0")]
    	private void SetRoll(float value)
    	{
    		float z = ((value < 180f) ? (0f - value) : (360f - value));
    		Vector3 localEulerAngles = _loadingIconTransform.localEulerAngles;
    		_loadingIconTransform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, z);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD37360 -- per handle (+0x148, +0x154): IsActive (0xCD57C0) then Cancel
    	//   (0xCD58F0), as Overlay's `if (_motion.IsActive()) _motion.Cancel();`
    	[Token(Token = "0x600002C")]
    	[Address(RVA = "0xD37360", Offset = "0xD35D60", VA = "0x180D37360")]
    	private void CancelAllMotions()
    	{
    		if (_fadeMotion.IsActive())
    		{
    			_fadeMotion.Cancel();
    		}
    		if (_rollMotion.IsActive())
    		{
    			_rollMotion.Cancel();
    		}
    	}

    	[Token(Token = "0x600002D")]
    	[Address(RVA = "0xD37410", Offset = "0xD35E10", VA = "0x180D37410")]
    	public GameStateSlotExtended()
    	{
    	}
    }
}
