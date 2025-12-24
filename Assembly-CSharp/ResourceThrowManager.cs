using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class ResourceThrowManager
{
	// Token: 0x06001744 RID: 5956 RVA: 0x000E6927 File Offset: 0x000E4B27
	public void update(float pElapsed)
	{
		this.updateRemoval();
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000E6930 File Offset: 0x000E4B30
	private void updateRemoval()
	{
		List<ResourceThrowData> tList = this._list;
		for (int i = tList.Count - 1; i >= 0; i--)
		{
			ResourceThrowData tData = tList[i];
			if (tData.isFinished())
			{
				tList.RemoveAt(i);
				Building tBuilding = World.world.buildings.get(tData.building_target_id);
				if (tBuilding != null)
				{
					tBuilding.startShake(0.3f, 0.1f, 0.1f);
				}
			}
		}
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x000E69A0 File Offset: 0x000E4BA0
	public void addNew(Vector2 pStart, Vector2 pEnd, float pDuration, string pResourceAssetId, int pResourceAmount, float pHeight, Building pBuildingTarget)
	{
		ResourceThrowData tData = new ResourceThrowData(pStart, pEnd, pDuration, pResourceAssetId, pResourceAmount, pBuildingTarget.getID(), pHeight);
		this._list.Add(tData);
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000E69D0 File Offset: 0x000E4BD0
	public List<ResourceThrowData> getList()
	{
		return this._list;
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000E69D8 File Offset: 0x000E4BD8
	public void clear()
	{
		this._list.Clear();
	}

	// Token: 0x04001301 RID: 4865
	private List<ResourceThrowData> _list = new List<ResourceThrowData>();
}
