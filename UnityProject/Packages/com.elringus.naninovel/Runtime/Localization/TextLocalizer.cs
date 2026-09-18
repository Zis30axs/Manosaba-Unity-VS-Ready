using System.Diagnostics.CodeAnalysis;
using System.Text;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ITextLocalizer"/>
    [InitializeAtRuntime]
    public class TextLocalizer : ITextLocalizer
    {
        private readonly StringBuilder builder = new();
        private readonly ITextManager docs;
        private readonly IScriptManager scripts;
        private readonly ILocalizationManager l10n;
        private readonly ICommunityLocalization communityL10n;

        public TextLocalizer (ITextManager docs, IScriptManager scripts,
            ILocalizationManager l10n, ICommunityLocalization communityL10n)
        {
            this.l10n = l10n;
            this.docs = docs;
            this.scripts = scripts;
            this.communityL10n = communityL10n;
        }

        public virtual UniTask InitializeService ()
        {
            l10n.AddChangeLocaleTask(HandleLocaleChanged);
            scripts.ScriptLoader.OnUnloaded += HandleScriptUnloaded;
            return UniTask.CompletedTask;
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            scripts.ScriptLoader.OnUnloaded -= HandleScriptUnloaded;
            l10n.RemoveChangeLocaleTask(HandleLocaleChanged);
            scripts.ScriptLoader.ReleaseAll(this);
            docs.DocumentLoader.ReleaseAll(this);
        }

        public virtual async UniTask Load (LocalizableText text, [MaybeNull] object holder = null)
        {
            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var part in text.Parts)
            {
                if (part.PlainText) continue;
                var textHolder = holder != null ? new LocalizableTextHolder(part, holder) : (LocalizableTextHolder?)null;
                tasks.Add(scripts.ScriptLoader.LoadOrErr(part.Spot.ScriptPath, textHolder));
                if (UsingL10nDocuments()) tasks.Add(LoadL10nDocumentForScript(part.Spot.ScriptPath));
            }
            await UniTask.WhenAll(tasks);
        }

        public virtual void Hold (LocalizableText text, object holder)
        {
            Debug.Assert(holder != null);

            foreach (var part in text.Parts)
            {
                if (part.PlainText) continue;
                var textHolder = new LocalizableTextHolder(part, holder);
                scripts.ScriptLoader.Hold(part.Spot.ScriptPath, textHolder);
            }
        }

        public virtual void Release (LocalizableText text, object holder)
        {
            Debug.Assert(holder != null);

            foreach (var part in text.Parts)
            {
                if (part.PlainText) continue;
                var textHolder = new LocalizableTextHolder(part, holder);
                scripts.ScriptLoader.Release(part.Spot.ScriptPath, textHolder);
            }
        }

        public virtual string Resolve (LocalizableText text)
        {
            if (text.Parts.Count == 1 && !text.Parts[0].PlainText)
                return Resolve(text.Parts[0].Id, text.Parts[0].Spot.ScriptPath);
            builder.Clear();
            foreach (var part in text.Parts)
                builder.Append(part.PlainText ? part.Text : Resolve(part.Id, part.Spot.ScriptPath));
            return builder.ToString();
        }

        protected virtual async UniTask LoadL10nDocumentForScript (string scriptPath)
        {
            var docPath = ToL10nPath(scriptPath);
            var loaded = await docs.DocumentLoader.Load(docPath, this);
            if (!loaded.Valid)
                Engine.Err($"Failed to load '{l10n.SelectedLocale}' localization document for '{scriptPath}' scenario script. " +
                           $"Make sure localization resources are generated.");
        }

        protected virtual string Resolve (string id, string scriptPath)
        {
            if (UsingL10nDocuments()) return ResolveFromDocument(id, scriptPath);
            return ResolveFromScript(id, scriptPath);
        }

        protected virtual string ResolveFromScript (string id, string scriptPath)
        {
            if (!scripts.ScriptLoader.IsLoaded(scriptPath))
                throw new Error($"Failed to resolve localized text for '{scriptPath}/{id}': script resource is not loaded. " +
                                $"Make sure to hold the localizable text before resolving.");
            var value = scripts.ScriptLoader.GetLoaded(scriptPath)?.Object?.TextMap?.GetTextOrNull(id);
            if (!string.IsNullOrEmpty(value)) return value;
            Engine.Warn($"Failed to resolve localized text for '{scriptPath}/{id}': script or text mapping is not available.");
            return $"{Compiler.Syntax.TextIdOpen}{scriptPath}/{id}{Compiler.Syntax.TextIdClose}";
        }

        protected virtual string ResolveFromDocument (string id, string scriptPath)
        {
            var docPath = ToL10nPath(scriptPath);
            if (!docs.DocumentLoader.IsLoaded(docPath))
                throw new Error($"Failed to resolve localized text for '{scriptPath}/{id}': managed text document '{docPath}' is not loaded. " +
                                $"Make sure to hold the localizable text before resolving.");
            if (id.StartsWithFast(ScriptTextIdentifier.RefPrefix)) id = id[1..];
            var value = docs.GetRecordValue(id, docPath);
            if (!string.IsNullOrEmpty(value)) return value;
            Engine.Warn($"Missing translation for '{l10n.SelectedLocale}/Text/Scripts/{scriptPath}#{id}'. Will use source locale instead.");
            return ResolveFromScript(id, scriptPath);
        }

        protected virtual bool UsingL10nDocuments ()
        {
            return communityL10n.Active || !l10n.IsSourceLocaleSelected();
        }

        protected virtual async UniTask HandleLocaleChanged (LocaleChangedArgs args)
        {
            // Community l10n is activated on launch and can't change at runtime.
            if (communityL10n.Active) return;

            var isSource = l10n.IsSourceLocale(args.CurrentLocale);
            var wasSource = l10n.IsSourceLocale(args.PreviousLocale);
            // Changed between none-source locales: l10n docs loader handled the change.
            if (isSource == wasSource) return;

            // Switched from source to a none-source locale: load required l10n docs.
            if (wasSource)
            {
                using var _ = ListPool<UniTask>.Rent(out var tasks);
                foreach (var res in scripts.ScriptLoader.GetAllLoaded())
                    tasks.Add(LoadL10nDocumentForScript(scripts.ScriptLoader.GetLocalPath(res.Path)));
                await UniTask.WhenAll(tasks);
            }
            // Switched from a non-source to source locale: release all l10n docs.
            else docs.DocumentLoader.ReleaseAll(this);
        }

        protected virtual void HandleScriptUnloaded (Resource<Script> res)
        {
            if (UsingL10nDocuments())
                docs.DocumentLoader.Release(ToL10nPath(scripts.ScriptLoader.GetLocalPath(res.Path)), this);
        }

        private static string ToL10nPath (string scriptPath)
        {
            return ManagedTextUtils.ResolveScriptL10nPath(scriptPath);
        }
    }
}
