using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX
{
    [Token(Token = "0x20005EE")]
    public class DigitalGlitch : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable
    {
    	[Token(Token = "0x20005EF")]
    	private class CameraComponent : MonoBehaviour
    	{
    		[Token(Token = "0x4001701")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Shader Shader;

    		[Token(Token = "0x4001702")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public Texture GlitchTexture;

    		[Token(Token = "0x4001703")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float Intensity;

    		[Token(Token = "0x4001704")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    		public bool RandomGlitchFrequency;

    		[Token(Token = "0x4001705")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public float GlitchFrequency;

    		[Token(Token = "0x4001706")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    		public bool PerformUVShifting;

    		[Token(Token = "0x4001707")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public float ShiftAmount;

    		[Token(Token = "0x4001708")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    		public bool PerformScreenShifting;

    		[Token(Token = "0x4001709")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x45")]
    		public bool PerformColorShifting;

    		[Token(Token = "0x400170A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		public Color TintColor;

    		[Token(Token = "0x400170B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		public bool BurnColors;

    		[Token(Token = "0x400170C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x59")]
    		public bool DodgeColors;

    		[Token(Token = "0x400170D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x5C")]
    		private float glitchUp;

    		[Token(Token = "0x400170E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		private float glitchDown;

    		[Token(Token = "0x400170F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x64")]
    		private float flicker;

    		[Token(Token = "0x4001710")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    		private float glitchUpTime;

    		[Token(Token = "0x4001711")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x6C")]
    		private float glitchDownTime;

    		[Token(Token = "0x4001712")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    		private float flickerTime;

    		[Token(Token = "0x4001713")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    		private Material material;

    		[Token(Token = "0x17000468")]
    		private Material Material
    		{
    			[Token(Token = "0x6001DC4")]
    			[Address(RVA = "0x4BBE10", Offset = "0x4BA810", VA = "0x1804BBE10")]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x6001DC5")]
    		[Address(RVA = "0x4BC090", Offset = "0x4BAA90", VA = "0x1804BC090")]
    		private void Start()
    		{
    		}

    		[Token(Token = "0x6001DC6")]
    		[Address(RVA = "0x4BC300", Offset = "0x4BAD00", VA = "0x1804BC300")]
    		private void OnDisable()
    		{
    		}

    		[Token(Token = "0x6001DC7")]
    		[Address(RVA = "0x4BC430", Offset = "0x4BAE30", VA = "0x1804BC430")]
    		private void OnRenderImage(RenderTexture source, RenderTexture destination)
    		{
    		}

    		[Token(Token = "0x6001DC8")]
    		[Address(RVA = "0x4BD040", Offset = "0x4BBA40", VA = "0x1804BD040")]
    		public CameraComponent()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005F0")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__32
    {
    		[Token(Token = "0x4001714")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001715")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001716")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DigitalGlitch _003C_003E4__this;

    		[Token(Token = "0x4001717")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001718")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001DC9")]
    		[Address(RVA = "0x4BD0D0", Offset = "0x4BBAD0", VA = "0x1804BD0D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001DCA")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40016F0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Shader glitchShader;

    	[Token(Token = "0x40016F1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private Texture glitchTexture;

    	[Token(Token = "0x40016F2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private float defaultDuration;

    	[Token(Token = "0x40016F3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	[SerializeField]
    	private float defaultIntensity;

    	[Token(Token = "0x40016F4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly int glitchTex;

    	[Token(Token = "0x40016F5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
    	private static readonly int intensity;

    	[Token(Token = "0x40016F6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private static readonly int colorTint;

    	[Token(Token = "0x40016F7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC")]
    	private static readonly int burnColors;

    	[Token(Token = "0x40016F8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private static readonly int dodgeColors;

    	[Token(Token = "0x40016F9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
    	private static readonly int performUVShifting;

    	[Token(Token = "0x40016FA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private static readonly int performColorShifting;

    	[Token(Token = "0x40016FB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
    	private static readonly int performScreenShifting;

    	[Token(Token = "0x40016FC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private static readonly int filterRadius;

    	[Token(Token = "0x40016FD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    	private static readonly int flipUp;

    	[Token(Token = "0x40016FE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private static readonly int flipDown;

    	[Token(Token = "0x40016FF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    	private static readonly int displace;

    	[Token(Token = "0x4001700")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private CameraComponent cameraComponent;

    	[Token(Token = "0x17000464")]
    	protected float Duration
    	{
    		[Token(Token = "0x6001DB8")]
    		[Address(RVA = "0x4B8670", Offset = "0x4B7070", VA = "0x1804B8670")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DB9")]
    		[Address(RVA = "0x4B8680", Offset = "0x4B7080", VA = "0x1804B8680")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000465")]
    	protected float Intensity
    	{
    		[Token(Token = "0x6001DBA")]
    		[Address(RVA = "0x4B8690", Offset = "0x4B7090", VA = "0x1804B8690")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DBB")]
    		[Address(RVA = "0x4B86A0", Offset = "0x4B70A0", VA = "0x1804B86A0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000466")]
    	protected ISpawnManager SpawnManager
    	{
    		[Token(Token = "0x6001DBC")]
    		[Address(RVA = "0x4BB3E0", Offset = "0x4B9DE0", VA = "0x1804BB3E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000467")]
    	protected virtual string SpawnedPath
    	{
    		[Token(Token = "0x6001DBD")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "6")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001DBE")]
    		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6001DBF")]
    	[Address(RVA = "0x4BB440", Offset = "0x4B9E40", VA = "0x1804BB440", Slot = "7")]
    	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6001DC0")]
    	[Address(RVA = "0x4BB890", Offset = "0x4BA290", VA = "0x1804BB890", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__32))]
    	public UniTask AwaitSpawn([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001DC1")]
    	[Address(RVA = "0x4BBA10", Offset = "0x4BA410", VA = "0x1804BBA10")]
    	private void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001DC2")]
    	[Address(RVA = "0x4BBB40", Offset = "0x4BA540", VA = "0x1804BBB40")]
    	public DigitalGlitch()
    	{
    	}
    }
}
