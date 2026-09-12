using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004CD")]
[EditInProjectSettings]
public class UIConfiguration : Configuration
{
	[Serializable]
	[Token(Token = "0x20004CE")]
	public class FontOption
	{
		[Token(Token = "0x4001248")]
		[FieldOffset(Offset = "0x10")]
		[Tooltip("Name of the font option. Will be displayed in the font settings dropdown list.")]
		public string FontName;

		[Token(Token = "0x4001249")]
		[FieldOffset(Offset = "0x18")]
		[Tooltip("Resource path to the font asset to apply for the affected text components when the option is selected.")]
		public string FontResource;

		[Token(Token = "0x400124A")]
		[FieldOffset(Offset = "0x20")]
		[LocalesPopup(true)]
		[Tooltip("When not empty, will auto-apply the font when the locale (language) is selected.")]
		public string ApplyOnLocale;

		[Token(Token = "0x60017FF")]
		[Address(RVA = "0x5FD020", Offset = "0x5FBA20", VA = "0x1805FD020")]
		public FontOption()
		{
		}
	}

	[Token(Token = "0x4001240")]
	public const string DefaultUIPathPrefix = "UI";

	[Token(Token = "0x4001241")]
	public const string DefaultFontPathPrefix = "Fonts";

	[Token(Token = "0x4001242")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Configuration of the resource loader used with UI resources.")]
	public ResourceLoaderConfiguration UILoader;

	[Token(Token = "0x4001243")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Configuration of the resource loader used with font resources.")]
	public ResourceLoaderConfiguration FontLoader;

	[Token(Token = "0x4001244")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Whether to assign a specific layer to all the UI objects managed by the engine. Required for some of the built-in features, eg `Toggle UI`.")]
	public bool OverrideObjectsLayer;

	[Token(Token = "0x4001245")]
	[FieldOffset(Offset = "0x2C")]
	[Tooltip("When `Override Objects Layer` is enabled, the specified layer will be assigned to all the managed UI objects.")]
	public int ObjectsLayer;

	[Token(Token = "0x4001246")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Font options, that should be available in the game settings UI (in addition to `Default`) for the player to choose from.")]
	public List<FontOption> FontOptions;

	[Token(Token = "0x4001247")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("Font name from `Font Options` to apply by default when the game is first started. When not specified, `Default` font is applied.")]
	public string DefaultFont;

	[Token(Token = "0x60017FD")]
	[Address(RVA = "0x5FCC10", Offset = "0x5FB610", VA = "0x1805FCC10")]
	public FontOption GetFontOption(string fontName)
	{
		return null;
	}

	[Token(Token = "0x60017FE")]
	[Address(RVA = "0x5FCD60", Offset = "0x5FB760", VA = "0x1805FCD60")]
	public UIConfiguration()
	{
	}
}
