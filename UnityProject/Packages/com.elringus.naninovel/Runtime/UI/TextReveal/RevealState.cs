using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel.UI
{
    public class RevealState : IRevealInfo
    {
        public event Action OnChange;

        public int LastRevealedCharIndex { get; private set; } = -1;
        public int LastAppendIndex { get; private set; } = -1;

        private readonly Dictionary<int, CharRevealInfo> indexToInfo = new();
        private readonly Dictionary<int, float> indexToAppendTime = new();

        public void SetLast (int charIndex, float duration = 0, AsyncToken token = default)
        {
            var info = new CharRevealInfo(Engine.Time.Time, duration, token);
            for (int i = LastRevealedCharIndex; i > charIndex; i--)
                indexToInfo.Remove(i);
            for (int i = LastRevealedCharIndex + 1; i <= charIndex; i++)
                indexToInfo[i] = info;
            LastRevealedCharIndex = charIndex;
            OnChange?.Invoke();
        }

        public void SetLatAppendIndex (int charIndex)
        {
            for (int i = LastAppendIndex + 1; i <= charIndex; i++)
                indexToAppendTime[i] = Engine.Time.Time;
            LastAppendIndex = charIndex;
            OnChange?.Invoke();
        }

        public void Reset ()
        {
            indexToInfo.Clear();
            indexToAppendTime.Clear();
            LastRevealedCharIndex = -1;
            LastAppendIndex = -1;
            OnChange?.Invoke();
        }

        public float GetRevealRatio (int charIndex, float modifier = 1)
        {
            if (!indexToInfo.TryGetValue(charIndex, out var info)) return 0;
            if (info.Token.Canceled || info.Token.Completed || info.Duration <= 0) return 1;
            return Mathf.Clamp01((Engine.Time.Time - info.StartTime) / (info.Duration * modifier));
        }

        public float GetAppendTime (int charIndex)
        {
            return indexToAppendTime.GetValueOrDefault(charIndex, -1);
        }
    }
}
