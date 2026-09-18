namespace Naninovel
{
    /// <summary>
    /// Implementation represents an object, that can be used to trigger a player input.
    /// </summary>
    public interface IInputTrigger
    {
        /// <summary>
        /// Whether the object is allowed to trigger input at the moment.
        /// </summary>
        bool CanTriggerInput ();
    }
}
