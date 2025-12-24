using System;
using System.Collections.Generic;

// Token: 0x0200018F RID: 399
[Serializable]
public class BaseTraitLibrary<T> : BaseLibraryWithUnlockables<T> where T : BaseTrait<T>
{
	// Token: 0x06000BD7 RID: 3031 RVA: 0x000AB28D File Offset: 0x000A948D
	public override void post_init()
	{
		base.post_init();
		this.list.Sort((T pT1, T pT2) => StringComparer.Ordinal.Compare(pT2.id, pT1.id));
		this.autoSetRarity();
		this.checkIcons();
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x000AB2CC File Offset: 0x000A94CC
	protected virtual void autoSetRarity()
	{
		foreach (T tTrait in this.list)
		{
			if (tTrait.unlocked_with_achievement)
			{
				tTrait.rarity = Rarity.R3_Legendary;
			}
			else
			{
				bool flag = tTrait.action_death != null || tTrait.action_special_effect != null || tTrait.action_get_hit != null || tTrait.action_birth != null || tTrait.action_attack_target != null || tTrait.action_on_augmentation_add != null || tTrait.action_on_augmentation_remove != null || tTrait.action_on_augmentation_load != null;
				bool tHasDecisions = tTrait.decision_ids != null;
				bool tHasSpells = tTrait.spells_ids != null;
				bool tHasCombatActions = tTrait.combat_actions_ids != null;
				bool tHasTag = tTrait.base_stats.hasTags();
				bool tHasPlot = !string.IsNullOrEmpty(tTrait.plot_id);
				int tCount = 0;
				if (flag)
				{
					tCount++;
				}
				if (tHasDecisions)
				{
					tCount++;
				}
				if (tHasSpells)
				{
					tCount++;
				}
				if (tHasCombatActions)
				{
					tCount++;
				}
				if (tHasTag)
				{
					tCount++;
				}
				if (tHasPlot)
				{
					tCount++;
				}
				if (tCount > 0)
				{
					if (tCount == 1)
					{
						tTrait.rarity = Rarity.R1_Rare;
					}
					else
					{
						tTrait.rarity = Rarity.R2_Epic;
					}
					tTrait.needs_to_be_explored = true;
				}
				else if (tTrait.rarity == Rarity.R0_Normal)
				{
					tTrait.needs_to_be_explored = false;
				}
			}
		}
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x000AB490 File Offset: 0x000A9690
	public override void linkAssets()
	{
		base.linkAssets();
		this.fillOppositeHashsetsWithAssets();
		this.linkDecisions();
		this.linkCombatActions();
		this.linkSpells();
		this.linkActorAssets();
		foreach (T tTrait in this.list)
		{
			if (tTrait.spawn_random_trait_allowed)
			{
				this._pot_allowed_to_be_given_randomly.AddTimes(tTrait.spawn_random_rate, tTrait);
			}
		}
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x000AB524 File Offset: 0x000A9724
	private void linkCombatActions()
	{
		foreach (T t in this.list)
		{
			t.linkCombatActions();
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x000AB57C File Offset: 0x000A977C
	private void linkSpells()
	{
		foreach (T t in this.list)
		{
			t.linkSpells();
		}
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x000AB5D4 File Offset: 0x000A97D4
	private void linkDecisions()
	{
		foreach (T tAsset in this.list)
		{
			if (tAsset.decision_ids != null)
			{
				tAsset.decisions_assets = new DecisionAsset[tAsset.decision_ids.Count];
				for (int i = 0; i < tAsset.decision_ids.Count; i++)
				{
					string tDecisionID = tAsset.decision_ids[i];
					DecisionAsset tDecisionAsset = AssetManager.decisions_library.get(tDecisionID);
					tAsset.decisions_assets[i] = tDecisionAsset;
				}
			}
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x000AB69C File Offset: 0x000A989C
	private void linkActorAssets()
	{
		foreach (ActorAsset tActorAsset in AssetManager.actor_library.list)
		{
			List<string> tTraits = this.getDefaultTraitsForMeta(tActorAsset);
			if (tTraits != null)
			{
				foreach (string tTraitId in tTraits)
				{
					T tTrait = this.get(tTraitId);
					if (tTrait.default_for_actor_assets == null)
					{
						tTrait.default_for_actor_assets = new List<ActorAsset>();
					}
					tTrait.default_for_actor_assets.Add(tActorAsset);
				}
			}
		}
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x000AB76C File Offset: 0x000A996C
	public override void editorDiagnostic()
	{
		this.checkOppositeErrors();
		foreach (T tTrait in this.list)
		{
			if (string.IsNullOrEmpty(tTrait.group_id))
			{
				BaseAssetLibrary.logAssetError("Group id not assigned", tTrait.id);
			}
			if (!tTrait.special_icon_logic && SpriteTextureLoader.getSprite(tTrait.path_icon) == null)
			{
				BaseAssetLibrary.logAssetError("Missing icon file", tTrait.path_icon);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x000AB828 File Offset: 0x000A9A28
	public override void editorDiagnosticLocales()
	{
		foreach (T tTrait in this.list)
		{
			this.checkLocale(tTrait, tTrait.getLocaleID());
			this.checkLocale(tTrait, tTrait.getDescriptionID());
			this.checkLocale(tTrait, tTrait.getDescriptionID2());
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x000AB8BC File Offset: 0x000A9ABC
	private void checkOppositeErrors()
	{
		foreach (T tMainTrait in this.list)
		{
			HashSet<T> tMainOppositeList = tMainTrait.opposite_traits;
			if (tMainOppositeList != null)
			{
				foreach (T tOppositeTrait in tMainOppositeList)
				{
					HashSet<T> tOppositeTraitList = tOppositeTrait.opposite_traits;
					if (tOppositeTraitList == null || !tOppositeTraitList.Contains(tMainTrait))
					{
						base.logErrorOpposites(tMainTrait.id, tOppositeTrait.id);
					}
				}
			}
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x000AB98C File Offset: 0x000A9B8C
	private void fillOppositeHashsetsWithAssets()
	{
		foreach (T tMainTrait in this.list)
		{
			if (tMainTrait.opposite_list != null && tMainTrait.opposite_list.Count > 0)
			{
				tMainTrait.opposite_traits = new HashSet<T>(tMainTrait.opposite_list.Count);
				foreach (string tID in tMainTrait.opposite_list)
				{
					T tOppositeTrait = this.get(tID);
					tMainTrait.opposite_traits.Add(tOppositeTrait);
				}
			}
		}
		foreach (T tMainTrait2 in this.list)
		{
			if (tMainTrait2.traits_to_remove_ids != null)
			{
				int tCount = tMainTrait2.traits_to_remove_ids.Length;
				tMainTrait2.traits_to_remove = new T[tCount];
				for (int i = 0; i < tCount; i++)
				{
					string tID2 = tMainTrait2.traits_to_remove_ids[i];
					T tTraitToAdd = this.get(tID2);
					tMainTrait2.traits_to_remove[i] = tTraitToAdd;
				}
			}
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x000ABB30 File Offset: 0x000A9D30
	private void checkIcons()
	{
		foreach (T tTrait in this.list)
		{
			if (string.IsNullOrEmpty(tTrait.path_icon))
			{
				tTrait.path_icon = this.icon_path + tTrait.getLocaleID();
			}
		}
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x000ABBB0 File Offset: 0x000A9DB0
	public override T add(T pAsset)
	{
		T tNewAsset = base.add(pAsset);
		if (tNewAsset.base_stats == null)
		{
			tNewAsset.base_stats = new BaseStats();
		}
		if (tNewAsset.base_stats_meta == null)
		{
			tNewAsset.base_stats_meta = new BaseStats();
		}
		return tNewAsset;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000ABC00 File Offset: 0x000A9E00
	public string addToGameplayReportShort(string pWhatFor)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhatFor + "\n";
		foreach (T tAsset in this.list)
		{
			string tName = tAsset.id;
			if (!(tName == "Phenotype"))
			{
				string tDescription = tAsset.getTranslatedDescription();
				string tLineInfo = "\n" + tName;
				if (!string.IsNullOrEmpty(tDescription))
				{
					tLineInfo = tLineInfo + ": " + tDescription;
				}
				tResult += tLineInfo;
			}
		}
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x000ABCC4 File Offset: 0x000A9EC4
	public string addToGameplayReport(string pWhatFor)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhatFor + "\n";
		foreach (T tAsset in this.list)
		{
			string tName = tAsset.getTranslatedName();
			if (!(tName == "Phenotype"))
			{
				string tDescription = tAsset.getTranslatedDescription();
				string tDescription2 = tAsset.getTranslatedDescription2();
				string tLineInfo = "\n" + tName;
				tLineInfo += "\n";
				if (!string.IsNullOrEmpty(tDescription))
				{
					tLineInfo = tLineInfo + "1: " + tDescription;
				}
				if (!string.IsNullOrEmpty(tDescription2))
				{
					tLineInfo = tLineInfo + "\n2: " + tDescription2;
				}
				tResult += tLineInfo;
			}
		}
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x000ABDC0 File Offset: 0x000A9FC0
	public T getRandomSpawnTrait()
	{
		return this._pot_allowed_to_be_given_randomly.GetRandom<T>();
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000ABDCD File Offset: 0x000A9FCD
	protected virtual string icon_path
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x000ABDDF File Offset: 0x000A9FDF
	protected virtual List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000B58 RID: 2904
	protected List<T> _pot_allowed_to_be_given_randomly = new List<T>();
}
