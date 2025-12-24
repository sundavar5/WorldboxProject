using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

// Token: 0x02000026 RID: 38
[Serializable]
public class BiomeAsset : Asset, IDescriptionAsset, ILocalizedAsset, IMultiLocalesAsset
{
	// Token: 0x060001EF RID: 495 RVA: 0x0000F44C File Offset: 0x0000D64C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TopTileType getTile(WorldTile pTile)
	{
		TileRank rank_type = pTile.main_type.rank_type;
		if (rank_type == TileRank.Low)
		{
			return this.getTileLow();
		}
		if (rank_type == TileRank.High)
		{
			return this.getTileHigh();
		}
		return null;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000F47C File Offset: 0x0000D67C
	public int getTileCount()
	{
		int num = 0;
		TopTileType tileHigh = this.getTileHigh();
		int num2 = num + ((tileHigh != null) ? tileHigh.getCurrentTiles().Count : 0);
		TopTileType tileLow = this.getTileLow();
		return num2 + ((tileLow != null) ? tileLow.getCurrentTiles().Count : 0);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000F4AF File Offset: 0x0000D6AF
	[CanBeNull]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TopTileType getTileHigh()
	{
		if (this._cached_tile_high == null)
		{
			this._cached_tile_high = AssetManager.top_tiles.get(this.tile_high);
		}
		return this._cached_tile_high;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000F4D5 File Offset: 0x0000D6D5
	[CanBeNull]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TopTileType getTileLow()
	{
		if (this._cached_tile_low == null)
		{
			this._cached_tile_low = AssetManager.top_tiles.get(this.tile_low);
		}
		return this._cached_tile_low;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000F4FC File Offset: 0x0000D6FC
	public void addTree(string pID, int pRateAmount = 1)
	{
		this.grow_vegetation_auto = true;
		if (this.pot_trees_spawn == null)
		{
			this.pot_trees_spawn = new List<string>();
		}
		for (int i = 0; i < pRateAmount; i++)
		{
			this.pot_trees_spawn.Add(pID);
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000F53C File Offset: 0x0000D73C
	public void addPlant(string pID, int pRateAmount = 1)
	{
		this.grow_vegetation_auto = true;
		if (this.pot_plants_spawn == null)
		{
			this.pot_plants_spawn = new List<string>();
		}
		for (int i = 0; i < pRateAmount; i++)
		{
			this.pot_plants_spawn.Add(pID);
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000F57C File Offset: 0x0000D77C
	public void addBush(string pID, int pRateAmount = 1)
	{
		this.grow_vegetation_auto = true;
		if (this.pot_bushes_spawn == null)
		{
			this.pot_bushes_spawn = new List<string>();
		}
		for (int i = 0; i < pRateAmount; i++)
		{
			this.pot_bushes_spawn.Add(pID);
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000F5BC File Offset: 0x0000D7BC
	public void addMineral(string pID, int pRateAmount = 1)
	{
		this.grow_minerals_auto = true;
		if (this.pot_minerals_spawn == null)
		{
			this.pot_minerals_spawn = new List<string>();
		}
		for (int i = 0; i < pRateAmount; i++)
		{
			this.pot_minerals_spawn.Add(pID);
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000F5FC File Offset: 0x0000D7FC
	public void addUnit(string pID, int pRateAmount = 1)
	{
		this.pot_spawn_units_auto = true;
		if (this.pot_units_spawn == null)
		{
			this.pot_units_spawn = new List<string>();
		}
		for (int i = 0; i < pRateAmount; i++)
		{
			this.pot_units_spawn.Add(pID);
		}
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000F63C File Offset: 0x0000D83C
	public void addSapientUnit(string pID, int pRateAmount = 1)
	{
		this.pot_spawn_units_auto = true;
		if (this.pot_sapient_units_spawn == null)
		{
			this.pot_sapient_units_spawn = new List<string>();
		}
		for (int i = 0; i < pRateAmount; i++)
		{
			this.pot_sapient_units_spawn.Add(pID);
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000F67B File Offset: 0x0000D87B
	internal void addActorTrait(string pTrait)
	{
		if (this.spawn_trait_actor == null)
		{
			this.spawn_trait_actor = new List<string>();
		}
		if (!this.spawn_trait_actor.Contains(pTrait))
		{
			this.spawn_trait_actor.Add(pTrait);
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000F6AA File Offset: 0x0000D8AA
	internal void addSubspeciesTrait(string pTrait)
	{
		if (this.spawn_trait_subspecies == null)
		{
			this.spawn_trait_subspecies = new List<string>();
		}
		if (!this.spawn_trait_subspecies.Contains(pTrait))
		{
			this.spawn_trait_subspecies.Add(pTrait);
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000F6D9 File Offset: 0x0000D8D9
	internal void addSubspeciesTraitAlways(string pTrait)
	{
		if (this.spawn_trait_subspecies_always == null)
		{
			this.spawn_trait_subspecies_always = new List<string>();
		}
		if (!this.spawn_trait_subspecies_always.Contains(pTrait))
		{
			this.spawn_trait_subspecies_always.Add(pTrait);
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000F708 File Offset: 0x0000D908
	internal void addSubspeciesTraitEvolution(string pTrait)
	{
		if (this.evolution_trait_subspecies == null)
		{
			this.evolution_trait_subspecies = new List<string>();
		}
		if (!this.evolution_trait_subspecies.Contains(pTrait))
		{
			this.evolution_trait_subspecies.Add(pTrait);
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000F737 File Offset: 0x0000D937
	internal void addCultureTrait(string pTrait)
	{
		if (this.spawn_trait_culture == null)
		{
			this.spawn_trait_culture = new List<string>();
		}
		if (!this.spawn_trait_culture.Contains(pTrait))
		{
			this.spawn_trait_culture.Add(pTrait);
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000F766 File Offset: 0x0000D966
	internal void addLanguageTrait(string pTrait)
	{
		if (this.spawn_trait_language == null)
		{
			this.spawn_trait_language = new List<string>();
		}
		if (!this.spawn_trait_language.Contains(pTrait))
		{
			this.spawn_trait_language.Add(pTrait);
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000F795 File Offset: 0x0000D995
	internal void addClanTrait(string pTrait)
	{
		if (this.spawn_trait_clan == null)
		{
			this.spawn_trait_clan = new List<string>();
		}
		if (!this.spawn_trait_clan.Contains(pTrait))
		{
			this.spawn_trait_clan.Add(pTrait);
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
	internal void addReligionTrait(string pTrait)
	{
		if (this.spawn_trait_religion == null)
		{
			this.spawn_trait_religion = new List<string>();
		}
		if (!this.spawn_trait_religion.Contains(pTrait))
		{
			this.spawn_trait_religion.Add(pTrait);
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000F7F3 File Offset: 0x0000D9F3
	public string getLocaleID()
	{
		return this.localized_key.Underscore();
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000F800 File Offset: 0x0000DA00
	public string getDescriptionID()
	{
		return this.getLocaleID() + "_description";
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000F812 File Offset: 0x0000DA12
	public IEnumerable<string> getLocaleIDs()
	{
		if (!LocalizedTextManager.stringExists(this.getLocaleID() + "_seeds"))
		{
			yield break;
		}
		yield return this.getLocaleID() + "_seeds";
		yield break;
	}

	// Token: 0x0400015E RID: 350
	public GrowTypeSelector grow_type_selector_trees;

	// Token: 0x0400015F RID: 351
	public GrowTypeSelector grow_type_selector_plants;

	// Token: 0x04000160 RID: 352
	public GrowTypeSelector grow_type_selector_bushes;

	// Token: 0x04000161 RID: 353
	public GrowTypeSelector grow_type_selector_minerals;

	// Token: 0x04000162 RID: 354
	public List<string> pot_sapient_units_spawn;

	// Token: 0x04000163 RID: 355
	public List<string> pot_units_spawn;

	// Token: 0x04000164 RID: 356
	public List<string> pot_trees_spawn;

	// Token: 0x04000165 RID: 357
	public List<string> pot_plants_spawn;

	// Token: 0x04000166 RID: 358
	public List<string> pot_bushes_spawn;

	// Token: 0x04000167 RID: 359
	public List<string> pot_minerals_spawn;

	// Token: 0x04000168 RID: 360
	public bool grow_vegetation_auto;

	// Token: 0x04000169 RID: 361
	public bool grow_minerals_auto;

	// Token: 0x0400016A RID: 362
	public bool pot_spawn_units_auto;

	// Token: 0x0400016B RID: 363
	public bool cold_biome;

	// Token: 0x0400016C RID: 364
	public bool dark_biome;

	// Token: 0x0400016D RID: 365
	public bool spread_ignore_burned_stages;

	// Token: 0x0400016E RID: 366
	public bool spread_biome;

	// Token: 0x0400016F RID: 367
	public bool spread_by_drops_water;

	// Token: 0x04000170 RID: 368
	public bool spread_by_drops_fire;

	// Token: 0x04000171 RID: 369
	public bool spread_by_drops_curse;

	// Token: 0x04000172 RID: 370
	public bool spread_by_drops_blessing;

	// Token: 0x04000173 RID: 371
	public bool spread_by_drops_powerup;

	// Token: 0x04000174 RID: 372
	public bool spread_by_drops_acid;

	// Token: 0x04000175 RID: 373
	public bool spread_by_drops_coffee;

	// Token: 0x04000176 RID: 374
	public bool special_biome;

	// Token: 0x04000177 RID: 375
	[DefaultValue(6)]
	public int grow_strength = 6;

	// Token: 0x04000178 RID: 376
	public string spread_only_in_era;

	// Token: 0x04000179 RID: 377
	public string tile_low;

	// Token: 0x0400017A RID: 378
	public string tile_high;

	// Token: 0x0400017B RID: 379
	[NonSerialized]
	private TopTileType _cached_tile_high;

	// Token: 0x0400017C RID: 380
	[NonSerialized]
	private TopTileType _cached_tile_low;

	// Token: 0x0400017D RID: 381
	public int generator_pot_amount;

	// Token: 0x0400017E RID: 382
	public int generator_max_size;

	// Token: 0x0400017F RID: 383
	public string localized_key;

	// Token: 0x04000180 RID: 384
	public int loot_generation;

	// Token: 0x04000181 RID: 385
	public string[] subspecies_name_suffix;

	// Token: 0x04000182 RID: 386
	public List<string> spawn_trait_actor;

	// Token: 0x04000183 RID: 387
	public List<string> spawn_trait_subspecies;

	// Token: 0x04000184 RID: 388
	public List<string> spawn_trait_subspecies_always;

	// Token: 0x04000185 RID: 389
	public List<string> spawn_trait_culture;

	// Token: 0x04000186 RID: 390
	public List<string> spawn_trait_clan;

	// Token: 0x04000187 RID: 391
	public List<string> spawn_trait_language;

	// Token: 0x04000188 RID: 392
	public List<string> spawn_trait_religion;

	// Token: 0x04000189 RID: 393
	public List<string> evolution_trait_subspecies;
}
