using System;
using System.Collections.Generic;

// Token: 0x020006D6 RID: 1750
public class SelectedUnitRefresher<T>
{
	// Token: 0x06003830 RID: 14384 RVA: 0x00193C48 File Offset: 0x00191E48
	public bool needsToRefresh(IReadOnlyCollection<T> pCurrent, IReadOnlyCollection<T> pNew)
	{
		foreach (T tButton in pCurrent)
		{
			this._temp_set_1.Add(tButton);
		}
		foreach (T tAsset in pNew)
		{
			this._temp_set_2.Add(tAsset);
		}
		return !this._temp_set_1.SetEquals(this._temp_set_2);
	}

	// Token: 0x06003831 RID: 14385 RVA: 0x00193CEC File Offset: 0x00191EEC
	public void addRendered(T pPrev)
	{
		this._temp_set_1.Add(pPrev);
	}

	// Token: 0x06003832 RID: 14386 RVA: 0x00193CFB File Offset: 0x00191EFB
	public void addCurrent(T pCurrent)
	{
		this._temp_set_2.Add(pCurrent);
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x00193D0A File Offset: 0x00191F0A
	public bool needsToRefresh()
	{
		return !this._temp_set_1.SetEquals(this._temp_set_2);
	}

	// Token: 0x06003834 RID: 14388 RVA: 0x00193D22 File Offset: 0x00191F22
	public void clear()
	{
		this._temp_set_1.Clear();
		this._temp_set_2.Clear();
	}

	// Token: 0x040029B4 RID: 10676
	private HashSet<T> _temp_set_1 = new HashSet<T>();

	// Token: 0x040029B5 RID: 10677
	private HashSet<T> _temp_set_2 = new HashSet<T>();
}
