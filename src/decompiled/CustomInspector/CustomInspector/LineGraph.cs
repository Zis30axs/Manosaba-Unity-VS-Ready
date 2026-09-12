using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x2000057")]
public class LineGraph
{
	[Token(Token = "0x4000153")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private bool _;

	[Token(Token = "0x4000154")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	[HideInInspector]
	private Vector2[] points;

	[Token(Token = "0x1700000F")]
	public Vector2[] Points
	{
		[Token(Token = "0x60000BE")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
		[Token(Token = "0x60000BF")]
		[Address(RVA = "0x43FC30", Offset = "0x43E630", VA = "0x18043FC30")]
		set
		{
		}
	}

	[Token(Token = "0x60000BC")]
	[Address(RVA = "0x43F920", Offset = "0x43E320", VA = "0x18043F920")]
	public LineGraph()
	{
	}

	[Token(Token = "0x60000BD")]
	[Address(RVA = "0x43FA20", Offset = "0x43E420", VA = "0x18043FA20")]
	public LineGraph(Vector2[] points)
	{
	}

	[Token(Token = "0x60000C0")]
	[Address(RVA = "0x43FD40", Offset = "0x43E740", VA = "0x18043FD40")]
	private void SortPoints()
	{
	}

	[Token(Token = "0x60000C1")]
	[Address(RVA = "0x43FF10", Offset = "0x43E910", VA = "0x18043FF10")]
	public float GetYValue(float xValue)
	{
		return default(float);
	}
}
