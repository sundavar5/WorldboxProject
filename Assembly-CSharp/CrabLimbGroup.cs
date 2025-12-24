using System;
using System.Collections.Generic;

// Token: 0x020003F5 RID: 1013
public class CrabLimbGroup
{
	// Token: 0x0600230D RID: 8973 RVA: 0x00124714 File Offset: 0x00122914
	public CrabLimbGroup(CrabLimb pCrabLimb, Actor pActor)
	{
		this.actor = pActor;
		this.crabLimb = pCrabLimb;
		List<CrabLimbItem> tList = new List<CrabLimbItem>();
		foreach (CrabLimbItem tCrabLimb in this.actor.avatar.GetComponentsInChildren<CrabLimbItem>(false))
		{
			if (tCrabLimb.crabLimb == this.crabLimb)
			{
				tList.Add(tCrabLimb);
			}
		}
		this._list = tList.ToArray();
		this._dmg_state = CrabLimbState.HighHP;
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x00124788 File Offset: 0x00122988
	internal void update(float pElapsed)
	{
		if (this._flicker_timer == 0f)
		{
			return;
		}
		this._flicker_timer -= pElapsed;
		if (this._flicker_timer < 0f)
		{
			this._flicker_timer = 0f;
		}
		float tProgress = 1f - this._flicker_timer / 0.15f;
		CrabLimbItem[] list = this._list;
		for (int i = 0; i < list.Length; i++)
		{
			list[i].flicker(tProgress);
		}
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x001247FC File Offset: 0x001229FC
	internal void showDamage()
	{
		if (this.IsFlickering())
		{
			return;
		}
		int tHealth = this.actor.getHealth();
		int tMaxHealth = this.actor.getMaxHealth();
		if ((float)tHealth > (float)tMaxHealth * 0.7f)
		{
			if (this._dmg_state == CrabLimbState.HighHP)
			{
				return;
			}
			this._dmg_state = CrabLimbState.HighHP;
		}
		else if ((float)tHealth > (float)tMaxHealth * 0.35f)
		{
			if (this._dmg_state == CrabLimbState.MedHP)
			{
				return;
			}
			this._dmg_state = CrabLimbState.MedHP;
		}
		else
		{
			if (this._dmg_state == CrabLimbState.LowHP)
			{
				return;
			}
			this._dmg_state = CrabLimbState.LowHP;
		}
		CrabLimbItem[] list = this._list;
		for (int i = 0; i < list.Length; i++)
		{
			list[i].stateChange(this._dmg_state);
		}
		this._flicker_timer = 0.15f;
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x001248A6 File Offset: 0x00122AA6
	internal bool IsFlickering()
	{
		return this._flicker_timer > 0f;
	}

	// Token: 0x0400193C RID: 6460
	public CrabLimb crabLimb;

	// Token: 0x0400193D RID: 6461
	private CrabLimbItem[] _list;

	// Token: 0x0400193E RID: 6462
	private CrabLimbState _dmg_state;

	// Token: 0x0400193F RID: 6463
	private Actor actor;

	// Token: 0x04001940 RID: 6464
	private float _flicker_timer;

	// Token: 0x04001941 RID: 6465
	private const float _flicker_interval = 0.15f;
}
