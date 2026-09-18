using UnityEngine;
using UnityEngine.EventSystems;

namespace Naninovel.UI
{
    // A hack to prevent button from de-selecting after it's activated with gamepad (eg, play voice in backlog).
    public class FocusOnSubmit : MonoBehaviour, ISubmitHandler
    {
        public void OnSubmit (BaseEventData eventData)
        {
            EventUtils.Select(gameObject);
        }
    }
}
