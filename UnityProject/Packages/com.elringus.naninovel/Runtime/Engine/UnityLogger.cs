using UnityEngine;

namespace Naninovel
{
    public class UnityLogger : ILogger
    {
        public void Log (string message) => Debug.Log(message);
        public void Warn (string message) => Debug.LogWarning(message);
        public void Err (string message) => Debug.LogError(message);
    }
}
