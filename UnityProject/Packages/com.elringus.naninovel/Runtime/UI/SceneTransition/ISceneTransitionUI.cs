
namespace Naninovel.UI
{
    /// <summary>
    /// Handles scene transition (<see cref="Commands.StartSceneTransition"/> and <see cref="Commands.FinishSceneTransition"/> commands).
    /// </summary>
    public interface ISceneTransitionUI : IManagedUI 
    {
        /// <summary>
        /// Saves the current main camera content to a temporary render texture to use during the transition.
        /// </summary>
        UniTask CaptureScene ();
        /// <summary>
        /// Performs transition between the previously captured scene texture and current main camera content.
        /// </summary>
        UniTask Transition (Transition transition, Tween tween, AsyncToken token = default);
    }
}
