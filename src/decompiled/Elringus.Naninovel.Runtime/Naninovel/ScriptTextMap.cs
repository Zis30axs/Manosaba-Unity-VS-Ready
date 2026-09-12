using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200042B")]
public class ScriptTextMap
{
	[Serializable]
	[Token(Token = "0x200042C")]
	public class SerializableTextMap : SerializableMap<string, string>
	{
		[Token(Token = "0x6001465")]
		[Address(RVA = "0x5BD4F0", Offset = "0x5BBEF0", VA = "0x1805BD4F0")]
		public SerializableTextMap()
		{
		}

		[Token(Token = "0x6001466")]
		[Address(RVA = "0x5BD5E0", Offset = "0x5BBFE0", VA = "0x1805BD5E0")]
		public SerializableTextMap(IDictionary<string, string> dictionary)
		{
		}
	}

	[Token(Token = "0x4000F5A")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private SerializableTextMap idToText;

	[Token(Token = "0x170002D2")]
	public IReadOnlyDictionary<string, string> Map
	{
		[Token(Token = "0x6001462")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001463")]
	[Address(RVA = "0x5BD1B0", Offset = "0x5BBBB0", VA = "0x1805BD1B0")]
	public ScriptTextMap(IDictionary<string, string> idToText)
	{
	}

	[Token(Token = "0x6001464")]
	[Address(RVA = "0x5BD360", Offset = "0x5BBD60", VA = "0x1805BD360")]
	public string GetTextOrNull(string id)
	{
		return null;
	}
}
