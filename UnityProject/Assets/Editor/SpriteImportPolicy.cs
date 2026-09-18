// Sprite import policy for the reconstructed sprite crops.
//
// In the shipped build a Sprite is a sub-asset of its Texture2D.  The extraction pipeline dumped the
// sprite *pixels* as one crop per sprite (files named "<texture>.sprite.png" / "<texture>.sprite~N.png")
// but not the sprite rect/pivot/border, so the sub-asset form cannot be rebuilt yet (see
// tools/unity-rebuild/a13_import_assets.py -> dest_for_sprite, and the ledger's DEFERRED_COMPOSITE
// entries for the metadata half).
//
// This postprocessor makes each crop import as a Sprite instead of a Texture2D.  Unity derives the
// asset name from the file name, and a13 writes the crops as "<original sprite name>.png", so the
// imported sprite keeps the name the game's prefabs refer to.
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public class SpriteImportPolicy : AssetPostprocessor
    {
        /// <summary>Only crops carry this marker; the owning sheet textures stay plain textures.</summary>
        private const string CropMarker = ".sprite";

        void OnPreprocessTexture()
        {
            if (assetPath == null || assetPath.IndexOf(CropMarker, System.StringComparison.Ordinal) < 0) return;

            var importer = assetImporter as TextureImporter;
            if (importer == null) return;

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            // The original pixels-per-unit and pivot were not dumped; 100 PPU / centre pivot is the
            // documented placeholder (recorded as a deviation, not as recovered data).
            importer.spritePixelsPerUnit = 100f;
            importer.mipmapEnabled = false;
            importer.alphaIsTransparency = true;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            importer.maxTextureSize = 2048;
            importer.textureCompression = TextureImporterCompression.Compressed;
            importer.isReadable = false;
        }
    }
}
