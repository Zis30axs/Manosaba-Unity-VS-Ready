using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x20005DA")]
    public interface IVariableInputUI : IManagedUI
    {
    	[Token(Token = "0x6001D2C")]
    	void Show(string variableName, CustomVariableValueType valueType, LocalizableText summary, LocalizableText predefinedValue, bool playOnSubmit);
    }
}
