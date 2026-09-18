namespace Naninovel.Commands
{
    [Doc(
        @"
Attempts to navigate naninovel script playback to a command after the last used [@gosub].
See [@gosub] command summary for more info and usage examples."
    )]
    public class Return : Command
    {
        [Doc("When specified, will reset the engine services state before returning to the initial script " +
             "from which the gosub was entered (in case it's not the currently played script). " +
             "Specify `*` to reset all the services, or specify service names to exclude from reset. " +
             "By default, the state does not reset.")]
        [ParameterAlias("reset")]
        public StringListParameter ResetState;

        protected IScriptPlayer Player => Engine.GetServiceOrErr<IScriptPlayer>();

        public override async UniTask Execute (AsyncToken token = default)
        {
            if (Player.GosubReturnSpots.Count == 0 || string.IsNullOrWhiteSpace(Player.GosubReturnSpots.Peek().ScriptPath))
            {
                Warn("Failed to return to the last gosub: state data is missing or invalid.");
                return;
            }
            await Reset();
            Navigate();
        }

        protected virtual async UniTask Reset ()
        {
            var stateManager = Engine.GetServiceOrErr<IStateManager>();
            if (Assigned(ResetState) && ResetState.Length == 1 && ResetState[0] == "*") await stateManager.ResetState();
            else if (Assigned(ResetState) && ResetState.Length > 0) await stateManager.ResetState(ResetState.ToReadOnlyList());
        }

        protected virtual void Navigate ()
        {
            var spot = Player.GosubReturnSpots.Pop();
            if (Player.PlayedScript && Player.PlayedScript.Path.EqualsFastIgnoreCase(spot.ScriptPath))
            {
                Player.ResumeAtLine(spot.LineIndex);
                return;
            }
            Player.PlayAtLine(spot.ScriptPath, spot.LineIndex, spot.InlineIndex);
        }
    }
}
