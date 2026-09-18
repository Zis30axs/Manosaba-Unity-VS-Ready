using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.General
{
    [Token(Token = "0x2000008")]
    public class TimeDebugCommands : MonoBehaviour
    {
    	[Token(Token = "0x400000F")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private FloatReactiveProperty _timeScale;

    	[Token(Token = "0x4000010")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private bool _withCommandKey;

    	[Token(Token = "0x4000011")]
    	[FieldOffset(Offset = "0x29")]
    	[SerializeField]
    	private bool _withShiftKey;

    	[Token(Token = "0x4000012")]
    	[FieldOffset(Offset = "0x2A")]
    	[SerializeField]
    	private bool _withOptionKey;

    	[Token(Token = "0x4000013")]
    	[FieldOffset(Offset = "0x2B")]
    	[SerializeField]
    	private bool _withControlKey;

    	[Token(Token = "0x4000014")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables;

    	[Token(Token = "0x4000015")]
    	[FieldOffset(Offset = "0x38")]
    	private float _storedTimeScale;

    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0xD43470", Offset = "0xD41E70", VA = "0x180D43470")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0xD435C0", Offset = "0xD41FC0", VA = "0x180D435C0")]
    	private void OnUpdateInDebugMode()
    	{
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xD43810", Offset = "0xD42210", VA = "0x180D43810")]
    	private bool AreModifierKeysPressed()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xD43B20", Offset = "0xD42520", VA = "0x180D43B20")]
    	public void SpeedUp(float num)
    	{
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xD43BE0", Offset = "0xD425E0", VA = "0x180D43BE0")]
    	public void SlowDown(float num)
    	{
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xD43CF0", Offset = "0xD426F0", VA = "0x180D43CF0")]
    	public void TogglePause()
    	{
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xD43E20", Offset = "0xD42820", VA = "0x180D43E20")]
    	public TimeDebugCommands()
    	{
    	}
    }
}
