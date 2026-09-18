using System;
using UnityEngine;

namespace LeTai.TrueShadow
{

public static class Math
{
	public static float Angle360(Vector2 from, Vector2 to)
	{
		float num = Vector2.SignedAngle(from, to);
		if (!(num < 0f))
		{
			return num;
		}
		return 360f + num;
	}

	public static Vector2 AngleDistanceVector(float angle, float distance, Vector2 zeroVector)
	{
		return Quaternion.Euler(0f, 0f, 0f - angle) * zeroVector * distance;
	}

	public static Vector2 Rotate(this Vector2 v, float angle)
	{
		float f = angle * (MathF.PI / 180f);
		float num = Mathf.Sin(f);
		float num2 = Mathf.Cos(f);
		return new Vector2(num2 * v.x - num * v.y, num * v.x + num2 * v.y);
	}
}
}
