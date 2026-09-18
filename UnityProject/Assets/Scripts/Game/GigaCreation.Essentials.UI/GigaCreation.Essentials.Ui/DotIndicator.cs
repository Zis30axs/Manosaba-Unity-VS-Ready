using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000033")]
    public class DotIndicator : MonoBehaviour
    {
    	[Token(Token = "0x40000B9")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Image _image;

    	[Token(Token = "0x40000BA")]
    	[FieldOffset(Offset = "0x28")]
    	[Space]
    	[SerializeField]
    	private Sprite _enable;

    	[Token(Token = "0x40000BB")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Sprite _disable;

    	[Token(Token = "0x60000E7")]
    	[Address(RVA = "0x30E870", Offset = "0x30D270", VA = "0x18030E870")]
    	private void OnDestroy()
    	{
    	}

    	[Token(Token = "0x60000E8")]
    	[Address(RVA = "0xCE5B60", Offset = "0xCE4560", VA = "0x180CE5B60")]
    	public void ChangeSpriteTo(bool isEnabled)
    	{
    	}

    	[Token(Token = "0x60000E9")]
    	[Address(RVA = "0xCE5B90", Offset = "0xCE4590", VA = "0x180CE5B90")]
    	public void Deactivate()
    	{
    	}

    	[Token(Token = "0x60000EA")]
    	[Address(RVA = "0xCE5BC0", Offset = "0xCE45C0", VA = "0x180CE5BC0")]
    	public void Activate()
    	{
    	}

    	[Token(Token = "0x60000EB")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DotIndicator()
    	{
    	}
    }
}
