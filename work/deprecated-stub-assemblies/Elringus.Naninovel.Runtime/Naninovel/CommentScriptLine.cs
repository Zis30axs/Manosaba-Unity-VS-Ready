using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003E3")]
    public class CommentScriptLine : ScriptLine
    {
    	[Token(Token = "0x4000E9D")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private string commentText;

    	[Token(Token = "0x1700028E")]
    	public string CommentText
    	{
    		[Token(Token = "0x60012BD")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60012BE")]
    	[Address(RVA = "0x5A4A80", Offset = "0x5A3480", VA = "0x1805A4A80")]
    	public CommentScriptLine(string commentText, int lineIndex, int indent, string lineHash)
    	{
    	}
    }
}
