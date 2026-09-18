using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Spawns particle system simulating [rain](/guide/special-effects#rain).",
        null,
        @"
; Start intensive Rain over 10 seconds.
@Rain power:1500 time:10
; Stop the Rain over 30 seconds.
@Rain power:0 time:30"
    )]
    [CommandAlias("rain")]
    public class SpawnRain : SpawnLocalizedEffect
    {
        [Doc("The intensity of the rain (particles spawn rate per second); defaults to 500. Set to 0 to disable (de-spawn) the effect.")]
        [ParameterAlias("power")]
        public DecimalParameter Intensity;
        [Doc("The particle system will gradually grow the spawn rate to the target level over the specified time, in seconds.")]
        [ParameterAlias("time")]
        public DecimalParameter FadeDuration;
        [Doc("Multiplier to the horizontal speed of the particles. Use to change angle of the rain drops.")]
        [ParameterAlias("xSpeed")]
        public DecimalParameter XVelocity;
        [Doc("Multiplier to the vertical speed of the particles.")]
        [ParameterAlias("ySpeed")]
        public DecimalParameter YVelocity;

        protected override string Path => "Rain";
        protected override bool DestroyWhen => Assigned(Intensity) && Intensity == 0;

        protected override StringListParameter GetSpawnParameters () => new List<string> {
            ToSpawnParam(Intensity),
            ToSpawnParam(FadeDuration),
            ToSpawnParam(XVelocity),
            ToSpawnParam(YVelocity)
        };

        protected override StringListParameter GetDestroyParameters () => new List<string> {
            ToSpawnParam(FadeDuration)
        };
    }
}
