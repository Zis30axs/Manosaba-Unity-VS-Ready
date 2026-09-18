namespace Naninovel.Commands
{
    [Doc(
        @"
Modifies a [background actor](/guide/backgrounds).",
        @"
Backgrounds are handled a bit differently from characters to better accommodate traditional VN game flow. 
Most of the time you'll probably have a single background actor on scene, which will constantly transition to different appearances.
To remove the hassle of repeating same actor ID in scripts, it's possible to provide only 
the background appearance and transition type (optional) as a nameless parameter assuming `MainBackground` 
actor should be affected. When this is not the case, ID of the background actor can be explicitly specified via the `id` parameter.",
        @"
; Set 'River' as the appearance of the main background.
@back River",
        @"
; Same as above, but also use a 'RadialBlur' transition effect.
@back River.RadialBlur",
        @"
; Position 'Smoke' background at the center of the screen
; and scale it 50% of the original size.
@back id:Smoke pos:50,50 scale:0.5",
        @"
; Tint all visible backgrounds on scene.
@back id:* tint:#ffdc22"
    )]
    [CommandAlias("back")]
    [ActorContext(BackgroundsConfiguration.DefaultPathPrefix, paramId: nameof(Id))]
    [ConstantContext("Poses/Backgrounds/{:Id??MainBackground}+Poses/Backgrounds/*", paramId: nameof(Pose))]
    public class ModifyBackground : ModifyOrthoActor<IBackgroundActor, BackgroundState, BackgroundMetadata, BackgroundsConfiguration, IBackgroundManager>
    {
        [Doc("Appearance (or [pose](/guide/backgrounds#poses)) to set for the modified background and type of a [transition effect](/guide/transition-effects) to use. " +
             "When transition is not specified, a cross-fade effect will be used by default.")]
        [ParameterAlias(NamelessParameterAlias), AppearanceContext(0, actorId: BackgroundsConfiguration.MainActorId), ConstantContext(typeof(TransitionType), 1)]
        public NamedStringParameter AppearanceAndTransition;

        protected override bool AllowPreload => base.AllowPreload || Assigned(AppearanceAndTransition) && !AppearanceAndTransition.DynamicValue;
        protected override string AssignedId => base.AssignedId ?? BackgroundsConfiguration.MainActorId;
        protected override string AlternativeAppearance => AppearanceAndTransition?.Name;
        protected override string AssignedTransition => base.AssignedTransition ?? AppearanceAndTransition?.NamedValue;
    }
}
