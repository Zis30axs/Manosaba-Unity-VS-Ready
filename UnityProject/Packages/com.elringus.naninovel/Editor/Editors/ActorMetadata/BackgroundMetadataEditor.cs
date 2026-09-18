using System;
using UnityEditor;

namespace Naninovel
{
    public class BackgroundMetadataEditor : OrthoMetadataEditor<IBackgroundActor, BackgroundMetadata>
    {
        protected override Action<SerializedProperty> GetCustomDrawer (string propertyName) => propertyName switch {
            nameof(BackgroundMetadata.MatchMode) => DrawWhen(!IsGeneric),
            nameof(BackgroundMetadata.CustomMatchRatio) => DrawWhen(!IsGeneric && Metadata.MatchMode == AspectMatchMode.Custom),
            nameof(BackgroundMetadata.Poses) => DrawWhen(HasResources, ActorPosesEditor.Draw),
            nameof(BackgroundMetadata.ScenePathRoot) => DrawWhen(Metadata.Implementation == typeof(SceneBackground).AssemblyQualifiedName, p => EditorUtils.FolderField(p)),
            _ => base.GetCustomDrawer(propertyName)
        };
    }
}
