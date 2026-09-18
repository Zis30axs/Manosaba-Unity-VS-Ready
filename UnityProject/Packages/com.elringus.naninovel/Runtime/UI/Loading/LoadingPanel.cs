using UnityEngine;

namespace Naninovel.UI
{
    public class LoadingPanel : CustomUI, ILoadingUI
    {
        [Tooltip("Event invoked when script preload progress is changed, in 0.0 to 1.0 range.")]
        [SerializeField] private FloatUnityEvent onProgressChanged;

        private IScriptLoader loader;

        protected override void Awake ()
        {
            base.Awake();

            loader = Engine.GetServiceOrErr<IScriptLoader>();
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            loader.OnLoadProgress += HandleProgressChanged;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            if (loader != null)
                loader.OnLoadProgress -= HandleProgressChanged;
        }

        protected override void HandleVisibilityChanged (bool visible)
        {
            base.HandleVisibilityChanged(visible);
            onProgressChanged?.Invoke(0);
        }

        protected virtual void HandleProgressChanged (float value) => onProgressChanged?.Invoke(value);
    }
}
