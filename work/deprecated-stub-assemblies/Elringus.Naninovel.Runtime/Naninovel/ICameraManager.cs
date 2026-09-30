using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000123")]
    public interface ICameraManager : IEngineService<CameraConfiguration>, IEngineService
    {
    	[Token(Token = "0x170000E1")]
    	Camera Camera
    	{
    		[Token(Token = "0x6000560")]
    		get;
    	}

    	[Token(Token = "0x170000E2")]
    	Camera UICamera
    	{
    		[Token(Token = "0x6000561")]
    		get;
    	}

    	[Token(Token = "0x170000E3")]
    	bool RenderUI
    	{
    		[Token(Token = "0x6000562")]
    		get;
    		[Token(Token = "0x6000563")]
    		set;
    	}

    	[Token(Token = "0x170000E4")]
    	Vector3 Offset
    	{
    		[Token(Token = "0x6000564")]
    		get;
    		[Token(Token = "0x6000565")]
    		set;
    	}

    	[Token(Token = "0x170000E5")]
    	Quaternion Rotation
    	{
    		[Token(Token = "0x6000566")]
    		get;
    		[Token(Token = "0x6000567")]
    		set;
    	}

    	[Token(Token = "0x170000E6")]
    	float Zoom
    	{
    		[Token(Token = "0x6000568")]
    		get;
    		[Token(Token = "0x6000569")]
    		set;
    	}

    	[Token(Token = "0x170000E7")]
    	bool Orthographic
    	{
    		[Token(Token = "0x600056A")]
    		get;
    		[Token(Token = "0x600056B")]
    		set;
    	}

    	[Token(Token = "0x170000E8")]
    	int QualityLevel
    	{
    		[Token(Token = "0x600056C")]
    		get;
    		[Token(Token = "0x600056D")]
    		set;
    	}

    	[Token(Token = "0x600056E")]
    	void SetLookMode(bool enabled, Vector2 lookZone, Vector2 lookSpeed, bool gravity);

    	[Token(Token = "0x600056F")]
    	Texture2D CaptureThumbnail();

    	[Token(Token = "0x6000570")]
    	UniTask ChangeOffset(Vector3 offset, Tween tween, [Optional] AsyncToken token);

    	[Token(Token = "0x6000571")]
    	UniTask ChangeRotation(Quaternion rotation, Tween tween, [Optional] AsyncToken token);

    	[Token(Token = "0x6000572")]
    	UniTask ChangeZoom(float zoom, Tween tween, [Optional] AsyncToken token);
    }
}
