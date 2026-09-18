using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// A text reveal effect that fades-in revealing characters.
    /// </summary>
    public class RevealFader : TextRevealEffect
    {
        [Tooltip("How long to stretch fade gradient, by character."), Range(0, 100)]
        [SerializeField] private float length = 10;
        [Tooltip("When below 1, will modify opacity of the text before the last character from which reveal started."), Range(0, 1)]
        [SerializeField] private float slackOpacity = 1;
        [Tooltip("Duration (in seconds) of fading slack text to the target opacity."), Range(0, 3)]
        [SerializeField] private float slackDuration = 0.5f;
        [Tooltip("Whether to respect <alpha> tags or otherwise custom opacity levels assigned to specific text characters. When enabled, text reveal effect will limit max opacity to the specified level. Be aware, that this involves additional work inside the reveal loop and affects performance.")]
        [SerializeField] private bool supportCustomOpacity;

        private readonly Dictionary<int, byte> customOpacity = new();

        private void OnEnable ()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(HandleTextChanged);
            Info.OnChange += Update;
        }

        private void OnDisable ()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(HandleTextChanged);
            if (Text) Info.OnChange -= Update;
        }

        private void Update ()
        {
            if (Text.textInfo.characterCount == 0) return;
            for (int i = 0; i < Text.textInfo.characterCount; i++)
                FadeCharacter(Text.textInfo.characterInfo[i], EvaluateOpacity(i));
            Text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        private void FadeCharacter (TMP_CharacterInfo info, byte opacity)
        {
            if (!info.isVisible) return;
            var colors = Text.textInfo.meshInfo[info.materialReferenceIndex].colors32;
            for (int i = 0; i < 4; i++)
                colors[info.vertexIndex + i].a = opacity;
        }

        private byte EvaluateOpacity (int charIndex)
        {
            var max = supportCustomOpacity && customOpacity.TryGetValue(charIndex, out var custom) ? custom : byte.MaxValue;
            if (!IsSlack(charIndex)) return (byte)(Info.GetRevealRatio(charIndex, length) * max);
            return (byte)(max - Mathf.Clamp01((Engine.Time.Time - Info.GetAppendTime(charIndex)) / slackDuration) * (1f - slackOpacity) * max);
        }

        private bool IsSlack (int charIndex)
        {
            return slackOpacity < 1 && charIndex <= Info.LastAppendIndex;
        }

        private void HandleTextChanged (Object obj)
        {
            if (obj != Text) return;
            if (supportCustomOpacity) ResolveCustomOpacity();
            Update();
        }

        private void ResolveCustomOpacity ()
        {
            customOpacity.Clear();
            for (int i = 0; i < Text.textInfo.characterCount; i++)
            {
                var charInfo = Text.textInfo.characterInfo[i];
                var meshInfo = Text.textInfo.meshInfo[charInfo.materialReferenceIndex];
                var color = meshInfo.colors32[charInfo.vertexIndex];
                customOpacity[i] = color.a;
            }
        }
    }
}
