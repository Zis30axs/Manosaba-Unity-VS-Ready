using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Allows ejecting localization resources for community localization feature.
    /// </summary>
    public class LocalizationEjector
    {
        private const string ejectArg = "-nani-eject";

        private readonly IResourceProviderManager providers;
        private readonly ILocalizationManager l10n;

        public LocalizationEjector (IResourceProviderManager providers, ILocalizationManager l10n)
        {
            this.providers = providers;
            this.l10n = l10n;
        }

        public static bool IsEjectionRequested ()
        {
            return Environment.GetCommandLineArgs().Any(a => a.StartsWithFast(ejectArg));
        }

        public async UniTask EjectScripts (string outDir)
        {
            if (ResolveEjectionLocale() == l10n.Configuration.SourceLocale)
                foreach (var script in await Engine.GetServiceOrErr<IScriptManager>().ScriptLoader.LoadAll())
                    EjectScript(script, outDir);
        }

        public async UniTask EjectText (string outDir)
        {
            var textLoader = Engine.GetConfiguration<ManagedTextConfiguration>().Loader
                .CreateLocalizableFor<TextAsset>(providers, l10n);
            textLoader.OverrideLocale = ResolveEjectionLocale();
            foreach (var resource in await textLoader.LoadAll())
                EjectText(resource.Object.text, textLoader.GetLocalPath(resource), outDir);
        }

        private void EjectScript (Script script, string outDir)
        {
            var outPath = Path.Combine(outDir, $"{script.Path}.txt");
            var existingDoc = File.Exists(outPath) ? ManagedTextUtils.ParseMultiline(File.ReadAllText(outPath)) : null;
            var localizer = new ScriptLocalizer(new() {
                Syntax = Compiler.Syntax,
                AnnotationPrefix = l10n.Configuration.AnnotationPrefix,
                Separator = l10n.Configuration.RecordSeparator[0]
            });
            var document = localizer.Localize(script, existingDoc);
            var output = ManagedTextUtils.SerializeMultiline(document);
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllText(outPath, output);
        }

        private void EjectText (string sourceText, string documentPath, string outDir)
        {
            var outPath = Path.Combine(outDir, $"{documentPath}.txt");
            var sourceDoc = ManagedTextUtils.Parse(sourceText, documentPath, outPath);
            var existingDoc = File.Exists(outPath) ? ManagedTextUtils.Parse(File.ReadAllText(outPath), documentPath) : null;
            var localizer = new ManagedTextLocalizer(new() {
                AnnotationPrefix = l10n.Configuration.AnnotationPrefix
            });
            var document = localizer.Localize(sourceDoc, existingDoc);
            var output = ManagedTextUtils.Serialize(document, documentPath);
            File.WriteAllText(outPath, output);
        }

        private string ResolveEjectionLocale ()
        {
            var locale = Environment.GetCommandLineArgs()
                .First(a => a.StartsWithFast(ejectArg)).GetAfter(ejectArg + "-")?.TrimFull();
            return locale is null || !l10n.LocaleAvailable(locale)
                ? l10n.Configuration.SourceLocale : locale;
        }
    }
}
