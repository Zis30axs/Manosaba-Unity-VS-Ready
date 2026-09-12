using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000020")]
public interface IPivotChangeableActor
{
	[Token(Token = "0x17000007")]
	Vector2 Pivot
	{
		[Token(Token = "0x6000051")]
		get;
		[Token(Token = "0x6000052")]
		set;
	}

	[Token(Token = "0x6000053")]
	void ChangePivot(float? x, float? y);
}
