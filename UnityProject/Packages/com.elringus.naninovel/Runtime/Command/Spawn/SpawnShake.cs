using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Applies [shake effect](/guide/special-effects#shake)
for the actor with the specified ID or main camera.",
        null,
        @"
; Shake 'Dialogue' text printer with default params.
@shake Dialogue",
        @"
; Start shaking 'Kohaku' character, show choice to stop and act accordingly.
@shake Kohaku count:0
@choice ""Continue shaking"" goto:.Continue
@choice ""Stop shaking"" goto:.Stop
@stop
# Stop
@shake Kohaku count:-1
# Continue
...",
        @"
; Shake main Naninovel camera horizontally 5 times.
@shake Camera count:5 hor! !ver"
    )]
    [CommandAlias("shake")]
    public class SpawnShake : SpawnEffect
    {
        [Doc("ID of the actor to shake. In case multiple actors with the same ID found " +
             "(eg, a character and a printer), will affect only the first found one. " +
             "When not specified, will shake the default text printer. " +
             "To shake main camera, use `Camera` keyword.")]
        [ParameterAlias(NamelessParameterAlias), ActorContext]
        public StringParameter ActorId;
        [Doc("The number of shake iterations. When set to 0, will loop until stopped with -1.")]
        [ParameterAlias("count")]
        public IntegerParameter ShakeCount;
        [Doc("The base duration of each shake iteration, in seconds.")]
        [ParameterAlias("time")]
        public DecimalParameter ShakeDuration;
        [Doc("The randomizer modifier applied to the base duration of the effect.")]
        [ParameterAlias("deltaTime")]
        public DecimalParameter DurationVariation;
        [Doc("The base displacement amplitude of each shake iteration, in units.")]
        [ParameterAlias("power")]
        public DecimalParameter ShakeAmplitude;
        [Doc("The randomized modifier applied to the base displacement amplitude.")]
        [ParameterAlias("deltaPower")]
        public DecimalParameter AmplitudeVariation;
        [Doc("Whether to displace the actor horizontally (by x-axis).")]
        [ParameterAlias("hor")]
        public BooleanParameter ShakeHorizontally;
        [Doc("Whether to displace the actor vertically (by y-axis).")]
        [ParameterAlias("ver")]
        public BooleanParameter ShakeVertically;

        protected override string Path => ResolvePath();
        protected override bool DestroyWhen => Assigned(ShakeCount) && ShakeCount == -1;

        private const string cameraId = "Camera";

        protected override StringListParameter GetSpawnParameters () => new List<string> {
            ToSpawnParam(Assigned(ActorId) ? ActorId : Engine.GetServiceOrErr<ITextPrinterManager>().DefaultPrinterId),
            ToSpawnParam(ShakeCount),
            ToSpawnParam(ShakeDuration),
            ToSpawnParam(DurationVariation),
            ToSpawnParam(ShakeAmplitude),
            ToSpawnParam(AmplitudeVariation),
            ToSpawnParam(ShakeHorizontally),
            ToSpawnParam(ShakeVertically)
        };

        protected virtual string ResolvePath ()
        {
            if (ActorId == cameraId) return "ShakeCamera";
            var manager = Engine.FindService<IActorManager, string>(ActorId,
                static (manager, actorId) => manager.ActorExists(actorId));
            if (manager is ICharacterManager) return $"ShakeCharacter#{ActorId}";
            if (manager is IBackgroundManager) return $"ShakeBackground#{ActorId}";
            return $"ShakePrinter#{ActorId}";
            // Can't throw here, as the actor may not be available (eg, pre-loading with dynamic policy).
        }
    }
}
