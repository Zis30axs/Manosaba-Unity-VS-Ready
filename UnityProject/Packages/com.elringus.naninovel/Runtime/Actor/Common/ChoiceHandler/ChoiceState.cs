using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Serializable state of a choice in <see cref="ChoiceHandlerState"/>.
    /// </summary>
    [Serializable]
    public struct ChoiceState : IEquatable<ChoiceState>
    {
        /// <summary>
        /// GUID of the state object.
        /// </summary>
        public string Id => id;
        /// <summary>
        /// Playback spot of the host choice command.
        /// </summary>
        public PlaybackSpot HostedAt => hostedAt;
        /// <summary>
        /// Whether the host choice command contains nested commands, which should be
        /// executed when the choice is picked.
        /// </summary>
        public bool Nested => nested;
        /// <summary>
        /// Text describing consequences of this choice.
        /// </summary>
        public LocalizableText Summary => summary;
        /// <summary>
        /// Whether to choice is locked/disabled.
        /// </summary>
        public bool Locked => locked;
        /// <summary>
        /// Path (relative to a `Resources` folder) to a button prefab representing the choice.
        /// </summary>
        public string ButtonPath => buttonPath;
        /// <summary>
        /// Local position of the choice button inside the choice handler.
        /// </summary>
        public Vector2 ButtonPosition => buttonPosition;
        /// <summary>
        /// Whether to apply <see cref="ButtonPosition"/> (whether user specified a custom position in the script command).
        /// </summary>
        public bool OverwriteButtonPosition => overwriteButtonPosition;
        /// <summary>
        /// Script text to execute when the choice is selected.
        /// </summary>
        public string OnSelectScript => onSelectScript;
        /// <summary>
        /// Whether to continue playing next command when the choice is selected.
        /// </summary>
        public bool AutoPlay => autoPlay;

        [SerializeField] private string id;
        [SerializeField] private PlaybackSpot hostedAt;
        [SerializeField] private bool nested;
        [SerializeField] private LocalizableText summary;
        [SerializeField] private bool locked;
        [SerializeField] private string buttonPath;
        [SerializeField] private Vector2 buttonPosition;
        [SerializeField] private bool overwriteButtonPosition;
        [SerializeField] private string onSelectScript;
        [SerializeField] private bool autoPlay;

        public ChoiceState (PlaybackSpot hostedAt, bool nested, LocalizableText summary = default, bool locked = false,
            string buttonPath = null, Vector2? buttonPosition = null, string onSelectScript = null, bool autoPlay = false)
        {
            id = Guid.NewGuid().ToString();
            this.hostedAt = hostedAt;
            this.nested = nested;
            this.summary = summary;
            this.locked = locked;
            this.buttonPath = buttonPath;
            this.buttonPosition = buttonPosition ?? default;
            overwriteButtonPosition = buttonPosition.HasValue;
            this.onSelectScript = onSelectScript;
            this.autoPlay = autoPlay;
        }

        public override bool Equals (object obj) => obj is ChoiceState state && Equals(state);

        public bool Equals (ChoiceState other) => id == other.id;

        public override int GetHashCode () => 1877310944 + EqualityComparer<string>.Default.GetHashCode(Id);

        public static bool operator == (ChoiceState left, ChoiceState right) => left.Equals(right);

        public static bool operator != (ChoiceState left, ChoiceState right) => !(left == right);
    }
}
