using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004F3")]
public class FastStringBuilder
{
	[Token(Token = "0x4001349")]
	[FieldOffset(Offset = "0x10")]
	public int Length;

	[Token(Token = "0x400134A")]
	[FieldOffset(Offset = "0x18")]
	private char[] array;

	[Token(Token = "0x400134B")]
	[FieldOffset(Offset = "0x20")]
	private int capacity;

	[Token(Token = "0x6001897")]
	[Address(RVA = "0x60BC50", Offset = "0x60A650", VA = "0x18060BC50")]
	public FastStringBuilder(int capacity)
	{
	}

	[Token(Token = "0x6001898")]
	[Address(RVA = "0x60BD40", Offset = "0x60A740", VA = "0x18060BD40")]
	public FastStringBuilder(string text)
	{
	}

	[Token(Token = "0x6001899")]
	[Address(RVA = "0x60BDD0", Offset = "0x60A7D0", VA = "0x18060BDD0")]
	public FastStringBuilder(string text, int capacity)
	{
	}

	[Token(Token = "0x600189A")]
	[Address(RVA = "0x60BE60", Offset = "0x60A860", VA = "0x18060BE60")]
	public char Get(int index)
	{
		return default(char);
	}

	[Token(Token = "0x600189B")]
	[Address(RVA = "0x60BE90", Offset = "0x60A890", VA = "0x18060BE90")]
	public void Set(int index, char ch)
	{
	}

	[Token(Token = "0x600189C")]
	[Address(RVA = "0x60BEC0", Offset = "0x60A8C0", VA = "0x18060BEC0")]
	public void SetValue(string text)
	{
	}

	[Token(Token = "0x600189D")]
	[Address(RVA = "0x60BF40", Offset = "0x60A940", VA = "0x18060BF40")]
	public void SetValue(FastStringBuilder other)
	{
	}

	[Token(Token = "0x600189E")]
	[Address(RVA = "0x60BFD0", Offset = "0x60A9D0", VA = "0x18060BFD0")]
	public void Append(char ch)
	{
	}

	[Token(Token = "0x600189F")]
	[Address(RVA = "0x60C030", Offset = "0x60AA30", VA = "0x18060C030")]
	public void Insert(int pos, FastStringBuilder str, int offset, int count)
	{
	}

	[Token(Token = "0x60018A0")]
	[Address(RVA = "0x60C1D0", Offset = "0x60ABD0", VA = "0x18060C1D0")]
	public void Insert(int pos, FastStringBuilder str)
	{
	}

	[Token(Token = "0x60018A1")]
	[Address(RVA = "0x60C200", Offset = "0x60AC00", VA = "0x18060C200")]
	public void Insert(int pos, char ch)
	{
	}

	[Token(Token = "0x60018A2")]
	[Address(RVA = "0x60C2B0", Offset = "0x60ACB0", VA = "0x18060C2B0")]
	public void RemoveAll(char character)
	{
	}

	[Token(Token = "0x60018A3")]
	[Address(RVA = "0x60C340", Offset = "0x60AD40", VA = "0x18060C340")]
	public void Remove(int start, int length)
	{
	}

	[Token(Token = "0x60018A4")]
	[Address(RVA = "0x60C3C0", Offset = "0x60ADC0", VA = "0x18060C3C0")]
	public void Reverse(int startIndex, int length)
	{
	}

	[Token(Token = "0x60018A5")]
	[Address(RVA = "0x60C480", Offset = "0x60AE80", VA = "0x18060C480")]
	public void Reverse()
	{
	}

	[Token(Token = "0x60018A6")]
	[Address(RVA = "0x60C490", Offset = "0x60AE90", VA = "0x18060C490")]
	public void Substring(FastStringBuilder output, int start, int length)
	{
	}

	[Token(Token = "0x60018A7")]
	[Address(RVA = "0x60C560", Offset = "0x60AF60", VA = "0x18060C560", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x60018A8")]
	[Address(RVA = "0x60C580", Offset = "0x60AF80", VA = "0x18060C580")]
	public void Replace(char oldChar, char newChar)
	{
	}

	[Token(Token = "0x60018A9")]
	[Address(RVA = "0x60C5D0", Offset = "0x60AFD0", VA = "0x18060C5D0")]
	public void Replace(string oldStr, string newStr)
	{
	}

	[Token(Token = "0x60018AA")]
	[Address(RVA = "0x60C8C0", Offset = "0x60B2C0", VA = "0x18060C8C0")]
	public void Clear()
	{
	}

	[Token(Token = "0x60018AB")]
	[Address(RVA = "0x60C8D0", Offset = "0x60B2D0", VA = "0x18060C8D0")]
	private void EnsureCapacity(int cap, bool keepValues)
	{
	}

	[Token(Token = "0x60018AC")]
	[Address(RVA = "0x60CA70", Offset = "0x60B470", VA = "0x18060CA70")]
	private static void Copy(char[] src, char[] dst)
	{
	}
}
