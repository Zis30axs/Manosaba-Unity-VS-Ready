// Audio import policy for the reconstructed audio library (26,538 clips).
//
// Why this exists: Unity's default AudioImporter (DecompressOnLoad + preload) is a poor fit for a
// library this size -- every clip would be decoded into memory at load time -- and the default
// import re-encodes the shipped Vorbis stream, which is what made a first bulk import run at
// ~2.4 clips/s (measured 2026-09-17: 8,562 of 27,906 assets in one hour, ETA ~2.2 h for audio
// alone).  The shipped files are already Vorbis; keeping long clips as streams avoids both the
// re-encode and the memory cost.
//
// Policy (documented, deterministic, no per-asset guesswork):
//   * > 300 KB  (BGM, ambience, long effects) : Streaming + Vorbis, no preload
//   * otherwise (voice, short SFX)            : CompressedInMemory + Vorbis q=0.7
//   * sample rate is preserved: the shipped stream is the reference, resampling would be lossy
//
// This is an *import setting* reconstruction, not asset content: the audit tracks importer
// configuration separately from the file data.
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public class AudioImportPolicy : AssetPostprocessor
    {
        /// <summary>Above this size a clip is treated as a long stream rather than a short cue.</summary>
        private const long StreamingThresholdBytes = 300 * 1024;

        private const float ShortClipQuality = 0.7f;

        /// <summary>
        /// Policy selector, readable from the environment so an import experiment can be run without
        /// editing code (set MANOSABA_AUDIO_POLICY before launching the Editor):
        ///   off       - leave Unity's defaults alone
        ///   mixed     - the default: Streaming above the threshold, CompressedInMemory below
        ///   streaming - every clip is a stream (no re-encode at import time)
        /// </summary>
        public static string Mode
        {
            get
            {
                string configured = Environment.GetEnvironmentVariable("MANOSABA_AUDIO_POLICY");
                return string.IsNullOrEmpty(configured) ? "mixed" : configured.Trim().ToLowerInvariant();
            }
        }

        /// <summary>Set false to disable the policy entirely (kept for scripted experiments).</summary>
        public static bool Enabled = true;

        private void OnPreprocessAudio()
        {
            string mode = Mode;
            if (!Enabled || mode == "off") return;

            var importer = assetImporter as AudioImporter;
            if (importer == null) return;

            long bytes = 0;
            try
            {
                var info = new FileInfo(assetPath);
                if (info.Exists) bytes = info.Length;
            }
            catch (IOException)
            {
                // Unreadable size: fall through to the short-clip branch, the safe default.
            }

            AudioImporterSampleSettings settings = importer.defaultSampleSettings;
            settings.compressionFormat = AudioCompressionFormat.Vorbis;
            settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;
            // Unity 6: preload moved into the per-platform sample settings; AudioImporter.preloadAudioData
            // is obsolete-with-error (CS0619).
            settings.preloadAudioData = false;

            bool asStream = mode == "streaming" || bytes > StreamingThresholdBytes;
            if (asStream)
            {
                settings.loadType = AudioClipLoadType.Streaming;
                settings.quality = 1.0f;   // the shipped stream is used as-is; nothing is re-encoded
            }
            else
            {
                settings.loadType = AudioClipLoadType.CompressedInMemory;
                settings.quality = ShortClipQuality;
            }

            importer.forceToMono = false;
            importer.loadInBackground = false;
            importer.defaultSampleSettings = settings;
        }
    }
}
