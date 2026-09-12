using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20003FC")]
public abstract class NamedParameter<TValue, TNamedValue> : CommandParameter<TValue> where TValue : INamed<TNamedValue> where TNamedValue : class, INullableValue
{
	[Token(Token = "0x1700029C")]
	public string Name
	{
		[Token(Token = "0x6001333")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700029D")]
	public TNamedValue NamedValue
	{
		[Token(Token = "0x6001334")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001335")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6001336")]
	protected NamedParameter()
	{
	}
}
