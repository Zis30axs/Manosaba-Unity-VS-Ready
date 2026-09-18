using System;
using UnityEditor;

namespace Naninovel
{
    public class OrthoMetadataEditor<TActor, TMeta> : MetadataEditor<TActor, TMeta>
        where TActor : IActor
        where TMeta : OrthoActorMetadata
    {
        protected override Action<SerializedProperty> GetCustomDrawer (string propertyName) => propertyName switch {
            nameof(OrthoActorMetadata.Pivot) => DrawWhen(HasResources),
            nameof(OrthoActorMetadata.PixelsPerUnit) => DrawWhen(HasResources),
            nameof(OrthoActorMetadata.EnableDepthPass) => DrawWhen(HasResources),
            nameof(OrthoActorMetadata.DepthAlphaCutoff) => DrawWhen(HasResources && Metadata.EnableDepthPass),
            nameof(OrthoActorMetadata.CustomTextureMaterial) => DrawWhen(HasResources && !IsGeneric),
            nameof(OrthoActorMetadata.CustomSpriteMaterial) => DrawWhen(HasResources && !IsGeneric && !Metadata.RenderTexture),
            nameof(OrthoActorMetadata.RenderTexture) => DrawWhen(HasResources && !IsGeneric),
            nameof(OrthoActorMetadata.RenderRectangle) => DrawWhen(!IsGeneric && Metadata.RenderTexture),
            _ => base.GetCustomDrawer(propertyName)
        };
    }
}
