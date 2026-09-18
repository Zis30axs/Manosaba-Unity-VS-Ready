using System.Collections.Generic;
using Naninovel.Expression;

namespace Naninovel.Commands
{
    [Doc(
        @"
Assigns result of a [script expression](/guide/script-expressions) to a [custom variable](/guide/custom-variables).",
        @"
If a variable with the specified name doesn't exist, it will be automatically created.<br/><br/>
Specify multiple set expressions by separating them with `;`. The expressions will be executed in sequence in the order of declaration.<br/><br/>
In case variable name starts with `t_` it's considered a reference to a value stored in 'Script' [managed text](/guide/managed-text) document.
Such variables can't be assigned and are intended for referencing localizable text values.",
        @"
; Assign 'foo' variable a 'bar' string value.
@set foo=""bar""",
        @"
; Assign 'foo' variable a 1 number value.
@set foo=1",
        @"
; Assign 'foo' variable a 'true' boolean value.
@set foo=true",
        @"
; If 'foo' is a number, add 0.5 to its value.
@set foo+=0.5",
        @"
; If 'angle' is a number, assign its cosine to 'foo' variable.
@set foo=cos(angle)",
        @"
; Get random number between -100 and 100, then raise to power of 4 
; and assign to 'foo' variable. Quotes are required when whitespace 
; is present inside the expression.
@set ""foo = pow(random(-100, 100), 4)""",
        @"
; If 'foo' is a number, add 1 to its value (increment).
@set foo++",
        @"
; If 'foo' is a number, subtract 1 from its value (decrement).
@set foo--",
        @"
; Assign 'foo' variable value of the 'bar' variable, 
; which is 'Hello World!' string.
@set bar=""Hello World!""
@set foo=bar",
        @"
; Defining multiple set expressions in one line; 
; the result will be the same as above.
@set bar=""Hello World!"";foo=bar",
        @"
; It's possible to inject variables to naninovel script command parameters.
@set scale=0
# EnlargeLoop
@char Kohaku.Default scale:{scale}
@set scale+=0.1
@goto .EnlargeLoop if:scale<1",
        @"
; ...and generic text lines.
@set drink=""Dr. Pepper""
My favourite drink is {drink}!",
        @"
; When using double quotes inside text expression value, escape them.
@set remark=""Shouting \""Stop the car!\"" was a mistake.""",
        @"
; Use global variable ('g_' prefix) to persist the value across sessions.
; The variable will remain true even when the game is restarted.
@set g_Ending001Reached=true",
        @"
; Increment the global variable only once, even when re-played.
@set g_GlobalCounter++ if:!hasPlayed()"
    )]
    [CommandAlias("set")]
    public class SetCustomVariable : Command
    {
        [Doc("Assignment expression.<br/><br/>" +
             "The expression should be in the following format: `var=expression`, where `var` is the name of the custom " +
             "variable to assign and `expression` is a [script expression](/guide/script-expressions), the result of which should be assigned to the variable.<br/><br/>" +
             "It's possible to use increment and decrement unary operators (`@set foo++`, `@set foo--`) and compound assignment (`@set foo+=10`, `@set foo-=3`, `@set foo*=0.1`, `@set foo/=2`).")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, AssignmentContext]
        public StringParameter Expression;

        public override async UniTask Execute (AsyncToken token = default)
        {
            var vars = Engine.GetServiceOrErr<ICustomVariableManager>();
            var asses = new List<Assignment>();

            ExpressionEvaluator.ParseAssignments(Expression, asses, LogErrorMessage);
            foreach (var ass in asses)
                AssignEvaluated(ass.Variable, ExpressionEvaluator.Evaluate(ass.Expression, LogErrorMessage), vars);

            if (ShouldSaveGlobalState(asses))
                await Engine.GetServiceOrErr<IStateManager>().SaveGlobal();
        }

        protected virtual void AssignEvaluated (string var, IOperand result, ICustomVariableManager vars)
        {
            if (result is String str) vars.SetVariableValue(var, new(str.Value));
            else if (result is Boolean boo) vars.SetVariableValue(var, new(boo.Value));
            else vars.SetVariableValue(var, new((float)((Numeric)result).Value));
        }

        protected virtual bool ShouldSaveGlobalState (IReadOnlyList<Assignment> asses)
        {
            foreach (var eval in asses)
                if (CustomVariablesConfiguration.HasGlobalPrefix(eval.Variable))
                    return true;
            return false;
        }

        protected virtual void LogErrorMessage (string desc = null)
        {
            Err($"Failed to evaluate assignment expression '{Expression}'. {desc ?? string.Empty}");
        }
    }
}
