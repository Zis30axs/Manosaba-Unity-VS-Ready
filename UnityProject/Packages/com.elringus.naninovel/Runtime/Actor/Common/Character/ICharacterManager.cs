using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage <see cref="ICharacterActor"/> actors.
    /// </summary>
    public interface ICharacterManager : IActorManager<ICharacterActor, CharacterState, CharacterMetadata, CharactersConfiguration>
    {
        /// <summary>
        /// Invoked when avatar texture of a managed character is changed.
        /// </summary>
        event Action<CharacterAvatarChangedArgs> OnCharacterAvatarChanged;

        /// <summary>
        /// Checks whether avatar texture with the specified (local) path exists.
        /// </summary>
        bool AvatarTextureExists (string avatarTexturePath);
        /// <summary>
        /// Un-assigns avatar texture from a character with the specified ID.
        /// </summary>
        void RemoveAvatarTextureFor (string characterId);
        /// <summary>
        /// Attempts to retrieve currently assigned avatar texture for a character with the specified ID.
        /// Will return null when character is not found or doesn't have an avatar texture assigned.
        /// </summary>
        [CanBeNull] Texture2D GetAvatarTextureFor (string characterId);
        /// <summary>
        /// Attempts to retrieve a (local) path of the currently assigned avatar texture for a character with the specified ID.
        /// Will return null when character is not found or doesn't have an avatar texture assigned.
        /// </summary>
        [CanBeNull] string GetAvatarTexturePathFor (string characterId);
        /// <summary>
        /// Assigns avatar texture with the specified (local) path to a character with the specified ID.
        /// </summary>
        void SetAvatarTexturePathFor (string characterId, string avatarTexturePath);
        /// <summary>
        /// Attempts to get author name, ie name displayed in text printers when character is the author of the printed text.
        /// When character doesn't have <see cref="CharacterMetadata.DisplayName"/> assigned, returns character ID.
        /// When <see cref="CharacterMetadata.HasName"/> is disabled, returns null.
        /// </summary>
        [CanBeNull] string GetAuthorName (string characterId);
        /// <summary>
        /// Given character x position, returns a look direction to the scene origin.
        /// </summary>
        CharacterLookDirection LookAtOriginDirection (float xPos);
        /// <summary>
        /// Evenly distribute positions of the visible managed characters using specified animation tween.
        /// </summary>
        /// <param name="lookAtOrigin">Whether to also make the characters look at the scene origin.</param>
        UniTask ArrangeCharacters (bool lookAtOrigin, Tween tween, AsyncToken token = default);
    }
}
