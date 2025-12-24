using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class BaseEffectController : BaseMapObject
{
	// Token: 0x06001FBF RID: 8127 RVA: 0x00111F20 File Offset: 0x00110120
	internal override void create()
	{
		base.create();
		this._timer_interval = 0.9f;
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x00111F33 File Offset: 0x00110133
	public void setLimits(int pLimitObjects, bool pLimitUnload)
	{
		if (pLimitObjects > 0)
		{
			this._object_limit_used = true;
		}
		this._object_limit = pLimitObjects;
		this._limit_unload = pLimitUnload;
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x00111F50 File Offset: 0x00110150
	public BaseEffect GetObject()
	{
		List<BaseEffect> tList = this._list;
		BaseEffect tNewEffect;
		if (tList.Count > this._active_index)
		{
			tNewEffect = tList[this._active_index];
		}
		else
		{
			tNewEffect = Object.Instantiate<Transform>(this.prefab).gameObject.GetComponent<BaseEffect>();
			this.addNewObject(tNewEffect);
			if (!tNewEffect.created)
			{
				tNewEffect.create();
			}
			tList.Add(tNewEffect);
			tNewEffect.effectIndex = tList.Count;
		}
		this._active_index++;
		tNewEffect.activate();
		return tNewEffect;
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x00111FD6 File Offset: 0x001101D6
	public int getActiveIndex()
	{
		return this._active_index;
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x00111FDE File Offset: 0x001101DE
	internal void addNewObject(BaseEffect pEffect)
	{
		pEffect.controller = this;
		pEffect.transform.parent = base.transform;
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x00111FF8 File Offset: 0x001101F8
	public void killObject(BaseEffect pObject)
	{
		if (!pObject.active)
		{
			return;
		}
		this.makeInactive(pObject);
		List<BaseEffect> tList = this._list;
		int deadIndex = pObject.effectIndex - 1;
		int aliveIndex = this._active_index - 1;
		if (deadIndex != aliveIndex)
		{
			BaseEffect switchObject = tList[aliveIndex];
			tList[aliveIndex] = pObject;
			tList[deadIndex] = switchObject;
			pObject.effectIndex = aliveIndex + 1;
			switchObject.effectIndex = deadIndex + 1;
		}
		if (this._active_index > 0)
		{
			this._active_index--;
		}
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x00112073 File Offset: 0x00110273
	private void makeInactive(BaseEffect pObject)
	{
		pObject.deactivate();
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x0011207C File Offset: 0x0011027C
	private void debugString()
	{
		string test = "";
		List<BaseEffect> tList = this._list;
		for (int i = 0; i < tList.Count; i++)
		{
			if (tList[i].active)
			{
				test += "O";
			}
			else
			{
				test += "x";
			}
		}
		Debug.Log(test + " ::: " + this._active_index.ToString());
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x001120EA File Offset: 0x001102EA
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateChildren(pElapsed);
		this.updateSpawn(pElapsed);
	}

	// Token: 0x06001FC8 RID: 8136 RVA: 0x00112104 File Offset: 0x00110304
	private void updateSpawn(float pElapsed)
	{
		if (World.world.isPaused())
		{
			return;
		}
		if (this.useInterval)
		{
			if (this._timer > 0f)
			{
				this._timer -= pElapsed;
				return;
			}
			this._timer = this._timer_interval;
			this.spawn();
		}
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x00112154 File Offset: 0x00110354
	private void updateChildren(float pElapsed)
	{
		List<BaseEffect> tList = this._list;
		for (int i = this._active_index - 1; i >= 0; i--)
		{
			BaseEffect tObj = tList[i];
			if (tObj.created && tObj.active)
			{
				tObj.update(pElapsed);
			}
		}
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x0011219A File Offset: 0x0011039A
	public virtual void spawn()
	{
	}

	// Token: 0x06001FCB RID: 8139 RVA: 0x0011219C File Offset: 0x0011039C
	public BaseEffect spawnNew()
	{
		if (this.isLimitReached())
		{
			if (!this._limit_unload)
			{
				return null;
			}
			this.killOldest();
		}
		BaseEffect tObject = this.GetObject();
		if (tObject.sprite_animation != null)
		{
			tObject.sprite_animation.resetAnim(0);
		}
		return tObject;
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x001121E8 File Offset: 0x001103E8
	private void killOldest()
	{
		if (this._list.Count == 0)
		{
			return;
		}
		BaseEffect tEffectOldest = this._list[0];
		double tEffectOldestTimestamp = double.MaxValue;
		foreach (BaseEffect tEffect in this._list)
		{
			if (tEffect.timestamp_spawned < tEffectOldestTimestamp)
			{
				tEffectOldest = tEffect;
				tEffectOldestTimestamp = tEffect.timestamp_spawned;
			}
		}
		this.killObject(tEffectOldest);
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x00112274 File Offset: 0x00110474
	internal bool isLimitReached()
	{
		return this._object_limit_used && this._active_index >= this._object_limit;
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x00112290 File Offset: 0x00110490
	internal void clear()
	{
		List<BaseEffect> tList = this._list;
		for (int i = 0; i < tList.Count; i++)
		{
			BaseEffect tEffect = tList[i];
			this.makeInactive(tEffect);
		}
		this._active_index = 0;
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x001122CB File Offset: 0x001104CB
	public bool isAnyActive()
	{
		return this._active_index > 0;
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x001122D8 File Offset: 0x001104D8
	internal void debug(DebugTool pTool)
	{
		pTool.setText(base.name, this._active_index.ToString() + "/" + this._list.Count.ToString(), 0f, false, 0L, false, false, "");
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x00112328 File Offset: 0x00110528
	internal List<BaseEffect> getList()
	{
		return this._list;
	}

	// Token: 0x0400171E RID: 5918
	public Transform prefab;

	// Token: 0x0400171F RID: 5919
	private int _active_index;

	// Token: 0x04001720 RID: 5920
	private readonly List<BaseEffect> _list = new List<BaseEffect>();

	// Token: 0x04001721 RID: 5921
	private float _timer;

	// Token: 0x04001722 RID: 5922
	private float _timer_interval = 1f;

	// Token: 0x04001723 RID: 5923
	private bool _object_limit_used;

	// Token: 0x04001724 RID: 5924
	private int _object_limit;

	// Token: 0x04001725 RID: 5925
	private bool _limit_unload;

	// Token: 0x04001726 RID: 5926
	public bool useInterval = true;

	// Token: 0x04001727 RID: 5927
	public EffectAsset asset;
}
