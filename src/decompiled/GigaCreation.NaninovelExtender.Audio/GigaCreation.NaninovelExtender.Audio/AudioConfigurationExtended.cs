using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Audio;

[Token(Token = "0x2000004")]
[EditInProjectSettings]
public class AudioConfigurationExtended : Configuration
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x18")]
	public ResponseSfx[] ResponseSfxData;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
	public AudioConfigurationExtended()
	{
	}
}
