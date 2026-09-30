using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeTai.TrueShadow
{

public static class Shims
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T FindObjectOfType<T>(bool includeInactive = false, bool sorted = true) where T : Object
	{
		if (sorted)
		{
			return Object.FindFirstObjectByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude);
		}
		return Object.FindAnyObjectByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T[] FindObjectsOfType<T>(bool includeInactive = false) where T : Object
	{
		return Object.FindObjectsByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None);
	}
}
}
