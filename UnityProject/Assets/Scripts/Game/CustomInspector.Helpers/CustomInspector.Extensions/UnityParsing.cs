using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector.Extensions
{
    [Token(Token = "0x2000008")]
    public static class UnityParsing
    {
    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0x445600", Offset = "0x444000", VA = "0x180445600")]
    	public static bool IsColor(string s, out Color parsedColor)
    	{
    		parsedColor = default;
    		parsedColor = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0x445670", Offset = "0x444070", VA = "0x180445670")]
    	public static Color ParseColor(string s)
    	{
    		return default(Color);
    	}

    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0x4456C0", Offset = "0x4440C0", VA = "0x1804456C0")]
    	public static Vector2Int ParseVector2Int(string s)
    	{
    		return default(Vector2Int);
    	}

    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0x4459D0", Offset = "0x4443D0", VA = "0x1804459D0")]
    	public static Vector2 ParseVector2(string s)
    	{
    		return default(Vector2);
    	}

    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0x445CE0", Offset = "0x4446E0", VA = "0x180445CE0")]
    	public static Vector3Int ParseVector3Int(string s)
    	{
    		return default(Vector3Int);
    	}

    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0x446010", Offset = "0x444A10", VA = "0x180446010")]
    	public static Vector3 ParseVector3(string s)
    	{
    		return default(Vector3);
    	}

    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0x446340", Offset = "0x444D40", VA = "0x180446340")]
    	public static Vector4 ParseVector4(string s)
    	{
    		return default(Vector4);
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0x4466A0", Offset = "0x4450A0", VA = "0x1804466A0")]
    	private static List<string> GetNumbers(string s)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0x446850", Offset = "0x445250", VA = "0x180446850")]
    	private static int ParseIntRounded(string floatStr)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0x446960", Offset = "0x445360", VA = "0x180446960")]
    	public static RectInt ParseRectInt(string s)
    	{
    		return default(RectInt);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x446B00", Offset = "0x445500", VA = "0x180446B00")]
    	public static Rect ParseRect(string s)
    	{
    		return default(Rect);
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0x446CE0", Offset = "0x4456E0", VA = "0x180446CE0")]
    	public static BoundsInt ParseBoundsInt(string s)
    	{
    		return default(BoundsInt);
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0x446DF0", Offset = "0x4457F0", VA = "0x180446DF0")]
    	public static Bounds ParseBounds(string s)
    	{
    		return default(Bounds);
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0x445670", Offset = "0x444070", VA = "0x180445670")]
    	public static Quaternion ParseQuaternion(string s)
    	{
    		return default(Quaternion);
    	}
    }
}
