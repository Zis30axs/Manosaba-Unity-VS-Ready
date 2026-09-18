using System.Collections;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Can be specified to the script parser instead of error collection to log the errors.
    /// </summary>
    public class ScriptParseErrorLogger : ICollection<ScriptParseError>
    {
        public int Count { get; } = 0;
        public bool IsReadOnly => false;

        private static readonly Stack<ScriptParseErrorLogger> pool = new();

        private readonly IEnumerator<ScriptParseError> enumerator = new List<ScriptParseError>.Enumerator();
        private string scriptPathOrName;

        private ScriptParseErrorLogger () { }

        public static ScriptParseErrorLogger GetFor (string scriptPathOrName)
        {
            var logger = pool.Count > 0 ? pool.Pop() : new();
            logger.scriptPathOrName = scriptPathOrName;
            return logger;
        }

        public static void Return (ScriptParseErrorLogger logger)
        {
            pool.Push(logger);
        }

        public void Add (ScriptParseError item) => Engine.Err(item.ToString(scriptPathOrName));
        public bool Remove (ScriptParseError item) => true;
        public void Clear () { }
        public bool Contains (ScriptParseError item) => false;
        public void CopyTo (ScriptParseError[] array, int arrayIndex) { }
        public IEnumerator<ScriptParseError> GetEnumerator () => enumerator;
        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();
    }
}
