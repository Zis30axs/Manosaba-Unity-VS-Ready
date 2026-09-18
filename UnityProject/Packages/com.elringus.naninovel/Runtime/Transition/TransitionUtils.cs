using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    public static class TransitionUtils
    {
        public static readonly string DefaultTransition = TransitionType.Crossfade.ToString();

        private const string keywordPrefix = "NANINOVEL_TRANSITION_";
        private const string crossfade = "Crossfade";

        private static readonly Dictionary<string, Vector4> nameToDefaultParamsMap = new(StringComparer.OrdinalIgnoreCase) {
            [TransitionType.Crossfade.ToString()] = Vector4.zero,
            [TransitionType.BandedSwirl.ToString()] = new(5, 10),
            [TransitionType.Blinds.ToString()] = new(6, 0),
            [TransitionType.CircleReveal.ToString()] = new(.25f, 0),
            [TransitionType.CircleStretch.ToString()] = Vector4.zero,
            [TransitionType.CloudReveal.ToString()] = Vector4.zero,
            [TransitionType.Crumble.ToString()] = Vector4.zero,
            [TransitionType.Dissolve.ToString()] = new(99999, 0),
            [TransitionType.DropFade.ToString()] = Vector4.zero,
            [TransitionType.LineReveal.ToString()] = new(.025f, .5f, .5f, 0),
            [TransitionType.Pixelate.ToString()] = Vector4.zero,
            [TransitionType.RadialBlur.ToString()] = Vector4.zero,
            [TransitionType.RadialWiggle.ToString()] = Vector4.zero,
            [TransitionType.RandomCircleReveal.ToString()] = Vector4.zero,
            [TransitionType.Ripple.ToString()] = new(20f, 10f, .05f),
            [TransitionType.RotateCrumble.ToString()] = Vector4.zero,
            [TransitionType.Saturate.ToString()] = Vector4.zero,
            [TransitionType.Shrink.ToString()] = new(200, 0),
            [TransitionType.SlideIn.ToString()] = new(1, 0),
            [TransitionType.SwirlGrid.ToString()] = new(15, 10),
            [TransitionType.Swirl.ToString()] = new(15, 0),
            [TransitionType.Water.ToString()] = Vector4.zero,
            [TransitionType.Waterfall.ToString()] = Vector4.zero,
            [TransitionType.Wave.ToString()] = new(.1f, 14, 20),
            [TransitionType.Custom.ToString()] = new(0, 0)
        };

        /// <summary>
        /// Resolve transition name from command parameter value.
        /// When not assigned, will return default transition.
        /// Will consider localization via <see cref="Compiler"/>.
        /// </summary>
        public static string ResolveParameterValue (StringParameter param)
        {
            if (!Command.Assigned(param)) return DefaultTransition;
            var value = param.Value;
            if (Compiler.Constants.TryGetValue(nameof(TransitionType), out var l10n))
                if (l10n.Values.FirstOrDefault(v => v.Alias.EqualsFastIgnoreCase(value)) is var vn)
                    if (!string.IsNullOrWhiteSpace(vn.Alias))
                        value = vn.Value;
            return value;
        }

        /// <summary>
        /// Converts specified transition name to corresponding shader keyword.
        /// Transition effect names are case-insensitive.
        /// </summary>
        public static string ToShaderKeyword (string transition)
        {
            return string.Concat(keywordPrefix, transition.ToUpperInvariant());
        }

        /// <summary>
        /// Attempts to find default transition parameters for transition effect with the specified name;
        /// returns <see cref="Vector4.zero"/> when not found. Transition effect names are case-insensitive.
        /// </summary>
        public static Vector4 GetDefaultParams (string transition)
        {
            return nameToDefaultParamsMap.TryGetValue(transition, out var result) ? result : Vector4.zero;
        }

        /// <summary>
        /// Attempts to find which transition effect is currently enabled in the specified material by checking enabled keywords;
        /// returns <see cref="TransitionType.Crossfade"/> when no transition keyword is enabled or found.
        /// </summary>
        public static string GetEnabled (Material material)
        {
            for (int i = 0; i < material.shaderKeywords.Length; i++)
                if (material.shaderKeywords[i].StartsWith(keywordPrefix) && material.IsKeywordEnabled(material.shaderKeywords[i]))
                    return material.shaderKeywords[i].GetAfter(keywordPrefix);
            return crossfade; // Crossfade is executed by default when no keywords enabled.
        }

        /// <summary>
        /// Enables a shader keyword corresponding to transition effect with the specified name in the specified material.
        /// Transition effect names are case-insensitive.
        /// </summary>
        public static void EnableKeyword (Material material, string transition)
        {
            for (int i = 0; i < material.shaderKeywords.Length; i++)
                if (material.shaderKeywords[i].StartsWith(keywordPrefix) && material.IsKeywordEnabled(material.shaderKeywords[i]))
                    material.DisableKeyword(material.shaderKeywords[i]);

            // Crossfade is executed when no transition keywords enabled.
            if (transition.EqualsFastIgnoreCase(crossfade)) return;

            var keyword = ToShaderKeyword(transition);
            material.EnableKeyword(keyword);
        }
    }
}
