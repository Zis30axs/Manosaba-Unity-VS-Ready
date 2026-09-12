using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200021A")]
public abstract class NullableNamed<TNamed, TNamedValue> : Nullable<TNamed> where TNamed : INamed<TNamedValue> where TNamedValue : class
{
	[Token(Token = "0x17000158")]
	public string Name
	{
		[Token(Token = "0x6000960")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000159")]
	public TNamedValue NamedValue
	{
		[Token(Token = "0x6000961")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000962")]
	protected NullableNamed()
	{
	}
}
