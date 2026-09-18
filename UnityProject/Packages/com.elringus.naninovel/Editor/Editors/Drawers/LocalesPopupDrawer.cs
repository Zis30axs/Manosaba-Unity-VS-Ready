using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Draws a selectable dropdown list (popup) of available locales (language tags) based on <see cref="Languages"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(LocalesPopupAttribute))]
    public class LocalesPopupDrawer : PropertyDrawer
    {
        private const string emptyOptionValue = ResourcePopupAttribute.EmptyValue;
        private static readonly GUIContent emptyOption = new(emptyOptionValue);
        private static readonly string[] values, valuesWithEmpty;
        private static readonly GUIContent[] options, optionsWithEmpty;

        static LocalesPopupDrawer ()
        {
            var all = Languages.GetAll();
            values = all.Select(l => l.Tag).ToArray();
            valuesWithEmpty = all.Select(l => l.Tag).ToArray();
            ArrayUtils.Insert(ref valuesWithEmpty, 0, emptyOptionValue);

            var optionsList = new List<GUIContent>();
            foreach (var lang in all)
            {
                var option = new GUIContent($"{lang.Name} ({lang.Tag})");
                optionsList.Add(option);
            }
            options = optionsList.ToArray();
            optionsList.Insert(0, emptyOption);
            optionsWithEmpty = optionsList.ToArray();
        }

        public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
        {
            var includeEmpty = attribute is LocalesPopupAttribute attr && attr.IncludeEmpty;
            Draw(rect, property, includeEmpty);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
        }

        /// <param name="property">The property for which to assign value of the selected element.</param>
        /// <param name="includeEmpty">Whether to include an empty ('None') option to the list.</param>
        public static void Draw (SerializedProperty property, bool includeEmpty = false)
        {
            Draw(EditorGUILayout.GetControlRect(), property, includeEmpty);
        }

        /// <param name="curValue">The current value the selected element.</param>
        /// <param name="label">The label to use for the popup field.</param>
        /// <param name="includeEmpty">Whether to include an empty ('None') option to the list.</param>
        public static string Draw (string curValue, GUIContent label = default, bool includeEmpty = false)
        {
            return Draw(EditorGUILayout.GetControlRect(), curValue, label, includeEmpty);
        }

        public static void Draw (Rect rect, SerializedProperty property, bool includeEmpty = false)
        {
            var optionsArray = includeEmpty ? optionsWithEmpty : options;
            var valuesArray = includeEmpty ? valuesWithEmpty : values;

            var curValue = includeEmpty && string.IsNullOrEmpty(property.stringValue) ? emptyOptionValue : property.stringValue;
            var label = EditorGUI.BeginProperty(Rect.zero, null, property);
            var curIndex = ArrayUtility.IndexOf(valuesArray, curValue);
            var newIndex = EditorGUI.Popup(rect, label, curIndex, optionsArray);

            var newValue = valuesArray.IsIndexValid(newIndex) ? valuesArray[newIndex] : valuesArray[0];
            if (includeEmpty && newValue == emptyOptionValue)
                newValue = string.Empty;

            if (property.stringValue != newValue)
                property.stringValue = newValue;
        }

        public static string Draw (Rect rect, string curValue, GUIContent label = default, bool includeEmpty = false)
        {
            var optionsArray = includeEmpty ? optionsWithEmpty : options;
            var valuesArray = includeEmpty ? valuesWithEmpty : values;

            curValue = includeEmpty && string.IsNullOrEmpty(curValue) ? emptyOptionValue : curValue;
            var curIndex = ArrayUtility.IndexOf(valuesArray, curValue);
            var newIndex = EditorGUI.Popup(rect, label, curIndex, optionsArray);

            var newValue = valuesArray.IsIndexValid(newIndex) ? valuesArray[newIndex] : valuesArray[0];
            if (includeEmpty && newValue == emptyOptionValue)
                newValue = string.Empty;

            return newValue;
        }
    }
}
