using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Cameras
{
    [Token(Token = "0x2000004")]
    [EditInProjectSettings]
    public class CameraConfigurationExtended : Configuration
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x18")]
    	public Vector2Int[] ResolutionsForPc;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x20")]
    	[Range(0f, 1f)]
    	public float[] ResolutionCoefficientsForMobile;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x28")]
    	public int[] TargetFpsValues;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x30")]
    	public CameraManagerExtended.SettingsExtended DefaultGraphicsOptions;

    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
    	public CameraConfigurationExtended()
    	{
    	}
    }
}
