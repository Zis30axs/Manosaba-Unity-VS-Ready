using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naninovel
{
    public class CGGalleryGridSlot : ScriptableGridSlot
    {
        public override string Id => Data.Id;
        public virtual float LastSelectTime { get; private set; }

        protected virtual CGSlotData Data { get; private set; }
        protected virtual RawImage ThumbnailImage => thumbnailImage;
        protected virtual Texture2D LockedTexture => lockedTexture;
        protected virtual Texture2D LoadingTexture => loadingTexture;
        protected virtual IReadOnlyList<Texture2D> CGTextures { get; private set; }
        protected virtual bool AnyUnlocked => CGTextures?.Any(t => t) ?? false;

        [SerializeField] private RawImage thumbnailImage;
        [SerializeField] private Texture2D lockedTexture;
        [SerializeField] private Texture2D loadingTexture;

        private ILocalizationManager l10n;
        private IUnlockableManager unlockables;
        private Action<IEnumerable<Texture2D>> showTextures;

        public virtual void Initialize (Action<IEnumerable<Texture2D>> showTextures)
        {
            this.showTextures = showTextures;
        }

        public virtual void Bind (CGSlotData data)
        {
            UnloadCGTextures();
            Data = data;
            Refresh();
        }

        public override void OnSelect (BaseEventData eventData)
        {
            base.OnSelect(eventData);
            LastSelectTime = Engine.Time.UnscaledTime;
        }

        protected virtual async UniTask LoadCGTextures ()
        {
            var prevThumbnailImage = ThumbnailImage.texture;
            ThumbnailImage.texture = LoadingTexture;
            var textures = new Texture2D[Data.TexturePaths.Count];
            await UniTask.WhenAll(Data.TexturePaths.Select(LoadCGTexture));
            CGTextures = textures;
            ThumbnailImage.texture = prevThumbnailImage;

            async UniTask LoadCGTexture (string path)
            {
                var unlockableId = PathToUnlockableId(path);
                if (!unlockables.ItemUnlocked(unlockableId)) return;
                var index = Data.TexturePaths.IndexOf(path);
                textures[index] = Data.TextureLoader.IsLoaded(path)
                    ? Data.TextureLoader.GetLoaded(path)
                    : await Data.TextureLoader.LoadOrErr(path, this);
            }
        }

        public virtual void UnloadCGTextures ()
        {
            if (Data.TexturePaths is null) return;
            foreach (var texturePath in Data.TexturePaths)
                Data.TextureLoader?.Release(texturePath, this);
        }

        protected virtual void Refresh () => HandleItemUpdated(default);

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(ThumbnailImage, LockedTexture);

            unlockables = Engine.GetServiceOrErr<IUnlockableManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            ThumbnailImage.texture = LoadingTexture;

            unlockables.OnItemUpdated += HandleItemUpdated;
            l10n.OnLocaleChanged += HandleLocaleChanged;
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            if (unlockables != null)
                unlockables.OnItemUpdated -= HandleItemUpdated;
            if (l10n != null)
                l10n.OnLocaleChanged -= HandleLocaleChanged;
        }

        protected virtual async void HandleItemUpdated (UnlockableItemUpdatedArgs _)
        {
            while (Id is null) // We get here after first OnEnable, but ID is not set yet.
            {
                await AsyncUtils.DelayFrame(1);
                if (!this) return;
            }

            await LoadCGTextures();

            if (!AnyUnlocked) ThumbnailImage.texture = LockedTexture;
            else ThumbnailImage.texture = CGTextures.FirstOrDefault(t => t);
        }

        protected virtual void HandleLocaleChanged (LocaleChangedArgs _) => Refresh();

        protected override void OnButtonClick ()
        {
            base.OnButtonClick();

            if (AnyUnlocked)
                showTextures(CGTextures);
        }

        private static string PathToUnlockableId (string path) => $"{CGGalleryPanel.CGPrefix}/{path}";
    }
}
