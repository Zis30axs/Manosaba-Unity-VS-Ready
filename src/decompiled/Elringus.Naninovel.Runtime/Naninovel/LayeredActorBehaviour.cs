using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000091")]
[ExecuteAlways]
public abstract class LayeredActorBehaviour : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x2000092")]
	public class CompositionMapItem
	{
		[Token(Token = "0x400021D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public string Key;

		[Token(Token = "0x400021E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[TextArea(1, 5)]
		public string Composition;

		[Token(Token = "0x60002A9")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public CompositionMapItem()
		{
		}
	}

	[Token(Token = "0x400020E")]
	private const string groupLiteral = "/";

	[Token(Token = "0x400020F")]
	private const string selectLiteral = ">";

	[Token(Token = "0x4000210")]
	private const string enableLiteral = "+";

	[Token(Token = "0x4000211")]
	private const string disableLiteral = "-";

	[Token(Token = "0x4000212")]
	private const string splitLiteral = ",";

	[Token(Token = "0x4000213")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly string[] splitLiterals;

	[Token(Token = "0x4000214")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Tooltip("Whether the actor should be rendered every frame. Enable when animating the layers or implementing other dynamic behaviour.")]
	[SerializeField]
	private bool animated;

	[Token(Token = "0x4000215")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x29")]
	[Tooltip("Whether to render the layers in a reversed order.")]
	[SerializeField]
	private bool reversed;

	[Token(Token = "0x4000216")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Tooltip("Shared material to use when rendering the layers. Will use layer renderer's material when not assigned.")]
	[SerializeField]
	private Material renderMaterial;

	[Token(Token = "0x4000217")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Tooltip("When assigned, will render the prefab content with the camera instead of procedural renderer. Less optimized, but supports more features, such as particle systems.")]
	[SerializeField]
	private Camera renderCamera;

	[Token(Token = "0x4000218")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Tooltip("Additional layers to include when rendering the actor in camera mode.")]
	[LayerMask]
	[SerializeField]
	private int cameraMask;

	[Token(Token = "0x4000219")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[Tooltip("Allows to map layer composition expressions to keys; the keys can then be used to specify layered actor appearances instead of the full expressions.")]
	[SerializeField]
	private List<CompositionMapItem> compositionMap;

	[Token(Token = "0x400021A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Tooltip("Appearance to use by default. Will use layered expression of the initial prefab state when not specified.")]
	[SerializeField]
	private string defaultAppearance;

	[Token(Token = "0x400021B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[Tooltip("Whether to disable layer-related behaviour and just render the prefab content. Enable when controlling appearance via external means (eg, with Animator).")]
	[SerializeField]
	private bool renderOnly;

	[Token(Token = "0x400021C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[Tooltip("Invoked when appearance of the actor is changed.")]
	[SerializeField]
	private StringUnityEvent onAppearanceChanged;

	[Token(Token = "0x1700006A")]
	public virtual string Composition
	{
		[Token(Token = "0x600028D")]
		[Address(RVA = "0x45DEE0", Offset = "0x45C8E0", VA = "0x18045DEE0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700006B")]
	public virtual bool Animated
	{
		[Token(Token = "0x600028E")]
		[Address(RVA = "0x456330", Offset = "0x454D30", VA = "0x180456330", Slot = "5")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700006C")]
	public virtual string DefaultAppearance
	{
		[Token(Token = "0x600028F")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "6")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700006D")]
	public virtual bool RenderOnly
	{
		[Token(Token = "0x6000290")]
		[Address(RVA = "0x45E0C0", Offset = "0x45CAC0", VA = "0x18045E0C0", Slot = "7")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700006E")]
	protected virtual bool Reversed
	{
		[Token(Token = "0x6000291")]
		[Address(RVA = "0x45E0D0", Offset = "0x45CAD0", VA = "0x18045E0D0", Slot = "8")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700006F")]
	protected virtual Material SharedRenderMaterial
	{
		[Token(Token = "0x6000292")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "9")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000070")]
	protected virtual Camera RenderCamera
	{
		[Token(Token = "0x6000293")]
		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000071")]
	protected virtual int CameraMask
	{
		[Token(Token = "0x6000294")]
		[Address(RVA = "0x45E0E0", Offset = "0x45CAE0", VA = "0x18045E0E0", Slot = "11")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x17000072")]
	protected virtual LayeredDrawer Drawer
	{
		[Token(Token = "0x6000295")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "12")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000296")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6000297")]
	[Address(RVA = "0x45E150", Offset = "0x45CB50", VA = "0x18045E150", Slot = "13")]
	public virtual IReadOnlyDictionary<string, string> GetCompositionMap()
	{
		return null;
	}

	[Token(Token = "0x6000298")]
	[Address(RVA = "0x45E2E0", Offset = "0x45CCE0", VA = "0x18045E2E0", Slot = "14")]
	public virtual void NotifyAppearanceChanged(string appearance)
	{
	}

	[Token(Token = "0x6000299")]
	[Address(RVA = "0x45E340", Offset = "0x45CD40", VA = "0x18045E340", Slot = "15")]
	public virtual void NotifyPerceivedVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x600029A")]
	[Address(RVA = "0x45E390", Offset = "0x45CD90", VA = "0x18045E390", Slot = "16")]
	public virtual void ApplyComposition(string value)
	{
	}

	[Token(Token = "0x600029B")]
	[Address(RVA = "0x45E470", Offset = "0x45CE70", VA = "0x18045E470", Slot = "17")]
	[ContextMenu("Rebuild Layers")]
	public virtual void RebuildLayers()
	{
	}

	[Token(Token = "0x600029C")]
	[Address(RVA = "0x45E4A0", Offset = "0x45CEA0", VA = "0x18045E4A0", Slot = "18")]
	public virtual RenderTexture Render(float pixelsPerUnit, [Optional] RenderTexture renderTexture)
	{
		return null;
	}

	[Token(Token = "0x600029D")]
	[Address(RVA = "0x45E500", Offset = "0x45CF00", VA = "0x18045E500", Slot = "19")]
	protected virtual void Awake()
	{
	}

	[Token(Token = "0x600029E")]
	[Address(RVA = "0x45E570", Offset = "0x45CF70", VA = "0x18045E570", Slot = "20")]
	protected virtual void OnDestroy()
	{
	}

	[Token(Token = "0x600029F")]
	[Address(RVA = "0x45E5B0", Offset = "0x45CFB0", VA = "0x18045E5B0", Slot = "21")]
	protected virtual void OnDrawGizmos()
	{
	}

	[Token(Token = "0x60002A0")]
	[Address(RVA = "0x45E5F0", Offset = "0x45CFF0", VA = "0x18045E5F0", Slot = "22")]
	protected virtual void OnValidate()
	{
	}

	[Token(Token = "0x60002A1")]
	[Address(RVA = "0x45E680", Offset = "0x45D080", VA = "0x18045E680", Slot = "23")]
	protected virtual LayeredDrawer CreateDrawer()
	{
		return null;
	}

	[Token(Token = "0x60002A2")]
	[Address(RVA = "0x45E780", Offset = "0x45D180", VA = "0x18045E780")]
	protected LayeredActorBehaviour()
	{
	}
}
