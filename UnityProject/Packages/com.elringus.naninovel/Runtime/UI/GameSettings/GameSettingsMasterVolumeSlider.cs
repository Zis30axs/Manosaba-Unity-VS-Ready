
namespace Naninovel.UI
{
    public class GameSettingsMasterVolumeSlider : ScriptableSlider
    {
        private IAudioManager audioManager;

        protected override void Awake ()
        {
            base.Awake();

            audioManager = Engine.GetServiceOrErr<IAudioManager>();
        }

        protected override void Start ()
        {
            base.Start();

            UIComponent.value = audioManager.MasterVolume;
        }

        protected override void OnValueChanged (float value)
        {
            audioManager.MasterVolume = value;
        }
    }
}
