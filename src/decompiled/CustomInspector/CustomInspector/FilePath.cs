using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x2000051")]
public class FilePath : AssetPath
{
	[Token(Token = "0x6000093")]
	[Address(RVA = "0x43F070", Offset = "0x43DA70", VA = "0x18043F070")]
	public FilePath(string defaultPath = "<invalid>")
	{
	}

	[Token(Token = "0x6000094")]
	[Address(RVA = "0x43F120", Offset = "0x43DB20", VA = "0x18043F120")]
	public FilePath(string defaultPath, Type fileType)
	{
	}

	[Token(Token = "0x6000095")]
	[Address(RVA = "0x43F130", Offset = "0x43DB30", VA = "0x18043F130")]
	public FilePath(Type fileType, string defaultPath = "<invalid>")
	{
	}

	[Token(Token = "0x6000096")]
	public T LoadAsset<T>() where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x6000097")]
	[Address(RVA = "0x43F150", Offset = "0x43DB50", VA = "0x18043F150")]
	public void OverrideAsset(UnityEngine.Object asset)
	{
	}

	[Token(Token = "0x6000098")]
	[Address(RVA = "0x43F1A0", Offset = "0x43DBA0", VA = "0x18043F1A0")]
	public string ReadAllText()
	{
		return null;
	}

	[Token(Token = "0x6000099")]
	[Address(RVA = "0x43F1F0", Offset = "0x43DBF0", VA = "0x18043F1F0")]
	public string[] ReadAllLines()
	{
		return null;
	}

	[Token(Token = "0x600009A")]
	[Address(RVA = "0x43F240", Offset = "0x43DC40", VA = "0x18043F240")]
	public void OverrideAllText(string content)
	{
	}

	[Token(Token = "0x600009B")]
	[Address(RVA = "0x43F290", Offset = "0x43DC90", VA = "0x18043F290")]
	public void AppendAllText(string content)
	{
	}
}
