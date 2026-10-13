using GigaCreation.NaninovelExtender.Common;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    /// <summary>Selects the ending route once; changing the preference never interrupts an active epilogue.</summary>
    [UsedImplicitly, CommandAlias("routeFireworksEpilogue")]
    public sealed class RouteFireworksEpilogue : Command
    {
        // Match ReturnToTitle's defaults; the ending recipe copies its explicit
        // 1.8 / 0.9 / true parameters intact, including their source metadata.
        [UsedImplicitly, ParameterAlias("time"), ParameterDefaultValue("1.2")]
        public DecimalParameter FadeDuration = 1.2f;
        [UsedImplicitly, ParameterAlias("delay"), ParameterDefaultValue("0.6")]
        public DecimalParameter MinDelayDuration = 0.6f;
        [UsedImplicitly]
        public BooleanParameter Wait;

        public override UniTask Execute(AsyncToken token = default)
        {
            token.ThrowIfCanceled();
            if (!Engine.GetServiceOrErr<EnhancementSettings>().FireworksEpilogueEnabled)
                return new ReturnToTitle {
                    FadeDuration = FadeDuration, MinDelayDuration = MinDelayDuration, Wait = Wait
                }.Execute(token);
            return new GotoModified {
                Path = new NamedString(FireworksEpilogueSession.ScriptPath, null)
            }.Execute(token);
        }
    }
}
