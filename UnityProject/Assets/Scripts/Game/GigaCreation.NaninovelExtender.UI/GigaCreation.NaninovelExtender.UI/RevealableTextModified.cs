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
    	private static readonly Regex s_captureRubyRegex;

    	[Token(Token = "0x400009F")]
    	[FieldOffset(Offset = "0x840")]
    	[Header("Parameters for RevealableTextModified")]
    	[SerializeField]
    	private float _marginTopWhenFirstLineHasRuby;

    	[Token(Token = "0x40000A0")]
    	[FieldOffset(Offset = "0x848")]
    	private readonly StringBuilder _stringBuilder;

    	[Token(Token = "0x17000017")]
    	private float AdditionalSpaceWidth
    	{
    		[Token(Token = "0x600009E")]
    		[Address(RVA = "0xD3F5A0", Offset = "0xD3DFA0", VA = "0x180D3F5A0")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x600009F")]
    	[Address(RVA = "0xD3F5E0", Offset = "0xD3DFE0", VA = "0x180D3F5E0", Slot = "160")]
    	protected override string ProcessRubyTags(string content)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000A0")]
    	[Address(RVA = "0xD40010", Offset = "0xD3EA10", VA = "0x180D40010")]
    	private void UpdateMargin(bool hasRubyInFirstLine)
    	{
    	}

    	[Token(Token = "0x60000A1")]
    	[Address(RVA = "0xD40100", Offset = "0xD3EB00", VA = "0x180D40100")]
    	private static int GetIndexOfEndOfFirstLine(string content)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60000A2")]
    	[Address(RVA = "0xD40300", Offset = "0xD3ED00", VA = "0x180D40300")]
    	private static string PrependLineHeightTagToAllLines(string content)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000A3")]
    	[Address(RVA = "0xD40380", Offset = "0xD3ED80", VA = "0x180D40380")]
    	public RevealableTextModified()
    	{
    	}
    }
}
