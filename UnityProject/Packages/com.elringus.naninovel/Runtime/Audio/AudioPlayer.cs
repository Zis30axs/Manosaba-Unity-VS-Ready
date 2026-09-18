using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel
{
    public class AudioPlayer : IAudioPlayer, IDisposable
    {
        public float Volume { get => controller.Volume; set => controller.Volume = value; }

        private readonly AudioController controller = Engine.CreateObject<AudioController>();

        public void Dispose ()
        {
            controller.StopAllClips();
            ObjectUtils.DestroyOrImmediate(controller.gameObject);
        }

        public bool IsPlaying (AudioClip clip) => controller.ClipPlaying(clip);

        public void Play (AudioClip clip, AudioSource audioSource = null, float volume = 1,
            bool loop = false, AudioMixerGroup mixerGroup = null, AudioClip introClip = null, bool additive = false)
        {
            controller.PlayClip(clip, audioSource, volume, loop, mixerGroup, introClip, additive);
        }

        public UniTask Play (AudioClip clip, float fadeInTime, AudioSource audioSource = null,
            float volume = 1, bool loop = false, AudioMixerGroup mixerGroup = null, AudioClip introClip = null, bool additive = false, AsyncToken token = default)
        {
            return controller.PlayClip(clip, fadeInTime, audioSource, volume, loop, mixerGroup, introClip, additive, token);
        }

        public void Stop (AudioClip clip) => controller.StopClip(clip);

        public void StopAll () => controller.StopAllClips();

        public UniTask Stop (AudioClip clip, float fadeOutTime, AsyncToken token = default)
        {
            return controller.StopClip(clip, fadeOutTime, token);
        }

        public UniTask StopAll (float fadeOutTime, AsyncToken token = default)
        {
            return controller.StopAllClips(fadeOutTime, token);
        }

        public IReadOnlyCollection<IAudioTrack> GetTracks (AudioClip clip) => controller.GetTracks(clip);
    }
}
