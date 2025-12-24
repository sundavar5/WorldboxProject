using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class Subspecies : MetaObjectWithTraits<SubspeciesData, SubspeciesTrait>, ISapient
{
	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000FBD23 File Offset: 0x000F9F23
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000FBD26 File Offset: 0x000F9F26
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.subspecies;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000FBD32 File Offset: 0x000F9F32
	protected override bool track_death_types
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x000FBD35 File Offset: 0x000F9F35
	protected override void setDefaultValues()
	{
		base.setDefaultValues();
		this.initReproductionCounters();
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x000FBD44 File Offset: 0x000F9F44
	public void newSpecies(ActorAsset pAsset, WorldTile pTile, bool pMutation = false)
	{
		this.data.species_id = pAsset.id;
		this.generateNewMetaObject();
		if (pMutation)
		{
			this.addDNAMutationToSeed();
		}
		this.generateNucleus();
		this.generateActorBirthTraits();
		this.generatePhenotype(pAsset, pTile);
		this.generateName(pAsset, pTile);
		this.createSkins();
		this._trait_changed_event = false;
		this.recalcBaseStats();
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x000FBDA0 File Offset: 0x000F9FA0
	protected override void generateNewMetaObject()
	{
		base.generateNewMetaObject();
		if (WorldLawLibrary.world_law_mutant_box.isEnabled())
		{
			int tAmount = Randy.randomInt(1, 4);
			for (int i = 0; i < tAmount; i++)
			{
				SubspeciesTrait tTrait = AssetManager.subspecies_traits.getRandomSpawnTrait();
				if (tTrait.isAvailable())
				{
					this.addTrait(tTrait, true);
				}
			}
		}
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x000FBDF0 File Offset: 0x000F9FF0
	private void createSkins()
	{
		ActorAsset tAsset = this.getActorAsset();
		int tSkinUnit = Randy.randomInt(0, tAsset.skin_citizen_female.Length);
		this.data.skin_id = tSkinUnit;
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x000FBE1F File Offset: 0x000FA01F
	public string getSkinFemale()
	{
		return this._cached_skin_female;
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x000FBE27 File Offset: 0x000FA027
	public string getSkinMale()
	{
		return this._cached_skin_male;
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x000FBE2F File Offset: 0x000FA02F
	public string getSkinWarrior()
	{
		return this._cached_skin_warrior;
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x000FBE37 File Offset: 0x000FA037
	public bool hasEvolvedIntoForm()
	{
		return this.data.evolved_into_subspecies.hasValue();
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x000FBE4C File Offset: 0x000FA04C
	public Subspecies getEvolvedInto()
	{
		Subspecies tSubspecies = World.world.subspecies.get(this.data.evolved_into_subspecies);
		if (tSubspecies == null)
		{
			return null;
		}
		if (!tSubspecies.isAlive())
		{
			return null;
		}
		return tSubspecies;
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x000FBE84 File Offset: 0x000FA084
	public void setEvolutionSubspecies(Subspecies pSubspecies)
	{
		if (this.data.evolved_into_subspecies.hasValue() && World.world.getWorldTimeElapsedSince(this.data.last_evolution_timestamp) < 60f)
		{
			return;
		}
		this.data.last_evolution_timestamp = World.world.getCurWorldTime();
		this.data.evolved_into_subspecies = pSubspecies.getID();
	}

	// Token: 0x06001B3F RID: 6975 RVA: 0x000FBEE6 File Offset: 0x000FA0E6
	public int getMaxRandomMutations()
	{
		return (int)this.base_stats_meta["mutation"];
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x000FBEFC File Offset: 0x000FA0FC
	public int getAmountOfRandomMutationsSubspecies()
	{
		int tMutationsStatMax = this.getMaxRandomMutations();
		if (tMutationsStatMax == 0)
		{
			return 0;
		}
		return Randy.randomInt(0, tMutationsStatMax + 1);
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x000FBF20 File Offset: 0x000FA120
	public int getAmountOfRandomMutationsActorTraits()
	{
		int tMutationsStatMax = this.getMaxRandomMutations() + 1;
		return Randy.randomInt(0, tMutationsStatMax);
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x000FBF40 File Offset: 0x000FA140
	public void mutateFrom(Subspecies pParentsSubspecies)
	{
		int tCurrentRandomMutations = pParentsSubspecies.getAmountOfRandomMutationsSubspecies();
		this.cloneSubspeciesTraits(pParentsSubspecies);
		this.nucleus.cloneFrom(pParentsSubspecies.nucleus);
		this.nucleus.doRandomGeneMutations(tCurrentRandomMutations + 1);
		this.mutateTraits(tCurrentRandomMutations);
		this.genesChangedEvent();
		this.increaseGeneration(pParentsSubspecies.getGeneration());
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x000FBF93 File Offset: 0x000FA193
	private void increaseGeneration(int pFromGeneration)
	{
		this.setGeneration(pFromGeneration + 1);
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000FBF9E File Offset: 0x000FA19E
	private void setGeneration(int pValue)
	{
		this.data.generation = pValue;
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x000FBFAC File Offset: 0x000FA1AC
	public int getGeneration()
	{
		return this.data.generation;
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x000FBFBC File Offset: 0x000FA1BC
	private void cloneSubspeciesTraits(Subspecies pParentsSubspecies)
	{
		bool tIsZombies = this.getActorAsset().unit_zombie;
		base.clearTraits();
		foreach (SubspeciesTrait tTrait in pParentsSubspecies.getTraits())
		{
			if (!tIsZombies || !tTrait.remove_for_zombies)
			{
				this.addTrait(tTrait, false);
			}
		}
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x000FC028 File Offset: 0x000FA228
	internal void mutateTraits(int pMutations)
	{
		int tAmountAdded = 0;
		for (int i = 0; i < pMutations; i++)
		{
			SubspeciesTrait tTrait = AssetManager.subspecies_traits.getRandomMutationTraitToAdd();
			if (this.addTrait(tTrait, true))
			{
				tAmountAdded++;
			}
		}
		if (tAmountAdded > 0)
		{
			int tCountRemoved = 0;
			for (int j = 0; j < tAmountAdded; j++)
			{
				SubspeciesTrait tTrait2 = AssetManager.subspecies_traits.getRandomMutationTraitToRemove();
				if (this.removeTrait(tTrait2))
				{
					tCountRemoved++;
				}
			}
		}
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x000FC08C File Offset: 0x000FA28C
	public override void increaseBirths()
	{
		base.increaseBirths();
		base.addRenown(1);
		RateCounter rateCounter = this.counter_births;
		if (rateCounter == null)
		{
			return;
		}
		rateCounter.registerEvent();
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x000FC0AB File Offset: 0x000FA2AB
	public bool needOppositeSexTypeForReproduction()
	{
		return this.hasTraitReproductionSexual();
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x000FC0B8 File Offset: 0x000FA2B8
	public bool isPartnerSuitableForReproduction(Actor pActor, Actor pTarget)
	{
		return !this.needOppositeSexTypeForReproduction() || pActor.data.sex != pTarget.data.sex;
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x000FC0E0 File Offset: 0x000FA2E0
	public int getRandomPhenotypeIndex()
	{
		PhenotypeAsset tAsset = this.getRandomPhenotypeAsset();
		if (tAsset == null)
		{
			return 0;
		}
		return tAsset.phenotype_index;
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x000FC0FF File Offset: 0x000FA2FF
	public PhenotypeAsset getRandomPhenotypeAsset()
	{
		if (this._phenotype_list_assets.Count == 0)
		{
			return null;
		}
		return this._phenotype_list_assets.GetRandom<PhenotypeAsset>();
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x000FC11B File Offset: 0x000FA31B
	public int getMainPhenotypeIndexForBanner()
	{
		return this._cached_phenotype_index_for_banner;
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000FC123 File Offset: 0x000FA323
	protected override AssetLibrary<SubspeciesTrait> trait_library
	{
		get
		{
			return AssetManager.subspecies_traits;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06001B4F RID: 6991 RVA: 0x000FC12A File Offset: 0x000FA32A
	protected override List<string> default_traits
	{
		get
		{
			return this.getActorAsset().default_subspecies_traits;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000FC137 File Offset: 0x000FA337
	protected override List<string> saved_traits
	{
		get
		{
			return this.data.saved_traits;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000FC144 File Offset: 0x000FA344
	protected override string species_id
	{
		get
		{
			return this.data.species_id;
		}
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x000FC154 File Offset: 0x000FA354
	public void generateActorBirthTraits()
	{
		ActorAsset tAsset = this.getActorAsset();
		this._actor_birth_traits.init(tAsset, this);
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x000FC175 File Offset: 0x000FA375
	public void makeSapient()
	{
		this.addTrait("amygdala", false);
		this.addTrait("advanced_hippocampus", false);
		this.addTrait("prefrontal_cortex", false);
		this.addTrait("wernicke_area", false);
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000FC1AC File Offset: 0x000FA3AC
	public void generateNucleus()
	{
		ActorAsset tAsset = this.getActorAsset();
		Randy.resetSeed(World.world.map_stats.life_dna + (long)tAsset.getIndexID() + (long)tAsset.countSubspecies() + (long)this.data.mutation);
		this.nucleus.createFrom(tAsset);
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000FC1FD File Offset: 0x000FA3FD
	public void addDNAMutationToSeed()
	{
		this.data.mutation = Randy.randomInt(0, 55555);
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x000FC215 File Offset: 0x000FA415
	public void genesChangedEvent()
	{
		this.nucleus.setDirty();
		this.recalcBaseStats();
		this.makeAllUnitsDirtyAndConfused();
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x000FC230 File Offset: 0x000FA430
	private void makeAllUnitsDirtyAndConfused()
	{
		foreach (Actor tActor in base.units)
		{
			if (!tActor.isRekt())
			{
				tActor.event_full_stats = true;
				tActor.setStatsDirty();
				tActor.cancelAllBeh();
				tActor.makeConfused(-1f, false);
			}
		}
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x000FC2A4 File Offset: 0x000FA4A4
	public bool isBiomeSpecific()
	{
		return !(this.data.biome_variant == "default_color");
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x000FC2C0 File Offset: 0x000FA4C0
	public bool hasPhenotype()
	{
		return this.getActorAsset().use_phenotypes;
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x000FC2CD File Offset: 0x000FA4CD
	public override void generateBanner()
	{
		this.data.banner_background_id = AssetManager.subspecies_banners_library.getNewIndexBackground();
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x000FC2E4 File Offset: 0x000FA4E4
	public int getMetabolicRate()
	{
		return this._cached_metabolic_rate;
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x000FC2EC File Offset: 0x000FA4EC
	protected override void recalcBaseStats()
	{
		base.recalcBaseStats();
		this.clearVisualCache();
		if (this._trait_changed_event)
		{
			this._trait_changed_event = false;
			this.makeAllUnitsDirtyAndConfused();
		}
		this.base_stats.mergeStats(this.getActorAsset().base_stats, 1f);
		this.base_stats.mergeStats(this.nucleus.getStats(), 1f);
		this.base_stats_meta.mergeStats(this.nucleus.getStatsMeta(), 1f);
		this.base_stats["health"] = Mathf.Max(this.base_stats["health"], 1f);
		this.base_stats["damage"] = Mathf.Max(this.base_stats["damage"], 1f);
		this.base_stats["lifespan"] = Mathf.Max(this.base_stats["lifespan"], 1f);
		this.base_stats["speed"] = Mathf.Max(this.base_stats["speed"], 1f);
		this._cached_metabolic_rate = (int)Mathf.Max((float)SimGlobals.m.base_metabolic_rate, this.base_stats["metabolic_rate"]);
		this._cached_energy_preserver = base.hasTrait("energy_preserver");
		this._timid = base.hasTrait("cautious_instincts");
		this._curious = base.hasTrait("inquisitive_nature");
		this._water_creature = base.hasTrait("aquatic");
		this._hovering = base.hasTrait("hovering");
		this.checkForgetMetas();
		this.cacheTags();
		this.calcAllowedFoodByDiet();
		this.checkMutationSkin();
		this.cacheSkins();
		this.checkReproductionStrategy();
		this.calculateAgeRelatedStats();
		this.checkCurrentColor();
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06001B5D RID: 7005 RVA: 0x000FC4BF File Offset: 0x000FA6BF
	public int cached_males
	{
		get
		{
			return this._cached_males;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000FC4C7 File Offset: 0x000FA6C7
	public int cached_females
	{
		get
		{
			return this._cached_females;
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06001B5F RID: 7007 RVA: 0x000FC4CF File Offset: 0x000FA6CF
	public bool has_trait_energy_preserver
	{
		get
		{
			return this._cached_energy_preserver;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000FC4D7 File Offset: 0x000FA6D7
	public bool has_trait_timid
	{
		get
		{
			return this._timid;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06001B61 RID: 7009 RVA: 0x000FC4DF File Offset: 0x000FA6DF
	public bool has_trait_curious
	{
		get
		{
			return this._curious;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06001B62 RID: 7010 RVA: 0x000FC4E7 File Offset: 0x000FA6E7
	public bool has_trait_water_creature
	{
		get
		{
			return this._water_creature;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000FC4EF File Offset: 0x000FA6EF
	public bool has_trait_hovering
	{
		get
		{
			return this._hovering;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000FC4F7 File Offset: 0x000FA6F7
	public bool has_trait_pollinating
	{
		get
		{
			return this._pollinating;
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x000FC500 File Offset: 0x000FA700
	private void checkForgetMetas()
	{
		bool is_sapient = this._is_sapient;
		bool tHadAdvancedMemory = this._has_advanced_memory;
		bool tHadAdvancedCommunication = this._has_advanced_communication;
		bool tCurrentIsSapient = base.hasMetaTag("has_sapience");
		bool tCurrentHasAdvancedMemory = base.hasMetaTag("has_advanced_memory");
		bool tCurrentHasAdvancedCommunication = base.hasMetaTag("has_advanced_communication");
		if (is_sapient && !tCurrentIsSapient)
		{
			foreach (Actor tActor in base.units)
			{
				if (!tActor.isRekt() && tActor.isKingdomCiv())
				{
					tActor.forgetKingdomAndCity();
				}
			}
		}
		if (tHadAdvancedMemory != tCurrentHasAdvancedMemory)
		{
			foreach (Actor tActor2 in base.units)
			{
				if (!tActor2.isRekt())
				{
					if (tActor2.hasCulture())
					{
						tActor2.forgetCulture();
					}
					if (tActor2.hasReligion())
					{
						tActor2.forgetReligion();
					}
				}
			}
		}
		if (tHadAdvancedCommunication != tCurrentHasAdvancedCommunication)
		{
			foreach (Actor tActor3 in base.units)
			{
				if (!tActor3.isRekt() && tActor3.hasLanguage())
				{
					tActor3.forgetLanguage();
				}
			}
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000FC66C File Offset: 0x000FA86C
	private void calculateAgeRelatedStats()
	{
		this.getActorAsset();
		int tLifespan = (int)this.base_stats["lifespan"];
		float tAgeAdult;
		float tAgeBreeding;
		if ((float)tLifespan > 30f && this.isSapient())
		{
			tAgeAdult = 16f;
			tAgeBreeding = 18f;
		}
		else
		{
			tAgeAdult = Mathf.Pow((float)tLifespan, 0.55f) * 1.1f;
			tAgeBreeding = tAgeAdult;
		}
		if (tAgeAdult > 16f)
		{
			tAgeAdult = 16f;
		}
		if (this.isSapient() && tAgeBreeding > 18f)
		{
			tAgeBreeding = 18f;
		}
		this.base_stats_meta["age_adult"] = tAgeAdult;
		this.base_stats_meta["age_breeding"] = tAgeBreeding;
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x000FC70C File Offset: 0x000FA90C
	private void cacheTags()
	{
		this._is_sapient = base.hasMetaTag("has_sapience");
		this._needs_food = base.hasMetaTag("needs_food");
		this._needs_mate = base.hasMetaTag("needs_mate");
		this._can_process_emotions = base.hasMetaTag("has_emotions");
		this._has_advanced_memory = base.hasMetaTag("has_advanced_memory");
		this._has_advanced_communication = base.hasMetaTag("has_advanced_communication");
		this._damaged_by_water = base.hasMetaTag("damaged_by_water");
		this._diet_vegetation = base.hasMetaTag("diet_vegetation");
		this._diet_meat = base.hasMetaTag("diet_meat");
		this._diet_blood = base.hasMetaTag("diet_blood");
		this._diet_minerals = base.hasMetaTag("diet_minerals");
		this._diet_wood = base.hasMetaTag("diet_wood");
		this._diet_cannibalism = base.hasMetaTag("diet_same_species");
		this._magic = base.hasMetaTag("magic");
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x000FC807 File Offset: 0x000FAA07
	public bool hasCannibalism()
	{
		return this._diet_cannibalism;
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x000FC80F File Offset: 0x000FAA0F
	public bool isSapient()
	{
		return this._is_sapient;
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x000FC817 File Offset: 0x000FAA17
	public bool isMagic()
	{
		return this._magic;
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x000FC81F File Offset: 0x000FAA1F
	public ReproductiveStrategy getReproductionStrategy()
	{
		if (this.hasTraitOviparity())
		{
			return ReproductiveStrategy.Egg;
		}
		if (this.hasTraitViviparity())
		{
			return ReproductiveStrategy.Pregnancy;
		}
		return ReproductiveStrategy.SpawnUnitImmediate;
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x000FC836 File Offset: 0x000FAA36
	public bool isReproductionSexual()
	{
		return base.hasMetaTag("reproduction_sexual");
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x000FC843 File Offset: 0x000FAA43
	public bool hasTraitReproductionSexual()
	{
		return base.hasTrait("reproduction_sexual");
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x000FC850 File Offset: 0x000FAA50
	public bool hasTraitReproductionSexualHermaphroditic()
	{
		return base.hasTrait("reproduction_hermaphroditic");
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x000FC85D File Offset: 0x000FAA5D
	public bool hasTraitOviparity()
	{
		return base.hasTrait("reproduction_strategy_oviparity");
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x000FC86A File Offset: 0x000FAA6A
	public bool hasTraitViviparity()
	{
		return base.hasTrait("reproduction_strategy_viviparity");
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x000FC878 File Offset: 0x000FAA78
	private void checkReproductionStrategy()
	{
		bool tPreviousEggForm = this._has_egg_form;
		if (base.hasTrait("reproduction_strategy_oviparity"))
		{
			this._has_egg_form = true;
			this._egg_id = "egg_shell_plain";
			foreach (SubspeciesTrait tTrait in base.getTraits())
			{
				if (tTrait.phenotype_egg)
				{
					this._egg_id = tTrait.id_egg;
					break;
				}
			}
			this._egg_asset = AssetManager.subspecies_traits.get(this._egg_id);
		}
		else
		{
			this._has_egg_form = false;
		}
		if (tPreviousEggForm != this._has_egg_form)
		{
			this.resetUnitSprites();
			foreach (Actor tActor in base.units)
			{
				if (!tActor.isRekt())
				{
					tActor.cancelAllBeh();
				}
			}
		}
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x000FC974 File Offset: 0x000FAB74
	private void checkMutationSkin()
	{
		this._mutation_skin_asset = null;
		bool tPrev = this._has_mutation_reskin;
		this._has_mutation_reskin = false;
		foreach (SubspeciesTrait tTrait in base.getTraits())
		{
			if (tTrait.is_mutation_skin)
			{
				this._mutation_skin_id = tTrait.id;
				this._mutation_skin_asset = AssetManager.subspecies_traits.get(this._mutation_skin_id);
				this._has_mutation_reskin = true;
				break;
			}
		}
		if (tPrev != this._has_mutation_reskin)
		{
			this.resetUnitSprites();
		}
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x000FCA14 File Offset: 0x000FAC14
	private void cacheSkins()
	{
		int tSkinId = this.data.skin_id;
		if (this._has_mutation_reskin)
		{
			int tAmount = this._mutation_skin_asset.skin_citizen_male.Count;
			int tIndex = Toolbox.loopIndex(tSkinId, tAmount);
			this._cached_skin_male = this._mutation_skin_asset.skin_citizen_male[tIndex];
			this._cached_skin_female = this._mutation_skin_asset.skin_citizen_female[tIndex];
			this._cached_skin_warrior = this._mutation_skin_asset.skin_warrior[tIndex];
			return;
		}
		ActorAsset tActorAsset = this.getActorAsset();
		this._cached_skin_male = tActorAsset.skin_citizen_male[tSkinId];
		this._cached_skin_female = tActorAsset.skin_citizen_female[tSkinId];
		this._cached_skin_warrior = tActorAsset.skin_warrior[tSkinId];
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x000FCAC8 File Offset: 0x000FACC8
	private void checkCurrentColor()
	{
		if (!this.getActorAsset().use_phenotypes)
		{
			return;
		}
		IList<PhenotypeAsset> pList = new ListPool<PhenotypeAsset>(this._phenotype_list_assets);
		this.clearPhenotypeCache();
		this.fillPhenotypeCache();
		if (!Toolbox.areListsEqual<PhenotypeAsset>(pList, this._phenotype_list_assets))
		{
			this.resetUnitSprites();
			this._cached_phenotype_index_for_banner = this._phenotype_list_assets.GetRandom<PhenotypeAsset>().phenotype_index;
		}
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x000FCB24 File Offset: 0x000FAD24
	private void fillPhenotypeCache()
	{
		ActorAsset tActorAsset = this.getActorAsset();
		if (tActorAsset.use_phenotypes)
		{
			foreach (SubspeciesTrait tTrait in base.getTraits())
			{
				if (tTrait.phenotype_skin)
				{
					PhenotypeAsset tPhenotypeAsset = tTrait.getPhenotypeAsset();
					this.cachePhenotype(tPhenotypeAsset);
				}
			}
			if (this._phenotypes_set_indexes.Count == 0)
			{
				PhenotypeAsset tPhenotypeAsset2 = tActorAsset.getDefaultPhenotypeAsset();
				this.cachePhenotype(tPhenotypeAsset2);
			}
		}
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x000FCBAC File Offset: 0x000FADAC
	private void clearPhenotypeCache()
	{
		this._phenotype_list_assets.Clear();
		this._phenotypes_set_indexes.Clear();
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x000FCBC4 File Offset: 0x000FADC4
	private void cachePhenotype(PhenotypeAsset pPhenotypeAsset)
	{
		this._phenotype_list_assets.Add(pPhenotypeAsset);
		this._phenotypes_set_indexes.Add(pPhenotypeAsset.phenotype_index);
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x000FCBE4 File Offset: 0x000FADE4
	public void checkPhenotypeColor()
	{
		foreach (Actor tActor in base.units)
		{
			if (!tActor.isRekt())
			{
				this.checkIfPhenotypeIsLegit(tActor);
			}
		}
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x000FCC40 File Offset: 0x000FAE40
	private void checkIfPhenotypeIsLegit(Actor pActor)
	{
		int tPrevPhenotype = pActor.data.phenotype_index;
		if (tPrevPhenotype == 0 || !this._phenotypes_set_indexes.Contains(tPrevPhenotype))
		{
			pActor.generatePhenotypeAndShade();
		}
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x000FCC70 File Offset: 0x000FAE70
	private void resetUnitSprites()
	{
		foreach (Actor tActor in base.units)
		{
			if (!tActor.isRekt())
			{
				this.checkIfPhenotypeIsLegit(tActor);
				tActor.setStatsDirty();
				tActor.clearSprites();
				tActor.clearLastColorCache();
			}
		}
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x000FCCE0 File Offset: 0x000FAEE0
	public int countCurrentFamilies()
	{
		int tResult = 0;
		using (IEnumerator<Family> enumerator = World.world.families.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.data.subspecies_id == this.data.id)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x000FCD48 File Offset: 0x000FAF48
	public Sprite getSpriteBackground()
	{
		return AssetManager.subspecies_banners_library.getSpriteBackground(this.data.banner_background_id);
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x000FCD5F File Offset: 0x000FAF5F
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.subspecies_colors_library;
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x000FCD66 File Offset: 0x000FAF66
	public bool isSpecies(string pSpeciesCheck)
	{
		return this.species_id == pSpeciesCheck;
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x000FCD74 File Offset: 0x000FAF74
	private void generateName(ActorAsset pAsset, WorldTile pTile)
	{
		using (StringBuilderPool tNameBuilder = new StringBuilderPool())
		{
			tNameBuilder.Append(pAsset.name_taxonomic_genus);
			if (!string.IsNullOrEmpty(pAsset.name_taxonomic_species))
			{
				tNameBuilder.Append(" ");
				tNameBuilder.Append(pAsset.name_taxonomic_species);
			}
			if (pAsset.name_subspecies_add_biome_suffix && pTile.Type.is_biome && pAsset.hasBiomePhenotype(pTile.Type.biome_asset.id))
			{
				string tBiomeName = pTile.Type.biome_asset.subspecies_name_suffix.GetRandom<string>();
				tNameBuilder.Append(" ");
				tNameBuilder.Append(tBiomeName);
			}
			int i = 0;
			while (i < 5 && this.hasNameInWorld(tNameBuilder))
			{
				tNameBuilder.Append(SubspeciesManager.NAME_ENDINGS.GetRandom<string>());
				i++;
			}
			tNameBuilder.ToTitleCase();
			base.setName(tNameBuilder.ToString(), true);
		}
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x000FCE68 File Offset: 0x000FB068
	private bool hasNameInWorld(StringBuilderPool pName)
	{
		ReadOnlySpan<char> tTemp = pName.AsSpan();
		Span<char> tName = new char[tTemp.Length];
		tTemp.ToLowerInvariant(tName);
		Span<char> tCheck = new char[pName.Length];
		foreach (Subspecies tSubspecies in World.world.subspecies)
		{
			if (tSubspecies != this)
			{
				string tCheckName = tSubspecies.name;
				if (tCheckName.Length == tName.Length)
				{
					tCheckName.AsSpan().ToLowerInvariant(tCheck);
					if (tName.SequenceEqual(tCheck))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x000FCF30 File Offset: 0x000FB130
	private void calcAllowedFoodByDiet()
	{
		this._allowed_food_by_diet.Clear();
		foreach (KeyValuePair<string, List<string>> tPair in AssetManager.resources.diet_food_pools)
		{
			string tSubspeciesDietTrait = tPair.Key;
			if (base.hasTrait(tSubspeciesDietTrait))
			{
				List<string> tFoodList = tPair.Value;
				this._allowed_food_by_diet.UnionWith(tFoodList);
			}
		}
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x000FCFB0 File Offset: 0x000FB1B0
	public HashSet<string> getAllowedFoodByDiet()
	{
		return this._allowed_food_by_diet;
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x000FCFB8 File Offset: 0x000FB1B8
	private void generatePhenotype(ActorAsset pAsset, WorldTile pTile)
	{
		if (!pAsset.use_phenotypes)
		{
			return;
		}
		this.data.biome_variant = pTile.Type.biome_id;
		if (string.IsNullOrEmpty(this.data.biome_variant))
		{
			this.data.biome_variant = "default_color";
		}
		this.generatePhenotype(pAsset, this.data.biome_variant);
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x000FD018 File Offset: 0x000FB218
	private void generatePhenotype(ActorAsset pAsset, string pColorVariationForBiome = "default_color")
	{
		if (!pAsset.use_phenotypes)
		{
			return;
		}
		if (pAsset.phenotypes_dict == null || pAsset.phenotypes_dict.Count == 0)
		{
			Debug.LogError("No phenotypes. Check assets " + pAsset.id);
			return;
		}
		if (!pAsset.hasBiomePhenotype(pColorVariationForBiome))
		{
			pColorVariationForBiome = "default_color";
		}
		List<string> tPhenotypeList = pAsset.phenotypes_dict[pColorVariationForBiome];
		if (tPhenotypeList.Count == 0)
		{
			return;
		}
		string tPhenotypeID = tPhenotypeList.GetRandom<string>();
		PhenotypeAsset tPhenotypeAsset = AssetManager.phenotype_library.get(tPhenotypeID);
		SubspeciesTrait tSkinTrait = AssetManager.subspecies_traits.get(tPhenotypeAsset.subspecies_trait_id);
		this.addTrait(tSkinTrait, false);
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x000FD0AC File Offset: 0x000FB2AC
	public override void loadData(SubspeciesData pData)
	{
		base.loadData(pData);
		this.nucleus.reset();
		List<ChromosomeData> saved_chromosome_data = this.data.saved_chromosome_data;
		if (saved_chromosome_data != null && saved_chromosome_data.Count > 0)
		{
			foreach (ChromosomeData tChromosomeData in this.data.saved_chromosome_data)
			{
				Chromosome tLoadedChromosome = new Chromosome(tChromosomeData.chromosome_type, false);
				tLoadedChromosome.load(tChromosomeData);
				this.nucleus.addChromosome(tLoadedChromosome);
			}
		}
		this._actor_birth_traits.setSubspecies(this);
		this._actor_birth_traits.reset();
		this._actor_birth_traits.fillTraitAssetsFromStringList(this.data.saved_actor_birth_traits);
		this.recalcBaseStats();
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x000FD180 File Offset: 0x000FB380
	public override void save()
	{
		base.save();
		this.data.saved_chromosome_data = this.nucleus.getListForSave();
		this.data.saved_traits = base.getTraitsAsStrings();
		this.data.saved_actor_birth_traits = this._actor_birth_traits.getTraitsAsStrings();
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x000FD1D0 File Offset: 0x000FB3D0
	public void debugClear()
	{
		this.loadData(this.data);
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06001B88 RID: 7048 RVA: 0x000FD1DE File Offset: 0x000FB3DE
	public float age_adult
	{
		get
		{
			return this.base_stats_meta["age_adult"];
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000FD1F0 File Offset: 0x000FB3F0
	public float age_breeding
	{
		get
		{
			return this.base_stats_meta["age_breeding"];
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06001B8A RID: 7050 RVA: 0x000FD202 File Offset: 0x000FB402
	public bool diet_vegetation
	{
		get
		{
			return this._diet_vegetation;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06001B8B RID: 7051 RVA: 0x000FD20A File Offset: 0x000FB40A
	public bool diet_meat
	{
		get
		{
			return this._diet_meat;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06001B8C RID: 7052 RVA: 0x000FD212 File Offset: 0x000FB412
	public BaseStats base_stats_male
	{
		get
		{
			return this.nucleus.base_stats_male;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000FD21F File Offset: 0x000FB41F
	public BaseStats base_stats_female
	{
		get
		{
			return this.nucleus.base_stats_female;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06001B8E RID: 7054 RVA: 0x000FD22C File Offset: 0x000FB42C
	public bool needs_food
	{
		get
		{
			return this._needs_food;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000FD234 File Offset: 0x000FB434
	public bool needs_mate
	{
		get
		{
			return this._needs_mate;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000FD23C File Offset: 0x000FB43C
	public bool can_process_emotions
	{
		get
		{
			return this._can_process_emotions;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000FD244 File Offset: 0x000FB444
	public bool has_advanced_memory
	{
		get
		{
			return this._has_advanced_memory;
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000FD24C File Offset: 0x000FB44C
	public bool has_advanced_communication
	{
		get
		{
			return this._has_advanced_communication;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000FD254 File Offset: 0x000FB454
	public bool is_damaged_by_water
	{
		get
		{
			return this._damaged_by_water;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000FD25C File Offset: 0x000FB45C
	public bool has_egg_form
	{
		get
		{
			return this._has_egg_form;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000FD264 File Offset: 0x000FB464
	public string egg_id
	{
		get
		{
			return this._egg_id;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000FD26C File Offset: 0x000FB46C
	public string egg_sprite_path
	{
		get
		{
			return this._egg_asset.sprite_path;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000FD279 File Offset: 0x000FB479
	public SubspeciesTrait egg_asset
	{
		get
		{
			return this._egg_asset;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000FD281 File Offset: 0x000FB481
	public Sprite egg_sprite
	{
		get
		{
			return this._egg_asset.getSprite();
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000FD28E File Offset: 0x000FB48E
	public bool has_mutation_reskin
	{
		get
		{
			return this._has_mutation_reskin;
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000FD296 File Offset: 0x000FB496
	public SubspeciesTrait mutation_skin_asset
	{
		get
		{
			return this._mutation_skin_asset;
		}
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x000FD2A0 File Offset: 0x000FB4A0
	public string getRandomBioProduct()
	{
		string result;
		using (ListPool<string> tTempPool = new ListPool<string>())
		{
			if (base.hasTrait("bioproduct_gems"))
			{
				tTempPool.Add("mineral_gems");
			}
			if (base.hasTrait("bioproduct_stone"))
			{
				tTempPool.Add("mineral_stone");
			}
			if (base.hasTrait("bioproduct_mushrooms"))
			{
				tTempPool.Add("mushroom_red");
				tTempPool.Add("mushroom_green");
				tTempPool.Add("mushroom_teal");
				tTempPool.Add("mushroom_white");
				tTempPool.Add("mushroom_yellow");
			}
			if (base.hasTrait("bioproduct_gold"))
			{
				tTempPool.Add("mineral_gold");
			}
			if (tTempPool.Count == 0)
			{
				result = "poop";
			}
			else
			{
				result = tTempPool.GetRandom<string>();
			}
		}
		return result;
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x000FD374 File Offset: 0x000FB574
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "subspecies");
		this._mutation_skin_asset = null;
		this._cached_phenotype_index_for_banner = 0;
		this._phenotype_list_assets.Clear();
		this._phenotypes_set_indexes.Clear();
		this.base_stats.reset();
		this.nucleus.reset();
		this._actor_birth_traits.reset();
		this.spells.reset();
		this._egg_asset = null;
		base.Dispose();
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x000FD3EE File Offset: 0x000FB5EE
	public bool hasParentSubspecies()
	{
		return this.data.parent_subspecies.hasValue();
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x000FD400 File Offset: 0x000FB600
	public void unstableGenomeEvent()
	{
		this.nucleus.unstableGenomeEvent();
		this.genesChangedEvent();
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x000FD413 File Offset: 0x000FB613
	public void cacheCounters()
	{
		this._cached_females = this.countFemales();
		this._cached_males = this.countMales();
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x000FD42D File Offset: 0x000FB62D
	public void eventGMO()
	{
		this.addTrait("gmo", false);
		this._trait_changed_event = true;
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x000FD443 File Offset: 0x000FB643
	public float getMaturationTimeMonths()
	{
		return 0f + this.base_stats_meta["maturation"];
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x000FD45B File Offset: 0x000FB65B
	public override bool addTrait(SubspeciesTrait pTrait, bool pRemoveOpposites = false)
	{
		return this.canAddTrait(pTrait) && base.addTrait(pTrait, pRemoveOpposites);
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000FD470 File Offset: 0x000FB670
	public bool canAddTrait(SubspeciesTrait pTrait)
	{
		ActorAsset tSpeciesAsset = this.getActorAsset();
		return (tSpeciesAsset.trait_filter_subspecies == null || !tSpeciesAsset.trait_filter_subspecies.Contains(pTrait.id)) && (tSpeciesAsset.trait_group_filter_subspecies == null || !tSpeciesAsset.trait_group_filter_subspecies.Contains(pTrait.group_id));
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x000FD4BF File Offset: 0x000FB6BF
	public string getPossibleAttribute()
	{
		if (this.nucleus.pot_possible_attributes.Count == 0)
		{
			return null;
		}
		return this.nucleus.pot_possible_attributes.GetRandom<string>();
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x000FD4E8 File Offset: 0x000FB6E8
	public bool addBirthTrait(string pActorTraitID)
	{
		ActorTrait tActorTraitAsset = AssetManager.traits.get(pActorTraitID);
		return tActorTraitAsset != null && this._actor_birth_traits.addTrait(tActorTraitAsset, false);
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x000FD513 File Offset: 0x000FB713
	public SubspeciesActorBirthTraits getActorBirthTraits()
	{
		return this._actor_birth_traits;
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x000FD51C File Offset: 0x000FB71C
	public int countMainKingdoms()
	{
		int tResult = 0;
		using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getMainSubspecies() == this)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x000FD574 File Offset: 0x000FB774
	public bool hasPopulationLimit()
	{
		return this.base_stats_meta["limit_population"] > 0f;
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x000FD590 File Offset: 0x000FB790
	public bool hasReachedPopulationLimit()
	{
		int tPopLimit = (int)this.base_stats_meta["limit_population"];
		return tPopLimit != 0 && this.countUnits() >= tPopLimit;
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x000FD5C0 File Offset: 0x000FB7C0
	public int countMainCities()
	{
		int tResult = 0;
		using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getMainSubspecies() == this)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x000FD618 File Offset: 0x000FB818
	public Subspecies getSkeletonForm()
	{
		long tID = this.data.skeleton_form_id;
		Subspecies tSubspecies = World.world.subspecies.get(tID);
		if (tSubspecies.isRekt())
		{
			return null;
		}
		return tSubspecies;
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x000FD64D File Offset: 0x000FB84D
	public override void traitModifiedEvent()
	{
		this._trait_changed_event = true;
		base.traitModifiedEvent();
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x000FD65C File Offset: 0x000FB85C
	public void setSkeletonForm(Subspecies pSkeletonForm)
	{
		this.data.skeleton_form_id = pSkeletonForm.id;
		ActorAsset tSkeletonAsset = pSkeletonForm.getActorAsset();
		string tPrefix = "";
		if (tSkeletonAsset.generated_subspecies_names_prefixes != null)
		{
			tPrefix = tSkeletonAsset.generated_subspecies_names_prefixes.GetRandom<string>();
		}
		if (string.IsNullOrEmpty(tPrefix))
		{
			return;
		}
		string tName = tPrefix.FirstToUpper() + " " + this.name;
		pSkeletonForm.setName(tName, false);
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x000FD6C3 File Offset: 0x000FB8C3
	private void clearVisualCache()
	{
		this._cached_unit_sprite_for_banner = null;
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x000FD6CC File Offset: 0x000FB8CC
	public Sprite getUnitSpriteForBanner()
	{
		if (this._cached_unit_sprite_for_banner != null)
		{
			return this._cached_unit_sprite_for_banner;
		}
		ActorAsset tAsset = this.getActorAsset();
		SubspeciesTrait tMutationAsset = null;
		ActorTextureSubAsset tTextureAsset;
		if (this.has_mutation_reskin)
		{
			tMutationAsset = this.mutation_skin_asset;
			tTextureAsset = tMutationAsset.texture_asset;
		}
		else
		{
			tTextureAsset = tAsset.texture_asset;
		}
		AnimationContainerUnit tContainerAdult = DynamicActorSpriteCreatorUI.getContainerForUI(tAsset, true, tTextureAsset, tMutationAsset, false, null, null);
		ColorAsset tKingdomColor = AssetManager.kingdoms.get(tAsset.kingdom_id_wild).default_kingdom_color;
		int tPhenotypeIndex = this.getMainPhenotypeIndexForBanner();
		int tPhenotypeShade = 0;
		Sprite tSprite = DynamicActorSpriteCreatorUI.getUnitSpriteForUI(tAsset, tContainerAdult.walking.frames[0], tContainerAdult, true, ActorSex.Male, tPhenotypeIndex, tPhenotypeShade, tKingdomColor, 0L, 0, false, false, false, false);
		this._cached_unit_sprite_for_banner = tSprite;
		return tSprite;
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x000FD774 File Offset: 0x000FB974
	public override bool hasCities()
	{
		using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getMainSubspecies() == this)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x000FD7CC File Offset: 0x000FB9CC
	public override IEnumerable<City> getCities()
	{
		foreach (City tCity in World.world.cities)
		{
			if (tCity.getMainSubspecies() == this)
			{
				yield return tCity;
			}
		}
		IEnumerator<City> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000FD7DC File Offset: 0x000FB9DC
	public override bool hasKingdoms()
	{
		using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getMainSubspecies() == this)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x000FD834 File Offset: 0x000FBA34
	public override IEnumerable<Kingdom> getKingdoms()
	{
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (tKingdom.getMainSubspecies() == this)
			{
				yield return tKingdom;
			}
		}
		IEnumerator<Kingdom> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000FD844 File Offset: 0x000FBA44
	public void initReproductionCounters()
	{
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000FD846 File Offset: 0x000FBA46
	private RateCounter checkNewCounter(RateCounter pCounter, string pID)
	{
		if (pCounter == null)
		{
			pCounter = new RateCounter(pID, 60);
			this.list_counters.Add(pCounter);
		}
		pCounter.reset();
		return pCounter;
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x000FD868 File Offset: 0x000FBA68
	public void debugReproductionEvents(DebugTool pTool)
	{
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x000FD86A File Offset: 0x000FBA6A
	public void counterReproduction()
	{
		RateCounter rateCounter = this.counter_reproduction;
		if (rateCounter == null)
		{
			return;
		}
		rateCounter.registerEvent();
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000FD87C File Offset: 0x000FBA7C
	public void countReproductionNeuron()
	{
		RateCounter rateCounter = this.counter_reproduction_neuron;
		if (rateCounter == null)
		{
			return;
		}
		rateCounter.registerEvent();
	}

	// Token: 0x04001506 RID: 5382
	private const float AGE_THRESHOLD_ADULT = 30f;

	// Token: 0x04001507 RID: 5383
	private const float AGE_AGE_BREEDING_CIV = 18f;

	// Token: 0x04001508 RID: 5384
	private const float AGE_THRESHOLD_ADULT_CIV = 16f;

	// Token: 0x04001509 RID: 5385
	private const float AGE_MAX_ADULT = 16f;

	// Token: 0x0400150A RID: 5386
	private const float AGE_EXPONENTIAL_ADULT = 0.55f;

	// Token: 0x0400150B RID: 5387
	private const float AGE_MULTIPLIER_ADULT = 1.1f;

	// Token: 0x0400150C RID: 5388
	public readonly Nucleus nucleus = new Nucleus();

	// Token: 0x0400150D RID: 5389
	private int _cached_phenotype_index_for_banner;

	// Token: 0x0400150E RID: 5390
	private List<PhenotypeAsset> _phenotype_list_assets = new List<PhenotypeAsset>();

	// Token: 0x0400150F RID: 5391
	private HashSet<int> _phenotypes_set_indexes = new HashSet<int>();

	// Token: 0x04001510 RID: 5392
	private bool _has_egg_form;

	// Token: 0x04001511 RID: 5393
	private bool _has_mutation_reskin;

	// Token: 0x04001512 RID: 5394
	private bool _needs_food;

	// Token: 0x04001513 RID: 5395
	private bool _needs_mate;

	// Token: 0x04001514 RID: 5396
	private bool _can_process_emotions;

	// Token: 0x04001515 RID: 5397
	private bool _is_sapient;

	// Token: 0x04001516 RID: 5398
	private bool _has_advanced_memory;

	// Token: 0x04001517 RID: 5399
	private bool _has_advanced_communication;

	// Token: 0x04001518 RID: 5400
	private bool _damaged_by_water;

	// Token: 0x04001519 RID: 5401
	private bool _timid;

	// Token: 0x0400151A RID: 5402
	private bool _curious;

	// Token: 0x0400151B RID: 5403
	private bool _water_creature;

	// Token: 0x0400151C RID: 5404
	private bool _hovering;

	// Token: 0x0400151D RID: 5405
	private bool _pollinating;

	// Token: 0x0400151E RID: 5406
	private bool _magic;

	// Token: 0x0400151F RID: 5407
	private bool _diet_flowers;

	// Token: 0x04001520 RID: 5408
	private bool _diet_fruits;

	// Token: 0x04001521 RID: 5409
	private bool _diet_crops;

	// Token: 0x04001522 RID: 5410
	private bool _diet_vegetation;

	// Token: 0x04001523 RID: 5411
	private bool _diet_meat;

	// Token: 0x04001524 RID: 5412
	private bool _diet_blood;

	// Token: 0x04001525 RID: 5413
	private bool _diet_minerals;

	// Token: 0x04001526 RID: 5414
	private bool _diet_wood;

	// Token: 0x04001527 RID: 5415
	private bool _diet_cannibalism;

	// Token: 0x04001528 RID: 5416
	private int _cached_metabolic_rate;

	// Token: 0x04001529 RID: 5417
	private bool _cached_energy_preserver;

	// Token: 0x0400152A RID: 5418
	private int _cached_males;

	// Token: 0x0400152B RID: 5419
	private int _cached_females;

	// Token: 0x0400152C RID: 5420
	private string _egg_id;

	// Token: 0x0400152D RID: 5421
	private SubspeciesTrait _egg_asset;

	// Token: 0x0400152E RID: 5422
	private string _mutation_skin_id;

	// Token: 0x0400152F RID: 5423
	private SubspeciesTrait _mutation_skin_asset;

	// Token: 0x04001530 RID: 5424
	private string _cached_skin_male;

	// Token: 0x04001531 RID: 5425
	private string _cached_skin_female;

	// Token: 0x04001532 RID: 5426
	private string _cached_skin_warrior;

	// Token: 0x04001533 RID: 5427
	private readonly HashSet<string> _allowed_food_by_diet = new HashSet<string>();

	// Token: 0x04001534 RID: 5428
	private readonly SubspeciesActorBirthTraits _actor_birth_traits = new SubspeciesActorBirthTraits();

	// Token: 0x04001535 RID: 5429
	private bool _trait_changed_event;

	// Token: 0x04001536 RID: 5430
	private Sprite _cached_unit_sprite_for_banner;

	// Token: 0x04001537 RID: 5431
	private const string reproduction_neuron = "reproduction_neuron";

	// Token: 0x04001538 RID: 5432
	private const string reproduction_basics_1 = "reproduction_basics_1";

	// Token: 0x04001539 RID: 5433
	private const string reproduction_basics_2 = "reproduction_basics_2";

	// Token: 0x0400153A RID: 5434
	private const string reproduction_basics_3 = "reproduction_basics_3";

	// Token: 0x0400153B RID: 5435
	private const string reproduction_basics_4 = "reproduction_basics_4";

	// Token: 0x0400153C RID: 5436
	private const string reproduction_sexual_try = "reproduction_sexual_try";

	// Token: 0x0400153D RID: 5437
	private const string reproduction_acts = "reproduction_acts";

	// Token: 0x0400153E RID: 5438
	private const string reproduction = "reproduction";

	// Token: 0x0400153F RID: 5439
	private const string births = "births";

	// Token: 0x04001540 RID: 5440
	private const string new_adults = "new_adults";

	// Token: 0x04001541 RID: 5441
	public static string[] ALL_REPRODUCTION_COUNTERS = new string[]
	{
		"reproduction_neuron",
		"reproduction_basics_1",
		"reproduction_basics_2",
		"reproduction_basics_3",
		"reproduction_basics_4",
		"reproduction_sexual_try",
		"reproduction_acts",
		"reproduction",
		"births",
		"new_adults"
	};

	// Token: 0x04001542 RID: 5442
	public RateCounter counter_reproduction_neuron;

	// Token: 0x04001543 RID: 5443
	public RateCounter counter_reproduction_basics_1;

	// Token: 0x04001544 RID: 5444
	public RateCounter counter_reproduction_basics_2;

	// Token: 0x04001545 RID: 5445
	public RateCounter counter_reproduction_basics_3;

	// Token: 0x04001546 RID: 5446
	public RateCounter counter_reproduction_basics_4;

	// Token: 0x04001547 RID: 5447
	public RateCounter counter_reproduction_sexual_try;

	// Token: 0x04001548 RID: 5448
	public RateCounter counter_reproduction;

	// Token: 0x04001549 RID: 5449
	public RateCounter counter_reproduction_acts;

	// Token: 0x0400154A RID: 5450
	public RateCounter counter_births;

	// Token: 0x0400154B RID: 5451
	public RateCounter counter_new_adults;

	// Token: 0x0400154C RID: 5452
	public List<RateCounter> list_counters = new List<RateCounter>();
}
