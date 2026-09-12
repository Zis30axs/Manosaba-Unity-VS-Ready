using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Models;

[Token(Token = "0x2000181")]
public interface ITrialStateManager : IEngineService
{
	[Token(Token = "0x600069C")]
	void RegisterCurrentStage(PerspectiveStageKind kind);

	[Token(Token = "0x600069D")]
	void UnregisterCurrentStage();

	[Token(Token = "0x600069E")]
	void RegisterCameraState(int index, float? height, float?[] rotation, float? distance);

	[Token(Token = "0x600069F")]
	void RegisterScreenVisibilityState(int index, bool visible);

	[Token(Token = "0x60006A0")]
	void RegisterScreenSplitState(int index, float normalizedPosX, float roll, ScreenSplitDirection direction);
}
