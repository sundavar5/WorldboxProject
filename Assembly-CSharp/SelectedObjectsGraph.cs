using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public class SelectedObjectsGraph : IEnumerable<NanoObject>, IEnumerable
{
	// Token: 0x17000203 RID: 515
	// (get) Token: 0x060025BD RID: 9661 RVA: 0x00137034 File Offset: 0x00135234
	public int Count
	{
		get
		{
			if (this._dirty)
			{
				this._selected_count = 0;
				for (int i = 0; i < this._selected_objects.Length; i++)
				{
					if (this._selected_objects[i] != null)
					{
						this._selected_count++;
					}
				}
				this._dirty = false;
			}
			return this._selected_count;
		}
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x00137088 File Offset: 0x00135288
	public void Clear()
	{
		Array.Clear(this._selected_objects, 0, this._selected_objects.Length);
		this._dirty = true;
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x001370A8 File Offset: 0x001352A8
	public void Add(NanoObject pObject)
	{
		if (pObject == null)
		{
			return;
		}
		for (int i = 0; i < this._selected_objects.Length; i++)
		{
			if (this._selected_objects[i] == null)
			{
				this._selected_objects[i] = pObject;
				this._dirty = true;
				return;
			}
		}
		Debug.LogWarning("SelectedObjects is full, cannot add more objects.");
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x001370F4 File Offset: 0x001352F4
	public void Remove(NanoObject pObject)
	{
		if (pObject == null)
		{
			return;
		}
		for (int i = 0; i < this._selected_objects.Length; i++)
		{
			if (this._selected_objects[i] == pObject)
			{
				this._selected_objects[i] = null;
				this._dirty = true;
				return;
			}
		}
		Debug.LogWarning("SelectedObjects does not contain the object to remove.");
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x0013713E File Offset: 0x0013533E
	public IEnumerator<NanoObject> GetEnumerator()
	{
		int num;
		for (int i = 0; i < this._selected_objects.Length; i = num + 1)
		{
			yield return this._selected_objects[i];
			num = i;
		}
		yield break;
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x0013714D File Offset: 0x0013534D
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x00137158 File Offset: 0x00135358
	public NanoObject First()
	{
		foreach (NanoObject obj in this)
		{
			if (obj != null)
			{
				return obj;
			}
		}
		return null;
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x001371A4 File Offset: 0x001353A4
	public bool Contains(NanoObject pObject)
	{
		using (IEnumerator<NanoObject> enumerator = this.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == pObject)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x17000204 RID: 516
	public NanoObject this[int index]
	{
		get
		{
			return this._selected_objects[index];
		}
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x001371FC File Offset: 0x001353FC
	public void RemoveWhere(Func<NanoObject, bool> predicate)
	{
		for (int i = 0; i < this._selected_objects.Length; i++)
		{
			if (this._selected_objects[i] != null && predicate(this._selected_objects[i]))
			{
				this._selected_objects[i] = null;
			}
		}
		this._dirty = true;
	}

	// Token: 0x04001CB3 RID: 7347
	private NanoObject[] _selected_objects = new NanoObject[3];

	// Token: 0x04001CB4 RID: 7348
	private int _selected_count;

	// Token: 0x04001CB5 RID: 7349
	private bool _dirty;
}
