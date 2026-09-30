using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000D2")]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ActorResourcesAttribute : Attribute
    {
    	[Token(Token = "0x4000339")]
    	[FieldOffset(Offset = "0x10")]
    	public readonly Type TypeConstraint;

    	[Token(Token = "0x400033A")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly bool AllowMultiple;

    	[Token(Token = "0x600040E")]
    	[Address(RVA = "0x476E20", Offset = "0x475820", VA = "0x180476E20")]
    	public ActorResourcesAttribute(Type typeConstraint, bool allowMultiple)
    	{
    	}
    }
}
