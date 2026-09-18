using UnityEngine.SceneManagement;

namespace Naninovel.Commands
{
    [Doc(
        @"
Unloads a [Unity scene](https://docs.unity3d.com/Manual/CreatingScenes.html) with the specified name.
Don't forget to add the required scenes to the [build settings](https://docs.unity3d.com/Manual/BuildSettings.html) to make them available for loading.
Be aware, that only scenes loaded additively can be then unloaded (at least one scene should always remain loaded).",
        null,
        @"
; Load scene 'TestScene2' in additive mode and then unload it.
@loadScene TestScene2 additive!
@unloadScene TestScene2"
    )]
    public class UnloadScene : Command
    {
        [Doc("Name of the scene to unload.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringParameter SceneName;

        public override async UniTask Execute (AsyncToken token = default)
        {
            await SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}
