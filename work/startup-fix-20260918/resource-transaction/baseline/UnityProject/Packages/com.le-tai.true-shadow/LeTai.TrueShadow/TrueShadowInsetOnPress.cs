using UnityEngine;

namespace LeTai.TrueShadow
{

[AddComponentMenu("UI/True Shadow/True Shadow Inset On Press")]
[RequireComponent(typeof(TrueShadow))]
public class TrueShadowInsetOnPress : AnimatedBiStateButton
{
	private TrueShadow[] _shadows;

	private float[] _normalOpacity;

	private bool _wasInset;

	private void OnEnable()
	{
		_shadows = GetComponents<TrueShadow>();
		_normalOpacity = new float[_shadows.Length];
	}

	protected override void Animate(float visualPressAmount)
	{
		bool flag = visualPressAmount > 0.5f;
		if (flag != _wasInset)
		{
			for (int i = 0; i < _shadows.Length; i++)
			{
				_shadows[i].Inset = flag;
			}
			_wasInset = flag;
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
			for (int j = 0; j < _shadows.Length; j++)
			{
				Color color = _shadows[j].Color;
				color.a = Mathf.Lerp(0f, _normalOpacity[j], lerpProgress);
				_shadows[j].Color = color;
			}
		}
	}

	private void MemorizeOpacity()
	{
		if (!base.IsAnimating)
		{
			for (int i = 0; i < _shadows.Length; i++)
			{
				_normalOpacity[i] = _shadows[i].Color.a;
			}
		}
	}

	protected override void OnWillPress()
	{
		_wasInset = _shadows[0].Inset;
		MemorizeOpacity();
		base.OnWillPress();
	}
}
}
