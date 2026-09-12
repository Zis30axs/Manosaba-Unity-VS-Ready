using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel;

[Token(Token = "0x2000409")]
public class CommandParser
{
	[Token(Token = "0x4000ECB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly HashSet<string> supportedParamIds;

	[Token(Token = "0x4000ECC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly MixedValueParser mixedParser;

	[Token(Token = "0x4000ECD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly StringBuilder builder;

	[Token(Token = "0x4000ECE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private Naninovel.Parsing.Command commandModel;

	[Token(Token = "0x4000ECF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private Command command;

	[Token(Token = "0x4000ED0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private Type commandType;

	[Token(Token = "0x4000ED1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private PlaybackSpot spot;

	[Token(Token = "0x4000ED2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private bool transient;

	[Token(Token = "0x4000ED3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private CommandLocalization locale;

	[Token(Token = "0x170002AD")]
	protected virtual IErrorHandler ErrorHandler
	{
		[Token(Token = "0x6001381")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002AE")]
	protected virtual Naninovel.Parsing.Command CommandModel
	{
		[Token(Token = "0x6001382")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "5")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002AF")]
	protected virtual IReadOnlyList<Parameter> ParameterModels
	{
		[Token(Token = "0x6001383")]
		[Address(RVA = "0x5AC9E0", Offset = "0x5AB3E0", VA = "0x1805AC9E0", Slot = "6")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002B0")]
	protected virtual Command Command
	{
		[Token(Token = "0x6001384")]
		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002B1")]
	protected virtual Type CommandType
	{
		[Token(Token = "0x6001385")]
		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "8")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002B2")]
	protected virtual string CommandId
	{
		[Token(Token = "0x6001386")]
		[Address(RVA = "0x5ACA10", Offset = "0x5AB410", VA = "0x1805ACA10", Slot = "9")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001387")]
	[Address(RVA = "0x5ACA80", Offset = "0x5AB480", VA = "0x1805ACA80")]
	public CommandParser(ITextIdentifier identifier, [Optional] IErrorHandler errorHandler)
	{
	}

	[Token(Token = "0x6001388")]
	[Address(RVA = "0x5ACDA0", Offset = "0x5AB7A0", VA = "0x1805ACDA0", Slot = "10")]
	public virtual Command Parse(CommandParseArgs args)
	{
		return null;
	}

	[Token(Token = "0x6001389")]
	[Address(RVA = "0x5ACF90", Offset = "0x5AB990", VA = "0x1805ACF90", Slot = "11")]
	protected virtual void ResetState(CommandParseArgs args)
	{
	}

	[Token(Token = "0x600138A")]
	[Address(RVA = "0x5AD170", Offset = "0x5ABB70", VA = "0x1805AD170", Slot = "12")]
	protected virtual void AddError(string error)
	{
	}

	[Token(Token = "0x600138B")]
	[Address(RVA = "0x5AD250", Offset = "0x5ABC50", VA = "0x1805AD250", Slot = "13")]
	protected virtual bool TryGetCommandType(out Type commandType)
	{
		commandType = default;
		commandType = default;
		return default(bool);
	}

	[Token(Token = "0x600138C")]
	[Address(RVA = "0x5AD370", Offset = "0x5ABD70", VA = "0x1805AD370", Slot = "14")]
	protected virtual bool TryCreateCommand(out Command command)
	{
		command = default;
		command = default;
		return default(bool);
	}

	[Token(Token = "0x600138D")]
	[Address(RVA = "0x5AD510", Offset = "0x5ABF10", VA = "0x1805AD510", Slot = "15")]
	protected virtual void AssignParameters()
	{
	}

	[Token(Token = "0x600138E")]
	[Address(RVA = "0x5AD940", Offset = "0x5AC340", VA = "0x1805AD940", Slot = "16")]
	protected virtual void AssignField(FieldInfo field)
	{
	}

	[Token(Token = "0x600138F")]
	[Address(RVA = "0x5ADC00", Offset = "0x5AC600", VA = "0x1805ADC00", Slot = "17")]
	protected virtual bool TryFindModelFor(FieldInfo field, out Parameter model)
	{
		model = default;
		model = default;
		return default(bool);
	}

	[Token(Token = "0x6001390")]
	[Address(RVA = "0x5AE1D0", Offset = "0x5ACBD0", VA = "0x1805AE1D0", Slot = "18")]
	protected virtual bool TryCreateParameter(FieldInfo field, out ICommandParameter parameter)
	{
		parameter = default;
		parameter = default;
		return default(bool);
	}

	[Token(Token = "0x6001391")]
	[Address(RVA = "0x5AE380", Offset = "0x5ACD80", VA = "0x1805AE380", Slot = "19")]
	protected virtual void CheckUnsupportedParameters()
	{
	}

	[Token(Token = "0x6001392")]
	[Address(RVA = "0x5AE750", Offset = "0x5AD150", VA = "0x1805AE750", Slot = "20")]
	protected virtual bool ShouldHashPlainText(FieldInfo field)
	{
		return default(bool);
	}

	[Token(Token = "0x6001393")]
	[Address(RVA = "0x5AE830", Offset = "0x5AD230", VA = "0x1805AE830", Slot = "21")]
	protected virtual string Interpolate(IEnumerable<IValueComponent> mixed)
	{
		return null;
	}
}
