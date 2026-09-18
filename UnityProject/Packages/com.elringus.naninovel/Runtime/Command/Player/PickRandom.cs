using Naninovel.Metadata;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Executes one of the nested commands, picked randomly.",
        null,
        @"
; Play one of 3 sounds with equal probability.
@random
    @sfx Sound1
    @sfx Sound2
    @sfx Sound3",
        @"
; Play 2nd sound with 80% probability or 1st/3rd with 10% each.
@random weight:0.1,0.8,0.1
    @sfx Sound1
    @sfx Sound2
    @sfx Sound3",
        @"
; Add a choice to shake camera, tint Kohaku actor or play 'SoundX' SFX,
; all with 33% probability. However, SFX playback will only be considered
; in case score is above 10.
@random
    @choice ""Shake camera!""
        You've asked for it!
        @shake Camera
    @group
        Going to tint Kohaku!
        @char Kohaku tint:red
    @sfx SoundX if:score>10
@stop"
    )]
    [CommandAlias("random"), RequireNested, Branch(BranchTraits.Nest | BranchTraits.Return)]
    public class PickRandom : Command, Command.INestedHost
    {
        [Doc("Customized probability for the nested commands, in 0.0 to 1.0 range. " +
             "By default all the commands have equal probability of being picked.")]
        public DecimalListParameter Weight;

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsEnteringNestedAt(playedIndex))
                return PickRandomNested(playlist, playedIndex);
            var exitIndex = playlist.GetNestedExitIndexAt(playedIndex, Indent);
            return playlist.ExitNestedAt(exitIndex, Indent);
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            return UniTask.CompletedTask;
        }

        protected virtual int PickRandomNested (ScriptPlaylist playlist, int hostIndex)
        {
            var maxSeed = -1f;
            var maxIndex = -1;
            var weightIndex = -1;
            for (int i = hostIndex + 1; i < playlist.Count; i++)
            {
                if (playlist[i].Indent == Indent + 1)
                {
                    var seed = Random.value * (Weight?.ElementAtOrNull(++weightIndex) ?? 1f);
                    if (seed > maxSeed && playlist[i].ShouldExecute)
                    {
                        maxSeed = seed;
                        maxIndex = i;
                    }
                }
                if (playlist.IsExitingNestedAt(i, Indent)) break;
            }
            return maxIndex == -1 ? playlist.SkipNestedAt(hostIndex + 1, Indent) : maxIndex;
        }
    }
}
