using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.MasterData;

[Token(Token = "0x2000006")]
public static class MasterDataProvider
{
	[Token(Token = "0x17000001")]
	public static IMasterDataBase Data
	{
		[Token(Token = "0x6000004")]
		[Address(RVA = "0xCD3C60", Offset = "0xCD2660", VA = "0x180CD3C60")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000005")]
		[Address(RVA = "0xCD3CA0", Offset = "0xCD26A0", VA = "0x180CD3CA0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xCD3D40", Offset = "0xCD2740", VA = "0x180CD3D40")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void LoadMasterData()
	{
	}
}
