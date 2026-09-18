using System;

namespace Naninovel
{
    [Serializable]
    public class EmptyScriptLine : ScriptLine
    {
        public EmptyScriptLine (int lineIndex, int indent)
            : base(lineIndex, indent, string.Empty) { }
    }
}
