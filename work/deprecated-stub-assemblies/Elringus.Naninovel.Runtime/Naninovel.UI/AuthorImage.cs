using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200058F")]
    [RequireComponent(typeof(CanvasGroup))]
    public class AuthorImage : ScriptableUIBehaviour
    {
    	[Token(Token = "0x400154B")]
    	[FieldOffset(Offset = "0x80")]
    	private ImageCrossfader crossfader;

    	[Token(Token = "0x6001B8F")]
    	[Address(RVA = "0x492520", Offset = "0x490F20", VA = "0x180492520", Slot = "59")]
    	public virtual UniTask ChangeTexture(Texture texture)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001B90")]
    	[Address(RVA = "0x4925C0", Offset = "0x490FC0", VA = "0x1804925C0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001B91")]
    	[Address(RVA = "0x4926F0", Offset = "0x4910F0", VA = "0x1804926F0", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001B92")]
    	[Address(RVA = "0x38A020", Offset = "0x388A20", VA = "0x18038A020")]
    	public AuthorImage()
    	{
    	}
    }
}
