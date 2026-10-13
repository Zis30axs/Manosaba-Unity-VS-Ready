using Naninovel;

namespace WitchTrials.Views
{
    [CommandAlias("finishFireworksEpilogue")]
    public sealed class FinishFireworksEpilogue : Command
    {
        public override UniTask Execute(AsyncToken token = default) => FireworksEpilogueSession.Finish(token);
    }
}
