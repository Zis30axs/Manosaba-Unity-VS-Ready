using Il2CppDummyDll;
using UnityEngine.EventSystems;

namespace Naninovel;

[Token(Token = "0x20002C1")]
public abstract class ScriptableUIControl<T> : ScriptableUIComponent<T> where T : UIBehaviour
{
	[Token(Token = "0x6000D27")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6000D28")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6000D29")]
	protected abstract void BindUIEvents();

	[Token(Token = "0x6000D2A")]
	protected abstract void UnbindUIEvents();

	[Token(Token = "0x6000D2B")]
	protected ScriptableUIControl()
	{
	}
}
