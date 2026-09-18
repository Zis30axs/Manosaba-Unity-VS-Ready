using Naninovel.Parsing;
using UnityEngine.UIElements;

namespace Naninovel
{
    public class LabelLineView : ScriptLineView
    {
        public readonly LineTextField ValueField;

        public LabelLineView (int lineIndex, LabelLine model, VisualElement container)
            : base(lineIndex, model.Indent, container)
        {
            var value = model.Label;
            ValueField = new(Compiler.Syntax.LabelLine, value);
            Content.Add(ValueField);
        }

        public override string GenerateLineText ()
        {
            return $"{GenerateLineIndent()}{Compiler.Syntax.LabelLine} {ValueField.value}";
        }
    }
}
