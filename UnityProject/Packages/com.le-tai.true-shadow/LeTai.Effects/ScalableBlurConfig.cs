using UnityEngine;

namespace LeTai.Effects
{

public class ScalableBlurConfig : BlurConfig
{
	[SerializeField]
	private float radius = 4f;

	[SerializeField]
	private int iteration = 4;

	[SerializeField]
	private int maxDepth = 6;

	[SerializeField]
	[Range(0f, 256f)]
	private float strength;

	private static readonly float UNIT_VARIANCE = 1f + Mathf.Sqrt(2f) / 2f;

	public float Radius
	{
		get
		{
			return radius;
		}
		set
		{
			radius = Mathf.Max(0f, value);
		}
	}

	public int Iteration
	{
		get
		{
			return iteration;
		}
		set
		{
			iteration = Mathf.Max(0, value);
		}
	}

	public int MaxDepth
	{
		get
		{
			return maxDepth;
		}
		set
		{
			maxDepth = Mathf.Max(1, value);
		}
	}

	public override int MinExtent => (Iteration + 1) * (Iteration + 1);

	public override float Strength
	{
		get
		{
			return strength = radius * (float)(3 * (1 << iteration) - 2) / UNIT_VARIANCE;
		}
		set
		{
			strength = Mathf.Max(0f, value);
			SetAdvancedFieldFromSimple();
		}
	}

	protected virtual void SetAdvancedFieldFromSimple()
	{
		strength = Mathf.Clamp(strength, 0f, 268435460f);
		float num = strength * 0.66f;
		radius = Mathf.Sqrt(num);
		iteration = 0;
		while ((float)(1 << iteration) < radius)
		{
			iteration++;
		}
		radius = num / (float)(1 << iteration);
	}

	private void OnValidate()
	{
		SetAdvancedFieldFromSimple();
	}
}
}
