using UnityEngine.UIElements;

namespace Naninovel
{
    public class GenericTextLineView : ScriptLineView
    {
        private readonly LineTextField valueField;

        public GenericTextLineView (int lineIndex, int lineIndent, string lineText, VisualElement container)
            : base(lineIndex, lineIndent, container)
        {
            valueField = new(value: lineText);
            valueField.multiline = true;
            Content.Add(valueField);
        }

        public override string GenerateLineText ()
        {
            return GenerateLineIndent() + valueField.value;
        }
    }
}
