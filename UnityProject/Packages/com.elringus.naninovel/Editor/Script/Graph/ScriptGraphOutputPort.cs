using UnityEditor.Experimental.GraphView;

namespace Naninovel
{
    public class ScriptGraphOutputPort
    {
        public readonly string ScriptName, Label; 
        public readonly Port Port;

        public ScriptGraphOutputPort (string scriptName, string label, Port port)
        {
            ScriptName = scriptName;
            Label = label;
            Port = port;
        }
    }
}
