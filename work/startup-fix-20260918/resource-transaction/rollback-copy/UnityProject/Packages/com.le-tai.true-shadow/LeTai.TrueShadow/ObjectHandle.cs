using System;
using UnityEngine;

namespace LeTai.TrueShadow
{

public static class ObjectHandle
{
	public static ObjectHandle<T> Take<T>(T obj) where T : UnityEngine.Object
	{
		return new ObjectHandle<T>(obj, own: true);
	}

	public static ObjectHandle<T> Borrow<T>(T obj) where T : UnityEngine.Object
	{
		return new ObjectHandle<T>(obj, own: false);
	}
}
public readonly struct ObjectHandle<T> : IDisposable where T : UnityEngine.Object
{
	public readonly bool own;

	public readonly T obj;

	internal ObjectHandle(T obj, bool own)
	{
		this.obj = obj;
		this.own = own;
	}

	public void Dispose()
	{
		if (own && obj != null)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}

	public static implicit operator T(ObjectHandle<T> handle)
	{
		return handle.obj;
	}
}
}
