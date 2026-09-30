using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeTai.TrueShadow
{

public class AnimatedBiStateButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public enum State
	{
		Up,
		AnimateDown,
		Down,
		AnimateUp
	}

	public float animationDuration = 0.1f;

	public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[Tooltip("Animate if the pointer is hovered over the element while already pressed")]
	public bool useEnterExitEvents = true;

	protected State state;

	protected float pressAmount;

	protected bool IsAnimating
	{
		get
		{
			if (state != State.AnimateDown)
			{
				return state == State.AnimateUp;
			}
			return true;
		}
	}

	public event Action willPress;

	public event Action willRelease;

	private void Update()
	{
		PollPointerUp();
		DoAnimation();
	}

	private void DoAnimation()
	{
		if (IsAnimating)
		{
			if (state == State.AnimateDown)
			{
				pressAmount += Time.deltaTime / animationDuration;
			}
			else if (state == State.AnimateUp)
			{
				pressAmount -= Time.deltaTime / animationDuration;
			}
			pressAmount = Mathf.Clamp01(pressAmount);
			float num = pressAmount;
			if (state == State.AnimateUp)
			{
				num = 1f - num;
			}
			num = animationCurve.Evaluate(num);
			if (state == State.AnimateUp)
			{
				num = 1f - num;
			}
			Animate(num);
			if (state == State.AnimateDown && pressAmount == 1f)
			{
				state = State.Down;
			}
			if (state == State.AnimateUp && pressAmount == 0f)
			{
				state = State.Up;
			}
		}
	}

	protected void Press()
	{
		if (state != State.Down && state != State.AnimateDown)
		{
			OnWillPress();
			state = State.AnimateDown;
		}
	}

	protected void Release()
	{
		if (state != State.Up && state != State.AnimateUp)
		{
			OnWillRelease();
			state = State.AnimateUp;
		}
	}

	private void PollPointerUp()
	{
		if (useEnterExitEvents && (state == State.Down || state == State.AnimateDown) && !Input.GetMouseButton(0))
		{
			Release();
		}
	}

	protected virtual void Animate(float visualPressAmount)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Press();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Release();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (useEnterExitEvents && Input.GetMouseButton(0))
		{
			Press();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (useEnterExitEvents)
		{
			Release();
		}
	}

	protected virtual void OnWillPress()
	{
		this.willPress?.Invoke();
	}

	protected virtual void OnWillRelease()
	{
		this.willRelease?.Invoke();
	}
}
}
