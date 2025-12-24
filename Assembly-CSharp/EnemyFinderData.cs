using System;
using System.Collections.Generic;

// Token: 0x0200056B RID: 1387
public class EnemyFinderData
{
	// Token: 0x06002D33 RID: 11571 RVA: 0x00160C15 File Offset: 0x0015EE15
	public bool isEmpty()
	{
		return this.list.Count == 0;
	}

	// Token: 0x06002D34 RID: 11572 RVA: 0x00160C25 File Offset: 0x0015EE25
	public void addEnemyList(List<Actor> pList)
	{
		this.list.AddRange(pList);
	}

	// Token: 0x06002D35 RID: 11573 RVA: 0x00160C33 File Offset: 0x0015EE33
	public void addEnemyList(List<Building> pList)
	{
		this.list.AddRange(pList);
	}

	// Token: 0x06002D36 RID: 11574 RVA: 0x00160C41 File Offset: 0x0015EE41
	public void reset()
	{
		this.list.Clear();
	}

	// Token: 0x040022A2 RID: 8866
	public readonly List<BaseSimObject> list = new List<BaseSimObject>();
}
