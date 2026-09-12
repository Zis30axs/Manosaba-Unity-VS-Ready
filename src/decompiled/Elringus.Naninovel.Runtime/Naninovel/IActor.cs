using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000059")]
public interface IActor
{
	[Token(Token = "0x1700002F")]
	string Id
	{
		[Token(Token = "0x6000148")]
		get;
	}

	[Token(Token = "0x17000030")]
	string Appearance
	{
		[Token(Token = "0x6000149")]
		get;
		[Token(Token = "0x600014A")]
		set;
	}

	[Token(Token = "0x17000031")]
	bool Visible
	{
		[Token(Token = "0x600014B")]
		get;
		[Token(Token = "0x600014C")]
		set;
	}

	[Token(Token = "0x17000032")]
	Vector3 Position
	{
		[Token(Token = "0x600014D")]
		get;
		[Token(Token = "0x600014E")]
		set;
	}

	[Token(Token = "0x17000033")]
	Quaternion Rotation
	{
		[Token(Token = "0x600014F")]
		get;
		[Token(Token = "0x6000150")]
		set;
	}

	[Token(Token = "0x17000034")]
	Vector3 Scale
	{
		[Token(Token = "0x6000151")]
		get;
		[Token(Token = "0x6000152")]
		set;
	}

	[Token(Token = "0x17000035")]
	Color TintColor
	{
		[Token(Token = "0x6000153")]
		get;
		[Token(Token = "0x6000154")]
		set;
	}

	[Token(Token = "0x6000155")]
	UniTask Initialize();

	[Token(Token = "0x6000156")]
	UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token);

	[Token(Token = "0x6000157")]
	UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token);

	[Token(Token = "0x6000158")]
	UniTask ChangePosition(Vector3 position, Tween tween, [Optional] AsyncToken token);

	[Token(Token = "0x6000159")]
	UniTask ChangeRotation(Quaternion rotation, Tween tween, [Optional] AsyncToken token);

	[Token(Token = "0x600015A")]
	UniTask ChangeScale(Vector3 scale, Tween tween, [Optional] AsyncToken token);

	[Token(Token = "0x600015B")]
	UniTask ChangeTintColor(Color tintColor, Tween tween, [Optional] AsyncToken token);
}
