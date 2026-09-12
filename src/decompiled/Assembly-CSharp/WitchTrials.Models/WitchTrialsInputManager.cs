using System.Runtime.InteropServices;
using GigaCreation.Essentials.Input;
using GigaCreation.NaninovelExtender.Audio;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Token(Token = "0x200016B")]
public class WitchTrialsInputManager : InputManager<WitchTrialsActions>
{
	[Token(Token = "0x40006E5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly string _naninovelActionMapName;

	[Token(Token = "0x40006E6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private IAudioManagerExtended _naniAudioManager;

	[Token(Token = "0x6000625")]
	[Address(RVA = "0x397C20", Offset = "0x396620", VA = "0x180397C20")]
	public WitchTrialsInputManager(string naninovelActionMapName)
	{
	}

	[Token(Token = "0x6000626")]
	[Address(RVA = "0x397CC0", Offset = "0x3966C0", VA = "0x180397CC0", Slot = "9")]
	public override void PlayResponseSfx(ResponseKind kind)
	{
	}

	[Token(Token = "0x6000627")]
	[Address(RVA = "0x397EE0", Offset = "0x3968E0", VA = "0x180397EE0", Slot = "10")]
	protected override string BuildActionNameOrId(string actionName, [Optional] string actionMapName)
	{
		return null;
	}
}
