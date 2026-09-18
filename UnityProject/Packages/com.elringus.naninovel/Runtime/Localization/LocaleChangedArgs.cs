using System;

namespace Naninovel
{
    /// <summary>
    /// Arguments associated with the <see cref="ILocalizationManager.OnLocaleChanged"/> event. 
    /// </summary>
    public readonly struct LocaleChangedArgs : IEquatable<LocaleChangedArgs>
    {
        /// <summary>
        /// Currently selected (active) locale.
        /// </summary>
        public readonly string CurrentLocale;
        /// <summary>
        /// Locale that was active before the change.
        /// </summary>
        public readonly string PreviousLocale;

        public LocaleChangedArgs (string currentLocale, string previousLocale)
        {
            CurrentLocale = currentLocale;
            PreviousLocale = previousLocale;
        }

        public bool Equals (LocaleChangedArgs other)
        {
            return CurrentLocale == other.CurrentLocale &&
                   PreviousLocale == other.PreviousLocale;
        }

        public override bool Equals (object obj)
        {
            return obj is LocaleChangedArgs other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(CurrentLocale, PreviousLocale);
        }
    }
}
