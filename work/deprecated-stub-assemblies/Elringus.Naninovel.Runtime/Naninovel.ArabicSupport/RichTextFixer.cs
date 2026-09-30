using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.ArabicSupport
{
    [Token(Token = "0x20004EF")]
    public static class RichTextFixer
    {
    	[Token(Token = "0x20004F0")]
    	private readonly struct Tag
    	{
    		[Token(Token = "0x4001345")]
    		[FieldOffset(Offset = "0x0")]
    		public readonly int Start;

    		[Token(Token = "0x4001346")]
    		[FieldOffset(Offset = "0x4")]
    		public readonly int End;

    		[Token(Token = "0x6001892")]
    		[Address(RVA = "0x525660", Offset = "0x524060", VA = "0x180525660")]
    		public Tag(int start, int end)
    		{ this = default; }
    	}

    	[Token(Token = "0x4001343")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly List<Tag> ClosedTags;

    	[Token(Token = "0x4001344")]
    	[FieldOffset(Offset = "0x8")]
    	private static readonly List<int> ClosedTagsHash;

    	[Token(Token = "0x600188F")]
    	[Address(RVA = "0x609F80", Offset = "0x608980", VA = "0x180609F80")]
    	public static void Fix(FastStringBuilder text)
    	{
    	}

    	[Token(Token = "0x6001890")]
    	[Address(RVA = "0x60A2E0", Offset = "0x608CE0", VA = "0x18060A2E0")]
    	public static void FindTag(FastStringBuilder str, int start, out int tagStart, out int tagEnd, out int tagType, out int hashCode)
    	{
    		tagStart = default;
    		tagEnd = default;
    		tagType = default;
    		hashCode = default;
    		tagStart = default;
    		tagEnd = default;
    		tagType = default;
    		hashCode = default;
    	}
    }
}
