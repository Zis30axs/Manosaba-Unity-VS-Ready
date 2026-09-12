using System;
using System.Reflection;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002F8")]
public static class ReflectionUtils
{
	[Token(Token = "0x6000E30")]
	[Address(RVA = "0x54CF40", Offset = "0x54B940", VA = "0x18054CF40")]
	public static FieldInfo GetFieldWithInheritance(this Type type, string fieldName, BindingFlags flags = BindingFlags.Default)
	{
		return null;
	}

	[Token(Token = "0x6000E31")]
	public static CustomAttributeData GetAttributeData<TAttribute>(MemberInfo member)
	{
		return null;
	}

	[Token(Token = "0x6000E32")]
	public static TValue GetAttributeValue<TAttribute, TValue>(MemberInfo member, int index)
	{
		return default(TValue);
	}

	[Token(Token = "0x6000E33")]
	[Address(RVA = "0x54CFC0", Offset = "0x54B9C0", VA = "0x18054CFC0")]
	public static bool IsStaticClass(Type type)
	{
		return default(bool);
	}
}
