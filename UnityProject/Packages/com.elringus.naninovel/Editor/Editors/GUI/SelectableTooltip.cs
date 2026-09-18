using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public static class SelectableTooltip
    {
        public static void Draw (string message, string tooltip = default)
        {
            EditorGUILayout.Space();
            var rect = GUILayoutUtility.GetRect(new(message), GUIStyles.ResourceTooltipStyle);
            try { EditorGUI.SelectableLabel(rect, message, GUIStyles.ResourceTooltipStyle); }
            catch { } // Throws when clicking/selecting the label.
            if (tooltip != null) EditorGUI.LabelField(rect, new GUIContent("", tooltip));
            EditorGUILayout.Space();
        }
    }
}
