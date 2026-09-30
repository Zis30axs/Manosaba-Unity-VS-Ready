using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{

[AddComponentMenu("UI/True Shadow/True Shadow Interaction Animation")]
[RequireComponent(typeof(TrueShadow))]
public class TrueShadowInteractionAnimation : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
{
	public float smoothTime = 0.05f;

	[Tooltip("Deselect on pointer up")]
	public bool autoDeselect;

	[Header("Size")]
	public float selectedSize = 28f;

	public float hoverSize = 28f;

	public float clickedSize = 24f;

	[Header("Distance")]
	public float selectedDistance = 12f;

	public float hoverDistance = 12f;

	public float clickedDistance = 8f;

	[Header("Color")]
	public Color selectedColor = new Color(0f, 0f, 0f, 0.25f);

	public Color hoverColor = new Color(0f, 0f, 0f, 0.2f);

	public Color clickedColor = new Color(0f, 0f, 0f, 0.25f);

	private float normalSize;

	private float normalDistance;

	private Color normalColor;

	private bool normalStateAcquired;

	private bool isSelected;

	private bool isHovered;

	private bool isClicked;

	private TrueShadow shadow;

	private Selectable selectable;

	private float targetSize;

	private float targetDistance;

	private Color targetColor;

	private static readonly Color FADED_COLOR = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	private readonly List<RaycastResult> raycastResults = new List<RaycastResult>();

	private float currentSizeVelocity;

	private float currentDistanceVelocity;

	private float currentColorRVelocity;

	private float currentColorGVelocity;

	private float currentColorBVelocity;

	private float currentColorAVelocity;

	private void OnEnable()
	{
		shadow = FindTrueShadow();
		selectable = GetComponent<Selectable>();
		isHovered = false;
		if (Input.mousePresent)
		{
			isHovered = IsOverGameObject(Input.mousePosition);
		}
		if (!isHovered)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				isHovered = IsOverGameObject(Input.GetTouch(i).position);
				if (isHovered)
				{
					break;
				}
			}
		}
		isSelected = !autoDeselect && EventSystem.current.currentSelectedGameObject == base.gameObject;
		isClicked = false;
		if (!normalStateAcquired)
		{
			targetSize = (normalSize = shadow.Size);
			targetDistance = (normalDistance = shadow.OffsetDistance);
			targetColor = (normalColor = shadow.Color);
			normalStateAcquired = true;
		}
		shadow.Size = (targetSize = normalSize);
		shadow.OffsetDistance = (targetDistance = normalDistance);
	}

	private TrueShadow FindTrueShadow()
	{
		TrueShadow[] components = GetComponents<TrueShadow>();
		if (components.Length == 0)
		{
			return null;
		}
		TrueShadowInteractionAnimation[] components2 = GetComponents<TrueShadowInteractionAnimation>();
		int i;
		for (i = 0; i < components2.Length && !(components2[i] == this); i++)
		{
		}
		return components[i];
	}

	private void OnStateChange()
	{
		if (isClicked)
		{
			targetSize = clickedSize;
			targetDistance = clickedDistance;
			targetColor = clickedColor;
		}
		else if (isSelected)
		{
			targetSize = selectedSize;
			targetDistance = selectedDistance;
			targetColor = selectedColor;
		}
		else if (isHovered)
		{
			targetSize = hoverSize;
			targetDistance = hoverDistance;
			targetColor = hoverColor;
		}
		else
		{
			targetSize = normalSize;
			targetDistance = normalDistance;
			targetColor = normalColor;
		}
	}

	private void Update()
	{
		if (!Mathf.Approximately(targetSize, shadow.Size))
		{
			shadow.Size = Mathf.SmoothDamp(shadow.Size, targetSize, ref currentSizeVelocity, smoothTime);
		}
		if (!Mathf.Approximately(targetDistance, shadow.OffsetDistance))
		{
			shadow.OffsetDistance = Mathf.SmoothDamp(shadow.OffsetDistance, targetDistance, ref currentDistanceVelocity, smoothTime);
		}
		Color color = shadow.Color;
		if (!Mathf.Approximately(targetColor.a, color.a))
		{
			float r = Mathf.SmoothDamp(color.r, targetColor.r, ref currentColorRVelocity, smoothTime);
			float g = Mathf.SmoothDamp(color.g, targetColor.g, ref currentColorGVelocity, smoothTime);
			float b = Mathf.SmoothDamp(color.b, targetColor.b, ref currentColorBVelocity, smoothTime);
			float a = Mathf.SmoothDamp(color.a, targetColor.a, ref currentColorAVelocity, smoothTime);
			shadow.Color = new Color(r, g, b, a);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHovered = true;
		OnStateChange();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHovered = false;
		OnStateChange();
	}

	public void OnSelect(BaseEventData eventData)
	{
		isSelected = true;
		OnStateChange();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		isSelected = false;
		OnStateChange();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isClicked = true;
		OnStateChange();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (autoDeselect && EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
		isClicked = false;
		OnStateChange();
	}

	private bool IsOverGameObject(Vector2 position)
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current)
		{
			position = position
		};
		EventSystem.current.RaycastAll(eventData, raycastResults);
		for (int i = 0; i < raycastResults.Count; i++)
		{
			if (raycastResults[i].gameObject == base.gameObject)
			{
				return true;
			}
		}
		return false;
	}
}
}
