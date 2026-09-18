using System.Linq;
using UnityEngine;

namespace Naninovel.Commands
{
    public abstract class ModifyActor<TActor, TState, TMeta, TConfig, TManager> : Command, Command.IPreloadable
        where TActor : class, IActor
        where TState : ActorState<TActor>, new()
        where TMeta : ActorMetadata
        where TConfig : ActorManagerConfiguration<TMeta>, new()
        where TManager : class, IActorManager<TActor, TState, TMeta, TConfig>
    {
        [Doc("ID of the actor to modify; specify `*` to affect all visible actors.")]
        public StringParameter Id;
        [Doc("Appearance to set for the modified actor.")]
        [AppearanceContext]
        public StringParameter Appearance;
        [Doc("Pose to set for the modified actor.")]
        public StringParameter Pose;
        [Doc("Type of the [transition effect](/guide/transition-effects) to use (crossfade is used by default).")]
        [ConstantContext(typeof(TransitionType))]
        public StringParameter Transition;
        [Doc("Parameters of the transition effect.")]
        [ParameterAlias("params")]
        public DecimalListParameter TransitionParams;
        [Doc("Path to the [custom dissolve](/guide/transition-effects#custom-transition-effects) texture (path should be relative to a `Resources` folder). " +
             "Has effect only when the transition is set to `Custom` mode.")]
        [ParameterAlias("dissolve")]
        public StringParameter DissolveTexturePath;
        [Doc("Visibility status to set for the modified actor.")]
        public BooleanParameter Visible;
        [Doc("Position (in world space) to set for the modified actor. Use Z-component (third member) to move (sort) by depth while in ortho mode.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Position;
        [Doc("Rotation to set for the modified actor.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Rotation;
        [Doc("Scale to set for the modified actor.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Scale;
        [Doc(SharedDocs.TintParameter)]
        [ParameterAlias("tint"), ColorContext]
        public StringParameter TintColor;
        [Doc(SharedDocs.EasingParameter)]
        [ParameterAlias("easing"), ConstantContext(typeof(EasingType))]
        public StringParameter EasingTypeName;
        [Doc(SharedDocs.DurationParameter)]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;
        [Doc(SharedDocs.LazyParameter)]
        [ParameterDefaultValue("false")]
        public BooleanParameter Lazy = false;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        protected virtual string AssignedId => Id;
        protected virtual string AssignedTransition => Transition;
        protected virtual string AssignedAppearance => Assigned(Appearance) ? Appearance.Value : PosedAppearance ?? (PosedViaAppearance ? null : AlternativeAppearance);
        protected virtual bool? AssignedVisibility => Assigned(Visible) ? Visible.Value : PosedVisibility;
        protected virtual float?[] AssignedPosition => Assigned(Position) ? Position : PosedPosition;
        protected virtual float?[] AssignedRotation => Assigned(Rotation) ? Rotation : PosedRotation;
        protected virtual float?[] AssignedScale => Assigned(Scale) ? Scale : PosedScale;
        protected virtual Color? AssignedTintColor => Assigned(TintColor) ? ParseColor(TintColor) : PosedTintColor;
        protected virtual float AssignedDuration => Assigned(Duration) ? Duration.Value : ActorManager.ActorManagerConfiguration.DefaultDuration;
        protected virtual TManager ActorManager => Engine.GetServiceOrErr<TManager>();
        protected virtual TConfig Configuration => ActorManager.Configuration;
        protected virtual string AlternativeAppearance => null;
        protected virtual bool AllowPreload => Assigned(Id) && !Id.DynamicValue && Assigned(Appearance) && !Appearance.DynamicValue;
        protected virtual bool PosedViaAppearance => GetPoseOrNull() != null && !Assigned(Pose);

        protected string PosedAppearance => GetPosed(nameof(ActorState.Appearance))?.Appearance;
        protected bool? PosedVisibility => GetPosed(nameof(ActorState.Visible))?.Visible;
        protected float?[] PosedPosition => GetPosed(nameof(ActorState.Position))?.Position.ToNullableArray();
        protected float?[] PosedRotation => GetPosed(nameof(ActorState.Rotation))?.Rotation.eulerAngles.ToNullableArray();
        protected float?[] PosedScale => GetPosed(nameof(ActorState.Scale))?.Scale.ToNullableArray();
        protected Color? PosedTintColor => GetPosed(nameof(ActorState.TintColor))?.TintColor;

        private Texture2D preloadedDissolveTexture;

        public virtual async UniTask PreloadResources ()
        {
            if (Assigned(DissolveTexturePath) && !DissolveTexturePath.DynamicValue)
            {
                var loadTask = Resources.LoadAsync<Texture2D>(DissolveTexturePath);
                await loadTask;
                preloadedDissolveTexture = loadTask.asset as Texture2D;
            }

            if (!AllowPreload || string.IsNullOrEmpty(AssignedId) || AssignedId == "*") return;
            await ActorManager.GetOrAddActor(AssignedId);
            var loader = ActorManager.GetAppearanceLoader(AssignedId);
            if (loader != null && !string.IsNullOrEmpty(AssignedAppearance))
                await loader.LoadOrErr(AssignedAppearance, this);
        }

        public virtual void ReleaseResources ()
        {
            preloadedDissolveTexture = null;

            if (!AllowPreload || string.IsNullOrEmpty(AssignedId)) return;
            var loader = ActorManager?.GetAppearanceLoader(AssignedId);
            if (loader != null && !string.IsNullOrEmpty(AssignedAppearance))
                loader.Release(AssignedAppearance, this);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            var tasks = WaitOrForget(Modify, Wait, token);
            // Make sure actor state is updated before next command is executed,
            // otherwise rollback may get invalid snapshot when not awaited.
            await AsyncUtils.WaitEndOfFrame(token);
            await tasks;
        }

        protected virtual async UniTask Modify (AsyncToken token)
        {
            if (ActorManager is null)
            {
                Err("Can't resolve actors manager.");
                return;
            }

            if (string.IsNullOrEmpty(AssignedId))
            {
                Err("Actor ID was not specified.");
                return;
            }

            var easingType = Configuration.DefaultEasing;
            if (Assigned(EasingTypeName) && !ParseUtils.TryConstantParameter(EasingTypeName, out easingType))
                Warn($"Failed to parse '{EasingTypeName}' easing.");
            if (AssignedId == "*")
            {
                using var _ = ListPool<UniTask>.Rent(out var tasks);
                foreach (var actor in ActorManager.Actors)
                    if (actor.Visible)
                        tasks.Add(ApplyModifications(actor, easingType, token));
                await UniTask.WhenAll(tasks);
            }
            else
            {
                var actor = await ActorManager.GetOrAddActor(AssignedId);
                token.ThrowIfCanceled();
                await ApplyModifications(actor, easingType, token);
            }
        }

        protected virtual async UniTask ApplyModifications (TActor actor, EasingType easingType, AsyncToken token)
        {
            // In case the actor is hidden, apply all the modifications (except visibility) without animation.
            var durationOrZero = actor.Visible ? AssignedDuration : 0;
            // Change appearance with normal duration when a transition is assigned to preserve the effect.
            var appearDuration = string.IsNullOrEmpty(AssignedTransition) ? durationOrZero : AssignedDuration;
            var tween = new Tween(durationOrZero, easingType, complete: !Lazy);
            await UniTask.WhenAll(
                ApplyAppearanceModification(actor, new(appearDuration, easingType, complete: !Lazy), token),
                ApplyPositionModification(actor, tween, token),
                ApplyRotationModification(actor, tween, token),
                ApplyScaleModification(actor, tween, token),
                ApplyTintColorModification(actor, tween, token),
                ApplyVisibilityModification(actor, new(AssignedDuration, easingType, complete: !Lazy), token)
            );
        }

        protected virtual async UniTask ApplyAppearanceModification (TActor actor, Tween tween, AsyncToken token)
        {
            if (string.IsNullOrEmpty(AssignedAppearance)) return;

            var transitionName = TransitionUtils.ResolveParameterValue(AssignedTransition);
            var defaultParams = TransitionUtils.GetDefaultParams(transitionName);
            var transitionParams = Assigned(TransitionParams)
                ? new(
                    TransitionParams.ElementAtOrNull(0) ?? defaultParams.x,
                    TransitionParams.ElementAtOrNull(1) ?? defaultParams.y,
                    TransitionParams.ElementAtOrNull(2) ?? defaultParams.z,
                    TransitionParams.ElementAtOrNull(3) ?? defaultParams.w)
                : defaultParams;
            if (Assigned(DissolveTexturePath) && !ObjectUtils.IsValid(preloadedDissolveTexture))
                preloadedDissolveTexture = Resources.Load<Texture2D>(DissolveTexturePath);
            var transition = new Transition(transitionName, transitionParams, preloadedDissolveTexture);

            await actor.ChangeAppearance(AssignedAppearance, tween, transition, token);
        }

        protected virtual async UniTask ApplyVisibilityModification (TActor actor, Tween tween, AsyncToken token)
        {
            if (AssignedVisibility.HasValue)
                await actor.ChangeVisibility(AssignedVisibility.Value, tween, token);
            else if (!actor.Visible && Configuration.AutoShowOnModify)
                await actor.ChangeVisibility(true, tween, token);
        }

        protected virtual async UniTask ApplyPositionModification (TActor actor, Tween tween, AsyncToken token)
        {
            var position = AssignedPosition;
            if (position is null) return;
            await actor.ChangePosition(new(
                position.ElementAtOrDefault(0) ?? actor.Position.x,
                position.ElementAtOrDefault(1) ?? actor.Position.y,
                position.ElementAtOrDefault(2) ?? actor.Position.z), tween, token);
        }

        protected virtual async UniTask ApplyRotationModification (TActor actor, Tween tween, AsyncToken token)
        {
            var rotation = AssignedRotation;
            if (rotation is null) return;
            await actor.ChangeRotation(Quaternion.Euler(
                rotation.ElementAtOrDefault(0) ?? actor.Rotation.eulerAngles.x,
                rotation.ElementAtOrDefault(1) ?? actor.Rotation.eulerAngles.y,
                rotation.ElementAtOrDefault(2) ?? actor.Rotation.eulerAngles.z), tween, token);
        }

        protected virtual async UniTask ApplyScaleModification (TActor actor, Tween tween, AsyncToken token)
        {
            var scale = AssignedScale;
            if (scale is null) return;
            await actor.ChangeScale(new(
                scale.ElementAtOrDefault(0) ?? actor.Scale.x,
                scale.ElementAtOrDefault(1) ?? actor.Scale.y,
                scale.ElementAtOrDefault(2) ?? actor.Scale.z), tween, token);
        }

        protected virtual async UniTask ApplyTintColorModification (TActor actor, Tween tween, AsyncToken token)
        {
            if (!AssignedTintColor.HasValue) return;
            await actor.ChangeTintColor(AssignedTintColor.Value, tween, token);
        }

        protected virtual Color? ParseColor (string color)
        {
            if (string.IsNullOrEmpty(color)) return null;

            if (!ColorUtility.TryParseHtmlString(TintColor, out var result))
            {
                Err($"Failed to parse '{TintColor}' color to apply tint modification. See the API docs for supported color formats.");
                return null;
            }
            return result;
        }

        protected virtual ActorPose<TState> GetPoseOrNull ()
        {
            var poseName = Assigned(Pose) ? Pose.Value : AlternativeAppearance;
            if (string.IsNullOrEmpty(poseName)) return null;
            return Configuration.GetActorOrSharedPose<TState>(AssignedId, poseName);
        }

        protected virtual TState GetPosed (string propertyName)
        {
            var pose = GetPoseOrNull();
            return pose != null && pose.IsPropertyOverridden(propertyName) ? pose.ActorState : null;
        }
    }
}
