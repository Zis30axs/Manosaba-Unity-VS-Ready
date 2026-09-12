using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004E2")]
[EditInProjectSettings]
public class UnlockablesConfiguration : Configuration
{
	[Token(Token = "0x400129E")]
	public const string DefaultPathPrefix = "Unlockables";

	[Token(Token = "0x400129F")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Configuration of the resource loader used with unlockable resources.")]
	public ResourceLoaderConfiguration Loader;

	[Token(Token = "0x6001871")]
	[Address(RVA = "0x607900", Offset = "0x606300", VA = "0x180607900")]
	public UnlockablesConfiguration()
	{
	}
}
