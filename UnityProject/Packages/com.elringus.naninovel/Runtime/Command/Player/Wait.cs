using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Holds script execution until the specified wait condition.",
        null,
        @"
; Thunder SFX will play 0.5 seconds after shake background effect finishes.
@spawn ShakeBackground
@wait 0.5
@sfx Thunder",
        @"
; Print first 2 words, then wait for input before printing the rest.
Lorem ipsum[wait i] dolor sit amet.
; You can also use the following shortcut (@i command) for this wait mode.
Lorem ipsum[i] dolor sit amet.",
        @"
; Start looped SFX, print message and wait for a skippable 5 seconds delay,
; then stop the SFX.
@sfx Noise loop!
Jeez, what a disgusting Noise. Shut it down![wait i5][< skip!]
@stopSfx Noise"
    )]
    public class Wait : Command
    {
        /// <summary>
        /// Literal used to indicate "wait-for-input" mode.
        /// </summary>
        public const string InputLiteral = "i";

        [Doc("Wait conditions:<br/>" +
             " - `i` user press continue or skip input key;<br/>" +
             " - `0.0` timer (seconds);<br/>" +
             " - `i0.0` timer, that is skip-able by continue or skip input keys.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringParameter WaitMode;

        public override async UniTask Execute (AsyncToken token = default)
        {
            // Don't just return here if skip is enabled; state snapshot is marked as allowed for player rollback when setting waiting for input.

            // Always wait for at least a frame; otherwise skip-able timer (eg, @wait i3) may not behave correctly
            // when used before/after a generic text line: https://forum.naninovel.com/viewtopic.php?p=156#p156
            await AsyncUtils.WaitEndOfFrame(token);

            if (!Assigned(WaitMode))
            {
                Warn($"'{nameof(WaitMode)}' parameter is not specified, the wait command will do nothing.");
                return;
            }

            var waitMode = WaitMode.Value;
            if (waitMode.EqualsFastIgnoreCase(InputLiteral))
                await WaitForInput(token);
            else if (waitMode.StartsWithFast(InputLiteral) && ParseUtils.TryInvariantFloat(waitMode.GetAfterFirst(InputLiteral), out var waitTime))
                await WaitForInputOrTimer(waitTime, token);
            else if (ParseUtils.TryInvariantFloat(waitMode, out waitTime))
                await WaitForTimer(waitTime, token);
            else Warn($"Failed to resolve value of the '{nameof(WaitMode)}' parameter for the wait command. Check the API reference for list of supported values.");
        }

        protected virtual async UniTask WaitForInput (AsyncToken token)
        {
            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            player.SetWaitingForInputEnabled(true);
            while (Application.isPlaying && token.EnsureNotCanceledOrCompleted())
            {
                await AsyncUtils.WaitEndOfFrame(token);
                if (!player.WaitingForInput || player.AutoPlayActive) break;
            }
        }

        protected virtual async UniTask WaitForInputOrTimer (float waitTime, AsyncToken token)
        {
            using var _ = CompleteOnContinue(ref token);
            await WaitForTimer(waitTime, token);
        }

        protected virtual async UniTask WaitForTimer (float waitTime, AsyncToken token)
        {
            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            if (player.SkipActive) return;

            var startTime = Engine.Time.Time;
            while (Application.isPlaying && !player.Completing && token.EnsureNotCanceledOrCompleted())
            {
                await AsyncUtils.WaitEndOfFrame(token);
                var waitedEnough = Engine.Time.Time - startTime >= waitTime;
                if (waitedEnough) break;
            }
        }
    }
}
