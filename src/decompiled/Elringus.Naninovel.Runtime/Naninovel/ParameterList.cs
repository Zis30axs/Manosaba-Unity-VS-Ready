using System.Collections;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000400")]
public abstract class ParameterList<TItem> : CommandParameter<List<TItem>> where TItem : class, new()
{
	[Token(Token = "0x170002A4")]
	public int Length
	{
		[Token(Token = "0x6001349")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x170002A5")]
	public TItem this[int index]
	{
		[Token(Token = "0x600134A")]
		get
		{
			return null;
		}
		[Token(Token = "0x600134B")]
		set
		{
		}
	}

	[Token(Token = "0x600134C")]
	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x600134D")]
	private IEnumerator<TItem> System_002ECollections_002EGeneric_002EIEnumerable_003CTItem_003E_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x600134E")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x600134F")]
	public TItem ElementAtOrNull(int index)
	{
		return null;
	}

	[Token(Token = "0x6001350")]
	protected override List<TItem> ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x6001351")]
	protected abstract TItem ParseItemValueText(string valueText, out string errors);

	[Token(Token = "0x6001352")]
	protected virtual string ItemToString(TItem item)
	{
		return null;
	}

	[Token(Token = "0x6001353")]
	protected ParameterList()
	{
	}
}
