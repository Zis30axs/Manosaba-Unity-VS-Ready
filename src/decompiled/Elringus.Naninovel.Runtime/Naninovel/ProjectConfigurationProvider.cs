using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000310")]
public class ProjectConfigurationProvider : IConfigurationProvider
{
	[Token(Token = "0x4000B8D")]
	public const string DefaultResourcesPath = "Naninovel/Configuration";

	[Token(Token = "0x4000B8E")]
	[FieldOffset(Offset = "0x10")]
	private readonly Dictionary<Type, Configuration> configObjects;

	[Token(Token = "0x4000B8F")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Dictionary<string, Configuration> loadedAssets;

	[Token(Token = "0x6000E98")]
	[Address(RVA = "0x556880", Offset = "0x555280", VA = "0x180556880")]
	public ProjectConfigurationProvider(string resourcesPath = "Naninovel/Configuration")
	{
	}

	[Token(Token = "0x6000E99")]
	[Address(RVA = "0x556C40", Offset = "0x555640", VA = "0x180556C40", Slot = "5")]
	public virtual Configuration GetConfiguration(Type type)
	{
		return null;
	}

	[Token(Token = "0x6000E9A")]
	public static TConfig LoadOrDefault<TConfig>(string resourcesPath = "Naninovel/Configuration") where TConfig : Configuration
	{
		return null;
	}

	[Token(Token = "0x6000E9B")]
	[Address(RVA = "0x556D30", Offset = "0x555730", VA = "0x180556D30")]
	public static Configuration LoadOrDefault(Type type, string resourcesPath = "Naninovel/Configuration")
	{
		return null;
	}

	[Token(Token = "0x6000E9C")]
	[Address(RVA = "0x557080", Offset = "0x555A80", VA = "0x180557080")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ClearCache()
	{
	}
}
