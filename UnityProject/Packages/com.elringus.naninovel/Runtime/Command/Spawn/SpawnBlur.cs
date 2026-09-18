using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Applies [blur effect](/guide/special-effects#blur) to supported actor:
backgrounds and characters of sprite, layered, diced, Live2D, Spine, video and scene implementations.",
        @"
The actor should have `IBlurable` interface implemented in order to support the effect.",
        @"
; Blur main background with default parameters.
@blur
; Remove blur from the main background.
@blur power:0",
        @"
; Blur 'Kohaku' actor with max power over 5 seconds.
@blur Kohaku power:1 time:5
; Remove blur from 'Kohaku' over 3.1 seconds.
@blur Kohaku power:0 time:3.1"
    )]
    [CommandAlias("blur")]
    public class SpawnBlur : SpawnEffect
    {
        [Doc("ID of the actor to apply the effect for; in case multiple actors with the same ID found " +
             "(eg, a character and a printer), will affect only the first found one. " +
             "When not specified, applies to the main background.")]
        [ParameterAlias(NamelessParameterAlias), ActorContext, ParameterDefaultValue(BackgroundsConfiguration.MainActorId)]
        public StringParameter ActorId = BackgroundsConfiguration.MainActorId;
        [Doc("Intensity of the effect, in 0.0 to 1.0 range. Defaults to 0.5. Set to 0 to disable (de-spawn) the effect.")]
        [ParameterAlias("power")]
        public DecimalParameter Intensity;
        [Doc("How long it will take the parameters to reach the target values, in seconds. Defaults to 1.0.")]
        [ParameterAlias("time")]
        public DecimalParameter FadeDuration;

        protected override string Path => $"Blur#{ActorId}";
        protected override bool DestroyWhen => Assigned(Intensity) && Intensity == 0;

        protected override StringListParameter GetSpawnParameters () => new List<string> {
            ToSpawnParam(ActorId),
            ToSpawnParam(Intensity),
            ToSpawnParam(FadeDuration)
        };

        protected override StringListParameter GetDestroyParameters () => new List<string> {
            ToSpawnParam(FadeDuration)
        };
    }
}
