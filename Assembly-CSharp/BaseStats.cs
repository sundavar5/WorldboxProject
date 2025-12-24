using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000024 RID: 36
[Serializable]
public class BaseStats : ICloneable
{
	// Token: 0x060001D7 RID: 471 RVA: 0x0000F080 File Offset: 0x0000D280
	private void set(string pID, float pAmount)
	{
		BaseStatAsset tAsset = AssetManager.base_stats_library.get(pID);
		if (tAsset.ignore)
		{
			return;
		}
		if (pAmount == 0f && !tAsset.normalize)
		{
			BaseStatsContainer tRemoveStatsContainer;
			if (this._stats_dict.TryGetValue(pID, out tRemoveStatsContainer))
			{
				if (tAsset.multiplier)
				{
					List<BaseStatsContainer> multipliers_list = this._multipliers_list;
					if (multipliers_list != null)
					{
						multipliers_list.Remove(tRemoveStatsContainer);
					}
				}
				this._stats_list.Remove(tRemoveStatsContainer);
				this._stats_dict.Remove(pID);
			}
			return;
		}
		BaseStatsContainer tStatsContainer;
		if (!this._stats_dict.TryGetValue(pID, out tStatsContainer))
		{
			tStatsContainer = new BaseStatsContainer();
			tStatsContainer.value = pAmount;
			tStatsContainer.id = pID;
			this._stats_dict[pID] = tStatsContainer;
			this._stats_list.Add(tStatsContainer);
			if (tAsset.multiplier)
			{
				if (this._multipliers_list == null)
				{
					this._multipliers_list = new List<BaseStatsContainer>();
				}
				this._multipliers_list.Add(tStatsContainer);
			}
			return;
		}
		this._stats_dict[pID].value = pAmount;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000F16F File Offset: 0x0000D36F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public List<BaseStatsContainer> getList()
	{
		return this._stats_list;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000F177 File Offset: 0x0000D377
	public void checkStatName(string pID)
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (AssetManager.base_stats_library.get(pID) == null)
		{
			Debug.LogError("base_stats_library.get() - no asset with id: " + pID);
			Debug.LogError("Only call stats with S.id, never with pure strings to avoid typos");
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
	public float get(string pID)
	{
		BaseStatsContainer tContainer;
		if (this._stats_dict.TryGetValue(pID, out tContainer))
		{
			return tContainer.value;
		}
		return 0f;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000F1D1 File Offset: 0x0000D3D1
	public bool hasStat(string pID)
	{
		return this._stats_dict.ContainsKey(pID);
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
	public BaseStatsContainer getContainer(string pID)
	{
		BaseStatsContainer tContainer = null;
		this._stats_dict.TryGetValue(pID, out tContainer);
		return tContainer;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000F200 File Offset: 0x0000D400
	internal void mergeStats(BaseStats pStats, float pMultiplier = 1f)
	{
		for (int i = 0; i < pStats._stats_list.Count; i++)
		{
			BaseStatsContainer tStat = pStats._stats_list[i];
			string id = tStat.id;
			this[id] += tStat.value * pMultiplier;
		}
		if (pStats._tags != null)
		{
			if (this._tags == null)
			{
				this._tags = new HashSet<string>(pStats._tags);
				return;
			}
			this._tags.UnionWith(pStats._tags);
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000F284 File Offset: 0x0000D484
	public void checkMultipliers()
	{
		if (this._multipliers_list == null)
		{
			return;
		}
		for (int i = 0; i < this._multipliers_list.Count; i++)
		{
			BaseStatsContainer tMultiplierContainer = this._multipliers_list[i];
			BaseStatAsset tAssetMultiplier = tMultiplierContainer.asset;
			BaseStatsContainer tContainerToMultiply = this.getContainer(tAssetMultiplier.main_stat_to_multiply);
			if (tContainerToMultiply != null)
			{
				tContainerToMultiply.value += tContainerToMultiply.value * tMultiplierContainer.value;
			}
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000F2EE File Offset: 0x0000D4EE
	public bool hasTag(string pTag)
	{
		HashSet<string> tags = this._tags;
		return tags != null && tags.Contains(pTag);
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000F302 File Offset: 0x0000D502
	public bool hasTags(string[] pTags)
	{
		HashSet<string> tags = this._tags;
		return tags != null && tags.Overlaps(pTags);
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000F318 File Offset: 0x0000D518
	public void normalize()
	{
		for (int i = 0; i < this._stats_list.Count; i++)
		{
			this._stats_list[i].normalize();
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000F34C File Offset: 0x0000D54C
	internal void clear()
	{
		List<BaseStatsContainer> multipliers_list = this._multipliers_list;
		if (multipliers_list != null)
		{
			multipliers_list.Clear();
		}
		this._stats_list.Clear();
		this._stats_dict.Clear();
		HashSet<string> tags = this._tags;
		if (tags == null)
		{
			return;
		}
		tags.Clear();
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000F385 File Offset: 0x0000D585
	public void reset()
	{
		this.clear();
	}

	// Token: 0x1700000D RID: 13
	public float this[string pKey]
	{
		get
		{
			return this.get(pKey);
		}
		set
		{
			this.set(pKey, value);
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void addTag(string pTag)
	{
		if (this._tags == null)
		{
			this._tags = new HashSet<string>();
		}
		this._tags.Add(pTag);
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000F3C2 File Offset: 0x0000D5C2
	public bool hasTags()
	{
		HashSet<string> tags = this._tags;
		return tags != null && tags.Count > 0;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
	public bool hasStats()
	{
		List<BaseStatsContainer> stats_list = this._stats_list;
		return stats_list != null && stats_list.Count > 0;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000F3EE File Offset: 0x0000D5EE
	public bool ShouldSerialize_tags()
	{
		return this.hasTags();
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000F3F6 File Offset: 0x0000D5F6
	public bool ShouldSerialize_stats_list()
	{
		return this.hasStats();
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000F3FE File Offset: 0x0000D5FE
	public void addCombatAction(string pCombatAction)
	{
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000F400 File Offset: 0x0000D600
	public object Clone()
	{
		BaseStats baseStats = new BaseStats();
		baseStats.mergeStats(this, 1f);
		return baseStats;
	}

	// Token: 0x0400015A RID: 346
	[JsonProperty]
	private List<BaseStatsContainer> _stats_list = new List<BaseStatsContainer>();

	// Token: 0x0400015B RID: 347
	private Dictionary<string, BaseStatsContainer> _stats_dict = new Dictionary<string, BaseStatsContainer>();

	// Token: 0x0400015C RID: 348
	private List<BaseStatsContainer> _multipliers_list;

	// Token: 0x0400015D RID: 349
	[JsonProperty]
	private HashSet<string> _tags;
}
