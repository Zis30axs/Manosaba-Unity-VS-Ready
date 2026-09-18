using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    public class TypingIndicator : MonoBehaviour
    {
        [SerializeField] private float printDotDelay = .5f;
        [SerializeField] private string typeSymbol = ". ";
        [SerializeField] private int symbolCount = 3;
        [SerializeField] private TMP_Text text;

        private float lastPrintDotTime;

        private void Awake ()
        {
            this.AssertRequiredObjects(text);
            text.text = string.Empty;
        }

        private void Update ()
        {
            if (Engine.Time.UnscaledTime < lastPrintDotTime + printDotDelay) return;

            lastPrintDotTime = Engine.Time.UnscaledTime;
            text.text = text.text.Length >= typeSymbol.Length * symbolCount ? string.Empty : text.text + typeSymbol;
        }
    }
}
