namespace Naninovel.Commands
{
    [Doc(
        @"
Modifies a [text printer actor](/guide/text-printers).",
        null,
        @"
; Will make 'Wide' printer default and hide any other visible printers.
@printer Wide",
        @"
; Will assign 'Right' appearance to 'Bubble' printer, make is default,
; position at the center of the scene and won't hide other printers.
@printer Bubble.Right pos:50,50 !hideOther"
    )]
    [CommandAlias("printer")]
    [ActorContext(TextPrintersConfiguration.DefaultPathPrefix, paramId: "Id")]
    public class ModifyTextPrinter : ModifyOrthoActor<ITextPrinterActor, TextPrinterState, TextPrinterMetadata, TextPrintersConfiguration, ITextPrinterManager>
    {
        [Doc("ID of the printer to modify and the appearance to set. When ID or appearance are not specified, will use default ones.")]
        [ParameterAlias(NamelessParameterAlias), ActorContext(TextPrintersConfiguration.DefaultPathPrefix, 0), AppearanceContext(1)]
        public NamedStringParameter IdAndAppearance;
        [Doc("Whether to make the printer the default one. Default printer will be subject of all the printer-related commands when `printer` parameter is not specified.")]
        [ParameterAlias("default"), ParameterDefaultValue("true")]
        public BooleanParameter MakeDefault = true;
        [Doc("Whether to hide all the other printers.")]
        [ParameterDefaultValue("true")]
        public BooleanParameter HideOther = true;

        protected override bool AllowPreload => !Assigned(IdAndAppearance) || !IdAndAppearance.DynamicValue;
        protected override string AssignedId => !string.IsNullOrEmpty(IdAndAppearance?.Name) ? IdAndAppearance.Name : ActorManager.DefaultPrinterId;
        protected override string AlternativeAppearance => IdAndAppearance?.NamedValue;

        protected override async UniTask Modify (AsyncToken token)
        {
            await base.Modify(token);

            if (MakeDefault && !string.IsNullOrEmpty(AssignedId))
                ActorManager.DefaultPrinterId = AssignedId;

            if (HideOther)
            {
                var tween = new Tween(AssignedDuration, complete: !Lazy);
                foreach (var printer in ActorManager.Actors)
                    if (printer.Id != AssignedId && printer.Visible)
                        printer.ChangeVisibility(false, tween, token: token).Forget();
            }
        }
    }
}
