using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200052D")]
    public class ContinueInputUI : CustomUI, IContinueInputUI, IManagedUI
    {
    	[Token(Token = "0x4001409")]
    	[FieldOffset(Offset = "0xE0")]
    	[SerializeField]
    	private GameObject trigger;

    	[Token(Token = "0x400140A")]
    	[FieldOffset(Offset = "0xE8")]
    	private IInputManager inputManager;

    	[Token(Token = "0x170003B3")]
    	protected GameObject Trigger
    	{
    		[Token(Token = "0x60019BA")]
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60019BB")]
    	[Address(RVA = "0x61E5F0", Offset = "0x61CFF0", VA = "0x18061E5F0", Slot = "83")]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60019BC")]
    	[Address(RVA = "0x61E6D0", Offset = "0x61D0D0", VA = "0x18061E6D0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019BD")]
    	[Address(RVA = "0x61E830", Offset = "0x61D230", VA = "0x18061E830", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x60019BE")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public ContinueInputUI()
    	{
    	}
    }
}
