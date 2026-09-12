using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization;

[Token(Token = "0x2000013")]
public class LocalizationPresenter : MonoBehaviour
{
	[Token(Token = "0x4000032")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private LocaleData _sourceLocaleData;

	[Token(Token = "0x4000033")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private LocaleData[] _localizationLocaleData;

	[Token(Token = "0x4000034")]
	[FieldOffset(Offset = "0x30")]
	private ILocalizationManager _localizationManager;

	[Token(Token = "0x6000033")]
	[Address(RVA = "0xCD34B0", Offset = "0xCD1EB0", VA = "0x180CD34B0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000034")]
	[Address(RVA = "0xCD39E0", Offset = "0xCD23E0", VA = "0x180CD39E0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000035")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LocalizationPresenter()
	{
	}
}
