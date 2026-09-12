using Il2CppDummyDll;

namespace CustomInspector;

[Token(Token = "0x2000021")]
public interface IMinMaxAttribute
{
	[Token(Token = "0x17000001")]
	float CapValue
	{
		[Token(Token = "0x600001F")]
		get;
	}

	[Token(Token = "0x17000002")]
	string CapPath
	{
		[Token(Token = "0x6000020")]
		get;
	}

	[Token(Token = "0x6000021")]
	bool DependsOnOtherProperty();
}
