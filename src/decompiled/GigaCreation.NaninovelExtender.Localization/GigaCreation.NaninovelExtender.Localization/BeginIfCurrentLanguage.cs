using System.Runtime.InteropServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.Localization;

[Token(Token = "0x2000004")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("ifLang")]
public class BeginIfCurrentLanguage : BeginIf
{
	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public StringParameter LanguageName;

	[Token(Token = "0x4000007")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter Unless;

	[Token(Token = "0x4000008")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ILocalizationProvider _localizationProvider;

	[Token(Token = "0x17000001")]
	private ILocalizationProvider LocalizationProvider
	{
		[Token(Token = "0x6000003")]
		[Address(RVA = "0xD30D10", Offset = "0xD2F710", VA = "0x180D30D10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD30DD0", Offset = "0xD2F7D0", VA = "0x180D30DD0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public BeginIfCurrentLanguage()
	{
	}
}
