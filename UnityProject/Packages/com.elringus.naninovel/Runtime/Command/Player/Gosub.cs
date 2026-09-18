using System.Diagnostics.CodeAnalysis;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Navigates naninovel script playback to the specified path and saves that path to global state;
[@return] commands use this info to redirect to command after the last invoked gosub command.",
        @"
While this command can be used as a function (subroutine) to invoke a common set of script lines,
remember that NaniScript is a scenario scripting DSL and is not suited for general programming.
It's strongly recommended to use [custom commands](/guide/custom-commands) instead.",
        @"
; Navigate to 'VictoryScene' label in the currently played script, then
; execute the commands and navigate back to the command after the 'gosub'.
@gosub .VictoryScene
...
@stop
# VictoryScene
@back Victory
@sfx Fireworks
@bgm Fanfares
You are victorious!
@return",
        @"
; Another example with some branching inside the subroutine.
@set time=10
; Here we get one result.
@gosub .Room
...
@set time=3
; And here we get another.
@gosub .Room
@stop
# Room
@print ""It's too early, I should visit after sunset."" if:time<21&time>6
@print ""I can sense an ominous presence!"" if:time>21|time<6
@return"
    )]
    [Branch(BranchTraits.Endpoint | BranchTraits.Return)]
    public class Gosub : Command
    {
        [Doc("Path to navigate into in the following format: `ScriptPath.Label`. " +
             "When label is omitted, will play specified script from the start. " +
             "When script path is omitted, will attempt to find a label in the currently played script.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, EndpointContext]
        public NamedStringParameter Path;
        [Doc("When specified, will reset the engine services state before loading a script (in case the path is leading to another script). " +
             "Specify `*` to reset all the services, or specify service names to exclude from reset. " +
             "By default, the state does not reset.")]
        [ParameterAlias("reset")]
        public StringListParameter ResetState;

        protected IScriptPlayer Player => Engine.GetServiceOrErr<IScriptPlayer>();
        protected IScriptManager Scripts => Engine.GetServiceOrErr<IScriptManager>();

        public override UniTask Execute (AsyncToken token = default)
        {
            PushReturnSpot();
            if (!TryGetScriptPathAndLabel(out var scriptPath, out var label)) return UniTask.CompletedTask;
            if (ShouldNavigatePlayedScript(scriptPath)) NavigatePlayedScript(label);
            else if (string.IsNullOrEmpty(label)) return Player.LoadAndPlay(scriptPath);
            else return Player.LoadAndPlayAtLabel(scriptPath, label);
            return UniTask.CompletedTask;
        }

        protected virtual void PushReturnSpot ()
        {
            var returnIndex = Player.Playlist.MoveAt(Player.PlayedIndex);
            var returnCommand = Player.Playlist[returnIndex];
            Player.GosubReturnSpots.Push(returnCommand.PlaybackSpot);
        }

        protected virtual bool TryGetScriptPathAndLabel (out string scriptPath, [MaybeNull] out string label)
        {
            scriptPath = Path.Value.Name;
            label = Path.Value.Value;
            var valid = !string.IsNullOrWhiteSpace(scriptPath) || Player.PlayedScript;
            if (!valid) Err("Failed to execute '@goto' command: script path is not specified and no script is currently played.");
            return valid;
        }

        protected virtual bool ShouldNavigatePlayedScript (string scriptPath)
        {
            return string.IsNullOrWhiteSpace(scriptPath) || scriptPath.StartsWithFast(Script.TransientPathPrefix) ||
                   Player.PlayedScript && scriptPath.EqualsFastIgnoreCase(Player.PlayedScript.Path);
        }

        protected virtual void NavigatePlayedScript ([MaybeNull] string label)
        {
            if (string.IsNullOrEmpty(label)) Player.Resume();
            else if (Player.PlayedScript.LabelExists(label)) Player.ResumeAtLabel(label);
            else Err($"Failed navigating script playback to '{label}' label: label not found in '{Player.PlayedScript.Path}' script.");
        }

        protected virtual async UniTask Reset ()
        {
            var stateManager = Engine.GetServiceOrErr<IStateManager>();
            if (Assigned(ResetState) && ResetState.Length == 1 && ResetState[0] == "*") await stateManager.ResetState();
            else if (Assigned(ResetState) && ResetState.Length > 0) await stateManager.ResetState(ResetState.ToReadOnlyList());
        }
    }
}
