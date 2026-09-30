using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Serialization;

namespace Naninovel
{
    [Token(Token = "0x200041C")]
    public class PlayScript : MonoBehaviour
    {
    	[Token(Token = "0x4000F08")]
    	[FieldOffset(Offset = "0x20")]
    	[FormerlySerializedAs("scriptName")]
    	[Tooltip("Local resource path of a script asset to play.")]
    	[ResourcePopup("Scripts", "Scripts", "None (play script text)")]
    	[SerializeField]
    	private string scriptPath;

    	[Token(Token = "0x4000F09")]
    	[FieldOffset(Offset = "0x28")]
    	[TextArea(3, 10)]
    	[Tooltip("The naninovel script text (commands) to execute; has no effect when 'Script Path' is specified. Argument of the event (if any) can be referenced in the script text via '{arg}' expression. Conditional block commands (if, else, etc) are not supported.")]
    	[SerializeField]
    	private string scriptText;

    	[Token(Token = "0x4000F0A")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Whether to automatically play the script when the game object is instantiated.")]
    	[SerializeField]
    	private bool playOnAwake;

    	[Token(Token = "0x4000F0B")]
    	[FieldOffset(Offset = "0x31")]
    	[Tooltip("Whether to disable waiting for input mode when the script is played.")]
    	[SerializeField]
    	private bool disableWaitInput;

    	[Token(Token = "0x4000F0C")]
    	[FieldOffset(Offset = "0x38")]
    	private string argument;

    	[Token(Token = "0x170002C6")]
    	protected virtual string ScriptPath
    	{
    		[Token(Token = "0x6001400")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002C7")]
    	protected virtual string ScriptText
    	{
    		[Token(Token = "0x6001401")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "5")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002C8")]
    	protected virtual bool PlayOnAwake
    	{
    		[Token(Token = "0x6001402")]
    		[Address(RVA = "0x43E270", Offset = "0x43CC70", VA = "0x18043E270", Slot = "6")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170002C9")]
    	protected virtual bool DisableWaitInput
    	{
    		[Token(Token = "0x6001403")]
    		[Address(RVA = "0x5405B0", Offset = "0x53EFB0", VA = "0x1805405B0", Slot = "7")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001404")]
    	[Address(RVA = "0x5B4B10", Offset = "0x5B3510", VA = "0x1805B4B10", Slot = "8")]
    	public virtual void Play()
    	{
    	}

    	[Token(Token = "0x6001405")]
    	[Address(RVA = "0x5B4B80", Offset = "0x5B3580", VA = "0x1805B4B80", Slot = "9")]
    	public virtual void Play(string argument)
    	{
    	}

    	[Token(Token = "0x6001406")]
    	[Address(RVA = "0x5B4BF0", Offset = "0x5B35F0", VA = "0x1805B4BF0", Slot = "10")]
    	public virtual void Play(float argument)
    	{
    	}

    	[Token(Token = "0x6001407")]
    	[Address(RVA = "0x5B4D50", Offset = "0x5B3750", VA = "0x1805B4D50", Slot = "11")]
    	public virtual void Play(int argument)
    	{
    	}

    	[Token(Token = "0x6001408")]
    	[Address(RVA = "0x5B4E50", Offset = "0x5B3850", VA = "0x1805B4E50", Slot = "12")]
    	public virtual void Play(bool argument)
    	{
    	}

    	[Token(Token = "0x6001409")]
    	[Address(RVA = "0x5B4FC0", Offset = "0x5B39C0", VA = "0x1805B4FC0", Slot = "13")]
    	protected virtual void Awake()
    	{
    	}

    	[Token(Token = "0x600140A")]
    	[Address(RVA = "0x5B5010", Offset = "0x5B3A10", VA = "0x1805B5010", Slot = "14")]
    	protected virtual void DoPlayScript()
    	{
    	}

    	[Token(Token = "0x600140B")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public PlayScript()
    	{
    	}
    }
}
