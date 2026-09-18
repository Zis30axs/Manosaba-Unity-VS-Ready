using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IAudioManager"/>.
    /// </summary>
    public static class AudioManagerExtensions
    {
        /// <summary>
        /// Plays voice clips with the specified resource paths in sequence.
        /// </summary>
        /// <param name="pathList">Names (local paths) of the voice resources.</param>
        /// <param name="volume">Volume of the voice playback.</param>
        /// <param name="group">Path of an <see cref="AudioMixerGroup"/> of the current <see cref="AudioMixer"/> to use when playing the voice.</param>
        /// <param name="authorId">ID of the author (character actor) of the played voices.</param>
        public static async UniTask PlayVoiceSequence (this IAudioManager manager, IReadOnlyCollection<string> pathList,
            float volume = 1f, string group = default, string authorId = default, AsyncToken token = default)
        {
            foreach (var path in pathList)
            {
                await manager.PlayVoice(path, volume, group, authorId, token);
                await UniTask.WaitWhile(() => manager.IsVoicePlaying(path) && token.EnsureNotCanceled());
            }
        }
    }
}
