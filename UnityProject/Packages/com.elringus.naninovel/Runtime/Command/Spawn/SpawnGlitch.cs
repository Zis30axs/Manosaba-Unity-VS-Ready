using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Applies [digital glitch](/guide/special-effects#digital-glitch)
post-processing effect to the main camera simulating digital video distortion and artifacts.",
        null,
        @"
; Apply the glitch effect with default parameters.
@glitch
; Apply the effect over 3.33 seconds with a low intensity.
@glitch time:3.33 power:0.1"
    )]
    [CommandAlias("glitch")]
    public class SpawnGlitch : SpawnEffect
    {
        [Doc("The duration of the effect, in seconds; default is 1.")]
        [ParameterAlias("time")]
        public DecimalParameter Duration;
        [Doc("The intensity of the effect, in 0.0 to 10.0 range; default is 1.")]
        [ParameterAlias("power")]
        public DecimalParameter Intensity;

        protected override string Path => "DigitalGlitch";

        protected override StringListParameter GetSpawnParameters () => new List<string> {
            ToSpawnParam(Duration),
            ToSpawnParam(Intensity)
        };
    }
}
