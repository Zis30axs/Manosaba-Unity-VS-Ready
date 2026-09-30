using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.Async.Internal
{
    [Token(Token = "0x2000646")]
    internal static class UnityEqualityComparer
    {
    	[Token(Token = "0x2000647")]
    	private static class Cache<T>
    	{
    		[Token(Token = "0x4001808")]
    		[FieldOffset(Offset = "0x0")]
    		public static readonly IEqualityComparer<T> Comparer;

    		[Token(Token = "0x6001F0A")]
    		static Cache()
    		{
    		}
    	}

    	[Token(Token = "0x2000648")]
    	private sealed class Vector2EqualityComparer : IEqualityComparer<Vector2>
    	{
    		[Token(Token = "0x6001F0B")]
    		[Address(RVA = "0x4D0B90", Offset = "0x4CF590", VA = "0x1804D0B90", Slot = "4")]
    		public bool Equals(Vector2 self, Vector2 vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F0C")]
    		[Address(RVA = "0x4D0C20", Offset = "0x4CF620", VA = "0x1804D0C20", Slot = "5")]
    		public int GetHashCode(Vector2 obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F0D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Vector2EqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x2000649")]
    	private sealed class Vector3EqualityComparer : IEqualityComparer<Vector3>
    	{
    		[Token(Token = "0x6001F0E")]
    		[Address(RVA = "0x4D0C70", Offset = "0x4CF670", VA = "0x1804D0C70", Slot = "4")]
    		public bool Equals(Vector3 self, Vector3 vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F0F")]
    		[Address(RVA = "0x4D0D00", Offset = "0x4CF700", VA = "0x1804D0D00", Slot = "5")]
    		public int GetHashCode(Vector3 obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F10")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Vector3EqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x200064A")]
    	private sealed class Vector4EqualityComparer : IEqualityComparer<Vector4>
    	{
    		[Token(Token = "0x6001F11")]
    		[Address(RVA = "0x4D0D60", Offset = "0x4CF760", VA = "0x1804D0D60", Slot = "4")]
    		public bool Equals(Vector4 self, Vector4 vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F12")]
    		[Address(RVA = "0x4D0E20", Offset = "0x4CF820", VA = "0x1804D0E20", Slot = "5")]
    		public int GetHashCode(Vector4 obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F13")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Vector4EqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x200064B")]
    	private sealed class ColorEqualityComparer : IEqualityComparer<Color>
    	{
    		[Token(Token = "0x6001F14")]
    		[Address(RVA = "0x4D0D60", Offset = "0x4CF760", VA = "0x1804D0D60", Slot = "4")]
    		public bool Equals(Color self, Color other)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F15")]
    		[Address(RVA = "0x4D0E20", Offset = "0x4CF820", VA = "0x1804D0E20", Slot = "5")]
    		public int GetHashCode(Color obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F16")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public ColorEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x200064C")]
    	private sealed class RectEqualityComparer : IEqualityComparer<Rect>
    	{
    		[Token(Token = "0x6001F17")]
    		[Address(RVA = "0x4D0EA0", Offset = "0x4CF8A0", VA = "0x1804D0EA0", Slot = "4")]
    		public bool Equals(Rect self, Rect other)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F18")]
    		[Address(RVA = "0x4D0F70", Offset = "0x4CF970", VA = "0x1804D0F70", Slot = "5")]
    		public int GetHashCode(Rect obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F19")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public RectEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x200064D")]
    	private sealed class BoundsEqualityComparer : IEqualityComparer<Bounds>
    	{
    		[Token(Token = "0x6001F1A")]
    		[Address(RVA = "0x4D0FF0", Offset = "0x4CF9F0", VA = "0x1804D0FF0", Slot = "4")]
    		public bool Equals(Bounds self, Bounds vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F1B")]
    		[Address(RVA = "0x4D1080", Offset = "0x4CFA80", VA = "0x1804D1080", Slot = "5")]
    		public int GetHashCode(Bounds obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F1C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public BoundsEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x200064E")]
    	private sealed class QuaternionEqualityComparer : IEqualityComparer<Quaternion>
    	{
    		[Token(Token = "0x6001F1D")]
    		[Address(RVA = "0x4D0D60", Offset = "0x4CF760", VA = "0x1804D0D60", Slot = "4")]
    		public bool Equals(Quaternion self, Quaternion vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F1E")]
    		[Address(RVA = "0x4D0E20", Offset = "0x4CF820", VA = "0x1804D0E20", Slot = "5")]
    		public int GetHashCode(Quaternion obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F1F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public QuaternionEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x200064F")]
    	private sealed class Color32EqualityComparer : IEqualityComparer<Color32>
    	{
    		[Token(Token = "0x6001F20")]
    		[Address(RVA = "0x4D1160", Offset = "0x4CFB60", VA = "0x1804D1160", Slot = "4")]
    		public bool Equals(Color32 self, Color32 vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F21")]
    		[Address(RVA = "0x4D11A0", Offset = "0x4CFBA0", VA = "0x1804D11A0", Slot = "5")]
    		public int GetHashCode(Color32 obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F22")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Color32EqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x2000650")]
    	private sealed class Vector2IntEqualityComparer : IEqualityComparer<Vector2Int>
    	{
    		[Token(Token = "0x6001F23")]
    		[Address(RVA = "0x4D11D0", Offset = "0x4CFBD0", VA = "0x1804D11D0", Slot = "4")]
    		public bool Equals(Vector2Int self, Vector2Int vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F24")]
    		[Address(RVA = "0x4D11F0", Offset = "0x4CFBF0", VA = "0x1804D11F0", Slot = "5")]
    		public int GetHashCode(Vector2Int obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F25")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Vector2IntEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x2000651")]
    	private sealed class Vector3IntEqualityComparer : IEqualityComparer<Vector3Int>
    	{
    		[Token(Token = "0x4001809")]
    		[FieldOffset(Offset = "0x0")]
    		public static readonly Vector3IntEqualityComparer Default;

    		[Token(Token = "0x6001F26")]
    		[Address(RVA = "0x4D1200", Offset = "0x4CFC00", VA = "0x1804D1200", Slot = "4")]
    		public bool Equals(Vector3Int self, Vector3Int vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F27")]
    		[Address(RVA = "0x4D1220", Offset = "0x4CFC20", VA = "0x1804D1220", Slot = "5")]
    		public int GetHashCode(Vector3Int obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F28")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public Vector3IntEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x2000652")]
    	private sealed class RangeIntEqualityComparer : IEqualityComparer<RangeInt>
    	{
    		[Token(Token = "0x6001F2A")]
    		[Address(RVA = "0x4D12E0", Offset = "0x4CFCE0", VA = "0x1804D12E0", Slot = "4")]
    		public bool Equals(RangeInt self, RangeInt vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F2B")]
    		[Address(RVA = "0x4D1300", Offset = "0x4CFD00", VA = "0x1804D1300", Slot = "5")]
    		public int GetHashCode(RangeInt obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F2C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public RangeIntEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x2000653")]
    	private sealed class RectIntEqualityComparer : IEqualityComparer<RectInt>
    	{
    		[Token(Token = "0x6001F2D")]
    		[Address(RVA = "0x4D1310", Offset = "0x4CFD10", VA = "0x1804D1310", Slot = "4")]
    		public bool Equals(RectInt self, RectInt other)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F2E")]
    		[Address(RVA = "0x4D1340", Offset = "0x4CFD40", VA = "0x1804D1340", Slot = "5")]
    		public int GetHashCode(RectInt obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F2F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public RectIntEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x2000654")]
    	private sealed class BoundsIntEqualityComparer : IEqualityComparer<BoundsInt>
    	{
    		[Token(Token = "0x6001F30")]
    		[Address(RVA = "0x4D1360", Offset = "0x4CFD60", VA = "0x1804D1360", Slot = "4")]
    		public bool Equals(BoundsInt self, BoundsInt vector)
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001F31")]
    		[Address(RVA = "0x4D1480", Offset = "0x4CFE80", VA = "0x1804D1480", Slot = "5")]
    		public int GetHashCode(BoundsInt obj)
    		{
    			return default(int);
    		}

    		[Token(Token = "0x6001F32")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public BoundsIntEqualityComparer()
    		{
    		}
    	}

    	[Token(Token = "0x40017EE")]
    	[FieldOffset(Offset = "0x0")]
    	public static readonly IEqualityComparer<Vector2> Vector2;

    	[Token(Token = "0x40017EF")]
    	[FieldOffset(Offset = "0x8")]
    	public static readonly IEqualityComparer<Vector3> Vector3;

    	[Token(Token = "0x40017F0")]
    	[FieldOffset(Offset = "0x10")]
    	public static readonly IEqualityComparer<Vector4> Vector4;

    	[Token(Token = "0x40017F1")]
    	[FieldOffset(Offset = "0x18")]
    	public static readonly IEqualityComparer<Color> Color;

    	[Token(Token = "0x40017F2")]
    	[FieldOffset(Offset = "0x20")]
    	public static readonly IEqualityComparer<Color32> Color32;

    	[Token(Token = "0x40017F3")]
    	[FieldOffset(Offset = "0x28")]
    	public static readonly IEqualityComparer<Rect> Rect;

    	[Token(Token = "0x40017F4")]
    	[FieldOffset(Offset = "0x30")]
    	public static readonly IEqualityComparer<Bounds> Bounds;

    	[Token(Token = "0x40017F5")]
    	[FieldOffset(Offset = "0x38")]
    	public static readonly IEqualityComparer<Quaternion> Quaternion;

    	[Token(Token = "0x40017F6")]
    	[FieldOffset(Offset = "0x40")]
    	private static readonly RuntimeTypeHandle vector2Type;

    	[Token(Token = "0x40017F7")]
    	[FieldOffset(Offset = "0x48")]
    	private static readonly RuntimeTypeHandle vector3Type;

    	[Token(Token = "0x40017F8")]
    	[FieldOffset(Offset = "0x50")]
    	private static readonly RuntimeTypeHandle vector4Type;

    	[Token(Token = "0x40017F9")]
    	[FieldOffset(Offset = "0x58")]
    	private static readonly RuntimeTypeHandle colorType;

    	[Token(Token = "0x40017FA")]
    	[FieldOffset(Offset = "0x60")]
    	private static readonly RuntimeTypeHandle color32Type;

    	[Token(Token = "0x40017FB")]
    	[FieldOffset(Offset = "0x68")]
    	private static readonly RuntimeTypeHandle rectType;

    	[Token(Token = "0x40017FC")]
    	[FieldOffset(Offset = "0x70")]
    	private static readonly RuntimeTypeHandle boundsType;

    	[Token(Token = "0x40017FD")]
    	[FieldOffset(Offset = "0x78")]
    	private static readonly RuntimeTypeHandle quaternionType;

    	[Token(Token = "0x40017FE")]
    	[FieldOffset(Offset = "0x80")]
    	public static readonly IEqualityComparer<Vector2Int> Vector2Int;

    	[Token(Token = "0x40017FF")]
    	[FieldOffset(Offset = "0x88")]
    	public static readonly IEqualityComparer<Vector3Int> Vector3Int;

    	[Token(Token = "0x4001800")]
    	[FieldOffset(Offset = "0x90")]
    	public static readonly IEqualityComparer<RangeInt> RangeInt;

    	[Token(Token = "0x4001801")]
    	[FieldOffset(Offset = "0x98")]
    	public static readonly IEqualityComparer<RectInt> RectInt;

    	[Token(Token = "0x4001802")]
    	[FieldOffset(Offset = "0xA0")]
    	public static readonly IEqualityComparer<BoundsInt> BoundsInt;

    	[Token(Token = "0x4001803")]
    	[FieldOffset(Offset = "0xA8")]
    	private static readonly RuntimeTypeHandle vector2IntType;

    	[Token(Token = "0x4001804")]
    	[FieldOffset(Offset = "0xB0")]
    	private static readonly RuntimeTypeHandle vector3IntType;

    	[Token(Token = "0x4001805")]
    	[FieldOffset(Offset = "0xB8")]
    	private static readonly RuntimeTypeHandle rangeIntType;

    	[Token(Token = "0x4001806")]
    	[FieldOffset(Offset = "0xC0")]
    	private static readonly RuntimeTypeHandle rectIntType;

    	[Token(Token = "0x4001807")]
    	[FieldOffset(Offset = "0xC8")]
    	private static readonly RuntimeTypeHandle boundsIntType;

    	[Token(Token = "0x6001F07")]
    	public static IEqualityComparer<T> GetDefault<T>()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001F08")]
    	[Address(RVA = "0x4CF930", Offset = "0x4CE330", VA = "0x1804CF930")]
    	private static object GetDefaultHelper(Type type)
    	{
    		return null;
    	}
    }
}
