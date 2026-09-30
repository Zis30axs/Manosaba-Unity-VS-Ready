using System;
using UnityEngine;

namespace LeTai.Effects
{

public class BlurHQConfig : BlurConfig
{
	[SerializeField]
	private float strength;

	private static readonly float SQRT05 = Mathf.Sqrt(2f);

	internal readonly float[] rawWeights = new float[128];

	internal readonly float[] weights = new float[64];

	internal readonly float[] offsets = new float[64];

	public int Extent { get; private set; }

	public int NumWeight { get; private set; }

	public override int MinExtent => 1;

	public override float Strength
	{
		get
		{
			return strength;
		}
		set
		{
			float b = Mathf.Min(value, 126f);
			if (Mathf.Approximately(strength, b))
			{
				return;
			}
			strength = b;
			Extent = Mathf.CeilToInt(strength);
			float num = 0.5f * strength;
			float num2 = 1f / num * SQRT05;
			float num3 = (rawWeights[0] = 1f);
			for (int i = 1; i <= Extent; i++)
			{
				float num6;
				if (num < 0.8f)
				{
					float num4 = Erf(((float)i + 0.5f) * num2);
					float num5 = Erf(((float)i - 0.5f) * num2);
					num6 = (num4 - num5) / 2f;
				}
				else
				{
					num6 = Mathf.Exp((float)(-i * i) / num / num);
				}
				rawWeights[i] = num6;
				num3 += num6 * 2f;
			}
			float num7 = 1f / num3;
			for (int j = 0; j <= Extent; j++)
			{
				rawWeights[j] *= num7;
			}
			weights[0] = rawWeights[0];
			NumWeight = Mathf.CeilToInt((float)Extent / 2f) + 1;
			for (int k = 1; k < NumWeight; k++)
			{
				int num8 = k * 2;
				int num9 = num8 - 1;
				float num10 = rawWeights[num9];
				float num11 = ((num8 <= Extent) ? rawWeights[num8] : 0f);
				float num12 = num10 + num11;
				weights[k] = num12;
				offsets[k] = (float)num9 + num11 / num12;
			}
		}
	}

	private static float Erf(float x)
	{
		int num = ((!(x < 0f)) ? 1 : (-1));
		x = Mathf.Abs(x);
		float num2 = 1f / (1f + 0.3275911f * x);
		float num3 = 1f - ((((1.0614054f * num2 + -1.4531521f) * num2 + 1.4214138f) * num2 + -244f / (273f * MathF.PI)) * num2 + 0.2548296f) * num2 * Mathf.Exp((0f - x) * x);
		return (float)num * num3;
	}
}
}
