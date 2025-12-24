using System;
using System.Collections.Generic;

// Token: 0x02000045 RID: 69
public class GenericTest
{
	// Token: 0x060002E1 RID: 737 RVA: 0x0001C80C File Offset: 0x0001AA0C
	public T get<T>(int pI) where T : class
	{
		return this.list[pI] as T;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0001C824 File Offset: 0x0001AA24
	public void Add(object pObject)
	{
		this.list.Add(pObject);
	}

	// Token: 0x04000267 RID: 615
	private List<object> list = new List<object>();
}
