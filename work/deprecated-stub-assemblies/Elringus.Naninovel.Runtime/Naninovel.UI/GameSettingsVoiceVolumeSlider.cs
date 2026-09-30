using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x2000558")]
    public class GameSettingsVoiceVolumeSlider : ScriptableSlider
    {
    	[Token(Token = "0x4001482")]
    	[FieldOffset(Offset = "0x90")]
    	[Tooltip("When specified, the slider will control voice volume of the printed message author (character actor) with the specified ID. When empty will control voice mixer group volume.")]
    	[SerializeField]
    	private string authorId;

    	[Token(Token = "0x4001483")]
    	[FieldOffset(Offset = "0x98")]
    	private IAudioManager audioManager;

    	[Token(Token = "0x6001A89")]
    	[Address(RVA = "0x62CDC0", Offset = "0x62B7C0", VA = "0x18062CDC0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A8A")]
    	[Address(RVA = "0x62CE80", Offset = "0x62B880", VA = "0x18062CE80", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001A8B")]
    	[Address(RVA = "0x62CF60", Offset = "0x62B960", VA = "0x18062CF60", Slot = "62")]
    	protected override void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6001A8C")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public GameSettingsVoiceVolumeSlider()
    	{
    	}
    }
}
