using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace WitchTrials.Views
{
    /// <summary>Plays the two existing objection voices as one explicitly concurrent group.</summary>
    [UsedImplicitly]
    [CommandAlias("objectionVoicePair")]
    public sealed class PlayObjectionVoicePair : AudioCommand, Command.IPreloadable
    {
        [RequiredParameter, ResourceContext(AudioConfiguration.DefaultVoicePathPrefix)]
        public StringParameter VoicePath;
        [ParameterDefaultValue("1")]
        public DecimalParameter Volume = 1f;
        public StringParameter GroupPath;
        public StringParameter AuthorId;
        [RequiredParameter, ResourceContext(AudioConfiguration.DefaultVoicePathPrefix)]
        public StringParameter SecondVoicePath;
        [ParameterDefaultValue("1")]
        public DecimalParameter SecondVolume = 1f;
        public StringParameter SecondGroupPath;
        public StringParameter SecondAuthorId;

        public async UniTask PreloadResources()
        {
            try
            {
                await Preload(VoicePath);
                await Preload(SecondVoicePath);
            }
            catch
            {
                ReleaseResources();
                throw;
            }
        }

        public void ReleaseResources()
        {
            Release(VoicePath);
            Release(SecondVoicePath);
        }

        public override UniTask Execute(AsyncToken token = default)
        {
            return AudioManager.PlayVoiceGroup(new[] {
                new VoicePlayback(VoicePath, Assigned(Volume) ? Volume.Value : 1f, GroupPath, AuthorId),
                new VoicePlayback(SecondVoicePath, Assigned(SecondVolume) ? SecondVolume.Value : 1f,
                    SecondGroupPath, SecondAuthorId)
            }, token);
        }

        private async UniTask Preload(StringParameter path)
        {
            if (!Assigned(path) || path.DynamicValue || string.IsNullOrWhiteSpace(path.Value)) return;
            await AudioManager.VoiceLoader.LoadOrErr(path, this);
        }

        private void Release(StringParameter path)
        {
            if (!Assigned(path) || path.DynamicValue || string.IsNullOrWhiteSpace(path.Value)) return;
            Engine.GetService<IAudioManager>()?.VoiceLoader?.Release(path, this);
        }
    }
}
