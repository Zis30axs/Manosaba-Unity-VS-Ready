using Naninovel.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Naninovel
{
    /// <summary>
    /// Represents a node of <see cref="ScriptGraphView"/>.
    /// </summary>
    public sealed class ScriptGraphNode : Node
    {
        public readonly string ScriptGuid;
        public readonly string ScriptPath;
        public readonly Dictionary<string, Port> InputPorts = new();
        public readonly List<ScriptGraphOutputPort> OutputPorts = new();

        private const string startLabel = "Start";

        private readonly ScriptsConfiguration config;
        private readonly List<Script> availableScripts;

        public ScriptGraphNode (ScriptsConfiguration config, Script script, string scriptGuid, List<Script> availableScripts)
        {
            this.config = config;
            this.availableScripts = availableScripts;
            expanded = true;
            m_CollapseButton.Clear();
            m_CollapseButton.SetEnabled(false);
            capabilities = Capabilities.Ascendable | Capabilities.Selectable | Capabilities.Movable;
            ScriptGuid = scriptGuid;
            ScriptPath = title = script.Path;

            RegisterCallback<MouseDownEvent>(OnNodeMouseDown);

            // Add synopsis.
            if (config.ShowSynopsis)
            {
                var synopsis = CreateSynopsis(script);
                mainContainer.Insert(1, synopsis);
            }

            // Add input port.
            InputPorts[string.Empty] = AddPort(Direction.Input, startLabel);

            // Add out ports.
            foreach (var line in script.Lines)
            {
                if (line is LabelScriptLine labelLine)
                {
                    InputPorts[labelLine.LabelText] = AddPort(Direction.Input, $"{Compiler.Syntax.LabelLine}{labelLine.LabelText}");
                    continue;
                }

                if (line is CommandScriptLine commandLine)
                {
                    if (commandLine.Command is Goto @goto && !(@goto.Path?.DynamicValue ?? true))
                        AddOutPort(@goto, @goto.Path.Name ?? script.name, @goto.Path.NamedValue, $"goto {@goto.Path}");
                    if (commandLine.Command is Gosub gosub && !(gosub.Path?.DynamicValue ?? true))
                        AddOutPort(gosub, gosub.Path.Name ?? script.name, gosub.Path.NamedValue, $"gosub {gosub.Path}");
                    if (commandLine.Command is AddChoice choice && Command.Assigned(choice.GotoPath) && !choice.GotoPath.DynamicValue)
                        AddOutPort(choice, choice.GotoPath.Name ?? script.name, choice.GotoPath.NamedValue, $"choice goto:{choice.GotoPath}");
                }
            }

            void AddOutPort (Command command, string gotoScript, string gotoLabel, string portLabel)
            {
                portLabel = $"{Compiler.Syntax.CommandLine}{portLabel}";
                var tooltip = command.ConditionalExpression?.DynamicValue ?? false
                    ? command.ConditionalExpression.RawValue.ToString()
                    : command.ConditionalExpression?.Value;
                var port = AddPort(Direction.Output, portLabel, tooltip);
                var label = string.IsNullOrEmpty(gotoLabel) ? string.Empty : gotoLabel;
                var portData = new ScriptGraphOutputPort(gotoScript, label, port);
                OutputPorts.Add(portData);
            }
        }

        public override void BuildContextualMenu (ContextualMenuPopulateEvent evt) { }

        public HashSet<ScriptGraphNode> GetConnectedNodes ()
        {
            var inputNodes = InputPorts.SelectMany(p => p.Value.connections.Select(c => c.output.node as ScriptGraphNode)).Where(n => n != this);
            var outputNodes = OutputPorts.SelectMany(p => p.Port.connections.Select(c => c.input.node as ScriptGraphNode)).Where(n => n != this);
            var result = new HashSet<ScriptGraphNode>();
            result.UnionWith(inputNodes);
            result.UnionWith(outputNodes);
            return result;
        }

        private static VisualElement CreateSynopsis (Script script)
        {
            var synopsisText = string.Empty;
            foreach (var scriptLine in script.Lines)
            {
                if (scriptLine is CommentScriptLine commentLine)
                {
                    if (string.IsNullOrWhiteSpace(commentLine.CommentText)) continue;
                    if (!string.IsNullOrEmpty(synopsisText)) synopsisText += "\n";
                    synopsisText += commentLine.CommentText;
                }
                else break;
            }

            if (string.IsNullOrEmpty(synopsisText))
                return new();

            var synopsisContainer = new VisualElement { name = "synopsis" };
            var divider = new VisualElement { name = "divider" };
            divider.AddToClassList("horizontal");
            synopsisContainer.Add(divider);
            var synopsisLabel = new Label(synopsisText);
            synopsisContainer.Add(synopsisLabel);
            return synopsisContainer;
        }

        private Port AddPort (Direction direction, string label, string tooltip = null)
        {
            var orientation = config.GraphOrientation == ScriptsConfiguration.GraphOrientationType.Vertical ? Orientation.Vertical : Orientation.Horizontal;
            var port = InstantiatePort(orientation, direction, direction == Direction.Input ? Port.Capacity.Multi : Port.Capacity.Single, typeof(Script));
            port.capabilities = Capabilities.Selectable;
            port.portName = label;
            port.Q<Label>("type").pickingMode = PickingMode.Position;
            if (!string.IsNullOrEmpty(tooltip))
            {
                port.tooltip = tooltip;
                port.Q<Label>("type").AddToClassList("if");
            }
            port.edgeConnector.activators.Clear(); // Prevents user from creating connections.
            port.RegisterCallback<MouseDownEvent>(OnPortMouseDown);
            inputContainer.Add(port);
            return port;
        }

        private void OnNodeMouseDown (MouseDownEvent evt)
        {
            var path = AssetDatabase.GUIDToAssetPath(ScriptGuid);
            if (string.IsNullOrEmpty(path)) return;
            var script = AssetDatabase.LoadAssetAtPath<Script>(path);
            if (evt.button == 0 && evt.clickCount >= 2 && ObjectUtils.IsValid(script))
            {
                Selection.activeObject = script;
                EditorGUIUtility.PingObject(script);
            }
        }

        private void OnPortMouseDown (MouseDownEvent evt)
        {
            if (evt.button != 0) return;
            evt.StopImmediatePropagation();

            var port = evt.currentTarget as Port;
            if (port is null) return;

            (parent.parent.parent.parent as ScriptGraphView)?.edges.ForEach(e => {
                e.selected = false;
                e.UpdateEdgeControl();
            });
            foreach (var edge in port.connections)
            {
                edge.selected = true;
                edge.UpdateEdgeControl();
            }

            var node = port.node as ScriptGraphNode;
            if (node is null) return;

            var gotoScript = default(Script);
            var gotoLabel = default(string);
            if (port.direction == Direction.Input)
            {
                var path = AssetDatabase.GUIDToAssetPath(node.ScriptGuid);
                gotoScript = string.IsNullOrEmpty(path) ? null : AssetDatabase.LoadAssetAtPath<Script>(path);
                gotoLabel = node.InputPorts.FirstOrDefault(kv => kv.Value == port).Key;
            }
            else
            {
                var scriptName = OutputPorts.FirstOrDefault(d => d.Port == port)?.ScriptName;
                var script = availableScripts.FirstOrDefault(s => s.name == scriptName);
                gotoScript = script;
                gotoLabel = OutputPorts.FirstOrDefault(d => d.Port == port)?.Label;
            }

            if (gotoScript)
            {
                Selection.activeObject = gotoScript;
                EditorGUIUtility.PingObject(gotoScript);

                // Scroll to line.
                if (gotoScript.Lines.Count == 0) return;
                EditorApplication.delayCall += ScrollToLineDelayed;
                void ScrollToLineDelayed ()
                {
                    if (!gotoScript) return;
                    var editors = Resources.FindObjectsOfTypeAll<ScriptImporterEditor>();
                    if (editors.Length == 0) return;
                    var editorView = editors[0].VisualEditor;
                    var lineView = string.IsNullOrEmpty(gotoLabel) ? editorView?.Lines.FirstOrDefault(l => l != null) :
                        editorView?.Lines.FirstOrDefault(l => l?.LineIndex == gotoScript.GetLineIndexForLabel(gotoLabel));
                    if (lineView is null) EditorApplication.delayCall += ScrollToLineDelayed;
                    else editorView.ScrollToLine(lineView);
                }
            }
        }
    }
}
