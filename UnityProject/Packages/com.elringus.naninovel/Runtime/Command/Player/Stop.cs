namespace Naninovel.Commands
{
    [Doc(
        @"
Stops the naninovel script execution.",
        null,
        @"
Show the choices and halt script execution until the player picks one.
@choice ""Choice 1""
@choice ""Choice 2""
@stop
We'll get here after player will make a choice."
    )]
    public class Stop : Command
    {
        public override UniTask Execute (AsyncToken token = default)
        {
            Engine.GetServiceOrErr<IScriptPlayer>().Stop();
            return UniTask.CompletedTask;
        }
    }
}
