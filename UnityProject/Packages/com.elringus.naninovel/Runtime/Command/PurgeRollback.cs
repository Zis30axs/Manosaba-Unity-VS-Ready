namespace Naninovel.Commands
{
    [Doc(
        @"
Prevents player from rolling back to the previous state snapshots.",
        null,
        @"
; Prevent player from rolling back to try picking another choice.

Pick a choice. You won't be able to rollback.
@choice One goto:.One
@choice Two goto:.Two
@stop

# One
@purgeRollback
You've picked one.
@stop

# Two
@purgeRollback
You've picked two.
@stop"
    )]
    public class PurgeRollback : Command
    {
        public override UniTask Execute (AsyncToken token = default)
        {
            Engine.GetService<IStateManager>()?.PurgeRollbackData();
            return UniTask.CompletedTask;
        }
    }
}
