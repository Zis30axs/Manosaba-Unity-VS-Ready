using UnityEditor;

namespace Naninovel
{
    [CustomEditor(typeof(PlayScript))]
    public class PlayScriptEditor : Editor
    {
        private SerializedProperty scriptPath;
        private SerializedProperty scriptText;
        private SerializedProperty playOnAwake;
        private SerializedProperty disableWaitInput;

        private void OnEnable ()
        {
            scriptPath = serializedObject.FindProperty("scriptPath");
            scriptText = serializedObject.FindProperty("scriptText");
            playOnAwake = serializedObject.FindProperty("playOnAwake");
            disableWaitInput = serializedObject.FindProperty("disableWaitInput");
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(scriptPath);
            if (string.IsNullOrEmpty(scriptPath.stringValue))
            {
                EditorGUILayout.PropertyField(scriptText);
                EditorGUILayout.PropertyField(disableWaitInput);
            }
            EditorGUILayout.PropertyField(playOnAwake);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
