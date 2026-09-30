using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006A0")]
    [CommandAlias("look")]
    public class CameraLook : Command
    {
    	[Token(Token = "0x4001956")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter Enable;

    	[Token(Token = "0x4001957")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("zone")]
    	[VectorContext("X,Y", -1, null)]
    	public DecimalListParameter LookZone;

    	[Token(Token = "0x4001958")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("speed")]
    	[VectorContext("X,Y", -1, null)]
    	public DecimalListParameter LookSpeed;

    	[Token(Token = "0x4001959")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Gravity;

    	[Token(Token = "0x400195A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly Vector2 defaultZone;

    	[Token(Token = "0x400195B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private static readonly Vector2 defaultSpeed;

    	[Token(Token = "0x6002036")]
    	[Address(RVA = "0x4E4470", Offset = "0x4E2E70", VA = "0x1804E4470", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002037")]
    	[Address(RVA = "0x4E46B0", Offset = "0x4E30B0", VA = "0x1804E46B0")]
    	public CameraLook()
    	{
    	}
    }
}
