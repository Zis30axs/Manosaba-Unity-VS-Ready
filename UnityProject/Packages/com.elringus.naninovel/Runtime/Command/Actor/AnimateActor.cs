using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Naninovel.Commands
{
    [Doc(
        @"
Animate properties of the actors with the specified IDs via key frames.
Key frames for the animated parameters are delimited with commas.",
        @"
It's not recommended to use this command for complex animations. Naniscript is a scenario scripting DSL and not
suited for complex automation or specification such as animation. Consider using dedicated animation tools instead,
such as Unity's [Animator](https://docs.unity3d.com/Manual/AnimationSection.html).

Be aware, that this command searches for actors with the specified IDs over all the actor managers,
and in case multiple actors with the same ID exist (eg, a character and a text printer), this will affect only the first found one.

When running the animate commands in parallel (`wait` is set to false) the affected actors state can mutate unpredictably.
This could cause unexpected results when rolling back or performing other commands that affect state of the actor. Make sure to reset
affected properties of the animated actors (position, tint, appearance, etc) after the command finishes or use `@animate CharacterId`
(without any args) to stop the animation prematurely.",
        @"
; Animate 'Kohaku' actor over three animation steps (key frames),
; changing positions: first step will take 1, second — 0.5 and third — 3 seconds.
@animate Kohaku posX:50,0,85 time:1,0.5,3 wait!",
        @"
; Start loop animations of 'Yuko' and 'Kohaku' actors; notice, that you can skip
; key values indicating that the parameter shouldn't change during the animation step.
@animate Kohaku,Yuko loop! appearance:Surprise,Sad,Default,Angry transition:DropFade,Ripple,Pixelate posX:15,85,50 posY:0,-25,-85 scale:1,1.25,1.85 tint:#25f1f8,lightblue,#ffffff,olive easing:EaseInBounce,EaseInQuad time:3,2,1,0.5
...
; Stop the animations.
@animate Yuko,Kohaku !loop",
        @"
; Start a long background animation for 'Kohaku'.
@animate Kohaku posX:90,0,90 scale:1,2,1 time:10
; Do something else while the animation is running.
...
; Here we're going to set a specific position for the character,
; but the animation could still be running in background, so reset it first.
@animate Kohaku
; Now it's safe to modify previously animated properties.
@char Kohaku pos:50 scale:1"
    )]
    [CommandAlias("animate")]
    public class AnimateActor : Command
    {
        /// <summary>
        /// Literals used to delimit adjacent animation key values.
        /// </summary>
        /// <remarks>
        /// Comma should be used by default; colon is for backward compat and may be removed in future.
        /// </remarks>
        public static readonly char[] KeyDelimiters = { ',', '|' };
        /// <summary>
        /// Path to the prefab to spawn with <see cref="ISpawnManager"/>.
        /// </summary>
        public const string prefabPath = "Animate";

        [Doc("IDs of the actors to animate.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringListParameter ActorIds;
        [Doc("Whether to loop the animation; make sure to set `wait` to false when loop is enabled, otherwise script playback will loop indefinitely.")]
        [ParameterDefaultValue("false")]
        public BooleanParameter Loop = false;
        [Doc("Appearances to set for the animated actors.")]
        public StringParameter Appearance;
        [Doc("Type of the [transition effect](/guide/transition-effects) to use when animating appearance change (crossfade is used by default).")]
        public StringParameter Transition;
        [Doc("Visibility status to set for the animated actors.")]
        public StringParameter Visibility;
        [Doc("Position values over X-axis (in 0 to 100 range, in percents from the left border of the scene) to set for the animated actors.")]
        [ParameterAlias("posX")]
        public StringParameter ScenePositionX;
        [Doc("Position values over Y-axis (in 0 to 100 range, in percents from the bottom border of the scene) to set for the animated actors.")]
        [ParameterAlias("posY")]
        public StringParameter ScenePositionY;
        [Doc("Position values over Z-axis (in world space) to set for the animated actors; while in ortho mode, can only be used for sorting.")]
        [ParameterAlias("posZ")]
        public StringParameter PositionZ;
        [Doc("Rotation values (over Z-axis) to set for the animated actors.")]
        public StringParameter Rotation;
        [Doc("Scale (`x,y,z` or a single uniform value) to set for the animated actors.")]
        public StringParameter Scale;
        [Doc(SharedDocs.TintParameter)]
        [ParameterAlias("tint")]
        public StringParameter TintColor;
        [Doc(SharedDocs.EasingParameter)]
        [ParameterAlias("easing")]
        public StringParameter EasingTypeName;
        [Doc("Duration of the animations per key, in seconds. When a key value is missing, will use one from a previous key. Default is 0.35s for all keys.")]
        [ParameterAlias("time")]
        public StringParameter Duration;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        private const string defaultDuration = "0.35";

        public static string BuildSpawnPath (string actorId) => $"{prefabPath}{SpawnConfiguration.IdDelimiter}{actorId}";

        public override async UniTask Execute (AsyncToken token = default)
        {
            var spawner = Engine.GetServiceOrErr<ISpawnManager>();
            using var _ = ListPool<UniTask<SpawnedObject>>.Rent(out var spawnTasks);
            foreach (var actorId in ActorIds)
                spawnTasks.Add(Spawn(spawner, actorId, token));
            var spawned = await UniTask.WhenAll(spawnTasks);
            await WaitOrForget(token => UniTask.WhenAll(spawned.Select(s => s.AwaitSpawn(token))), Wait, token);
        }

        protected virtual async UniTask<SpawnedObject> Spawn (ISpawnManager spawner, string actorId, AsyncToken token)
        {
            var path = BuildSpawnPath(actorId);
            var spawned = await spawner.GetOrSpawn(path, token);
            var paras = GetParametersForActor(actorId);
            spawned.SetSpawnParameters(paras, false);
            return spawned;
        }

        protected virtual IReadOnlyList<string> GetParametersForActor (string actorId)
        {
            var parameters = new string[13]; // Don't cache it, otherwise parameters will leak across actors on async spawn init.
            parameters[0] = actorId;
            parameters[1] = Loop.Value.ToString(CultureInfo.InvariantCulture);
            parameters[2] = Assigned(Appearance) ? Appearance : null;
            parameters[3] = Assigned(Transition) ? Transition : null;
            parameters[4] = Assigned(Visibility) ? Visibility : null;
            parameters[5] = Assigned(ScenePositionX) ? ScenePositionX : null;
            parameters[6] = Assigned(ScenePositionY) ? ScenePositionY : null;
            parameters[7] = Assigned(PositionZ) ? PositionZ : null;
            parameters[8] = Assigned(Rotation) ? Rotation : null;
            parameters[9] = Assigned(Scale) ? Scale : null;
            parameters[10] = Assigned(TintColor) ? TintColor : null;
            parameters[11] = Assigned(EasingTypeName) ? EasingTypeName : null;
            parameters[12] = Assigned(Duration) ? Duration.Value : defaultDuration;
            return parameters;
        }
    }
}
