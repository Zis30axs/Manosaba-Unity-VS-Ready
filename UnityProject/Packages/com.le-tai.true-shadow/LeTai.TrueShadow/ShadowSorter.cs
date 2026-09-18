using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.TrueShadow
{

[ExecuteAlways]
public class ShadowSorter : MonoBehaviour
{
	private readonly struct SortEntry : IComparable<SortEntry>
	{
		public readonly TrueShadow shadow;

		public readonly Transform shadowTransform;

		public readonly Transform rendererTransform;

		public SortEntry(TrueShadow shadow)
		{
			this.shadow = shadow;
			shadowTransform = shadow.transform;
			rendererTransform = shadow.shadowRenderer.transform;
		}

		public int CompareTo(SortEntry other)
		{
			return other.shadowTransform.GetSiblingIndex().CompareTo(shadowTransform.GetSiblingIndex());
		}
	}

	private readonly struct SortGroup
	{
		public readonly Transform parentTransform;

		public readonly List<SortEntry> sortEntries;

		public SortGroup(SortEntry firstEntry)
		{
			sortEntries = new List<SortEntry> { firstEntry };
			parentTransform = firstEntry.shadowTransform.parent;
		}

		public void Add(SortEntry pair)
		{
			if (!(pair.shadowTransform.parent != parentTransform))
			{
				int num = sortEntries.BinarySearch(pair);
				if (num < 0)
				{
					sortEntries.Insert(~num, pair);
				}
			}
		}

		public override int GetHashCode()
		{
			return parentTransform.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is SortGroup sortGroup)
			{
				return sortGroup.parentTransform == parentTransform;
			}
			return false;
		}
	}

	private static ShadowSorter instance;

	private readonly IndexedSet<TrueShadow> shadows = new IndexedSet<TrueShadow>();

	private readonly IndexedSet<SortGroup> sortGroups = new IndexedSet<SortGroup>();

	[Obsolete]
	public static ShadowSorter Instance
	{
		get
		{
			if (!instance)
			{
				ShadowSorter[] array = Shims.FindObjectsOfType<ShadowSorter>();
				for (int num = array.Length - 1; num > 0; num--)
				{
					UnityEngine.Object.Destroy(array[num]);
				}
				instance = ((array.Length != 0) ? array[0] : null);
				if (!instance)
				{
					instance = new GameObject("ShadowSorter")
					{
						hideFlags = HideFlags.HideAndDontSave
					}.AddComponent<ShadowSorter>();
				}
			}
			return instance;
		}
	}

	public void Register(TrueShadow shadow)
	{
		shadows.AddUnique(shadow);
	}

	public void UnRegister(TrueShadow shadow)
	{
		shadows.Remove(shadow);
	}

	private void LateUpdate()
	{
		if (!this)
		{
			return;
		}
		for (int i = 0; i < shadows.Count; i++)
		{
			TrueShadow trueShadow = shadows[i];
			if ((bool)trueShadow && trueShadow.isActiveAndEnabled)
			{
				trueShadow.CheckHierarchyDirtied();
				if (trueShadow.HierarchyDirty)
				{
					AddSortEntry(trueShadow);
				}
			}
		}
		Sort();
	}

	private void AddSortEntry(TrueShadow shadow)
	{
		SortEntry sortEntry = new SortEntry(shadow);
		SortGroup item = new SortGroup(sortEntry);
		int num = sortGroups.IndexOf(item);
		if (num > -1)
		{
			sortGroups[num].Add(sortEntry);
		}
		else
		{
			sortGroups.Add(item);
		}
	}

	public void Sort()
	{
		for (int i = 0; i < sortGroups.Count; i++)
		{
			SortGroup sortGroup = sortGroups[i];
			if (!sortGroup.parentTransform)
			{
				continue;
			}
			foreach (SortEntry sortEntry in sortGroup.sortEntries)
			{
				sortEntry.rendererTransform.SetParent(sortGroup.parentTransform, worldPositionStays: false);
				int siblingIndex = sortEntry.rendererTransform.GetSiblingIndex();
				int siblingIndex2 = sortEntry.shadowTransform.GetSiblingIndex();
				if (siblingIndex > siblingIndex2)
				{
					sortEntry.rendererTransform.SetSiblingIndex(siblingIndex2);
				}
				else
				{
					sortEntry.rendererTransform.SetSiblingIndex(siblingIndex2 - 1);
				}
				sortEntry.shadow.UnSetHierachyDirty();
			}
			foreach (SortEntry sortEntry2 in sortGroup.sortEntries)
			{
				sortEntry2.shadow.ForgetSiblingIndexChanges();
			}
		}
		sortGroups.Clear();
	}

	private void OnApplicationQuit()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
}
