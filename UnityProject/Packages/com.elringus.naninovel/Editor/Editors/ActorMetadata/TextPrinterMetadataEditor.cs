using System;
using UnityEditor;

namespace Naninovel
{
    public class TextPrinterMetadataEditor : OrthoMetadataEditor<ITextPrinterActor, TextPrinterMetadata>
    {
        protected override Action<SerializedProperty> GetCustomDrawer (string propertyName) => propertyName switch {
            nameof(TextPrinterMetadata.EnableDepthPass) => DrawNothing(),
            nameof(TextPrinterMetadata.DepthAlphaCutoff) => DrawNothing(),
            nameof(TextPrinterMetadata.CustomTextureMaterial) => DrawNothing(),
            nameof(TextPrinterMetadata.CustomSpriteMaterial) => DrawNothing(),
            nameof(TextPrinterMetadata.RenderTexture) => DrawNothing(),
            nameof(TextPrinterMetadata.RenderRectangle) => DrawNothing(),
            _ => base.GetCustomDrawer(propertyName)
        };
    }
}
