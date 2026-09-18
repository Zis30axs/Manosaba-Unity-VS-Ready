using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Cameras
{
    [Token(Token = "0x2000005")]
    public static class CameraConfigurationUtility
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF4EB0, 1000 B
    	// ABI: rcx = hidden Rect return pointer (r14), dx = the 2-byte System.Nullable<bool> (bx).
    	//   0x180CF4FB6  Engine$$GetService<ICameraManager> (TryGetService<ICameraManager> inlined: result != null)
    	//   null -> 0x180CF5224 Debug$$LogWarning(STR 'The ICameraManager service not found。') and return default(Rect)
    	//   0x180CF5013  thunk 0x204B0 -> IEngineService<CameraConfiguration>.get_Configuration (slot 0) -> rdi
    	//   0x180CF5068  matchScreenWidth.HasValue ? 0x180CF5071 GetValueOrDefault() : 0x180CF5135 config.MatchScreenWidth ([rdi+0x24])
    	//                == `matchScreenWidth ?? config.MatchScreenWidth`
    	//   false -> 0x180CF513D resolution = (Vector2)config.ReferenceResolution ([rdi+0x18], cvtdq2ps)
    	//   true  -> 0x180CF5159 ebx = ReferenceResolution.y; 0x180CF517D icall 0x30E68D0 = Screen.get_width (esi)
    	//            (the package's CameraConfiguration.EvaluateSceneRect at 0x485850 calls the same two icalls
    	//            in the opposite order: 0x25F3B70 first, and divides ReferenceResolution.y by it, so
    	//            0x25F3B70 = Screen.get_height and 0x25F3F60 = Screen.get_width);
    	//            0x180CF51A9 (float)ref.y * (float)Screen.width; 0x180CF51BB icall 0x30E68D8 = Screen.get_height;
    	//            0x180CF51C9 divss -> width = Screen.width * ref.y / Screen.height; height = ref.y
    	//   0x180CF51D0  divss by [rdi+0x20] = ReferencePPU (size = resolution / ppu)
    	//   0x180CF51DA  [rdi+0x28] = InitialPosition.xy; x = pos.x - w * 0.5 (0x2AE2728 = 0.5f); y = pos.y - h * 0.5
    	//   0x180CF521E  store Rect(x, y, w, h)
    	// The last three steps are exactly CameraConfiguration.EvaluateSceneRect(center, resolution, ppu)
    	// (size = resolution / ppu; position = center - size / 2).
    	// Annotated disassembly: work/spawn-nre/disas/70_EvaluateSceneRect.txt
    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xCF4EB0", Offset = "0xCF38B0", VA = "0x180CF4EB0")]
    	public static Rect EvaluateSceneRect([Optional] bool? matchScreenWidth)
    	{
    		if (!Engine.TryGetService<ICameraManager>(out var cameraManager))
    		{
    			Debug.LogWarning("The ICameraManager service not found。");
    			return default;
    		}
    		CameraConfiguration config = cameraManager.Configuration;
    		bool match = matchScreenWidth ?? config.MatchScreenWidth;
    		Vector2 resolution;
    		if (!match)
    		{
    			resolution = config.ReferenceResolution;
    		}
    		else
    		{
    			float width = Screen.width * (float)config.ReferenceResolution.y / Screen.height;
    			resolution = new Vector2(width, config.ReferenceResolution.y);
    		}
    		return CameraConfiguration.EvaluateSceneRect(config.InitialPosition, resolution, config.ReferencePPU);
    	}
    }
}
