using System.Runtime.InteropServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x200002E")]
public class RebuildableLabel : MonoBehaviour
{
	[Token(Token = "0x40000A8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private TextMeshProUGUI _uiLabel;

	[Token(Token = "0x40000A9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("Parameters")]
	[SerializeField]
	private LabelData _labelData;

	[Token(Token = "0x40000AA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Space]
	[SerializeField]
	private float _defaultFontSize;

	[Token(Token = "0x40000AB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private Color _defaultLabelColor;

	[Token(Token = "0x40000AC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
	[SerializeField]
	private float _defaultCharacterSpacing;

	[Token(Token = "0x40000AD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private HorizontalAlignmentOptions _defaultHorizontalAlignment;

	[Token(Token = "0x40000AE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Space]
	[SerializeField]
	private string _labelId;

	[Token(Token = "0x40000AF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private ILocalizationProvider _localizationProvider;

	[Token(Token = "0x17000027")]
	public float PreferredWidth
	{
		[Token(Token = "0x60000CE")]
		[Address(RVA = "0xCE42D0", Offset = "0xCE2CD0", VA = "0x180CE42D0")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000028")]
	public float PreferredHeight
	{
		[Token(Token = "0x60000CF")]
		[Address(RVA = "0xCE4300", Offset = "0xCE2D00", VA = "0x180CE4300")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000029")]
	public string LabelId
	{
		[Token(Token = "0x60000D0")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002A")]
	protected TextMeshProUGUI UiLabel
	{
		[Token(Token = "0x60000D1")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002B")]
	protected virtual Color DefaultLabelColor
	{
		[Token(Token = "0x60000D2")]
		[Address(RVA = "0xCE4330", Offset = "0xCE2D30", VA = "0x180CE4330", Slot = "4")]
		get
		{
			return default(Color);
		}
	}

	[Token(Token = "0x1700002C")]
	private ILocalizationProvider LocalizationProvider
	{
		[Token(Token = "0x60000D3")]
		[Address(RVA = "0xCE4340", Offset = "0xCE2D40", VA = "0x180CE4340")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000D4")]
	[Address(RVA = "0xCE4400", Offset = "0xCE2E00", VA = "0x180CE4400")]
	private void Reset()
	{
	}

	[Token(Token = "0x60000D5")]
	[Address(RVA = "0xCE44A0", Offset = "0xCE2EA0", VA = "0x180CE44A0")]
	public void Rebuild(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
	{
	}

	[Token(Token = "0x60000D6")]
	[Address(RVA = "0xCE4610", Offset = "0xCE3010", VA = "0x180CE4610")]
	private void RebuildElements(LabelData labelData, [Optional] LocaleKind? locale)
	{
	}

	[Token(Token = "0x60000D7")]
	[Address(RVA = "0xCE4940", Offset = "0xCE3340", VA = "0x180CE4940")]
	private void SetInternalData(LabelData labelData)
	{
	}

	[Token(Token = "0x60000D8")]
	[Address(RVA = "0xCE4AE0", Offset = "0xCE34E0", VA = "0x180CE4AE0")]
	private void ChangeFont()
	{
	}

	[Token(Token = "0x60000D9")]
	[Address(RVA = "0xCE4BF0", Offset = "0xCE35F0", VA = "0x180CE4BF0")]
	private void ModifyLabel(LabelData labelData, [Optional] LocaleKind? locale)
	{
	}

	[Token(Token = "0x60000DA")]
	[Address(RVA = "0xCE4DB0", Offset = "0xCE37B0", VA = "0x180CE4DB0")]
	public void InsertTexts(params string[] texts)
	{
	}

	[Token(Token = "0x60000DB")]
	[Address(RVA = "0xCE5010", Offset = "0xCE3A10", VA = "0x180CE5010")]
	public void StoreLabelParametersAsDefault()
	{
	}

	[Token(Token = "0x60000DC")]
	[Address(RVA = "0xCE50D0", Offset = "0xCE3AD0", VA = "0x180CE50D0")]
	public void ResetDefaultParameters()
	{
	}

	[Token(Token = "0x60000DD")]
	[Address(RVA = "0x311810", Offset = "0x310210", VA = "0x180311810")]
	public RebuildableLabel()
	{
	}
}
