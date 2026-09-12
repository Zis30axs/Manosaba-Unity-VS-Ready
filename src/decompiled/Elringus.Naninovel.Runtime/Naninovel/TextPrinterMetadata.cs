using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000072")]
public class TextPrinterMetadata : OrthoActorMetadata
{
	[Serializable]
	[Token(Token = "0x2000073")]
	public class Map : ActorMetadataMap<TextPrinterMetadata>
	{
		[Token(Token = "0x60001F9")]
		[Address(RVA = "0x45A080", Offset = "0x458A80", VA = "0x18045A080")]
		public Map()
		{
		}
	}

	[Token(Token = "0x4000180")]
	[FieldOffset(Offset = "0x70")]
	[Tooltip("Whether to automatically reset the printer on each `@print` command (unless `reset` parameter is explicitly disabled).")]
	public bool AutoReset;

	[Token(Token = "0x4000181")]
	[FieldOffset(Offset = "0x71")]
	[Tooltip("Whether to automatically make the printer default and hide other printers on each `@print` command (unless `default` parameter is explicitly disabled).")]
	public bool AutoDefault;

	[Token(Token = "0x4000182")]
	[FieldOffset(Offset = "0x72")]
	[Tooltip("Whether to automatically wait for user input on each `@print` command (unless `waitInput` parameter is explicitly disabled).")]
	public bool AutoWait;

	[Token(Token = "0x4000183")]
	[FieldOffset(Offset = "0x73")]
	[Tooltip("Controls whether to wait for user input after text reveal was skipped. When disabled, will not wait, no matter the `Auto Wait` option or `waitInput` parameter.")]
	public bool WaitAfterRevealSkip;

	[Token(Token = "0x4000184")]
	[FieldOffset(Offset = "0x74")]
	[Tooltip("Whether to add printed messages to the printer backlog.")]
	public bool AddToBacklog;

	[Token(Token = "0x4000185")]
	[FieldOffset(Offset = "0x75")]
	[Tooltip("Whether to stop any playing voices on each `@print` command.")]
	public bool StopVoice;

	[Token(Token = "0x4000186")]
	[FieldOffset(Offset = "0x78")]
	[Tooltip("Default visibility change animation duration; used when corresponding parameter is not specified in script command.")]
	public float ChangeVisibilityDuration;

	[Token(Token = "0x4000187")]
	[FieldOffset(Offset = "0x7C")]
	[Tooltip("Whether to wait until the printer is fully visible before starting to print the text.")]
	public bool WaitVisibilityBeforePrint;

	[Token(Token = "0x4000188")]
	[FieldOffset(Offset = "0x80")]
	[Tooltip("Number of frames to wait before completing @print command. A value greater than zero is required to make the printed text visible while in skip mode.")]
	public int PrintFrameDelay;

	[Token(Token = "0x4000189")]
	[FieldOffset(Offset = "0x84")]
	[Tooltip("Whether to always show printed text instantly, no matter the reveal speed setting.")]
	public bool RevealInstantly;

	[Token(Token = "0x60001F7")]
	[Address(RVA = "0x459E50", Offset = "0x458850", VA = "0x180459E50")]
	public TextPrinterMetadata()
	{
	}

	[Token(Token = "0x60001F8")]
	public override ActorPose<TState> GetPose<TState>(string poseName)
	{
		return null;
	}
}
