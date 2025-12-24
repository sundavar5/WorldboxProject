using System;
using System.Collections.Generic;

// Token: 0x020000D1 RID: 209
public class CombatActionHolder
{
	// Token: 0x0600063A RID: 1594 RVA: 0x0005EA80 File Offset: 0x0005CC80
	public void fillFromIDS(List<string> pIDs)
	{
		foreach (string tID in pIDs)
		{
			CombatActionAsset tAsset = AssetManager.combat_action_library.get(tID);
			if (tAsset != null)
			{
				foreach (CombatActionPool tPool in tAsset.pools)
				{
					if (this._combat_action_pools[(int)tPool] == null)
					{
						this._combat_action_pools[(int)tPool] = new List<CombatActionAsset>();
					}
					this._combat_action_pools[(int)tPool].Add(tAsset);
				}
			}
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0005EB20 File Offset: 0x0005CD20
	public List<CombatActionAsset> getPool(CombatActionPool pPool)
	{
		return this._combat_action_pools[(int)pPool];
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0005EB2C File Offset: 0x0005CD2C
	public void reset()
	{
		if (this._has_combat_actions)
		{
			for (int i = 0; i < this._combat_action_pools.Length; i++)
			{
				List<CombatActionAsset> list = this._combat_action_pools[i];
				if (list != null)
				{
					list.Clear();
				}
			}
			this._has_combat_actions = false;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0005EB70 File Offset: 0x0005CD70
	public void mergeWith(CombatActionHolder pCombatActions)
	{
		for (int i = 0; i < pCombatActions._combat_action_pools.Length; i++)
		{
			List<CombatActionAsset> tList = pCombatActions._combat_action_pools[i];
			if (tList != null && tList.Count != 0)
			{
				if (this._combat_action_pools[i] == null)
				{
					this._combat_action_pools[i] = new List<CombatActionAsset>();
				}
				this._combat_action_pools[i].AddRange(tList);
				this._has_combat_actions = true;
			}
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0005EBD0 File Offset: 0x0005CDD0
	public bool isEmpty()
	{
		return !this._has_combat_actions;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0005EBDB File Offset: 0x0005CDDB
	public bool hasAny()
	{
		return this._has_combat_actions;
	}

	// Token: 0x04000717 RID: 1815
	private readonly List<CombatActionAsset>[] _combat_action_pools = new List<CombatActionAsset>[Enum.GetValues(typeof(CombatActionPool)).Length];

	// Token: 0x04000718 RID: 1816
	private bool _has_combat_actions;
}
