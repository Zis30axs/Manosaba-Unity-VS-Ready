using UnityEngine;

namespace Naninovel
{
    public static class WebUtils
    {
        public static AudioType EvaluateAudioTypeFromMime (string mimeType) => mimeType switch {
            "audio/aiff" => AudioType.AIFF,
            "audio/mpeg" => AudioType.MPEG,
            "audio/mpeg3" => AudioType.MPEG,
            "audio/mp3" => AudioType.MPEG,
            "audio/ogg" => AudioType.OGGVORBIS,
            "video/ogg" => AudioType.OGGVORBIS,
            "audio/wav" => AudioType.WAV,
            _ => AudioType.UNKNOWN
        };

        /// <summary>
        /// Navigates to the specified URL using default or current web browser.
        /// </summary>
        /// <remarks>
        /// When used outside of WebGL or in editor will use <see cref="Application.OpenURL"/>,
        /// otherwise native window.open() JS function is used.
        /// </remarks>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="target">Browsing context: _self, _blank, _parent, _top. Not supported outside of WebGL.</param>
        public static void OpenURL (string url, string target = "_self")
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            WebGLExtensions.OpenURL(url, target);
            #else
            Application.OpenURL(url);
            #endif
        }
    }
}
