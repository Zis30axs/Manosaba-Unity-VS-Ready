using System;
using UnityEngine.EventSystems;

namespace Naninovel.UI
{
    /// <inheritdoc cref="IClickThroughPanel"/>
    public class ClickThroughPanel : CustomUI, IClickThroughPanel, IPointerClickHandler
    {
        private IInputManager inputManager;
        private Action onClick;
        private bool hideOnClick;

        public virtual void Show (bool hideOnClick, Action onClick, params string[] allowedSamplers)
        {
            this.hideOnClick = hideOnClick;
            this.onClick = onClick;
            Show();
            inputManager.AddBlockingUI(this, allowedSamplers);
        }

        public override void Hide ()
        {
            onClick = null;
            inputManager.RemoveBlockingUI(this);
            base.Hide();
        }

        public virtual void OnPointerClick (PointerEventData eventData)
        {
            onClick?.Invoke();
            if (hideOnClick) Hide();
        }

        protected override void Awake ()
        {
            base.Awake();

            inputManager = Engine.GetServiceOrErr<IInputManager>();
        }
    }
}
