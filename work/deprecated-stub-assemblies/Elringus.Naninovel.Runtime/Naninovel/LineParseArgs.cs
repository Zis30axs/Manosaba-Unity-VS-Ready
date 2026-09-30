using System;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x2000411")]
    public readonly struct LineParseArgs<TModel> : IEquatable<LineParseArgs<TModel>> where TModel : IScriptLine
    {
    	[Token(Token = "0x4000EE5")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly string ScriptPath;

    	[Token(Token = "0x4000EE6")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly string LineText;

    	[Token(Token = "0x4000EE7")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly int LineIndex;

    	[Token(Token = "0x4000EE8")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly bool Transient;

    	[Token(Token = "0x4000EE9")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly TModel LineModel;

    	[Token(Token = "0x60013B9")]
    	public LineParseArgs(string scriptPath, string lineText, int lineIndex, bool transient, TModel lineModel)
    	{ this = default; }

    	[Token(Token = "0x60013BA")]
    	public bool Equals(LineParseArgs<TModel> other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013BB")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013BC")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
