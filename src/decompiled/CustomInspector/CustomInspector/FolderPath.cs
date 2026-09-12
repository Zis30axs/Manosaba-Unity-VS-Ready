using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x2000053")]
public class FolderPath : AssetPath
{
	[Token(Token = "0x600009D")]
	[Address(RVA = "0x43F2E0", Offset = "0x43DCE0", VA = "0x18043F2E0")]
	public FolderPath(string defaultPath = "<invalid>")
	{
	}

	[Token(Token = "0x600009E")]
	[Address(RVA = "0x43F390", Offset = "0x43DD90", VA = "0x18043F390")]
	public void CreateAsset(UnityEngine.Object asset, string assetName)
	{
	}

	[Token(Token = "0x600009F")]
	[Address(RVA = "0x43F3E0", Offset = "0x43DDE0", VA = "0x18043F3E0")]
	public void DeleteAsset(string assetName)
	{
	}

	[Token(Token = "0x60000A0")]
	[Address(RVA = "0x43F440", Offset = "0x43DE40", VA = "0x18043F440")]
	public void DeleteAsset(string assetName, Type assetType)
	{
	}

	[Token(Token = "0x60000A1")]
	public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x60000A2")]
	[Address(RVA = "0x43F490", Offset = "0x43DE90", VA = "0x18043F490")]
	public UnityEngine.Object LoadAsset(string assetName, Type type)
	{
		return null;
	}

	[Token(Token = "0x60000A3")]
	[Address(RVA = "0x43F4E0", Offset = "0x43DEE0", VA = "0x18043F4E0")]
	public List<FolderPath> GetSubFolders()
	{
		return null;
	}

	[Token(Token = "0x60000A4")]
	[Address(RVA = "0x43F530", Offset = "0x43DF30", VA = "0x18043F530")]
	public List<FilePath> GetFiles(string fileEnding = "*")
	{
		return null;
	}

	[Token(Token = "0x60000A5")]
	[Address(RVA = "0x43F580", Offset = "0x43DF80", VA = "0x18043F580")]
	public List<FilePath> GetAllFiles(string fileEnding = "*")
	{
		return null;
	}

	[Token(Token = "0x60000A6")]
	[Address(RVA = "0x43F5D0", Offset = "0x43DFD0", VA = "0x18043F5D0")]
	public void CreateOrReplaceFileAllText(string fileName, string content)
	{
	}

	[Token(Token = "0x60000A7")]
	[Address(RVA = "0x43F620", Offset = "0x43E020", VA = "0x18043F620")]
	public void CreateFileAllText(string fileName, string content)
	{
	}

	[Token(Token = "0x60000A8")]
	[Address(RVA = "0x43F670", Offset = "0x43E070", VA = "0x18043F670")]
	public void OverrideFileAllText(string fileName, string content)
	{
	}

	[Token(Token = "0x60000A9")]
	[Address(RVA = "0x43F6C0", Offset = "0x43E0C0", VA = "0x18043F6C0")]
	public void AppendFileAllText(string fileName, string content)
	{
	}
}
