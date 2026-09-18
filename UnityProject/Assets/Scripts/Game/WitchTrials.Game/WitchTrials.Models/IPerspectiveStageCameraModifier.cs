using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000189")]
    public interface IPerspectiveStageCameraModifier
    {
    	[Token(Token = "0x60006B4")]
    	UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct);

    	[Token(Token = "0x60006B5")]
    	UniTask ShakeCameraAsync(int cameraIndex, Vector3 power, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct);
    }
}
