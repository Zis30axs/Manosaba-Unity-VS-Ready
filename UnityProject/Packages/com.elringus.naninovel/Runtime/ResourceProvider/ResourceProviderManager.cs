using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="IResourceProviderManager"/>
    [InitializeAtRuntime]
    public class ResourceProviderManager : IResourceProviderManager
    {
        public event Action<string> OnProviderMessage;

        public virtual ResourceProviderConfiguration Configuration { get; }

        private readonly Dictionary<string, IResourceProvider> providersMap = new();
        private readonly Dictionary<UnityEngine.Object, HashSet<object>> holdersMap = new();

        public ResourceProviderManager (ResourceProviderConfiguration config)
        {
            Configuration = config;
        }

        public virtual UniTask InitializeService ()
        {
            if (Configuration.MasterProvider != null)
                Configuration.MasterProvider.OnMessage += message => HandleProviderMessage(Configuration.MasterProvider, message);
            return UniTask.CompletedTask;
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            foreach (var provider in providersMap.Values)
                provider?.UnloadResourcesBlocking();
            Configuration.MasterProvider?.UnloadResourcesBlocking();
        }

        public virtual bool IsProviderInitialized (string providerType) => providersMap.ContainsKey(providerType);

        public virtual IResourceProvider GetProvider (string providerType)
        {
            if (!providersMap.ContainsKey(providerType))
                providersMap[providerType] = InitializeProvider(providerType);
            return providersMap[providerType];
        }

        public virtual void GetProviders (IList<IResourceProvider> providers, IReadOnlyList<string> types)
        {
            if (Configuration.MasterProvider != null)
                providers.Add(Configuration.MasterProvider);
            foreach (var type in types)
            {
                var provider = GetProvider(type);
                if (provider != null) providers.Add(provider);
            }
        }

        public virtual int Hold (UnityEngine.Object obj, object holder)
        {
            if (!obj) throw new Error($"Failed to hold '{obj}' object by '{holder}': specified object is invalid (null or destroyed).");
            var holders = GetHolders(obj);
            holders.Add(holder);
            return holders.Count;
        }

        public virtual int Release (UnityEngine.Object obj, object holder)
        {
            if (!obj) return 0;
            var holders = GetHolders(obj);
            holders.Remove(holder);
            return holders.Count;
        }

        public virtual int CountHolders (UnityEngine.Object obj)
        {
            if (!obj) return 0;
            return GetHolders(obj).Count;
        }

        protected virtual IResourceProvider InitializeProjectProvider ()
        {
            var projectProvider = new ProjectResourceProvider(Configuration.ProjectRootPath);
            return projectProvider;
        }

        protected virtual IResourceProvider InitializeGoogleDriveProvider ()
        {
            #if UNITY_GOOGLE_DRIVE_AVAILABLE
            var gDriveProvider = new GoogleDriveResourceProvider(Configuration.GoogleDriveRootPath, Configuration.GoogleDriveCachingPolicy, Configuration.GoogleDriveRequestLimit);
            gDriveProvider.AddConverter(new JpgOrPngToTextureConverter());
            gDriveProvider.AddConverter(new GDocToScriptAssetConverter());
            return gDriveProvider;
            #else
            return null;
            #endif
        }

        protected virtual IResourceProvider InitializeLocalProvider ()
        {
            var localProvider = new LocalResourceProvider(Configuration.LocalRootPath);
            localProvider.AddConverter(new JpgOrPngToTextureConverter());
            localProvider.AddConverter(new NaniToScriptAssetConverter());
            localProvider.AddConverter(new WavToAudioClipConverter());
            return localProvider;
        }

        protected virtual IResourceProvider InitializeAddressableProvider ()
        {
            #if ADDRESSABLES_AVAILABLE
            if (Application.isEditor && !Configuration.AllowAddressableInEditor) return null; // Otherwise could be issues with addressables added on previous build, but renamed after.
            var extraLabels = Configuration.ExtraLabels != null && Configuration.ExtraLabels.Length > 0 ? Configuration.ExtraLabels : null;
            return new AddressableResourceProvider(ResourceProviderConfiguration.AddressableId, extraLabels);
            #else
            return null;
            #endif
        }

        protected virtual IResourceProvider InitializeProvider (string providerType)
        {
            IResourceProvider provider;

            switch (providerType)
            {
                case ResourceProviderConfiguration.ProjectTypeName:
                    provider = InitializeProjectProvider();
                    break;
                case ResourceProviderConfiguration.AddressableTypeName:
                    provider = InitializeAddressableProvider();
                    break;
                case ResourceProviderConfiguration.LocalTypeName:
                    provider = InitializeLocalProvider();
                    break;
                case ResourceProviderConfiguration.GoogleDriveTypeName:
                    provider = InitializeGoogleDriveProvider();
                    break;
                default:
                    var customType = Type.GetType(providerType);
                    if (customType is null) throw new Error($"Failed to initialize '{providerType}' resource provider. Make sure provider types are set correctly in 'Loader' properties of the Naninovel configuration menus.");
                    provider = (IResourceProvider)Activator.CreateInstance(customType);
                    if (provider is null) throw new Error($"Failed to initialize '{providerType}' custom resource provider. Make sure the implementation has a parameterless constructor.");
                    return provider;
            }

            if (provider != null)
                provider.OnMessage += message => HandleProviderMessage(provider, message);

            return provider;
        }

        private void HandleProviderMessage (IResourceProvider provider, string message)
        {
            OnProviderMessage?.Invoke($"[{provider.GetType().Name}] {message}");
        }

        private HashSet<object> GetHolders (UnityEngine.Object obj)
        {
            if (holdersMap.TryGetValue(obj, out var holders)) return holders;
            holders = new();
            holdersMap[obj] = holders;
            return holders;
        }
    }
}
