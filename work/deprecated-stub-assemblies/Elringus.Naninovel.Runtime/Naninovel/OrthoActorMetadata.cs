using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000064")]
    public abstract class OrthoActorMetadata : ActorMetadata
    {
    	[Token(Token = "0x400014F")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Pivot point of the actor.")]
    	public Vector2 Pivot;

    	[Token(Token = "0x4000150")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("PPU value of the actor.")]
    	public float PixelsPerUnit;

    	[Token(Token = "0x4000151")]
    	[FieldOffset(Offset = "0x3C")]
    	[Tooltip("Whether to perform an additional render pass writing to z-buffer. Required for some effects (eg, depth of field).")]
    	public bool EnableDepthPass;

    	[Token(Token = "0x4000152")]
    	[FieldOffset(Offset = "0x40")]
    	[Range(0.001f, 0.999f)]
    	[Tooltip("When depth pass is enabled, controls the transparency level (alpha) threshold to cut off rendered pixels of the depth mask.")]
    	public float DepthAlphaCutoff;

    	[Token(Token = "0x4000153")]
    	[FieldOffset(Offset = "0x48")]
    	[Tooltip("A custom material (shader) to use when drawing and transitioning appearance textures of the actor. Be aware, that the shader is expected to have specific properties; check default shader (Naninovel/Resources/Naninovel/Shaders/TransitionalTexture) for a reference.")]
    	public Material CustomTextureMaterial;

    	[Token(Token = "0x4000154")]
    	[FieldOffset(Offset = "0x50")]
    	[Tooltip("A custom material (shader) to use when drawing sprite of the actor (in case it's represented by a sprite on scene). By default a simple unlit transparent shader is used; in case you want to support lighting or surface effects, assign a custom shader here.")]
    	public Material CustomSpriteMaterial;

    	[Token(Token = "0x4000155")]
    	[FieldOffset(Offset = "0x58")]
    	[Tooltip("When assigned and supported by the implementation, the actor will be rendered to the texture instead of a game object on scene.")]
    	public RenderTexture RenderTexture;

    	[Token(Token = "0x4000156")]
    	[FieldOffset(Offset = "0x60")]
    	[Tooltip("When rendering to texture, allows modifying area of the actor to render. X and Y are horizontal and vertical offsets from bottom-left corner, W and H are width and height scale of the area. Both parameters are relative, eg for 10% offset, use 0.1.\n\nExample: [X0 Y0.5 W1.5 H1.5] will render upper-half part of the actor uniformly stretched to the entire texture.")]
    	public Rect RenderRectangle;

    	[Token(Token = "0x60001AC")]
    	[Address(RVA = "0x456D80", Offset = "0x455780", VA = "0x180456D80")]
    	protected OrthoActorMetadata()
    	{
    	}
    }
}
