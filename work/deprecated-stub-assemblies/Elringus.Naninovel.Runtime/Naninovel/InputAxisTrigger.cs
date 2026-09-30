using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000366")]
    public class InputAxisTrigger
    {
    	[Token(Token = "0x4000C8E")]
    	[FieldOffset(Offset = "0x10")]
    	[Tooltip("Name of the axis.")]
    	public string AxisName;

    	[Token(Token = "0x4000C8F")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Whether trigger should happen when axis value is positive, negative or both.")]
    	public InputAxisTriggerMode TriggerMode;

    	[Token(Token = "0x4000C90")]
    	[FieldOffset(Offset = "0x1C")]
    	[Tooltip("When axis value is below or equal to this value, the trigger won't be activated.")]
    	[Range(0f, 0.999f)]
    	public float TriggerTolerance;

    	[Token(Token = "0x6001061")]
    	[Address(RVA = "0x572FC0", Offset = "0x5719C0", VA = "0x180572FC0")]
    	public float Sample()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6001062")]
    	[Address(RVA = "0x572FD0", Offset = "0x5719D0", VA = "0x180572FD0")]
    	public InputAxisTrigger()
    	{
    	}
    }
}
