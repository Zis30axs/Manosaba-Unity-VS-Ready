using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200003D")]
public class CharacterMetadata : OrthoActorMetadata
{
	[Serializable]
	[Token(Token = "0x200003E")]
	public class Map : ActorMetadataMap<CharacterMetadata>
	{
		[Token(Token = "0x60000F0")]
		[Address(RVA = "0x453400", Offset = "0x451E00", VA = "0x180453400")]
		public Map()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x200003F")]
	public class Pose : ActorPose<CharacterState>
	{
		[Token(Token = "0x60000F1")]
		[Address(RVA = "0x453440", Offset = "0x451E40", VA = "0x180453440")]
		public Pose()
		{
		}
	}

	[Token(Token = "0x40000D2")]
	[FieldOffset(Offset = "0x70")]
	[Tooltip("Look direction as portrait (baked) on the character texture; required to properly flip characters to make them 'face' the correct side of the scene.")]
	public CharacterLookDirection BakedLookDirection;

	[Token(Token = "0x40000D3")]
	[FieldOffset(Offset = "0x74")]
	[Tooltip("When enabled, character's 'Display Name' will be shown in printer UI when character is the author of the printed text.")]
	public bool HasName;

	[Token(Token = "0x40000D4")]
	[FieldOffset(Offset = "0x78")]
	[Tooltip("Full name of the character to display in text printers when character is the author of the printed text. Will use character ID when not specified.\nIt's possible to localize the display names or bind them to a custom variable (and dynamically change throughout the game); see the guide on `Characters` -> `Display Names` for more info.")]
	public string DisplayName;

	[Token(Token = "0x40000D5")]
	[FieldOffset(Offset = "0x80")]
	[Tooltip("Whether to apply character-specific color to printer messages and name label UI.")]
	public bool UseCharacterColor;

	[Token(Token = "0x40000D6")]
	[FieldOffset(Offset = "0x84")]
	[Tooltip("Character-specific color to tint printer name label UI.")]
	public Color NameColor;

	[Token(Token = "0x40000D7")]
	[FieldOffset(Offset = "0x94")]
	[Tooltip("Character-specific color to tint printer messages.")]
	public Color MessageColor;

	[Token(Token = "0x40000D8")]
	[FieldOffset(Offset = "0xA4")]
	[Tooltip("When enabled, will apply specified poses based on whether this actor is the author of the last printed text.")]
	public bool HighlightWhenSpeaking;

	[Token(Token = "0x40000D9")]
	[FieldOffset(Offset = "0xA8")]
	[Tooltip("Highlight will happen only when the specified number (or more) of characters are visible on scene. Leave zero to highlight no matter the visible characters count.")]
	public int HighlightCharacterCount;

	[Token(Token = "0x40000DA")]
	[FieldOffset(Offset = "0xB0")]
	[Tooltip("Name of the pose to apply when the character is speaking. Leave empty to not apply any pose.")]
	public string SpeakingPose;

	[Token(Token = "0x40000DB")]
	[FieldOffset(Offset = "0xB8")]
	[Tooltip("Name of the pose to apply when the character is not speaking. Leave empty to not apply any pose.")]
	public string NotSpeakingPose;

	[Token(Token = "0x40000DC")]
	[FieldOffset(Offset = "0xC0")]
	[Tooltip("Whether to also move the highlighted character to the topmost position (closer to the camera over z-axis).")]
	public bool PlaceOnTop;

	[Token(Token = "0x40000DD")]
	[FieldOffset(Offset = "0xC4")]
	[Tooltip("The highlight pose animation duration.")]
	public float HighlightDuration;

	[Token(Token = "0x40000DE")]
	[FieldOffset(Offset = "0xC8")]
	[Tooltip("The highlight pose animation easing.")]
	public EasingType HighlightEasing;

	[Token(Token = "0x40000DF")]
	[FieldOffset(Offset = "0xD0")]
	[Tooltip("Path to the sound (SFX) to play when printing (revealing) messages and the character is author. The sound will be played on each character reveal, so make sure it's very short and sharp (without any pause/silence at the beginning of the audio clip).")]
	[ResourcePopup("Audio")]
	public string MessageSound;

	[Token(Token = "0x40000E0")]
	[FieldOffset(Offset = "0xD8")]
	[Tooltip("When `Message Sound` is assigned, controls the playback type:\n • Looped — Play the sound in loop, stop when the message is fully revealed.\n • One Shot — Play the sound from start for each revealed character in the message.\n • One Shot Clipped — Same as `One Shot`, but restart the sound in case it's still playing while next character is revealed.")]
	public MessageSoundPlayback MessageSoundPlayback;

	[Token(Token = "0x40000E1")]
	[FieldOffset(Offset = "0xE0")]
	[Tooltip("When assigned, will instantiate the game object under the character object and use the attached audio source component to play voice clips associated with the character.")]
	public AudioSource VoiceSource;

	[Token(Token = "0x40000E2")]
	[FieldOffset(Offset = "0xE8")]
	[Tooltip("When the character is an author of a printed message, selected text printer will automatically be used to handle the printing. Only custom printers are allowed.")]
	[ActorPopup("TextPrinters")]
	public string LinkedPrinter;

	[Token(Token = "0x40000E3")]
	[FieldOffset(Offset = "0xF0")]
	[Tooltip("Named states (poses) of the character; pose name can be used as appearance in `@char` commands to set enabled properties of the associated state.")]
	public List<Pose> Poses;

	[Token(Token = "0x60000EE")]
	[Address(RVA = "0x453120", Offset = "0x451B20", VA = "0x180453120")]
	public CharacterMetadata()
	{
	}

	[Token(Token = "0x60000EF")]
	public override ActorPose<TState> GetPose<TState>(string poseName)
	{
		return null;
	}
}
