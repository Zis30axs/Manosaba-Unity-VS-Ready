using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x200057E")]
public interface ISceneTransitionUI : IManagedUI
{
	[Token(Token = "0x6001B36")]
	UniTask CaptureScene();

	[Token(Token = "0x6001B37")]
	UniTask Transition(Transition transition, Tween tween, [Optional] AsyncToken token);
}
