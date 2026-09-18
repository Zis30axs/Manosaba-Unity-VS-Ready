using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to play <see cref="AudioClip"/>.
    /// </summary>
    public interface IAudioPlayer
    {
        /// <summary>
        /// Current volume of the player.
        /// </summary>
        float Volume { get; set; }

        /// <summary>
        /// Checks whether specified clip is currently playing.
        /// </summary>
        bool IsPlaying (AudioClip clip);
        /// <summary>
        /// Starts playback of the specified clip.
        /// </summary>
        void Play (AudioClip clip, AudioSource audioSource = null, float volume = 1f,
            bool loop = false, AudioMixerGroup mixerGroup = null, AudioClip introClip = null, bool additive = false);
        /// <summary>
        /// Starts playback of the specified clip fading in volume over the specified time (in seconds).
        /// </summary>
        UniTask Play (AudioClip clip, float fadeInTime, AudioSource audioSource = null, float volume = 1f,
            bool loop = false, AudioMixerGroup mixerGroup = null, AudioClip introClip = null, bool additive = false,
            AsyncToken token = default);
        /// <summary>
        /// Stops playback of the specified clip.
        /// </summary>
        void Stop (AudioClip clip);
        /// <summary>
        /// Stops playback of all the playing clips.
        /// </summary>
        void StopAll ();
        /// <summary>
        /// Stops playback of the specified clip fading out volume over the specified time (in seconds).
        /// </summary>
        UniTask Stop (AudioClip clip, float fadeOutTime, AsyncToken token = default);
        /// <summary>
        /// Stops playback of all the playing clips fading out volume over the specified time (in seconds).
        /// </summary>
        UniTask StopAll (float fadeOutTime, AsyncToken token = default);
        /// <summary>
        /// Returns <see cref="IAudioTrack"/> associated with the specified clip.
        /// </summary>
        IReadOnlyCollection<IAudioTrack> GetTracks (AudioClip clip);
    }
}
