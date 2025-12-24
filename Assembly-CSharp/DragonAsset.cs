using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class DragonAsset : ScriptableObject
{
	// Token: 0x06002115 RID: 8469 RVA: 0x0011A758 File Offset: 0x00118958
	public DragonAssetContainer getAsset(DragonState pState)
	{
		if (this._dict == null)
		{
			this._dict = new Dictionary<DragonState, DragonAssetContainer>();
			foreach (DragonAssetContainer tContainer in this.list)
			{
				this._dict.Add(tContainer.id, tContainer);
			}
		}
		return this._dict[pState];
	}

	// Token: 0x0400187C RID: 6268
	private Dictionary<DragonState, DragonAssetContainer> _dict;

	// Token: 0x0400187D RID: 6269
	public DragonAssetContainer[] list;
}
