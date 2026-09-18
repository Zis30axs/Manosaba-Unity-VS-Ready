using Naninovel.UI;
using UnityEditor;

namespace Naninovel
{
    [CustomEditor(typeof(RevealableText))]
    [CanEditMultipleObjects]
    public class RevealableTextEditor : NaninovelTMProTextEditor
    {
        private SerializedProperty revealRubyInstantly;

        protected override void OnEnable ()
        {
            base.OnEnable();

            revealRubyInstantly = serializedObject.FindProperty("revealRubyInstantly");
        }

        protected override void DrawExtraRubySettings ()
        {
            EditorGUILayout.PropertyField(revealRubyInstantly);
        }
    }
}
