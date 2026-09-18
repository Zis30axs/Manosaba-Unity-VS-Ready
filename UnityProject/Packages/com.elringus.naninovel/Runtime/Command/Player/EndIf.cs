namespace Naninovel.Commands
{
    [Doc(
        @"
Alternative to using indentation in conditional blocks: marks end of the block
opened with previous [@if] command, no matter the indentation.
For usage examples see [conditional execution](/guide/naninovel-scripts#conditional-execution) guide."
    )]
    public class EndIf : Command
    {
        public override UniTask Execute (AsyncToken token = default) => UniTask.CompletedTask;
    }
}
