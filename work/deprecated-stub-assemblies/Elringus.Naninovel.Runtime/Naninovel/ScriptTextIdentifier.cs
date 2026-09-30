using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000427")]
    public class ScriptTextIdentifier
    {
    	[Token(Token = "0x2000428")]
    	public readonly struct Options
    	{
    		[Token(Token = "0x4000F53")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public readonly int Revision;

    		[Token(Token = "0x4000F54")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public readonly string AssetPath;

    		[Token(Token = "0x600145E")]
    		[Address(RVA = "0x5BCE40", Offset = "0x5BB840", VA = "0x1805BCE40")]
    		public Options(int rev = 0, string path = default)
    		{ this = default; }
    	}

    	[Token(Token = "0x2000429")]
    	public readonly struct Result
    	{
    		[Token(Token = "0x4000F55")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public readonly IReadOnlyList<int> ModifiedLines;

    		[Token(Token = "0x4000F56")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public readonly int Revision;

    		[Token(Token = "0x600145F")]
    		[Address(RVA = "0x589040", Offset = "0x587A40", VA = "0x180589040")]
    		public Result(IReadOnlyList<int> lines, int rev)
    		{ this = default; }
    	}

    	[Token(Token = "0x4000F4B")]
    	public const string RefPrefix = "&";

    	[Token(Token = "0x4000F4C")]
    	public const string VolatilePrefix = "~";

    	[Token(Token = "0x4000F4D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly List<Action<string>> modifications;

    	[Token(Token = "0x4000F4E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly HashSet<string> existingIds;

    	[Token(Token = "0x4000F4F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly HashSet<int> modifiedLineIndexes;

    	[Token(Token = "0x4000F50")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private string scriptPath;

    	[Token(Token = "0x4000F51")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private Script script;

    	[Token(Token = "0x4000F52")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private int lineIndex;

    	[Token(Token = "0x6001453")]
    	[Address(RVA = "0x5BB500", Offset = "0x5B9F00", VA = "0x1805BB500")]
    	public Result Identify(Script script, Options options = default)
    	{
    		return default(Result);
    	}

    	[Token(Token = "0x6001454")]
    	[Address(RVA = "0x5BB7D0", Offset = "0x5BA1D0", VA = "0x1805BB7D0")]
    	private void Reset(Script script, string scriptPath)
    	{
    	}

    	[Token(Token = "0x6001455")]
    	[Address(RVA = "0x5BB960", Offset = "0x5BA360", VA = "0x1805BB960")]
    	private void ProcessLine(ScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001456")]
    	[Address(RVA = "0x5BBBC0", Offset = "0x5BA5C0", VA = "0x1805BBBC0")]
    	private void ProcessCommand(Command command)
    	{
    	}

    	[Token(Token = "0x6001457")]
    	[Address(RVA = "0x5BBDB0", Offset = "0x5BA7B0", VA = "0x1805BBDB0")]
    	private void ProcessParameter(ICommandParameter param)
    	{
    	}

    	[Token(Token = "0x6001458")]
    	[Address(RVA = "0x5BC0D0", Offset = "0x5BAAD0", VA = "0x1805BC0D0")]
    	private void ProcessValuePart(IReadOnlyList<RawValuePart> parts, int index)
    	{
    	}

    	[Token(Token = "0x6001459")]
    	[Address(RVA = "0x5BC650", Offset = "0x5BB050", VA = "0x1805BC650")]
    	private bool IsMissingStableId(RawValuePart part)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600145A")]
    	[Address(RVA = "0x5BC700", Offset = "0x5BB100", VA = "0x1805BC700")]
    	private string ResolveText(RawValuePart part)
    	{
    		return null;
    	}

    	[Token(Token = "0x600145B")]
    	[Address(RVA = "0x5BC770", Offset = "0x5BB170", VA = "0x1805BC770")]
    	private int ApplyModifications(int revision)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600145C")]
    	[Address(RVA = "0x5BC9E0", Offset = "0x5BB3E0", VA = "0x1805BC9E0")]
    	private void NotifyCollision(string id)
    	{
    	}

    	[Token(Token = "0x600145D")]
    	[Address(RVA = "0x5BCC50", Offset = "0x5BB650", VA = "0x1805BCC50")]
    	public ScriptTextIdentifier()
    	{
    	}
    }
}
