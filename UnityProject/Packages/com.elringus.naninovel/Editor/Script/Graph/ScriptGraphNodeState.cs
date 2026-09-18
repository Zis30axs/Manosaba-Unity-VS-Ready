using System;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    public struct ScriptGraphNodeState : IEquatable<ScriptGraphNodeState>
    {
        public string ScriptGuid => scriptGuid;
        public Rect Position => position;

        [SerializeField] private string scriptGuid;
        [SerializeField] private Rect position;

        public ScriptGraphNodeState (string scriptGuid, Rect position)
        {
            this.scriptGuid = scriptGuid;
            this.position = position;
        }

        public bool Equals (ScriptGraphNodeState other)
        {
            return scriptGuid == other.scriptGuid && position.Equals(other.position);
        }

        public override bool Equals (object obj)
        {
            return obj is ScriptGraphNodeState other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                return ((ScriptGuid != null ? ScriptGuid.GetHashCode() : 0) * 397) ^ Position.GetHashCode();
            }
        }

        public static bool operator == (ScriptGraphNodeState left, ScriptGraphNodeState right)
        {
            return left.Equals(right);
        }

        public static bool operator != (ScriptGraphNodeState left, ScriptGraphNodeState right)
        {
            return !left.Equals(right);
        }
    }
}
