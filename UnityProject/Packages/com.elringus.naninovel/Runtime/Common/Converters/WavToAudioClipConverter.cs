using System;
using System.IO;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Converts <see cref="T:byte[]"/> raw data of a .wav audio file to <see cref="AudioClip"/>.
    /// Only PCM16 44100Hz stereo .wav are supported.
    /// </summary>
    public class WavToAudioClipConverter : IRawConverter<AudioClip>
    {
        public RawDataRepresentation[] Representations { get; } = {
            new(".wav", "audio/wav")
        };

        public AudioClip ConvertBlocking (byte[] obj, string path)
        {
            var floatArr = Pcm16ToFloatArray(obj);

            var audioClip = AudioClip.Create("Generated WAV Audio", floatArr.Length / 2, 2, 44100, false);
            audioClip.name = Path.GetFileNameWithoutExtension(path);
            audioClip.SetData(floatArr, 0);

            return audioClip;
        }

        public async UniTask<AudioClip> Convert (byte[] obj, string path)
        {
            var floatArr = await UniTask.Run(() => Pcm16ToFloatArray(obj));

            var audioClip = AudioClip.Create("Generated WAV Audio", floatArr.Length / 2, 2, 44100, false);
            audioClip.name = Path.GetFileNameWithoutExtension(path);
            audioClip.SetData(floatArr, 0);

            return audioClip;
        }

        public object ConvertBlocking (object obj, string path) => ConvertBlocking(obj as byte[], path);

        public async UniTask<object> Convert (object obj, string path) => await Convert(obj as byte[], path);

        private static float[] Pcm16ToFloatArray (byte[] input)
        {
            // PCM16 wav usually has 44 byte headers, though not always. 
            // https://stackoverflow.com/questions/19991405/how-can-i-detect-whether-a-wav-file-has-a-44-or-46-byte-header
            const int headerSize = 444;
            var inputSamples = input.Length / 2; // 16 bit input, so 2 bytes per sample.
            var output = new float[inputSamples];
            var outputIndex = 0;
            for (var n = headerSize; n < inputSamples; n++)
            {
                short sample = BitConverter.ToInt16(input, n * 2);
                output[outputIndex++] = sample / 32768f;
            }

            return output;
        }
    }
}
