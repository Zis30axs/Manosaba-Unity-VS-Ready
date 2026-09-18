using System.Linq;
using System.Threading;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel
{
    public class GameSettingsPreviewPrinter : ScriptableUIBehaviour
    {
        protected virtual IRevealableText RevealableText { get; private set; }

        [Tooltip("A component implementing `" + nameof(IRevealableText) + "` interface for displaying the preview text.")]
        [SerializeField] private Graphic revealableText;

        private TextRevealer revealer;
        private CancellationTokenSource revealCTS;
        private ITextPrinterManager printMng;
        private IScriptPlayer player;

        public virtual void StartPrinting ()
        {
            revealCTS?.Cancel();

            RevealableText.RevealProgress = 0;
            if (RevealableText is Graphic graphic)
                graphic.Rebuild(CanvasUpdate.PreRender); // Otherwise it's not displaying anything.

            var revealDelay = Mathf.Lerp(printMng.Configuration.MaxRevealDelay, 0, printMng.BaseRevealSpeed);
            if (revealDelay == 0)
                RevealableText.RevealProgress = 1;
            else
            {
                revealCTS = new();
                RevealTextOverTime(revealDelay, revealCTS.Token).Forget();
            }
        }

        protected override void Awake ()
        {
            base.Awake();

            RevealableText = revealableText as IRevealableText;
            if (RevealableText is null)
                throw new Error($"Field `{nameof(revealableText)}` on `{nameof(GameSettingsPreviewPrinter)}` component is either not assigned or doesn't implement `{nameof(IRevealableText)}` interface.");
            revealer = new(RevealableText);
            printMng = Engine.GetServiceOrErr<ITextPrinterManager>();
            player = Engine.GetServiceOrErr<IScriptPlayer>();
            GetComponentInParent<IManagedUI>().OnVisibilityChanged += HandleRootVisibilityChanged;
        }

        protected virtual async UniTask RevealTextOverTime (float revealDelay, CancellationToken token)
        {
            await revealer.Reveal(revealDelay, token);
            if (token.IsCancellationRequested) return;
            var waitUntilTime = Engine.Time.UnscaledTime + GetDelayForText(RevealableText.Text);
            while (Engine.Time.UnscaledTime < waitUntilTime)
                await AsyncUtils.WaitEndOfFrame(token);
            if (!token.IsCancellationRequested) StartPrinting();
        }

        protected virtual void HandleRootVisibilityChanged (bool visible)
        {
            if (visible) StartPrinting();
            else revealCTS?.Cancel();
        }

        protected virtual float GetDelayForText (string text)
        {
            var count = text.Count(char.IsLetterOrDigit);
            var delay = Mathf.Lerp(0, printMng.Configuration.MaxAutoWaitDelay, printMng.BaseAutoDelay) * count;
            if (delay < player.Configuration.MinAutoPlayDelay)
                return player.Configuration.MinAutoPlayDelay;
            return delay;
        }
    }
}
