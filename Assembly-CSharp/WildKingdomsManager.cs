using System;
using System.Collections.Generic;

// Token: 0x02000284 RID: 644
public class WildKingdomsManager : MetaSystemManager<Kingdom, KingdomData>
{
	// Token: 0x060018B2 RID: 6322 RVA: 0x000EBE74 File Offset: 0x000EA074
	public WildKingdomsManager()
	{
		this.type_id = "kingdom_wild";
		foreach (KingdomAsset tAsset in AssetManager.kingdoms.list)
		{
			this.newWildKingdom(tAsset);
		}
		WildKingdomsManager.abandoned = this.get("abandoned");
		WildKingdomsManager.ruins = this.get("ruins");
		WildKingdomsManager.nature = this.get("nature");
		WildKingdomsManager.neutral = this.get("neutral");
		WildKingdomsManager.neutral.data.original_actor_asset = "druid";
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x000EBF44 File Offset: 0x000EA144
	public override void startCollectHistoryData()
	{
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x000EBF46 File Offset: 0x000EA146
	public override void clearLastYearStats()
	{
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x000EBF48 File Offset: 0x000EA148
	private Kingdom newWildKingdom(KingdomAsset pAsset)
	{
		long latest = this._latest;
		this._latest = latest - 1L;
		long tID = latest;
		if (tID == -1L)
		{
			latest = this._latest;
			this._latest = latest - 1L;
			tID = latest;
		}
		Kingdom tKingdom = base.newObject(tID);
		tKingdom.asset = pAsset;
		this._dict.Add(tKingdom.asset.id, tKingdom);
		tKingdom.createWildKingdom();
		if (pAsset.default_civ_color_index != -1)
		{
			tKingdom.data.setColorID(pAsset.default_civ_color_index);
		}
		tKingdom.data.name = pAsset.id;
		return tKingdom;
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x000EBFD8 File Offset: 0x000EA1D8
	public override void clear()
	{
		foreach (Kingdom kingdom in this)
		{
			kingdom.clearListUnits();
			kingdom.clearBuildingList();
		}
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x000EC024 File Offset: 0x000EA224
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_wild;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Kingdom tKingdom = tUnit.kingdom;
			if (tKingdom != null && tKingdom.isDirtyUnits())
			{
				tKingdom.listUnit(tUnit);
			}
		}
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x000EC073 File Offset: 0x000EA273
	public void beginChecksBuildings()
	{
		if (this._dirty_buildings)
		{
			this.updateDirtyBuildings();
		}
		this._dirty_buildings = false;
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000EC08C File Offset: 0x000EA28C
	private void updateDirtyBuildings()
	{
		this.clearAllBuildingLists();
		foreach (Building tBuilding in World.world.buildings)
		{
			if (tBuilding.isAlive() && tBuilding.kingdom.wild)
			{
				tBuilding.kingdom.listBuilding(tBuilding);
			}
		}
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x000EC100 File Offset: 0x000EA300
	public void setDirtyBuildings()
	{
		this._dirty_buildings = true;
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x000EC10C File Offset: 0x000EA30C
	private void clearAllBuildingLists()
	{
		foreach (Kingdom kingdom in this)
		{
			kingdom.clearBuildingList();
		}
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x000EC154 File Offset: 0x000EA354
	public override void checkDeadObjects()
	{
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x000EC158 File Offset: 0x000EA358
	public Kingdom get(string pID)
	{
		if (string.IsNullOrEmpty(pID))
		{
			return null;
		}
		Kingdom tObject;
		this._dict.TryGetValue(pID, out tObject);
		return tObject;
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x000EC17F File Offset: 0x000EA37F
	public override void removeObject(Kingdom pObject)
	{
		this._dict.Remove(pObject.asset.id);
		base.removeObject(pObject);
	}

	// Token: 0x04001370 RID: 4976
	public static Kingdom abandoned;

	// Token: 0x04001371 RID: 4977
	public static Kingdom ruins;

	// Token: 0x04001372 RID: 4978
	public static Kingdom neutral;

	// Token: 0x04001373 RID: 4979
	public static Kingdom nature;

	// Token: 0x04001374 RID: 4980
	protected readonly Dictionary<string, Kingdom> _dict = new Dictionary<string, Kingdom>();

	// Token: 0x04001375 RID: 4981
	private bool _dirty_buildings = true;

	// Token: 0x04001376 RID: 4982
	private long _latest;
}
