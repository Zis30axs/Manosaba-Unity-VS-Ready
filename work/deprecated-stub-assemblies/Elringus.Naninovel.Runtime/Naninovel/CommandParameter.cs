#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20003EB")]
    public static class CommandParameter
    {
    	[Token(Token = "0x60012D9")]
    	public static T FromRaw<T>(RawValue? raw, PlaybackSpot? spot, out string errors) where T : ICommandParameter, new()
    	{
    		errors = default;
    		return default(T);
    	}

    	[Token(Token = "0x60012DA")]
    	[Address(RVA = "0x5A5CF0", Offset = "0x5A46F0", VA = "0x1805A5CF0")]
    	public static IReadOnlyList<ParameterInfo> Extract(Command command)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012DB")]
    	[Address(RVA = "0x5A5FD0", Offset = "0x5A49D0", VA = "0x1805A5FD0")]
    	public static IReadOnlyList<FieldInfo> ExtractFields(Type commandType)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012DC")]
    	public static T Ref<T>(T param) where T : ICommandParameter, new()
    	{
    		return default(T);
    	}
    }
    [Serializable]
    [Token(Token = "0x20003ED")]
    public abstract class CommandParameter<TValue> : Nullable<TValue>, ICommandParameter
    {
    	[Token(Token = "0x4000EA9")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private RawValue raw;

    	[Token(Token = "0x4000EAA")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private PlaybackSpot spot;

    	[Token(Token = "0x17000295")]
    	public bool DynamicValue
    	{
    		[Token(Token = "0x60012E0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000296")]
    	public PlaybackSpot? PlaybackSpot
    	{
    		[Token(Token = "0x60012E1")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000297")]
    	public RawValue? RawValue
    	{
    		[Token(Token = "0x60012E2")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60012E3")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x60012E4")]
    	public virtual void AssignRaw(RawValue? raw, PlaybackSpot? spot, out string errors)
    	{
    		errors = default;
    		errors = default;
    	}

    	[Token(Token = "0x60012E5")]
    	protected override TValue GetValue()
    	{
    		return default(TValue);
    	}

    	[Token(Token = "0x60012E6")]
    	protected override void SetValue(TValue value)
    	{
    	}

    	[Token(Token = "0x60012E7")]
    	protected virtual TValue EvaluateDynamicValue()
    	{
    		return default(TValue);
    	}

    	[Token(Token = "0x60012E8")]
    	protected virtual string InterpolatePlainText(IReadOnlyList<RawValuePart> parts)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012E9")]
    	protected virtual int ParseIntegerText(string intText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return default(int);
    	}

    	[Token(Token = "0x60012EA")]
    	protected virtual float ParseFloatText(string floatText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return default(float);
    	}

    	[Token(Token = "0x60012EB")]
    	protected virtual bool ParseBooleanText(string boolText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return default(bool);
    	}

    	[Token(Token = "0x60012EC")]
    	protected virtual void ParseNamedValueText(string valueText, out string name, out string value, out string errors)
    	{
    		name = default;
    		value = default;
    		errors = default;
    		name = default;
    		value = default;
    		errors = default;
    	}

    	[Token(Token = "0x60012ED")]
    	protected abstract TValue ParseRaw(RawValue raw, out string errors);

    	[Token(Token = "0x60012EE")]
    	protected CommandParameter()
    	{
    	}
    }
}
