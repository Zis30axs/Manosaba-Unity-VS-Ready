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
    	private List<Sprite> sprites;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private List<Texture2D> textures;

    	[Token(Token = "0x17000001")]
    	public IReadOnlyList<Sprite> Sprites
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000002")]
    	public IReadOnlyList<Texture2D> Textures
    	{
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0x63CA50", Offset = "0x63B450", VA = "0x18063CA50")]
    	[CanBeNull]
    	public Sprite GetSprite(string name)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x63CB90", Offset = "0x63B590", VA = "0x18063CB90")]
    	public DicedSpriteAtlas()
    	{
    	}
    }
}
