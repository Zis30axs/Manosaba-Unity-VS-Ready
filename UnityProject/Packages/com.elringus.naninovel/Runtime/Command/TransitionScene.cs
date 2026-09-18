using Naninovel.Metadata;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Performs scene transition masking the real scene content with anything that is visible at the moment
the command starts execution (except the UI), executing nested commands to change the scene and finishing
with specified [transition effect](/guide/transition-effects).<br/><br/>
The command works similar to actor appearance transitions, but covers the whole scene. Use it to change multiple
actors and other visible entities to a new state in a single batch with a transition effect.",
        @"
The UI will be hidden and user input blocked while the transition is in progress (nested commands are running).
You can change that by overriding the `ISceneTransitionUI`, which handles the transition process.<br/><br/>
Async nested commands will execute immediately, w/o the need to specify `time:0` for each.<br/><br/>
The nested block is expected to always finish; don't nest any commands that could
navigate outside the nested block, as this may cause undefined behaviour.",
        @"
; Set up initial scene with 'Felix' character and sunny vibe.
@char Felix
@back SunnyDay
@sun power:1
Felix: What a nice day!

; Transition to new scene with 'Jenna' character and rainy vibe
; via 'DropFade' transition effect over 3 seconds.
@trans DropFade time:3
    @hide Felix
    @char Jenna
    @back RainyDay
    @sun power:0
    @rain power:1
Jenna: When will the damn rain stop?"
    )]
    [CommandAlias("trans"), RequireNested, Branch(BranchTraits.Nest | BranchTraits.Return)]
    public class TransitionScene : Command, Command.INestedHost, Command.IPreloadable
    {
        [Doc("Type of the [transition effect](/guide/transition-effects) to use (crossfade is used by default).")]
        [ParameterAlias(NamelessParameterAlias), ConstantContext(typeof(TransitionType))]
        public StringParameter Transition;
        [Doc("Parameters of the transition effect.")]
        [ParameterAlias("params")]
        public DecimalListParameter TransitionParams;
        [Doc("Path to the [custom dissolve](/guide/transition-effects#custom-transition-effects) texture (path should be relative to a `Resources` folder). " +
             "Has effect only when the transition is set to `Custom` mode.")]
        [ParameterAlias("dissolve")]
        public StringParameter DissolveTexturePath;
        [Doc("Name of the [easing function](/guide/transition-effects#animation-easing) to use for the transition.")]
        [ParameterAlias("easing"), ConstantContext(typeof(EasingType))]
        public StringParameter EasingTypeName;
        [Doc("Duration (in seconds) of the transition.")]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration = .35f;

        private IScriptPlayer player => Engine.GetServiceOrErr<IScriptPlayer>();
        private bool running;
        private Texture2D preloadedDissolveTexture;

        public virtual async UniTask PreloadResources ()
        {
            if (Assigned(DissolveTexturePath) && !DissolveTexturePath.DynamicValue)
            {
                var loader = Resources.LoadAsync<Texture2D>(DissolveTexturePath);
                await loader;
                preloadedDissolveTexture = loader.asset as Texture2D;
            }
        }

        public virtual void ReleaseResources ()
        {
            preloadedDissolveTexture = null;
        }

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsEnteringNestedAt(playedIndex))
                return running
                    ? playedIndex + 1
                    : playlist.SkipNestedAt(playedIndex, Indent);

            if (playlist.IsExitingNestedAt(playedIndex, Indent))
            {
                if (!running) return playlist.ExitNestedAt(playedIndex, Indent);
                // force nested commands to complete instantly
                player.Complete().Forget();
                return playlist.IndexOf(this);
            }

            return playedIndex + 1;
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            if (!running) await StartTransition();
            else await FinishTransition(token);
        }

        protected virtual UniTask StartTransition ()
        {
            running = true;
            var transitionUI = GetTransitionUI();
            return transitionUI.CaptureScene();
        }

        protected virtual async UniTask FinishTransition (AsyncToken token)
        {
            running = false;
            var easing = ResolveEasing();
            var transition = ResolveTransition();
            var transitionUI = GetTransitionUI();
            using var _ = CompleteOnContinueWhenEnabled(ref token);
            await transitionUI.Transition(transition, new(Duration, easing), token);
        }

        protected virtual EasingType ResolveEasing ()
        {
            var type = EasingType.Linear;
            if (Assigned(EasingTypeName) && !ParseUtils.TryConstantParameter(EasingTypeName, out type))
                Warn($"Failed to parse '{EasingTypeName}' easing.");
            return type;
        }

        protected virtual Transition ResolveTransition ()
        {
            var name = TransitionUtils.ResolveParameterValue(Transition);
            var defaults = TransitionUtils.GetDefaultParams(name);
            var @params = Assigned(TransitionParams) ? new(
                TransitionParams.ElementAtOrNull(0) ?? defaults.x,
                TransitionParams.ElementAtOrNull(1) ?? defaults.y,
                TransitionParams.ElementAtOrNull(2) ?? defaults.z,
                TransitionParams.ElementAtOrNull(3) ?? defaults.w) : defaults;

            if (Assigned(DissolveTexturePath) && !ObjectUtils.IsValid(preloadedDissolveTexture))
                preloadedDissolveTexture = Resources.Load<Texture2D>(DissolveTexturePath);

            return new(name, @params, preloadedDissolveTexture);
        }

        protected virtual UI.ISceneTransitionUI GetTransitionUI ()
        {
            var ui = Engine.GetServiceOrErr<IUIManager>().GetUI<UI.ISceneTransitionUI>();
            if (ui is null) Err($"Failed to perform scene transition: '{nameof(UI.ISceneTransitionUI)}' UI is not available.");
            return ui;
        }
    }
}
