using System;
using System.Collections.Generic;

namespace LeTai.TrueShadow
{

internal class ChangeTracker<T> : IChangeTracker
{
	private T previousValue;

	private readonly Func<T> getValue;

	private readonly Func<T, T> onChange;

	private readonly Func<T, T, bool> compare;

	public ChangeTracker(Func<T> getValue, Func<T, T> onChange, Func<T, T, bool> compare = null)
	{
		this.getValue = getValue;
		this.onChange = onChange;
		this.compare = compare ?? new Func<T, T, bool>(EqualityComparer<T>.Default.Equals);
		previousValue = this.getValue();
	}

	public void Forget()
	{
		previousValue = getValue();
	}

	public void Check()
	{
		T val = getValue();
		if (!compare(previousValue, val))
		{
			previousValue = onChange(val);
		}
	}
}
}
