using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000342")]
public class EngineParams
{
	[Token(Token = "0x17000218")]
	public IConfigurationProvider ConfigurationProvider
	{
		[Token(Token = "0x6000FA4")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000FA5")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000219")]
	public IEngineBehaviour Behaviour
	{
		[Token(Token = "0x6000FA6")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000FA7")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700021A")]
	public ITime Time
	{
		[Token(Token = "0x6000FA8")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000FA9")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700021B")]
	public IList<IEngineService> Services
	{
		[Token(Token = "0x6000FAA")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000FAB")]
		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6000FAC")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public EngineParams()
	{
	}
}
