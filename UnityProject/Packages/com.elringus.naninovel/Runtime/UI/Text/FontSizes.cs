using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Allows storing presets of font sizes used with <see cref="CustomUI.FontChangeConfiguration"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Naninovel/Font Sizes", fileName = "NewFontSizes")]
    public class FontSizes : ScriptableObject
    {
        public int[] Sizes = { 25, -1, 40, 45 };
    }
}
