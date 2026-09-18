using System;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Implementation represents a UI object managed by <see cref="IUIManager"/> service.
    /// </summary>
    public interface IManagedUI
    {
        /// <summary>
        /// Event invoked when <see cref="Visible"/> of the UI object is changed.
        /// </summary>
        event Action<bool> OnVisibilityChanged;

        /// <summary>
        /// Whether the UI element is currently visible to the user.
        /// </summary>
        /// <remarks>
        /// The visibility should change instantly when set.
        /// </remarks>
        bool Visible { get; set; }
        /// <summary>
        /// Camera the UI element uses for reference when scaling and handling user input.
        /// </summary>
        Camera RenderCamera { get; set; }
        /// <summary>
        /// When not null, will not yield the UI from being modal when another UI is made modal with the same group.
        /// When '*' will never yield the UI from being modal (until it's explicitly removed via <see cref="IUIManager.RemoveModalUI"/>).
        /// </summary>
        string ModalGroup { get; }
        /// <summary>
        /// Sorting order of the UI. Higher values makes the UI render on top.
        /// </summary>
        int SortingOrder { get; }

        /// <summary>
        /// Allows to execute an async initialization logic.
        /// Invoked once by <see cref="IUIManager"/> when managed UI is instantiated.
        /// </summary>
        UniTask Initialize ();
        /// <summary>
        /// Gradually changes visibility over default (implementation-specific) or specified <paramref name="duration"/> (in seconds).
        /// </summary>
        /// <remarks>
        /// <see cref="Visible"/> should be set to <paramref name="visible"/> at the time this method is invoked.
        /// Should not return until visibility (as perceived by user) has been completely changed (including any associated animations).
        /// </remarks>
        UniTask ChangeVisibility (bool visible, float? duration = null, AsyncToken token = default);
        /// <summary>
        /// Applies specified fonts to the TMPro text components contained in the UI.
        /// Null identifies default font initially set in text components of the UI prefab.
        /// </summary>
        void SetFont (TMP_FontAsset font);
        /// <summary>
        /// Applies specified font size to the text elements contained in the UI.
        /// -1 identifies default size initially set in text components of the UI prefab.
        /// </summary>
        void SetFontSize (int size);
    }
}
