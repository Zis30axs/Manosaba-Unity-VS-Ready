using System;
using UnityEditor;

namespace Naninovel
{
    public class CharacterMetadataEditor : OrthoMetadataEditor<ICharacterActor, CharacterMetadata>
    {
        protected override Action<SerializedProperty> GetCustomDrawer (string propertyName) => propertyName switch {
            nameof(CharacterMetadata.BakedLookDirection) => DrawWhen(HasResources),
            nameof(CharacterMetadata.DisplayName) => DrawWhen(Metadata.HasName),
            nameof(CharacterMetadata.NameColor) => DrawWhen(Metadata.UseCharacterColor),
            nameof(CharacterMetadata.MessageColor) => DrawWhen(Metadata.UseCharacterColor),
            nameof(CharacterMetadata.HighlightWhenSpeaking) => DrawWhen(HasResources),
            nameof(CharacterMetadata.HighlightCharacterCount) => DrawWhen(HasResources && Metadata.HighlightWhenSpeaking),
            nameof(CharacterMetadata.SpeakingPose) => DrawWhen(HasResources && Metadata.HighlightWhenSpeaking),
            nameof(CharacterMetadata.NotSpeakingPose) => DrawWhen(HasResources && Metadata.HighlightWhenSpeaking),
            nameof(CharacterMetadata.PlaceOnTop) => DrawWhen(HasResources && Metadata.HighlightWhenSpeaking),
            nameof(CharacterMetadata.HighlightDuration) => DrawWhen(HasResources && Metadata.HighlightWhenSpeaking),
            nameof(CharacterMetadata.HighlightEasing) => DrawWhen(HasResources && Metadata.HighlightWhenSpeaking),
            nameof(CharacterMetadata.MessageSoundPlayback) => DrawWhen(!string.IsNullOrEmpty(Metadata.MessageSound)),
            nameof(CharacterMetadata.VoiceSource) => DrawWhen(HasResources),
            nameof(CharacterMetadata.Poses) => DrawWhen(HasResources, ActorPosesEditor.Draw),
            _ => base.GetCustomDrawer(propertyName)
        };
    }
}
