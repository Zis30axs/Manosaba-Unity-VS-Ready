using Il2CppDummyDll;
using UnityEngine.EventSystems;

namespace Naninovel;

[Token(Token = "0x20002C0")]
public abstract class ScriptableUIComponent<T> : ScriptableUIBehaviour where T : UIBehaviour
{
	[Token(Token = "0x4000AC2")]
	[FieldOffset(Offset = "0x0")]
	private T uiComponent;

	[Token(Token = "0x170001D8")]
	public virtual T UIComponent
	{
		[Token(Token = "0x6000D25")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000D26")]
	protected ScriptableUIComponent()
	{
	}
}
