using System;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ITextPrinterManager"/>
    [InitializeAtRuntime]
    public class TextPrinterManager : OrthoActorManager<ITextPrinterActor, TextPrinterState,
        TextPrinterMetadata, TextPrintersConfiguration>, IStatefulService<SettingsStateMap>, ITextPrinterManager
    {
        [Serializable]
        public class Settings
        {
            public float BaseRevealSpeed = .5f;
            public float BaseAutoDelay = .5f;
        }

        [Serializable]
        public new class GameState
        {
            public string DefaultPrinterId;
        }

        public event Action<PrintMessageArgs> OnPrintStarted;
        public event Action<PrintMessageArgs> OnPrintFinished;

        public virtual string DefaultPrinterId { get; set; }
        public virtual float BaseRevealSpeed { get; set; }
        public virtual float BaseAutoDelay { get; set; }

        private readonly IScriptPlayer scriptPlayer;

        public TextPrinterManager (TextPrintersConfiguration config, CameraConfiguration cameraConfig, IScriptPlayer scriptPlayer)
            : base(config, cameraConfig)
        {
            this.scriptPlayer = scriptPlayer;
        }

        public override async UniTask InitializeService ()
        {
            await base.InitializeService();

            DefaultPrinterId = Configuration.DefaultPrinterId;
        }

        public override void ResetService ()
        {
            base.ResetService();
            DefaultPrinterId = Configuration.DefaultPrinterId;
        }

        public virtual void SaveServiceState (SettingsStateMap stateMap)
        {
            var settings = new Settings {
                BaseRevealSpeed = BaseRevealSpeed,
                BaseAutoDelay = BaseAutoDelay
            };
            stateMap.SetState(settings);
        }

        public virtual UniTask LoadServiceState (SettingsStateMap stateMap)
        {
            var settings = stateMap.GetState<Settings>();

            if (settings is null) // Apply default settings.
            {
                BaseRevealSpeed = Configuration.DefaultBaseRevealSpeed;
                BaseAutoDelay = Configuration.DefaultBaseAutoDelay;
                return UniTask.CompletedTask;
            }

            BaseRevealSpeed = settings.BaseRevealSpeed;
            BaseAutoDelay = settings.BaseAutoDelay;
            return UniTask.CompletedTask;
        }

        public override void SaveServiceState (GameStateMap stateMap)
        {
            base.SaveServiceState(stateMap);

            var gameState = new GameState {
                DefaultPrinterId = DefaultPrinterId ?? Configuration.DefaultPrinterId
            };
            stateMap.SetState(gameState);
        }

        public override async UniTask LoadServiceState (GameStateMap stateMap)
        {
            await base.LoadServiceState(stateMap);

            var state = stateMap.GetState<GameState>() ?? new GameState();
            DefaultPrinterId = state.DefaultPrinterId ?? Configuration.DefaultPrinterId;
        }

        public virtual async UniTask Print (string printerId, PrintedMessage message,
            bool append = false, float speed = 1, AsyncToken token = default)
        {
            var printer = await GetOrAddActor(printerId);
            token.ThrowIfCanceled();
            var meta = Configuration.GetMetadataOrDefault(printerId);

            OnPrintStarted?.Invoke(new(printer, message, append, speed));

            if (append) printer.AppendText(message.Text);
            else printer.AddMessage(message);

            var delay = (meta.RevealInstantly || scriptPlayer.SkipActive) ? 0
                : Mathf.Lerp(Configuration.MaxRevealDelay, 0, BaseRevealSpeed * speed);
            await printer.Reveal(delay, token);

            OnPrintFinished?.Invoke(new(printer, message, append, speed));
        }
    }
}
