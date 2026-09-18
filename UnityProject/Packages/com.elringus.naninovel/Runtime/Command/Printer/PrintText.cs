using System;
using System.Linq;
using JetBrains.Annotations;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Prints (reveals over time) specified text message using a text printer actor.",
        @"
This command is used under the hood when processing generic text lines, eg generic line `Kohaku: Hello World!` will be 
automatically transformed into `@print ""Hello World!"" author:Kohaku` when parsing the naninovel scripts.<br/>
Will reset (clear) the printer before printing the new message by default; set `reset` parameter to *false* or disable `Auto Reset` in the printer actor configuration to prevent that and append the text instead.<br/>
Will make the printer default and hide other printers by default; set `default` parameter to *false* or disable `Auto Default` in the printer actor configuration to prevent that.<br/>
Will wait for user input before finishing the task by default; set `waitInput` parameter to *false* or disable `Auto Wait` in the printer actor configuration to return as soon as the text is fully revealed.",
        @"
; Will print the phrase with a default printer.
@print ""Lorem ipsum dolor sit amet.""",
        @"
; To include quotes in the text itself, escape them.
@print ""Shouting \""Stop the car!\"" was a mistake.""",
        @"
; Reveal message with half of the normal speed and
; don't wait for user input to continue.
@print ""Lorem ipsum dolor sit amet."" speed:0.5 !waitInput",
        @"
