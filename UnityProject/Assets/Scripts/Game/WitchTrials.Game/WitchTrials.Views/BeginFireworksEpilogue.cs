using Naninovel;

namespace WitchTrials.Views
{
    [CommandAlias("beginFireworksEpilogue")]
    public sealed class BeginFireworksEpilogue : Command, Command.IPreloadable
    {
        public async UniTask PreloadResources()
        {
            var loader = Engine.GetServiceOrErr<IAudioManager>().AudioLoader;
            await UniTask.WhenAll(loader.Load(FireworksEpilogueSession.BgmLoop, this),
                loader.Load(FireworksEpilogueSession.BgmIntro, this));
        }

        public void ReleaseResources()
        {
            var loader = Engine.GetService<IAudioManager>()?.AudioLoader;
            loader?.Release(FireworksEpilogueSession.BgmLoop, this);
            loader?.Release(FireworksEpilogueSession.BgmIntro, this);
        }

        public override UniTask Execute(AsyncToken token = default) => FireworksEpilogueSession.Begin(token);
    }
}
