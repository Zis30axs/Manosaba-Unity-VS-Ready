using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200030C")]
    public abstract class Configuration : ScriptableObject
    {
    	[Token(Token = "0x200030D")]
    	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
    	public sealed class EditInProjectSettingsAttribute : Attribute
    	{
    		[Token(Token = "0x6000E95")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public EditInProjectSettingsAttribute()
    		{
    		}
    	}

    	[Token(Token = "0x6000E93")]
    	public static T GetOrDefault<T>() where T : Configuration
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E94")]
    	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
    	protected Configuration()
    	{
    	}
    }
}
