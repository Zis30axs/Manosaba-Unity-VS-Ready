using System;

namespace Naninovel
{
    /// <summary>
    /// Arguments associated with the <see cref="IUnlockableManager.OnItemUpdated"/> event. 
    /// </summary>
    public readonly struct UnlockableItemUpdatedArgs : IEquatable<UnlockableItemUpdatedArgs>
    {
        /// <summary>
        /// ID of the updated unlockable item.
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// Whether the item is now unlocked.
        /// </summary>
        public readonly bool Unlocked;
        /// <summary>
        /// Whether the item has been added to the unlockables map for the first time.
        /// </summary>
        public readonly bool Added;

        public UnlockableItemUpdatedArgs (string id, bool unlocked, bool added)
        {
            Id = id;
            Unlocked = unlocked;
            Added = added;
        }

        public bool Equals (UnlockableItemUpdatedArgs other)
        {
            return Id == other.Id &&
                   Unlocked == other.Unlocked &&
                   Added == other.Added;
        }

        public override bool Equals (object obj)
        {
            return obj is UnlockableItemUpdatedArgs other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(Id, Unlocked, Added);
        }
    }
}
