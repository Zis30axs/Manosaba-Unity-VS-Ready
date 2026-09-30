using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200041D")]
    public class Script : ScriptableObject
    {
    	[Token(Token = "0x4000F0D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private string path;

    	[Token(Token = "0x4000F0E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private ScriptTextMap textMap;

    	[Token(Token = "0x4000F0F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private ScriptPlaylist playlist;

    	[Token(Token = "0x4000F10")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeReference]
    	private ScriptLine[] lines;

    	[Token(Token = "0x170002CA")]
    	public string Path
    	{
    		[Token(Token = "0x600140C")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002CB")]
    	public ScriptTextMap TextMap
    	{
    		[Token(Token = "0x600140D")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002CC")]
    	public IReadOnlyList<ScriptLine> Lines
    	{
    		[Token(Token = "0x600140E")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002CD")]
    	public ScriptPlaylist Playlist
    	{
    		[Token(Token = "0x600140F")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001410")]
    	[Address(RVA = "0x5B52D0", Offset = "0x5B3CD0", VA = "0x1805B52D0")]
    	public static Script Create(string path, ScriptLine[] lines, ScriptTextMap textMap)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001411")]
    	[Address(RVA = "0x5B56B0", Offset = "0x5B40B0", VA = "0x1805B56B0")]
    	public static Script FromText(string path, string text, [Optional] string file)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001412")]
    	[Address(RVA = "0x5B5890", Offset = "0x5B4290", VA = "0x1805B5890")]
    	public static Script FromTransient(string name, string text)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001413")]
    	[Address(RVA = "0x5B5A90", Offset = "0x5B4490", VA = "0x1805B5A90")]
    	public List<Command> ExtractCommands()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001414")]
    	public TLine FindLine<TLine>(Predicate<TLine> predicate) where TLine : ScriptLine
    	{
    		return null;
    	}

    	[Token(Token = "0x6001415")]
    	public List<TLine> FindLines<TLine>(Predicate<TLine> predicate) where TLine : ScriptLine
    	{
    		return null;
    	}

    	[Token(Token = "0x6001416")]
    	[Address(RVA = "0x5B5C50", Offset = "0x5B4650", VA = "0x1805B5C50")]
    	public bool LabelExists(string label)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001417")]
    	[Address(RVA = "0x5B5DE0", Offset = "0x5B47E0", VA = "0x1805B5DE0")]
    	public int GetLineIndexForLabel(string label)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001418")]
    	[Address(RVA = "0x5B5F70", Offset = "0x5B4970", VA = "0x1805B5F70")]
    	public string GetLabelForLine(int lineIndex)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001419")]
    	[Address(RVA = "0x5B6040", Offset = "0x5B4A40", VA = "0x1805B6040")]
    	public string GetCommentForLine(int lineIndex)
    	{
    		return null;
    	}

    	[Token(Token = "0x600141A")]
    	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
    	public Script()
    	{
    	}
    }
}
