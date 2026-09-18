using System;
using Naninovel.UI;

namespace Naninovel
{
    /// <summary>
    /// Blocks user interaction with the Naninovel UIs and input sampling until the instance is disposed.
    /// Requires <see cref="IUIManager"/> and <see cref="IClickThroughPanel"/> to be available when instantiated.
    /// </summary>
    public class InteractionBlocker : IDisposable
    {
        private readonly IClickThroughPanel panel;

        public InteractionBlocker ()
        {
            var panel = Engine.GetService<IUIManager>()?.GetUI<IClickThroughPanel>();
            if (panel == null || panel.Visible) return;
            this.panel = panel;
            panel.Show(false, null);
        }

        public void Dispose () => panel?.Hide();
    }
}
