using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Rendering;

namespace Naninovel
{
    [Token(Token = "0x200009F")]
    public class LayeredDrawer : IDisposable
    {
    	[Token(Token = "0x400023A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly Dictionary<int, bool> cullingToUsed;

    	[Token(Token = "0x400023B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly Transform transform;

    	[Token(Token = "0x400023C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly Material sharedMaterial;

    	[Token(Token = "0x400023D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly Camera camera;

    	[Token(Token = "0x400023E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly int cameraMask;

    	[Token(Token = "0x400023F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    	private readonly bool reversed;

    	[Token(Token = "0x4000240")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private readonly List<ILayeredRendererLayer> layers;

    	[Token(Token = "0x4000241")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly CommandBuffer commandBuffer;

    	[Token(Token = "0x4000242")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private readonly MaterialPropertyBlock propertyBlock;

    	[Token(Token = "0x4000243")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private RenderCanvas renderCanvas;

    	[Token(Token = "0x4000244")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private Rect canvasRect;

    	[Token(Token = "0x4000245")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private int? cullingLayer;

    	[Token(Token = "0x17000074")]
    	public virtual IReadOnlyCollection<ILayeredRendererLayer> Layers
    	{
    		[Token(Token = "0x60002D8")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "5")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60002D9")]
    	[Address(RVA = "0x462660", Offset = "0x461060", VA = "0x180462660")]
    	public LayeredDrawer(Transform transform, Camera camera = default, int cameraMask = 0, Material sharedMaterial = default, bool reversed = false)
    	{
    	}

    	[Token(Token = "0x60002DA")]
    	[Address(RVA = "0x462A10", Offset = "0x461410", VA = "0x180462A10", Slot = "6")]
    	public virtual void ReleaseCameraLayer()
    	{
    	}

    	[Token(Token = "0x60002DB")]
    	[Address(RVA = "0x462F20", Offset = "0x461920", VA = "0x180462F20", Slot = "7")]
    	public virtual void Dispose()
    	{
    	}

    	[Token(Token = "0x60002DC")]
    	[Address(RVA = "0x462F40", Offset = "0x461940", VA = "0x180462F40")]
    	public void BuildLayers()
    	{
    	}

    	[Token(Token = "0x60002DD")]
    	[Address(RVA = "0x463060", Offset = "0x461A60", VA = "0x180463060", Slot = "8")]
    	public virtual RenderTexture DrawLayers(float pixelsPerUnit, RenderTexture renderTexture = default)
    	{
    		return null;
    	}

    	[Token(Token = "0x60002DE")]
    	[Address(RVA = "0x463970", Offset = "0x462370", VA = "0x180463970", Slot = "9")]
    	public virtual void DrawGizmos()
    	{
    	}

    	[Token(Token = "0x60002DF")]
    	[Address(RVA = "0x463C40", Offset = "0x462640", VA = "0x180463C40", Slot = "10")]
    	protected virtual Vector2 EvaluateDrawDimensions(float pixelsPerUnit)
    	{
    		return default(Vector2);
    	}

    	[Token(Token = "0x60002E0")]
    	[Address(RVA = "0x463C60", Offset = "0x462660", VA = "0x180463C60", Slot = "11")]
    	protected virtual RenderTexture DrawWithCamera(float pixelsPerUnit, RenderTexture renderTexture = default)
    	{
    		return null;
    	}

    	[Token(Token = "0x60002E1")]
    	[Address(RVA = "0x463F00", Offset = "0x462900", VA = "0x180463F00", Slot = "12")]
    	protected virtual void ClearLayers()
    	{
    	}

    	[Token(Token = "0x60002E2")]
    	[Address(RVA = "0x464140", Offset = "0x462B40", VA = "0x180464140", Slot = "13")]
    	protected virtual IReadOnlyCollection<Renderer> GetRenderers()
    	{
    		return null;
    	}

    	[Token(Token = "0x60002E3")]
    	[Address(RVA = "0x464510", Offset = "0x462F10", VA = "0x180464510", Slot = "14")]
    	protected virtual void UpdateCanvas()
    	{
    	}

    	[Token(Token = "0x60002E4")]
    	[Address(RVA = "0x4647B0", Offset = "0x4631B0", VA = "0x1804647B0", Slot = "15")]
    	protected virtual Rect EvaluateCanvasRect()
    	{
    		return default(Rect);
    	}

    	[Token(Token = "0x60002E5")]
    	[Address(RVA = "0x464E10", Offset = "0x463810", VA = "0x180464E10", Slot = "16")]
    	protected virtual RenderTexture PrepareRenderTexture(RenderTexture renderTexture, Vector2 drawDimensions)
    	{
    		return null;
    	}

    	[Token(Token = "0x60002E6")]
    	[Address(RVA = "0x464F60", Offset = "0x463960", VA = "0x180464F60", Slot = "17")]
    	protected virtual void PrepareCommandBuffer(RenderTexture renderTexture, Matrix4x4 orthoMatrix)
    	{
    	}

    	[Token(Token = "0x60002E7")]
    	[Address(RVA = "0x465200", Offset = "0x463C00", VA = "0x180465200", Slot = "18")]
    	protected virtual void DrawLayer(LayeredRendererLayer layer, Matrix4x4 rotationMatrix, float pixelsPerUnit)
    	{
    	}

    	[Token(Token = "0x60002E8")]
    	[Address(RVA = "0x465920", Offset = "0x464320", VA = "0x180465920", Slot = "19")]
    	protected virtual int CountRenderers()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60002E9")]
    	[Address(RVA = "0x465990", Offset = "0x464390", VA = "0x180465990", Slot = "20")]
    	protected virtual void BuildCameraLayers()
    	{
    	}

    	[Token(Token = "0x60002EA")]
    	[Address(RVA = "0x466150", Offset = "0x464B50", VA = "0x180466150", Slot = "21")]
    	protected virtual void HoldCameraLayer()
    	{
    	}

    	[Token(Token = "0x60002EB")]
    	[Address(RVA = "0x466740", Offset = "0x465140", VA = "0x180466740", Slot = "22")]
    	protected virtual void BuildRendererLayers()
    	{
    	}

    	[Token(Token = "0x60002EC")]
    	[Address(RVA = "0x466C70", Offset = "0x465670", VA = "0x180466C70")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	private static void ResetCulling()
    	{
    	}
    }
}
