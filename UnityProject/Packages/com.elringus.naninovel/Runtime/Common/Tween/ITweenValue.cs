namespace Naninovel
{
    /// <summary>
    /// The tween object of <see cref="Tweener{TTweenValue}"/>.
    /// </summary>
    public interface ITweenValue
    {
        /// <summary>
        /// Properties of the tween animation.
        /// </summary>
        Tween Props { get; }

        /// <summary>
        /// Perform the value tween over specified ratio, in 0.0 to 1.0 range.
        /// </summary>
        void Tween (float ratio);
    }
}
