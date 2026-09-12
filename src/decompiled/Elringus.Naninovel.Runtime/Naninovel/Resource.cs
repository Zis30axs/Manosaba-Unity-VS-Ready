using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000275")]
public class Resource
{
	[Token(Token = "0x4000985")]
	[FieldOffset(Offset = "0x0")]
	public static readonly Resource Invalid;

	[Token(Token = "0x4000986")]
	[FieldOffset(Offset = "0x10")]
	public readonly string Path;

	[Token(Token = "0x4000987")]
	[FieldOffset(Offset = "0x18")]
	public readonly Object Object;

	[Token(Token = "0x1700018A")]
	public bool Valid
	{
		[Token(Token = "0x6000AE8")]
		[Address(RVA = "0x535930", Offset = "0x534330", VA = "0x180535930")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000AE9")]
	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
	public Resource(string path = default, Object obj = default)
	{
	}

	[Token(Token = "0x6000AEA")]
	[Address(RVA = "0x535940", Offset = "0x534340", VA = "0x180535940")]
	public static implicit operator Object(Resource resource)
	{
		return null;
	}

	[Token(Token = "0x6000AEB")]
	[Address(RVA = "0x535950", Offset = "0x534350", VA = "0x180535950", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
[Token(Token = "0x2000276")]
public class Resource<TResource> : Resource where TResource : Object
{
	[Token(Token = "0x4000988")]
	[FieldOffset(Offset = "0x0")]
	public new static readonly Resource<TResource> Invalid;

	[Token(Token = "0x1700018B")]
	public new TResource Object
	{
		[Token(Token = "0x6000AED")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000AEE")]
	public Resource(string path = default, TResource obj = default)
	{
	}

	[Token(Token = "0x6000AEF")]
	public static implicit operator TResource(Resource<TResource> resource)
	{
		return null;
	}

	[Token(Token = "0x6000AF0")]
	private TResource CastObject(Object obj)
	{
		return null;
	}
}
