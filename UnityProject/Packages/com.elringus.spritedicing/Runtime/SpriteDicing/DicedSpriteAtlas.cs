using System.Collections.Generic;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace SpriteDicing
{
    [Token(Token = "0x2000004")]
    [CreateAssetMenu]
    public class DicedSpriteAtlas : ScriptableObject
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	// The recovered constructor (RVA 0x63CB90, 0x18063CBF6 / 0x18063CC22) allocates both lists: field initializers, which the
    	// Il2CppDumper skeleton had dropped (an atlas created by the pipeline builder would otherwise start with null lists).
    	private List<Sprite> sprites = new List<Sprite>();

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private List<Texture2D> textures = new List<Texture2D>();

    	[Token(Token = "0x17000001")]
    	public IReadOnlyList<Sprite> Sprites
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 -- shared trivial getter: mov rax, [rcx+0x18] (`sprites`)
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return sprites;
    		}
    	}

    	[Token(Token = "0x17000002")]
    	public IReadOnlyList<Texture2D> Textures
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 -- shared trivial getter: mov rax, [rcx+0x20] (`textures`)
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return textures;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x63CA50, 304 B
    	// Linear search of `sprites` (+0x18): UnityEngine.Object.GetName (0x182073890) compared with `name` (reference test, then
    	// length + SequenceEqual = string equality); the first match is returned, no match -> null.  The list and the sprites are
    	// dereferenced without tests (0x18063CB7B is the NullReferenceException tail).
    	// Naninovel's DicedSprite actors call it with the appearance (`/` replaced by `.`): the atlas sprites carry the source names.
    	// Annotated disassembly: work/title-path/disas/sda_01_GetSprite.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0x63CA50", Offset = "0x63B450", VA = "0x18063CA50")]
    	[CanBeNull]
    	public Sprite GetSprite(string name)
    	{
    		for (int i = 0; i < sprites.Count; i++)
    		{
    			if (sprites[i].name == name)
    			{
    				return sprites[i];
    			}
    		}
    		return null;
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x63CB90", Offset = "0x63B590", VA = "0x18063CB90")]
    	public DicedSpriteAtlas()
    	{
    	}
    }
}
