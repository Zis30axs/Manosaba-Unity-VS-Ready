using UnityEngine;

namespace Naninovel.UI
{
    public class GameSettingsVoiceVolumeSlider : ScriptableSlider
    {
        [Tooltip("When specified, the slider will control voice volume of the printed message author (character actor) with the specified ID. When empty will control voice mixer group volume.")]
        [SerializeField] private string authorId;

        private IAudioManager audioManager;

        protected override void Awake ()
        {
            base.Awake();

            audioManager = Engine.GetServiceOrErr<IAudioManager>();
        }

        protected override void Start ()
        {
            base.Start();

            var authorVolume = audioManager.GetAuthorVolume(authorId);
            UIComponent.value = Mathf.Approximately(authorVolume, -1) ? audioManager.VoiceVolume : authorVolume;
        }

        protected override void OnValueChanged (float value)
        {
            if (string.IsNullOrEmpty(authorId))
                audioManager.VoiceVolume = value;
            else audioManager.SetAuthorVolume(authorId, value);
        }
    }
}
