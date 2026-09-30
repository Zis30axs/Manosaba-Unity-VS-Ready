using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai
{

public static class ExtensionMethods
{
	private static Mesh fullscreenTriangle;

	private static Mesh FullscreenTriangle
	{
		get
		{
			if (fullscreenTriangle != null)
			{
				return fullscreenTriangle;
			}
			fullscreenTriangle = new Mesh
			{
				name = "Fullscreen Triangle"
			};
			fullscreenTriangle.SetVertices(new List<Vector3>
			{
				new Vector3(-1f, -1f, 0f),
				new Vector3(-1f, 3f, 0f),
				new Vector3(3f, -1f, 0f)
			});
			fullscreenTriangle.SetIndices(new int[3] { 0, 1, 2 }, MeshTopology.Triangles, 0, calculateBounds: false);
			fullscreenTriangle.UploadMeshData(markNoLongerReadable: false);
			return fullscreenTriangle;
		}
	}

	public static Vector4 ToMinMaxVector(this Rect self)
	{
		return new Vector4(self.xMin, self.yMin, self.xMax, self.yMax);
	}

	public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass = 0)
	{
		cmd.SetGlobalTexture("_MainTex", source);
		cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		cmd.DrawMesh(FullscreenTriangle, Matrix4x4.identity, material, 0, pass);
	}

	internal static bool Approximately(this Rect self, Rect other)
	{
		if (QuickApproximate(self.x, other.x) && QuickApproximate(self.y, other.y) && QuickApproximate(self.width, other.width))
		{
			return QuickApproximate(self.height, other.height);
		}
		return false;
	}

	private static bool QuickApproximate(float a, float b)
	{
		return Mathf.Abs(b - a) < 1.175494E-38f;
	}

	public static Vector3 WithZ(this Vector2 self, float z)
	{
		return new Vector3(self.x, self.y, z);
	}

	public static Color WithA(this Color self, float a)
	{
		return new Color(self.r, self.g, self.b, a);
	}

	public static void NextFrames(this MonoBehaviour behaviour, Action action, int nFrames = 1)
	{
		behaviour.StartCoroutine(NextFrame(action, nFrames));
	}

	private static IEnumerator NextFrame(Action action, int nFrames)
	{
		for (int i = 0; i < nFrames; i++)
		{
			yield return null;
		}
		action();
	}

	public static void SetKeyword(this Material material, string keyword, bool enabled)
	{
		if (enabled)
		{
			material.EnableKeyword(keyword);
		}
		else
		{
			material.DisableKeyword(keyword);
		}
	}

	public static Vector2 Frac(this Vector2 vec)
	{
		return new Vector2(vec.x - Mathf.Floor(vec.x), vec.y - Mathf.Floor(vec.y));
	}

	public static Vector2 LocalToScreenPoint(this RectTransform rt, Vector3 localPoint, Camera referenceCamera = null)
	{
		return RectTransformUtility.WorldToScreenPoint(referenceCamera, rt.TransformPoint(localPoint));
	}

	public static Vector2 ScreenToCanvasSize(this RectTransform rt, Vector2 size, Camera referenceCamera = null)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Vector2.zero, referenceCamera, out var localPoint);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, size, referenceCamera, out var localPoint2);
		return localPoint2 - localPoint;
	}
}
}
