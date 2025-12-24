using System;
using System.Collections.Generic;

// Token: 0x02000576 RID: 1398
public class MultiStackPool<T> where T : new()
{
	// Token: 0x06002DD5 RID: 11733 RVA: 0x0016580C File Offset: 0x00163A0C
	public U get<U>() where U : T, new()
	{
		Type tType = typeof(U);
		StackPool<T> tPool;
		if (!this._pools.TryGetValue(tType, out tPool))
		{
			tPool = new StackPool<T>();
			this._pools.Add(tType, tPool);
		}
		return tPool.get<U>();
	}

	// Token: 0x06002DD6 RID: 11734 RVA: 0x00165850 File Offset: 0x00163A50
	public void release(T pObject)
	{
		Type tType = pObject.GetType();
		StackPool<T> tPool;
		if (!this._pools.TryGetValue(tType, out tPool))
		{
			return;
		}
		tPool.release(pObject);
	}

	// Token: 0x06002DD7 RID: 11735 RVA: 0x00165884 File Offset: 0x00163A84
	public void clear()
	{
		foreach (StackPool<T> stackPool in this._pools.Values)
		{
			stackPool.clear();
		}
		this._pools.Clear();
	}

	// Token: 0x040022BE RID: 8894
	private Dictionary<Type, StackPool<T>> _pools = new Dictionary<Type, StackPool<T>>();
}
