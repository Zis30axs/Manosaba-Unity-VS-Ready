using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000560")]
public class LoadingPanel : CustomUI, ILoadingUI, IManagedUI
{
	[Token(Token = "0x400148B")]
	[FieldOffset(Offset = "0xE0")]
	[Tooltip("Event invoked when script preload progress is changed, in 0.0 to 1.0 range.")]
	[SerializeField]
	private FloatUnityEvent onProgressChanged;

	[Token(Token = "0x400148C")]
	[FieldOffset(Offset = "0xE8")]
	private IScriptLoader loader;

	[Token(Token = "0x6001AA6")]
	[Address(RVA = "0x62DCB0", Offset = "0x62C6B0", VA = "0x18062DCB0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001AA7")]
	[Address(RVA = "0x62DD70", Offset = "0x62C770", VA = "0x18062DD70", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001AA8")]
	[Address(RVA = "0x62DE20", Offset = "0x62C820", VA = "0x18062DE20", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001AA9")]
	[Address(RVA = "0x62DED0", Offset = "0x62C8D0", VA = "0x18062DED0", Slot = "55")]
	protected override void HandleVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x6001AAA")]
	[Address(RVA = "0x62DF40", Offset = "0x62C940", VA = "0x18062DF40", Slot = "95")]
	protected virtual void HandleProgressChanged(float value)
	{
	}

	[Token(Token = "0x6001AAB")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public LoadingPanel()
	{
	}
}
