namespace Naninovel.Commands
{
    [Doc(
        @"
Removes all the messages from [printer backlog](/guide/text-printers#printer-backlog).",
        null,
        @"
; Printed text will be removed from the backlog.
Lorem ipsum dolor sit amet, consectetur adipiscing elit.
@clearBacklog"
    )]
    public class ClearBacklog : Command
    {
        public override UniTask Execute (AsyncToken token = default)
        {
            Engine.GetService<IUIManager>()?.GetUI<UI.IBacklogUI>()?.Clear();
            return UniTask.CompletedTask;
        }
    }
}
