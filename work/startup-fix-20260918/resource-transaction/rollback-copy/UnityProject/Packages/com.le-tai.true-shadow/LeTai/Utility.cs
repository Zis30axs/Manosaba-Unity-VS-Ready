using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LeTai
{

public static class Utility
{
	public static void LogList<T>(IEnumerable<T> list, Func<T, object> getData)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		foreach (T item in list)
		{
			stringBuilder.Append(num + ":    ");
			stringBuilder.Append(getData(item).ToString());
			stringBuilder.Append("\n");
			num++;
		}
		Debug.Log(stringBuilder.ToString());
	}

	public static int SimplePingPong(int t, int max)
	{
		if (t > max)
		{
			return 2 * max - t;
		}
		return t;
	}

	public static void SafeDestroy(UnityEngine.Object obj)
	{
		if (!(obj != null))
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (obj is GameObject gameObject)
			{
				gameObject.transform.parent = null;
			}
			UnityEngine.Object.Destroy(obj);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}
}
}
