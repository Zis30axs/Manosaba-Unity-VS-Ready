using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Simulates [depth of field](/guide/special-effects#depth-of-field-bokeh) (aka DOF, bokeh) effect,
when only the object in focus stays sharp, while others are blurred.",
        null,
        @"
; Enable the effect with defaults and lock focus on 'Kohaku' game object.
@bokeh focus:Kohaku
; Fade-off (disable) the effect over 10 seconds.
@bokeh power:0 time:10
; Set focus point 10 units away from the camera,
; focal distance to 0.95 and apply it over 3 seconds.
@bokeh dist:10 power:0.95 time:3"
    )]
    [CommandAlias("bokeh")]
    public class SpawnBokeh : SpawnEffect
    {
        [Doc("Name of the game object to set focus for (optional). When set, the focus will always " +
             "stay on the game object, while `dist` parameter will be ignored.")]
        [ParameterAlias("focus")]
        public StringParameter FocusObjectName;
        [Doc("Distance (in units) from Naninovel camera to the focus point. " +
             "Ignored when `focus` parameter is specified. Defaults to 10.")]
        [ParameterAlias("dist")]
        public DecimalParameter FocusDistance;
        [Doc("Amount of blur to apply for the de-focused areas; also determines focus sensitivity. " +
             "Defaults to 3.75. Set to 0 to disable (de-spawn) the effect.")]
        [ParameterAlias("power")]
        public DecimalParameter FocalLength;
        [Doc("How long it will take the parameters to reach the target values, in seconds. Defaults to 1.0.")]
        [ParameterAlias("time")]
        public DecimalParameter Duration;

        protected override string Path => "DepthOfField";
        protected override bool DestroyWhen => Assigned(FocalLength) && FocalLength == 0;

        protected override StringListParameter GetSpawnParameters () => new List<string> {
            ToSpawnParam(FocusObjectName),
            ToSpawnParam(FocusDistance),
            ToSpawnParam(FocalLength),
            ToSpawnParam(Duration)
        };

        protected override StringListParameter GetDestroyParameters () => new List<string> {
            ToSpawnParam(Duration)
        };
    }
}
