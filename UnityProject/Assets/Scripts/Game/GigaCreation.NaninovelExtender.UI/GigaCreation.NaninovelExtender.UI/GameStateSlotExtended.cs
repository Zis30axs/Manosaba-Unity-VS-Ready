using Il2CppDummyDll;
using LitMotion;
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

    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xD36A30", Offset = "0xD35430", VA = "0x180D36A30", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xD36A90", Offset = "0xD35490", VA = "0x180D36A90", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xD36BF0", Offset = "0xD355F0", VA = "0x180D36BF0", Slot = "86")]
    	protected override void SetEmptyState(int slotNumber)
    	{
    	}

    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xD36DB0", Offset = "0xD357B0", VA = "0x180D36DB0", Slot = "87")]
    	protected override void SetNonEmptyState(int slotNumber, GameStateMap state)
    	{
    	}

    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0xD36E00", Offset = "0xD35800", VA = "0x180D36E00")]
    	public void OnBeginToLoad()
    	{
    	}

    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xD371B0", Offset = "0xD35BB0", VA = "0x180D371B0")]
    	public void OnEndToLoad()
    	{
    	}

    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0xD372C0", Offset = "0xD35CC0", VA = "0x180D372C0")]
    	private void SetRoll(float value)
    	{
    	}

    	[Token(Token = "0x600002C")]
    	[Address(RVA = "0xD37360", Offset = "0xD35D60", VA = "0x180D37360")]
    	private void CancelAllMotions()
    	{
    	}

    	[Token(Token = "0x600002D")]
    	[Address(RVA = "0xD37410", Offset = "0xD35E10", VA = "0x180D37410")]
    	public GameStateSlotExtended()
    	{
    	}
    }
}
