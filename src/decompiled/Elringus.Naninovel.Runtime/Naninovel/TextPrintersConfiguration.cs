using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000074")]
[EditInProjectSettings]
public class TextPrintersConfiguration : OrthoActorManagerConfiguration<TextPrinterMetadata>
{
	[Token(Token = "0x400018A")]
	public const string DefaultPathPrefix = "TextPrinters";

	[Token(Token = "0x400018B")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("ID of the text printer to use by default.")]
	public string DefaultPrinterId;

	[Token(Token = "0x400018C")]
	[FieldOffset(Offset = "0x40")]
	[Range(0f, 1f)]
	[Tooltip("Base reveal speed (game settings) to set when the game is first started.")]
	public float DefaultBaseRevealSpeed;

	[Token(Token = "0x400018D")]
	[FieldOffset(Offset = "0x44")]
	[Range(0f, 1f)]
	[Tooltip("Base auto delay (game settings) to set when the game is first started.")]
	public float DefaultBaseAutoDelay;

	[Token(Token = "0x400018E")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("Delay limit (in seconds) when revealing (printing) the text messages. Specific reveal speed is set via `message speed` in the game settings; this value defines the available range (higher the value, lower the reveal speed).")]
	[Range(0.01f, 1f)]
	public float MaxRevealDelay;

	[Token(Token = "0x400018F")]
	[FieldOffset(Offset = "0x4C")]
	[Tooltip("Delay limit (in seconds) per each printed character while waiting to continue in auto play mode. Specific delay is set via `auto delay` in the game settings; this value defines the available range.")]
	[Range(0f, 0.5f)]
	public float MaxAutoWaitDelay;

	[Token(Token = "0x4000190")]
	[FieldOffset(Offset = "0x50")]
	[Tooltip("Whether to scale the wait time in auto play mode by the reveal speed set in the print commands.")]
	public bool ScaleAutoWait;

	[Token(Token = "0x4000191")]
	[FieldOffset(Offset = "0x58")]
	[Tooltip("Metadata to use by default when creating text printer actors and custom metadata for the created actor ID doesn't exist.")]
	public TextPrinterMetadata DefaultMetadata;

	[Token(Token = "0x4000192")]
	[FieldOffset(Offset = "0x60")]
	[Tooltip("Metadata to use when creating text printer actors with specific IDs.")]
	public TextPrinterMetadata.Map Metadata;

	[Token(Token = "0x17000057")]
	public override TextPrinterMetadata DefaultActorMetadata
	{
		[Token(Token = "0x60001FA")]
		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000058")]
	public override ActorMetadataMap<TextPrinterMetadata> ActorMetadataMap
	{
		[Token(Token = "0x60001FB")]
		[Address(RVA = "0x45A0C0", Offset = "0x458AC0", VA = "0x18045A0C0", Slot = "8")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001FC")]
	[Address(RVA = "0x45A0D0", Offset = "0x458AD0", VA = "0x18045A0D0")]
	public TextPrintersConfiguration()
	{
	}

	[Token(Token = "0x60001FD")]
	protected override ActorPose<TState> GetSharedPose<TState>(string poseName)
	{
		return null;
	}

	[Token(Token = "0x60001FE")]
	[Address(RVA = "0x45A470", Offset = "0x458E70", VA = "0x18045A470")]
	private static TextPrinterMetadata CreateBuiltinMeta(bool reset = true)
	{
		return null;
	}
}
