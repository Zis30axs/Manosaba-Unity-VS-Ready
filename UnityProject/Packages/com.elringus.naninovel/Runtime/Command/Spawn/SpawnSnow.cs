using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Spawns particle system simulating [snow](/guide/special-effects#snow).",
        null,
        @"
; Start intensive snow over 10 seconds.
@snow power:300 time:10
; Stop the snow over 30 seconds.
@snow power:0 time:30"
    )]
    [CommandAlias("snow")]
    public class SpawnSnow : SpawnLocalizedEffect
    {
        [Doc("The intensity of the snow (particles spawn rate per second); defaults to 100. Set to 0 to disable (de-spawn) the effect.")]
        [ParameterAlias("power")]
        public DecimalParameter Intensity;
        [Doc("The particle system will gradually grow the spawn rate to the target level over the specified time, in seconds.")]
        [ParameterAlias("time")]
        public DecimalParameter FadeDuration;

        protected override string Path => "Snow";
        protected override bool DestroyWhen => Assigned(Intensity) && Intensity == 0;

        protected override StringListParameter GetSpawnParameters () => new List<string> {
            ToSpawnParam(Intensity),
            ToSpawnParam(FadeDuration)
        };

        protected override StringListParameter GetDestroyParameters () => new List<string> {
            ToSpawnParam(FadeDuration)
        };
    }
}
