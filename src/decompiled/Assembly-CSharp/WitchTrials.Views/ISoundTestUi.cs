using Il2CppDummyDll;
using Naninovel.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000C5")]
public interface ISoundTestUi : IManagedUI
{
	[Token(Token = "0x60002CA")]
	void ModifyPlayer(string trackName);
}
