using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000430")]
public class PlayedScriptRegister
{
	[Serializable]
	[Token(Token = "0x2000431")]
	private class PlayedScript
	{
		[Token(Token = "0x4000F60")]
		[FieldOffset(Offset = "0x10")]
		public string ScriptPath;

		[Token(Token = "0x4000F61")]
		[FieldOffset(Offset = "0x18")]
		public List<IntRange> PlayedIndexes;

		[Token(Token = "0x60014A8")]
		[Address(RVA = "0x5BE9C0", Offset = "0x5BD3C0", VA = "0x1805BE9C0")]
		public PlayedScript(string scriptPath)
		{
		}

		[Token(Token = "0x60014A9")]
		[Address(RVA = "0x5BEAE0", Offset = "0x5BD4E0", VA = "0x1805BEAE0")]
		public void AddIndex(int index)
		{
		}

		[Token(Token = "0x60014AA")]
		[Address(RVA = "0x5BEC60", Offset = "0x5BD660", VA = "0x1805BEC60")]
		public bool ContainsIndex(int index)
		{
			return default(bool);
		}
	}

	[Token(Token = "0x4000F5F")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private List<PlayedScript> playedScripts;

	[Token(Token = "0x60014A1")]
	[Address(RVA = "0x5BDDB0", Offset = "0x5BC7B0", VA = "0x1805BDDB0")]
	public IReadOnlyDictionary<string, IReadOnlyList<IntRange>> GetPlayed()
	{
		return null;
	}

	[Token(Token = "0x60014A2")]
	[Address(RVA = "0x5BE060", Offset = "0x5BCA60", VA = "0x1805BE060")]
	public void RegisterPlayedIndex(string scriptPath, int playlistIndex)
	{
	}

	[Token(Token = "0x60014A3")]
	[Address(RVA = "0x5BE210", Offset = "0x5BCC10", VA = "0x1805BE210")]
	public bool IsIndexPlayed(string scriptPath, int playlistIndex)
	{
		return default(bool);
	}

	[Token(Token = "0x60014A4")]
	[Address(RVA = "0x5BE2E0", Offset = "0x5BCCE0", VA = "0x1805BE2E0")]
	public bool IsScriptPlayed(string scriptPath)
	{
		return default(bool);
	}

	[Token(Token = "0x60014A5")]
	[Address(RVA = "0x5BE440", Offset = "0x5BCE40", VA = "0x1805BE440")]
	public int CountPlayed()
	{
		return default(int);
	}

	[Token(Token = "0x60014A6")]
	[Address(RVA = "0x5BE690", Offset = "0x5BD090", VA = "0x1805BE690")]
	private PlayedScript GetOrCreateDataForScript(string scriptPath)
	{
		return null;
	}

	[Token(Token = "0x60014A7")]
	[Address(RVA = "0x5BE900", Offset = "0x5BD300", VA = "0x1805BE900")]
	public PlayedScriptRegister()
	{
	}
}
