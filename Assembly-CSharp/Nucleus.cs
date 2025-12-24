using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class Nucleus
{
	// Token: 0x060007F5 RID: 2037 RVA: 0x000700F8 File Offset: 0x0006E2F8
	public Nucleus()
	{
		this._base_stats_all[0] = this._merged_base_stats;
		this._base_stats_all[1] = this._merged_base_stats_meta;
		this._base_stats_all[2] = this._merged_base_stats_male;
		this._base_stats_all[3] = this._merged_base_stats_female;
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00070198 File Offset: 0x0006E398
	public BaseStats base_stats_male
	{
		get
		{
			return this._merged_base_stats_male;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000701A0 File Offset: 0x0006E3A0
	public BaseStats base_stats_female
	{
		get
		{
			return this._merged_base_stats_female;
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x000701A8 File Offset: 0x0006E3A8
	public void createFrom(ActorAsset pActorAsset)
	{
		this.reset();
		Chromosome tLastChromosome = null;
		using (ListPool<string> tShuffledGeneStats = this.generateGenesFromBaseStats(pActorAsset.genome_parts))
		{
			tShuffledGeneStats.Shuffle<string>();
			bool tWorldLawGeneSpaghetti = WorldLawLibrary.world_law_gene_spaghetti.isEnabled();
			for (int i = 0; i < tShuffledGeneStats.Count; i++)
			{
				string tGeneID = tShuffledGeneStats[i];
				if (tWorldLawGeneSpaghetti && Randy.randomChance(0.777f))
				{
					string tRandomGene = AssetManager.gene_library.getRandomNormalGene();
					if (!string.IsNullOrEmpty(tRandomGene))
					{
						tGeneID = tRandomGene;
					}
				}
				GeneAsset tAsset = AssetManager.gene_library.get(tGeneID);
				if (tAsset == null)
				{
					Debug.LogError("GeneAsset not found: " + tGeneID);
				}
				else if (tLastChromosome != null && tLastChromosome.canAddGene(tAsset))
				{
					tLastChromosome.addGene(tAsset);
				}
				else
				{
					List<string> chromosomes_first = pActorAsset.chromosomes_first;
					ChromosomeTypeAsset tChromosomeType;
					if (chromosomes_first != null && chromosomes_first.Count > 0 && this.chromosomes.Count == 0)
					{
						tChromosomeType = AssetManager.chromosome_type_library.get(pActorAsset.chromosomes_first.GetRandom<string>());
					}
					else
					{
						tChromosomeType = AssetManager.chromosome_type_library.list.GetRandom<ChromosomeTypeAsset>();
					}
					Chromosome tNewChromosome = new Chromosome(tChromosomeType.id, true);
					this.addChromosome(tNewChromosome);
					tLastChromosome = tNewChromosome;
					tLastChromosome.addGene(tAsset);
				}
			}
			this.fillAllEmptyLoci();
			this.shuffleGenesBetweenChromosomes();
			this.setDirty();
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0007030C File Offset: 0x0006E50C
	public void fillAllEmptyLoci()
	{
		foreach (Chromosome chromosome in this.chromosomes)
		{
			chromosome.fillEmptyLoci();
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0007035C File Offset: 0x0006E55C
	private void shuffleGenesBetweenChromosomes()
	{
		using (ListPool<GeneAsset> tGenesForShuffle = new ListPool<GeneAsset>())
		{
			for (int iChromosome = 0; iChromosome < this.chromosomes.Count; iChromosome++)
			{
				Chromosome tChromosome = this.chromosomes[iChromosome];
				for (int iGene = 0; iGene < tChromosome.genes.Count; iGene++)
				{
					GeneAsset tGeneAsset = tChromosome.genes[iGene];
					if (!tGeneAsset.is_empty)
					{
						tChromosome.genes[iGene] = GeneLibrary.gene_for_generation;
						tGenesForShuffle.Add(tGeneAsset);
					}
				}
			}
			tGenesForShuffle.Shuffle<GeneAsset>();
			for (int iChromosome2 = 0; iChromosome2 < this.chromosomes.Count; iChromosome2++)
			{
				Chromosome tChromosome2 = this.chromosomes[iChromosome2];
				for (int iGene2 = 0; iGene2 < tChromosome2.genes.Count; iGene2++)
				{
					if (tChromosome2.genes[iGene2].for_generation)
					{
						tChromosome2.genes[iGene2] = tGenesForShuffle.Pop<GeneAsset>();
					}
				}
			}
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00070468 File Offset: 0x0006E668
	private bool isStatGene(string pID)
	{
		return !(pID == "bonus_male") && !(pID == "bonus_female") && !(pID == "bonus_sex_random") && !(pID == "bad");
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x000704A4 File Offset: 0x0006E6A4
	private ListPool<string> generateGenesFromBaseStats(HashSet<GenomePart> pGeneTemplateStats)
	{
		ListPool<string> tResultList = new ListPool<string>();
		Dictionary<string, float> tRemainingValues = new Dictionary<string, float>();
		this.fillRemainingValues(pGeneTemplateStats, tRemainingValues, tResultList);
		bool tCanStillAdd = true;
		int tBreakError = 300;
		while (tCanStillAdd)
		{
			tCanStillAdd = false;
			if (tBreakError-- < 0)
			{
				Debug.LogError("generateGenesFromBaseStats infinite loop");
				break;
			}
			using (HashSet<GenomePart>.Enumerator enumerator = pGeneTemplateStats.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GenomePart tGenomePart = enumerator.Current;
					if (this.isStatGene(tGenomePart.id))
					{
						List<GeneAsset> tListGenes = AssetManager.gene_library.getGenesWithStat(tGenomePart.id);
						if (Randy.randomChance(0.8f))
						{
							tListGenes.Sort((GeneAsset pG1, GeneAsset pG2) => Nucleus.sortByStatValue(pG1, pG2, tGenomePart.id));
						}
						else
						{
							tListGenes.Shuffle<GeneAsset>();
						}
						for (int iG = 0; iG < tListGenes.Count; iG++)
						{
							GeneAsset tAsset = tListGenes[iG];
							bool tCanAddNewGene = true;
							foreach (BaseStatsContainer tStat in tAsset.base_stats.getList())
							{
								if (!tRemainingValues.ContainsKey(tStat.id) || tRemainingValues[tStat.id] < tStat.value)
								{
									tCanAddNewGene = false;
									break;
								}
							}
							foreach (BaseStatsContainer tStat2 in tAsset.base_stats_meta.getList())
							{
								if (!tRemainingValues.ContainsKey(tStat2.id) || tRemainingValues[tStat2.id] < tStat2.value)
								{
									tCanAddNewGene = false;
									break;
								}
							}
							if (tCanAddNewGene)
							{
								foreach (BaseStatsContainer tStat3 in tAsset.base_stats.getList())
								{
									Dictionary<string, float> dictionary = tRemainingValues;
									string id = tStat3.id;
									dictionary[id] -= tStat3.value;
								}
								foreach (BaseStatsContainer tStat4 in tAsset.base_stats_meta.getList())
								{
									Dictionary<string, float> dictionary = tRemainingValues;
									string id = tStat4.id;
									dictionary[id] -= tStat4.value;
								}
								tResultList.Add(tAsset.id);
								tCanStillAdd = true;
								break;
							}
						}
						if (tCanStillAdd)
						{
							break;
						}
					}
				}
			}
		}
		return tResultList;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000707C8 File Offset: 0x0006E9C8
	private void fillRemainingValues(HashSet<GenomePart> pGeneTemplateStats, Dictionary<string, float> pDictRemainingValues, ListPool<string> pResultGenes)
	{
		foreach (GenomePart tGenomePart in pGeneTemplateStats)
		{
			string tID = tGenomePart.id;
			if (this.isStatGene(tID))
			{
				pDictRemainingValues.Add(tID, tGenomePart.value);
			}
			else
			{
				this.addSpecialGenes(tID, (int)tGenomePart.value, pResultGenes);
			}
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00070840 File Offset: 0x0006EA40
	private void addSpecialGenes(string pSpecialGeneID, int pAmount, ListPool<string> pResultGenes)
	{
		for (int i = 0; i < pAmount; i++)
		{
			if (pSpecialGeneID == "bonus_sex_random")
			{
				if (Randy.randomBool())
				{
					pResultGenes.Add("bonus_male");
				}
				else
				{
					pResultGenes.Add("bonus_female");
				}
			}
			else
			{
				pResultGenes.Add(pSpecialGeneID);
			}
		}
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00070890 File Offset: 0x0006EA90
	private static int sortByStatValue(GeneAsset pA, GeneAsset pB, string pStatID)
	{
		if (!pA.base_stats.hasStat(pStatID) && !pB.base_stats.hasStat(pStatID))
		{
			return 0;
		}
		if (!pA.base_stats.hasStat(pStatID))
		{
			return 1;
		}
		if (!pB.base_stats.hasStat(pStatID))
		{
			return -1;
		}
		return pB.base_stats[pStatID].CompareTo(pA.base_stats[pStatID]);
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x000708FC File Offset: 0x0006EAFC
	public List<ChromosomeData> getListForSave()
	{
		List<ChromosomeData> tList = new List<ChromosomeData>();
		foreach (Chromosome chromosome in this.chromosomes)
		{
			ChromosomeData tGeneList = chromosome.getDataForSave();
			tList.Add(tGeneList);
		}
		return tList;
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0007095C File Offset: 0x0006EB5C
	public BaseStats getStats()
	{
		if (this._dirty)
		{
			this.recalculate();
		}
		return this._merged_base_stats;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00070972 File Offset: 0x0006EB72
	public BaseStats getStatsMeta()
	{
		if (this._dirty)
		{
			this.recalculate();
		}
		return this._merged_base_stats_meta;
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00070988 File Offset: 0x0006EB88
	private void recalculate()
	{
		if (!this._dirty)
		{
			return;
		}
		this._dirty = false;
		BaseStats tBaseStats = this._merged_base_stats;
		BaseStats tBaseStatsMeta = this._merged_base_stats_meta;
		BaseStats tBaseStatsMale = this._merged_base_stats_male;
		BaseStats tBaseStatsFemale = this._merged_base_stats_female;
		this.clearAllBaseStats();
		foreach (Chromosome tChromosome in this.chromosomes)
		{
			tChromosome.recalculate();
			tBaseStats.mergeStats(tChromosome.getStats(), 1f);
			tBaseStatsMeta.mergeStats(tChromosome.getStatsMeta(), 1f);
			tBaseStatsMale.mergeStats(tChromosome.getStatsMale(), 1f);
			tBaseStatsFemale.mergeStats(tChromosome.getStatsFemale(), 1f);
		}
		this.pot_possible_attributes.Clear();
		this.pot_possible_attributes.AddTimes((int)tBaseStats["intelligence"], "intelligence");
		this.pot_possible_attributes.AddTimes((int)tBaseStats["warfare"], "warfare");
		this.pot_possible_attributes.AddTimes((int)tBaseStats["stewardship"], "stewardship");
		this.pot_possible_attributes.AddTimes((int)tBaseStats["diplomacy"], "diplomacy");
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00070AD4 File Offset: 0x0006ECD4
	public void setDirty()
	{
		this._dirty = true;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00070ADD File Offset: 0x0006ECDD
	public void addChromosome(Chromosome pChromosome)
	{
		this.chromosomes.Add(pChromosome);
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00070AEB File Offset: 0x0006ECEB
	public void reset()
	{
		this.setDirty();
		this.chromosomes.Clear();
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00070B00 File Offset: 0x0006ED00
	private void clearAllBaseStats()
	{
		BaseStats[] base_stats_all = this._base_stats_all;
		for (int i = 0; i < base_stats_all.Length; i++)
		{
			base_stats_all[i].clear();
		}
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00070B2C File Offset: 0x0006ED2C
	public void cloneFrom(Nucleus pParentsSubspeciesNucleus)
	{
		this.chromosomes.Clear();
		foreach (Chromosome tParentChromosome in pParentsSubspeciesNucleus.chromosomes)
		{
			Chromosome tClonedChromosome = new Chromosome(tParentChromosome.chromosome_type, false);
			tClonedChromosome.cloneFrom(tParentChromosome);
			this.addChromosome(tClonedChromosome);
		}
		this.setDirty();
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00070BA4 File Offset: 0x0006EDA4
	public void doRandomGeneMutations(int pMutationAmount)
	{
		foreach (Chromosome tChromosome in this.chromosomes)
		{
			for (int i = 0; i < pMutationAmount; i++)
			{
				tChromosome.mutateRandomGene();
			}
		}
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00070C04 File Offset: 0x0006EE04
	public void unstableGenomeEvent()
	{
		foreach (Chromosome chromosome in this.chromosomes)
		{
			chromosome.shuffleGenes();
		}
		this.setDirty();
	}

	// Token: 0x04000859 RID: 2137
	public readonly List<Chromosome> chromosomes = new List<Chromosome>();

	// Token: 0x0400085A RID: 2138
	private readonly BaseStats _merged_base_stats_male = new BaseStats();

	// Token: 0x0400085B RID: 2139
	private readonly BaseStats _merged_base_stats_female = new BaseStats();

	// Token: 0x0400085C RID: 2140
	private readonly BaseStats _merged_base_stats = new BaseStats();

	// Token: 0x0400085D RID: 2141
	private readonly BaseStats _merged_base_stats_meta = new BaseStats();

	// Token: 0x0400085E RID: 2142
	private bool _dirty = true;

	// Token: 0x0400085F RID: 2143
	private readonly BaseStats[] _base_stats_all = new BaseStats[4];

	// Token: 0x04000860 RID: 2144
	public readonly List<string> pot_possible_attributes = new List<string>();
}
