namespace Naninovel.Commands
{
    [Doc("Automatically save the game to a quick save slot.")]
    [CommandAlias("save")]
    public class AutoSave : Command
    {
        public override UniTask Execute (AsyncToken token = default)
        {
            // Don't await here, otherwise script player won't be able to sync the running commands.
            Engine.GetServiceOrErr<IStateManager>().QuickSave().Forget();
            return UniTask.CompletedTask;
        }
    }
}
