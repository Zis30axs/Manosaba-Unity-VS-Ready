using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000357")]
    public readonly struct ServiceInitializationData : IEquatable<ServiceInitializationData>
    {
    	[Token(Token = "0x4000C72")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly Type Type;

    	[Token(Token = "0x4000C73")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly int Priority;

    	[Token(Token = "0x4000C74")]
    	[FieldOffset(Offset = "0x10")]
    	public readonly Type[] CtorArgs;

    	[Token(Token = "0x4000C75")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly Type Override;

    	[Token(Token = "0x6001008")]
    	[Address(RVA = "0x56E870", Offset = "0x56D270", VA = "0x18056E870")]
    	public ServiceInitializationData(Type type, InitializeAtRuntimeAttribute attr)
    	{ this = default; }

    	[Token(Token = "0x6001009")]
    	[Address(RVA = "0x56EC10", Offset = "0x56D610", VA = "0x18056EC10", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600100A")]
    	[Address(RVA = "0x56ECF0", Offset = "0x56D6F0", VA = "0x18056ECF0", Slot = "4")]
    	public bool Equals(ServiceInitializationData other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600100B")]
    	[Address(RVA = "0x56ED70", Offset = "0x56D770", VA = "0x18056ED70", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600100C")]
    	[Address(RVA = "0x56ECF0", Offset = "0x56D6F0", VA = "0x18056ECF0")]
    	public static bool operator ==(ServiceInitializationData left, ServiceInitializationData right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600100D")]
    	[Address(RVA = "0x56EDE0", Offset = "0x56D7E0", VA = "0x18056EDE0")]
    	public static bool operator !=(ServiceInitializationData left, ServiceInitializationData right)
    	{
    		return default(bool);
    	}
    }
}
