using GigaCreation.Tools.Service;
using FastEnumUtility;
using System.Runtime.InteropServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.Localization
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD30D10, 186 B -- lazy: [this+0x48], else ServiceLocator.Get<ILocalizationManager>(cl = 1) (0x180751910)
    		// Annotated disassembly: work/title-path/disas/bicl_01_get_LocalizationProvider.txt
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0xD30D10", Offset = "0xD2F710", VA = "0x180D30D10")]
    		get
    		{
    			return _localizationProvider ??= ServiceLocator.Get<GigaCreation.Essentials.Localization.ILocalizationManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD30DD0, 416 B
    	// The stock BeginIf.Execute with the expression replaced by a language test:
    	//   0x180D30ED0  the ignored `if` parameter warning (Command vtable +0x1A8 = Warn), as in BeginIf.Execute
    	//   0x180D30F75  LanguageName [+0x38] .Value (HasValue tested, an unassigned parameter ends in the NullReferenceException tail 0x180D310CE)
    	//                .Replace("-", "") (String.Replace) -> FastEnum.TryParse<LocaleKind>(name, ignoreCase = true, out kind) (0x180696BA0)
    	//   0x180D30FCA  Unless [+0x40] -> bool? (Nullable<bool>.op_Implicit); its value byte selects != (setne) / == (sete) of
    	//                ILocalizationProvider slot 1 CurrentLocale against the parsed kind; no match -> BeginIf.HandleConditionalBlock(this) (0x1804EDF20)
    	//   0x180D31064  parse failure -> Warn(String.Format("Invalid language name: {0}.", LanguageName))
    	// Annotated disassembly: work/title-path/disas/bicl_02_Execute.txt
    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD30DD0", Offset = "0xD2F7D0", VA = "0x180D30DD0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		if (Assigned(ConditionalExpression))
    		{
    			Warn("Parameter 'if' in '@if' command is ignored; use nameless parameter for the condition instead.");
    		}
    		if (FastEnum.TryParse<LocaleKind>(LanguageName.Value.Replace("-", ""), true, out LocaleKind languageKind))
    		{
    			bool unless = ((bool?)Unless).GetValueOrDefault();
    			bool isCurrent = unless ? LocalizationProvider.CurrentLocale != languageKind : LocalizationProvider.CurrentLocale == languageKind;
    			if (!isCurrent)
    			{
    				HandleConditionalBlock(this);
    			}
    		}
    		else
    		{
    			Warn(string.Format("Invalid language name: {0}.", LanguageName));
    		}
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public BeginIfCurrentLanguage()
    	{
    	}
    }
}
