using System;
using System.Collections.Generic;

// Token: 0x02000287 RID: 647
public class LanguageManager : MetaSystemManager<Language, LanguageData>
{
	// Token: 0x060018E8 RID: 6376 RVA: 0x000EC675 File Offset: 0x000EA875
	public LanguageManager()
	{
		this.type_id = "language";
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000EC698 File Offset: 0x000EA898
	public Language newLanguage(Actor pActor, bool pAddDefaultTraits)
	{
		World.world.game_stats.data.languagesCreated += 1L;
		World.world.map_stats.languagesCreated += 1L;
		Language tNewObject = base.newObject();
		tNewObject.newLanguage(pActor, pAddDefaultTraits);
		MetaHelper.addRandomTrait<LanguageTrait>(tNewObject, AssetManager.language_traits);
		this.addRandomTraitFromBiomeToLanguage(tNewObject, pActor.current_tile);
		return tNewObject;
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x000EC702 File Offset: 0x000EA902
	public void addRandomTraitFromBiomeToLanguage(Language pLanguage, WorldTile pTile)
	{
		BiomeAsset biome_asset = pTile.Type.biome_asset;
		pLanguage.addRandomTraitFromBiome<LanguageTrait>(pTile, (biome_asset != null) ? biome_asset.spawn_trait_language : null, AssetManager.language_traits);
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x000EC728 File Offset: 0x000EA928
	public Language getMainLanguage(List<Actor> pUnitList)
	{
		for (int i = 0; i < pUnitList.Count; i++)
		{
			Actor tActor = pUnitList[i];
			if (tActor.hasLanguage())
			{
				Language tMetaObject = tActor.language;
				base.countMetaObject(tMetaObject);
			}
		}
		return base.getMostUsedMetaObject();
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000EC76A File Offset: 0x000EA96A
	public override void removeObject(Language pObject)
	{
		World.world.game_stats.data.languagesForgotten += 1L;
		World.world.map_stats.languagesForgotten += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Language tLanguage = tUnit.language;
			if (tLanguage != null && tLanguage.isDirtyUnits())
			{
				tLanguage.listUnit(tUnit);
			}
		}
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x000EC7F7 File Offset: 0x000EA9F7
	public void beginChecksKingdoms()
	{
		if (this._dirty_kingdoms)
		{
			this.updateDirtyKingdoms();
		}
		this._dirty_kingdoms = false;
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x000EC810 File Offset: 0x000EAA10
	private void updateDirtyKingdoms()
	{
		this.clearAllKingdomListst();
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			Language tLanguage = tKingdom.getLanguage();
			if (tLanguage != null)
			{
				tLanguage.listKingdom(tKingdom);
			}
		}
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x000EC874 File Offset: 0x000EAA74
	private void clearAllKingdomListst()
	{
		foreach (Language language in this)
		{
			language.clearListKingdoms();
		}
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x000EC8BC File Offset: 0x000EAABC
	public void beginChecksCities()
	{
		if (this._dirty_cities)
		{
			this.updateDirtyCities();
		}
		this._dirty_cities = false;
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x000EC8D4 File Offset: 0x000EAAD4
	private void updateDirtyCities()
	{
		this.clearAllCitiesListst();
		foreach (City tCity in World.world.cities)
		{
			if (tCity.hasLanguage())
			{
				tCity.language.listCity(tCity);
			}
		}
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x000EC938 File Offset: 0x000EAB38
	private void clearAllCitiesListst()
	{
		foreach (Language language in this)
		{
			language.clearListCities();
		}
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x000EC980 File Offset: 0x000EAB80
	public void setDirtyKingdoms()
	{
		this._dirty_kingdoms = true;
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x000EC989 File Offset: 0x000EAB89
	public void setDirtyCities()
	{
		this._dirty_cities = true;
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x000EC992 File Offset: 0x000EAB92
	public override bool isLocked()
	{
		return this.isUnitsDirty() || this._dirty_cities || this._dirty_kingdoms;
	}

	// Token: 0x0400138D RID: 5005
	private bool _dirty_kingdoms = true;

	// Token: 0x0400138E RID: 5006
	private bool _dirty_cities = true;
}
