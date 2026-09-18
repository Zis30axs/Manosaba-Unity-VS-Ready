namespace Naninovel
{
    /// <inheritdoc cref="IScriptManager"/>
    [InitializeAtRuntime]
    public class ScriptManager : IScriptManager
    {
        public virtual ScriptsConfiguration Configuration { get; }
        public IResourceLoader<Script> ScriptLoader => scriptLoader;
        public IResourceLoader<Script> ExternalScriptLoader => extScriptLoader;
        public virtual int TotalCommandsCount { get; private set; }

        private readonly IResourceProviderManager providers;
        private ResourceLoader<Script> scriptLoader;
        private ResourceLoader<Script> extScriptLoader;

        public ScriptManager (ScriptsConfiguration config, IResourceProviderManager providers)
        {
            Configuration = config;
            this.providers = providers;
        }

        public virtual UniTask InitializeService ()
        {
            scriptLoader = Configuration.Loader.CreateFor<Script>(providers);
            extScriptLoader = Configuration.ExternalLoader.CreateFor<Script>(providers);
            TotalCommandsCount = ProjectStats.GetOrDefault().TotalCommandCount;
            return UniTask.CompletedTask;
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            scriptLoader?.ReleaseAll(this);
            extScriptLoader?.ReleaseAll(this);
        }
    }
}
