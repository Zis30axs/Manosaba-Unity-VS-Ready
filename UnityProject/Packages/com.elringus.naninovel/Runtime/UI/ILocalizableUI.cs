namespace Naninovel.UI
{
    /// <summary>
    /// Implementing <see cref="IManagedUI"/> is notified when localization is changed.
    /// </summary>
    public interface ILocalizableUI
    {
        UniTask HandleLocalizationChanged (LocaleChangedArgs args);
    }
}
