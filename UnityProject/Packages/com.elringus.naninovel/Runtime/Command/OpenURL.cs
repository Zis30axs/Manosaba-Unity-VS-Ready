namespace Naninovel.Commands
{
    [Doc(
        @"
Opens specified URL (web address) with default web browser.",
        @"
When outside of WebGL or in editor, Unity's `Application.OpenURL` method is used to handle the command;
consult the [documentation](https://docs.unity3d.com/ScriptReference/Application.OpenURL.html) for behaviour details and limitations.
Under WebGL native `window.open()` JS function is invoked: https://developer.mozilla.org/en-US/docs/Web/API/Window/open.",
        @"
; Open blank page in the current tab.
@openURL ""about:blank""",
        @"
; Open Naninovel website in new tab.
@openURL ""https://naninovel.com"" target:_blank"
    )]
    public class OpenURL : Command
    {
        [Doc("URL to open.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringParameter URL;
        [Doc("Browsing context: _self (current tab), _blank (new tab), _parent, _top.")]
        [ParameterDefaultValue("_self")]
        public StringParameter Target = "_self";

        public override UniTask Execute (AsyncToken token = default)
        {
            WebUtils.OpenURL(URL, Target);
            return UniTask.CompletedTask;
        }
    }
}
