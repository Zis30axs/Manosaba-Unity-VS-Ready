using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200034B")]
    public class ProjectStats : ScriptableObject
    {
    	[Token(Token = "0x4000C47")]
    	public const string ResourcesPath = "Naninovel/ProjectStats";

    	[Token(Token = "0x4000C48")]
    	[FieldOffset(Offset = "0x18")]
    	public int TotalScriptsCount;

    	[Token(Token = "0x4000C49")]
    	[FieldOffset(Offset = "0x1C")]
    	public int TotalCommandCount;

    	[Token(Token = "0x4000C4A")]
    	[FieldOffset(Offset = "0x20")]
    	public int TotalWordsCount;

    	[Token(Token = "0x6000FD6")]
    	[Address(RVA = "0x569D00", Offset = "0x568700", VA = "0x180569D00")]
    	public static ProjectStats GetOrDefault()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000FD7")]
    	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
    	public ProjectStats()
    	{
    	}
    }
}
