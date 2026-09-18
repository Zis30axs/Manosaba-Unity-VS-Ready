namespace Naninovel.Commands
{
    [Doc(
        @"
Shows an input field UI where user can enter an arbitrary text.
Upon submit the entered text will be assigned to the specified custom variable.",
        @"
To assign a display name for a character using this command consider [binding the name to a custom variable](/guide/characters#display-names).",
        @"
; Prompt to enter an arbitrary text and assign it to 'name' custom variable.
@input name summary:""Choose your name.""
; Halt the playback until player submits the input.
@stop

; You can then inject the assigned 'name' variable in naninovel scripts.
Archibald: Greetings, {name}!

; ...or use it inside set and conditional expressions.
@set score++ if:name==""Felix"""
    )]
    [CommandAlias("input")]
    public class InputCustomVariable : Command, Command.IPreloadable, Command.ILocalizable
    {
        [Doc("Name of a custom variable to which the entered text will be assigned.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringParameter VariableName;
        [Doc("Type of the input content; defaults to the specified variable type." +
             "Use to change assigned variable type or when assigning to a new variable. " +
             "Supported types: `String`, `Numeric`, `Boolean`.")]
        [ParameterAlias("type"), ConstantContext(typeof(CustomVariableValueType))]
        public StringParameter VariableType;
        [Doc("An optional summary text to show along with input field. " +
             "When the text contain spaces, wrap it in double quotes (`\"`). " +
             "In case you wish to include the double quotes in the text itself, escape them.")]
        public LocalizableTextParameter Summary;
        [Doc("A predefined value to set for the input field.")]
        [ParameterAlias("value")]
        public LocalizableTextParameter PredefinedValue;
        [Doc("Whether to automatically resume script playback when user submits the input form.")]
        [ParameterAlias("play"), ParameterDefaultValue("true")]
        public BooleanParameter PlayOnSubmit = true;

        public UniTask PreloadResources () => PreloadStaticTextResources(Summary, PredefinedValue);
        public void ReleaseResources () => ReleaseStaticTextResources(Summary, PredefinedValue);

        public override async UniTask Execute (AsyncToken token = default)
        {
            using var _ = await LoadDynamicTextResources(Summary, PredefinedValue);
            var valueType = ResolveType();
            var inputUI = Engine.GetServiceOrErr<IUIManager>().GetUI<UI.IVariableInputUI>();
            inputUI?.Show(VariableName, valueType, Summary, PredefinedValue, PlayOnSubmit);
        }

        protected virtual CustomVariableValueType ResolveType ()
        {
            if (Assigned(VariableType))
            {
                if (!ParseUtils.TryConstantParameter(VariableType, out CustomVariableValueType type))
                    Warn($"Failed to parse '{VariableType}' enum.");
                return type;
            }
            if (Engine.GetServiceOrErr<ICustomVariableManager>().TryGetVariableValue(VariableName, out var value))
                return value.Type;
            return CustomVariableValueType.String;
        }
    }
}
