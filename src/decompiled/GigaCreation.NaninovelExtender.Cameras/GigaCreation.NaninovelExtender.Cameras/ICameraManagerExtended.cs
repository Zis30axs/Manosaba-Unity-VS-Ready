using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Cameras;

[Token(Token = "0x200000C")]
public interface ICameraManagerExtended : IEngineService<CameraConfigurationExtended>, IEngineService
{
	[Token(Token = "0x17000009")]
	bool FullScreen
	{
		[Token(Token = "0x600002D")]
		get;
		[Token(Token = "0x600002E")]
		set;
	}

	[Token(Token = "0x1700000A")]
	int ResolutionLevel
	{
		[Token(Token = "0x600002F")]
		get;
		[Token(Token = "0x6000030")]
		set;
	}

	[Token(Token = "0x1700000B")]
	int TargetFpsLevel
	{
		[Token(Token = "0x6000031")]
		get;
		[Token(Token = "0x6000032")]
		set;
	}

	[Token(Token = "0x1700000C")]
	bool IsPostProcessingEnabled
	{
		[Token(Token = "0x6000033")]
		get;
		[Token(Token = "0x6000034")]
		set;
	}

	[Token(Token = "0x1700000D")]
	Vector2Int[] AvailableResolutionsForPc
	{
		[Token(Token = "0x6000035")]
		get;
	}
}
