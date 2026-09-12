using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.BuildTimestampDisplay;

[Token(Token = "0x2000005")]
[RequireComponent(typeof(Text))]
[AddComponentMenu("GIGA CREATION/Build Timestamp Display Legacy Text")]
public class BuildTimestampDisplayLegacyText : MonoBehaviour
{
	[Token(Token = "0x400000C")]
	[FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private BuildTimestamp _buildTimestamp;

	[Token(Token = "0x400000D")]
	[FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private Text _label;

	[Token(Token = "0x400000E")]
	[FieldOffset(Offset = "0x30")]
	[Header("Parameters")]
	[SerializeField]
	private string _format;

	[Token(Token = "0x400000F")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private float _utcOffsetHours;

	[Token(Token = "0x17000001")]
	private Text Label
	{
		[Token(Token = "0x6000006")]
		[Address(RVA = "0xD41990", Offset = "0xD40390", VA = "0x180D41990")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000007")]
	[Address(RVA = "0xD41AD0", Offset = "0xD404D0", VA = "0x180D41AD0")]
	private void Start()
	{
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xD41D30", Offset = "0xD40730", VA = "0x180D41D30")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public void SetBuildTimestampAsset()
	{
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0xD41DD0", Offset = "0xD407D0", VA = "0x180D41DD0")]
	public BuildTimestampDisplayLegacyText()
	{
	}
}
