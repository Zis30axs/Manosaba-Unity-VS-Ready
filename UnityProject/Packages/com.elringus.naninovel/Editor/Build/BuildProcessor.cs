using UnityEditor;

namespace Naninovel
{
    /// <remarks>
    /// On build pre-process:
    ///   - Update projects stats, such as total command count required to report player read progress.
    ///   - When addressable provider is used: assign an addressable address and label to the assets referenced in <see cref="EditorResources"/>;
    ///   - Otherwise: copy the <see cref="EditorResources"/> assets to a temp 'Resources' folder (except the assets already stored in 'Resources' folders).
    /// On build post-process or build fail: 
    ///   - restore any affected assets and delete the created temporary 'Resources' folder.
    /// </remarks>
    public static class BuildProcessor
    {
        /// <summary>
        /// Whether the build processor is currently running.
        /// </summary>
        public static bool Building { get; private set; }

        public static void PreprocessBuild (BuildPlayerOptions options)
        {
            var config = Configuration.GetOrDefault<ResourceProviderConfiguration>();
            var addressableBuilder = CreateAddressableBuilder(config);
            var builder = new ResourcesBuilder(config, addressableBuilder);
            _ = ProjectStatsResolver.Resolve();
            builder.Build(options);
        }

        public static void PostprocessBuild () => ResourcesBuilder.Cleanup();

        public static void Initialize ()
        {
            var config = Configuration.GetOrDefault<ResourceProviderConfiguration>();
            if (config.EnableBuildProcessing)
                BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayer);
        }

        [MenuItem("Naninovel/Build Resources", priority = 4)]
        private static void BuildResourcesMenu ()
        {
            PreprocessBuild(new());
            PostprocessBuild();
        }

        private static void BuildPlayer (BuildPlayerOptions options)
        {
            try
            {
                Building = true;
                PreprocessBuild(options);
                BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
            }
            finally
            {
                PostprocessBuild();
                Building = false;
            }
        }

        private static IAddressableBuilder CreateAddressableBuilder (ResourceProviderConfiguration config)
        {
            #if ADDRESSABLES_AVAILABLE
            if (config.UseAddressables) return new AddressableBuilder(config);
            #endif
            Engine.Log("Consider installing Addressable Asset System and enabling 'Use Addressables' in Naninovel's 'Resource Provider' configuration menu. When the system is not available, all the assets assigned as Naninovel resources and not stored in 'Resources' folders will be copied and re-imported when building the player, which could significantly increase build time. Check https://naninovel.com/guide/resource-providers#addressable for more info.");
            return new MockAddressableBuilder();
        }
    }
}
