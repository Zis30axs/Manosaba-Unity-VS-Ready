using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Metadata;

[Token(Token = "0x2000064")]
public class Branch
{
	[Token(Token = "0x1700006F")]
	public BranchTraits Traits
	{
		[Token(Token = "0x600021F")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		[CompilerGenerated]
		get
		{
			return default(BranchTraits);
		}
		[Token(Token = "0x6000220")]
		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000070")]
	public string? SwitchRoot
	{
		[Token(Token = "0x6000221")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000222")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000071")]
	public string? Endpoint
	{
		[Token(Token = "0x6000223")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000224")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6000225")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public Branch()
	{
	}
}
