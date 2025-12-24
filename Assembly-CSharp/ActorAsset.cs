using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x020001FE RID: 510
[Serializable]
public class ActorAsset : BaseUnlockableAsset, IDescriptionAsset, ILocalizedAsset, IAnimationFrames
{
	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060011EA RID: 4586 RVA: 0x000D3010 File Offset: 0x000D1210
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_actors;
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060011EB RID: 4587 RVA: 0x000D3023 File Offset: 0x000D1223
	[JsonIgnore]
	public bool has_sound_idle
	{
		get
		{
			return this.sound_idle != null;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060011EC RID: 4588 RVA: 0x000D3031 File Offset: 0x000D1231
	[JsonIgnore]
	public bool has_sound_idle_loop
	{
		get
		{
			return this.sound_idle_loop != null;
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060011ED RID: 4589 RVA: 0x000D303F File Offset: 0x000D123F
	[JsonIgnore]
	public bool has_sound_spawn
	{
		get
		{
			return this.sound_spawn != null;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060011EE RID: 4590 RVA: 0x000D304D File Offset: 0x000D124D
	[JsonIgnore]
	public bool has_sound_death
	{
		get
		{
			return this.sound_death != null;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060011EF RID: 4591 RVA: 0x000D305B File Offset: 0x000D125B
	[JsonIgnore]
	public bool has_sound_attack
	{
		get
		{
			return this.sound_attack != null;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000D3069 File Offset: 0x000D1269
	[JsonIgnore]
	public bool has_sound_hit
	{
		get
		{
			return this.sound_hit != null;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000D3077 File Offset: 0x000D1277
	[JsonIgnore]
	public bool has_music_theme
	{
		get
		{
			return this.music_theme != null;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000D3085 File Offset: 0x000D1285
	[JsonIgnore]
	public bool has_music_theme_civ
	{
		get
		{
			return this.music_theme_civ != null;
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000D3093 File Offset: 0x000D1293
	public void addSubspeciesNamePrefix(string pName)
	{
		if (this.generated_subspecies_names_prefixes == null)
		{
			this.generated_subspecies_names_prefixes = new List<string>();
		}
		this.generated_subspecies_names_prefixes.Add(pName);
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000D30B4 File Offset: 0x000D12B4
	public bool hasDefaultEggForm()
	{
		List<string> list = this.default_subspecies_traits;
		return list != null && list.Contains("reproduction_strategy_oviparity");
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x000D30CC File Offset: 0x000D12CC
	public string getDefaultEggID()
	{
		string tId = "egg_shell_plain";
		foreach (string tTraitID in this.default_subspecies_traits)
		{
			SubspeciesTrait tTrait = AssetManager.subspecies_traits.get(tTraitID);
			if (tTrait.phenotype_egg)
			{
				tId = tTrait.id_egg;
				break;
			}
		}
		return tId;
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x060011F6 RID: 4598 RVA: 0x000D313C File Offset: 0x000D133C
	// (set) Token: 0x060011F7 RID: 4599 RVA: 0x000D3169 File Offset: 0x000D1369
	[JsonIgnore]
	public string debug_phenotype_colors
	{
		get
		{
			if (string.IsNullOrEmpty(this._debug_phenotype_color))
			{
				List<string> list = this.phenotypes_list;
				this._debug_phenotype_color = ((list != null) ? list.GetRandom<string>() : null);
			}
			return this._debug_phenotype_color;
		}
		set
		{
			this._debug_phenotype_color = value;
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000D3172 File Offset: 0x000D1372
	[DefaultValue("male_1")]
	public string skin_civ_default_male
	{
		get
		{
			return "male_1";
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000D3179 File Offset: 0x000D1379
	[DefaultValue("female_1")]
	public string skin_civ_default_female
	{
		get
		{
			return "female_1";
		}
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x000D3180 File Offset: 0x000D1380
	public void setZombie(bool pAnimal = true)
	{
		if (this.id != "zombie")
		{
			this.base_asset_id = "zombie";
		}
		this.needs_to_be_explored = false;
		this.use_phenotypes = false;
		this.name_locale = "Zombie";
		this.can_attack_brains = true;
		this.kingdom_id_wild = "undead";
		this.collective_term = "group_swarm";
		this.kingdom_id_civilization = string.Empty;
		this.architecture_id = string.Empty;
		this.build_order_template_id = string.Empty;
		this.take_items = false;
		this.follow_herd = false;
		this.can_be_killed_by_divine_light = true;
		this.unit_zombie = true;
		this.has_baby_form = false;
		this.has_advanced_textures = false;
		this.unlocked_with_achievement = false;
		this.achievement_id = null;
		this.addTrait("zombie");
		this.addTrait("stupid");
		this.can_turn_into_zombie = false;
		this.can_turn_into_mush = false;
		this.base_stats["lifespan"] = 0f;
		this.zombie_id_internal = string.Empty;
		this.job = Toolbox.a<string>(new string[]
		{
			"decision"
		});
		this.can_evolve_into_new_species = false;
		this.addDecision("attack_golden_brain");
		if (this.traits != null)
		{
			this.traits = this.traits.FindAll((string pTrait) => !AssetManager.traits.get(pTrait).remove_for_zombie_actor_asset);
		}
		if (this.default_subspecies_traits != null)
		{
			this.default_subspecies_traits = this.default_subspecies_traits.FindAll((string pTrait) => !AssetManager.subspecies_traits.get(pTrait).remove_for_zombies);
		}
		this.default_kingdom_traits = null;
		this.default_culture_traits = null;
		this.default_clan_traits = null;
		this.default_language_traits = null;
		this.default_religion_traits = null;
		this.addTraitGroupFilter("advanced_brain");
		this.addTraitGroupFilter("reproduction_strategy");
		this.addTraitGroupFilter("reproductive_methods");
		this.addTraitGroupFilter("eggs");
		this.addTraitGroupFilter("harmony");
		if (pAnimal)
		{
			this.generateFmodPaths("zombie_animal");
		}
		else
		{
			this.generateFmodPaths("zombie");
		}
		this.music_theme = "Units_Zombie";
		this.sound_hit = "event:/SFX/HIT/HitFlesh";
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x000D33A3 File Offset: 0x000D15A3
	public void setCanTurnIntoZombieAsset(string pZombieID, bool pAutoZombieAsset)
	{
		this.can_turn_into_zombie = true;
		this.zombie_auto_asset = pAutoZombieAsset;
		this.zombie_id_internal = pZombieID;
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x000D33BC File Offset: 0x000D15BC
	public string getZombieID()
	{
		string tResult;
		if (this.zombie_auto_asset)
		{
			tResult = this.zombie_id_internal + "_" + this.id;
		}
		else
		{
			tResult = this.zombie_id_internal;
		}
		return tResult;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x000D33F4 File Offset: 0x000D15F4
	public void cloneTaxonomyFromForSapiens(string pFrom)
	{
		ActorAsset tAsset = AssetManager.actor_library.get(pFrom);
		this.name_taxonomic_kingdom = tAsset.name_taxonomic_kingdom;
		this.name_taxonomic_phylum = tAsset.name_taxonomic_phylum;
		this.name_taxonomic_class = tAsset.name_taxonomic_class;
		this.name_taxonomic_order = tAsset.name_taxonomic_order;
		this.name_taxonomic_family = tAsset.name_taxonomic_family;
		this.name_taxonomic_genus = tAsset.name_taxonomic_genus;
		this.name_taxonomic_species = "sapiens";
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000D3460 File Offset: 0x000D1660
	public string getTaxonomyRank(string pType)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(pType);
		if (num <= 1997942759U)
		{
			if (num != 370076495U)
			{
				if (num != 1671819501U)
				{
					if (num == 1997942759U)
					{
						if (pType == "taxonomy_species")
						{
							return this.name_taxonomic_species;
						}
					}
				}
				else if (pType == "taxonomy_class")
				{
					return this.name_taxonomic_class;
				}
			}
			else if (pType == "taxonomy_genus")
			{
				return this.name_taxonomic_genus;
			}
		}
		else if (num <= 2219494011U)
		{
			if (num != 2178547709U)
			{
				if (num == 2219494011U)
				{
					if (pType == "taxonomy_family")
					{
						return this.name_taxonomic_family;
					}
				}
			}
			else if (pType == "taxonomy_order")
			{
				return this.name_taxonomic_order;
			}
		}
		else if (num != 2677668788U)
		{
			if (num == 3582153604U)
			{
				if (pType == "taxonomy_kingdom")
				{
					return this.name_taxonomic_kingdom;
				}
			}
		}
		else if (pType == "taxonomy_phylum")
		{
			return this.name_taxonomic_phylum;
		}
		return string.Empty;
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000D3571 File Offset: 0x000D1771
	public bool isTaxonomyRank(string pType, string pID)
	{
		return this.getTaxonomyRank(pType) == pID;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000D3580 File Offset: 0x000D1780
	public void setSocialStructure(string pTerm, int pLimit, bool pCreateOnSpawn = true, bool pFollowHerd = true, FamilyParentsMode pShowParents = FamilyParentsMode.Alpha)
	{
		this.collective_term = pTerm;
		this.family_limit = pLimit;
		this.create_family_at_spawn = pCreateOnSpawn;
		this.family_show_parents = pShowParents;
		this.follow_herd = pFollowHerd;
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x000D35A8 File Offset: 0x000D17A8
	public void generateFmodPaths(string pID)
	{
		string tBasePath = "event:/SFX/UNITS/" + pID;
		if (!this.has_sound_attack)
		{
			this.sound_attack = tBasePath + "/attack";
		}
		if (!this.has_sound_death)
		{
			this.sound_death = tBasePath + "/death";
		}
		if (!this.has_sound_idle)
		{
			this.sound_idle = tBasePath + "/idle";
		}
		if (!this.has_sound_spawn)
		{
			this.sound_spawn = tBasePath + "/spawn";
		}
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000D3628 File Offset: 0x000D1828
	public void clonePhenotype(string pFrom)
	{
		ActorAsset tAsset = AssetManager.actor_library.get(pFrom);
		if (tAsset.phenotypes_dict == null)
		{
			return;
		}
		this.phenotypes_dict = new Dictionary<string, List<string>>(tAsset.phenotypes_dict);
		this.phenotypes_list = new List<string>(tAsset.phenotypes_list);
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x000D366C File Offset: 0x000D186C
	public PhenotypeAsset getDefaultPhenotypeAsset()
	{
		string tPhenoID = this.phenotypes_list[0];
		return AssetManager.phenotype_library.get(tPhenoID);
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x000D3691 File Offset: 0x000D1891
	public void clearTraits()
	{
		if (this.traits != null)
		{
			this.traits.Clear();
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000D36A6 File Offset: 0x000D18A6
	public string getCollectiveTermID()
	{
		return this.collective_term;
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x000D36AE File Offset: 0x000D18AE
	public override string getLocaleID()
	{
		return this.name_locale.Underscore();
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x000D36BB File Offset: 0x000D18BB
	public string getDescriptionID()
	{
		GodPower godPower = this.getGodPower();
		if (godPower == null)
		{
			return null;
		}
		return godPower.getDescriptionID();
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000D36D0 File Offset: 0x000D18D0
	public string getLocalizedName()
	{
		string tResult;
		if (!base.isAvailable())
		{
			tResult = LocalizedTextManager.getText("achievement_tip_hidden", null, false);
		}
		else
		{
			tResult = LocalizedTextManager.getText(this.getLocaleID(), null, false);
		}
		return tResult;
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x000D3704 File Offset: 0x000D1904
	public string getLocalizedDescription()
	{
		string tResult;
		if (!base.isAvailable())
		{
			if (this.unlocked_with_achievement)
			{
				string text = LocalizedTextManager.getText("actor_locked_tooltip_text_achievement", null, false);
				string tAchievementIdTranslated = "<color=#00ffffff>" + base.getAchievementLocaleID().Localize() + "</color>";
				tResult = text.Replace("$achievement_id$", tAchievementIdTranslated);
			}
			else
			{
				tResult = LocalizedTextManager.getText("actor_locked_tooltip_text_exploration", null, false);
			}
		}
		else
		{
			tResult = LocalizedTextManager.getText(this.getDescriptionID(), null, false);
		}
		return tResult;
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x000D3774 File Offset: 0x000D1974
	public void addPreferredColors(params string[] pColors)
	{
		this.preferred_colors = new HashSet<string>(pColors);
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x000D3782 File Offset: 0x000D1982
	public string getTranslatedName()
	{
		return this.getLocaleID().Localize();
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x000D3790 File Offset: 0x000D1990
	public void addGenome(params ValueTuple<string, float>[] pListGenomePartsIDs)
	{
		foreach (ValueTuple<string, float> valueTuple in pListGenomePartsIDs)
		{
			string tGenomePartID = valueTuple.Item1;
			float tValue = valueTuple.Item2;
			GenomePart tNewGenomePart = new GenomePart(tGenomePartID, tValue);
			if (!this.genome_parts.Add(tNewGenomePart))
			{
				GenomePart tOldGenomePart;
				this.genome_parts.TryGetValue(tNewGenomePart, out tOldGenomePart);
				GenomePart tPartToOverwrite = new GenomePart(tGenomePartID, tOldGenomePart.value + tValue);
				this.genome_parts.Remove(tOldGenomePart);
				this.genome_parts.Add(tPartToOverwrite);
			}
		}
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x000D3812 File Offset: 0x000D1A12
	public string getIconPath()
	{
		return "ui/Icons/" + this.icon;
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x000D3824 File Offset: 0x000D1A24
	public Sprite getSpriteIcon()
	{
		if (this._cached_sprite == null)
		{
			this._cached_sprite = SpriteTextureLoader.getSprite(this.getIconPath());
		}
		return this._cached_sprite;
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000D3845 File Offset: 0x000D1A45
	public override Sprite getSprite()
	{
		return this.getSpriteIcon();
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x000D384D File Offset: 0x000D1A4D
	public bool hasBiomePhenotype(string pBiomeID)
	{
		return this.phenotypes_dict != null && this.phenotypes_dict.Count != 0 && this.phenotypes_dict.ContainsKey(pBiomeID);
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x000D3874 File Offset: 0x000D1A74
	public BaseStats getStatsForOverview()
	{
		if (this._cached_overview_stats == null)
		{
			this._cached_overview_stats = new BaseStats();
			this._cached_overview_stats["health"] = this.base_stats["health"];
			this._cached_overview_stats["lifespan"] = this.base_stats["lifespan"];
			this._cached_overview_stats["damage"] = this.base_stats["damage"];
			this._cached_overview_stats["speed"] = this.base_stats["speed"];
			foreach (GenomePart tGeneStat in this.genome_parts)
			{
				string id = tGeneStat.id;
				if (id == "health" || id == "lifespan" || id == "damage" || id == "speed")
				{
					BaseStats cached_overview_stats = this._cached_overview_stats;
					string id2 = tGeneStat.id;
					cached_overview_stats[id2] += tGeneStat.value;
				}
			}
		}
		return this._cached_overview_stats;
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x000D39C0 File Offset: 0x000D1BC0
	public bool hasDecisions()
	{
		List<string> list = this.decision_ids;
		return list != null && list.Count > 0;
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x000D39D8 File Offset: 0x000D1BD8
	public DecisionAsset[] getDecisions()
	{
		if (this.hasDecisions() && this._cached_assets_decisions == null)
		{
			this._cached_assets_decisions = new DecisionAsset[64];
			foreach (string tDecisionID in this.decision_ids)
			{
				DecisionAsset tAsset = AssetManager.decisions_library.get(tDecisionID);
				if (tAsset != null)
				{
					DecisionAsset[] cached_assets_decisions = this._cached_assets_decisions;
					int cached_assets_decisions_counter = this._cached_assets_decisions_counter;
					this._cached_assets_decisions_counter = cached_assets_decisions_counter + 1;
					cached_assets_decisions[cached_assets_decisions_counter] = tAsset;
				}
			}
		}
		return this._cached_assets_decisions;
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06001214 RID: 4628 RVA: 0x000D3A70 File Offset: 0x000D1C70
	public int decisions_counter
	{
		get
		{
			return this._cached_assets_decisions_counter;
		}
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000D3A78 File Offset: 0x000D1C78
	public string getDefaultKingdom()
	{
		return this.kingdom_id_wild;
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000D3A80 File Offset: 0x000D1C80
	public HashSet<SubspeciesTrait> getDefaultSubspeciesTraits()
	{
		if (this.default_subspecies_traits == null)
		{
			return null;
		}
		if (this._cached_assets_subspecies_traits == null)
		{
			this._cached_assets_subspecies_traits = new HashSet<SubspeciesTrait>();
			this.default_subspecies_traits.Sort((string pS1, string pS2) => string.Compare(pS1, pS2, StringComparison.Ordinal));
			foreach (string tTraitID in this.default_subspecies_traits)
			{
				SubspeciesTrait tTrait = AssetManager.subspecies_traits.get(tTraitID);
				if (tTrait != null)
				{
					this._cached_assets_subspecies_traits.Add(tTrait);
				}
			}
		}
		return this._cached_assets_subspecies_traits;
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000D3B38 File Offset: 0x000D1D38
	public int countPopulation()
	{
		return this.units.Count;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000D3B48 File Offset: 0x000D1D48
	public int countSubspecies()
	{
		int tResult = 0;
		using (IEnumerator<Subspecies> enumerator = World.world.subspecies.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getActorAsset() == this)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000D3BA0 File Offset: 0x000D1DA0
	public int countFamilies()
	{
		int tResult = 0;
		using (IEnumerator<Family> enumerator = World.world.families.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getActorAsset() == this)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x000D3BF8 File Offset: 0x000D1DF8
	public void addSpell(string pID)
	{
		if (this.spell_ids == null)
		{
			this.spell_ids = new List<string>();
		}
		this.spell_ids.Add(pID);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000D3C19 File Offset: 0x000D1E19
	public void addTraitGroupFilter(string pTrait)
	{
		if (this.trait_group_filter_subspecies == null)
		{
			this.trait_group_filter_subspecies = new List<string>();
		}
		if (!this.trait_group_filter_subspecies.Contains(pTrait))
		{
			this.trait_group_filter_subspecies.Add(pTrait);
		}
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x000D3C48 File Offset: 0x000D1E48
	public void addTrait(string pTraitID)
	{
		if (this.traits == null)
		{
			this.traits = new List<string>();
		}
		if (!this.traits.Contains(pTraitID))
		{
			this.traits.Add(pTraitID);
		}
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x000D3C77 File Offset: 0x000D1E77
	public void addTraitIgnore(string pTraitID)
	{
		if (this.traits_ignore == null)
		{
			this.traits_ignore = new HashSet<string>();
		}
		this.traits_ignore.Add(pTraitID);
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x000D3C99 File Offset: 0x000D1E99
	public void removeTrait(string pTrait)
	{
		List<string> list = this.traits;
		if (list != null)
		{
			list.Remove(pTrait);
		}
		List<string> list2 = this.traits;
		if (list2 != null && list2.Count == 0)
		{
			this.traits = null;
		}
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000D3CCC File Offset: 0x000D1ECC
	public void addSubspeciesTrait(string pTrait)
	{
		if (this.default_subspecies_traits == null)
		{
			this.default_subspecies_traits = new List<string>();
		}
		if (!this.default_subspecies_traits.Contains(pTrait))
		{
			this.default_subspecies_traits.Add(pTrait);
		}
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x000D3CFB File Offset: 0x000D1EFB
	public void addCultureTrait(string pTrait)
	{
		if (this.default_culture_traits == null)
		{
			this.default_culture_traits = new List<string>();
		}
		if (!this.default_culture_traits.Contains(pTrait))
		{
			this.default_culture_traits.Add(pTrait);
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x000D3D2A File Offset: 0x000D1F2A
	public void addLanguageTrait(string pTrait)
	{
		if (this.default_language_traits == null)
		{
			this.default_language_traits = new List<string>();
		}
		if (!this.default_language_traits.Contains(pTrait))
		{
			this.default_language_traits.Add(pTrait);
		}
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x000D3D59 File Offset: 0x000D1F59
	public void addKingdomTrait(string pTrait)
	{
		if (this.default_kingdom_traits == null)
		{
			this.default_kingdom_traits = new List<string>();
		}
		if (!this.default_kingdom_traits.Contains(pTrait))
		{
			this.default_kingdom_traits.Add(pTrait);
		}
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000D3D88 File Offset: 0x000D1F88
	public void addClanTrait(string pTrait)
	{
		if (this.default_clan_traits == null)
		{
			this.default_clan_traits = new List<string>();
		}
		if (!this.default_clan_traits.Contains(pTrait))
		{
			this.default_clan_traits.Add(pTrait);
		}
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000D3DB7 File Offset: 0x000D1FB7
	public void addReligionTrait(string pTrait)
	{
		if (this.default_religion_traits == null)
		{
			this.default_religion_traits = new List<string>();
		}
		if (!this.default_religion_traits.Contains(pTrait))
		{
			this.default_religion_traits.Add(pTrait);
		}
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x000D3DE6 File Offset: 0x000D1FE6
	public void addDecision(string pDecision)
	{
		if (this.decision_ids == null)
		{
			this.decision_ids = new List<string>();
		}
		if (!this.decision_ids.Contains(pDecision))
		{
			this.decision_ids.Add(pDecision);
		}
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x000D3E18 File Offset: 0x000D2018
	public override bool unlock(bool pSaveData = true)
	{
		if (!base.unlock(pSaveData))
		{
			return false;
		}
		string tAssetId = string.IsNullOrEmpty(this.base_asset_id) ? this.id : this.base_asset_id;
		ActorAsset tAsset;
		if (tAssetId != this.id)
		{
			tAsset = AssetManager.actor_library.get(tAssetId);
			if (!tAsset.unlock(true))
			{
				return false;
			}
		}
		else
		{
			tAsset = this;
		}
		PowerButton tButton;
		if (PowerButton.actor_spawn_buttons.TryGetValue(tAsset, out tButton))
		{
			tButton.icon.color = Toolbox.color_white;
		}
		return true;
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000D3E93 File Offset: 0x000D2093
	protected override bool isDebugUnlockedAll()
	{
		return DebugConfig.isOn(DebugOption.UnlockAllActors);
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000D3E9C File Offset: 0x000D209C
	public bool canEditItem(EquipmentAsset pItem)
	{
		return (!pItem.is_pool_weapon && this.can_edit_equipment) || (pItem.is_pool_weapon && this.can_edit_equipment);
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000D3EC4 File Offset: 0x000D20C4
	public void addResource(string pID, int pAmount, bool pNewList = false)
	{
		if (this.resources_given == null || pNewList)
		{
			this.resources_given = new List<ResourceContainer>();
		}
		if (this.resources_given.Count > 0)
		{
			for (int i = 0; i < this.resources_given.Count; i++)
			{
				ResourceContainer tRes = this.resources_given[i];
				if (tRes.id == pID)
				{
					tRes.amount += pAmount;
					this.resources_given[i] = tRes;
					return;
				}
			}
		}
		this.resources_given.Add(new ResourceContainer(pID, pAmount));
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x000D3F54 File Offset: 0x000D2154
	public BuildingAsset getBuildingDockAsset()
	{
		string tID_docks = "docks_" + this.architecture_id;
		return AssetManager.buildings.get(tID_docks);
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x000D3F80 File Offset: 0x000D2180
	public void setSimpleCivSettings()
	{
		this.skin_citizen_male = Toolbox.a<string>(new string[]
		{
			"male_1"
		});
		this.skin_citizen_female = Toolbox.a<string>(new string[]
		{
			"female_1"
		});
		this.skin_warrior = Toolbox.a<string>(new string[]
		{
			"warrior_1"
		});
		this.production = new string[]
		{
			"bread"
		};
		this.build_order_template_id = "build_order_basic";
		this.name_template_sets = Toolbox.a<string>(new string[]
		{
			"default_name"
		});
		this.job = Toolbox.a<string>(new string[]
		{
			"decision"
		});
		this.job_citizen = Toolbox.a<string>(new string[]
		{
			"unit_citizen"
		});
		this.job_kingdom = Toolbox.a<string>(new string[]
		{
			"decision"
		});
		this.job_baby = Toolbox.a<string>(new string[]
		{
			"decision"
		});
		this.job_attacker = Toolbox.a<string>(new string[]
		{
			"attacker"
		});
		this.kingdom_id_wild = "neutral_animals";
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000D4098 File Offset: 0x000D2298
	public bool canBecomeSapient()
	{
		return !string.IsNullOrEmpty(this.kingdom_id_civilization);
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000D40A8 File Offset: 0x000D22A8
	public bool hasDefaultSpells()
	{
		return this.spells != null && this.spells.hasAny();
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x000D40C0 File Offset: 0x000D22C0
	public TooltipData getTooltip()
	{
		GodPower tPowerAsset = this.getGodPower();
		return new TooltipData
		{
			tip_name = this.getLocaleID(),
			tip_description = this.getDescriptionID(),
			power = tPowerAsset
		};
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x000D40F8 File Offset: 0x000D22F8
	public GodPower getGodPower()
	{
		string text;
		if ((text = this.power_id) == null)
		{
			text = (this.base_asset_id ?? this.id);
		}
		string tPowerId = text;
		if (!AssetManager.powers.has(tPowerId))
		{
			return null;
		}
		return AssetManager.powers.get(tPowerId);
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x000D413C File Offset: 0x000D233C
	public string getNameTemplate(MetaType pType)
	{
		string[] array = this.name_template_sets;
		string tRandomSet = (array != null) ? array.GetRandom<string>() : null;
		if (!string.IsNullOrEmpty(tRandomSet))
		{
			return AssetManager.name_sets.get(tRandomSet).get(pType);
		}
		if (pType == MetaType.Unit)
		{
			string.IsNullOrEmpty(this.name_template_unit);
			return this.name_template_unit;
		}
		return this.name_template_unit;
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06001231 RID: 4657 RVA: 0x000D4194 File Offset: 0x000D2394
	[JsonIgnore]
	public string boat_texture_id
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000D419C File Offset: 0x000D239C
	public string[] getWalk()
	{
		return this.animation_walk;
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x000D41A4 File Offset: 0x000D23A4
	public string[] getIdle()
	{
		return this.animation_idle;
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x000D41AC File Offset: 0x000D23AC
	public string[] getSwim()
	{
		return this.animation_swim;
	}

	// Token: 0x04000FDC RID: 4060
	[DefaultValue(true)]
	public bool split_ai_update = true;

	// Token: 0x04000FDD RID: 4061
	[DefaultValue(true)]
	public bool has_ai_system = true;

	// Token: 0x04000FDE RID: 4062
	[DefaultValue(1)]
	public int item_making_skill = 1;

	// Token: 0x04000FDF RID: 4063
	public bool affected_by_dust;

	// Token: 0x04000FE0 RID: 4064
	public string sound_idle;

	// Token: 0x04000FE1 RID: 4065
	public string sound_idle_loop;

	// Token: 0x04000FE2 RID: 4066
	public string sound_spawn;

	// Token: 0x04000FE3 RID: 4067
	public string sound_death;

	// Token: 0x04000FE4 RID: 4068
	public string sound_attack;

	// Token: 0x04000FE5 RID: 4069
	[DefaultValue("event:/SFX/HIT/HitGeneric")]
	public string sound_hit = "event:/SFX/HIT/HitGeneric";

	// Token: 0x04000FE6 RID: 4070
	public bool show_controllable_tip = true;

	// Token: 0x04000FE7 RID: 4071
	public bool show_task_icon = true;

	// Token: 0x04000FE8 RID: 4072
	public string music_theme;

	// Token: 0x04000FE9 RID: 4073
	public string music_theme_civ;

	// Token: 0x04000FEA RID: 4074
	[DefaultValue(UnitTextureAtlasID.Units)]
	public UnitTextureAtlasID texture_atlas;

	// Token: 0x04000FEB RID: 4075
	public bool ignored_by_infinity_coin;

	// Token: 0x04000FEC RID: 4076
	[DefaultValue("")]
	public string name_taxonomic_kingdom = "";

	// Token: 0x04000FED RID: 4077
	[DefaultValue("")]
	public string name_taxonomic_phylum = "";

	// Token: 0x04000FEE RID: 4078
	[DefaultValue("")]
	public string name_taxonomic_class = "";

	// Token: 0x04000FEF RID: 4079
	[DefaultValue("")]
	public string name_taxonomic_order = "";

	// Token: 0x04000FF0 RID: 4080
	[DefaultValue("")]
	public string name_taxonomic_family = "";

	// Token: 0x04000FF1 RID: 4081
	[DefaultValue("")]
	public string name_taxonomic_genus = "";

	// Token: 0x04000FF2 RID: 4082
	[DefaultValue("")]
	public string name_taxonomic_species = "";

	// Token: 0x04000FF3 RID: 4083
	[DefaultValue(true)]
	public bool name_subspecies_add_biome_suffix = true;

	// Token: 0x04000FF4 RID: 4084
	public bool auto_civ;

	// Token: 0x04000FF5 RID: 4085
	[DefaultValue("")]
	public string name_locale = "";

	// Token: 0x04000FF6 RID: 4086
	[DefaultValue(StatusTier.Advanced)]
	public StatusTier allowed_status_tiers = StatusTier.Advanced;

	// Token: 0x04000FF7 RID: 4087
	[DefaultValue(true)]
	public bool render_status_effects = true;

	// Token: 0x04000FF8 RID: 4088
	[DefaultValue(ActorSize.S13_Human)]
	public ActorSize actor_size = ActorSize.S13_Human;

	// Token: 0x04000FF9 RID: 4089
	public string[] animation_walk;

	// Token: 0x04000FFA RID: 4090
	[DefaultValue(10f)]
	public float animation_walk_speed = 10f;

	// Token: 0x04000FFB RID: 4091
	public string[] animation_swim;

	// Token: 0x04000FFC RID: 4092
	[DefaultValue(8f)]
	public float animation_swim_speed = 8f;

	// Token: 0x04000FFD RID: 4093
	public string[] animation_idle = ActorAnimationSequences.walk_0;

	// Token: 0x04000FFE RID: 4094
	[DefaultValue(10f)]
	public float animation_idle_speed = 10f;

	// Token: 0x04000FFF RID: 4095
	[DefaultValue(10f)]
	public float max_shake_timer = 10f;

	// Token: 0x04001000 RID: 4096
	[DefaultValue(true)]
	public bool animation_speed_based_on_walk_speed = true;

	// Token: 0x04001001 RID: 4097
	[DefaultValue("base_attack")]
	public string default_attack = "base_attack";

	// Token: 0x04001002 RID: 4098
	public bool immune_to_tumor;

	// Token: 0x04001003 RID: 4099
	public bool immune_to_slowness;

	// Token: 0x04001004 RID: 4100
	public int aggression;

	// Token: 0x04001005 RID: 4101
	[DefaultValue(true)]
	public bool shadow = true;

	// Token: 0x04001006 RID: 4102
	[DefaultValue("unitShadow_5")]
	public string shadow_texture = "unitShadow_5";

	// Token: 0x04001007 RID: 4103
	[DefaultValue("unitShadow_2")]
	public string shadow_texture_egg = "unitShadow_2";

	// Token: 0x04001008 RID: 4104
	[DefaultValue("unitShadow_4")]
	public string shadow_texture_baby = "unitShadow_4";

	// Token: 0x04001009 RID: 4105
	[DefaultValue(true)]
	public bool hit_fx_alternative_offset = true;

	// Token: 0x0400100A RID: 4106
	[DefaultValue(true)]
	public bool can_level_up = true;

	// Token: 0x0400100B RID: 4107
	[DefaultValue(true)]
	public bool can_talk_with = true;

	// Token: 0x0400100C RID: 4108
	public float base_throwing_range;

	// Token: 0x0400100D RID: 4109
	[DefaultValue(true)]
	public bool use_tool_items = true;

	// Token: 0x0400100E RID: 4110
	public bool use_items;

	// Token: 0x0400100F RID: 4111
	public bool take_items;

	// Token: 0x04001010 RID: 4112
	public bool control_can_jump = true;

	// Token: 0x04001011 RID: 4113
	public bool control_can_talk = true;

	// Token: 0x04001012 RID: 4114
	public bool control_can_dash = true;

	// Token: 0x04001013 RID: 4115
	public bool control_can_backstep = true;

	// Token: 0x04001014 RID: 4116
	public bool control_can_steal = true;

	// Token: 0x04001015 RID: 4117
	public bool control_can_swear = true;

	// Token: 0x04001016 RID: 4118
	public bool control_can_kick = true;

	// Token: 0x04001017 RID: 4119
	public bool use_phenotypes;

	// Token: 0x04001018 RID: 4120
	[JsonIgnore]
	public Dictionary<string, List<string>> phenotypes_dict;

	// Token: 0x04001019 RID: 4121
	public List<string> phenotypes_list;

	// Token: 0x0400101A RID: 4122
	public List<string> generated_subspecies_names_prefixes;

	// Token: 0x0400101B RID: 4123
	public bool can_be_killed_by_stuff;

	// Token: 0x0400101C RID: 4124
	public bool can_be_killed_by_life_eraser;

	// Token: 0x0400101D RID: 4125
	public bool can_be_killed_by_divine_light;

	// Token: 0x0400101E RID: 4126
	[DefaultValue(true)]
	public bool show_on_meta_layer = true;

	// Token: 0x0400101F RID: 4127
	public bool ignore_tile_speed_multiplier;

	// Token: 0x04001020 RID: 4128
	public bool skip_fight_logic;

	// Token: 0x04001021 RID: 4129
	public bool can_attack_buildings;

	// Token: 0x04001022 RID: 4130
	public bool can_attack_brains;

	// Token: 0x04001023 RID: 4131
	[DefaultValue(true)]
	public bool count_as_unit = true;

	// Token: 0x04001024 RID: 4132
	public bool only_melee_attack;

	// Token: 0x04001025 RID: 4133
	public bool flag_ufo;

	// Token: 0x04001026 RID: 4134
	public bool flag_finger;

	// Token: 0x04001027 RID: 4135
	public bool flag_turtle;

	// Token: 0x04001028 RID: 4136
	public bool default_animal;

	// Token: 0x04001029 RID: 4137
	public bool civ;

	// Token: 0x0400102A RID: 4138
	public bool unit_other;

	// Token: 0x0400102B RID: 4139
	[DefaultValue("")]
	public string kingdom_id_wild = "";

	// Token: 0x0400102C RID: 4140
	[DefaultValue("")]
	public string kingdom_id_civilization = "";

	// Token: 0x0400102D RID: 4141
	public bool special;

	// Token: 0x0400102E RID: 4142
	[DefaultValue(true)]
	public bool show_in_taxonomy_tooltip = true;

	// Token: 0x0400102F RID: 4143
	[DefaultValue(true)]
	public bool render_budding = true;

	// Token: 0x04001030 RID: 4144
	public string family_banner_frame_generation_exclusion = "families/frame_11";

	// Token: 0x04001031 RID: 4145
	public string family_banner_frame_generation_inclusion;

	// Token: 0x04001032 RID: 4146
	public bool family_banner_frame_only_inclusion;

	// Token: 0x04001033 RID: 4147
	[DefaultValue("")]
	public string texture_id = "";

	// Token: 0x04001034 RID: 4148
	[DefaultValue("")]
	public string architecture_id = "";

	// Token: 0x04001035 RID: 4149
	public string texture_path_zombie_for_auto_loader_main;

	// Token: 0x04001036 RID: 4150
	public string texture_path_zombie_for_auto_loader_heads;

	// Token: 0x04001037 RID: 4151
	public ActorTextureSubAsset texture_asset;

	// Token: 0x04001038 RID: 4152
	public bool prevent_unconscious_rotation;

	// Token: 0x04001039 RID: 4153
	public bool render_heads_for_babies;

	// Token: 0x0400103A RID: 4154
	private string _debug_phenotype_color = "";

	// Token: 0x0400103B RID: 4155
	public bool body_separate_part_hands;

	// Token: 0x0400103C RID: 4156
	public bool has_baby_form;

	// Token: 0x0400103D RID: 4157
	public bool has_advanced_textures;

	// Token: 0x0400103E RID: 4158
	public List<string> decision_ids;

	// Token: 0x0400103F RID: 4159
	private DecisionAsset[] _cached_assets_decisions;

	// Token: 0x04001040 RID: 4160
	private int _cached_assets_decisions_counter;

	// Token: 0x04001041 RID: 4161
	private HashSet<SubspeciesTrait> _cached_assets_subspecies_traits;

	// Token: 0x04001042 RID: 4162
	private Sprite _cached_sprite;

	// Token: 0x04001043 RID: 4163
	private BaseStats _cached_overview_stats;

	// Token: 0x04001044 RID: 4164
	[DefaultValue(0.5f)]
	public float hovering_min = 0.5f;

	// Token: 0x04001045 RID: 4165
	[DefaultValue(1.2f)]
	public float hovering_max = 1.2f;

	// Token: 0x04001046 RID: 4166
	public bool hovering;

	// Token: 0x04001047 RID: 4167
	public bool flying;

	// Token: 0x04001048 RID: 4168
	public bool very_high_flyer;

	// Token: 0x04001049 RID: 4169
	public bool disable_jump_animation;

	// Token: 0x0400104A RID: 4170
	public bool rotating_animation;

	// Token: 0x0400104B RID: 4171
	[DefaultValue(true)]
	public bool die_on_blocks = true;

	// Token: 0x0400104C RID: 4172
	public bool ignore_blocks;

	// Token: 0x0400104D RID: 4173
	[DefaultValue(true)]
	public bool move_from_block = true;

	// Token: 0x0400104E RID: 4174
	[DefaultValue(true)]
	public bool run_to_water_when_on_fire = true;

	// Token: 0x0400104F RID: 4175
	public bool damaged_by_ocean;

	// Token: 0x04001050 RID: 4176
	[DefaultValue(true)]
	public bool cancel_beh_on_land = true;

	// Token: 0x04001051 RID: 4177
	public bool force_ocean_creature;

	// Token: 0x04001052 RID: 4178
	public bool force_land_creature;

	// Token: 0x04001053 RID: 4179
	public bool is_humanoid;

	// Token: 0x04001054 RID: 4180
	public bool is_boat;

	// Token: 0x04001055 RID: 4181
	public bool is_boat_transport;

	// Token: 0x04001056 RID: 4182
	public bool draw_boat_mark;

	// Token: 0x04001057 RID: 4183
	public bool draw_boat_mark_big;

	// Token: 0x04001058 RID: 4184
	[DefaultValue("")]
	public string boat_type = "";

	// Token: 0x04001059 RID: 4185
	[DefaultValue(6)]
	public int animal_breeding_close_units_limit = 6;

	// Token: 0x0400105A RID: 4186
	[DefaultValue("")]
	public string avatar_prefab = "";

	// Token: 0x0400105B RID: 4187
	[NonSerialized]
	public bool has_avatar_prefab;

	// Token: 0x0400105C RID: 4188
	public bool ignore_generic_render;

	// Token: 0x0400105D RID: 4189
	public bool need_colored_sprite;

	// Token: 0x0400105E RID: 4190
	public bool die_from_dispel;

	// Token: 0x0400105F RID: 4191
	[DefaultValue(true)]
	public bool die_in_lava = true;

	// Token: 0x04001060 RID: 4192
	public bool can_be_moved_by_powers;

	// Token: 0x04001061 RID: 4193
	public bool can_be_hurt_by_powers;

	// Token: 0x04001062 RID: 4194
	public bool can_turn_into_ice_one;

	// Token: 0x04001063 RID: 4195
	public bool can_turn_into_mush;

	// Token: 0x04001064 RID: 4196
	public bool can_turn_into_tumor;

	// Token: 0x04001065 RID: 4197
	public bool can_evolve_into_new_species;

	// Token: 0x04001066 RID: 4198
	public bool has_soul;

	// Token: 0x04001067 RID: 4199
	[DefaultValue(true)]
	public bool can_receive_traits = true;

	// Token: 0x04001068 RID: 4200
	public string base_asset_id;

	// Token: 0x04001069 RID: 4201
	public string power_id;

	// Token: 0x0400106A RID: 4202
	public bool zombie_auto_asset;

	// Token: 0x0400106B RID: 4203
	public bool can_turn_into_zombie;

	// Token: 0x0400106C RID: 4204
	[DefaultValue("")]
	public string zombie_id_internal = "";

	// Token: 0x0400106D RID: 4205
	public string zombie_color_hex = "#3B8130";

	// Token: 0x0400106E RID: 4206
	public bool unit_zombie;

	// Token: 0x0400106F RID: 4207
	public bool dynamic_sprite_zombie;

	// Token: 0x04001070 RID: 4208
	[DefaultValue("")]
	public string skeleton_id = "";

	// Token: 0x04001071 RID: 4209
	[DefaultValue("")]
	public string mush_id = "";

	// Token: 0x04001072 RID: 4210
	[DefaultValue("")]
	public string tumor_id = "";

	// Token: 0x04001073 RID: 4211
	[DefaultValue("")]
	public string evolution_id = "";

	// Token: 0x04001074 RID: 4212
	public bool can_turn_into_demon_in_age_of_chaos;

	// Token: 0x04001075 RID: 4213
	public bool show_icon_inspect_window;

	// Token: 0x04001076 RID: 4214
	[DefaultValue("")]
	public string show_icon_inspect_window_id = "";

	// Token: 0x04001077 RID: 4215
	public bool hide_favorite_icon;

	// Token: 0x04001078 RID: 4216
	[DefaultValue(true)]
	public bool can_be_favorited = true;

	// Token: 0x04001079 RID: 4217
	public bool can_be_inspected;

	// Token: 0x0400107A RID: 4218
	[DefaultValue(2.5f)]
	public float inspect_avatar_scale = 2.5f;

	// Token: 0x0400107B RID: 4219
	public float inspect_avatar_offset_x;

	// Token: 0x0400107C RID: 4220
	public float inspect_avatar_offset_y;

	// Token: 0x0400107D RID: 4221
	[DefaultValue(100)]
	public int nutrition_max = 100;

	// Token: 0x0400107E RID: 4222
	[DefaultValue(3)]
	public int months_breeding_timeout = 3;

	// Token: 0x0400107F RID: 4223
	[DefaultValue(18)]
	public int age_spawn = 18;

	// Token: 0x04001080 RID: 4224
	[DefaultValue(true)]
	public bool can_edit_traits = true;

	// Token: 0x04001081 RID: 4225
	[DefaultValue(false)]
	public bool can_edit_equipment;

	// Token: 0x04001082 RID: 4226
	[DefaultValue(true)]
	public bool finish_scale_on_creation = true;

	// Token: 0x04001083 RID: 4227
	[DefaultValue(2f)]
	public float path_movement_timeout = 2f;

	// Token: 0x04001084 RID: 4228
	public bool source_meat;

	// Token: 0x04001085 RID: 4229
	public bool source_meat_insect;

	// Token: 0x04001086 RID: 4230
	[DefaultValue(0.3f)]
	public float default_height = 0.3f;

	// Token: 0x04001087 RID: 4231
	public bool update_z;

	// Token: 0x04001088 RID: 4232
	public bool visible_on_minimap;

	// Token: 0x04001089 RID: 4233
	public bool follow_herd;

	// Token: 0x0400108A RID: 4234
	[DefaultValue(true)]
	public bool inspect_stats = true;

	// Token: 0x0400108B RID: 4235
	[DefaultValue(true)]
	public bool inspect_children = true;

	// Token: 0x0400108C RID: 4236
	[DefaultValue(true)]
	public bool inspect_generation = true;

	// Token: 0x0400108D RID: 4237
	[DefaultValue(true)]
	public bool inspect_sex = true;

	// Token: 0x0400108E RID: 4238
	[DefaultValue(true)]
	public bool inspect_kills = true;

	// Token: 0x0400108F RID: 4239
	[DefaultValue(true)]
	public bool inspect_experience = true;

	// Token: 0x04001090 RID: 4240
	[DefaultValue(true)]
	public bool inspect_show_species = true;

	// Token: 0x04001091 RID: 4241
	[DefaultValue(true)]
	public bool inspect_mind = true;

	// Token: 0x04001092 RID: 4242
	[DefaultValue(true)]
	public bool inspect_genealogy = true;

	// Token: 0x04001093 RID: 4243
	[DefaultValue(true)]
	public bool allow_possession = true;

	// Token: 0x04001094 RID: 4244
	[DefaultValue(true)]
	public bool allow_strange_urge_movement = true;

	// Token: 0x04001095 RID: 4245
	public bool inspect_home;

	// Token: 0x04001096 RID: 4246
	public bool immune_to_injuries;

	// Token: 0x04001097 RID: 4247
	[DefaultValue(true)]
	public bool can_be_cloned = true;

	// Token: 0x04001098 RID: 4248
	[DefaultValue(10)]
	public int experience_given = 10;

	// Token: 0x04001099 RID: 4249
	public string[] job;

	// Token: 0x0400109A RID: 4250
	public string[] job_citizen;

	// Token: 0x0400109B RID: 4251
	public string[] job_kingdom;

	// Token: 0x0400109C RID: 4252
	public string[] job_baby;

	// Token: 0x0400109D RID: 4253
	public string[] job_attacker;

	// Token: 0x0400109E RID: 4254
	public string effect_cast_top = "fx_cast_top_blue";

	// Token: 0x0400109F RID: 4255
	public string effect_cast_ground = "fx_cast_ground_blue";

	// Token: 0x040010A0 RID: 4256
	public string effect_teleport = "fx_teleport_blue";

	// Token: 0x040010A1 RID: 4257
	public List<string> spell_ids;

	// Token: 0x040010A2 RID: 4258
	public bool effect_damage;

	// Token: 0x040010A3 RID: 4259
	public bool can_flip;

	// Token: 0x040010A4 RID: 4260
	public bool special_dead_animation;

	// Token: 0x040010A5 RID: 4261
	public bool death_animation_angle;

	// Token: 0x040010A6 RID: 4262
	[DefaultValue(StatusTier.Advanced)]
	public StatusTier status_tiers = StatusTier.Advanced;

	// Token: 0x040010A7 RID: 4263
	[DefaultValue(true)]
	public bool has_sprite_renderer = true;

	// Token: 0x040010A8 RID: 4264
	public bool die_by_lightning;

	// Token: 0x040010A9 RID: 4265
	[DefaultValue(true)]
	public bool has_skin = true;

	// Token: 0x040010AA RID: 4266
	[DefaultValue("")]
	public string grow_into_id = "";

	// Token: 0x040010AB RID: 4267
	[DefaultValue("iconQuestionMark")]
	public string icon = "iconQuestionMark";

	// Token: 0x040010AC RID: 4268
	public bool skip_save;

	// Token: 0x040010AD RID: 4269
	public string color_hex;

	// Token: 0x040010AE RID: 4270
	[NonSerialized]
	public Color32? color;

	// Token: 0x040010AF RID: 4271
	public ConstructionCost cost;

	// Token: 0x040010B0 RID: 4272
	[DefaultValue(40)]
	public int species_spawn_radius = 40;

	// Token: 0x040010B1 RID: 4273
	public bool can_have_subspecies;

	// Token: 0x040010B2 RID: 4274
	public int genome_size;

	// Token: 0x040010B3 RID: 4275
	[DefaultValue(30)]
	public int family_spawn_radius = 30;

	// Token: 0x040010B4 RID: 4276
	[DefaultValue(20)]
	public int family_limit = 20;

	// Token: 0x040010B5 RID: 4277
	public bool create_family_at_spawn;

	// Token: 0x040010B6 RID: 4278
	[DefaultValue(FamilyParentsMode.Normal)]
	public FamilyParentsMode family_show_parents;

	// Token: 0x040010B7 RID: 4279
	[DefaultValue("COLLECTIVE_NAME")]
	public string collective_term = "COLLECTIVE_NAME";

	// Token: 0x040010B8 RID: 4280
	[DefaultValue(50)]
	public int language_spawn_radius = 50;

	// Token: 0x040010B9 RID: 4281
	public List<string> traits;

	// Token: 0x040010BA RID: 4282
	public HashSet<string> traits_ignore;

	// Token: 0x040010BB RID: 4283
	public List<string> preferred_attribute;

	// Token: 0x040010BC RID: 4284
	[DefaultValue(null)]
	public HashSet<string> preferred_colors;

	// Token: 0x040010BD RID: 4285
	public string[] production;

	// Token: 0x040010BE RID: 4286
	[DefaultValue(null)]
	public string[] name_template_sets;

	// Token: 0x040010BF RID: 4287
	[DefaultValue("default_name")]
	public string name_template_unit = "default_name";

	// Token: 0x040010C0 RID: 4288
	[DefaultValue("")]
	public string banner_id = "";

	// Token: 0x040010C1 RID: 4289
	[DefaultValue("")]
	public string build_order_template_id = "";

	// Token: 0x040010C2 RID: 4290
	[DefaultValue(4)]
	public int civ_base_cities = 4;

	// Token: 0x040010C3 RID: 4291
	[DefaultValue(0.35f)]
	public float civ_base_army_multiplier = 0.35f;

	// Token: 0x040010C4 RID: 4292
	public List<string> default_subspecies_traits;

	// Token: 0x040010C5 RID: 4293
	public List<string> default_clan_traits;

	// Token: 0x040010C6 RID: 4294
	public List<string> default_culture_traits;

	// Token: 0x040010C7 RID: 4295
	public List<string> default_language_traits;

	// Token: 0x040010C8 RID: 4296
	public List<string> default_kingdom_traits;

	// Token: 0x040010C9 RID: 4297
	public List<string> default_religion_traits;

	// Token: 0x040010CA RID: 4298
	public List<string> trait_filter_subspecies;

	// Token: 0x040010CB RID: 4299
	public List<string> trait_group_filter_subspecies;

	// Token: 0x040010CC RID: 4300
	public List<ResourceContainer> resources_given;

	// Token: 0x040010CD RID: 4301
	public string[] skin_citizen_male;

	// Token: 0x040010CE RID: 4302
	public string[] skin_citizen_female;

	// Token: 0x040010CF RID: 4303
	public string[] skin_warrior;

	// Token: 0x040010D0 RID: 4304
	public string[] default_weapons;

	// Token: 0x040010D1 RID: 4305
	public ActorGetSprite get_override_sprite;

	// Token: 0x040010D2 RID: 4306
	[NonSerialized]
	public bool has_override_sprite;

	// Token: 0x040010D3 RID: 4307
	public ActorGetSprites get_override_avatar_frames;

	// Token: 0x040010D4 RID: 4308
	[NonSerialized]
	public bool has_override_avatar_frames;

	// Token: 0x040010D5 RID: 4309
	public List<string> chromosomes_first;

	// Token: 0x040010D6 RID: 4310
	public HashSet<GenomePart> genome_parts = new HashSet<GenomePart>();

	// Token: 0x040010D7 RID: 4311
	[DefaultValue(3)]
	public int max_random_amount = 3;

	// Token: 0x040010D8 RID: 4312
	[DefaultValue(true)]
	public bool can_be_surprised = true;

	// Token: 0x040010D9 RID: 4313
	[NonSerialized]
	public HashSet<Actor> units = new HashSet<Actor>();

	// Token: 0x040010DA RID: 4314
	[NonSerialized]
	public ArchitectureAsset architecture_asset;

	// Token: 0x040010DB RID: 4315
	[NonSerialized]
	public SpellHolder spells;

	// Token: 0x040010DC RID: 4316
	public BaseActionActor action_on_load;

	// Token: 0x040010DD RID: 4317
	public WorldAction action_click;

	// Token: 0x040010DE RID: 4318
	public WorldAction action_death;

	// Token: 0x040010DF RID: 4319
	public DeadAnimation action_dead_animation;

	// Token: 0x040010E0 RID: 4320
	public GetHitAction action_get_hit;

	// Token: 0x040010E1 RID: 4321
	public WorldAction check_flip;

	// Token: 0x040010E2 RID: 4322
	public bool force_hide_stamina;

	// Token: 0x040010E3 RID: 4323
	public bool force_hide_mana;
}
