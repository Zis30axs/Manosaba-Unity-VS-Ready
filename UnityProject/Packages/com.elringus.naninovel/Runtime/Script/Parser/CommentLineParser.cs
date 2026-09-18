using Naninovel.Parsing;

namespace Naninovel
{
    public class CommentLineParser : ScriptLineParser<CommentScriptLine, CommentLine>
    {
        protected override CommentScriptLine Parse (CommentLine lineModel)
        {
            return new(lineModel.Comment.Text, LineIndex, lineModel.Indent, LineHash);
        }
    }
}
