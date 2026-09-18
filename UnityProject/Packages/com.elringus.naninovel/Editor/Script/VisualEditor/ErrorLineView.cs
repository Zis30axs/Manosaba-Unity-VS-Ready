using UnityEngine.UIElements;

namespace Naninovel
{
    public class ErrorLineView : ScriptLineView
    {
        public string CommandId { get; }

        private readonly LineTextField valueField;

        public ErrorLineView (int lineIndex, int lineIndent, string lineText,
            VisualElement container, string commandId, string error = default)
            : base(lineIndex, lineIndent, container)
        {
            CommandId = commandId;
            valueField = new(value: lineText);
            Content.Add(valueField);
            if (!string.IsNullOrEmpty(error))
                tooltip = "Error: " + error;
        }

        public override string GenerateLineText ()
        {
            return GenerateLineIndent() + valueField.value;
        }
    }
}
