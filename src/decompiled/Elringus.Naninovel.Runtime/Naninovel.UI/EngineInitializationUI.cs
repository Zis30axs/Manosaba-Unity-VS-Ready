using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x200055C")]
public class EngineInitializationUI : ScriptableUIBehaviour
{
	[Token(Token = "0x4001484")]
	[FieldOffset(Offset = "0x80")]
	[Tooltip("Event invoked when engine initialization progress is changed, in 0.0 to 1.0 range.")]
	[SerializeField]
	private FloatUnityEvent onInitializationProgress;

	[Token(Token = "0x6001A9A")]
	[Address(RVA = "0x62D080", Offset = "0x62BA80", VA = "0x18062D080", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001A9B")]
	[Address(RVA = "0x62D270", Offset = "0x62BC70", VA = "0x18062D270", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001A9C")]
	[Address(RVA = "0x62D460", Offset = "0x62BE60", VA = "0x18062D460", Slot = "59")]
	protected virtual void NotifyProgressChanged(float value)
	{
	}

	[Token(Token = "0x6001A9D")]
	[Address(RVA = "0x38A020", Offset = "0x388A20", VA = "0x18038A020")]
	public EngineInitializationUI()
	{
	}
}
