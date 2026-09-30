using LeTai.TrueShadow.PluginInterfaces;
using UnityEngine;

namespace LeTai.TrueShadow
{

[ExecuteAlways]
[RequireComponent(typeof(TrueShadow))]
public class DisableShadowCache : MonoBehaviour, ITrueShadowCustomHashProvider
{
	private TrueShadow shadow;

	public bool everyFrame;

	private void OnEnable()
	{
		shadow = GetComponent<TrueShadow>();
		Dirty();
	}

	private void Update()
	{
		if (everyFrame)
		{
			Dirty();
		}
	}

	private void Dirty()
	{
		shadow.CustomHash = Random.Range(int.MinValue, int.MaxValue);
		shadow.SetTextureDirty();
	}

	private void OnDisable()
	{
		shadow.CustomHash = 0;
		shadow.SetTextureDirty();
	}
}
}
