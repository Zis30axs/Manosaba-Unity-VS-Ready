using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Platforms;

[Token(Token = "0x2000004")]
[ExecuteAlways]
public class BuildNumberUtility : MonoBehaviour
{
	[Token(Token = "0x4000006")]
	private const int MinBuildNum = 1;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private int _buildNum;

	[Token(Token = "0x17000001")]
	public bool IsBuildNumValid
	{
		[Token(Token = "0x6000003")]
		[Address(RVA = "0xCD5B50", Offset = "0xCD4550", VA = "0x180CD5B50")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000002")]
	public bool IsBuildNumMin
	{
		[Token(Token = "0x6000004")]
		[Address(RVA = "0xCD5B60", Offset = "0xCD4560", VA = "0x180CD5B60")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public BuildNumberUtility()
	{
	}
}
