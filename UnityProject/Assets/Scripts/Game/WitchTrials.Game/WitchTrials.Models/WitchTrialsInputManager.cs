using System.Runtime.InteropServices;
using GigaCreation.Essentials.Input;
using GigaCreation.NaninovelExtender.Audio;
using Il2CppDummyDll;

namespace WitchTrials.Models
{
    [Token(Token = "0x200016B")]
    public class WitchTrialsInputManager : InputManager<WitchTrialsActions>
    {
    	[Token(Token = "0x40006E5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly string _naninovelActionMapName;

    	[Token(Token = "0x40006E6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private IAudioManagerExtended _naniAudioManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x397C20, 156 B
    	// InputManager<WitchTrialsActions>..ctor first, then `_naninovelActionMapName` (+0x20) = argument.
    	[Token(Token = "0x6000625")]
    	[Address(RVA = "0x397C20", Offset = "0x396620", VA = "0x180397C20")]
    	public WitchTrialsInputManager(string naninovelActionMapName)
    	{
    		_naninovelActionMapName = naninovelActionMapName;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x397CC0, 529 B
    	// - `_naniAudioManager` (+0x28) null: inlined Engine.TryGetService<IAudioManagerExtended> (METHODINFO present, body
    	//   Engine.GetService<T> into a local); null result -> Debug.LogWarning("Naninovel AudioManager not found.") and return;
    	//   otherwise cache it.
    	// - IAudioManagerExtended slot 0 PlayResponseSfx(kind).
    	[Token(Token = "0x6000626")]
    	[Address(RVA = "0x397CC0", Offset = "0x3966C0", VA = "0x180397CC0", Slot = "9")]
    	public override void PlayResponseSfx(ResponseKind kind)
    	{
    		if (_naniAudioManager == null)
    		{
    			if (!Naninovel.Engine.TryGetService<IAudioManagerExtended>(out IAudioManagerExtended audioManager))
    			{
    				UnityEngine.Debug.LogWarning("Naninovel AudioManager not found.");
    				return;
    			}
    			_naniAudioManager = audioManager;
    		}
    		_naniAudioManager.PlayResponseSfx(kind);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x397EE0, 261 B
    	// `actionMapName` null -> `_naninovelActionMapName` (+0x20); then the base InputManager<T>.BuildActionNameOrId
    	// (0x832780) inlined: whitespace-only/null map name -> actionName, else map + "/" + actionName.
    	[Token(Token = "0x6000627")]
    	[Address(RVA = "0x397EE0", Offset = "0x3968E0", VA = "0x180397EE0", Slot = "10")]
    	protected override string BuildActionNameOrId(string actionName, [Optional] string actionMapName)
    	{
    		if (actionMapName == null)
    		{
    			actionMapName = _naninovelActionMapName;
    		}
    		return base.BuildActionNameOrId(actionName, actionMapName);
    	}
    }
}
