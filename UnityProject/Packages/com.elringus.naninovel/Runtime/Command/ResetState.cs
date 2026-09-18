using System.Linq;

namespace Naninovel.Commands
{
    [Doc(
        @"
Resets state of the [engine services](/guide/engine-services) and unloads (disposes)
all the resources loaded by Naninovel (textures, audio, video, etc); will basically revert to an empty initial engine state.",
        @"
Be aware, that this command can not be undone (rewound back).",
        @"
; Reset all the services (script will stop playing).
@resetState",
        @"
; Reset all the services except script player, custom variable and
; audio managers, allowing current script and audio tracks
; continue playing and preserving values of the custom variables.
@resetState IScriptPlayer,ICustomVariableManager,IAudioManager",
        @"
; Reset only 'ICharacterManager' and 'IBackgroundManager' services
; removing all the character and background actors from scene
; and unloading associated resources from memory.
@resetState only:ICharacterManager,IBackgroundManager"
    )]
    public class ResetState : Command
    {
        [Doc("Names of the [engine services](/guide/engine-services) (interfaces) to exclude from reset. " +
             "Consider adding `ICustomVariableManager` to preserve the local variables.")]
        [ParameterAlias(NamelessParameterAlias)]
        public StringListParameter Exclude;
        [Doc("Names of the [engine services](/guide/engine-services) (interfaces) to reset; " +
             "other services won't be affected. Doesn't have effect when the nameless (exclude) parameter is assigned.")]
        public StringListParameter Only;

        public override async UniTask Execute (AsyncToken token = default)
        {
            if (Assigned(Exclude)) await Engine.GetServiceOrErr<IStateManager>().ResetState(Exclude.ToReadOnlyList());
            else if (Assigned(Only))
            {
                var serviceTypes = Engine.Services.Select(s => s.GetType()).ToArray();
                var onlyTypeNames = Only.Value.Select(v => v.Value);
                var onlyTypes = serviceTypes.Where(t => onlyTypeNames.Any(ot => ot == t.Name || t.GetInterface(ot) != null));
                var excludeTypes = serviceTypes.Where(t => !onlyTypes.Any(ot => ot.IsAssignableFrom(t))).ToArray();
                await Engine.GetServiceOrErr<IStateManager>().ResetState(excludeTypes);
            }
            else await Engine.GetServiceOrErr<IStateManager>().ResetState();
        }
    }
}
