namespace Naninovel.Commands
{
    [Doc(
        @"
Plays a voice clip at the specified path.",
        null,
        @"
; Given a 'Rawr' voice resource is available, play it.
@voice Rawr"
    )]
    [CommandAlias("voice")]
    public class PlayVoice : AudioCommand, Command.IPreloadable
    {
        [Doc("Path to the voice clip to play.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ResourceContext(AudioConfiguration.DefaultVoicePathPrefix)]
        public StringParameter VoicePath;
        [Doc("Volume of the playback.")]
        [ParameterDefaultValue("1")]
        public DecimalParameter Volume = 1f;
        [Doc("Audio mixer [group path](https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.FindMatchingGroups) that should be used when playing the audio.")]
        [ParameterAlias("group")]
        public StringParameter GroupPath;
        [Doc("ID of the character actor this voice belongs to. When specified and [per-author volume](/guide/voicing#author-volume) is used, volume will be adjusted accordingly.")]
        public StringParameter AuthorId;

        public async UniTask PreloadResources ()
        {
            if (!Assigned(VoicePath) || VoicePath.DynamicValue) return;
            await AudioManager.VoiceLoader.Load(VoicePath, this);
        }

        public void ReleaseResources ()
        {
            if (!Assigned(VoicePath) || VoicePath.DynamicValue) return;
            AudioManager?.VoiceLoader?.Release(VoicePath, this);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            await AudioManager.PlayVoice(VoicePath, Volume, GroupPath, AuthorId);
        }
    }
}
