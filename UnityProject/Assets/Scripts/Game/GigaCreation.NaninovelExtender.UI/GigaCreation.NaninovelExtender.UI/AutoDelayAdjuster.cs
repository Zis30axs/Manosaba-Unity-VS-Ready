using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3F160 -- lazy: _scriptPlayerConfiguration (+0x20), else
    		//   Engine.GetConfiguration<ScriptPlayerConfiguration>()
    		// Annotated disassembly: work/title-path/disas/ada_0xD3F160.txt
    		[Token(Token = "0x6000097")]
    		[Address(RVA = "0xD3F160", Offset = "0xD3DB60", VA = "0x180D3F160")]
    		get
    		{
    			if (_scriptPlayerConfiguration == null)
    			{
    				_scriptPlayerConfiguration = Engine.GetConfiguration<ScriptPlayerConfiguration>();
    			}
    			return _scriptPlayerConfiguration;
    		}
    	}

    	[Token(Token = "0x17000016")]
    	private IAudioManager NaniAudioManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3F220 -- lazy: _naniAudioManager (+0x28), else Engine.GetServiceOrErr<IAudioManager>()
    		// Annotated disassembly: work/title-path/disas/ada_0xD3F220.txt
    		[Token(Token = "0x6000098")]
    		[Address(RVA = "0xD3F220", Offset = "0xD3DC20", VA = "0x180D3F220")]
    		get
    		{
    			if (_naniAudioManager == null)
    			{
    				_naniAudioManager = Engine.GetServiceOrErr<IAudioManager>();
    			}
    			return _naniAudioManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3F2E0 (the body shared with RestoreIfShortened) -- RestoreIfShortened()
    	// Annotated disassembly: work/title-path/disas/ada_0xD3F2E0.txt
    	[Token(Token = "0x6000099")]
    	[Address(RVA = "0xD3F2E0", Offset = "0xD3DCE0", VA = "0x180D3F2E0")]
    	private void OnDestroy()
    	{
    		RestoreIfShortened();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3F320, 529 B -- voice = NaniAudioManager.GetPlayedVoice() (IAudioManager slot 29);
    	//   a non-null voice other than _previousPlayedVoicePath (+0x38) shortens the auto-play delay, anything else restores
    	//   it (both inlined); the voice is remembered
    	// Annotated disassembly: work/title-path/disas/ada_0xD3F320.txt
    	[Token(Token = "0x600009A")]
    	[Address(RVA = "0xD3F320", Offset = "0xD3DD20", VA = "0x180D3F320")]
    	public void Adjust()
    	{
    		string playedVoice = NaniAudioManager.GetPlayedVoice();
    		if (playedVoice != null && playedVoice != _previousPlayedVoicePath)
    		{
    			ShortenIfNotShortened();
    		}
    		else
    		{
    			RestoreIfShortened();
    		}
    		_previousPlayedVoicePath = playedVoice;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3F540, 82 B -- unless _isShortened (+0x30): store MinAutoPlayDelay (+0x20) in
    	//   _storedMinAutoPlayDelay (+0x34), multiply it by DelayMultiplier 0.2 (data 0x2AE2670), set _isShortened
    	// Annotated disassembly: work/title-path/disas/ada_0xD3F540.txt
    	[Token(Token = "0x600009B")]
    	[Address(RVA = "0xD3F540", Offset = "0xD3DF40", VA = "0x180D3F540")]
    	private void ShortenIfNotShortened()
    	{
    		if (!_isShortened)
    		{
    			_storedMinAutoPlayDelay = ScriptPlayerConfiguration.MinAutoPlayDelay;
    			ScriptPlayerConfiguration.MinAutoPlayDelay *= 0.2f;
    			_isShortened = true;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3F2E0, 44 B -- with _isShortened: MinAutoPlayDelay = _storedMinAutoPlayDelay,
    	//   _isShortened = false
    	// Annotated disassembly: work/title-path/disas/ada_0xD3F2E0.txt
    	[Token(Token = "0x600009C")]
    	[Address(RVA = "0xD3F2E0", Offset = "0xD3DCE0", VA = "0x180D3F2E0")]
    	private void RestoreIfShortened()
    	{
    		if (_isShortened)
    		{
    			ScriptPlayerConfiguration.MinAutoPlayDelay = _storedMinAutoPlayDelay;
    			_isShortened = false;
    		}
    	}

    	[Token(Token = "0x600009D")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public AutoDelayAdjuster()
    	{
    	}
    }
}
