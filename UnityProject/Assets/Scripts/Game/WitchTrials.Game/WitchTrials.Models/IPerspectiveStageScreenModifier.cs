using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace WitchTrials.Models
{
    [Token(Token = "0x200018C")]
    public interface IPerspectiveStageScreenModifier
    {
    	[Token(Token = "0x60006B9")]
    	UniTask ChangeScreenVisibilityAsync(int screenIndex, bool visible, float duration, [Optional] CancellationToken ct);

    	[Token(Token = "0x60006BA")]
    	void SplitScreen(int screenIndex, float normalizedPosX, float roll, ScreenSplitDirection direction);
    }
}
