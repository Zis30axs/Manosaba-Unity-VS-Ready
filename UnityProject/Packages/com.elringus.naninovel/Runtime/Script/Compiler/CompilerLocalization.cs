using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.Parsing;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Locale-specific NaniScript compiler options.
    /// </summary>
    [CreateAssetMenu(menuName = "Naninovel/Compiler Localization", fileName = "NewNaniScriptL10n")]
    public class CompilerLocalization : ScriptableObject
    {
        [Tooltip("Marks beginning of comment lines. ';' by default.")]
        public string CommentLine = Syntax.Default.CommentLine;
        [Tooltip("Marks beginning of label lines. '#' by default.")]
        public string LabelLine = Syntax.Default.LabelLine;
        [Tooltip("Marks beginning of the command lines. '@' by default.")]
        public string CommandLine = Syntax.Default.CommandLine;
        [Tooltip("Delimits author prefix from generic text content. ': ' by default.")]
        public string AuthorAssign = Syntax.Default.AuthorAssign;
        [Tooltip("Delimits author appearance from author identifier in generic text line prefix. '.' by default.")]
        public string AuthorAppearance = Syntax.Default.AuthorAppearance;
        [Tooltip("Marks beginning of script expression. '{' by default.")]
        public string ExpressionOpen = Syntax.Default.ExpressionOpen;
        [Tooltip("Marks end of script expression. '}' by default.")]
        public string ExpressionClose = Syntax.Default.ExpressionClose;
        [Tooltip("Marks beginning of inlined command in generic text line. '[' by default.")]
        public string InlinedOpen = Syntax.Default.InlinedOpen;
        [Tooltip("Marks end of inlined command in generic text line. ']' by default.")]
        public string InlinedClose = Syntax.Default.InlinedClose;
        [Tooltip("Delimits command parameter value from parameter identifier. ':' by default.")]
        public string ParameterAssign = Syntax.Default.ParameterAssign;
        [Tooltip("Delimits items in list parameter value. ',' by default.")]
        public string ListDelimiter = Syntax.Default.ListDelimiter;
        [Tooltip("Delimits value from name in named parameter value. '.' by default.")]
        public string NamedDelimiter = Syntax.Default.NamedDelimiter;
        [Tooltip("Marks beginning of text identifier in localizable text parameter value and generic text line. '|#' by default.")]
        public string TextIdOpen = Syntax.Default.TextIdOpen;
        [Tooltip("Marks end of text identifier in localizable text parameter value and generic text line. '|' by default.")]
        public string TextIdClose = Syntax.Default.TextIdClose;
        [Tooltip("The flag placed before/after identifier of boolean command parameter to represent negative/positive value. '!' by default.")]
        public string BooleanFlag = Syntax.Default.BooleanFlag;
        [Tooltip("Constant representing positive boolean value. 'true' by default.")]
        public string True = Syntax.Default.True;
        [Tooltip("Constant representing negative boolean value. 'false' by default.")]
        public string False = Syntax.Default.False;
        [Tooltip("Prefix to identify global script variables. 'g_' by default.")]
        public string GlobalVariablePrefix = "g_";
        [Tooltip("Prefix to identify script constants pulled from managed text. 't_' by default.")]
        public string ScriptConstantPrefix = "t_";

        [Tooltip("Locale-specific command aliases.")]
        public List<CommandLocalization> Commands = new();
        [Tooltip("Locale-specific expression function aliases.")]
        public List<FunctionLocalization> Functions = new();
        [Tooltip("Locale-specific constant aliases.")]
        public List<ConstantLocalization> Constants = new();

        public Syntax GetSyntax () => new(
            commentLine: CommentLine,
            labelLine: LabelLine,
            commandLine: CommandLine,
            authorAssign: AuthorAssign,
            authorAppearance: AuthorAppearance,
            expressionOpen: ExpressionOpen,
            expressionClose: ExpressionClose,
            inlinedOpen: InlinedOpen,
            inlinedClose: InlinedClose,
            parameterAssign: ParameterAssign,
            listDelimiter: ListDelimiter,
            namedDelimiter: NamedDelimiter,
            textIdOpen: TextIdOpen,
            textIdClose: TextIdClose,
            booleanFlag: BooleanFlag,
            @true: True,
            @false: False
        );

        [ContextMenu("Add Existing Commands")]
        private void AddExistingCommands ()
        {
            var hash = new HashSet<string>(Commands.Select(c => c.Id));
            foreach (var cmd in Command.CommandTypes.Values.OrderBy(t => t.Name))
                if (!hash.Contains(cmd.Name))
                    Commands.Add(new() {
                        Id = cmd.Name,
                        Parameters = AddExistingParameters(cmd)
                    });
        }

        [ContextMenu("Add Existing Functions")]
        private void AddExistingFunctions ()
        {
            var hash = new HashSet<string>(Functions.Select(c => c.MethodName));
            foreach (var fn in ExpressionFunctions.Resolve().DistinctBy(t => t.Method.Name).OrderBy(t => t.Method.Name))
                if (!hash.Contains(fn.Method.Name))
                    Functions.Add(new() {
                        MethodName = fn.Method.Name
                    });
        }

        private List<ParameterLocalization> AddExistingParameters (Type commandType)
        {
            return CommandParameter.ExtractFields(commandType)
                .OrderBy(t => t.Name)
                .Select(f => new ParameterLocalization {
                    Id = f.Name,
                    Alias = ""
                }).ToList();
        }
    }
}
