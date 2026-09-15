using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000018")]
public class SetShaderTimerAsUnscale : MonoBehaviour
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x30F1C0 (.cctor, 89 B): the static initializer `Shader.PropertyToID("_UnscaledTime")`
	[Token(Token = "0x400002C")]
	[FieldOffset(Offset = "0x0")]
	private static readonly int s_unscaledTime = Shader.PropertyToID("_UnscaledTime");

	// RECONSTRUCTED | Confidence: A | RVA: 0x30F0D0, 224 B
	// icalls `Time::get_unscaledTime()` and `Shader::SetGlobalFloatImpl(Int32,Single)` (Shader.SetGlobalFloat(int, float) inlined).
	[Token(Token = "0x6000050")]
	[Address(RVA = "0x30F0D0", Offset = "0x30DAD0", VA = "0x18030F0D0")]
	private void Update()
	{
		Shader.SetGlobalFloat(s_unscaledTime, Time.unscaledTime);
	}

	[Token(Token = "0x6000051")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SetShaderTimerAsUnscale()
	{
	}
}
