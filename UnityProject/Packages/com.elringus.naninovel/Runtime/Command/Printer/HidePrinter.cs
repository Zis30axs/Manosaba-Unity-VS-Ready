namespace Naninovel.Commands
{
    [Doc(
        @"
Hides a text printer.",
        null,
        @"
; Hide a default printer.
@hidePrinter",
        @"
; Hide printer with ID 'Wide'.
@hidePrinter Wide"
    )]
    public class HidePrinter : PrinterCommand
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
            var hideDuration = Assigned(Duration) ? Duration.Value : printerMeta.ChangeVisibilityDuration;
            if (token.Completed) printer.Visible = false;
            else await printer.ChangeVisibility(false, new(hideDuration), token: token);
        }
    }
}
