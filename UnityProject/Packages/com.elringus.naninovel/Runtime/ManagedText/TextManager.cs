using System.Collections.Generic;
using Naninovel.ManagedText;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ITextManager"/>
    [InitializeAtRuntime]
    public class TextManager : ITextManager
    {
        public virtual ManagedTextConfiguration Configuration { get; }
        public virtual IResourceLoader DocumentLoader => textLoader;

        private readonly IResourceProviderManager resources;
        private readonly ILocalizationManager l10n;
        private readonly ICommunityLocalization communityL10n;
        private readonly ScriptLocalizationParser l10nParser;
        private readonly ManagedTextFieldAssigner fieldAssigner;
        private readonly Dictionary<string, ManagedTextDocument> docByPath = new();
        private IResourceLoader<TextAsset> textLoader;

        public TextManager (ManagedTextConfiguration config, IResourceProviderManager resources,
            ILocalizationManager l10n, ICommunityLocalization communityL10n)
        {
            Configuration = config;
            l10nParser = new(new() { Separator = l10n.Configuration.RecordSeparator[0] });
            fieldAssigner = new(this);
            this.resources = resources;
            this.l10n = l10n;
            this.communityL10n = communityL10n;
        }

        public virtual UniTask InitializeService ()
        {
            textLoader = communityL10n.Active ? communityL10n.TextLoader :
                Configuration.Loader.CreateLocalizableFor<TextAsset>(resources, l10n);
            textLoader.OnLoaded += HandleTextLoaded;
            textLoader.OnUnloaded += HandleTextUnloaded;
            l10n.AddChangeLocaleTask(HandleLocaleChanged);
            return UniTask.CompletedTask;
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            textLoader.OnLoaded -= HandleTextLoaded;
            textLoader.OnUnloaded -= HandleTextUnloaded;
            textLoader.ReleaseAll(this);
            communityL10n.TextLoader.ReleaseAll(this);
            l10n?.RemoveChangeLocaleTask(HandleLocaleChanged);
        }

        public virtual ManagedTextDocument GetDocument (string documentPath)
        {
            return docByPath.GetValueOrDefault(documentPath);
        }

        protected virtual void HandleTextLoaded (Resource<TextAsset> resource)
        {
            var path = textLoader.GetLocalPath(resource.Path);
            docByPath[path] = ParseDocument(resource.Object.text, path);
        }

        protected virtual void HandleTextUnloaded (Resource<TextAsset> resource)
        {
            var path = textLoader.GetLocalPath(resource.Path);
            docByPath.Remove(path);
        }

        protected virtual async UniTask HandleLocaleChanged (LocaleChangedArgs _)
        {
            await fieldAssigner.Assign();
        }

        protected virtual ManagedTextDocument ParseDocument (string text, string documentPath)
        {
            if (IsScriptL10nDocument(documentPath)) return l10nParser.Parse(text);
            return ManagedTextUtils.Parse(text, documentPath, documentPath);
        }

        protected virtual bool IsScriptL10nDocument (string documentPath)
        {
            const string prefix = ManagedTextPaths.ScriptLocalizationPrefix + "/";
            return documentPath.StartsWithFast(prefix);
        }
    }
}
