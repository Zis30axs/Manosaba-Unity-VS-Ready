using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000344")]
public class EngineVersion : ScriptableObject
{
	[Token(Token = "0x4000C42")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private string engineVersion;

	[Token(Token = "0x4000C43")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private bool preview;

	[Token(Token = "0x4000C44")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	[ReadOnly]
	private string buildDate;

	[Token(Token = "0x17000226")]
	public string Version
	{
		[Token(Token = "0x6000FB9")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000227")]
	public bool Preview
	{
		[Token(Token = "0x6000FBA")]
		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000228")]
	public string Build
	{
		[Token(Token = "0x6000FBB")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000FBC")]
	[Address(RVA = "0x5699F0", Offset = "0x5683F0", VA = "0x1805699F0")]
	public static EngineVersion LoadFromResources()
	{
		return null;
	}

	[Token(Token = "0x6000FBD")]
	[Address(RVA = "0x569A70", Offset = "0x568470", VA = "0x180569A70")]
	public string BuildVersionTag()
	{
		return null;
	}

	[Token(Token = "0x6000FBE")]
	[Address(RVA = "0x569B30", Offset = "0x568530", VA = "0x180569B30")]
	public EngineVersion()
	{
	}
}
