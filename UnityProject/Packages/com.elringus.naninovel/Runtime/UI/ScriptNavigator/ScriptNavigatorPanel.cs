using System.Collections.Generic;

namespace Naninovel.UI
{
    public class ScriptNavigatorPanel : NavigatorPanel, IScriptNavigatorUI
    {
        protected override UniTask<IReadOnlyCollection<string>> LocateAllScriptPaths ()
        {
            return ScriptManager.ScriptLoader.Locate();
        }
    }
}
