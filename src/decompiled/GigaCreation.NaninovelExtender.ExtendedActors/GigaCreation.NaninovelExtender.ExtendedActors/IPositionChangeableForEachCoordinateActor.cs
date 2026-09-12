using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000021")]
public interface IPositionChangeableForEachCoordinateActor
{
	[Token(Token = "0x6000054")]
	UniTask ChangePositionXAsync(float positionX, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default);

	[Token(Token = "0x6000055")]
	UniTask ChangePositionYAsync(float positionY, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default);

	[Token(Token = "0x6000056")]
	UniTask ChangePositionZAsync(float positionZ, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default);
}
