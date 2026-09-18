namespace Naninovel.Commands
{
    [Doc(
        @"
Used to apply [generic parameters](/guide/naninovel-scripts#generic-parameters) via `[< ...]` syntax.",
        null,
        @"
; After printing following line waiting for input won't activate
; (player won't have to confirm prompt to continue reading).
Lorem ipsum dolor sit amet.[< skip!]",
        @"
; Following line will be authored by Kohaku and Yuko actors, while
; the display name label on the printer will show ""All Together"".
Kohaku,Yuko: How low hello![< as:""All Together""]",
        @"
; First part of the sentence will be printed with 50% speed,
; while the second one with 250% speed and wait for input won't activate.
Lorem ipsum[< speed:0.5] dolor sit amet.[< speed:2.5 skip!]"
    )]
    [CommandAlias("<")]
    public class ParametrizeGeneric : Command, Command.IPreloadable, Command.ILocalizable
    {
        [Doc("ID of the printer actor to use.")]
        [ParameterAlias("printer"), ActorContext(TextPrintersConfiguration.DefaultPathPrefix)]
        public StringParameter PrinterId;
        [Doc("ID of the actor, which should be associated with the printed message. " +
             "Specify `*` or use `,` to delimit multiple actor IDs to make all/selected characters authors of the text; " +
             "useful when coupled with `as` parameter to represent multiple characters speaking at the same time.")]
        [ParameterAlias("author"), ActorContext(CharactersConfiguration.DefaultPathPrefix)]
        public StringParameter AuthorId;
        [Doc("When specified, will use the label instead of author ID (or associated display name) " +
             "to represent author name in the text printer while printing the message. Useful to " +
             "override default name for a few messages or represent multiple authors speaking at the same time " +
             "without triggering author-specific behaviour of the text printer, such as message color or avatar.")]
        [ParameterAlias("as")]
        public LocalizableTextParameter AuthorLabel;
        [Doc("Text reveal speed multiplier; should be positive or zero. Setting to one will yield the default speed.")]
        [ParameterAlias("speed")]
        public DecimalParameter RevealSpeed;
        [Doc("Whether to not wait for user input after finishing the printing task.")]
        [ParameterAlias("skip")]
        public BooleanParameter SkipWaitingInput;
        [Doc("Whether to not reset printed text before printing this line effectively appending the text.")]
        [ParameterAlias("join")]
        public BooleanParameter Join;

        public UniTask PreloadResources () => PreloadStaticTextResources(AuthorLabel);
        public void ReleaseResources () => ReleaseStaticTextResources(AuthorLabel);

        public override async UniTask Execute (AsyncToken token = default)
        {
            using var _ = await LoadDynamicTextResources(AuthorLabel);
        }
    }
}
