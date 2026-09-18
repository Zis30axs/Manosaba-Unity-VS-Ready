using TMPro;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Allows accessing localization resources authored by the player community for the published game.
    /// </summary>
    public interface ICommunityLocalization : IEngineService
    {
        /// <summary>
        /// Whether the community localization is currently enabled.
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// Author of the active localization.
        /// </summary>
        string Author { get; }
        /// <summary>
        /// Manages text resources associated with the community localization.
        /// </summary>
        /// <remarks>
        /// The resources are serialized managed text documents; they are used instead of the
        /// <see cref="ITextManager"/>'s own resources when the community l10n is active.
        /// </remarks>
        IResourceLoader<TextAsset> TextLoader { get; }

        /// <summary>
        /// Loads font asset based on font face specified by the community localization author.
        /// </summary>
        UniTask<TMP_FontAsset> LoadFont ();
    }
}
