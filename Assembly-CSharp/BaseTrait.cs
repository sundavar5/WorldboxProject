using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

// Token: 0x0200018D RID: 397
[Serializable]
public class BaseTrait<TTrait> : BaseAugmentationAsset, IDescription2Asset, IDescriptionAsset, ILocalizedAsset where TTrait : BaseTrait<TTrait>
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000AACC0 File Offset: 0x000A8EC0
	[JsonIgnore]
	public virtual string typed_id
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000AACD2 File Offset: 0x000A8ED2
	public bool hasPlotAsset()
	{
		return !string.IsNullOrEmpty(this.plot_id);
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000BBE RID: 3006 RVA: 0x000AACE2 File Offset: 0x000A8EE2
	[JsonIgnore]
	public PlotAsset plot_asset
	{
		get
		{
			return AssetManager.plots_library.get(this.plot_id);
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x000AACF4 File Offset: 0x000A8EF4
	public string getId()
	{
		return this.id;
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x000AACFC File Offset: 0x000A8EFC
	public WorldAction getSpecialEffect()
	{
		return this.action_special_effect;
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x000AAD04 File Offset: 0x000A8F04
	public float getSpecialEffectInterval()
	{
		return this.special_effect_interval;
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x000AAD0C File Offset: 0x000A8F0C
	public void addOpposite(string pID)
	{
		if (this.opposite_list == null)
		{
			this.opposite_list = new List<string>();
		}
		this.opposite_list.Add(pID);
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x000AAD2D File Offset: 0x000A8F2D
	public void addOpposites(IEnumerable<string> pListIDS)
	{
		if (this.opposite_list == null)
		{
			this.opposite_list = new List<string>(pListIDS);
			return;
		}
		this.opposite_list.AddRange(pListIDS);
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x000AAD50 File Offset: 0x000A8F50
	public void removeOpposite(string pID)
	{
		this.opposite_list.Remove(pID);
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x000AAD5F File Offset: 0x000A8F5F
	public override string getLocaleID()
	{
		if (!this.has_localized_id)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(this.special_locale_id))
		{
			return this.special_locale_id;
		}
		return this.typed_id + "_" + this.id;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x000AAD95 File Offset: 0x000A8F95
	public string getDescriptionID()
	{
		if (!this.has_description_1)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(this.special_locale_description))
		{
			return this.special_locale_description;
		}
		return this.typed_id + "_" + this.id + "_info";
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x000AADD0 File Offset: 0x000A8FD0
	public string getDescriptionID2()
	{
		if (!this.has_description_2)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(this.special_locale_description_2))
		{
			return this.special_locale_description_2;
		}
		return this.typed_id + "_" + this.id + "_info_2";
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x000AAE0B File Offset: 0x000A900B
	public string getTranslatedName()
	{
		return LocalizedTextManager.getText(this.getLocaleID(), null, false);
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x000AAE1C File Offset: 0x000A901C
	public string getTranslatedDescription()
	{
		string tID = this.getDescriptionID();
		if (LocalizedTextManager.stringExists(tID))
		{
			return LocalizedTextManager.getText(tID, null, false);
		}
		return null;
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x000AAE44 File Offset: 0x000A9044
	public string getTranslatedDescription2()
	{
		string tID = this.getDescriptionID2();
		if (LocalizedTextManager.stringExists(tID))
		{
			return LocalizedTextManager.getText(tID, null, false);
		}
		return null;
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x000AAE6A File Offset: 0x000A906A
	protected override bool isDebugUnlockedAll()
	{
		return DebugConfig.isOn(DebugOption.UnlockAllTraits);
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000AAE73 File Offset: 0x000A9073
	protected virtual IEnumerable<ITraitsOwner<TTrait>> getRelatedMetaList()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x000AAE88 File Offset: 0x000A9088
	private ListPool<ITraitsOwner<TTrait>> getOwnersList()
	{
		ListPool<ITraitsOwner<TTrait>> tResult = new ListPool<ITraitsOwner<TTrait>>();
		TTrait tThisTrait = (TTrait)((object)this);
		foreach (ITraitsOwner<TTrait> tObject in this.getRelatedMetaList())
		{
			if (tObject.hasTrait(tThisTrait))
			{
				tResult.Add(tObject);
			}
		}
		return tResult;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000AAEEC File Offset: 0x000A90EC
	[return: TupleElementNames(new string[]
	{
		"pTotal",
		"pCivs",
		"pMobs"
	})]
	private ValueTuple<int, int, int> countTraitOwnersByCategories()
	{
		int fTotal = 0;
		int fCivs = 0;
		int fMobs = 0;
		TTrait tThisTrait = (TTrait)((object)this);
		foreach (ITraitsOwner<TTrait> tObject in this.getRelatedMetaList())
		{
			if (tObject.hasTrait(tThisTrait))
			{
				fTotal++;
				if (this.isSapient(tObject))
				{
					fCivs++;
				}
				else
				{
					fMobs++;
				}
			}
		}
		return new ValueTuple<int, int, int>(fTotal, fCivs, fMobs);
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x000AAF70 File Offset: 0x000A9170
	public virtual string getCountRows()
	{
		string result;
		using (ListPool<ITraitsOwner<TTrait>> tOwnersList = this.getOwnersList())
		{
			int tPopulation = 0;
			foreach (ITraitsOwner<TTrait> ptr in tOwnersList)
			{
				ITraitsOwner<TTrait> tObject = ptr;
				tPopulation += ((IMetaObject)tObject).countUnits();
			}
			using (StringBuilderPool tBuilder = new StringBuilderPool())
			{
				tBuilder.Append(LocalizedTextManager.getText(this.typed_id + "_amount_text", null, false).Replace("$amount$", this.getColoredNumber(tOwnersList.Count.ToString())));
				tBuilder.AppendLine();
				tBuilder.Append(LocalizedTextManager.getText("population_amount", null, false).Replace("$amount$", this.getColoredNumber(tPopulation.ToString())));
				result = tBuilder.ToString();
			}
		}
		return result;
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x000AB080 File Offset: 0x000A9280
	private string getColoredNumber(string pText)
	{
		return Toolbox.coloredString(pText, "#F3961F");
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x000AB090 File Offset: 0x000A9290
	protected string getCountRowsByCategories()
	{
		ValueTuple<int, int, int> tCounts = this.countTraitOwnersByCategories();
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			tBuilder.Append(LocalizedTextManager.getText("trait_owners_civs", null, false).Replace("$amount$", this.getColoredNumber(tCounts.Item2.ToString())));
			tBuilder.AppendLine();
			tBuilder.Append(LocalizedTextManager.getText("trait_owners_mobs", null, false).Replace("$amount$", this.getColoredNumber(tCounts.Item3.ToString())));
			result = tBuilder.ToString();
		}
		return result;
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x000AB134 File Offset: 0x000A9334
	protected virtual bool isSapient(ITraitsOwner<TTrait> pObject)
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x000AB146 File Offset: 0x000A9346
	public bool ShouldSerializebase_stats_meta()
	{
		return !this.base_stats_meta.isEmpty();
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x000AB158 File Offset: 0x000A9358
	public void setTraitInfoToGrinMark()
	{
		this.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_grin_mark";
		this.special_locale_id = "subspecies_trait_grin_mark";
		this.special_locale_description = "subspecies_trait_grin_mark_info";
		this.special_locale_description_2 = "subspecies_trait_grin_mark_info_2";
		this.show_for_unlockables_ui = false;
		this.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(this.action_on_augmentation_add, new WorldActionTrait(WorldBehaviourActions.addForGrinReaper));
		this.action_on_augmentation_load = (WorldActionTrait)Delegate.Combine(this.action_on_augmentation_load, new WorldActionTrait(WorldBehaviourActions.addForGrinReaper));
		this.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(this.action_on_augmentation_remove, new WorldActionTrait(WorldBehaviourActions.removeUsedForGrinReaper));
		this.action_on_object_remove = (WorldActionTrait)Delegate.Combine(this.action_on_object_remove, new WorldActionTrait(WorldBehaviourActions.removeUsedForGrinReaper));
	}

	// Token: 0x04000B42 RID: 2882
	public float value;

	// Token: 0x04000B43 RID: 2883
	public WorldAction action_death;

	// Token: 0x04000B44 RID: 2884
	public WorldAction action_growth;

	// Token: 0x04000B45 RID: 2885
	public WorldAction action_birth;

	// Token: 0x04000B46 RID: 2886
	public GetHitAction action_get_hit;

	// Token: 0x04000B47 RID: 2887
	[DefaultValue(true)]
	public bool spawn_random_trait_allowed = true;

	// Token: 0x04000B48 RID: 2888
	[DefaultValue(5)]
	public int spawn_random_rate = 5;

	// Token: 0x04000B49 RID: 2889
	public bool special_icon_logic;

	// Token: 0x04000B4A RID: 2890
	public BaseStats base_stats_meta;

	// Token: 0x04000B4B RID: 2891
	public List<string> opposite_list;

	// Token: 0x04000B4C RID: 2892
	[NonSerialized]
	public HashSet<TTrait> opposite_traits;

	// Token: 0x04000B4D RID: 2893
	public string[] traits_to_remove_ids;

	// Token: 0x04000B4E RID: 2894
	[NonSerialized]
	public TTrait[] traits_to_remove;

	// Token: 0x04000B4F RID: 2895
	[DefaultValue("")]
	public string special_locale_description = string.Empty;

	// Token: 0x04000B50 RID: 2896
	[DefaultValue("")]
	public string special_locale_description_2 = string.Empty;

	// Token: 0x04000B51 RID: 2897
	[DefaultValue(true)]
	public bool has_localized_id = true;

	// Token: 0x04000B52 RID: 2898
	[DefaultValue(true)]
	public bool has_description_1 = true;

	// Token: 0x04000B53 RID: 2899
	[DefaultValue(true)]
	public bool has_description_2 = true;

	// Token: 0x04000B54 RID: 2900
	[DefaultValue(Rarity.R1_Rare)]
	public Rarity rarity = Rarity.R1_Rare;

	// Token: 0x04000B55 RID: 2901
	[DefaultValue(true)]
	public bool can_be_in_book = true;

	// Token: 0x04000B56 RID: 2902
	[DefaultValue("")]
	public string plot_id = string.Empty;

	// Token: 0x04000B57 RID: 2903
	[JsonIgnore]
	public List<ActorAsset> default_for_actor_assets;
}
