using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel;

[Token(Token = "0x2000419")]
public abstract class ScriptLineParser<TResult, TModel> where TResult : ScriptLine where TModel : IScriptLine
{
	[Token(Token = "0x170002BF")]
	protected virtual string ScriptPath
	{
		[Token(Token = "0x60013DF")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60013E0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170002C0")]
	protected virtual int LineIndex
	{
		[Token(Token = "0x60013E1")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x60013E2")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170002C1")]
	protected virtual string LineHash
	{
		[Token(Token = "0x60013E3")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60013E4")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170002C2")]
	protected virtual bool Transient
	{
		[Token(Token = "0x60013E5")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60013E6")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x60013E7")]
	public static string GetHash(string lineText)
	{
		return null;
	}

	[Token(Token = "0x60013E8")]
	public virtual TResult Parse(LineParseArgs<TModel> args)
	{
		return null;
	}

	[Token(Token = "0x60013E9")]
	protected abstract TResult Parse(TModel lineModel);

	[Token(Token = "0x60013EA")]
	protected ScriptLineParser()
	{
	}
}
