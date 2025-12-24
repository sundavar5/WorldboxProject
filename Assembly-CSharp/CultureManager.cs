using System;
using System.Collections.Generic;

// Token: 0x0200025E RID: 606
public class CultureManager : MetaSystemManager<Culture, CultureData>
{
	// Token: 0x060016C3 RID: 5827 RVA: 0x000E43AC File Offset: 0x000E25AC
	public CultureManager()
	{
		this.type_id = "culture";
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000E43D0 File Offset: 0x000E25D0
	public Culture newCulture(Actor pFounder, bool pAddDefaultTraits)
	{
		World.world.game_stats.data.culturesCreated += 1L;
		World.world.map_stats.culturesCreated += 1L;
		Culture tNewObject = base.newObject();
		tNewObject.createCulture(pFounder, pAddDefaultTraits);
		this.addRandomTraitFromBiomeToCulture(tNewObject, pFounder.current_tile);
		MetaHelper.addRandomTrait<CultureTrait>(tNewObject, AssetManager.culture_traits);
		return tNewObject;
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000E443A File Offset: 0x000E263A
	public void addRandomTraitFromBiomeToCulture(Culture pCulture, WorldTile pTile)
	{
		BiomeAsset biome_asset = pTile.Type.biome_asset;
		pCulture.addRandomTraitFromBiome<CultureTrait>(pTile, (biome_asset != null) ? biome_asset.spawn_trait_culture : null, AssetManager.culture_traits);
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x000E445F File Offset: 0x000E265F
	public override void removeObject(Culture pObject)
	{
		World.world.game_stats.data.culturesForgotten += 1L;
		World.world.map_stats.culturesForgotten += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x000E44A0 File Offset: 0x000E26A0
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Culture tCulture = tUnit.culture;
			if (tCulture != null && tCulture.isDirtyUnits())
			{
				tCulture.listUnit(tUnit);
			}
		}
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000E44EF File Offset: 0x000E26EF
	public void beginChecksKingdoms()
	{
		if (this._dirty_kingdoms)
		{
			this.updateDirtyKingdoms();
		}
		this._dirty_kingdoms = false;
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x000E4508 File Offset: 0x000E2708
	private void updateDirtyKingdoms()
	{
		this.clearAllKingdomLists();
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.hasCulture())
			{
				tKingdom.culture.listKingdom(tKingdom);
			}
		}
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000E456C File Offset: 0x000E276C
	private void clearAllKingdomLists()
	{
		foreach (Culture culture in this)
		{
			culture.clearListKingdoms();
		}
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000E45B4 File Offset: 0x000E27B4
	public void beginChecksCities()
	{
		if (this._dirty_cities)
		{
			this.updateDirtyCities();
		}
		this._dirty_cities = false;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000E45CC File Offset: 0x000E27CC
	private void updateDirtyCities()
	{
		this.clearAllCitiesListst();
		foreach (City tCity in World.world.cities)
		{
			if (tCity.hasCulture())
			{
				tCity.culture.listCity(tCity);
			}
		}
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x000E4630 File Offset: 0x000E2830
	private void clearAllCitiesListst()
	{
		foreach (Culture culture in this)
		{
			culture.clearListCities();
		}
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x000E4678 File Offset: 0x000E2878
	public void setDirtyCities()
	{
		this._dirty_cities = true;
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x000E4681 File Offset: 0x000E2881
	public void setDirtyKingdoms()
	{
		this._dirty_kingdoms = true;
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x000E468A File Offset: 0x000E288A
	public override bool isLocked()
	{
		return this.isUnitsDirty() || this._dirty_cities || this._dirty_kingdoms;
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000E46AC File Offset: 0x000E28AC
	public Culture getMainCulture(List<Actor> pUnitList)
	{
		for (int i = 0; i < pUnitList.Count; i++)
		{
			Actor tActor = pUnitList[i];
			if (tActor.hasCulture())
			{
				Culture tMetaObject = tActor.culture;
				base.countMetaObject(tMetaObject);
			}
		}
		return base.getMostUsedMetaObject();
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x000E46EE File Offset: 0x000E28EE
	public override void clear()
	{
		base.clear();
	}

	// Token: 0x040012CB RID: 4811
	private bool _dirty_kingdoms = true;

	// Token: 0x040012CC RID: 4812
	private bool _dirty_cities = true;
}