; Print the line with ""Together"" displayed as author name and
; make all visible characters author of the printed text.
@print ""Hello World!"" author:* as:""Together""",
        @"
; Similar, but make only ""Kohaku"" and ""Yuko"" the authors.
@print ""Hello World!"" author:Kohaku,Yuko as:""Kohaku and Yuko"""
    )]
    [CommandAlias("print")]
    public class PrintText : PrinterCommand, Command.IPreloadable, Command.ILocalizable
    {
        [Doc("Text of the message to print. When the text contain spaces, wrap it in double quotes (`\"`). " +
             "In case you wish to include the double quotes in the text itself, escape them.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public LocalizableTextParameter Text;
        [Doc("ID of the printer actor to use. Will use a default one when not specified.")]
        [ParameterAlias("printer"), ActorContext(TextPrintersConfiguration.DefaultPathPrefix)]
        public StringParameter PrinterId;
        [Doc("ID of the actor, which should be associated with the printed message. Ignored when appending. " +
             "Specify `*` or use `,` to delimit multiple actor IDs to make all/selected characters authors of the text; " +
             "useful when coupled with `as` parameter to represent multiple characters speaking at the same time.")]
        [ParameterAlias("author"), ActorContext(CharactersConfiguration.DefaultPathPrefix)]
        public StringParameter AuthorId;
        [Doc("When specified, will use the label instead of author ID (or associated display name) " +
             "to represent author name in the text printer while printing the message. Useful to " +
             "override default name for a few messages or represent multiple authors speaking at the same time " +
             "without triggering author-specific behaviour of the text printer, such as message color or avatar.")]
        [ParameterAlias("as")]
        public LocalizableTextParameter AuthorLabel;
        [Doc("Text reveal speed multiplier; should be positive or zero. Setting to one will yield the default speed.")]
        [ParameterAlias("speed"), ParameterDefaultValue("1")]
        public DecimalParameter RevealSpeed = 1f;
        [Doc("Whether to reset text of the printer before executing the printing task. " +
             "Default value is controlled via `Auto Reset` property in the printer actor configuration menu.")]
        [ParameterAlias("reset")]
        public BooleanParameter ResetPrinter;
        [Doc("Whether to make the printer default and hide other printers before executing the printing task. " +
             "Default value is controlled via `Auto Default` property in the printer actor configuration menu.")]
        [ParameterAlias("default")]
        public BooleanParameter DefaultPrinter;
        [Doc("Whether to wait for user input after finishing the printing task. " +
             "Default value is controlled via `Auto Wait` property in the printer actor configuration menu.")]
        [ParameterAlias("waitInput")]
        public BooleanParameter WaitForInput;
        [Doc("Whether to append the printed text to the last printer message.")]
        public BooleanParameter Append;
        [Doc("Controls duration (in seconds) of the printers show and hide animations associated with this command. " +
             "Default value for each printer is set in the actor configuration.")]
        [ParameterAlias("fadeTime")]
        public DecimalParameter ChangeVisibilityDuration;
        [Doc("Whether to await the text reveal and prompt for completion (wait for input) before playing next command.")]
        public BooleanParameter Wait;

        protected override string AssignedPrinterId => PrinterId;
        protected override string AssignedAuthorId => AuthorId;
        protected virtual float AssignedRevealSpeed => RevealSpeed;
        protected virtual string AutoVoicePath { get; set; }
        protected IAudioManager AudioManager => Engine.GetServiceOrErr<IAudioManager>();
        protected IScriptPlayer ScriptPlayer => Engine.GetServiceOrErr<IScriptPlayer>();
        protected ITextLocalizer TextLocalizer => Engine.GetServiceOrErr<ITextLocalizer>();
        protected CharacterMetadata AuthorMeta => CharacterManager.Configuration.GetMetadataOrDefault(AssignedAuthorId);

        public override async UniTask PreloadResources ()
        {
            await base.PreloadResources();

            await PreloadStaticTextResources(Text);

            if (AudioManager.Configuration.EnableAutoVoicing && !string.IsNullOrEmpty(AutoVoicePath = BuildAutoVoicePath()))
                await AudioManager.VoiceLoader.Load(AutoVoicePath, this);

            if (Assigned(AuthorId) && !AuthorId.DynamicValue && !string.IsNullOrEmpty(AuthorMeta.MessageSound))
                await AudioManager.AudioLoader.Load(AuthorMeta.MessageSound, this);
        }

        public override void ReleaseResources ()
        {
            base.ReleaseResources();

            ReleaseStaticTextResources(Text);

            if (!string.IsNullOrEmpty(AutoVoicePath))
                AudioManager?.VoiceLoader?.Release(AutoVoicePath, this);

            if (Assigned(AuthorId) && !AuthorId.DynamicValue && !string.IsNullOrEmpty(AuthorMeta.MessageSound))
                AudioManager?.AudioLoader?.Release(AuthorMeta.MessageSound, this);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            var printer = await GetOrAddPrinter(token);
            await WaitOrForget(token => Print(printer, token), Wait, token);
        }

        protected virtual async UniTask Print (ITextPrinterActor printer, AsyncToken token)
        {
            var meta = PrinterManager.Configuration.GetMetadataOrDefault(printer.Id);
            var resetText = ShouldResetText(meta);
            if (resetText) ResetText(printer);

            if (!printer.Visible)
                await ShowPrinter(printer, meta, token);

            if (ShouldSetDefaultPrinter(meta))
                SetDefaultPrinter(printer, token);

            if (meta.StopVoice) AudioManager.StopVoice();

            if (ShouldPlayAutoVoice())
                await PlayAutoVoice(printer, token);

            // Copy to a temp var to prevent multiple evaluations of dynamic values.
            var printedText = Text.Value;
            if (printedText.IsEmpty) return;

            using var _ = await LoadDynamicTextResources((Text, printedText));

            await Print(printedText, printer, token);

            for (int i = 0; i < meta.PrintFrameDelay; i++)
                await AsyncUtils.WaitEndOfFrame(token);

            if (ShouldWaitForInput(meta, token))
                await WaitInput(printedText, token);
            else
            {
                if (IsPlayingAutoVoice()) await WaitAutoVoice(token);
                if (ShouldAllowRollbackWhenInputNotAwaited(token))
                    Engine.GetService<IStateManager>()?.PeekRollbackStack()?.AllowPlayerRollback();
            }

            if (meta.AddToBacklog)
                AddBacklog(printedText, meta);
        }

        protected virtual bool ShouldResetText (TextPrinterMetadata meta)
        {
            return Assigned(ResetPrinter) && ResetPrinter.Value || !Assigned(ResetPrinter) && meta.AutoReset;
        }

        protected virtual void ResetText (ITextPrinterActor printer)
        {
            printer.Messages = Array.Empty<PrintedMessage>();
            printer.RevealProgress = 0f;
        }

        [CanBeNull]
        protected virtual string BuildAutoVoicePath ()
        {
            return AutoVoiceResolver.Resolve(Text);
        }

        protected virtual async UniTask ShowPrinter (ITextPrinterActor printer, TextPrinterMetadata meta, AsyncToken token)
        {
            var showDuration = Assigned(ChangeVisibilityDuration) ? ChangeVisibilityDuration.Value : meta.ChangeVisibilityDuration;
            var showTask = printer.ChangeVisibility(true, new(showDuration), token: token);
            if (meta.WaitVisibilityBeforePrint) await showTask;
            else showTask.Forget();
        }

        protected virtual bool ShouldSetDefaultPrinter (TextPrinterMetadata meta)
        {
            return Assigned(DefaultPrinter) && DefaultPrinter.Value || !Assigned(DefaultPrinter) && meta.AutoDefault;
        }

        protected virtual void SetDefaultPrinter (ITextPrinterActor defaultPrinter, AsyncToken token)
        {
            if (PrinterManager.DefaultPrinterId != defaultPrinter.Id)
                PrinterManager.DefaultPrinterId = defaultPrinter.Id;

            foreach (var printer in PrinterManager.Actors)
                if (printer.Id != defaultPrinter.Id && printer.Visible)
                    HideOtherPrinter(printer);

            void HideOtherPrinter (ITextPrinterActor other)
            {
                var otherMeta = PrinterManager.Configuration.GetMetadataOrDefault(other.Id);
                var otherHideDuration = Assigned(ChangeVisibilityDuration) ? ChangeVisibilityDuration.Value : otherMeta.ChangeVisibilityDuration;
                other.ChangeVisibility(false, new(otherHideDuration), token: token).Forget();
            }
        }

        protected virtual bool ShouldPlayAutoVoice ()
        {
            return AudioManager.Configuration.EnableAutoVoicing &&
                   !string.IsNullOrEmpty(PlaybackSpot.ScriptPath) &&
                   !ScriptPlayer.SkipActive;
        }

        protected virtual async UniTask PlayAutoVoice (ITextPrinterActor printer, AsyncToken token)
        {
            if (string.IsNullOrEmpty(AutoVoicePath)) AutoVoicePath = BuildAutoVoicePath();
            if (string.IsNullOrEmpty(AutoVoicePath)) return;
            if (!await AudioManager.VoiceLoader.Exists(AutoVoicePath)) return;
            var playedVoicePath = AudioManager.GetPlayedVoice();
            if (AudioManager.Configuration.VoiceOverlapPolicy == VoiceOverlapPolicy.PreventCharacterOverlap &&
                printer.Messages.LastOrDefault().Author?.Id == AssignedAuthorId && !string.IsNullOrEmpty(playedVoicePath))
                AudioManager.StopVoice();
            await AudioManager.PlayVoice(AutoVoicePath, authorId: AssignedAuthorId, token: token);
        }

        protected virtual UniTask Print (LocalizableText text, ITextPrinterActor printer, AsyncToken token)
        {
            var message = new PrintedMessage(text, new(AssignedAuthorId, AuthorLabel));
            return PrinterManager.Print(printer.Id, message, Append, AssignedRevealSpeed, token);
        }

        protected virtual bool ShouldWaitForInput (TextPrinterMetadata meta, AsyncToken token)
        {
            if (token.Completed && !meta.WaitAfterRevealSkip) return false;
            if (Assigned(WaitForInput)) return WaitForInput.Value;
            return meta.AutoWait;
        }

        protected virtual bool ShouldAllowRollbackWhenInputNotAwaited (AsyncToken token)
        {
            // Required for rollback to work when WaitAfterRevealSkip is disabled.
            return !(Assigned(WaitForInput) && !WaitForInput) && token.Completed;
        }

        protected virtual async UniTask WaitInput (LocalizableText text, AsyncToken token)
        {
            if (ScriptPlayer.AutoPlayActive)
                await WaitAutoPlayDelay(text, token);
            ScriptPlayer.SetWaitingForInputEnabled(true);
        }

        protected virtual void AddBacklog (LocalizableText text, TextPrinterMetadata meta)
        {
            var backlogUI = Engine.GetServiceOrErr<IUIManager>().GetUI<IBacklogUI>();
            if (backlogUI is null) return;
            var voicePath = !string.IsNullOrEmpty(AutoVoicePath) && AudioManager.VoiceLoader.IsLoaded(AutoVoicePath) ? AutoVoicePath : null;
            if (Append) backlogUI.AppendMessage(text, voicePath);
            else backlogUI.AddMessage(text, AssignedAuthorId, PlaybackSpot, voicePath);
        }

        protected virtual async UniTask WaitAutoVoice (AsyncToken token)
        {
            while (IsPlayingAutoVoice() && token.EnsureNotCanceledOrCompleted())
                await AsyncUtils.WaitEndOfFrame();
        }

        protected virtual async UniTask WaitAutoPlayDelay (LocalizableText text, AsyncToken token)
        {
            var baseDelay = Configuration.ScaleAutoWait ? PrinterManager.BaseAutoDelay * AssignedRevealSpeed : PrinterManager.BaseAutoDelay;
            var textLength = TextLocalizer.Resolve(text).Count(char.IsLetterOrDigit);
            var autoPlayDelay = Mathf.Lerp(0, Configuration.MaxAutoWaitDelay, baseDelay) * textLength;
            var waitUntilTime = Engine.Time.Time + autoPlayDelay;
            while ((Engine.Time.Time < waitUntilTime || IsPlayingAutoVoice()) && token.EnsureNotCanceledOrCompleted())
                await AsyncUtils.WaitEndOfFrame();
        }

        protected virtual bool IsPlayingAutoVoice ()
        {
            return ShouldPlayAutoVoice() && AudioManager.GetPlayedVoice() == AutoVoicePath;
        }
    }
}
