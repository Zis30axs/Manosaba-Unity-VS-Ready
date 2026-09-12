using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Movies;

[Token(Token = "0x2000005")]
[EditInProjectSettings]
public class MoviesConfigurationExtended : Configuration
{
	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x18")]
	public string AudioMixerGroup;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x20")]
	[Range(0f, 1f)]
	public float MovieVolume;

	[Token(Token = "0x6000007")]
	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
	public MoviesConfigurationExtended()
	{
	}
}
