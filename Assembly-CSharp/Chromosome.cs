using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class Chromosome
{
	// Token: 0x0600076D RID: 1901 RVA: 0x0006D1E4 File Offset: 0x0006B3E4
	public Chromosome(string pType, bool pNew)
	{
		this.chromosome_type = pType;
		if (pNew)
		{
			int tSize = this.getAsset().amount_loci;
			GeneAsset tEmptyGene = AssetManager.gene_library.get("empty");
			for (int i = 0; i < tSize; i++)
			{
				this.genes.Add(tEmptyGene);
			}
			this.generateAmplifiers(pType);
		}
		this._base_stats_all[0] = this._merged_base_stats;
		this._base_stats_all[1] = this._merged_base_stats_meta;
		this._base_stats_all[2] = this._merged_base_stats_male;
		this._base_stats_all[3] = this._merged_base_stats_female;
		this._columns = this.getAsset().amount_loci / 6;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0006D2F0 File Offset: 0x0006B4F0
	public bool isLocusAmplifier(int pX, int pY)
	{
		int tIndex = this.getIndexFrom(pX, pY);
		return this.isLocusAmplifier(tIndex);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0006D30D File Offset: 0x0006B50D
	public bool isLocusAmplifier(int pLocusIndex)
	{
		return this._loci_amplifiers.Contains(pLocusIndex);
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0006D31B File Offset: 0x0006B51B
	public bool isVoidLocus(int pLocusIndex)
	{
		return this._loci_empty.Contains(pLocusIndex);
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0006D32C File Offset: 0x0006B52C
	public bool isSpecialLocusAt(int pX, int pY)
	{
		int tIndex = this.getIndexFrom(pX, pY);
		return this.isSpecialLocus(tIndex);
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0006D34C File Offset: 0x0006B54C
	public bool isVoidLocusAt(int pX, int pY)
	{
		int tIndex = this.getIndexFrom(pX, pY);
		return this.isVoidLocus(tIndex);
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0006D36C File Offset: 0x0006B56C
	public bool isAllSidesVoidLocus(int pX, int pY)
	{
		int tTotalSides = this.countBounds(pX, pY);
		int tCount = 0;
		if (this.isVoidLocusAt(pX - 1, pY))
		{
			tCount++;
		}
		if (this.isVoidLocusAt(pX + 1, pY))
		{
			tCount++;
		}
		if (this.isVoidLocusAt(pX, pY + 1))
		{
			tCount++;
		}
		if (this.isVoidLocusAt(pX, pY - 1))
		{
			tCount++;
		}
		return tCount == tTotalSides;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0006D3C8 File Offset: 0x0006B5C8
	private bool isAmplifierLocusAt(int pX, int pY)
	{
		int tIndex = this.getIndexFrom(pX, pY);
		return this.isLocusAmplifier(tIndex);
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0006D3E5 File Offset: 0x0006B5E5
	private bool isForcedSynergyAt(int pX, int pY)
	{
		return this.isAmplifierLocusAt(pX, pY) || this.getGeneAt(pX, pY).synergy_sides_always;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0006D408 File Offset: 0x0006B608
	public bool isForcedSynergyLeft(int pX, int pY)
	{
		if (this.hasBoundLeft(pX, pY))
		{
			return false;
		}
		ValueTuple<int, int> directionOffset = this.getDirectionOffset(GeneDirection.Left);
		int tX = directionOffset.Item1;
		int tY = directionOffset.Item2;
		return this.isForcedSynergyAt(pX + tX, pY + tY);
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0006D444 File Offset: 0x0006B644
	public bool isForcedSynergyRight(int pX, int pY)
	{
		if (this.hasBoundRight(pX, pY))
		{
			return false;
		}
		ValueTuple<int, int> directionOffset = this.getDirectionOffset(GeneDirection.Right);
		int tX = directionOffset.Item1;
		int tY = directionOffset.Item2;
		return this.isForcedSynergyAt(pX + tX, pY + tY);
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0006D480 File Offset: 0x0006B680
	public bool isForcedSynergyUp(int pX, int pY)
	{
		if (this.hasBoundUp(pX, pY))
		{
			return false;
		}
		ValueTuple<int, int> directionOffset = this.getDirectionOffset(GeneDirection.Up);
		int tX = directionOffset.Item1;
		int tY = directionOffset.Item2;
		return this.isForcedSynergyAt(pX + tX, pY + tY);
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0006D4BC File Offset: 0x0006B6BC
	public bool isForcedSynergyDown(int pX, int pY)
	{
		if (this.hasBoundDown(pX, pY))
		{
			return false;
		}
		ValueTuple<int, int> directionOffset = this.getDirectionOffset(GeneDirection.Down);
		int tX = directionOffset.Item1;
		int tY = directionOffset.Item2;
		return this.isForcedSynergyAt(pX + tX, pY + tY);
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0006D4F5 File Offset: 0x0006B6F5
	public LocusType getLocusType(int pLocusIndex)
	{
		if (this.isLocusAmplifier(pLocusIndex))
		{
			return LocusType.Amplifier;
		}
		if (this.isVoidLocus(pLocusIndex))
		{
			return LocusType.Empty;
		}
		return LocusType.Standard;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0006D510 File Offset: 0x0006B710
	public void fillStatsForTooltip(LocusElement pLocus, BaseStats pStatsContainer)
	{
		int tLocusIndex = pLocus.locus_index;
		if (this.isVoidLocus(tLocusIndex))
		{
			return;
		}
		GeneAsset tGeneAsset = pLocus.getGeneAsset();
		if (tGeneAsset.is_bonus_male)
		{
			this.combineBonusesForSides(tLocusIndex, pStatsContainer);
		}
		else if (tGeneAsset.is_bonus_female)
		{
			this.combineBonusesForSides(tLocusIndex, pStatsContainer);
		}
		else
		{
			this.getBonusesFromGene(pLocus.locus_index, pStatsContainer, null, true);
		}
		pStatsContainer.normalize();
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0006D570 File Offset: 0x0006B770
	private void generateAmplifiers(string pType)
	{
		ChromosomeTypeAsset tAsset = AssetManager.chromosome_type_library.get(pType);
		using (ListPool<int> tList = new ListPool<int>())
		{
			for (int i = 0; i < tAsset.amount_loci; i++)
			{
				tList.Add(i);
			}
			tList.Shuffle<int>();
			int tAmountOfLociAmplifiers = Randy.randomInt(tAsset.amount_loci_min_amplifier, tAsset.amount_loci_max_amplifier);
			int tAmountOfLociEmpty = Randy.randomInt(tAsset.amount_loci_min_empty, tAsset.amount_loci_max_empty);
			for (int j = 0; j < tAmountOfLociAmplifiers; j++)
			{
				this._loci_amplifiers.Add(tList.Pop<int>());
			}
			for (int k = 0; k < tAmountOfLociEmpty; k++)
			{
				this._loci_empty.Add(tList.Pop<int>());
			}
		}
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0006D634 File Offset: 0x0006B834
	public bool canAddGene(GeneAsset pAsset)
	{
		return this.countEmpty() != 0;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0006D641 File Offset: 0x0006B841
	public void setGene(GeneAsset pAsset, int pIndex)
	{
		this.genes[pIndex] = pAsset;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0006D650 File Offset: 0x0006B850
	public GeneAsset getGene(int pIndex)
	{
		return this.genes[pIndex];
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0006D65E File Offset: 0x0006B85E
	public ChromosomeTypeAsset getAsset()
	{
		return AssetManager.chromosome_type_library.get(this.chromosome_type);
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0006D670 File Offset: 0x0006B870
	public void load(ChromosomeData pData)
	{
		this.chromosome_type = pData.chromosome_type;
		foreach (string tGeneID in pData.loci)
		{
			GeneAsset tGeneAsset = AssetManager.gene_library.get(tGeneID);
			if (tGeneAsset != null)
			{
				this.genes.Add(tGeneAsset);
			}
		}
		this._loci_amplifiers.AddRange(pData.super_loci);
		this._loci_empty.AddRange(pData.void_loci);
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0006D708 File Offset: 0x0006B908
	public ChromosomeData getDataForSave()
	{
		ChromosomeData tData = new ChromosomeData();
		foreach (GeneAsset tAsset in this.genes)
		{
			tData.loci.Add(tAsset.id);
		}
		tData.super_loci.AddRange(this._loci_amplifiers);
		tData.void_loci.AddRange(this._loci_empty);
		tData.chromosome_type = this.chromosome_type;
		return tData;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0006D79C File Offset: 0x0006B99C
	public void addGene(GeneAsset pGeneAsset)
	{
		for (int i = 0; i < this.genes.Count; i++)
		{
			if (this.genes[i].is_empty && this.canAddToLocus(i))
			{
				this.genes[i] = pGeneAsset;
				break;
			}
		}
		this.setDirty();
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0006D7F0 File Offset: 0x0006B9F0
	public bool isSpecialLocus(int pIndex)
	{
		return this._loci_amplifiers.Contains(pIndex) || this._loci_empty.Contains(pIndex);
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0006D80E File Offset: 0x0006BA0E
	public bool canAddToLocus(int pIndex)
	{
		return !this._loci_amplifiers.Contains(pIndex) && !this._loci_empty.Contains(pIndex);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0006D834 File Offset: 0x0006BA34
	public int countNonEmpty()
	{
		int tResult = 0;
		for (int i = 0; i < this.genes.Count; i++)
		{
			if (!this.genes[i].is_empty && this.canAddToLocus(i))
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0006D87C File Offset: 0x0006BA7C
	public int countEmpty()
	{
		int tResult = 0;
		for (int i = 0; i < this.genes.Count; i++)
		{
			if (this.genes[i].is_empty && this.canAddToLocus(i))
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0006D8C2 File Offset: 0x0006BAC2
	public BaseStats getStats()
	{
		if (this._dirty)
		{
			this.recalculate();
		}
		return this._merged_base_stats;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0006D8D8 File Offset: 0x0006BAD8
	public BaseStats getStatsMeta()
	{
		if (this._dirty)
		{
			this.recalculate();
		}
		return this._merged_base_stats_meta;
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0006D8EE File Offset: 0x0006BAEE
	public BaseStats getStatsMale()
	{
		if (this._dirty)
		{
			this.recalculate();
		}
		return this._merged_base_stats_male;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0006D904 File Offset: 0x0006BB04
	public BaseStats getStatsFemale()
	{
		if (this._dirty)
		{
			this.recalculate();
		}
		return this._merged_base_stats_female;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0006D91A File Offset: 0x0006BB1A
	public void setDirty()
	{
		this._dirty = true;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0006D924 File Offset: 0x0006BB24
	public void recalculate()
	{
		if (!this._dirty)
		{
			return;
		}
		this._dirty = false;
		this.clearAllBaseStats();
		BaseStats tBaseStats = this._merged_base_stats;
		BaseStats tBaseStatsMeta = this._merged_base_stats_meta;
		BaseStats tBaseStatsMale = this._merged_base_stats_male;
		BaseStats tBaseStatsFemale = this._merged_base_stats_female;
		for (int i = 0; i < this.genes.Count; i++)
		{
			if (!this.isVoidLocus(i))
			{
				this.getBonusesFromGene(i, tBaseStats, tBaseStatsMeta, false);
			}
		}
		for (int j = 0; j < this.genes.Count; j++)
		{
			GeneAsset tGene = this.genes[j];
			if (!this.isVoidLocus(j))
			{
				if (tGene.is_bonus_male)
				{
					this.combineBonusesForSides(j, tBaseStatsMale);
				}
				if (tGene.is_bonus_female)
				{
					this.combineBonusesForSides(j, tBaseStatsFemale);
				}
			}
		}
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0006D9EC File Offset: 0x0006BBEC
	private void combineBonusesForSides(int pLocusIndex, BaseStats pBaseStatsMain)
	{
		ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(pLocusIndex);
		int tX = xyfromIndex.Item1;
		int tY = xyfromIndex.Item2;
		bool flag = this.isNextToBad(pLocusIndex);
		this.getBonusesFromGene(tX, tY + 1, pBaseStatsMain, null, false);
		this.getBonusesFromGene(tX, tY - 1, pBaseStatsMain, null, false);
		this.getBonusesFromGene(tX - 1, tY, pBaseStatsMain, null, false);
		this.getBonusesFromGene(tX + 1, tY, pBaseStatsMain, null, false);
		if (flag)
		{
			foreach (BaseStatsContainer tStatsContainer in pBaseStatsMain.getList().ToArray())
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
				pBaseStatsMain[tStatsContainer.id] = tVar;
			}
			pBaseStatsMain.normalize();
		}
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0006DAC0 File Offset: 0x0006BCC0
	private void getBonusesFromGene(int pX, int pY, BaseStats pBaseStatsMain, BaseStats pBaseStatsMeta = null, bool pCombineMeta = false)
	{
		if (this.getGeneAt(pX, pY) == null)
		{
			return;
		}
		int tLocusIndex = this.getIndexFrom(pX, pY);
		this.getBonusesFromGene(tLocusIndex, pBaseStatsMain, pBaseStatsMeta, pCombineMeta);
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0006DAF0 File Offset: 0x0006BCF0
	private void getBonusesFromGene(int pLocusIndex, BaseStats pBaseStatsMain, BaseStats pBaseStatsMeta = null, bool pCombineMeta = false)
	{
		GeneAsset tGene = this.genes[pLocusIndex];
		bool tSynergyBonus = this.hasFullSynergy(pLocusIndex);
		bool flag = this.isNextToBad(pLocusIndex);
		if (flag)
		{
			tSynergyBonus = false;
		}
		if (flag)
		{
			pBaseStatsMain.mergeStats(tGene.getHalfStats(), 1f);
			if (pCombineMeta)
			{
				pBaseStatsMain.mergeStats(tGene.getHalfStatsMeta(), 1f);
				return;
			}
			if (pBaseStatsMeta != null)
			{
				pBaseStatsMeta.mergeStats(tGene.getHalfStatsMeta(), 1f);
				return;
			}
		}
		else
		{
			pBaseStatsMain.mergeStats(tGene.base_stats, 1f);
			if (pCombineMeta)
			{
				pBaseStatsMain.mergeStats(tGene.base_stats_meta, 1f);
			}
			if (pBaseStatsMeta != null)
			{
				pBaseStatsMeta.mergeStats(tGene.base_stats_meta, 1f);
			}
			if (tSynergyBonus && !tGene.synergy_sides_always)
			{
				pBaseStatsMain.mergeStats(tGene.base_stats, 1f);
				if (pCombineMeta)
				{
					pBaseStatsMain.mergeStats(tGene.base_stats_meta, 1f);
					return;
				}
				if (pBaseStatsMeta != null)
				{
					pBaseStatsMeta.mergeStats(tGene.base_stats_meta, 1f);
				}
			}
		}
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0006DBE0 File Offset: 0x0006BDE0
	private void clearAllBaseStats()
	{
		BaseStats[] base_stats_all = this._base_stats_all;
		for (int i = 0; i < base_stats_all.Length; i++)
		{
			base_stats_all[i].clear();
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0006DC0C File Offset: 0x0006BE0C
	public bool hasFullSynergyAt(int pX, int pY)
	{
		int tCount = 0;
		if (this.isAllSidesVoidLocus(pX, pY))
		{
			return false;
		}
		if (this.isNextToBad(pX, pY))
		{
			return false;
		}
		if (this.isNextToBadAmplifier(pX, pY))
		{
			return false;
		}
		if (this.hasSynergyConnectionLeft(pX, pY))
		{
			tCount++;
		}
		if (this.hasSynergyConnectionRight(pX, pY))
		{
			tCount++;
		}
		if (this.hasSynergyConnectionUp(pX, pY))
		{
			tCount++;
		}
		if (this.hasSynergyConnectionDown(pX, pY))
		{
			tCount++;
		}
		int tTotalSides = 0;
		if (!this.hasBoundLeft(pX, pY))
		{
			tTotalSides++;
		}
		if (!this.hasBoundRight(pX, pY))
		{
			tTotalSides++;
		}
		if (!this.hasBoundUp(pX, pY))
		{
			tTotalSides++;
		}
		if (!this.hasBoundDown(pX, pY))
		{
			tTotalSides++;
		}
		return tCount == tTotalSides;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
	public bool hasFullSynergy(int pLocusIndex)
	{
		ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(pLocusIndex);
		int tX = xyfromIndex.Item1;
		int tY = xyfromIndex.Item2;
		return this.hasFullSynergyAt(tX, tY);
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0006DCE4 File Offset: 0x0006BEE4
	public bool hasAnySynergy(int pLocusIndex)
	{
		ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(pLocusIndex);
		int tX = xyfromIndex.Item1;
		int tY = xyfromIndex.Item2;
		return this.hasSynergyConnectionLeft(tX, tY) || this.hasSynergyConnectionRight(tX, tY) || this.hasSynergyConnectionUp(tX, tY) || this.hasSynergyConnectionDown(tX, tY);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0006DD38 File Offset: 0x0006BF38
	public string getSynergyTooltipText(int pLocusIndex)
	{
		ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(pLocusIndex);
		int tFromX = xyfromIndex.Item1;
		int tFromY = xyfromIndex.Item2;
		GeneAsset tGene = this.getGeneAt(tFromX, tFromY);
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			bool tBadHere = this.isBadAt(tFromX, tFromY);
			if (this.hasAnySynergy(pLocusIndex) && !tBadHere)
			{
				tBuilder.Append(Toolbox.coloredString(LocalizedTextManager.getText("sequence_synergy", null, false), "#FFFFAA"));
			}
			else
			{
				tBuilder.Append(LocalizedTextManager.getText("sequence_synergy", null, false));
			}
			tBuilder.Append("\n");
			bool flag = this.hasSynergyConnectionLeft(tFromX, tFromY);
			bool tHasRight = this.hasSynergyConnectionRight(tFromX, tFromY);
			bool flag2 = this.hasSynergyConnectionUp(tFromX, tFromY);
			bool tHasDown = this.hasSynergyConnectionDown(tFromX, tFromY);
			GeneAsset tSideAssetLeft = this.getGeneLeft(tFromX, tFromY);
			GeneAsset tSideAssetRight = this.getGeneRight(tFromX, tFromY);
			GeneAsset tSideAssetUp = this.getGeneUp(tFromX, tFromY);
			GeneAsset tSideAssetDown = this.getGeneDown(tFromX, tFromY);
			bool isForcedSynergyHere = this.isForcedSynergyAt(tFromX, tFromY);
			if (flag2)
			{
				if (tBadHere || this.isBadAt(tFromX, tFromY - 1) || this.hasAmplifierBad(tFromX, tFromY - 1))
				{
					tBuilder.Append(this.getBadConnectionString());
				}
				else if (isForcedSynergyHere)
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tSideAssetUp.genetic_code_down));
				}
				else
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tGene.genetic_code_up));
				}
			}
			else if (this.hasBoundUp(tFromX, tFromY) || this.isConnectionDeniedUp(tFromX, tFromY))
			{
				tBuilder.Append("<color=#444444>???????</color>");
			}
			else
			{
				tBuilder.Append(this.getNotConnectedText(tGene.genetic_code_up, World.world.getCurSessionTime()));
			}
			tBuilder.Append("\n");
			if (flag)
			{
				if (tBadHere || this.isBadAt(tFromX - 1, tFromY) || this.hasAmplifierBad(tFromX - 1, tFromY))
				{
					tBuilder.Append(this.getBadConnectionString());
				}
				else if (isForcedSynergyHere)
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tSideAssetLeft.genetic_code_right));
				}
				else
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tGene.genetic_code_left));
				}
			}
			else if (this.hasBoundLeft(tFromX, tFromY) || this.isConnectionDeniedLeft(tFromX, tFromY))
			{
				tBuilder.Append("<color=#444444>???????</color>");
			}
			else
			{
				tBuilder.Append(this.getNotConnectedText(tGene.genetic_code_left, World.world.getCurSessionTime()));
			}
			tBuilder.Append(" ... ");
			if (tHasRight)
			{
				if (tBadHere || this.isBadAt(tFromX + 1, tFromY) || this.hasAmplifierBad(tFromX + 1, tFromY))
				{
					tBuilder.Append(this.getBadConnectionString());
				}
				else if (isForcedSynergyHere)
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tSideAssetRight.genetic_code_left));
				}
				else
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tGene.genetic_code_right));
				}
			}
			else if (this.hasBoundRight(tFromX, tFromY) || this.isConnectionDeniedRight(tFromX, tFromY))
			{
				tBuilder.Append("<color=#444444>???????</color>");
			}
			else
			{
				tBuilder.Append(this.getNotConnectedText(tGene.genetic_code_right, World.world.getCurSessionTime()));
			}
			tBuilder.Append("\n");
			if (tHasDown)
			{
				if (tBadHere || this.isBadAt(tFromX, tFromY + 1) || this.hasAmplifierBad(tFromX, tFromY + 1))
				{
					tBuilder.Append(this.getBadConnectionString());
				}
				else if (isForcedSynergyHere)
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tSideAssetDown.genetic_code_up));
				}
				else
				{
					tBuilder.Append(NucleobaseHelper.getColoredNucleobaseFull(tGene.genetic_code_down));
				}
			}
			else if (this.hasBoundDown(tFromX, tFromY) || this.isConnectionDeniedDown(tFromX, tFromY))
			{
				tBuilder.Append("<color=#444444>???????</color>");
			}
			else
			{
				tBuilder.Append(this.getNotConnectedText(tGene.genetic_code_down, World.world.getCurSessionTime()));
			}
			tBuilder.Append("\n");
			tBuilder.Append("\n");
			result = tBuilder.ToString();
		}
		return result;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0006E0EC File Offset: 0x0006C2EC
	private string getBadConnectionString()
	{
		return InsultStringGenerator.getBadConnectionString();
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0006E0F4 File Offset: 0x0006C2F4
	private string getNotConnectedText(char pChar, double pTime)
	{
		string tFullNucleobase = NucleobaseHelper.getFullNucleobaseName(pChar);
		string tColor = NucleobaseHelper.getColorHex(pChar, true);
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			for (int i = 0; i < tFullNucleobase.Length; i++)
			{
				tBuilder.Append(tFullNucleobase[i]);
			}
			int tCharInt = (int)(pChar * 'd');
			int xPosition = (int)((pTime + (double)tCharInt) * 8.0 % (double)tFullNucleobase.Length);
			tBuilder[xPosition] = '?';
			result = Toolbox.coloredString(tBuilder.ToString(), tColor);
		}
		return result;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0006E190 File Offset: 0x0006C390
	private int getIndexFrom(int pX, int pY)
	{
		return pX + pY * 6;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0006E198 File Offset: 0x0006C398
	public ValueTuple<int, int> getXYFromIndex(int pIndex)
	{
		int item = pIndex % 6;
		int tY = pIndex / 6;
		return new ValueTuple<int, int>(item, tY);
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
	public Sprite getSpriteNormal()
	{
		Sprite[] tSpriteList = SpriteTextureLoader.getSpriteList("chromosomes/normal/", false);
		if (this._cached_sprite_index == -1)
		{
			this._cached_sprite_index = Randy.randomInt(0, tSpriteList.Length - 1);
		}
		this._cached_sprite = tSpriteList[this._cached_sprite_index];
		return this._cached_sprite;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0006E1FC File Offset: 0x0006C3FC
	public Sprite getSpriteGolden()
	{
		Sprite[] tSpriteList = SpriteTextureLoader.getSpriteList("chromosomes/golden/", false);
		if (this._cached_sprite_index == -1)
		{
			this._cached_sprite_index = Randy.randomInt(0, tSpriteList.Length - 1);
		}
		this._cached_sprite = tSpriteList[this._cached_sprite_index];
		return this._cached_sprite;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0006E243 File Offset: 0x0006C443
	public void cloneFrom(Chromosome pParentChromosome)
	{
		this.genes.AddRange(pParentChromosome.genes);
		this._loci_empty.AddRange(pParentChromosome._loci_empty);
		this._loci_amplifiers.AddRange(pParentChromosome._loci_amplifiers);
		this.setDirty();
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0006E280 File Offset: 0x0006C480
	public void mutateRandomGene()
	{
		using (ListPool<int> tList = new ListPool<int>())
		{
			for (int i = 0; i < this.genes.Count; i++)
			{
				if (!this.isSpecialLocus(i))
				{
					tList.Add(i);
				}
			}
			int tIndex = tList.GetRandom<int>();
			GeneAsset tNewGene = AssetManager.gene_library.getRandomGeneForMutation();
			this.setGene(tNewGene, tIndex);
			this.setDirty();
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0006E2F8 File Offset: 0x0006C4F8
	public bool hasGene(GeneAsset pAsset)
	{
		List<GeneAsset> list = this.genes;
		return list != null && list.Contains(pAsset);
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0006E30C File Offset: 0x0006C50C
	public GeneAsset getGeneAtDirectionFrom(int pFromX, int pFromY, GeneDirection pDirection)
	{
		ValueTuple<int, int> directionOffset = this.getDirectionOffset(pDirection);
		int tX = directionOffset.Item1;
		int tY = directionOffset.Item2;
		int tPositionX = pFromX + tX;
		int tPositionY = pFromY + tY;
		if (!this.isCoordinatesValid(tPositionX, tPositionY))
		{
			return null;
		}
		int tIndex = this.getIndexFrom(pFromX + tX, pFromY + tY);
		return this.genes[tIndex];
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0006E35C File Offset: 0x0006C55C
	public GeneAsset getGeneAt(int pFromX, int pFromY)
	{
		if (!this.isCoordinatesValid(pFromX, pFromY))
		{
			return null;
		}
		int tIndex = this.getIndexFrom(pFromX, pFromY);
		if (!this.isIndexValid(tIndex))
		{
			return null;
		}
		return this.genes[tIndex];
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0006E395 File Offset: 0x0006C595
	public GeneAsset getGeneLeft(int pFromX, int pFromY)
	{
		return this.getGeneAtDirectionFrom(pFromX, pFromY, GeneDirection.Left);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0006E3A0 File Offset: 0x0006C5A0
	public GeneAsset getGeneRight(int pFromX, int pFromY)
	{
		return this.getGeneAtDirectionFrom(pFromX, pFromY, GeneDirection.Right);
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0006E3AB File Offset: 0x0006C5AB
	public GeneAsset getGeneUp(int pFromX, int pFromY)
	{
		return this.getGeneAtDirectionFrom(pFromX, pFromY, GeneDirection.Up);
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0006E3B6 File Offset: 0x0006C5B6
	public GeneAsset getGeneDown(int pFromX, int pFromY)
	{
		return this.getGeneAtDirectionFrom(pFromX, pFromY, GeneDirection.Down);
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0006E3C1 File Offset: 0x0006C5C1
	private bool isIndexValid(int pIndex)
	{
		return pIndex >= 0 && pIndex < this.genes.Count;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0006E3DA File Offset: 0x0006C5DA
	private bool isCoordinatesValid(int pX, int pY)
	{
		return pX >= 0 && pY >= 0 && pX < 6 && pY < this._columns;
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0006E3FC File Offset: 0x0006C5FC
	public ValueTuple<int, int> getDirectionOffset(GeneDirection pDirection)
	{
		switch (pDirection)
		{
		case GeneDirection.Up:
			return Chromosome.DIRECTIONS[0];
		case GeneDirection.Down:
			return Chromosome.DIRECTIONS[1];
		case GeneDirection.Left:
			return Chromosome.DIRECTIONS[2];
		case GeneDirection.Right:
			return Chromosome.DIRECTIONS[3];
		default:
			return new ValueTuple<int, int>(0, 0);
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0006E458 File Offset: 0x0006C658
	public bool canBeConnectedTo(int pFromX, int pFromY, int pToX, int pToY)
	{
		GeneAsset tFromAsset = this.getGeneAt(pFromX, pFromY);
		GeneAsset tToAsset = this.getGeneAt(pToX, pToY);
		if (tFromAsset == null || tToAsset == null)
		{
			return false;
		}
		if (!tFromAsset.is_empty)
		{
			bool is_empty = tToAsset.is_empty;
		}
		return false;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0006E490 File Offset: 0x0006C690
	public int countBounds(int pX, int pY)
	{
		int tCount = 0;
		if (this.isCoordinatesValid(pX - 1, pY))
		{
			tCount++;
		}
		if (this.isCoordinatesValid(pX + 1, pY))
		{
			tCount++;
		}
		if (this.isCoordinatesValid(pX, pY - 1))
		{
			tCount++;
		}
		if (this.isCoordinatesValid(pX, pY + 1))
		{
			tCount++;
		}
		return tCount;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0006E4E0 File Offset: 0x0006C6E0
	public bool hasSynergyConnectionLeft(int pFromX, int pFromY)
	{
		return !this.hasBoundLeft(pFromX, pFromY) && this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Left);
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0006E4F7 File Offset: 0x0006C6F7
	public bool hasSynergyConnectionRight(int pFromX, int pFromY)
	{
		return !this.hasBoundRight(pFromX, pFromY) && this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Right);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0006E50E File Offset: 0x0006C70E
	public bool hasSynergyConnectionUp(int pFromX, int pFromY)
	{
		return !this.hasBoundUp(pFromX, pFromY) && this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Up);
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0006E525 File Offset: 0x0006C725
	public bool hasSynergyConnectionDown(int pFromX, int pFromY)
	{
		return !this.hasBoundDown(pFromX, pFromY) && this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Down);
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0006E53C File Offset: 0x0006C73C
	public bool isAllLociSynergy()
	{
		int tIndex = -1;
		foreach (GeneAsset tGene in this.genes)
		{
			tIndex++;
			if (!tGene.is_empty && !tGene.synergy_sides_always)
			{
				if (tGene.is_bad)
				{
					return false;
				}
				ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(tIndex);
				int tX = xyfromIndex.Item1;
				int tY = xyfromIndex.Item2;
				if (!this.hasAllSynergiesAt(tX, tY, false))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0006E5D8 File Offset: 0x0006C7D8
	public bool hasAllSynergiesAt(int pFromX, int pFromY, bool pCheckBounds = true)
	{
		if (this.isAllSidesVoidLocus(pFromX, pFromY))
		{
			return false;
		}
		bool flag = pCheckBounds ? this.hasSynergyConnectionLeft(pFromX, pFromY) : (this.hasBoundLeft(pFromX, pFromY) || this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Left));
		bool tRight = pCheckBounds ? this.hasSynergyConnectionRight(pFromX, pFromY) : (this.hasBoundRight(pFromX, pFromY) || this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Right));
		bool tUp = pCheckBounds ? this.hasSynergyConnectionUp(pFromX, pFromY) : (this.hasBoundUp(pFromX, pFromY) || this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Up));
		bool tDown = pCheckBounds ? this.hasSynergyConnectionDown(pFromX, pFromY) : (this.hasBoundDown(pFromX, pFromY) || this.hasSynergyConnection(pFromX, pFromY, GeneDirection.Down));
		return flag && tRight && tUp && tDown;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0006E688 File Offset: 0x0006C888
	public bool hasSynergyConnection(int pFromX, int pFromY, GeneDirection pDirection)
	{
		GeneAsset tAssetParent = this.getGeneAt(pFromX, pFromY);
		bool tLocusAmplifier = this.isAmplifierLocusAt(pFromX, pFromY);
		bool tAnyBad = false;
		if (tAssetParent.synergy_sides_always)
		{
			tLocusAmplifier = true;
		}
		if (tAssetParent.is_bad)
		{
			tAnyBad = true;
		}
		if (!tLocusAmplifier && tAssetParent.is_empty)
		{
			return false;
		}
		ValueTuple<int, int> directionOffset = this.getDirectionOffset(pDirection);
		int tX = directionOffset.Item1;
		int tY = directionOffset.Item2;
		GeneAsset tAssetSide = this.getGeneAt(pFromX + tX, pFromY + tY);
		bool tForcedSynergySide = this.isAmplifierLocusAt(pFromX + tX, pFromY + tY);
		if (tAssetSide != null && tAssetSide.synergy_sides_always)
		{
			tForcedSynergySide = true;
		}
		if (tAssetSide != null && tAssetSide.is_bad)
		{
			tAnyBad = true;
		}
		if (!tForcedSynergySide)
		{
			if (tAssetSide == null)
			{
				return false;
			}
			if (tAssetSide.is_empty)
			{
				return false;
			}
		}
		if (!tAnyBad && tLocusAmplifier && tForcedSynergySide)
		{
			return false;
		}
		switch (pDirection)
		{
		case GeneDirection.Up:
			if (tLocusAmplifier || tForcedSynergySide)
			{
				return true;
			}
			if (tAssetParent.genetic_code_up == tAssetSide.genetic_code_down)
			{
				return true;
			}
			break;
		case GeneDirection.Down:
			if (tLocusAmplifier || tForcedSynergySide)
			{
				return true;
			}
			if (tAssetParent.genetic_code_down == tAssetSide.genetic_code_up)
			{
				return true;
			}
			break;
		case GeneDirection.Left:
			if (tLocusAmplifier || tForcedSynergySide)
			{
				return true;
			}
			if (tAssetParent.genetic_code_left == tAssetSide.genetic_code_right)
			{
				return true;
			}
			break;
		case GeneDirection.Right:
			if (tLocusAmplifier || tForcedSynergySide)
			{
				return true;
			}
			if (tAssetParent.genetic_code_right == tAssetSide.genetic_code_left)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0006E7BB File Offset: 0x0006C9BB
	public bool isConnectionDeniedUp(int pFromX, int pFromY)
	{
		return this.hasBoundAt(pFromX, pFromY - 1) || (this.isForcedSynergyUp(pFromX, pFromY) && this.isForcedSynergyAt(pFromX, pFromY));
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0006E7E2 File Offset: 0x0006C9E2
	public bool isConnectionDeniedDown(int pFromX, int pFromY)
	{
		return this.hasBoundAt(pFromX, pFromY + 1) || (this.isForcedSynergyDown(pFromX, pFromY) && this.isForcedSynergyAt(pFromX, pFromY));
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0006E809 File Offset: 0x0006CA09
	public bool isConnectionDeniedLeft(int pFromX, int pFromY)
	{
		return this.hasBoundAt(pFromX - 1, pFromY) || (this.isForcedSynergyLeft(pFromX, pFromY) && this.isForcedSynergyAt(pFromX, pFromY));
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0006E830 File Offset: 0x0006CA30
	public bool isConnectionDeniedRight(int pFromX, int pFromY)
	{
		return this.hasBoundAt(pFromX + 1, pFromY) || (this.isForcedSynergyRight(pFromX, pFromY) && this.isForcedSynergyAt(pFromX, pFromY));
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0006E857 File Offset: 0x0006CA57
	public bool hasBoundAt(int pX, int pY)
	{
		return !this.isCoordinatesValid(pX, pY) || this.isVoidLocusAt(pX, pY);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0006E872 File Offset: 0x0006CA72
	public bool hasBoundLeft(int pX, int pY)
	{
		return this.hasBoundAt(pX - 1, pY);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0006E87E File Offset: 0x0006CA7E
	public bool hasBoundRight(int pX, int pY)
	{
		return this.hasBoundAt(pX + 1, pY);
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0006E88A File Offset: 0x0006CA8A
	public bool hasBoundUp(int pX, int pY)
	{
		return this.hasBoundAt(pX, pY - 1);
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0006E896 File Offset: 0x0006CA96
	public bool hasBoundDown(int pX, int pY)
	{
		return this.hasBoundAt(pX, pY + 1);
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0006E8A4 File Offset: 0x0006CAA4
	public void fillEmptyLoci()
	{
		for (int i = 0; i < this.genes.Count; i++)
		{
			GeneAsset tGeneAsset = this.genes[i];
			if (!this.isSpecialLocus(i) && tGeneAsset.is_empty)
			{
				GeneAsset tNewGene = AssetManager.gene_library.getRandomSimpleGene();
				this.setGene(tNewGene, i);
			}
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0006E8F8 File Offset: 0x0006CAF8
	public bool isNextToBad(int pLocusIndex)
	{
		ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(pLocusIndex);
		int tX = xyfromIndex.Item1;
		int tY = xyfromIndex.Item2;
		return this.isNextToBad(tX, tY);
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0006E924 File Offset: 0x0006CB24
	public bool isNextToBad(int pX, int pY)
	{
		foreach (ValueTuple<int, int> valueTuple in Chromosome.DIRECTIONS)
		{
			int tX = valueTuple.Item1;
			int tY = valueTuple.Item2;
			if (this.isBadAt(pX + tX, pY + tY))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0006E96C File Offset: 0x0006CB6C
	public bool hasGenesAround(int pIndex)
	{
		ValueTuple<int, int> xyfromIndex = this.getXYFromIndex(pIndex);
		int tX = xyfromIndex.Item1;
		int tY = xyfromIndex.Item2;
		return this.hasGenesAround(tX, tY);
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0006E998 File Offset: 0x0006CB98
	public bool hasGenesAround(int pX, int pY)
	{
		foreach (ValueTuple<int, int> valueTuple in Chromosome.DIRECTIONS)
		{
			int tX = valueTuple.Item1;
			int tY = valueTuple.Item2;
			int tCoordX = pX + tX;
			int tCoordY = pY + tY;
			if (this.isAmplifierLocusAt(tCoordX, tCoordY))
			{
				return true;
			}
			GeneAsset tGeneAsset = this.getGeneAt(tCoordX, tCoordY);
			if (tGeneAsset != null && !tGeneAsset.is_empty)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0006EA04 File Offset: 0x0006CC04
	public bool isNextToBadAmplifier(int pX, int pY)
	{
		foreach (ValueTuple<int, int> valueTuple in Chromosome.DIRECTIONS)
		{
			int tX = valueTuple.Item1;
			int tY = valueTuple.Item2;
			if (this.hasAmplifierBad(pX + tX, pY + tY))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0006EA4C File Offset: 0x0006CC4C
	public bool isBadAt(int pX, int pY)
	{
		if (this.isVoidLocusAt(pX, pY))
		{
			return false;
		}
		GeneAsset tGeneAsset = this.getGeneAt(pX, pY);
		return tGeneAsset != null && tGeneAsset.is_bad;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0006EA7E File Offset: 0x0006CC7E
	public bool hasAmplifierBad(int pX, int pY)
	{
		return this.isLocusAmplifier(pX, pY) && this.isNextToBad(pX, pY);
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0006EA9C File Offset: 0x0006CC9C
	public void shuffleGenes()
	{
		GeneAsset tPlaceholderGene = GeneLibrary.gene_for_generation;
		using (ListPool<GeneAsset> tShuffledGenes = new ListPool<GeneAsset>())
		{
			for (int i = 0; i < this.genes.Count; i++)
			{
				GeneAsset tGeneAsset = this.genes[i];
				if (!tGeneAsset.is_empty)
				{
					tShuffledGenes.Add(tGeneAsset);
					this.genes[i] = tPlaceholderGene;
				}
			}
			tShuffledGenes.Shuffle<GeneAsset>();
			int j = 0;
			while (j < this.genes.Count && tShuffledGenes.Count != 0)
			{
				if (this.genes[j].for_generation)
				{
					this.genes[j] = tShuffledGenes.Pop<GeneAsset>();
				}
				j++;
			}
			this.setDirty();
		}
	}

	// Token: 0x04000800 RID: 2048
	private const string IMAGE_PATH_NORMAL = "chromosomes/normal/";

	// Token: 0x04000801 RID: 2049
	private const string IMAGE_PATH_GOLD = "chromosomes/golden/";

	// Token: 0x04000802 RID: 2050
	private const string STRING_UNKOWN = "???????";

	// Token: 0x04000803 RID: 2051
	private const string COLOR_BOUND = "#444444";

	// Token: 0x04000804 RID: 2052
	private const string COLORED_UNKOWN_TEXT = "<color=#444444>???????</color>";

	// Token: 0x04000805 RID: 2053
	public readonly List<GeneAsset> genes = new List<GeneAsset>();

	// Token: 0x04000806 RID: 2054
	private readonly BaseStats _merged_base_stats_male = new BaseStats();

	// Token: 0x04000807 RID: 2055
	private readonly BaseStats _merged_base_stats_female = new BaseStats();

	// Token: 0x04000808 RID: 2056
	private readonly BaseStats _merged_base_stats = new BaseStats();

	// Token: 0x04000809 RID: 2057
	private readonly BaseStats _merged_base_stats_meta = new BaseStats();

	// Token: 0x0400080A RID: 2058
	private static readonly ValueTuple<int, int>[] DIRECTIONS = new ValueTuple<int, int>[]
	{
		new ValueTuple<int, int>(0, -1),
		new ValueTuple<int, int>(0, 1),
		new ValueTuple<int, int>(-1, 0),
		new ValueTuple<int, int>(1, 0)
	};

	// Token: 0x0400080B RID: 2059
	private bool _dirty = true;

	// Token: 0x0400080C RID: 2060
	private Sprite _cached_sprite;

	// Token: 0x0400080D RID: 2061
	private int _cached_sprite_index = -1;

	// Token: 0x0400080E RID: 2062
	public string chromosome_type;

	// Token: 0x0400080F RID: 2063
	private readonly List<int> _loci_amplifiers = new List<int>();

	// Token: 0x04000810 RID: 2064
	private readonly List<int> _loci_empty = new List<int>();

	// Token: 0x04000811 RID: 2065
	private readonly BaseStats[] _base_stats_all = new BaseStats[4];

	// Token: 0x04000812 RID: 2066
	private readonly int _columns;
}
