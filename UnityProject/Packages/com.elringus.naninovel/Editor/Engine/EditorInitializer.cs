using System.Collections.Generic;

namespace Naninovel
{
    public static class EditorInitializer
    {
        public static async UniTask Initialize ()
        {
            if (Engine.Initialized) return;

            var configProvider = new ProjectConfigurationProvider();
            var services = new List<IEngineService>();

            var resources = new ResourceProviderManager(configProvider.GetConfiguration<ResourceProviderConfiguration>());
            services.Add(resources);

            var l10n = new LocalizationManager(configProvider.GetConfiguration<LocalizationConfiguration>(), resources);
            services.Add(l10n);

            var communityL10n = new CommunityLocalization(resources);
            services.Add(communityL10n);

            var scripts = new ScriptManager(configProvider.GetConfiguration<ScriptsConfiguration>(), resources);
            services.Add(scripts);

            var text = new TextManager(configProvider.GetConfiguration<ManagedTextConfiguration>(), resources, l10n, communityL10n);
            services.Add(text);
            
            var vars = new CustomVariableManager(configProvider.GetConfiguration<CustomVariablesConfiguration>(), text);
            services.Add(vars);

            var localizer = new TextLocalizer(text, scripts, l10n, communityL10n);
            services.Add(localizer);

            await Engine.Initialize(new() {
                Services = services,
                ConfigurationProvider = configProvider,
                Behaviour = new EditorBehaviour(),
                Time = new UnityTime()
            });
        }
    }
}
