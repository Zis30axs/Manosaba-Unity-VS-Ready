namespace Naninovel.Commands
{
    [Doc(
        @"
Appends specified text to a text printer.",
        @"
The entire text is appended instantly, without triggering the reveal effect.",
        @"
; Print first part of the sentence as usual (with gradual reveal),
; then append the end of the sentence at once.
Lorem ipsum
@append "" dolor sit amet."""
    )]
    [CommandAlias("append")]
    public class AppendText : PrinterCommand, Command.IPreloadable, Command.ILocalizable
    {
        [Doc("The text to append.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public LocalizableTextParameter Text;
        [Doc("ID of the printer actor to use. Will use a default one when not specified.")]
        [ParameterAlias("printer"), ActorContext(TextPrintersConfiguration.DefaultPathPrefix)]
        public StringParameter PrinterId;
        [Doc("ID of the actor, which should be associated with the appended text.")]
        [ParameterAlias("author"), ActorContext(CharactersConfiguration.DefaultPathPrefix)]
        public StringParameter AuthorId;

        protected override string AssignedPrinterId => PrinterId;
        protected override string AssignedAuthorId => AuthorId;
        protected IUIManager UIManager => Engine.GetServiceOrErr<IUIManager>();

        public override async UniTask PreloadResources ()
        {
            await base.PreloadResources();
            await PreloadStaticTextResources(Text);
        }

        public override void ReleaseResources ()
        {
            base.ReleaseResources();
            ReleaseStaticTextResources(Text);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            using var _ = await LoadDynamicTextResources(Text);
            var printer = await GetOrAddPrinter(token);
            printer.AppendText(Text);
            printer.RevealProgress = 1f;
            UIManager.GetUI<UI.IBacklogUI>()?.AppendMessage(Text);
        }
    }
}
