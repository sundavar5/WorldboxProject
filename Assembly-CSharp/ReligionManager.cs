using System;
using System.Collections.Generic;

// Token: 0x020002DC RID: 732
public class ReligionManager : MetaSystemManager<Religion, ReligionData>
{
	// Token: 0x06001B22 RID: 6946 RVA: 0x000FB9DD File Offset: 0x000F9BDD
	public ReligionManager()
	{
		this.type_id = "religion";
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x000FBA00 File Offset: 0x000F9C00
	public Religion newReligion(Actor pFounder, bool pAddDefaultTraits)
	{
		World.world.game_stats.data.religionsCreated += 1L;
		World.world.map_stats.religionsCreated += 1L;
		Religion tNewObject = base.newObject();
		tNewObject.newReligion(pFounder, pFounder.current_tile, pAddDefaultTraits);
		MetaHelper.addRandomTrait<ReligionTrait>(tNewObject, AssetManager.religion_traits);
		this.addRandomTraitFromBiomeToReligion(tNewObject, pFounder.current_tile);
		return tNewObject;
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x000FBA70 File Offset: 0x000F9C70
	private void addRandomTraitFromBiomeToReligion(Religion pReligion, WorldTile pTile)
	{
		BiomeAsset biome_asset = pTile.Type.biome_asset;
		pReligion.addRandomTraitFromBiome<ReligionTrait>(pTile, (biome_asset != null) ? biome_asset.spawn_trait_religion : null, AssetManager.religion_traits);
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x000FBA98 File Offset: 0x000F9C98
	public Religion getMainReligion(List<Actor> pUnitList)
	{
		for (int i = 0; i < pUnitList.Count; i++)
		{
			Actor tActor = pUnitList[i];
			if (tActor.hasReligion())
			{
				Religion tMetaObject = tActor.religion;
				base.countMetaObject(tMetaObject);
			}
		}
		return base.getMostUsedMetaObject();
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x000FBADA File Offset: 0x000F9CDA
	public override void removeObject(Religion pObject)
	{
		World.world.game_stats.data.religionsForgotten += 1L;
		World.world.map_stats.religionsForgotten += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x000FBB18 File Offset: 0x000F9D18
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Religion tReligion = tUnit.religion;
			if (tReligion != null && tReligion.isDirtyUnits())
			{
				tReligion.listUnit(tUnit);
			}
		}
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x000FBB67 File Offset: 0x000F9D67
	public void beginChecksKingdoms()
	{
		if (this._dirty_kingdoms)
		{
			this.updateDirtyKingdoms();
		}
		this._dirty_kingdoms = false;
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x000FBB80 File Offset: 0x000F9D80
	private void updateDirtyKingdoms()
	{
		this.clearAllKingdomLists();
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.hasReligion())
			{
				tKingdom.religion.listKingdom(tKingdom);
			}
		}
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x000FBBE4 File Offset: 0x000F9DE4
	private void clearAllKingdomLists()
	{
		foreach (Religion religion in this)
		{
			religion.clearListKingdoms();
		}
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x000FBC2C File Offset: 0x000F9E2C
	public void beginChecksCities()
	{
		if (this._dirty_cities)
		{
			this.updateDirtyCities();
		}
		this._dirty_cities = false;
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x000FBC44 File Offset: 0x000F9E44
	private void updateDirtyCities()
	{
		this.clearAllCitiesLists();
		foreach (City tCity in World.world.cities)
		{
			if (tCity.hasReligion())
			{
				tCity.religion.listCity(tCity);
			}
		}
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x000FBCA8 File Offset: 0x000F9EA8
	private void clearAllCitiesLists()
	{
		foreach (Religion religion in this)
		{
			religion.clearListCities();
		}
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x000FBCF0 File Offset: 0x000F9EF0
	public void setDirtyKingdoms()
	{
		this._dirty_kingdoms = true;
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x000FBCF9 File Offset: 0x000F9EF9
	public void setDirtyCities()
	{
		this._dirty_cities = true;
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x000FBD02 File Offset: 0x000F9F02
	public override bool isLocked()
	{
		return this.isUnitsDirty() || this._dirty_cities || this._dirty_kingdoms;
	}

	// Token: 0x04001500 RID: 5376
	private bool _dirty_kingdoms = true;

	// Token: 0x04001501 RID: 5377
	private bool _dirty_cities = true;
}
