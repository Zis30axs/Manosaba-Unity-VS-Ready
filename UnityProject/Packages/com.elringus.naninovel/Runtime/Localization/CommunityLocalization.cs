using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ICommunityLocalization"/>
    [InitializeAtRuntime]
    public class CommunityLocalization : ICommunityLocalization
    {
        public virtual bool Active { get; private set; }
        public virtual string Author { get; private set; } = "";
        public IResourceLoader<TextAsset> TextLoader => textLoader;

        protected virtual string Root => Path.Combine(Application.persistentDataPath, "Localization");
        protected virtual string InfoFilePath => Path.Combine(Root, "Info.txt");
        protected virtual string TextFolder => Path.Combine(Root, "Text");
        protected virtual string ScriptFolder => Path.Combine(TextFolder, ManagedTextPaths.ScriptLocalizationPrefix);
        protected virtual string FontFileName { get; private set; } = "";

        private readonly IResourceProviderManager resources;
        private ResourceLoader<TextAsset> textLoader;

        public CommunityLocalization (IResourceProviderManager resources)
        {
            this.resources = resources;
        }

        public virtual async UniTask InitializeService ()
        {
            textLoader = CreateTextLoader();
            if (!IsPlatformSupported()) return;

            try
            {
                Active = File.Exists(InfoFilePath);
                if (Active)
                {
                    (Author, FontFileName) = await ResolveInfo();
                    Engine.Log($"Community localization by '{Author}' is active.");
                }
            }
            catch { Active = false; }

            if (IsEjectionRequested())
                Engine.AddPostInitializationTask(Eject);
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            Engine.RemovePostInitializationTask(Eject);
        }

        protected virtual ResourceLoader<TextAsset> CreateTextLoader ()
        {
            var provider = new LocalResourceProvider(TextFolder);
            provider.AddConverter(new TxtToTextAssetConverter());
            return new(new IResourceProvider[] { provider }, resources);
        }

        public virtual UniTask<TMP_FontAsset> LoadFont ()
        {
            var path = Path.Combine(Root, FontFileName);
            var face = new Font(path);
            var font = TMP_FontAsset.CreateFontAsset(face);
            font.name = Path.GetFileNameWithoutExtension(FontFileName);
            return UniTask.FromResult(font);
        }

        protected virtual bool IsPlatformSupported ()
        {
            return Application.isEditor ||
                   Application.platform == RuntimePlatform.WindowsPlayer ||
                   Application.platform == RuntimePlatform.OSXPlayer ||
                   Application.platform == RuntimePlatform.LinuxPlayer ||
                   Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }

        protected virtual bool IsEjectionRequested ()
        {
            return LocalizationEjector.IsEjectionRequested();
        }

        protected virtual async UniTask Eject ()
        {
            Directory.CreateDirectory(ScriptFolder);
            Directory.CreateDirectory(TextFolder);
            if (!File.Exists(InfoFilePath))
                File.WriteAllText(InfoFilePath, "Author\nFont");
            var ejector = new LocalizationEjector(
                Engine.GetServiceOrErr<IResourceProviderManager>(),
                Engine.GetServiceOrErr<ILocalizationManager>());
            await ejector.EjectScripts(ScriptFolder);
            await ejector.EjectText(TextFolder);
            Engine.Log($"Ejected community localization resources to '{Root}'.");
        }

        protected virtual async UniTask<(string Author, string Font)> ResolveInfo ()
        {
            var content = (await IOUtils.ReadTextFile(InfoFilePath)).TrimFull();
            var lines = content.SplitByNewLine(StringSplitOptions.RemoveEmptyEntries);
            var author = lines.ElementAtOrDefault(0)?.TrimFull() ?? "Unknown";
            var font = lines.ElementAtOrDefault(1)?.TrimFull() ?? "Unknown";
            return (author, font);
        }
    }
}
