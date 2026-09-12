using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200003B")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class ShowIfAttribute : PropertyAttribute
{
	[Token(Token = "0x400011A")]
	public const float indentation = 15f;

	[Token(Token = "0x400011B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	public readonly BoolOperator? op;

	[Token(Token = "0x400011C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	public readonly ComparisonOp? comOp;

	[Token(Token = "0x400011D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	public readonly string[] conditionPaths;

	[Token(Token = "0x400011E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	public DisabledStyle style;

	[Token(Token = "0x400011F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	public int indent;

	[Token(Token = "0x17000008")]
	public bool Invert
	{
		[Token(Token = "0x6000054")]
		[Address(RVA = "0x43D680", Offset = "0x43C080", VA = "0x18043D680")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000055")]
		[Address(RVA = "0x43D690", Offset = "0x43C090", VA = "0x18043D690")]
		[CompilerGenerated]
		protected set
		{
		}
	}

	[Token(Token = "0x6000056")]
	[Address(RVA = "0x43D6A0", Offset = "0x43C0A0", VA = "0x18043D6A0")]
	private ShowIfAttribute()
	{
	}

	[Token(Token = "0x6000057")]
	[Address(RVA = "0x43D6C0", Offset = "0x43C0C0", VA = "0x18043D6C0")]
	protected ShowIfAttribute(params string[] conditionPaths)
	{
	}

	[Token(Token = "0x6000058")]
	[Address(RVA = "0x43D730", Offset = "0x43C130", VA = "0x18043D730")]
	public ShowIfAttribute(string conditionPath)
	{
	}

	[Token(Token = "0x6000059")]
	[Address(RVA = "0x43D810", Offset = "0x43C210", VA = "0x18043D810")]
	public ShowIfAttribute(StaticConditions condition)
	{
	}

	[Token(Token = "0x600005A")]
	[Address(RVA = "0x43D880", Offset = "0x43C280", VA = "0x18043D880")]
	public ShowIfAttribute(BoolOperator op, params string[] conditionPaths)
	{
	}

	[Token(Token = "0x600005B")]
	[Address(RVA = "0x43D960", Offset = "0x43C360", VA = "0x18043D960")]
	public ShowIfAttribute(BoolOperator op, StaticConditions cond1, [Optional] string cond2, [Optional] string cond3, [Optional] string cond4)
	{
	}

	[Token(Token = "0x600005C")]
	[Address(RVA = "0x43DA70", Offset = "0x43C470", VA = "0x18043DA70")]
	public ShowIfAttribute(BoolOperator op, string cond1, StaticConditions cond2, [Optional] string cond3, [Optional] string cond4)
	{
	}

	[Token(Token = "0x600005D")]
	[Address(RVA = "0x43DB80", Offset = "0x43C580", VA = "0x18043DB80")]
	public ShowIfAttribute(BoolOperator op, string cond1, string cond2, StaticConditions cond3, [Optional] string cond4)
	{
	}

	[Token(Token = "0x600005E")]
	[Address(RVA = "0x43DC90", Offset = "0x43C690", VA = "0x18043DC90")]
	public ShowIfAttribute(ComparisonOp com, params string[] conditionPaths)
	{
	}
}
