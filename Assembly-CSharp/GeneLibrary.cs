using System;
using System.Collections.Generic;

// Token: 0x02000102 RID: 258
[Serializable]
public class GeneLibrary : BaseTraitLibrary<GeneAsset>
{
	// Token: 0x060007D8 RID: 2008 RVA: 0x0006F09B File Offset: 0x0006D29B
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return null;
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0006F09E File Offset: 0x0006D29E
	public override void init()
	{
		base.init();
		this.addSpecial();
		this.addBaseStats();
		this.addFightStats();
		this.addBonusStats();
		this.addAttributes();
		GeneLibrary.gene_for_generation = this.get("temp_for_generation");
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0006F0D4 File Offset: 0x0006D2D4
	public string getRandomNormalGene()
	{
		int tMax = 10;
		foreach (GeneAsset tAsset in this.list.LoopRandom<GeneAsset>())
		{
			if (tMax <= 0)
			{
				break;
			}
			tMax--;
			if (tAsset.isAvailable() && !tAsset.is_empty && !tAsset.for_generation)
			{
				return tAsset.id;
			}
		}
		return null;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0006F150 File Offset: 0x0006D350
	private void addBonusStats()
	{
		this.add(new GeneAsset
		{
			id = "attack_speed"
		});
		this.t.base_stats["attack_speed"] = 1f;
		this.t.setUnlockedWithAchievement("achievementGenesExplorer");
		this.add(new GeneAsset
		{
			id = "scale_plus"
		});
		this.t.base_stats["scale"] = 0.03f;
		this.t.setUnlockedWithAchievement("achievementSimpleStupidGenetics");
		this.add(new GeneAsset
		{
			id = "scale_minus"
		});
		this.t.base_stats["scale"] = -0.01f;
		this.t.setUnlockedWithAchievement("achievementAntWorld");
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0006F220 File Offset: 0x0006D420
	private void addFightStats()
	{
		this.add(new GeneAsset
		{
			id = "armor_1",
			is_simple = true
		});
		this.t.base_stats["armor"] = 1f;
		this.add(new GeneAsset
		{
			id = "armor_2"
		});
		this.t.base_stats["armor"] = 6f;
		this.add(new GeneAsset
		{
			id = "armor_3"
		});
		this.t.base_stats["armor"] = 10f;
		this.add(new GeneAsset
		{
			id = "damage_1",
			is_simple = true
		});
		this.t.base_stats["damage"] = 1f;
		this.add(new GeneAsset
		{
			id = "damage_2"
		});
		this.t.base_stats["damage"] = 6f;
		this.add(new GeneAsset
		{
			id = "damage_3"
		});
		this.t.base_stats["damage"] = 10f;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x0006F364 File Offset: 0x0006D564
	private void addBaseStats()
	{
		this.add(new GeneAsset
		{
			id = "birth_rate_1"
		});
		this.t.base_stats["birth_rate"] = 1f;
		this.add(new GeneAsset
		{
			id = "offspring_1"
		});
		this.t.base_stats["offspring"] = 1f;
		this.add(new GeneAsset
		{
			id = "offspring_2"
		});
		this.t.base_stats["offspring"] = 3f;
		this.add(new GeneAsset
		{
			id = "offspring_3"
		});
		this.t.base_stats["offspring"] = 5f;
		this.add(new GeneAsset
		{
			id = "offspring_4"
		});
		this.t.base_stats["offspring"] = 10f;
		this.add(new GeneAsset
		{
			id = "lifespan_1"
		});
		this.t.base_stats["lifespan"] = 5f;
		this.add(new GeneAsset
		{
			id = "lifespan_2"
		});
		this.t.base_stats["lifespan"] = 20f;
		this.add(new GeneAsset
		{
			id = "lifespan_3"
		});
		this.t.base_stats["lifespan"] = 50f;
		this.add(new GeneAsset
		{
			id = "lifespan_4"
		});
		this.t.base_stats["lifespan"] = 100f;
		this.add(new GeneAsset
		{
			id = "health_1",
			is_simple = true
		});
		this.t.base_stats["health"] = 1f;
		this.add(new GeneAsset
		{
			id = "health_2",
			is_simple = true
		});
		this.t.base_stats["health"] = 10f;
		this.add(new GeneAsset
		{
			id = "health_3"
		});
		this.t.base_stats["health"] = 50f;
		this.add(new GeneAsset
		{
			id = "health_4"
		});
		this.t.base_stats["health"] = 100f;
		this.add(new GeneAsset
		{
			id = "health_5"
		});
		this.t.base_stats["health"] = 300f;
		this.add(new GeneAsset
		{
			id = "stamina_1",
			is_simple = true
		});
		this.t.base_stats["stamina"] = 10f;
		this.add(new GeneAsset
		{
			id = "stamina_2"
		});
		this.t.base_stats["stamina"] = 50f;
		this.add(new GeneAsset
		{
			id = "stamina_3"
		});
		this.t.base_stats["stamina"] = 100f;
		this.add(new GeneAsset
		{
			id = "speed_1"
		});
		this.t.base_stats["speed"] = 1f;
		this.add(new GeneAsset
		{
			id = "speed_2"
		});
		this.t.base_stats["speed"] = 2f;
		this.add(new GeneAsset
		{
			id = "speed_3"
		});
		this.t.base_stats["speed"] = 5f;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0006F75C File Offset: 0x0006D95C
	private void addAttributes()
	{
		this.add(new GeneAsset
		{
			id = "diplomacy_1",
			is_simple = true
		});
		this.t.base_stats["diplomacy"] = 1f;
		this.add(new GeneAsset
		{
			id = "diplomacy_2"
		});
		this.t.base_stats["diplomacy"] = 2f;
		this.add(new GeneAsset
		{
			id = "diplomacy_3"
		});
		this.t.base_stats["diplomacy"] = 3f;
		this.add(new GeneAsset
		{
			id = "warfare_1",
			is_simple = true
		});
		this.t.base_stats["warfare"] = 1f;
		this.add(new GeneAsset
		{
			id = "warfare_2"
		});
		this.t.base_stats["warfare"] = 2f;
		this.add(new GeneAsset
		{
			id = "warfare_3"
		});
		this.t.base_stats["warfare"] = 3f;
		this.add(new GeneAsset
		{
			id = "stewardship_1",
			is_simple = true
		});
		this.t.base_stats["stewardship"] = 1f;
		this.add(new GeneAsset
		{
			id = "stewardship_2"
		});
		this.t.base_stats["stewardship"] = 2f;
		this.add(new GeneAsset
		{
			id = "stewardship_3"
		});
		this.t.base_stats["stewardship"] = 3f;
		this.add(new GeneAsset
		{
			id = "intelligence_1",
			is_simple = true
		});
		this.t.base_stats["intelligence"] = 1f;
		this.add(new GeneAsset
		{
			id = "intelligence_2"
		});
		this.t.base_stats["intelligence"] = 2f;
		this.add(new GeneAsset
		{
			id = "intelligence_3"
		});
		this.t.base_stats["intelligence"] = 3f;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0006F9D4 File Offset: 0x0006DBD4
	private void addSpecial()
	{
		this.add(new GeneAsset
		{
			id = "empty",
			path_icon = "ui/Icons/iconEmptyLocus",
			is_empty = true,
			show_in_knowledge_window = false,
			needs_to_be_explored = false,
			show_genepool_nucleobases = false,
			can_drop_and_grab = false
		});
		this.add(new GeneAsset
		{
			id = "temp_for_generation",
			path_icon = "ui/Icons/iconEmptyLocus",
			for_generation = true,
			is_empty = true,
			show_in_knowledge_window = false,
			needs_to_be_explored = false,
			show_genepool_nucleobases = false,
			can_drop_and_grab = false,
			has_description_1 = false,
			has_description_2 = false,
			has_localized_id = false
		});
		this.add(new GeneAsset
		{
			id = "bad",
			is_stat_gene = false,
			is_bad = true,
			is_simple = true,
			needs_to_be_explored = true,
			synergy_sides_always = true,
			show_genepool_nucleobases = false
		});
		this.add(new GeneAsset
		{
			id = "bonus_male",
			is_stat_gene = false,
			show_genepool_nucleobases = false,
			synergy_sides_always = true,
			is_bonus_male = true
		});
		this.add(new GeneAsset
		{
			id = "bonus_female",
			is_stat_gene = false,
			show_genepool_nucleobases = false,
			synergy_sides_always = true,
			is_bonus_female = true
		});
		this.add(new GeneAsset
		{
			id = "mutagenic",
			is_stat_gene = false,
			synergy_sides_always = true,
			show_genepool_nucleobases = false
		});
		this.t.base_stats_meta["mutation"] = 1f;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0006FB74 File Offset: 0x0006DD74
	public override GeneAsset add(GeneAsset pAsset)
	{
		GeneAsset geneAsset = base.add(pAsset);
		geneAsset.has_description_1 = false;
		geneAsset.has_description_2 = false;
		return geneAsset;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0006FB8B File Offset: 0x0006DD8B
	public GeneAsset getRandomSimpleGene()
	{
		return this._gene_assets_simple.GetRandom<GeneAsset>();
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0006FB98 File Offset: 0x0006DD98
	public GeneAsset getRandomGeneForMutation()
	{
		return this._gene_assets_mutations.GetRandom<GeneAsset>();
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0006FBA8 File Offset: 0x0006DDA8
	public void regenerateBasicDNACodesWithLifeSeed(long pLifeSeed)
	{
		foreach (GeneAsset tAsset in this.list)
		{
			if (tAsset.show_genepool_nucleobases)
			{
				long tSeed = pLifeSeed + (long)tAsset.getIndexID();
				tAsset.generateDNA(tSeed);
			}
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0006FC10 File Offset: 0x0006DE10
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (GeneAsset tAsset in this.list)
		{
			if (tAsset.is_simple)
			{
				this._gene_assets_simple.Add(tAsset);
			}
		}
		foreach (GeneAsset tAsset2 in this.list)
		{
			if (!tAsset2.is_empty)
			{
				this._gene_assets_mutations.Add(tAsset2);
			}
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0006FCC8 File Offset: 0x0006DEC8
	public List<GeneAsset> getGenesWithStat(string pStatID)
	{
		if (!this._cached_stat_genes_dictionary.ContainsKey(pStatID))
		{
			List<GeneAsset> tResult = this.filterGenes(pStatID);
			this._cached_stat_genes_dictionary[pStatID] = tResult;
		}
		return this._cached_stat_genes_dictionary[pStatID];
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0006FD04 File Offset: 0x0006DF04
	private List<GeneAsset> filterGenes(string pStatID)
	{
		List<GeneAsset> tResult = new List<GeneAsset>();
		foreach (GeneAsset tGeneAsset in AssetManager.gene_library.list)
		{
			if (!tGeneAsset.is_empty && (tGeneAsset.base_stats.hasStat(pStatID) || tGeneAsset.base_stats_meta.hasStat(pStatID)))
			{
				tResult.Add(tGeneAsset);
			}
		}
		return tResult;
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0006FD88 File Offset: 0x0006DF88
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/genes/";
		}
	}

	// Token: 0x04000838 RID: 2104
	[NonSerialized]
	private Dictionary<string, List<GeneAsset>> _cached_stat_genes_dictionary = new Dictionary<string, List<GeneAsset>>();

	// Token: 0x04000839 RID: 2105
	[NonSerialized]
	private List<GeneAsset> _gene_assets_simple = new List<GeneAsset>();

	// Token: 0x0400083A RID: 2106
	[NonSerialized]
	private List<GeneAsset> _gene_assets_mutations = new List<GeneAsset>();

	// Token: 0x0400083B RID: 2107
	public static GeneAsset gene_for_generation;
}
