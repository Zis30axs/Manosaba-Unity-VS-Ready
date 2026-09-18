using Naninovel.Parsing;
using UnityEngine.UIElements;

namespace Naninovel
{
    public class CommentLineView : ScriptLineView
    {
        private readonly LineTextField valueField;

        public CommentLineView (int lineIndex, CommentLine model, VisualElement container)
            : base(lineIndex, model.Indent, container)
        {
            var value = model.Comment;
            valueField = new(Compiler.Syntax.CommentLine, value);
            valueField.multiline = true;
            Content.Add(valueField);
        }

        public override string GenerateLineText ()
        {
            return $"{GenerateLineIndent()}{Compiler.Syntax.CommentLine} {valueField.value}";
        }
    }
}
