#nullable enable annotations
﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20004A2")]
    public abstract class TransitionalRenderer : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20004A3")]
    	[CompilerGenerated]
    	private struct _003CBlur_003Ed__50
    {
    		[Token(Token = "0x4001165")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001166")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001167")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x4001168")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Tween tween;

    		[Token(Token = "0x4001169")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public float intensity;

    		[Token(Token = "0x400116A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x400116B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600172A")]
    		[Address(RVA = "0x5EDD40", Offset = "0x5EC740", VA = "0x1805EDD40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600172B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004A4")]
    	[CompilerGenerated]
    	private struct _003CChangeLookDirection_003Ed__53
    {
    		[Token(Token = "0x400116C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400116D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400116E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x400116F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CharacterLookDirection bakedDirection;

    		[Token(Token = "0x4001170")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public CharacterLookDirection direction;

    		[Token(Token = "0x4001171")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public Tween tween;

    		[Token(Token = "0x4001172")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public AsyncToken token;

    		[Token(Token = "0x4001173")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600172C")]
    		[Address(RVA = "0x5EE1E0", Offset = "0x5ECBE0", VA = "0x1805EE1E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600172D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004A5")]
    	[CompilerGenerated]
    	private struct _003CDoFlipX_003Ed__55
    {
    		[Token(Token = "0x4001174")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001175")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001176")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Tween tween;

    		[Token(Token = "0x4001177")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x4001178")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001179")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400117A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600172E")]
    		[Address(RVA = "0x5EE490", Offset = "0x5ECE90", VA = "0x1805EE490", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600172F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004A6")]
    	[CompilerGenerated]
    	private struct _003CFadeIn_003Ed__49
    {
    		[Token(Token = "0x400117B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400117C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400117D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x400117E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Tween tween;

    		[Token(Token = "0x400117F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001180")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001730")]
    		[Address(RVA = "0x5EEA70", Offset = "0x5ED470", VA = "0x1805EEA70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001731")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004A7")]
    	[CompilerGenerated]
    	private struct _003CFadeOut_003Ed__48
    {
    		[Token(Token = "0x4001181")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001182")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001183")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x4001184")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Tween tween;

    		[Token(Token = "0x4001185")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001186")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001732")]
    		[Address(RVA = "0x5EED80", Offset = "0x5ED780", VA = "0x1805EED80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001733")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004A8")]
    	[CompilerGenerated]
    	private struct _003CFadeTo_003Ed__47
    {
    		[Token(Token = "0x4001187")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001188")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001189")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x400118A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Tween tween;

    		[Token(Token = "0x400118B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public float opacity;

    		[Token(Token = "0x400118C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x400118D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001734")]
    		[Address(RVA = "0x5EF090", Offset = "0x5EDA90", VA = "0x1805EF090", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001735")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004A9")]
    	[CompilerGenerated]
    	private struct _003CTintTo_003Ed__46
    {
    		[Token(Token = "0x400118E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400118F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001190")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x4001191")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Tween tween;

    		[Token(Token = "0x4001192")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public Color color;

    		[Token(Token = "0x4001193")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public AsyncToken token;

    		[Token(Token = "0x4001194")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001736")]
    		[Address(RVA = "0x5EF550", Offset = "0x5EDF50", VA = "0x1805EF550", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001737")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004AA")]
    	[CompilerGenerated]
    	private struct _003CTransitionTo_003Ed__45
    {
    		[Token(Token = "0x4001195")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001196")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001197")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionalRenderer _003C_003E4__this;

    		[Token(Token = "0x4001198")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001199")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public Transition? transition;

    		[Token(Token = "0x400119A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		public Tween tween;

    		[Token(Token = "0x400119B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    		public Texture texture;

    		[Token(Token = "0x400119C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400119D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001738")]
    		[Address(RVA = "0x5EFA60", Offset = "0x5EE460", VA = "0x1805EFA60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001739")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400115F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly Tweener<FloatTween> transitionTweener;

    	[Token(Token = "0x4001160")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private readonly Tweener<ColorTween> colorTweener;

    	[Token(Token = "0x4001161")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly Tweener<FloatTween> fadeTweener;

    	[Token(Token = "0x4001162")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private readonly Tweener<FloatTween> blurTweener;

    	[Token(Token = "0x4001163")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private BlurFilter blurFilter;

    	[Token(Token = "0x4001164")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private float opacityLastFrame;

    	[Token(Token = "0x17000343")]
    	public virtual TransitionalMaterial TextureMaterial
    	{
    		[Token(Token = "0x60016FB")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60016FC")]
    		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000344")]
    	public virtual Transition Transition
    	{
    		[Token(Token = "0x60016FD")]
    		[Address(RVA = "0x5EAE50", Offset = "0x5E9850", VA = "0x1805EAE50", Slot = "5")]
    		get
    		{
    			return default(Transition);
    		}
    		[Token(Token = "0x60016FE")]
    		[Address(RVA = "0x5EB060", Offset = "0x5E9A60", VA = "0x1805EB060", Slot = "6")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000345")]
    	public virtual Texture MainTexture
    	{
    		[Token(Token = "0x60016FF")]
    		[Address(RVA = "0x5EB1D0", Offset = "0x5E9BD0", VA = "0x1805EB1D0", Slot = "7")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001700")]
    		[Address(RVA = "0x5EB210", Offset = "0x5E9C10", VA = "0x1805EB210", Slot = "8")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000346")]
    	public virtual Texture TransitionTexture
    	{
    		[Token(Token = "0x6001701")]
    		[Address(RVA = "0x5EB250", Offset = "0x5E9C50", VA = "0x1805EB250", Slot = "9")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001702")]
    		[Address(RVA = "0x5EB2E0", Offset = "0x5E9CE0", VA = "0x1805EB2E0", Slot = "10")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000347")]
    	public virtual float TransitionProgress
    	{
    		[Token(Token = "0x6001703")]
    		[Address(RVA = "0x5EB320", Offset = "0x5E9D20", VA = "0x1805EB320", Slot = "11")]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001704")]
    		[Address(RVA = "0x5EB350", Offset = "0x5E9D50", VA = "0x1805EB350", Slot = "12")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000348")]
    	public virtual Color TintColor
    	{
    		[Token(Token = "0x6001705")]
    		[Address(RVA = "0x5EB3A0", Offset = "0x5E9DA0", VA = "0x1805EB3A0", Slot = "13")]
    		get
    		{
    			return default(Color);
    		}
    		[Token(Token = "0x6001706")]
    		[Address(RVA = "0x5EB450", Offset = "0x5E9E50", VA = "0x1805EB450", Slot = "14")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000349")]
    	public virtual float Opacity
    	{
    		[Token(Token = "0x6001707")]
    		[Address(RVA = "0x5EB4A0", Offset = "0x5E9EA0", VA = "0x1805EB4A0", Slot = "15")]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001708")]
    		[Address(RVA = "0x5EB540", Offset = "0x5E9F40", VA = "0x1805EB540", Slot = "16")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700034A")]
    	public virtual bool FlipX
    	{
    		[Token(Token = "0x6001709")]
    		[Address(RVA = "0x456330", Offset = "0x454D30", VA = "0x180456330", Slot = "17")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600170A")]
    		[Address(RVA = "0x4B1840", Offset = "0x4B0240", VA = "0x1804B1840", Slot = "18")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700034B")]
    	public virtual bool FlipY
    	{
    		[Token(Token = "0x600170B")]
    		[Address(RVA = "0x45E0D0", Offset = "0x45CAD0", VA = "0x18045E0D0", Slot = "19")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600170C")]
    		[Address(RVA = "0x5EB630", Offset = "0x5EA030", VA = "0x1805EB630", Slot = "20")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700034C")]
    	public virtual float BlurIntensity
    	{
    		[Token(Token = "0x600170D")]
    		[Address(RVA = "0x398D00", Offset = "0x397700", VA = "0x180398D00", Slot = "21")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x600170E")]
    		[Address(RVA = "0x4B70F0", Offset = "0x4B5AF0", VA = "0x1804B70F0", Slot = "22")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700034D")]
    	public virtual Vector2 Pivot
    	{
    		[Token(Token = "0x600170F")]
    		[Address(RVA = "0x5EB640", Offset = "0x5EA040", VA = "0x1805EB640", Slot = "23")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Vector2);
    		}
    		[Token(Token = "0x6001710")]
    		[Address(RVA = "0x5EB660", Offset = "0x5EA060", VA = "0x1805EB660", Slot = "24")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6001711")]
    	[Address(RVA = "0x5EB670", Offset = "0x5EA070", VA = "0x1805EB670")]
    	public static TransitionalRenderer CreateFor(OrthoActorMetadata actorMetadata, GameObject actorObject, bool premultipliedAlpha)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001712")]
    	[Address(RVA = "0x5EBAB0", Offset = "0x5EA4B0", VA = "0x1805EBAB0", Slot = "25")]
    	[AsyncStateMachine(typeof(_003CTransitionTo_003Ed__45))]
    	public virtual UniTask TransitionTo(Texture texture, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001713")]
    	[Address(RVA = "0x5EBD40", Offset = "0x5EA740", VA = "0x1805EBD40", Slot = "26")]
    	[AsyncStateMachine(typeof(_003CTintTo_003Ed__46))]
    	public virtual UniTask TintTo(Color color, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001714")]
    	[Address(RVA = "0x5EBF10", Offset = "0x5EA910", VA = "0x1805EBF10", Slot = "27")]
    	[AsyncStateMachine(typeof(_003CFadeTo_003Ed__47))]
    	public virtual UniTask FadeTo(float opacity, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001715")]
    	[Address(RVA = "0x5EC0D0", Offset = "0x5EAAD0", VA = "0x1805EC0D0", Slot = "28")]
    	[AsyncStateMachine(typeof(_003CFadeOut_003Ed__48))]
    	public virtual UniTask FadeOut(Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001716")]
    	[Address(RVA = "0x5EC280", Offset = "0x5EAC80", VA = "0x1805EC280", Slot = "29")]
    	[AsyncStateMachine(typeof(_003CFadeIn_003Ed__49))]
    	public virtual UniTask FadeIn(Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001717")]
    	[Address(RVA = "0x5EC430", Offset = "0x5EAE30", VA = "0x1805EC430", Slot = "30")]
    	[AsyncStateMachine(typeof(_003CBlur_003Ed__50))]
    	public virtual UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001718")]
    	[Address(RVA = "0x5EC5F0", Offset = "0x5EAFF0", VA = "0x1805EC5F0", Slot = "31")]
    	public virtual CharacterLookDirection GetLookDirection(CharacterLookDirection bakedDirection)
    	{
    		return default(CharacterLookDirection);
    	}

    	[Token(Token = "0x6001719")]
    	[Address(RVA = "0x5EC650", Offset = "0x5EB050", VA = "0x1805EC650", Slot = "32")]
    	public virtual void SetLookDirection(CharacterLookDirection direction, CharacterLookDirection bakedDirection)
    	{
    	}

    	[Token(Token = "0x600171A")]
    	[Address(RVA = "0x5EC6D0", Offset = "0x5EB0D0", VA = "0x1805EC6D0", Slot = "33")]
    	[AsyncStateMachine(typeof(_003CChangeLookDirection_003Ed__53))]
    	public virtual UniTask ChangeLookDirection(CharacterLookDirection direction, CharacterLookDirection bakedDirection, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600171B")]
    	[Address(RVA = "0x5EC890", Offset = "0x5EB290", VA = "0x1805EC890", Slot = "34")]
    	protected virtual void Initialize(bool premultipliedAlpha, [Optional] Material customMaterial)
    	{
    	}

    	[Token(Token = "0x600171C")]
    	[Address(RVA = "0x5ECB00", Offset = "0x5EB500", VA = "0x1805ECB00", Slot = "35")]
    	[AsyncStateMachine(typeof(_003CDoFlipX_003Ed__55))]
    	protected virtual UniTask DoFlipX(Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600171D")]
    	[Address(RVA = "0x5ECCB0", Offset = "0x5EB6B0", VA = "0x1805ECCB0", Slot = "36")]
    	protected virtual void OnDestroy()
    	{
    	}

    	[Token(Token = "0x600171E")]
    	[Address(RVA = "0x5ECD00", Offset = "0x5EB700", VA = "0x1805ECD00", Slot = "37")]
    	protected virtual bool ShouldRender()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600171F")]
    	[Address(RVA = "0x5ECEC0", Offset = "0x5EB8C0", VA = "0x1805ECEC0", Slot = "38")]
    	protected virtual void RenderToTexture(RenderTexture texture)
    	{
    	}

    	[Token(Token = "0x6001720")]
    	[Address(RVA = "0x5ECFE0", Offset = "0x5EB9E0", VA = "0x1805ECFE0", Slot = "39")]
    	protected virtual void FitUVs(Vector2 renderSize)
    	{
    	}

    	[Token(Token = "0x6001721")]
    	[Address(RVA = "0x5ED390", Offset = "0x5EBD90", VA = "0x1805ED390", Slot = "40")]
    	protected virtual (Vector2, Vector2) GetMainUVModifiers(Vector2 renderSize, Vector2 textureSize)
    	{
    		return default((Vector2, Vector2));
    	}

    	[Token(Token = "0x6001722")]
    	[Address(RVA = "0x5ED5A0", Offset = "0x5EBFA0", VA = "0x1805ED5A0", Slot = "41")]
    	protected virtual (Vector2, Vector2) GetTransitionUVModifiers(Vector2 renderSize, Vector2 textureSize)
    	{
    		return default((Vector2, Vector2));
    	}

    	[Token(Token = "0x6001723")]
    	[Address(RVA = "0x5ED5E0", Offset = "0x5EBFE0", VA = "0x1805ED5E0", Slot = "42")]
    	protected virtual void DrawQuad(RenderTexture target, Vector2 size)
    	{
    	}

    	[Token(Token = "0x6001724")]
    	[Address(RVA = "0x5EDA50", Offset = "0x5EC450", VA = "0x1805EDA50")]
    	private void LateUpdate()
    	{
    	}

    	[Token(Token = "0x6001725")]
    	[Address(RVA = "0x5EDA80", Offset = "0x5EC480", VA = "0x1805EDA80")]
    	protected TransitionalRenderer()
    	{
    	}
    }
}
