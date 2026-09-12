using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20003A5")]
[EditInProjectSettings]
public class ManagedTextConfiguration : Configuration
{
	[Token(Token = "0x4000D93")]
	public const string DefaultPathPrefix = "Text";

	[Token(Token = "0x4000D94")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Configuration of the resource loader used with the managed text documents.")]
	public ResourceLoaderConfiguration Loader;

	[Token(Token = "0x4000D95")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Local resource paths of the managed text documents for which to use multiline format.")]
	public string[] MultilineDocuments;

	[Token(Token = "0x60011AD")]
	[Address(RVA = "0x591A60", Offset = "0x590460", VA = "0x180591A60", Slot = "4")]
	public virtual bool IsMultilineDocument(string documentPath)
	{
		return default(bool);
	}

	[Token(Token = "0x60011AE")]
	[Address(RVA = "0x591BB0", Offset = "0x5905B0", VA = "0x180591BB0")]
	public ManagedTextConfiguration()
	{
	}
}
