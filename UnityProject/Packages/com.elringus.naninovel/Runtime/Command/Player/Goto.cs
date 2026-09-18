using System;
using System.Diagnostics.CodeAnalysis;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Navigates naninovel script playback to the specified path.",
        null,
        @"
; Loads and starts playing 'Script001' script from the start.
@goto Script001",
        @"
; Save as above, but start playing from the label 'AfterStorm'.
@goto Script001.AfterStorm",
        @"
; Navigates to 'Epilogue' label in the currently played script.
@goto .Epilogue
...
# Epilogue
..."
    )]
    [Branch(BranchTraits.Endpoint)]
    public class Goto : Command
    {
        /// <summary>
        /// When applied to an <see cref="IEngineService"/> implementation, the service won't be reset
        /// while executing the goto command and <see cref="StateConfiguration.ResetOnGoto"/> is enabled.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, Inherited = false)]
        public sealed class DontResetAttribute : Attribute { }

        /// <summary>
        /// When assigned to <see cref="ResetState"/>, forces reset of all the services,
        /// except the ones with <see cref="DontResetAttribute"/>.
        /// </summary>
        public const string ResetAllFlag = "*";
        /// <summary>
        /// When assigned to <see cref="ResetState"/>, forces no reset.
        /// </summary>
        public const string NoResetFlag = "-";

        [Doc("Path to navigate into in the following format: `ScriptPath.Label`. " +
             "When label is omitted, will play specified script from the start. " +
             "When script path is omitted, will attempt to find a label in the currently played script.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, EndpointContext]
        public NamedStringParameter Path;
        [Doc("When specified, will control whether to reset the engine services state before loading a script (in case the path is leading to another script):<br/>" +
             " - Specify `*` to reset all the services, except the ones with `Goto.DontReset` attribute.<br/>" +
             " - Specify service type names (separated by comma) to exclude from reset; all the other services will be reset, including the ones with `Goto.DontReset` attribute.<br/>" +
             " - Specify `-` to force no reset (even if it's enabled by default in the configuration).<br/><br/>" +
             "Notice, that while some services have `Goto.DontReset` attribute applied and are not reset by default, they should still be specified when excluding specific services from reset.")]
        [ParameterAlias("reset")]
        public StringListParameter ResetState;
        [Doc("Whether to hold resources in the target script, which make them preload together with the script this command specified in. " +
             "Has no effect outside `Conservative` resource policy. Refer to [memory management](/guide/memory-management) guide for more info.")]
        public BooleanParameter Hold;
        [Doc("Whether to release resources before navigating to the target script to free the memory. " +
             "Has no effect outside `Optimistic` resource policy. Refer to [memory management](/guide/memory-management) guide for more info.")]
        public BooleanParameter Release;

        protected IScriptManager Scripts => Engine.GetServiceOrErr<IScriptManager>();
        protected IScriptPlayer Player => Engine.GetServiceOrErr<IScriptPlayer>();
        protected IScriptLoader Loader => Engine.GetServiceOrErr<IScriptLoader>();
        protected ResourcePolicy Policy => Engine.GetConfiguration<ResourceProviderConfiguration>().ResourcePolicy;

        public override async UniTask Execute (AsyncToken token = default)
        {
            if (!TryGetScriptPathAndLabel(out var scriptPath, out var label)) return;
            if (ShouldNavigatePlayedScript(scriptPath)) NavigatePlayedScript(label);
            else if (ShouldMaskWithLoadingScreen())
                using (await LoadingScreen.Show(token))
                    await NavigateOtherScript(scriptPath, label);
            else await NavigateOtherScript(scriptPath, label);
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

        protected virtual bool ShouldMaskWithLoadingScreen ()
        {
            if (!Player.Configuration.ShowLoadingUI) return false;
            if (Policy == ResourcePolicy.Conservative) return !Assigned(Hold) || !Hold;
            return Assigned(Release) && Release;
        }

        protected virtual async UniTask NavigateOtherScript (string scriptPath, [MaybeNull] string label)
        {
            var state = Engine.GetServiceOrErr<IStateManager>();
            if (ShouldResetAll()) await state.ResetState(Engine.Types.DontReset, LoadAndPlay);
            else if (ShouldResetSpecific()) await state.ResetState(ResetState.ToReadOnlyList(), LoadAndPlay);
            else await LoadAndPlay();

            UniTask LoadAndPlay () => string.IsNullOrEmpty(label)
                ? Player.LoadAndPlay(scriptPath)
                : Player.LoadAndPlayAtLabel(scriptPath, label);
        }

        protected virtual bool ShouldResetAll ()
        {
            var config = Engine.GetConfiguration<StateConfiguration>();
            return !Assigned(ResetState) && config.ResetOnGoto ||
                   Assigned(ResetState) && ResetState.Length == 1 && ResetState[0] == ResetAllFlag;
        }

        protected virtual bool ShouldResetSpecific ()
        {
            return Assigned(ResetState) && ResetState.Length > 0 && ResetState[0] != NoResetFlag;
        }
    }
}
