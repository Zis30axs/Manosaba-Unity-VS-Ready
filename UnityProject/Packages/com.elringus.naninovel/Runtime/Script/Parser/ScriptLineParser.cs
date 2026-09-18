using Naninovel.Parsing;

namespace Naninovel
{
    public abstract class ScriptLineParser<TResult, TModel>
        where TResult : ScriptLine
        where TModel : IScriptLine
    {
        protected virtual string ScriptPath { get; private set; }
        protected virtual int LineIndex { get; private set; }
        protected virtual string LineHash { get; private set; }
        protected virtual bool Transient { get; private set; }

        /// <summary>
        /// Produces a persistent hash code from the specified script line text (trimmed).
        /// </summary>
        public static string GetHash (string lineText)
        {
            return CryptoUtils.PersistentHexCode(lineText.TrimFull());
        }

        public virtual TResult Parse (LineParseArgs<TModel> args)
        {
            ScriptPath = args.ScriptPath;
            LineIndex = args.LineIndex;
            LineHash = GetHash(args.LineText);
            Transient = args.Transient;
            return Parse(args.LineModel);
        }

        protected abstract TResult Parse (TModel lineModel);
    }
}
