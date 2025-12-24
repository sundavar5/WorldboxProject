using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000577 RID: 1399
public class ObjectPoolGenericMono<T> where T : Component
{
	// Token: 0x06002DD9 RID: 11737 RVA: 0x001658F7 File Offset: 0x00163AF7
	public ObjectPoolGenericMono(T pPrefab, Transform pParentTransform)
	{
		this._prefab = pPrefab;
		this._parent_transform = pParentTransform;
	}

	// Token: 0x06002DDA RID: 11738 RVA: 0x00165924 File Offset: 0x00163B24
	public void clear(bool pDisable = true)
	{
		this._elements_inactive.Clear();
		this.sortElements();
		foreach (T tElement in this._elements_total)
		{
			this.release(tElement, pDisable);
		}
	}

	// Token: 0x06002DDB RID: 11739 RVA: 0x0016598C File Offset: 0x00163B8C
	private void sortElements()
	{
		this._elements_total.Sort((T a, T b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
	}

	// Token: 0x06002DDC RID: 11740 RVA: 0x001659B8 File Offset: 0x00163BB8
	public T getFirstActive()
	{
		return this._elements_total[0];
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x001659C6 File Offset: 0x00163BC6
	public IReadOnlyList<T> getListTotal()
	{
		return this._elements_total;
	}

	// Token: 0x06002DDE RID: 11742 RVA: 0x001659D0 File Offset: 0x00163BD0
	public void disableInactive()
	{
		foreach (T tElement in this._elements_inactive)
		{
			if (tElement.gameObject.activeSelf)
			{
				tElement.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x00165A40 File Offset: 0x00163C40
	public T getNext()
	{
		T tElement = this.getNewOrActivate();
		this.checkActive(tElement);
		return tElement;
	}

	// Token: 0x06002DE0 RID: 11744 RVA: 0x00165A5C File Offset: 0x00163C5C
	private T getNewOrActivate()
	{
		T tElement;
		if (this._elements_inactive.Count > 0)
		{
			tElement = this._elements_inactive.Dequeue();
		}
		else
		{
			tElement = Object.Instantiate<T>(this._prefab, this._parent_transform);
			this._elements_total.Add(tElement);
			Object @object = tElement;
			string[] array = new string[5];
			int num = 0;
			Type typeFromHandle = typeof(T);
			array[num] = ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
			array[1] = " ";
			array[2] = this._elements_total.Count.ToString();
			array[3] = " ";
			array[4] = tElement.transform.GetSiblingIndex().ToString();
			@object.name = string.Concat(array);
		}
		return tElement;
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x00165B18 File Offset: 0x00163D18
	public void release(T pElement, bool pDisable = true)
	{
		if (this._parent_transform.gameObject.activeInHierarchy)
		{
			pElement.transform.SetAsLastSibling();
		}
		if (!this._elements_inactive.Contains(pElement))
		{
			this._elements_inactive.Enqueue(pElement);
		}
		if (pElement.gameObject.activeSelf && pDisable)
		{
			pElement.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x00165B86 File Offset: 0x00163D86
	private void checkActive(T pElement)
	{
		if (!pElement.gameObject.activeSelf)
		{
			pElement.gameObject.SetActive(true);
		}
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x00165BAB File Offset: 0x00163DAB
	public int countTotal()
	{
		return this._elements_total.Count;
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x00165BB8 File Offset: 0x00163DB8
	public int countInactive()
	{
		return this._elements_inactive.Count;
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x00165BC5 File Offset: 0x00163DC5
	public int countActive()
	{
		return this._elements_total.Count - this._elements_inactive.Count;
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x00165BE0 File Offset: 0x00163DE0
	public void resetParent()
	{
		foreach (T tElement in this._elements_total)
		{
			this.resetParent(tElement);
		}
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x00165C34 File Offset: 0x00163E34
	public void resetParent(T pElement)
	{
		if (!this._parent_transform.gameObject.activeInHierarchy)
		{
			return;
		}
		pElement.transform.SetParent(this._parent_transform);
	}

	// Token: 0x040022BF RID: 8895
	private readonly List<T> _elements_total = new List<T>();

	// Token: 0x040022C0 RID: 8896
	private readonly Queue<T> _elements_inactive = new Queue<T>();

	// Token: 0x040022C1 RID: 8897
	private readonly T _prefab;

	// Token: 0x040022C2 RID: 8898
	private readonly Transform _parent_transform;
}
