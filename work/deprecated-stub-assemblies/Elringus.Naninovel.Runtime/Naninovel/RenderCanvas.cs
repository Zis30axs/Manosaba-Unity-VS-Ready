using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000065")]
    public class RenderCanvas : MonoBehaviour
    {
    	[Token(Token = "0x4000157")]
    	[FieldOffset(Offset = "0x20")]
    	public Vector2 Size;

    	[Token(Token = "0x4000158")]
    	[FieldOffset(Offset = "0x28")]
    	public Vector2 Offset;

    	[Token(Token = "0x17000047")]
    	public Rect Rect
    	{
    		[Token(Token = "0x60001AD")]
    		[Address(RVA = "0x456E00", Offset = "0x455800", VA = "0x180456E00")]
    		get
    		{
    			return default(Rect);
    		}
    	}

    	[Token(Token = "0x60001AE")]
    	[Address(RVA = "0x456E20", Offset = "0x455820", VA = "0x180456E20")]
    	private void OnDrawGizmos()
    	{
    	}

    	[Token(Token = "0x60001AF")]
    	[Address(RVA = "0x456F80", Offset = "0x455980", VA = "0x180456F80")]
    	public RenderCanvas()
    	{
    	}
    }
}
