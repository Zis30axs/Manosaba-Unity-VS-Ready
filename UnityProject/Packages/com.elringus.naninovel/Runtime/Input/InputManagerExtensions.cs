using static Naninovel.InputNames;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IInputManager"/>.
    /// </summary>
    public static class InputManagerExtensions
    {
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/>; returns true when successful. 
        /// </summary>
        /// <remarks>
        /// Use <see cref="InputNames"/> for list of the pre-defined sampler names.
        /// </remarks>
        public static bool TryGetSampler (this IInputManager m, string name, out IInputSampler sampler) => (sampler = m?.GetSampler(name)) != null;
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Submit"/>. 
        /// </summary>
        public static IInputSampler GetSubmit (this IInputManager m) => m.GetSampler(Submit);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Cancel"/>. 
        /// </summary>
        public static IInputSampler GetCancel (this IInputManager m) => m.GetSampler(Cancel);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Delete"/>. 
        /// </summary>
        public static IInputSampler GetDelete (this IInputManager m) => m.GetSampler(Delete);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.NavigateX"/>. 
        /// </summary>
        public static IInputSampler GetNavigateX (this IInputManager m) => m.GetSampler(NavigateX);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.NavigateY"/>. 
        /// </summary>
        public static IInputSampler GetNavigateY (this IInputManager m) => m.GetSampler(NavigateY);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.ScrollY"/>. 
        /// </summary>
        public static IInputSampler GetScrollY (this IInputManager m) => m.GetSampler(ScrollY);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Page"/>. 
        /// </summary>
        public static IInputSampler GetPage (this IInputManager m) => m.GetSampler(Page);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Tab"/>. 
        /// </summary>
        public static IInputSampler GetTab (this IInputManager m) => m.GetSampler(Tab);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Continue"/>. 
        /// </summary>
        public static IInputSampler GetContinue (this IInputManager m) => m.GetSampler(Continue);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Skip"/>. 
        /// </summary>
        public static IInputSampler GetSkip (this IInputManager m) => m.GetSampler(Skip);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.ToggleSkip"/>. 
        /// </summary>
        public static IInputSampler GetToggleSkip (this IInputManager m) => m.GetSampler(ToggleSkip);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.SkipMovie"/>. 
        /// </summary>
        public static IInputSampler GetSkipMovie (this IInputManager m) => m.GetSampler(SkipMovie);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.AutoPlay"/>. 
        /// </summary>
        public static IInputSampler GetAutoPlay (this IInputManager m) => m.GetSampler(AutoPlay);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.ToggleUI"/>. 
        /// </summary>
        public static IInputSampler GetToggleUI (this IInputManager m) => m.GetSampler(ToggleUI);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.ShowBacklog"/>. 
        /// </summary>
        public static IInputSampler GetShowBacklog (this IInputManager m) => m.GetSampler(ShowBacklog);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Rollback"/>. 
        /// </summary>
        public static IInputSampler GetRollback (this IInputManager m) => m.GetSampler(Rollback);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.CameraLookX"/>. 
        /// </summary>
        public static IInputSampler GetCameraLookX (this IInputManager m) => m.GetSampler(CameraLookX);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.CameraLookY"/>. 
        /// </summary>
        public static IInputSampler GetCameraLookY (this IInputManager m) => m.GetSampler(CameraLookY);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.Pause"/>. 
        /// </summary>
        public static IInputSampler GetPause (this IInputManager m) => m.GetSampler(Pause);
        /// <summary>
        /// Attempts to <see cref="IInputManager.GetSampler(string)"/> of <see cref="InputNames.ToggleConsole"/>. 
        /// </summary>
        public static IInputSampler GetToggleConsole (this IInputManager m) => m.GetSampler(ToggleConsole);
    }
}
