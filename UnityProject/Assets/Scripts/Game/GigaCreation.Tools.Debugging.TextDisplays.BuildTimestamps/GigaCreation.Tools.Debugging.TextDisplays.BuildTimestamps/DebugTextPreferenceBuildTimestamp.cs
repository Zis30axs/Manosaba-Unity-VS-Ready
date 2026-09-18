using GigaCreation.Tools.BuildTimestampDisplay;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.TextDisplays.BuildTimestamps
{
    [Token(Token = "0x2000004")]
    public class DebugTextPreferenceBuildTimestamp : DebugTextPreferenceBase
    {
    	[Token(Token = "0x4000006")]
    	private const string BuildTimestampAssetNotFoundMessage = "The BuildTimestamp asset could not be found. This asset will be generated after you run the build.";

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x50")]
    	[Space]
    	[SerializeField]
    	private BuildTimestamp _buildTimestamp;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private string _format;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private float _utcOffsetHours;

    	[Token(Token = "0x17000001")]
    	protected override string LabelText
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0xD44510", Offset = "0xD42F10", VA = "0x180D44510", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public void SetBuildTimestampAsset()
    	{
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD44650", Offset = "0xD43050", VA = "0x180D44650")]
    	public DebugTextPreferenceBuildTimestamp()
    	{
    	}
    }
}
