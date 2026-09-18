using System.Linq;
using Naninovel.UI;
using UnityEngine.UI;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IUIManager"/>.
    /// </summary>
    public static class UIManagerExtensions
    {
        /// <summary>
        /// Attempts to retrieve managed UI of the specified type <typeparamref name="T"/>.
        /// </summary>
        public static bool TryGetUI<T> (this IUIManager manager, out T ui) where T : class, IManagedUI
        {
            ui = manager?.GetUI<T>();
            return ui != null;
        }

        /// <summary>
        /// Attempts to retrieve managed UI with the specified name.
        /// </summary>
        public static bool TryGetUI (this IUIManager manager, string name, out IManagedUI ui)
        {
            ui = manager?.GetUI(name);
            return ui != null;
        }

        /// <summary>
        /// Attempts to select game object under the topmost visible managed UI.
        /// Returns true when object was found and focused; false otherwise.
        /// </summary>
        public static bool FocusTop (this IUIManager manager)
        {
            using var _ = ListPool<IManagedUI>.Rent(out var uis);
            manager.GetManagedUIs(uis);

            var top = uis.OfType<CustomUI>()
                .Where(ui => ui.Visible)
                .OrderByDescending(ui => ui.SortingOrder).FirstOrDefault();
            if (!top) return false;
            if (top.FocusObject && top.FocusObject.activeInHierarchy)
                EventUtils.Select(top.FocusObject);
            var selectable = top.Selectables
                .FirstOrDefault(s => s.Navigation.mode != Navigation.Mode.None &&
                                     s.Selectable.gameObject.activeInHierarchy).Selectable;
            if (!selectable) return false;
            EventUtils.Select(selectable.gameObject);
            return true;
        }
    }
}
