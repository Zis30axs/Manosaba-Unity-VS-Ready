using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000113")]
public class CameraLookController
{
	[Token(Token = "0x400043A")]
	[FieldOffset(Offset = "0x28")]
	private readonly Transform transform;

	[Token(Token = "0x400043B")]
	[FieldOffset(Offset = "0x30")]
	private readonly Tweener<VectorTween> gravitateToOriginTweener;

	[Token(Token = "0x400043C")]
	[FieldOffset(Offset = "0x38")]
	private bool enabled;

	[Token(Token = "0x400043D")]
	[FieldOffset(Offset = "0x40")]
	private readonly IInputSampler xSampler;

	[Token(Token = "0x400043E")]
	[FieldOffset(Offset = "0x48")]
	private readonly IInputSampler ySampler;

	[Token(Token = "0x170000CA")]
	public bool Enabled
	{
		[Token(Token = "0x60004EB")]
		[Address(RVA = "0x43D680", Offset = "0x43C080", VA = "0x18043D680")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60004EC")]
		[Address(RVA = "0x485A30", Offset = "0x484430", VA = "0x180485A30")]
		set
		{
		}
	}

	[Token(Token = "0x170000CB")]
	public Vector2 LookZone
	{
		[Token(Token = "0x60004ED")]
		[Address(RVA = "0x485A40", Offset = "0x484440", VA = "0x180485A40")]
		[CompilerGenerated]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60004EE")]
		[Address(RVA = "0x485A60", Offset = "0x484460", VA = "0x180485A60")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000CC")]
	public Vector2 LookSpeed
	{
		[Token(Token = "0x60004EF")]
		[Address(RVA = "0x33FBD0", Offset = "0x33E5D0", VA = "0x18033FBD0")]
		[CompilerGenerated]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60004F0")]
		[Address(RVA = "0x485A70", Offset = "0x484470", VA = "0x180485A70")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000CD")]
	public bool Gravity
	{
		[Token(Token = "0x60004F1")]
		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60004F2")]
		[Address(RVA = "0x468AD0", Offset = "0x4674D0", VA = "0x180468AD0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000CE")]
	private Vector2 position
	{
		[Token(Token = "0x60004F3")]
		[Address(RVA = "0x485A80", Offset = "0x484480", VA = "0x180485A80")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60004F4")]
		[Address(RVA = "0x485B50", Offset = "0x484550", VA = "0x180485B50")]
		set
		{
		}
	}

	[Token(Token = "0x170000CF")]
	private Vector2 origin
	{
		[Token(Token = "0x60004F5")]
		[Address(RVA = "0x3A3310", Offset = "0x3A1D10", VA = "0x1803A3310")]
		get
		{
			return default(Vector2);
		}
	}

	[Token(Token = "0x60004F6")]
	[Address(RVA = "0x485C20", Offset = "0x484620", VA = "0x180485C20")]
	public CameraLookController(Transform transform, IInputSampler xSampler, IInputSampler ySampler)
	{
	}

	[Token(Token = "0x60004F7")]
	[Address(RVA = "0x485DE0", Offset = "0x4847E0", VA = "0x180485DE0")]
	public CameraLookState GetState()
	{
		return default(CameraLookState);
	}

	[Token(Token = "0x60004F8")]
	[Address(RVA = "0x485E00", Offset = "0x484800", VA = "0x180485E00")]
	public void Update()
	{
	}

	[Token(Token = "0x60004F9")]
	[Address(RVA = "0x486330", Offset = "0x484D30", VA = "0x180486330")]
	private void SetEnabled(bool value)
	{
	}
}
