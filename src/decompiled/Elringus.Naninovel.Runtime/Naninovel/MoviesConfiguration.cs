using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20003B9")]
[EditInProjectSettings]
public class MoviesConfiguration : Configuration
{
	[Token(Token = "0x4000DEC")]
	public const string DefaultPathPrefix = "Movies";

	[Token(Token = "0x4000DED")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Configuration of the resource loader used with movie resources.")]
	public ResourceLoaderConfiguration Loader;

	[Token(Token = "0x4000DEE")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Whether to skip movie playback when user activates `cancel` input keys.")]
	public bool SkipOnInput;

	[Token(Token = "0x4000DEF")]
	[FieldOffset(Offset = "0x21")]
	[Tooltip("Whether to skip frames to catch up with current time.")]
	public bool SkipFrames;

	[Token(Token = "0x4000DF0")]
	[FieldOffset(Offset = "0x24")]
	[Tooltip("Time in seconds to fade in/out before starting/finishing playing the movie.")]
	public float FadeDuration;

	[Token(Token = "0x4000DF1")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Texture to show while fading. Will use a simple black texture when not specified.")]
	public Texture2D CustomFadeTexture;

	[Token(Token = "0x4000DF2")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Whether to automatically play a movie after engine initialization and before showing the main menu.")]
	public bool PlayIntroMovie;

	[Token(Token = "0x4000DF3")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("Path to the intro movie resource.")]
	[ResourcePopup("Movies", "Movies", null)]
	public string IntroMovieName;

	[Token(Token = "0x600120F")]
	[Address(RVA = "0x599360", Offset = "0x597D60", VA = "0x180599360")]
	public MoviesConfiguration()
	{
	}
}
