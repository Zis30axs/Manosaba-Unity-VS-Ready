using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel
{
    [Token(Token = "0x2000203")]
    public class AudioTrack : IAudioTrack
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000204")]
    	[CompilerGenerated]
    	private struct _003CFade_003Ed__35
    {
    		[Token(Token = "0x400088E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400088F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000890")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AudioTrack _003C_003E4__this;

    		[Token(Token = "0x4000891")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float volume;

    		[Token(Token = "0x4000892")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float fadeTime;

    		[Token(Token = "0x4000893")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4000894")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000919")]
    		[Address(RVA = "0x524960", Offset = "0x523360", VA = "0x180524960", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600091A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000205")]
    	[CompilerGenerated]
    	private struct _003CPlay_003Ed__32
    {
    		[Token(Token = "0x4000895")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000896")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000897")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AudioTrack _003C_003E4__this;

    		[Token(Token = "0x4000898")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float fadeInTime;

    		[Token(Token = "0x4000899")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x400089A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600091B")]
    		[Address(RVA = "0x524DB0", Offset = "0x5237B0", VA = "0x180524DB0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600091C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000206")]
    	[CompilerGenerated]
    	private struct _003CStop_003Ed__34
    {
    		[Token(Token = "0x400089B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400089C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400089D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AudioTrack _003C_003E4__this;

    		[Token(Token = "0x400089E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float fadeOutTime;

    		[Token(Token = "0x400089F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x40008A0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600091D")]
    		[Address(RVA = "0x525210", Offset = "0x523C10", VA = "0x180525210", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600091E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400088C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly Tweener<FloatTween> volumeTweener;

    	[Token(Token = "0x400088D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private readonly Timer stopTimer;

    	[Token(Token = "0x17000144")]
    	public AudioClip Clip
    	{
    		[Token(Token = "0x6000903")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000145")]
    	public AudioClip IntroClip
    	{
    		[Token(Token = "0x6000904")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000146")]
    	public AudioSource Source
    	{
    		[Token(Token = "0x6000905")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000147")]
    	public bool Valid
    	{
    		[Token(Token = "0x6000906")]
    		[Address(RVA = "0x5238A0", Offset = "0x5222A0", VA = "0x1805238A0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000148")]
    	public bool Loop
    	{
    		[Token(Token = "0x6000907")]
    		[Address(RVA = "0x523A20", Offset = "0x522420", VA = "0x180523A20", Slot = "9")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000908")]
    		[Address(RVA = "0x523AD0", Offset = "0x5224D0", VA = "0x180523AD0", Slot = "10")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000149")]
    	public bool Playing
    	{
    		[Token(Token = "0x6000909")]
    		[Address(RVA = "0x523B20", Offset = "0x522520", VA = "0x180523B20", Slot = "8")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700014A")]
    	public bool Mute
    	{
    		[Token(Token = "0x600090A")]
    		[Address(RVA = "0x523BD0", Offset = "0x5225D0", VA = "0x180523BD0", Slot = "11")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600090B")]
    		[Address(RVA = "0x523C80", Offset = "0x522680", VA = "0x180523C80", Slot = "12")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700014B")]
    	public float Volume
    	{
    		[Token(Token = "0x600090C")]
    		[Address(RVA = "0x523CD0", Offset = "0x5226D0", VA = "0x180523CD0", Slot = "13")]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x600090D")]
    		[Address(RVA = "0x523D10", Offset = "0x522710", VA = "0x180523D10", Slot = "14")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x14000014")]
    	public event Action OnPlay
    	{
    		[Token(Token = "0x60008FF")]
    		[Address(RVA = "0x5234E0", Offset = "0x521EE0", VA = "0x1805234E0", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000900")]
    		[Address(RVA = "0x5235D0", Offset = "0x521FD0", VA = "0x1805235D0", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000015")]
    	public event Action OnStop
    	{
    		[Token(Token = "0x6000901")]
    		[Address(RVA = "0x5236C0", Offset = "0x5220C0", VA = "0x1805236C0", Slot = "6")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000902")]
    		[Address(RVA = "0x5237B0", Offset = "0x5221B0", VA = "0x1805237B0", Slot = "7")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x600090E")]
    	[Address(RVA = "0x523D60", Offset = "0x522760", VA = "0x180523D60")]
    	public AudioTrack(AudioClip clip, AudioSource source, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default)
    	{
    	}

    	[Token(Token = "0x600090F")]
    	[Address(RVA = "0x524070", Offset = "0x522A70", VA = "0x180524070", Slot = "15")]
    	public void Play()
    	{
    	}

    	[Token(Token = "0x6000910")]
    	[Address(RVA = "0x524240", Offset = "0x522C40", VA = "0x180524240")]
    	[AsyncStateMachine(typeof(_003CPlay_003Ed__32))]
    	public UniTask Play(float fadeInTime, AsyncToken token = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000911")]
    	[Address(RVA = "0x5243E0", Offset = "0x522DE0", VA = "0x1805243E0", Slot = "16")]
    	public void Stop()
    	{
    	}

    	[Token(Token = "0x6000912")]
    	[Address(RVA = "0x5244C0", Offset = "0x522EC0", VA = "0x1805244C0")]
    	[AsyncStateMachine(typeof(_003CStop_003Ed__34))]
    	public UniTask Stop(float fadeOutTime, AsyncToken token = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000913")]
    	[Address(RVA = "0x524660", Offset = "0x523060", VA = "0x180524660", Slot = "17")]
    	[AsyncStateMachine(typeof(_003CFade_003Ed__35))]
    	public UniTask Fade(float volume, float fadeTime, AsyncToken token = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000914")]
    	[Address(RVA = "0x524810", Offset = "0x523210", VA = "0x180524810")]
    	private void CompleteAllRunners()
    	{
    	}

    	[Token(Token = "0x6000915")]
    	[Address(RVA = "0x5248D0", Offset = "0x5232D0", VA = "0x1805248D0")]
    	private void InvokeOnStop()
    	{
    	}
    }
}
