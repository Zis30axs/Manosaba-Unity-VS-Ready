using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents an asset dropped to <see cref="AssetDropHandler"/>.
    /// </summary>
    public class DroppedAsset
    {
        /// <summary>
        /// The dropped asset.
        /// </summary>
        public Object Asset { get; }
        /// <summary>
        /// The dropped asset GUID.
        /// </summary>
        public string Guid { get; }
        /// <summary>
        /// Relative path to the asset when discovered inside a dropped folder.
        /// When the asset is dropped directly equals to the asset name.
        /// </summary>
        public string RelativePath { get; }

        public DroppedAsset (Object asset, string guid, string relativePath = null)
        {
            Asset = asset;
            Guid = guid;
            RelativePath = relativePath ?? asset.name;
        }
    }
}
