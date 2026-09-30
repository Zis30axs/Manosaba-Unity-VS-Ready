using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200049E")]
    public class BlurFilter : IDisposable
    {
    	[Token(Token = "0x4001142")]
    	private const string shaderName = "Hidden/Naninovel/BlurFilter";

    	[Token(Token = "0x4001143")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly int intensityId;

    	[Token(Token = "0x4001144")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly Material blurMaterial;

    	[Token(Token = "0x4001145")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly int iterations;

    	[Token(Token = "0x4001146")]
    	[FieldOffset(Offset = "0x1C")]
    	private readonly bool downsample;

    	[Token(Token = "0x60016C4")]
    	[Address(RVA = "0x5E8720", Offset = "0x5E7120", VA = "0x1805E8720")]
    	public BlurFilter(int iterations, bool downsample)
    	{
    	}

    	[Token(Token = "0x60016C5")]
    	[Address(RVA = "0x5E88A0", Offset = "0x5E72A0", VA = "0x1805E88A0")]
    	public void BlurTexture(RenderTexture texture, float intensity)
    	{
    	}

    	[Token(Token = "0x60016C6")]
    	[Address(RVA = "0x5E8C40", Offset = "0x5E7640", VA = "0x1805E8C40", Slot = "4")]
    	public void Dispose()
    	{
    	}

    	[Token(Token = "0x60016C7")]
    	[Address(RVA = "0x5E8C50", Offset = "0x5E7650", VA = "0x1805E8C50")]
    	private static Material CreateBlurMaterial()
    	{
    		return null;
    	}

    	[Token(Token = "0x60016C8")]
    	[Address(RVA = "0x5E8CF0", Offset = "0x5E76F0", VA = "0x1805E8CF0")]
    	private RenderTexture GetTemporaryTexture(Texture source)
    	{
    		return null;
    	}

    	[Token(Token = "0x60016C9")]
    	[Address(RVA = "0x5E8D90", Offset = "0x5E7790", VA = "0x1805E8D90")]
    	private void CopySourceTexture(Texture source, RenderTexture target)
    	{
    	}
    }
}
