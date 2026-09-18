using Il2CppDummyDll;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001C9")]
    public static class DebugTextPreferenceExtensions
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0x3B4820, 82 B
    	// `cmovne` between the literals 'O' and 'X'.
    	[Token(Token = "0x60007C1")]
    	[Address(RVA = "0x3B4820", Offset = "0x3B3220", VA = "0x1803B4820")]
    	public static string ToDebugString(this bool self)
    	{
    		return self ? "O" : "X";
    	}
    }
}
