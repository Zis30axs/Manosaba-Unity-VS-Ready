using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Naninovel
{
    public class CustomVariablesSettings : ConfigurationSettings<CustomVariablesConfiguration>
    {
        private const float headerLeftMargin = 5;
        private const float paddingWidth = 10;

        private static readonly GUIContent nameContent = new("Name", "Name of the custom variable. Add `g_` prefix to make it a global variable.");
        private static readonly GUIContent valueContent = new("Value (expression)", "A script expression to initialize the variable.");

        private ReorderableList predefinedVariablesList;

        public override void OnDeactivate ()
        {
            base.OnDeactivate();
            ValidateNames();
        }

        protected override Dictionary<string, Action<SerializedProperty>> OverrideConfigurationDrawers ()
        {
            var drawers = base.OverrideConfigurationDrawers();
            drawers[nameof(CustomVariablesConfiguration.PredefinedVariables)] = DrawPredefinedVariablesEditor;
            return drawers;
        }

        private void DrawPredefinedVariablesEditor (SerializedProperty property)
        {
            var label = EditorGUI.BeginProperty(Rect.zero, null, property);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            // Always check list's serialized object parity with the inspected object.
            if (predefinedVariablesList is null || predefinedVariablesList.serializedProperty.serializedObject != SerializedObject)
                InitializePredefinedVariablesList();

            predefinedVariablesList?.DoLayoutList();

            EditorGUI.EndProperty();
        }

        private void InitializePredefinedVariablesList ()
        {
            predefinedVariablesList = new(SerializedObject, SerializedObject.FindProperty(nameof(CustomVariablesConfiguration.PredefinedVariables)), true, true, true, true);
            predefinedVariablesList.drawHeaderCallback = DrawPredefinedVariablesListHeader;
            predefinedVariablesList.drawElementCallback = DrawPredefinedVariablesListElement;
        }

        private void DrawPredefinedVariablesListHeader (Rect rect)
        {
            var propertyRect = new Rect(headerLeftMargin + rect.x, rect.y, (rect.width / 2f) - paddingWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(propertyRect, nameContent);
            propertyRect.x += propertyRect.width + paddingWidth;
            EditorGUI.LabelField(propertyRect, valueContent);
        }

        private void DrawPredefinedVariablesListElement (Rect rect, int index, bool isActive, bool isFocused)
        {
            var propertyRect = new Rect(rect.x, rect.y + EditorGUIUtility.standardVerticalSpacing, (rect.width / 2f) - paddingWidth, EditorGUIUtility.singleLineHeight);
            var elementProperty = predefinedVariablesList.serializedProperty.GetArrayElementAtIndex(index);
            var nameProperty = elementProperty.FindPropertyRelative("name");
            var valueProperty = elementProperty.FindPropertyRelative("value");

            EditorGUI.PropertyField(propertyRect, nameProperty, GUIContent.none);
            propertyRect.x += propertyRect.width + paddingWidth;
            EditorGUI.PropertyField(propertyRect, valueProperty, GUIContent.none);
        }

        private void ValidateNames ()
        {
            if (predefinedVariablesList?.serializedProperty == null) return;
            for (int i = 0; i < predefinedVariablesList.serializedProperty.arraySize; i++)
                ValidateName(predefinedVariablesList.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue);
        }

        private void ValidateName (string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (name.Trim().Any(char.IsWhiteSpace))
                Engine.Warn($"Incorrect custom variable name '{name}': contains whitespace.");
            if (name.Length > 0 && int.TryParse(name[0].ToString(), out _))
                Engine.Warn($"Incorrect custom variable name '{name}': can't start with a number.");
        }
    }
}
