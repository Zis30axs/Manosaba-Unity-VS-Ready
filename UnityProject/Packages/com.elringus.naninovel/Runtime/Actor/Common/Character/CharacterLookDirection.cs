using UnityEngine;

namespace Naninovel
{
    public enum CharacterLookDirection
    {
        Center,
        Left,
        Right
    }

    public static class CharacterLookDirectionExtensions
    {
        public static Vector2 ToVector2 (this CharacterLookDirection lookDirection) => lookDirection switch {
            CharacterLookDirection.Left => Vector2.left,
            CharacterLookDirection.Right => Vector2.right,
            _ => Vector2.zero
        };
    }
}
