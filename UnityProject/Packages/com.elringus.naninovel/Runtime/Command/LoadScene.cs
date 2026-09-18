using UnityEngine.SceneManagement;

namespace Naninovel.Commands
{
    [Doc(
        @"
Loads a [Unity scene](https://docs.unity3d.com/Manual/CreatingScenes.html) with the specified name.
Don't forget to add the required scenes to the [build settings](https://docs.unity3d.com/Manual/BuildSettings.html) to make them available for loading.",
        null,
        @"
; Load scene 'TestScene1' in single mode.
@loadScene TestScene1",
        @"
; Load scene 'TestScene2' in additive mode.
@loadScene TestScene2 additive!"
    )]
    public class LoadScene : Command
    {
        [Doc("Name of the scene to load.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringParameter SceneName;
        [Doc("Whether to load the scene additively, or unload any currently loaded scenes before loading the new one (default). " +
             "See the [load scene documentation](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html) for more information.")]
        [ParameterDefaultValue("false")]
        public BooleanParameter Additive = false;

        public override async UniTask Execute (AsyncToken token = default)
        {
            await SceneManager.LoadSceneAsync(SceneName, Additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }
    }
}
