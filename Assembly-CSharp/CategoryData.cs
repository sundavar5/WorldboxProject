using System;
using System.Collections.Generic;
using UnityPools;

// Token: 0x020006B5 RID: 1717
public class CategoryData : IDisposable
{
	// Token: 0x060036DD RID: 14045 RVA: 0x0018D2F1 File Offset: 0x0018B4F1
	public LinkedListNode<Dictionary<string, long>> AddLast(Dictionary<string, long> pDict)
	{
		return this._data.AddLast(pDict);
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x060036DE RID: 14046 RVA: 0x0018D2FF File Offset: 0x0018B4FF
	public LinkedListNode<Dictionary<string, long>> Last
	{
		get
		{
			return this._data.Last;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x060036DF RID: 14047 RVA: 0x0018D30C File Offset: 0x0018B50C
	public int Count
	{
		get
		{
			return this._data.Count;
		}
	}

	// Token: 0x060036E0 RID: 14048 RVA: 0x0018D31C File Offset: 0x0018B51C
	public void Clear()
	{
		foreach (Dictionary<string, long> toRelease in this._data)
		{
			UnsafeCollectionPool<Dictionary<string, long>, KeyValuePair<string, long>>.Release(toRelease);
		}
		this._data.Clear();
		ListPool<object> listPool = this.db_list;
		if (listPool != null)
		{
			listPool.Dispose();
		}
		this.db_list = null;
	}

	// Token: 0x060036E1 RID: 14049 RVA: 0x0018D390 File Offset: 0x0018B590
	public void Dispose()
	{
		this.Clear();
		this._data = null;
	}

	// Token: 0x040028A8 RID: 10408
	private LinkedList<Dictionary<string, long>> _data = new LinkedList<Dictionary<string, long>>();

	// Token: 0x040028A9 RID: 10409
	internal ListPool<object> db_list;
}
