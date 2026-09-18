using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage unlockable items (CG and movie gallery items, tips, etc).
    /// </summary>
    public interface IUnlockableManager : IEngineService<UnlockablesConfiguration>
    {
        /// <summary>
        /// Invoked when unlocked state of an unlockable item is changed (or when it's added to the map for the first time).
        /// </summary>
        event Action<UnlockableItemUpdatedArgs> OnItemUpdated;

        /// <summary>
        /// Identifiers of the managed unlockable items.
        /// </summary>
        IReadOnlyCollection<string> ItemIds { get; }

        /// <summary>
        /// Checks whether unlockable item with the specified ID exists and is unlocked.
        /// </summary>
        bool ItemUnlocked (string itemId);
        /// <summary>
        /// Modifies unlockable state for an unlockable item with the specified ID.
        /// In case item with the specified ID doesn't exist, will add it to the map.
        /// </summary>
        void SetItemUnlocked (string itemId, bool unlocked);
        /// <summary>
        /// Makes unlockable item with the specified ID unlocked.
        /// In case item with the specified ID doesn't exist, will add it to the map.
        /// </summary>
        void UnlockItem (string itemId);
        /// <summary>
        /// Makes unlockable item with the specified ID locked.
        /// In case item with the specified ID doesn't exist, will add it to the map.
        /// </summary>
        void LockItem (string itemId);
        /// <summary>
        /// Makes all the stored unlockable items unlocked.
        /// </summary>
        void UnlockAllItems ();
        /// <summary>
        /// Makes all the stored unlockable items locked.
        /// </summary>
        void LockAllItems ();
    }
}
