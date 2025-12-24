using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000100 RID: 256
[Serializable]
public class GeneAsset : BaseTrait<GeneAsset>
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060007CB RID: 1995 RVA: 0x0006EDDC File Offset: 0x0006CFDC
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_genes;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060007CC RID: 1996 RVA: 0x0006EDEF File Offset: 0x0006CFEF
	public override string typed_id
	{
		get
		{
			return "gene";
		}
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0006EDF6 File Offset: 0x0006CFF6
	public GeneAsset()
	{
		this.group_id = "genes";
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0006EE1E File Offset: 0x0006D01E
	public override BaseCategoryAsset getGroup()
	{
		return null;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0006EE21 File Offset: 0x0006D021
	public string getSequence()
	{
		if (this.is_bad)
		{
			return this.getHarmfulSequence();
		}
		if (!this.show_genepool_nucleobases)
		{
			return this.getLockedSequence();
		}
		if (base.isAvailable())
		{
			return this.getColoredSequence();
		}
		return this.getLockedSequence();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0006EE56 File Offset: 0x0006D056
	private string getHarmfulSequence()
	{
		return InsultStringGenerator.getDNASequenceBad();
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0006EE5D File Offset: 0x0006D05D
	public string getColoredSequence()
	{
		if (string.IsNullOrEmpty(this._cached_sequence))
		{
			this._cached_sequence = NucleobaseHelper.getColoredSequence(this._genetic_code);
		}
		return this._cached_sequence;
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0006EE83 File Offset: 0x0006D083
	public string getLockedSequence()
	{
		return "??? ??? ??? ??? ??? ???";
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0006EE8C File Offset: 0x0006D08C
	public BaseStats getHalfStats()
	{
		BaseStats tStats = this._cached_half_stats;
		if (tStats == null)
		{
			tStats = new BaseStats();
			this._cached_half_stats = tStats;
			foreach (BaseStatsContainer tStatsContainer in this.base_stats.getList().ToArray())
			{
				float tVar;
				if (Mathf.Approximately(Mathf.Floor(tStatsContainer.value), tStatsContainer.value))
				{
					tVar = Mathf.Floor(tStatsContainer.value * 0.5f);
				}
				else
				{
					tVar = tStatsContainer.value * 0.5f;
				}
				tStats[tStatsContainer.id] = tVar;
			}
			tStats.normalize();
		}
		return tStats;
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0006EF24 File Offset: 0x0006D124
	public BaseStats getHalfStatsMeta()
	{
		BaseStats tStats = this._cached_half_stats_meta;
		if (tStats == null)
		{
			tStats = new BaseStats();
			this._cached_half_stats_meta = tStats;
			foreach (BaseStatsContainer tStatsContainer in this.base_stats_meta.getList().ToArray())
			{
				float tVar;
				if (Mathf.Approximately(Mathf.Floor(tStatsContainer.value), tStatsContainer.value))
				{
					tVar = Mathf.Floor(tStatsContainer.value * 0.5f);
				}
				else
				{
					tVar = tStatsContainer.value * 0.5f;
				}
				tStats[tStatsContainer.id] = tVar;
			}
		}
		tStats.normalize();
		return tStats;
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0006EFBC File Offset: 0x0006D1BC
	public void generateDNA(long pSeed)
	{
		this._genetic_code = this.generateRandomCodonString(pSeed, 15);
		this.genetic_code_left = this._genetic_code[0];
		string genetic_code = this._genetic_code;
		this.genetic_code_right = genetic_code[genetic_code.Length - 1];
		this.genetic_code_up = this._genetic_code[8];
		this.genetic_code_down = this._genetic_code[10];
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0006F028 File Offset: 0x0006D228
	private string generateRandomCodonString(long pSeed, int pLength)
	{
		Random tRandom = new Random((int)pSeed);
		string tResult = "";
		for (int i = 0; i < pLength; i++)
		{
			tResult += "ACGT"[tRandom.Next("ACGT".Length)].ToString();
			if ((i + 1) % 3 == 0 && i + 1 < pLength)
			{
				tResult += " ";
			}
		}
		return tResult;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0006F092 File Offset: 0x0006D292
	protected override bool isDebugUnlockedAll()
	{
		return DebugConfig.isOn(DebugOption.UnlockAllGenes);
	}

	// Token: 0x0400081F RID: 2079
	private const string CHARS_FOR_CODONS = "ACGT";

	// Token: 0x04000820 RID: 2080
	public bool is_stat_gene = true;

	// Token: 0x04000821 RID: 2081
	public bool can_drop_and_grab = true;

	// Token: 0x04000822 RID: 2082
	public bool is_empty;

	// Token: 0x04000823 RID: 2083
	public bool for_generation;

	// Token: 0x04000824 RID: 2084
	public bool is_bad;

	// Token: 0x04000825 RID: 2085
	public bool is_simple;

	// Token: 0x04000826 RID: 2086
	public bool is_bonus_male;

	// Token: 0x04000827 RID: 2087
	public bool is_bonus_female;

	// Token: 0x04000828 RID: 2088
	public bool show_genepool_nucleobases = true;

	// Token: 0x04000829 RID: 2089
	public bool synergy_sides_always;

	// Token: 0x0400082A RID: 2090
	private string _genetic_code;

	// Token: 0x0400082B RID: 2091
	[NonSerialized]
	public char genetic_code_right;

	// Token: 0x0400082C RID: 2092
	[NonSerialized]
	public char genetic_code_left;

	// Token: 0x0400082D RID: 2093
	[NonSerialized]
	public char genetic_code_up;

	// Token: 0x0400082E RID: 2094
	[NonSerialized]
	public char genetic_code_down;

	// Token: 0x0400082F RID: 2095
	private string _cached_sequence;

	// Token: 0x04000830 RID: 2096
	private string _cached_sequence_locked;

	// Token: 0x04000831 RID: 2097
	private BaseStats _cached_half_stats;

	// Token: 0x04000832 RID: 2098
	private BaseStats _cached_half_stats_meta;
}
