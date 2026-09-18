namespace Naninovel.Commands
{
    [Doc(
        @"
Shows a text printer.",
        null,
        @"
; Show a default printer.
@showPrinter",
        @"
; Show printer with ID 'Wide'.
@showPrinter Wide"
    )]
    public class ShowPrinter : PrinterCommand
    {
        [Doc("ID of the printer actor to use. Will use a default one when not specified.")]
        [ParameterAlias(NamelessParameterAlias), ActorContext(TextPrintersConfiguration.DefaultPathPrefix)]
        public StringParameter PrinterId;
        [Doc(SharedDocs.DurationParameter)]
        [ParameterAlias("time")]
        public DecimalParameter Duration;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        protected override string AssignedPrinterId => PrinterId;

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Hide, Wait, token);
        }

        protected virtual async UniTask Hide (AsyncToken token)
        {
            var printer = await GetOrAddPrinter(token);
            var printerMeta = PrinterManager.Configuration.GetMetadataOrDefault(printer.Id);
            var showDuration = Assigned(Duration) ? Duration.Value : printerMeta.ChangeVisibilityDuration;
            if (token.Completed) printer.Visible = true;
            else await printer.ChangeVisibility(true, new(showDuration), token: token);
        }
    }
}
