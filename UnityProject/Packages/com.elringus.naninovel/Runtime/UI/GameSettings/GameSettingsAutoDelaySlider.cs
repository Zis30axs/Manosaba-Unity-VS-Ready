using UnityEngine;

namespace Naninovel.UI
{
    public class GameSettingsAutoDelaySlider : ScriptableSlider
    {
        protected virtual bool PreviewPrinterAvailable => ObjectUtils.IsValid(previewPrinter) && previewPrinter.isActiveAndEnabled;
        protected GameSettingsPreviewPrinter PreviewPrinter => previewPrinter;

        [SerializeField] private GameSettingsPreviewPrinter previewPrinter;

        private ITextPrinterManager printerManager;

        protected override void Awake ()
        {
            base.Awake();

            printerManager = Engine.GetServiceOrErr<ITextPrinterManager>();
            UIComponent.value = printerManager.BaseAutoDelay;
        }

        protected override void OnValueChanged (float value)
        {
            printerManager.BaseAutoDelay = value;

            if (PreviewPrinterAvailable)
                previewPrinter.StartPrinting();
        }
    }
}
