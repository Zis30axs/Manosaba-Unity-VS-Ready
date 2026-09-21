using System;
using System.Text;
using System.Text.RegularExpressions;
using Il2CppDummyDll;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000026")]
    public class RevealableTextModified : RevealableText
    {
    	[Token(Token = "0x400009C")]
    	private const string RubyLinkId = "NANINOVEL.RUBY";

    	[Token(Token = "0x400009D")]
    	private const string LineBreakTag = "<br>";

    	[Token(Token = "0x400009E")]
    	[FieldOffset(Offset = "0x0")]
    	// RECONSTRUCTED | Confidence: A | static initializer from the .cctor at 0xD40440: pattern string literal
    	//   <ruby="([\s\S]*?)">([\s\S]*?)</ruby>, RegexOptions 8 = Compiled
    	private static readonly Regex s_captureRubyRegex = new Regex(@"<ruby=""([\s\S]*?)"">([\s\S]*?)</ruby>", RegexOptions.Compiled);

    	[Token(Token = "0x400009F")]
    	[FieldOffset(Offset = "0x840")]
    	[Header("Parameters for RevealableTextModified")]
    	[SerializeField]
    	private float _marginTopWhenFirstLineHasRuby;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xD40380: new StringBuilder() -> +0x848, then RevealableText..ctor
    	[Token(Token = "0x40000A0")]
    	[FieldOffset(Offset = "0x848")]
    	private readonly StringBuilder _stringBuilder = new StringBuilder();

    	[Token(Token = "0x17000017")]
    	private float AdditionalSpaceWidth
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3F5A0 -- m_fontSize (+0x214) / 100.0 (data 0x2AE2BC4) *
    		//   m_fontAsset (+0x100).normalSpacingOffset (+0x1A4)
    		[Token(Token = "0x600009E")]
    		[Address(RVA = "0xD3F5A0", Offset = "0xD3DFA0", VA = "0x180D3F5A0")]
    		get
    		{
    			return m_fontSize / 100f * m_fontAsset.normalSpacingOffset;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (Clear() vs. Length = 0) | RVA: 0xD3F5E0, 580 instructions
    	// - s_captureRubyRegex.Matches; ruby in the first line (the first match's Index before the first <br>) -> UpdateMargin
    	//   inlined (margin.y = _marginTopWhenFirstLineHasRuby or 0); _stringBuilder (+0x848) length 0.
    	// - Per match with 3 groups: preferred widths of the base text and of `<size={RubySizeScale*100}%>{ruby}</size>`
    	//   (vtable +0xA58 slot 146); offset (ruby + base) * 0.5 (data 0x2AE2728) plus AdditionalSpaceWidth (inlined),
    	//   compensation (base - ruby) * 0.5; the text since the previous match, `<line-height=100%>` unless slot 147
    	//   AddRubyLineHeight, then the base text and the ruby markup with slot 145 RubyVerticalOffset and
    	//   "NANINOVEL.RUBY"; the cursor moves past the match.
    	// - The rest of the content, then PrependLineHeightTagToAllLines inlined (Regex.Replace).
    	//   The stub returned null, which NaninovelTMProText.ProcessEventTags passed to Regex.Matches (G4b round 18).
    	// Annotated disassembly: work/title-path/disas/rtm_02_ProcessRubyTags.txt
    	[Token(Token = "0x600009F")]
    	[Address(RVA = "0xD3F5E0", Offset = "0xD3DFE0", VA = "0x180D3F5E0", Slot = "160")]
    	protected override string ProcessRubyTags(string content)
    	{
    		MatchCollection matches = s_captureRubyRegex.Matches(content);
    		UpdateMargin(matches.Count > 0 && matches[0].Index < GetIndexOfEndOfFirstLine(content));
    		_stringBuilder.Clear();
    		int lastIndex = 0;
    		foreach (Match match in matches)
    		{
    			if (match.Groups.Count != 3)
    			{
    				continue;
    			}
    			string rubyValue = match.Groups[1].ToString();
    			string baseText = match.Groups[2].ToString();
    			Vector2 baseTextSize = GetPreferredValues(baseText);
    			Vector2 rubyTextSize = GetPreferredValues(string.Format("<size={0}%>{1}</size>", RubySizeScale * 100f, rubyValue));
    			float additionalSpaceWidth = AdditionalSpaceWidth;
    			float rubyTextOffset = (rubyTextSize.x + baseTextSize.x) * 0.5f;
    			float compensationOffset = (baseTextSize.x - rubyTextSize.x) * 0.5f;
    			_stringBuilder.Append(content.Substring(lastIndex, match.Index - lastIndex));
    			if (!AddRubyLineHeight)
    			{
    				_stringBuilder.Append("<line-height=100%>");
    			}
    			_stringBuilder.Append(baseText).Append("<space=-").Append(rubyTextOffset + additionalSpaceWidth)
    				.Append("><voffset=").Append(RubyVerticalOffset)
    				.Append("><size=").Append(RubySizeScale * 100f)
    				.Append("%><link=\"").Append(RubyLinkId).Append("\">")
    				.Append(rubyValue)
    				.Append("</link></size></voffset><space=").Append(compensationOffset)
    				.Append(">");
    			lastIndex = match.Index + match.Length;
    		}
    		_stringBuilder.Append(content.Substring(lastIndex, content.Length - lastIndex));
    		return PrependLineHeightTagToAllLines(_stringBuilder.ToString());
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40010 -- top = +0x840 or 0; three margin getter calls (vtable +0x5C8, slot 73)
    	//   for x / z / w and one setter (+0x5D8, slot 74)
    	[Token(Token = "0x60000A0")]
    	[Address(RVA = "0xD40010", Offset = "0xD3EA10", VA = "0x180D40010")]
    	private void UpdateMargin(bool hasRubyInFirstLine)
    	{
    		float top = (hasRubyInFirstLine ? _marginTopWhenFirstLineHasRuby : 0f);
    		margin = new Vector4(margin.x, top, margin.z, margin.w);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40100 -- CompareInfo.IndexOfOrdinal(content, "<br>", 0, Length, false);
    	//   -1 -> content.Length
    	[Token(Token = "0x60000A1")]
    	[Address(RVA = "0xD40100", Offset = "0xD3EB00", VA = "0x180D40100")]
    	private static int GetIndexOfEndOfFirstLine(string content)
    	{
    		int index = content.IndexOf(LineBreakTag, StringComparison.Ordinal);
    		return (index == -1) ? content.Length : index;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD40300 -- static Regex.Replace(content, pattern, replacement)
    	[Token(Token = "0x60000A2")]
    	[Address(RVA = "0xD40300", Offset = "0xD3ED00", VA = "0x180D40300")]
    	private static string PrependLineHeightTagToAllLines(string content)
    	{
    		return Regex.Replace(content, "(^|<br>)(?!<line-height=100%>)", "$1<line-height=100%>");
    	}

    	[Token(Token = "0x60000A3")]
    	[Address(RVA = "0xD40380", Offset = "0xD3ED80", VA = "0x180D40380")]
    	public RevealableTextModified()
    	{
    	}
    }
}
