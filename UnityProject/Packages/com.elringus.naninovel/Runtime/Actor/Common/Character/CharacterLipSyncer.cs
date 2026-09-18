using System;

namespace Naninovel
{
    public class CharacterLipSyncer : IDisposable
    {
        public bool SyncAllowed { get; set; } = true;

        private readonly string authorId;
        private readonly Action<bool> setIsSpeaking;
        private readonly ITextPrinterManager textPrinterManager;
        private readonly IAudioManager audioManager;

        public CharacterLipSyncer (string authorId, Action<bool> setIsSpeaking)
        {
            this.authorId = authorId;
            this.setIsSpeaking = setIsSpeaking;
            audioManager = Engine.GetServiceOrErr<IAudioManager>();
            textPrinterManager = Engine.GetServiceOrErr<ITextPrinterManager>();
            textPrinterManager.OnPrintStarted += HandlePrintStarted;
            setIsSpeaking.Invoke(false);
        }

        public void Dispose ()
        {
            if (textPrinterManager != null)
            {
                textPrinterManager.OnPrintStarted -= HandlePrintStarted;
                textPrinterManager.OnPrintFinished -= HandlePrintFinished;
            }
        }

        private void HandlePrintStarted (PrintMessageArgs args)
        {
            if (!SyncAllowed || args.Message.Author?.Id != authorId) return;

            setIsSpeaking.Invoke(true);

            var playedVoicePath = audioManager.GetPlayedVoice();
            if (!string.IsNullOrEmpty(playedVoicePath))
            {
                var track = audioManager.GetVoiceTrack(playedVoicePath)!;
                track.OnStop -= HandleVoiceClipStopped;
                track.OnStop += HandleVoiceClipStopped;
            }
            else textPrinterManager.OnPrintFinished += HandlePrintFinished;
        }

        private void HandlePrintFinished (PrintMessageArgs args)
        {
            if (args.Message.Author?.Id != authorId) return;

            setIsSpeaking.Invoke(false);
            textPrinterManager.OnPrintFinished -= HandlePrintFinished;
        }

        private void HandleVoiceClipStopped ()
        {
            setIsSpeaking.Invoke(false);
        }
    }
}
