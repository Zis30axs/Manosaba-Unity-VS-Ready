using System.Diagnostics;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace CustomInspector;

[Token(Token = "0x200003C")]
[Conditional("UNITY_EDITOR")]
public class ShowIfNotAttribute : ShowIfAttribute
{
	[Token(Token = "0x600005F")]
	[Address(RVA = "0x43DD70", Offset = "0x43C770", VA = "0x18043DD70")]
	public ShowIfNotAttribute(string conditionPath)
	{
	}

	[Token(Token = "0x6000060")]
	[Address(RVA = "0x43DD90", Offset = "0x43C790", VA = "0x18043DD90")]
	public ShowIfNotAttribute(StaticConditions condition)
	{
	}

	[Token(Token = "0x6000061")]
	[Address(RVA = "0x43DE10", Offset = "0x43C810", VA = "0x18043DE10")]
	public ShowIfNotAttribute(BoolOperator op, params string[] conditionPaths)
	{
	}

	[Token(Token = "0x6000062")]
	[Address(RVA = "0x43DE30", Offset = "0x43C830", VA = "0x18043DE30")]
	public ShowIfNotAttribute(BoolOperator op, StaticConditions cond1, [Optional] string cond2, [Optional] string cond3, [Optional] string cond4)
	{
	}

	[Token(Token = "0x6000063")]
	[Address(RVA = "0x43DF40", Offset = "0x43C940", VA = "0x18043DF40")]
	public ShowIfNotAttribute(BoolOperator op, string cond1, StaticConditions cond2, [Optional] string cond3, [Optional] string cond4)
	{
	}

	[Token(Token = "0x6000064")]
	[Address(RVA = "0x43E060", Offset = "0x43CA60", VA = "0x18043E060")]
	public ShowIfNotAttribute(BoolOperator op, string cond1, string cond2, StaticConditions cond3, [Optional] string cond4)
	{
	}

	[Token(Token = "0x6000065")]
	[Address(RVA = "0x43E180", Offset = "0x43CB80", VA = "0x18043E180")]
	public ShowIfNotAttribute(ComparisonOp com, params string[] conditionPaths)
	{
	}
}
