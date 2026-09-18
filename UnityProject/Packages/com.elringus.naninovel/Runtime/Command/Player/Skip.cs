namespace Naninovel.Commands
{
    [Doc(
        @"
Allows to enable or disable script player ""skip"" mode.",
        null,
        @"
; Enable skip mode.
@skip",
        @"
; Disable skip mode.
@skip false"
    )]
    public class Skip : Command
    {
        [Doc("Whether to enable (default) or disable the skip mode.")]
        [ParameterAlias(NamelessParameterAlias), ParameterDefaultValue("true")]
        public BooleanParameter Enable = true;

        public override UniTask Execute (AsyncToken token = default)
        {
            var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
            scriptPlayer.SetSkipEnabled(Enable);
            return UniTask.CompletedTask;
        }
    }
}
