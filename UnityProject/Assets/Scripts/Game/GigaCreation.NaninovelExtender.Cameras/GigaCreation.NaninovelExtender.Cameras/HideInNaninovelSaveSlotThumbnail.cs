using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Cameras
{
    [Token(Token = "0x200000B")]
    public class HideInNaninovelSaveSlotThumbnail : MonoBehaviour
    {
    	[Token(Token = "0x400002A")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xCF77A0", Offset = "0xCF61A0", VA = "0x180CF77A0")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0xCF7840", Offset = "0xCF6240", VA = "0x180CF7840")]
    	public void SetCanvasEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x600002C")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public HideInNaninovelSaveSlotThumbnail()
    	{
    	}
    }
}
