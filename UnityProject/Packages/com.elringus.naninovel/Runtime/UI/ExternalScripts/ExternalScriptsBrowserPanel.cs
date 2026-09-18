using System.Collections.Generic;

namespace Naninovel.UI
{
    public class ExternalScriptsBrowserPanel : NavigatorPanel, IExternalScriptsUI
    {
        protected override UniTask<IReadOnlyCollection<string>> LocateAllScriptPaths ()
        {
            return ScriptManager.ExternalScriptLoader.Locate();
        }
    }
}
