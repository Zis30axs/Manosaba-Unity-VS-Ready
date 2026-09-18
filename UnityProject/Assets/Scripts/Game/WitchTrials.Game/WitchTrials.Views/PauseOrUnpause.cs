using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000010")]
    public class PauseOrUnpause : MonoBehaviour
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0x30DD40, 99 B
    	// icall `Time::set_timeScale(Single)`; the non-zero value is the float 1.0 at data RVA 0x2AE2820.
    	[Token(Token = "0x6000030")]
    	[Address(RVA = "0x30DD40", Offset = "0x30C740", VA = "0x18030DD40")]
    	public void Pause(bool enable)
    	{
    		Time.timeScale = (enable ? 0f : 1f);
    	}

    	[Token(Token = "0x6000031")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public PauseOrUnpause()
    	{
    	}
    }
}
