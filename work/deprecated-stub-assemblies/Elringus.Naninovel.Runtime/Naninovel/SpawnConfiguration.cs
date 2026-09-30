using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200045C")]
    [EditInProjectSettings]
    public class SpawnConfiguration : Configuration
    {
    	[Token(Token = "0x4001038")]
    	public const string DefaultPathPrefix = "Spawn";

    	[Token(Token = "0x4001039")]
    	public const string IdDelimiter = "#";

    	[Token(Token = "0x400103A")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Configuration of the resource loader used with spawn resources.")]
    	public ResourceLoaderConfiguration Loader;

    	[Token(Token = "0x600158A")]
    	[Address(RVA = "0x5D3870", Offset = "0x5D2270", VA = "0x1805D3870")]
    	public static string ProcessInputPath(string input, out string id)
    	{
    		id = default;
    		id = default;
    		return null;
    	}

    	[Token(Token = "0x600158B")]
    	[Address(RVA = "0x5D3D80", Offset = "0x5D2780", VA = "0x1805D3D80")]
    	public SpawnConfiguration()
    	{
    	}
    }
}
