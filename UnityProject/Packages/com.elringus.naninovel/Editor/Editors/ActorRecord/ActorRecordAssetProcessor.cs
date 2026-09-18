using System;
using System.IO;
using UnityEditor;

namespace Naninovel
{
    public class ActorRecordAssetProcessor : AssetModificationProcessor
    {
        private static EditorResources editorResources;

        private static AssetDeleteResult OnWillDeleteAsset (string assetPath, RemoveAssetOptions options)
        {
            if (TryRemoveInFolder(assetPath)) return AssetDeleteResult.DidNotDelete;
            var assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            if (!typeof(ActorRecord).IsAssignableFrom(assetType))
                return AssetDeleteResult.DidNotDelete;

            var record = AssetDatabase.LoadAssetAtPath<ActorRecord>(assetPath);
            var metadata = record.GetMetadata();
            RemoveActorRecord(record.name, metadata);
            RemoveAssociatedResources(metadata);
            AssetDatabase.SaveAssets();
            return AssetDeleteResult.DidNotDelete;
        }

        private static AssetMoveResult OnWillMoveAsset (string sourcePath, string destinationPath)
        {
            var assetType = AssetDatabase.GetMainAssetTypeAtPath(sourcePath);
            if (!typeof(ActorRecord).IsAssignableFrom(assetType))
                return AssetMoveResult.DidNotMove;

            var sourceId = Path.GetFileNameWithoutExtension(sourcePath);
            var destinationId = Path.GetFileNameWithoutExtension(destinationPath);
            if (sourceId == destinationId) return AssetMoveResult.DidNotMove;
            var record = AssetDatabase.LoadAssetAtPath<ActorRecord>(sourcePath);
            var metadata = record.GetMetadata();
            MoveActorRecord(sourceId, destinationId, metadata);
            AssetDatabase.SaveAssets();
            return AssetMoveResult.DidNotMove;
        }

        private static bool TryRemoveInFolder (string assetPath)
        {
            if (!AssetDatabase.IsValidFolder(assetPath)) return false;
            foreach (var guid in AssetDatabase.FindAssets($"t:{nameof(ActorRecord)}", new[] { assetPath }))
                OnWillDeleteAsset(AssetDatabase.GUIDToAssetPath(guid), default);
            return true;
        }

        private static void RemoveActorRecord (string actorId, ActorMetadata meta)
        {
            var config = LoadConfiguration(meta);
            config.MetadataMap.RemoveRecord(actorId);
            EditorUtility.SetDirty(config);
        }

        private static void MoveActorRecord (string sourceId, string destinationId, ActorMetadata meta)
        {
            var config = LoadConfiguration(meta);
            config.MetadataMap.MoveRecord(sourceId, destinationId);
            EditorUtility.SetDirty(config);
        }

        private static ActorManagerConfiguration LoadConfiguration (ActorMetadata meta)
        {
            var config = GetActorConfig(meta);
            if (!config) throw new InvalidOperationException($"Failed to load '{meta.GetType().Name}' configuration.");
            return config;
        }

        private static ActorManagerConfiguration GetActorConfig (ActorMetadata meta)
        {
            if (meta is CharacterMetadata) return Configuration.GetOrDefault<CharactersConfiguration>();
            if (meta is BackgroundMetadata) return Configuration.GetOrDefault<BackgroundsConfiguration>();
            if (meta is TextPrinterMetadata) return Configuration.GetOrDefault<TextPrintersConfiguration>();
            if (meta is ChoiceHandlerMetadata) return Configuration.GetOrDefault<ChoiceHandlersConfiguration>();
            throw new Error($"Unknown metadata type: '{meta.GetType().FullName}'.");
        }

        private static void RemoveAssociatedResources (ActorMetadata metadata)
        {
            editorResources ??= EditorResources.LoadOrDefault();
            var categoryId = metadata.GetResourceCategoryId();
            editorResources.RemoveCategory(categoryId);
            EditorUtility.SetDirty(editorResources);
        }
    }
}
