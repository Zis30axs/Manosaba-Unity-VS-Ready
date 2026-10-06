using System;
using Naninovel;
using UnityEngine;
using UnityEngine.Audio;

namespace WitchTrials.Views
{
    /// <summary>
    /// Freezes the BGM, SFX and voice groups while at least one holder exists (the open witch book) and puts each group's
    /// pitch back to what it was when the last holder lets go, so the audio continues where it stopped.
    /// <para>
    /// A group pitch of 0 is how the game already pauses the voice during a debate (WitchTrialsNaninovelManager.PauseTrial).
    /// It keeps AudioSource.isPlaying true; AudioListener.pause would report every source as stopped, and Naninovel's
    /// AudioController would recycle those tracks the next time it plays a clip.  The three parameters are exposed by
    /// Assets/NaninovelData/Audio/WitchTrials.mixer.
    /// </para>
    /// </summary>
    public static class WitchBookAudioPause
    {
        private static readonly string[] PitchParameters = { "BGM Pitch", "SFX Pitch", "Voice Pitch" };
        private static readonly float[] RestoreValues = new float[PitchParameters.Length];

        private static AudioMixer _mixer;
        private static int _holders;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            _holders = 0;
            _mixer = null;
        }

        public static IDisposable Acquire()
        {
            if (_holders++ == 0) Freeze();
            return new Handle();
        }

        private static void Freeze()
        {
            _mixer = Engine.TryGetService<IAudioManager>(out IAudioManager audio) ? audio.AudioMixer : null;
            if (_mixer == null)
            {
                Debug.LogWarning("WitchBookAudioPause: the Naninovel audio mixer is not available; audio keeps playing.");
                return;
            }
            for (int i = 0; i < PitchParameters.Length; i++)
            {
                if (!_mixer.GetFloat(PitchParameters[i], out RestoreValues[i]))
                {
                    Debug.LogWarning("WitchBookAudioPause: the mixer does not expose '" + PitchParameters[i] + "'.");
                    RestoreValues[i] = float.NaN;
                    continue;
                }
                _mixer.SetFloat(PitchParameters[i], 0f);
            }
        }

        private static void Thaw()
        {
            if (_mixer == null) return;
            for (int i = 0; i < PitchParameters.Length; i++)
                if (!float.IsNaN(RestoreValues[i])) _mixer.SetFloat(PitchParameters[i], RestoreValues[i]);
            _mixer = null;
        }

        private sealed class Handle : IDisposable
        {
            private bool _released;

            public void Dispose()
            {
                if (_released) return;
                _released = true;
                if (--_holders == 0) Thaw();
            }
        }
    }
}
