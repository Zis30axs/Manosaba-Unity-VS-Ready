using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.TrueShadow
{

public class ProjectSettings : ScriptableObject
{
	public const string DEFAULT_RESOURCE_PATH = "True Shadow Project Settings Default";

	public const string RESOURCE_PATH = "True Shadow Project Settings";

	private static ProjectSettings instance;

	[SerializeField]
	internal bool useGlobalAngleByDefault;

	[Knob]
	[SerializeField]
	internal float globalAngle = 90f;

	[SerializeField]
	internal bool showQuickPresetsButtons = true;

	[SerializeField]
	internal List<QuickPreset> quickPresets = new List<QuickPreset>(8);

	public static ProjectSettings Instance
	{
		get
		{
			if (!instance)
			{
				instance = Resources.Load<ProjectSettings>("True Shadow Project Settings");
			}
			if (!instance)
			{
				Debug.LogError("Can't find \"True Shadow Project Settings\" in a Resources folder. Please restore the file or re-install True Shadow.");
			}
			return instance;
		}
	}

	public bool UseGlobalAngleByDefault
	{
		get
		{
			return useGlobalAngleByDefault;
		}
		set
		{
			useGlobalAngleByDefault = value;
		}
	}

	public float GlobalAngle
	{
		get
		{
			return globalAngle;
		}
		set
		{
			globalAngle = value;
			this.globalAngleChanged?.Invoke(globalAngle);
		}
	}

	public bool ShowQuickPresetsButtons
	{
		get
		{
			return showQuickPresetsButtons;
		}
		private set
		{
			showQuickPresetsButtons = value;
		}
	}

	public List<QuickPreset> QuickPresets
	{
		get
		{
			return quickPresets;
		}
		set
		{
			quickPresets = value;
		}
	}

	public event Action<float> globalAngleChanged;
}
}
