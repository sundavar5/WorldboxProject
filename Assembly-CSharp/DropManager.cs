using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class DropManager
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x000BD268 File Offset: 0x000BB468
	public DropManager(Transform pDropContainer)
	{
		this._dropContainer = pDropContainer;
		string tPath = "effects/p_drop";
		this._original_drop = (GameObject)Resources.Load(tPath, typeof(GameObject));
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x000BD2B0 File Offset: 0x000BB4B0
	public Drop spawn(WorldTile pTile, string pDropID, float zHeight = -1f, float pScale = -1f, long pCasterId = -1L)
	{
		DropAsset tAsset = AssetManager.drops.get(pDropID);
		return this.spawn(pTile, tAsset, zHeight, pScale, false, pCasterId);
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x000BD2D8 File Offset: 0x000BB4D8
	public Drop spawn(WorldTile pTile, DropAsset pAsset, float zHeight = -1f, float pScale = -1f, bool pForceSurprise = false, long pCasterId = -1L)
	{
		Drop tDrop = this.getObject();
		if (pForceSurprise)
		{
			tDrop.setForceSurprise();
		}
		tDrop.launchStraight(pTile, pAsset, zHeight);
		if (pScale == -1f)
		{
			pScale = pAsset.default_scale;
		}
		tDrop.setScale(new Vector3(pScale, pScale, tDrop.transform.localScale.z));
		tDrop.setCasterId(pCasterId);
		return tDrop;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x000BD338 File Offset: 0x000BB538
	public void spawnParabolicDrop(WorldTile pTile, string pDropID, float pStartHeight = 0f, float pMinHeight = 0f, float pMaxHeight = 0f, float pMinRadius = 0f, float pMaxRadius = 0f, float pScale = -1f)
	{
		this.spawn(pTile, pDropID, pMinHeight, pScale, -1L).launchParabolic(pStartHeight, pMinHeight, pMaxHeight, pMinRadius, pMaxRadius);
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x000BD358 File Offset: 0x000BB558
	public void clear()
	{
		List<Drop> tDrops = this._drops;
		for (int i = 0; i < tDrops.Count; i++)
		{
			tDrops[i].makeInactive();
		}
		this._activeIndex = 0;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x000BD390 File Offset: 0x000BB590
	private void killObject(Drop pObject)
	{
		pObject.makeInactive();
		int tDeadIndex = pObject.drop_index - 1;
		int tAliveIndex = this._activeIndex - 1;
		List<Drop> tDrops = this._drops;
		if (tDeadIndex != tAliveIndex)
		{
			Drop tSwitchDrop = tDrops[tAliveIndex];
			tDrops[tAliveIndex] = pObject;
			tDrops[tDeadIndex] = tSwitchDrop;
			pObject.drop_index = tAliveIndex + 1;
			tSwitchDrop.drop_index = tDeadIndex + 1;
		}
		if (this._activeIndex > 0)
		{
			this._activeIndex--;
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000BD404 File Offset: 0x000BB604
	public void landDrop(Drop pObject)
	{
		WorldTile tTile = pObject.current_tile;
		this.killObject(pObject);
		if (tTile == null)
		{
			return;
		}
		World.world.flash_effects.flashPixel(tTile, 14, ColorType.White);
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x000BD438 File Offset: 0x000BB638
	public Drop getObject()
	{
		List<Drop> tDrops = this._drops;
		Drop tDrop;
		if (tDrops.Count > this._activeIndex)
		{
			tDrop = tDrops[this._activeIndex];
		}
		else
		{
			tDrop = Object.Instantiate<GameObject>(this._original_drop, this._dropContainer).GetComponent<Drop>();
			tDrop.gameObject.layer = this._dropContainer.gameObject.layer;
			tDrop.transform.parent = this._dropContainer;
			tDrops.Add(tDrop);
			tDrop.drop_index = tDrops.Count;
		}
		this._activeIndex++;
		tDrop.prepare();
		return tDrop;
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x000BD4D4 File Offset: 0x000BB6D4
	public void update(float pElapsed)
	{
		Bench.bench("drops", "game_total", false);
		if (this._timeout_timer > 0f)
		{
			this._timeout_timer -= World.world.delta_time;
		}
		List<Drop> tDrops = this._drops;
		for (int i = this._activeIndex - 1; i >= 0; i--)
		{
			Drop tObj = tDrops[i];
			if (tObj.created && tObj.active)
			{
				tObj.update(pElapsed);
			}
			else if (this._activeIndex == tObj.drop_index)
			{
				this._activeIndex--;
				Debug.LogError("do we ever hit this??? " + this._activeIndex.ToString());
			}
		}
		Bench.benchEnd("drops", "game_total", false, 0L, false);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x000BD5A0 File Offset: 0x000BB7A0
	public void debug(DebugTool pTool)
	{
		pTool.setText("drops total", this._drops.Count.ToString() ?? "", 0f, false, 0L, false, false, "");
		pTool.setText("drops active", this._activeIndex.ToString() ?? "", 0f, false, 0L, false, false, "");
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x000BD611 File Offset: 0x000BB811
	public int getActiveIndex()
	{
		return this._activeIndex;
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x000BD61C File Offset: 0x000BB81C
	private void debugString()
	{
		string test = "";
		for (int i = 0; i < this._drops.Count; i++)
		{
			if (this._drops[i].active)
			{
				test += "O";
			}
			else
			{
				test += "x";
			}
		}
		Debug.Log(test + " ::: " + this._activeIndex.ToString());
	}

	// Token: 0x04000D4A RID: 3402
	private List<Drop> _drops = new List<Drop>();

	// Token: 0x04000D4B RID: 3403
	private float _timeout_timer;

	// Token: 0x04000D4C RID: 3404
	private int _activeIndex;

	// Token: 0x04000D4D RID: 3405
	private GameObject _original_drop;

	// Token: 0x04000D4E RID: 3406
	private Transform _dropContainer;
}
