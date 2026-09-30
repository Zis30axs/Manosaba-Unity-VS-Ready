using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000308")]
    public static class TextureUtils
    {
    	[Token(Token = "0x6000E80")]
    	[Address(RVA = "0x553ED0", Offset = "0x5528D0", VA = "0x180553ED0")]
    	public static Texture2D CreateTexture(int width, int height, TextureWrapMode wrapMode = TextureWrapMode.Clamp, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmap = false, bool linear = false, string name = "")
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E81")]
    	[Address(RVA = "0x553FB0", Offset = "0x5529B0", VA = "0x180553FB0")]
    	public static Texture2D CreateTexture(int size, TextureWrapMode wrapMode = TextureWrapMode.Clamp, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmap = false, bool linear = false, string name = "")
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E82")]
    	[Address(RVA = "0x553FF0", Offset = "0x5529F0", VA = "0x180553FF0")]
    	public static Texture2D CreateTexture(int width, int height, Color[] pixelColors, TextureWrapMode wrapMode = TextureWrapMode.Clamp, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmap = false, bool linear = false, string name = "")
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E83")]
    	[Address(RVA = "0x554080", Offset = "0x552A80", VA = "0x180554080")]
    	public static Texture2D CreateTexture(int size, Color[] pixelColors, TextureWrapMode wrapMode = TextureWrapMode.Clamp, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmap = false, bool linear = false, string name = "")
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E84")]
    	[Address(RVA = "0x554100", Offset = "0x552B00", VA = "0x180554100")]
    	public static Color[] GetPixels(this Texture2D texture, Rect pixelsRect, [Optional] Color overboundColor)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E85")]
    	[Address(RVA = "0x554300", Offset = "0x552D00", VA = "0x180554300")]
    	public static Texture2D ToTexture2D(this RenderTexture renderTexture)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E86")]
    	[Address(RVA = "0x5545E0", Offset = "0x552FE0", VA = "0x1805545E0")]
    	public static void Clear(this RenderTexture renderTexture)
    	{
    	}

    	[Token(Token = "0x6000E87")]
    	[Address(RVA = "0x554780", Offset = "0x553180", VA = "0x180554780")]
    	public static void Clear(this RenderTexture renderTexture, bool clearDepth, bool clearColor, Color backgroundColor)
    	{
    	}
    }
}
