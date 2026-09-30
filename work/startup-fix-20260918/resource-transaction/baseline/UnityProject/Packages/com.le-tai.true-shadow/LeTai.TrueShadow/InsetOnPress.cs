using UnityEngine;

namespace LeTai.TrueShadow
{

[RequireComponent(typeof(TrueShadow))]
public class InsetOnPress : AnimatedBiStateButton
{
	private TrueShadow[] shadows;

	private float[] normalOpacity;

	private bool wasInset;

	private void OnEnable()
	{
		shadows = GetComponents<TrueShadow>();
		normalOpacity = new float[shadows.Length];
	}

	protected override void Animate(float visualPressAmount)
	{
		bool flag = visualPressAmount > 0.5f;
		if (flag != wasInset)
		{
			for (int i = 0; i < shadows.Length; i++)
			{
				shadows[i].Inset = flag;
			}
			wasInset = flag;
		}
		if (flag)
		{
			SetAllOpacity(visualPressAmount * 2f - 1f);
		}
		else
		{
			SetAllOpacity(1f - visualPressAmount * 2f);
		}
		void SetAllOpacity(float lerpProgress)
		{
			for (int j = 0; j < shadows.Length; j++)
			{
				Color color = shadows[j].Color;
				color.a = Mathf.Lerp(0f, normalOpacity[j], lerpProgress);
				shadows[j].Color = color;
			}
		}
	}

	private void MemorizeOpacity()
	{
		if (!base.IsAnimating)
		{
			for (int i = 0; i < shadows.Length; i++)
			{
				normalOpacity[i] = shadows[i].Color.a;
			}
		}
	}

	protected override void OnWillPress()
	{
		wasInset = shadows[0].Inset;
		MemorizeOpacity();
		base.OnWillPress();
	}
}
}
