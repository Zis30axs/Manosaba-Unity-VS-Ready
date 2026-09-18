using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(@"
Modifies a [character actor](/guide/characters).",
        null,
        @"
; Shows character with ID 'Sora' with a default appearance.
@char Sora",
        @"
; Same as above, but sets appearance to 'Happy'.
@char Sora.Happy",
        @"
; Same as above, but additionally positions the character 45% away 
; from the left border of the scene and 10% away from the bottom border; 
; also makes it look to the left.
@char Sora.Happy look:left pos:45,10",
        @"
; Make Sora appear at the bottom-center and in front of Felix.
@char Sora pos:50,0,-1
@char Felix pos:,,0",
        @"
; Tint all visible characters on scene.
@char * tint:#ffdc22"
    )]
    [CommandAlias("char")]
    [ActorContext(CharactersConfiguration.DefaultPathPrefix, paramId: nameof(Id))]
    [ConstantContext("Poses/Characters/{:Id??:IdAndAppearance[0]}+Poses/Characters/*", paramId: nameof(Pose))]
    public class ModifyCharacter : ModifyOrthoActor<ICharacterActor, CharacterState, CharacterMetadata, CharactersConfiguration, ICharacterManager>
    {
        [Doc("ID of the character to modify (specify `*` to affect all visible characters) and an appearance (or [pose](/guide/characters#poses)) to set. " +
             "When appearance is not specified, will use either a `Default` (is exists) or a random one.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ActorContext(CharactersConfiguration.DefaultPathPrefix, 0), AppearanceContext(1)]
        public NamedStringParameter IdAndAppearance;
        [Doc("Look direction of the actor; supported values: left, right, center.")]
        [ParameterAlias("look"), ConstantContext(typeof(CharacterLookDirection))]
        public StringParameter LookDirection;
        [Doc("Name (path) of the [avatar texture](/guide/characters#avatar-textures) to assign for the character. " +
             "Use `none` to remove (un-assign) avatar texture from the character.")]
        [ParameterAlias("avatar")]
        public StringParameter AvatarTexturePath;
        [Doc("Whether this command is an inlined prefix of a generic text line. Used internally by the script asset parser and serializer, don't assign manually.")]
        [IgnoreParameter]
        public BooleanParameter IsGenericPrefix;

        protected override bool AllowPreload => !IdAndAppearance.DynamicValue;
        protected override string AssignedId => base.AssignedId ?? IdAndAppearance?.Name;
        protected override string AlternativeAppearance => IdAndAppearance?.NamedValue;
        protected virtual CharacterLookDirection? AssignedLookDirection => Assigned(LookDirection) ? ParseLookDirection(LookDirection) : PosedLookDirection;

        protected CharacterLookDirection? PosedLookDirection => GetPosed(nameof(CharacterState.LookDirection))?.LookDirection;

        protected override async UniTask Modify (AsyncToken token)
        {
            if (!Assigned(AvatarTexturePath)) // Check if we can map current appearance to an avatar texture path.
            {
                var avatarPath = $"{AssignedId}/{AssignedAppearance}";
                if (ActorManager.AvatarTextureExists(avatarPath))
                {
                    if (ActorManager.GetAvatarTexturePathFor(AssignedId) != avatarPath)
                        ActorManager.SetAvatarTexturePathFor(AssignedId, avatarPath);
                }
                else // Check if a default avatar texture for the character exists and assign if it does.
                {
                    var defaultAvatarPath = $"{AssignedId}/Default";
                    if (ActorManager.AvatarTextureExists(defaultAvatarPath) && ActorManager.GetAvatarTexturePathFor(AssignedId) != defaultAvatarPath)
                        ActorManager.SetAvatarTexturePathFor(AssignedId, defaultAvatarPath);
                }
            }
            else // User specified specific avatar texture path, assigning it.
            {
                if (AvatarTexturePath?.Value.EqualsFastIgnoreCase("none") ?? false)
                    ActorManager.RemoveAvatarTextureFor(AssignedId);
                else ActorManager.SetAvatarTexturePathFor(AssignedId, AvatarTexturePath);
            }

            await base.Modify(token); // Wait for modifications after applying the avatar texture to prevent concurrency issues.
        }

        protected override async UniTask ApplyModifications (ICharacterActor actor, EasingType easingType, AsyncToken token)
        {
            var arrange = ShouldAutoArrange(actor);
            using var _ = ListPool<UniTask>.Rent(out var tasks);
            var duration = actor.Visible ? AssignedDuration : 0;
            var tween = new Tween(duration, easingType, complete: !Lazy);
            tasks.Add(base.ApplyModifications(actor, easingType, token));
            tasks.Add(ApplyLookDirectionModification(actor, tween, token));
            if (arrange) tasks.Add(ActorManager.ArrangeCharacters(!AssignedLookDirection.HasValue, new(AssignedDuration, easingType, complete: !Lazy), token));
            await UniTask.WhenAll(tasks);
        }

        protected virtual bool ShouldAutoArrange (ICharacterActor actor)
        {
            if (!Configuration.AutoArrangeOnAdd) return false;
            var addingActor = !actor.Visible && (AssignedVisibility.HasValue && AssignedVisibility.Value || Configuration.AutoShowOnModify);
            var positionAssigned = AssignedPosition != null;
            var renderedToTexture = Configuration.GetMetadataOrDefault(AssignedId).RenderTexture != null;
            return addingActor && !positionAssigned && !renderedToTexture;
        }

        protected virtual async UniTask ApplyLookDirectionModification (ICharacterActor actor, Tween tween, AsyncToken token)
        {
            if (!AssignedLookDirection.HasValue) return;
            if (Mathf.Approximately(tween.Duration, 0)) actor.LookDirection = AssignedLookDirection.Value;
            else await actor.ChangeLookDirection(AssignedLookDirection.Value, tween, token);
        }

        protected virtual CharacterLookDirection? ParseLookDirection (string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (ParseUtils.TryConstantParameter<CharacterLookDirection>(value, out var dir)) return dir;
            Err($"'{value}' is not a valid value for a character look direction; see API guide for '@char' command for the list of supported values.");
            return null;
        }
    }
}
