using Naninovel;

namespace WitchTrials.Views
{
    [CommandAlias("fireworksMood")]
    public sealed class SetFireworksEpilogueMood : Command
    {
        [ParameterAlias(""), RequiredParameter]
        public StringParameter Mood;

        public BooleanParameter Wait = true;

        public override UniTask Execute(AsyncToken token = default) =>
            WaitOrForget(t => FireworksEpilogueSession.SetMood(Mood.Value, t), Wait, token);
    }
}
