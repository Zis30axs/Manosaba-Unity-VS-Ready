using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Spawns particle system simulating [sun shafts](/guide/special-effects#sun-shafts) aka god rays.",
        null,
        @"
; Start intensive sunshine over 10 seconds.
@sun power:1 time:10
; Stop the sunshine over 30 seconds.
@sun power:0 time:30"
    )]
    [CommandAlias("sun")]
    public class SpawnSun : SpawnLocalizedEffect
    {
        [Doc("The intensity of the rays (opacity), in 0.0 to 1.0 range; default is 0.85. Set to 0 to disable (de-spawn) the effect.")]
        [ParameterAlias("power")]
        public DecimalParameter Intensity;
        [Doc("The particle system will gradually grow the spawn rate to the target level over the specified time, in seconds.")]
        [ParameterAlias("time")]
        public DecimalParameter FadeDuration;

        protected override string Path => "SunShafts";
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
