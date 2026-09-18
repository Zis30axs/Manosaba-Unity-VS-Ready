using System;

namespace Naninovel
{
    /// <summary>
    /// Arguments associated with the game save and load events invoked by <see cref="IStateManager"/>. 
    /// </summary>
    public readonly struct GameSaveLoadArgs : IEquatable<GameSaveLoadArgs>
    {
        /// <summary>
        /// ID of the save slot the operation is associated with.
        /// </summary>
        public readonly string SlotId;
        /// <summary>
        /// Whether it's a quick save/load operation.
        /// </summary>
        public readonly bool Quick;

        public GameSaveLoadArgs (string slotId, bool quick)
        {
            SlotId = slotId;
            Quick = quick;
        }

        public bool Equals (GameSaveLoadArgs other)
        {
            return SlotId == other.SlotId && Quick == other.Quick;
        }
        public override bool Equals (object obj)
        {
            return obj is GameSaveLoadArgs other && Equals(other);
        }
        public override int GetHashCode ()
        {
            return HashCode.Combine(SlotId, Quick);
        }
    }
}
