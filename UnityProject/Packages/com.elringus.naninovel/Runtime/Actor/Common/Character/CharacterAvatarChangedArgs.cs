using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Arguments associated with the <see cref="ICharacterManager.OnCharacterAvatarChanged"/> event.
    /// </summary>
    public readonly struct CharacterAvatarChangedArgs : IEquatable<CharacterAvatarChangedArgs>
    {
        /// <summary>
        /// ID of the character for which the avatar texture has changed.
        /// </summary>
        public readonly string CharacterId;
        /// <summary>
        /// The new avatar texture of the character.
        /// </summary>
        public readonly Texture2D AvatarTexture;

        public CharacterAvatarChangedArgs (string characterId, Texture2D avatarTexture)
        {
            CharacterId = characterId;
            AvatarTexture = avatarTexture;
        }

        public bool Equals (CharacterAvatarChangedArgs other)
        {
            return CharacterId == other.CharacterId &&
                   Equals(AvatarTexture, other.AvatarTexture);
        }

        public override bool Equals (object obj)
        {
            return obj is CharacterAvatarChangedArgs other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(CharacterId, AvatarTexture);
        }
    }
}
