using Naninovel.Metadata;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Delays execution of the nested commands for specified time interval.",
        @"
Be aware, that the delayed execution won't happen if game gets saved/loaded
or rolled-back. It's fine to use delayed execution for ""cosmetic"" events,
such as one-shot visual or audio effects, but don't delay commands, which
could affect persistent game state, as this could lead to undefined behaviour.",
        @"
; The text is printed without delay, as the '@delay' command is not awaited.
; The Thunder effects are played after a random delay of 3 to 8 seconds.
@delay {random(3,8)}
    @sfx Thunder
    @shake Camera
The Thunder might go off any second..."
    )]
    [RequireNested, Branch(BranchTraits.Nest | BranchTraits.Return)]
    public class Delay : Command, Command.INestedHost
    {
        [Doc("Delay time, in seconds.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public DecimalParameter Seconds;

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            // Nested commands are played in transient execution context after the delay.
            if (playlist.IsEnteringNestedAt(playedIndex))
                return playlist.SkipNestedAt(playedIndex, Indent);
            throw new Error("Nested commands of @delay command should never be executed under main context. " +
                            "This could happen if you navigate to labels nested under @delay, which is not supported.");
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            var delayedList = BuildDelayedList();
            WaitDelay(Seconds, token)
                .ContinueWith(() => ExecuteDelayed(delayedList, token)).Forget();
            return UniTask.CompletedTask;
        }

        protected virtual ScriptPlaylist BuildDelayedList ()
        {
            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            var start = player.PlayedIndex + 1;
            var count = player.Playlist.GetNestedExitIndexAt(player.PlayedIndex, Indent) - start + 1;
            var commands = player.Playlist.GetRange(start, count);
            return new($"Delayed transient for {PlaybackSpot}", commands);
        }

        protected virtual async UniTask WaitDelay (float waitTime, AsyncToken token)
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

        protected virtual async UniTask ExecuteDelayed (ScriptPlaylist delayedList, AsyncToken token)
        {
            if (!token.EnsureNotCanceledOrCompleted()) return;
            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            await player.PlayTransient(delayedList, token);
        }
    }
}
