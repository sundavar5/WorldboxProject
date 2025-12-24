using System;
using System.Collections.Generic;

// Token: 0x02000578 RID: 1400
public class StackPool<T> where T : new()
{
	// Token: 0x06002DE8 RID: 11752 RVA: 0x00165C60 File Offset: 0x00163E60
	public T get()
	{
		T tObject;
		if (this.pool.Count > 0)
		{
			tObject = this.pool.Pop();
		}
		else
		{
			tObject = Activator.CreateInstance<T>();
		}
		return tObject;
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x00165C90 File Offset: 0x00163E90
	public U get<U>() where U : T, new()
	{
		U tObject;
		if (this.pool.Count > 0)
		{
			tObject = (U)((object)this.pool.Pop());
		}
		else
		{
			tObject = Activator.CreateInstance<U>();
		}
		return tObject;
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x00165CCA File Offset: 0x00163ECA
	public void release(T pObject)
	{
		this.pool.Push(pObject);
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x00165CD8 File Offset: 0x00163ED8
	public void clear()
	{
		this.pool.Clear();
	}

	// Token: 0x040022C3 RID: 8899
	public Stack<T> pool = new Stack<T>();
}
