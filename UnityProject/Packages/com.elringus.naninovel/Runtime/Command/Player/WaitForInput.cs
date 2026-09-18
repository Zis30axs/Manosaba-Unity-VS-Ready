namespace Naninovel.Commands
{
    [Doc(
        @"
Holds script execution until user activates a `continue` input. Shortcut for `@wait i`.",
        null,
        @"
; User will have to activate a 'continue' input after the first sentence
; for the printer to continue printing out the following text.
Lorem ipsum dolor sit amet.[i] Consectetur adipiscing elit."
    )]
    [CommandAlias("i")]
    public class WaitForInput : Command
    {
        public override async UniTask Execute (AsyncToken token = default)
        {
            var waitCommand = new Wait { PlaybackSpot = PlaybackSpot, Indent = Indent };
            waitCommand.WaitMode = Wait.InputLiteral;
            await waitCommand.Execute(token);
        }
    }
}
