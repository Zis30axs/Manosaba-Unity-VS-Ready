using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Naninovel
{
    /// <summary>
    /// Draws Unity's layer mask list.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerMaskAttribute))]
    public class LayerMaskDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect rect, SerializedProperty prop, GUIContent label)
        {
            var oldMask = FieldToMask(prop.intValue);
            var newMask = EditorGUI.MaskField(rect, label, oldMask, InternalEditorUtility.layers);
            prop.intValue = MaskToField(newMask);
        }

        private static int FieldToMask (int field)
        {
            if (field == -1) return -1;
            var mask = 0;
            var layers = InternalEditorUtility.layers;
            for (var i = 0; i < layers.Length; i++)
                if ((field & (1 << LayerMask.NameToLayer(layers[i]))) != 0)
                    mask |= 1 << i;
            return mask;
        }

        private static int MaskToField (int mask)
        {
            if (mask == -1) return -1;
            var field = 0;
            var layers = InternalEditorUtility.layers;
            for (var i = 0; i < layers.Length; i++)
                if ((mask & (1 << i)) != 0)
                    field |= 1 << LayerMask.NameToLayer(layers[i]);
                else field &= ~(1 << LayerMask.NameToLayer(layers[i]));
            return field;
        }
    }
}
