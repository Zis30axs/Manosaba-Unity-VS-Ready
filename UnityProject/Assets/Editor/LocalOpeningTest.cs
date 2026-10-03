using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Naninovel;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    // Editor-only, in-memory replacement. No test resources are registered with Addressables.
    [InitializeOnLoad]
    public static class LocalOpeningTest
    {
        private const string OpeningPath = "Act01_Chapter01/Act01_Chapter01_Adv01";
        private const string MediaRoot = "Assets/Editor/LocalOpeningTest/Media/";
        private static readonly string SourcePath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../funny/opening-bmpt72-test.nani"));
        private static LocalOpeningResourceProvider localProvider;
        private static string source;

        static LocalOpeningTest()
        {
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.EnteredEditMode) Cleanup();
            };
            AssemblyReloadEvents.beforeAssemblyReload += Cleanup;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Register()
        {
            Engine.RemovePreInitializationTask(PrepareProvider);
            Engine.AddPreInitializationTask(PrepareProvider);
            Engine.RemovePostInitializationTask(Install);
            Engine.AddPostInitializationTask(Install);
        }

        private static UniTask PrepareProvider()
        {
            source = File.Exists(SourcePath) ? File.ReadAllText(SourcePath) : string.Empty;
            if (!source.Split('\n').Any(line => line.TrimStart().StartsWith("@", StringComparison.Ordinal)))
            {
                Debug.Log("[LocalOpeningTest] Disabled: original opening will play.");
                return UniTask.CompletedTask;
            }
            var errors = new List<ScriptParseError>();
            Script replacement = Compiler.ScriptAssetParser.ParseText(OpeningPath, source, new ParseOptions(errors, true));
            if (errors.Count > 0)
            {
                UnityEngine.Object.DestroyImmediate(replacement);
                throw new InvalidOperationException(string.Join("\n", errors.Select(error => error.ToString(SourcePath))));
            }
            replacement.hideFlags = HideFlags.HideAndDontSave;
            string prefix = Configuration.GetOrDefault<ScriptsConfiguration>().Loader.PathPrefix;
            string fullPath = string.IsNullOrEmpty(prefix) ? OpeningPath : prefix + "/" + OpeningPath;
            localProvider = new LocalOpeningResourceProvider(fullPath, replacement);
            // Leave CGGalleryUI/SettingsUI to the Addressables UI provider. Their package
            // EditorResources records would otherwise win over the custom GalleryUI/OptionsUI
            // addresses and loading an alias here would instantiate the same prefab twice.
            foreach (var record in EditorResources.LoadOrDefault().GetAllRecords())
                if (!ShouldSkipEditorUiRecord(record.Key) && !string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(record.Value)))
                    localProvider.AddResourceGuid(record.Key, record.Value);
            RegisterGeneratedScenarioResources(localProvider);
            RegisterGeneratedLocalizationResources(localProvider);
            ResourceProviderConfiguration.EditorProvider = localProvider;
            RegisterAsset(localProvider, "Characters/LocalBMPT72/Default", "BMPT72.png");
            RegisterAsset(localProvider, "Audio/LocalOpening/Reverse", "Reverse.wav");
            RegisterAsset(localProvider, "Audio/LocalOpening/Autocannon", "Autocannon.ogg");
            RegisterAsset(localProvider, "Backgrounds/LocalOpening/Default", "SchoolCourtyard.png");
            Debug.Log("[LocalOpeningTest] Independent in-memory opening ready; commands=" + replacement.Playlist.Count + ". Original script asset is unchanged.");
            return UniTask.CompletedTask;
        }

        private static bool ShouldSkipEditorUiRecord(string resourcePath)
        {
            if (resourcePath == "UI/CGGalleryUI")
                return HasAsset("Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/GalleryUI.prefab");
            if (resourcePath == "UI/SettingsUI")
                return HasAsset("Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/OptionsUI.prefab");
            return false;
        }

        private static bool HasAsset(string assetPath)
        {
            return !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(assetPath))
                && AssetDatabase.LoadMainAssetAtPath(assetPath) != null;
        }

        private static void RegisterGeneratedScenarioResources(LocalOpeningResourceProvider provider)
        {
            string root = Path.Combine(Application.dataPath, "GameContent", "#WitchTrials", "Scenarios");
            if (!Directory.Exists(root)) return;

            int registered = 0;
            foreach (string file in Directory.EnumerateFiles(root, "*.asset", SearchOption.AllDirectories))
            {
                string assetPath = ToAssetPath(file);
                string guid = AssetDatabase.AssetPathToGUID(assetPath);
                if (string.IsNullOrEmpty(guid)) continue;
                string relative = file.Substring(root.Length + 1).Replace('\\', '/');
                relative = Path.ChangeExtension(relative, null);
                provider.AddResourceGuid("Scripts/" + relative, guid);
                registered++;
            }

            Debug.Log("[LocalOpeningTest] Registered generated scenario resources: " + registered);
        }

        private static void RegisterGeneratedLocalizationResources(LocalOpeningResourceProvider provider)
        {
            string root = Path.Combine(Application.dataPath, "GameContent", "#WitchTrials", "Localization");
            if (!Directory.Exists(root)) return;

            int registered = 0;
            foreach (string file in Directory.EnumerateFiles(root, "*.txt", SearchOption.AllDirectories))
            {
                string assetPath = ToAssetPath(file);
                string guid = AssetDatabase.AssetPathToGUID(assetPath);
                if (string.IsNullOrEmpty(guid)) continue;
                string relative = file.Substring(root.Length + 1).Replace('\\', '/');
                relative = Path.ChangeExtension(relative, null);
                provider.AddResourceGuid("Localization/" + relative, guid);
                registered++;
            }

            Debug.Log("[LocalOpeningTest] Registered generated localization resources: " + registered);
        }

        private static string ToAssetPath(string absolutePath)
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string relative = absolutePath.Substring(projectRoot.Length + 1).Replace('\\', '/');
            return relative.StartsWith("Assets/", StringComparison.Ordinal) ? relative : "Assets/" + relative;
        }

        private static UniTask Install()
        {
            if (localProvider == null) return UniTask.CompletedTask;
            var config = Engine.GetConfiguration<CharactersConfiguration>();
            config.Metadata.AddRecord("LocalBMPT72", new CharacterMetadata
            {
                DisplayName = MulticolorAuthorNames.BuildName("tricolor-b", "MPT-72"),
                PixelsPerUnit = 170,
                Pivot = new Vector2(.5f, 0),
                EnableDepthPass = false,
                UseCharacterColor = true,
                NameColor = Color.white,
                MessageColor = Color.white
            });
            config.Metadata.AddRecord("LocalBMPTUnknown", new CharacterMetadata
            {
                Implementation = typeof(NarratorCharacter).AssemblyQualifiedName,
                DisplayName = "？？？"
            });
            Engine.GetConfiguration<BackgroundsConfiguration>().Metadata.AddRecord("LocalOpening", new BackgroundMetadata
            {
                PixelsPerUnit = 100,
                Pivot = new Vector2(.5f, .5f),
                EnableDepthPass = false,
                MatchMode = AspectMatchMode.Crop
            });
            Debug.Log("[LocalOpeningTest] Enabled for a new opening. Source: " + SourcePath);
            return UniTask.CompletedTask;
        }

        private static void RegisterAsset(EditorResourceProvider provider, string path, string file)
        {
            string assetPath = MediaRoot + file;
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (string.IsNullOrEmpty(guid) || AssetDatabase.LoadMainAssetAtPath(assetPath) == null)
                throw new FileNotFoundException("Import the local test media before Play: " + assetPath);
            provider.AddResourceGuid(path, guid);
        }

        private static void Cleanup()
        {
            localProvider?.DisposeOpening();
            localProvider = null;
            source = null;
        }
    }
}
