using Naninovel;

namespace WitchTrials.Views
{
    [CommandAlias("fireworksCredits")]
    public sealed class ShowFireworksEpilogueCredits : Command
    {
        public override UniTask Execute(AsyncToken token = default) => FireworksEpilogueSession.ShowCredits(token);
    }
}
