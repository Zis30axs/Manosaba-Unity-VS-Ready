using UnityEngine;

namespace Naninovel.UI
{
    public class ContinueInputUI : CustomUI, IContinueInputUI
    {
        protected GameObject Trigger => trigger;

        [SerializeField] private GameObject trigger;

        private IInputManager inputManager;

        public override UniTask Initialize ()
        {
            inputManager?.GetContinue()?.AddObjectTrigger(trigger);
            return UniTask.CompletedTask;
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(trigger);

            inputManager = Engine.GetServiceOrErr<IInputManager>();
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            inputManager?.GetContinue()?.RemoveObjectTrigger(trigger);
        }
    }
}
