using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000267")]
public class ProjectResources : ScriptableObject
{
	[Serializable]
	[Token(Token = "0x2000268")]
	private struct ProjectResource
	{
		[Token(Token = "0x4000952")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string Path;

		[Token(Token = "0x4000953")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public string Type;
	}

	[Token(Token = "0x4000950")]
	public const string ResourcePath = "UnityCommon/ProjectResources";

	[Token(Token = "0x4000951")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<ProjectResource> resourcePaths;

	[Token(Token = "0x6000ABA")]
	[Address(RVA = "0x532E70", Offset = "0x531870", VA = "0x180532E70")]
	public static ProjectResources Get()
	{
		return null;
	}

	[Token(Token = "0x6000ABB")]
	[Address(RVA = "0x532F20", Offset = "0x531920", VA = "0x180532F20")]
	public IReadOnlyDictionary<string, Type> GetAllResources([Optional] string prefixFilter)
	{
		return null;
	}

	[Token(Token = "0x6000ABC")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	private void LocateAllResources()
	{
	}

	[Token(Token = "0x6000ABD")]
	[Address(RVA = "0x533160", Offset = "0x531B60", VA = "0x180533160")]
	public ProjectResources()
	{
	}
}
