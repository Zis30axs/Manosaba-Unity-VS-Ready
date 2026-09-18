using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Stores data about played script commands.
    /// </summary>
    [System.Serializable]
    public class PlayedScriptRegister
    {
        [System.Serializable]
        private class PlayedScript
        {
            public string ScriptPath;
            public List<IntRange> PlayedIndexes;

            public PlayedScript (string scriptPath)
            {
                ScriptPath = scriptPath;
                PlayedIndexes = new();
            }

            public void AddIndex (int index)
            {
                for (int i = 0; i < PlayedIndexes.Count; i++)
                {
                    var range = PlayedIndexes[i];
                    if (range.Contains(index)) return;
                    if (range.StartIndex == (index + 1))
                    {
                        PlayedIndexes[i] = new(index, range.EndIndex);
                        return;
                    }
                    if (range.EndIndex == (index - 1))
                    {
                        PlayedIndexes[i] = new(range.StartIndex, index);
                        return;
                    }
                }
                PlayedIndexes.Add(new(index, index));
            }

            public bool ContainsIndex (int index)
            {
                for (int i = 0; i < PlayedIndexes.Count; i++)
                    if (PlayedIndexes[i].Contains(index))
                        return true;
                return false;
            }
        }

        [SerializeField] private List<PlayedScript> playedScripts = new();

        public IReadOnlyDictionary<string, IReadOnlyList<IntRange>> GetPlayed ()
        {
            return playedScripts.ToDictionary(p => p.ScriptPath, p => p.PlayedIndexes as IReadOnlyList<IntRange>);
        }

        public void RegisterPlayedIndex (string scriptPath, int playlistIndex)
        {
            if (IsIndexPlayed(scriptPath, playlistIndex)) return;
            var data = GetOrCreateDataForScript(scriptPath);
            data.AddIndex(playlistIndex);
        }

        public bool IsIndexPlayed (string scriptPath, int playlistIndex)
        {
            var data = GetOrCreateDataForScript(scriptPath);
            return data.ContainsIndex(playlistIndex);
        }

        public bool IsScriptPlayed (string scriptPath)
        {
            foreach (var script in playedScripts)
                if (script.ScriptPath == scriptPath)
                    return true;
            return false;
        }

        public int CountPlayed ()
        {
            var counter = 0;
            foreach (var script in playedScripts)
            foreach (var range in script.PlayedIndexes)
                counter += range.EndIndex - range.StartIndex + 1;
            return counter;
        }

        private PlayedScript GetOrCreateDataForScript (string scriptPath)
        {
            for (int i = 0; i < playedScripts.Count; i++)
                if (playedScripts[i].ScriptPath == scriptPath)
                    return playedScripts[i];
            var result = new PlayedScript(scriptPath);
            playedScripts.Add(result);
            return result;
        }
    }
}
