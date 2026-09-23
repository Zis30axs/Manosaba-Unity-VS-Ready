using System.Linq;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    // One per layered character prefab.  A LayeredActorLayer of the prefab wires its onLayerHeld event to
    // ModifyLayers (dynamic int) and SetRendererEnabled(true), and onLayerReleased to SetRendererEnabled(false)
    // (persistent calls in the shipped prefabs).  Naninovel's LayeredDrawer only picks a free "Naninovel N" culling
    // layer and points the prefab's render camera at it; moving the layer renderers onto that layer is this
    // component's job - it is what keeps the raw layers out of the main camera (culling mask Default | UI) and puts
    // them in front of the render camera.  While it was a stub every layered character's sprites stayed on Default:
    // the main camera drew all 13 preloaded characters' legs over the opening still, and the render texture the
    // actor shows stayed empty.
    [Token(Token = "0x200002E")]
    public class LayerModifier : MonoBehaviour
    {
    	[Token(Token = "0x400009D")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Renderer[] _layers;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1DAF0, 1175 B -- Component.get_transform, GetComponentsInChildren<LayeredActorLayer>,
    	//   Enumerable.Select with <>c.<Reset>b__1_0 (0xD1E2E0: layer.GetComponent<Renderer>()), Enumerable.Where with
    	//   <>c.<Reset>b__1_1 (0xD1E340: go != null, the UnityEngine.Object inequality on m_CachedPtr), ToArray, stored to +0x20.
    	//   (Editor-time: Unity calls Reset when the component is added.)
    	// Annotated disassembly: work/title-path/disas/lm_01_Reset.txt, lm_04_Reset_lambdas.txt
    	[Token(Token = "0x6000097")]
    	[Address(RVA = "0xD1DAF0", Offset = "0xD1C4F0", VA = "0x180D1DAF0")]
    	private void Reset()
    	{
    		_layers = transform.GetComponentsInChildren<LayeredActorLayer>()
    			.Select((LayeredActorLayer layer) => layer.GetComponent<Renderer>())
    			.Where((Renderer go) => go != null)
    			.ToArray();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1DF90, 420 B -- index loop over _layers (+0x20, bound [rbp+0x18]); per element
    	//   the icalls Component::get_gameObject_Injected (0x180D1E048) and GameObject::set_layer_Injected (0x180D1E0BD,
    	//   edx = cameraCullingLayer).  No null test of its own: a null array / element / destroyed renderer throws.
    	// Annotated disassembly: work/title-path/disas/lm_02_ModifyLayers.txt
    	[Token(Token = "0x6000098")]
    	[Address(RVA = "0xD1DF90", Offset = "0xD1C990", VA = "0x180D1DF90")]
    	public void ModifyLayers(int cameraCullingLayer)
    	{
    		foreach (Renderer layer in _layers)
    		{
    			layer.gameObject.layer = cameraCullingLayer;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1E140, 242 B -- the same loop with the icall Renderer::set_enabled_Injected
    	//   (0x180D1E1E0, dl = enable).
    	// Annotated disassembly: work/title-path/disas/lm_03_SetRendererEnabled.txt
    	[Token(Token = "0x6000099")]
    	[Address(RVA = "0xD1E140", Offset = "0xD1CB40", VA = "0x180D1E140")]
    	public void SetRendererEnabled(bool enable)
    	{
    		foreach (Renderer layer in _layers)
    		{
    			layer.enabled = enable;
    		}
    	}

    	[Token(Token = "0x600009A")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public LayerModifier()
    	{
    	}
    }
}
