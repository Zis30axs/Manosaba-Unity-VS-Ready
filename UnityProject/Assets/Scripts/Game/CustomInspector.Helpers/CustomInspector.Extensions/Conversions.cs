using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector.Extensions
{
    [Token(Token = "0x2000007")]
    public static class Conversions
    {
    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0x445570", Offset = "0x443F70", VA = "0x180445570")]
    	public static Quaternion ToQuaternion(this Vector4 v4)
    	{
    		return default(Quaternion);
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0x445590", Offset = "0x443F90", VA = "0x180445590")]
    	public static bool IsUnityNull(this object obj)
    	{
    		return default(bool);
    	}
    }
}
