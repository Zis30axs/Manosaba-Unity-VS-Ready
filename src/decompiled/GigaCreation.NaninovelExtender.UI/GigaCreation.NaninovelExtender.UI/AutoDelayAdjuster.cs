using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x2000025")]
public class AutoDelayAdjuster : MonoBehaviour
{
	[Token(Token = "0x4000096")]
	private const float DelayMultiplier = 0.2f;

	[Token(Token = "0x4000097")]
	[FieldOffset(Offset = "0x20")]
	private ScriptPlayerConfiguration _scriptPlayerConfiguration;

	[Token(Token = "0x4000098")]
	[FieldOffset(Offset = "0x28")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x4000099")]
	[FieldOffset(Offset = "0x30")]
	private bool _isShortened;

	[Token(Token = "0x400009A")]
	[FieldOffset(Offset = "0x34")]
	private float _storedMinAutoPlayDelay;

	[Token(Token = "0x400009B")]
	[FieldOffset(Offset = "0x38")]
	private string _previousPlayedVoicePath;

	[Token(Token = "0x17000015")]
	private ScriptPlayerConfiguration ScriptPlayerConfiguration
	{
		[Token(Token = "0x6000097")]
		[Address(RVA = "0xD3F160", Offset = "0xD3DB60", VA = "0x180D3F160")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000016")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x6000098")]
		[Address(RVA = "0xD3F220", Offset = "0xD3DC20", VA = "0x180D3F220")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000099")]
	[Address(RVA = "0xD3F2E0", Offset = "0xD3DCE0", VA = "0x180D3F2E0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x600009A")]
	[Address(RVA = "0xD3F320", Offset = "0xD3DD20", VA = "0x180D3F320")]
	public void Adjust()
	{
	}

	[Token(Token = "0x600009B")]
	[Address(RVA = "0xD3F540", Offset = "0xD3DF40", VA = "0x180D3F540")]
	private void ShortenIfNotShortened()
	{
	}

	[Token(Token = "0x600009C")]
	[Address(RVA = "0xD3F2E0", Offset = "0xD3DCE0", VA = "0x180D3F2E0")]
	private void RestoreIfShortened()
	{
	}

	[Token(Token = "0x600009D")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public AutoDelayAdjuster()
	{
	}
}
