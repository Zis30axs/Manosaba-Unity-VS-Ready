using System;
using Naninovel.UI;

namespace Naninovel
{
    /// <summary>
    /// Shows <see cref="ILoadingUI"/> until the instance is disposed.
    /// Requires <see cref="IUIManager"/> and <see cref="ILoadingUI"/> to be available when instantiated.
    /// </summary>
    public class LoadingScreen : IDisposable
    {
        private readonly ILoadingUI loadingUI;

        private LoadingScreen ()
        {
            loadingUI = Engine.GetServiceOrErr<IUIManager>()?.GetUI<ILoadingUI>();
        }

        public static async UniTask<IDisposable> Show (AsyncToken token = default)
        {
            var screen = new LoadingScreen();
            if (screen.loadingUI != null)
                await screen.loadingUI.ChangeVisibility(true, token: token.CancellationToken);
            token.ThrowIfCanceled();
            return screen;
        }

        public void Dispose () => loadingUI?.Hide();
    }
}
