using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    [CustomEditor(typeof(UI.NaninovelTMProText))]
    [CanEditMultipleObjects]
    public class NaninovelTMProTextEditor : TMP_EditorPanelUI
    {
        private static readonly GUIContent content = new("<b>Naninovel Settings</b>");

        private bool foldout;
        private SerializedProperty rubyVerticalOffset;
        private SerializedProperty rubySizeScale;
        private SerializedProperty addRubyLineHeight;
        private SerializedProperty unlockTipsOnPrint;
        private SerializedProperty tipTemplate;
        private SerializedProperty onTipClicked;
        private SerializedProperty fixArabicText;
        private SerializedProperty fixArabicFarsi;
        private SerializedProperty fixArabicTextTags;
        private SerializedProperty fixArabicPreserveNumbers;
        private SerializedProperty linkTemplate;
        private SerializedProperty onLinkClicked;

        protected override void OnEnable ()
        {
            base.OnEnable();

            rubyVerticalOffset = serializedObject.FindProperty("rubyVerticalOffset");
            rubySizeScale = serializedObject.FindProperty("rubySizeScale");
            addRubyLineHeight = serializedObject.FindProperty("addRubyLineHeight");
            unlockTipsOnPrint = serializedObject.FindProperty("unlockTipsOnPrint");
            tipTemplate = serializedObject.FindProperty("tipTemplate");
            onTipClicked = serializedObject.FindProperty("onTipClicked");
            fixArabicText = serializedObject.FindProperty("fixArabicText");
            fixArabicFarsi = serializedObject.FindProperty("fixArabicFarsi");
            fixArabicTextTags = serializedObject.FindProperty("fixArabicTextTags");
            fixArabicPreserveNumbers = serializedObject.FindProperty("fixArabicPreserveNumbers");
            linkTemplate = serializedObject.FindProperty("linkTemplate");
            onLinkClicked = serializedObject.FindProperty("onLinkClicked");
        }

        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            var rect = EditorGUILayout.GetControlRect(false, 24);

            if (GUI.Button(rect, content, TMP_UIStyleManager.sectionHeader))
                foldout = !foldout;

            GUI.Label(rect, foldout ? k_UiStateLabel[0] : k_UiStateLabel[1], TMP_UIStyleManager.rightLabel);

            if (foldout)
            {
                EditorGUILayout.LabelField("Ruby Text", EditorStyles.boldLabel);
                ++EditorGUI.indentLevel;
                {
                    EditorGUILayout.PropertyField(rubyVerticalOffset);
                    EditorGUILayout.PropertyField(rubySizeScale);
                    EditorGUILayout.PropertyField(addRubyLineHeight);
                    DrawExtraRubySettings();
                }
                --EditorGUI.indentLevel;

                EditorGUILayout.LabelField("Tips", EditorStyles.boldLabel);
                ++EditorGUI.indentLevel;
                {
                    EditorGUILayout.PropertyField(unlockTipsOnPrint);
                    EditorGUILayout.PropertyField(tipTemplate);
                    EditorGUILayout.PropertyField(onTipClicked);
                }
                --EditorGUI.indentLevel;

                EditorGUILayout.LabelField("Arabic Text Support", EditorStyles.boldLabel);
                ++EditorGUI.indentLevel;
                {
                    EditorGUILayout.PropertyField(fixArabicText);
                    EditorGUI.BeginDisabledGroup(!fixArabicText.boolValue);
                    EditorGUILayout.PropertyField(fixArabicFarsi);
                    EditorGUILayout.PropertyField(fixArabicTextTags);
                    EditorGUILayout.PropertyField(fixArabicPreserveNumbers);
                    EditorGUI.EndDisabledGroup();
                }
                --EditorGUI.indentLevel;

                EditorGUILayout.LabelField("Links", EditorStyles.boldLabel);
                ++EditorGUI.indentLevel;
                {
                    EditorGUILayout.PropertyField(linkTemplate);
                    EditorGUILayout.PropertyField(onLinkClicked);
                }
                --EditorGUI.indentLevel;
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawExtraRubySettings () { }
    }
}
