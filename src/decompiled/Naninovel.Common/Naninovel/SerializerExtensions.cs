using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000015")]
public static class SerializerExtensions
{
	[Token(Token = "0x6000048")]
	[Address(RVA = "0x10CFC70", Offset = "0x10CE670", VA = "0x1810CFC70")]
	public static string? SerializeOrNull(this ISerializer serializer, object? poco)
	{
		return null;
	}

	[Token(Token = "0x6000049")]
	[Address(RVA = "0x10CFCE0", Offset = "0x10CE6E0", VA = "0x1810CFCE0")]
	public static string SerializeOrNull(this ISerializer serializer, object? poco, Type type)
	{
		return null;
	}

	[Token(Token = "0x600004A")]
	[Address(RVA = "0x10CFD60", Offset = "0x10CE760", VA = "0x1810CFD60")]
	public static object DeserializeOrNull(this ISerializer serializer, string? serialized, Type type)
	{
		return null;
	}

	[Token(Token = "0x600004B")]
	public static T? DeserializeOrNull<T>(this ISerializer serializer, string? serialized)
	{
		return default(T?);
	}

	[Token(Token = "0x600004C")]
	public static T Deserialize<T>(this ISerializer serializer, string serialized) where T : notnull
	{
		return default(T);
	}

	[Token(Token = "0x600004D")]
	[Address(RVA = "0x10CFE30", Offset = "0x10CE830", VA = "0x1810CFE30")]
	public static bool TrySerialize(this ISerializer serializer, object poco, out string serialized)
	{
		serialized = default;
		serialized = default;
		return default(bool);
	}

	[Token(Token = "0x600004E")]
	[Address(RVA = "0x10CFF50", Offset = "0x10CE950", VA = "0x1810CFF50")]
	public static bool TrySerialize(this ISerializer serializer, object poco, Type type, out string serialized)
	{
		serialized = default;
		poco = default;
		serialized = default;
		poco = default;
		return default(bool);
	}

	[Token(Token = "0x600004F")]
	public static bool TryDeserialize<T>(this ISerializer serializer, string serialized, out T poco) where T : notnull
	{
		poco = default;
		poco = default;
		return default(bool);
	}
}
