using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000403")]
public struct RawValuePart : IEquatable<RawValuePart>
{
	[Token(Token = "0x4000EBD")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string id;

	[Token(Token = "0x4000EBE")]
	[FieldOffset(Offset = "0x8")]
	[SerializeField]
	private string text;

	[Token(Token = "0x4000EBF")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private string expression;

	[Token(Token = "0x170002A8")]
	public string Id
	{
		[Token(Token = "0x600135C")]
		[Address(RVA = "0x5AAA00", Offset = "0x5A9400", VA = "0x1805AAA00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002A9")]
	public string Text
	{
		[Token(Token = "0x600135D")]
		[Address(RVA = "0x5AAA80", Offset = "0x5A9480", VA = "0x1805AAA80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002AA")]
	public string Expression
	{
		[Token(Token = "0x600135E")]
		[Address(RVA = "0x5AAAF0", Offset = "0x5A94F0", VA = "0x1805AAAF0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002AB")]
	public ParameterValuePartKind Kind
	{
		[Token(Token = "0x600135F")]
		[Address(RVA = "0x5AAB70", Offset = "0x5A9570", VA = "0x1805AAB70")]
		get
		{
			return default(ParameterValuePartKind);
		}
	}

	[Token(Token = "0x6001360")]
	[Address(RVA = "0x5AABA0", Offset = "0x5A95A0", VA = "0x1805AABA0")]
	public static RawValuePart FromIdentifiedText(string id)
	{
		return default(RawValuePart);
	}

	[Token(Token = "0x6001361")]
	[Address(RVA = "0x5AACB0", Offset = "0x5A96B0", VA = "0x1805AACB0")]
	public static RawValuePart FromPlainText(string text)
	{
		return default(RawValuePart);
	}

	[Token(Token = "0x6001362")]
	[Address(RVA = "0x5AADD0", Offset = "0x5A97D0", VA = "0x1805AADD0")]
	public static RawValuePart FromExpression(string expressionBody)
	{
		return default(RawValuePart);
	}

	[Token(Token = "0x6001363")]
	[Address(RVA = "0x5AAEF0", Offset = "0x5A98F0", VA = "0x1805AAEF0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6001364")]
	[Address(RVA = "0x5AB000", Offset = "0x5A9A00", VA = "0x1805AB000", Slot = "4")]
	public bool Equals(RawValuePart other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001365")]
	[Address(RVA = "0x5AB110", Offset = "0x5A9B10", VA = "0x1805AB110", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6001366")]
	[Address(RVA = "0x5AB1B0", Offset = "0x5A9BB0", VA = "0x1805AB1B0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x6001367")]
	[Address(RVA = "0x5AAB70", Offset = "0x5A9570", VA = "0x1805AAB70")]
	private ParameterValuePartKind EvaluateKind()
	{
		return default(ParameterValuePartKind);
	}
}
