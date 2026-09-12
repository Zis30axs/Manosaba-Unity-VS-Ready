using Il2CppDummyDll;
using JetBrains.Annotations;

namespace GigaCreation.Essentials.AddressablesUtils;

[Token(Token = "0x2000005")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class AddressablesDefinitions
{
	[Token(Token = "0x400000B")]
	[FieldOffset(Offset = "0x0")]
	public static DeploymentEnvironmentType CurrentDeploymentEnvironment;

	[Token(Token = "0x17000006")]
	public static string AppVersion
	{
		[Token(Token = "0x6000009")]
		[Address(RVA = "0xCBE320", Offset = "0xCBCD20", VA = "0x180CBE320")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000007")]
	public static string Environment
	{
		[Token(Token = "0x600000A")]
		[Address(RVA = "0xCBE370", Offset = "0xCBCD70", VA = "0x180CBE370")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600000B")]
	[Address(RVA = "0xCBE400", Offset = "0xCBCE00", VA = "0x180CBE400")]
	public static string BuildVersionDirName(string version)
	{
		return null;
	}
}
