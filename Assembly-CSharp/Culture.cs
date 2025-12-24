using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class Culture : MetaObjectWithTraits<CultureData, CultureTrait>
{
	// Token: 0x1700014B RID: 331
	// (get) Token: 0x0600168A RID: 5770 RVA: 0x000E35E4 File Offset: 0x000E17E4
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x0600168B RID: 5771 RVA: 0x000E35E7 File Offset: 0x000E17E7
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.cultures;
		}
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x000E35F4 File Offset: 0x000E17F4
	public void createCulture(Actor pActor, bool pAddDefaultTraits)
	{
		string tName = pActor.generateName(MetaType.Culture, this.getID(), ActorSex.None);
		base.setName(tName, true);
		BaseSystemData data = this.data;
		Culture culture = pActor.culture;
		data.name_culture_id = ((culture != null) ? culture.getID() : -1L);
		this.data.original_actor_asset = pActor.asset.id;
		CultureData data2 = this.data;
		City city = pActor.city;
		data2.creator_city_name = ((city != null) ? city.name : null);
		CultureData data3 = this.data;
		City city2 = pActor.city;
		data3.creator_city_id = ((city2 != null) ? city2.getID() : -1L);
		this.data.creator_name = pActor.getName();
		this.data.creator_id = pActor.getID();
		this.data.creator_species_id = pActor.asset.id;
		this.data.creator_subspecies_name = pActor.subspecies.name;
		this.data.creator_subspecies_id = pActor.subspecies.getID();
		CultureData data4 = this.data;
		Clan clan = pActor.clan;
		data4.creator_clan_name = ((clan != null) ? clan.name : null);
		CultureData data5 = this.data;
		Clan clan2 = pActor.clan;
		data5.creator_clan_id = ((clan2 != null) ? clan2.getID() : -1L);
		CultureData data6 = this.data;
		Kingdom kingdom = pActor.kingdom;
		data6.creator_kingdom_id = ((kingdom != null) ? kingdom.getID() : -1L);
		CultureData data7 = this.data;
		Kingdom kingdom2 = pActor.kingdom;
		data7.creator_kingdom_name = ((kingdom2 != null) ? kingdom2.name : null);
		this.data.name_template_set = pActor.asset.name_template_sets.GetRandom<string>();
		this.books.setMeta(this, null, null);
		this.generateNewMetaObject(pAddDefaultTraits);
		base.setDirty();
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x000E3798 File Offset: 0x000E1998
	public void cloneAndEvolveOnomastics(Culture pFrom)
	{
		CultureData tCultureData = pFrom.data;
		this.data.parent_culture_id = pFrom.id;
		this.data.name_template_set = tCultureData.name_template_set;
		pFrom.loadAllOnomasticsData();
		this._onomastics_data.Clear();
		foreach (KeyValuePair<MetaType, OnomasticsData> tPair in pFrom._onomastics_data)
		{
			OnomasticsData tCopy = new OnomasticsData();
			tCopy.loadFromShortTemplate(tPair.Value.getShortTemplate());
			OnomasticsEvolver.scramble(tCopy);
			this._onomastics_data.Add(tPair.Key, tCopy);
		}
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000E3850 File Offset: 0x000E1A50
	protected override void recalcBaseStats()
	{
		base.recalcBaseStats();
		this.recalcPreferredWeapons();
		this.recalcTownPlanTraits();
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000E3864 File Offset: 0x000E1A64
	private void recalcTownPlanTraits()
	{
		this._traits_town_plan_zones.Clear();
		foreach (CultureTrait tTrait in base.getTraits())
		{
			if (tTrait.town_layout_plan)
			{
				this._traits_town_plan_zones.Add(tTrait);
			}
		}
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x000E38CC File Offset: 0x000E1ACC
	public void countConversion()
	{
		base.addRenown(1);
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06001691 RID: 5777 RVA: 0x000E38D5 File Offset: 0x000E1AD5
	protected override AssetLibrary<CultureTrait> trait_library
	{
		get
		{
			return AssetManager.culture_traits;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06001692 RID: 5778 RVA: 0x000E38DC File Offset: 0x000E1ADC
	protected override List<string> default_traits
	{
		get
		{
			return this.getActorAsset().default_culture_traits;
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06001693 RID: 5779 RVA: 0x000E38E9 File Offset: 0x000E1AE9
	protected override List<string> saved_traits
	{
		get
		{
			return this.data.saved_traits;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x000E38F6 File Offset: 0x000E1AF6
	protected override string species_id
	{
		get
		{
			return this.data.original_actor_asset;
		}
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x000E3903 File Offset: 0x000E1B03
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x000E390B File Offset: 0x000E1B0B
	public override void increaseBirths()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x000E391D File Offset: 0x000E1B1D
	public int countCities()
	{
		return this.cities.Count;
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x000E392A File Offset: 0x000E1B2A
	public int countKingdoms()
	{
		return this.kingdoms.Count;
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x000E3937 File Offset: 0x000E1B37
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.culture_colors_library;
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x000E393E File Offset: 0x000E1B3E
	public bool canUseRoads()
	{
		return base.hasTrait("roads");
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x000E3950 File Offset: 0x000E1B50
	public void testDebugNewBook()
	{
		if (base.units.Count == 0)
		{
			return;
		}
		Actor tActor = base.units.GetRandom<Actor>();
		if (tActor.getCity() == null)
		{
			return;
		}
		if (!tActor.city.hasBookSlots())
		{
			return;
		}
		World.world.books.generateNewBook(tActor);
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x000E39A0 File Offset: 0x000E1BA0
	private void recalcPreferredWeapons()
	{
		this._preferred_weapons_craft_subtypes.Clear();
		this._preferred_weapons_craft_assets.Clear();
		foreach (CultureTrait tTrait in base.getTraits())
		{
			if (tTrait.is_weapon_trait)
			{
				if (tTrait.related_weapon_subtype_ids != null)
				{
					foreach (string tSubtypeID in tTrait.related_weapon_subtype_ids)
					{
						this._preferred_weapons_craft_subtypes.Add(tSubtypeID);
					}
				}
				if (tTrait.related_weapons_ids != null)
				{
					foreach (string tWeaponID in tTrait.related_weapons_ids)
					{
						EquipmentAsset tAsset = AssetManager.items.get(tWeaponID);
						if (tAsset != null)
						{
							this._preferred_weapons_craft_assets.Add(tAsset);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x000E3AC0 File Offset: 0x000E1CC0
	public bool hasPreferredWeaponsToCraft()
	{
		return this._preferred_weapons_craft_assets.Count > 0;
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x000E3AD0 File Offset: 0x000E1CD0
	public List<EquipmentAsset> getPreferredWeaponAssets()
	{
		return this._preferred_weapons_craft_assets;
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x000E3AD8 File Offset: 0x000E1CD8
	public string getPreferredWeaponSubtypeIDs()
	{
		if (this._preferred_weapons_craft_subtypes.Count == 0)
		{
			return null;
		}
		return this._preferred_weapons_craft_subtypes.GetRandom<string>();
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x000E3AF4 File Offset: 0x000E1CF4
	public float chanceToGiveTraits()
	{
		return 0.5f;
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x000E3AFB File Offset: 0x000E1CFB
	public void clearListCities()
	{
		this.cities.Clear();
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x000E3B08 File Offset: 0x000E1D08
	public void clearListKingdoms()
	{
		this.kingdoms.Clear();
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x000E3B15 File Offset: 0x000E1D15
	public void listCity(City pCity)
	{
		this.cities.Add(pCity);
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x000E3B23 File Offset: 0x000E1D23
	public void listKingdom(Kingdom pKingdom)
	{
		this.kingdoms.Add(pKingdom);
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x000E3B31 File Offset: 0x000E1D31
	public override void generateBanner()
	{
		this.data.banner_decor_id = AssetManager.culture_banners_library.getNewIndexBackground();
		this.data.banner_element_id = AssetManager.culture_banners_library.getNewIndexIcon();
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x000E3B60 File Offset: 0x000E1D60
	public override void save()
	{
		base.save();
		this.data.saved_traits = base.getTraitsAsStrings();
		if (this._onomastics_data.Count > 0)
		{
			this.data.onomastics = new Dictionary<MetaType, string>(this._onomastics_data.Count);
			using (Dictionary<MetaType, OnomasticsData>.KeyCollection.Enumerator enumerator = this._onomastics_data.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MetaType tType = enumerator.Current;
					OnomasticsData tData = this._onomastics_data[tType];
					this.data.onomastics[tType] = tData.getShortTemplate();
				}
				return;
			}
		}
		Dictionary<MetaType, string> onomastics = this.data.onomastics;
		if (onomastics != null)
		{
			onomastics.Clear();
		}
		this.data.onomastics = null;
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000E3C38 File Offset: 0x000E1E38
	public override void loadData(CultureData pData)
	{
		base.loadData(pData);
		if (!string.IsNullOrEmpty(this.data.name_template_set) && !AssetManager.name_sets.has(this.data.name_template_set))
		{
			this.data.name_template_set = null;
			this._name_set_asset = null;
		}
		this.books.setDirty();
		this.books.setMeta(this, null, null);
		this._onomastics_data.Clear();
		if (pData.onomastics != null)
		{
			foreach (KeyValuePair<MetaType, string> tPair in pData.onomastics)
			{
				OnomasticsData tData = new OnomasticsData();
				tData.loadFromShortTemplate(tPair.Value);
				this._onomastics_data.Add(tPair.Key, tData);
			}
		}
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x000E3D1C File Offset: 0x000E1F1C
	public override void updateDirty()
	{
		foreach (City city in this.cities)
		{
			city.setStatusDirty();
		}
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000E3D6C File Offset: 0x000E1F6C
	public void debug(DebugTool pTool)
	{
		pTool.setText("id:", base.id, 0f, false, 0L, false, false, "");
		pTool.setText("name:", this.name, 0f, false, 0L, false, false, "");
		pTool.setText("followers:", this.countUnits(), 0f, false, 0L, false, false, "");
		pTool.setText("cities:", this.countCities(), 0f, false, 0L, false, false, "");
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000E3E08 File Offset: 0x000E2008
	internal void updateTitleCenter()
	{
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000E3E0A File Offset: 0x000E200A
	public Sprite getElementSprite()
	{
		return AssetManager.culture_banners_library.getSpriteIcon(this.data.banner_element_id);
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000E3E21 File Offset: 0x000E2021
	public Sprite getDecorSprite()
	{
		return AssetManager.culture_banners_library.getSpriteBackground(this.data.banner_decor_id);
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000E3E38 File Offset: 0x000E2038
	public List<long> getBooks()
	{
		return this.books.getList();
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000E3E45 File Offset: 0x000E2045
	public override bool isReadyForRemoval()
	{
		return !this.books.hasBooks() && base.isReadyForRemoval();
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000E3E5C File Offset: 0x000E205C
	public override void convertSameSpeciesAroundUnit(Actor pActorMain, bool pOverrideExisting = false)
	{
		foreach (Actor tActor in base.getUnitFromChunkForConversion(pActorMain))
		{
			if (pOverrideExisting || !tActor.hasCulture())
			{
				tActor.setCulture(this);
			}
		}
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000E3EB8 File Offset: 0x000E20B8
	public override void forceConvertSameSpeciesAroundUnit(Actor pActorMain)
	{
		this.convertSameSpeciesAroundUnit(pActorMain, true);
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x000E3EC4 File Offset: 0x000E20C4
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "culture");
		this.books.clear();
		this._preferred_weapons_craft_subtypes.Clear();
		this._preferred_weapons_craft_assets.Clear();
		this.cities.Clear();
		this.kingdoms.Clear();
		this._traits_town_plan_zones.Clear();
		this._name_set_asset = null;
		this._onomastics_data.Clear();
		base.Dispose();
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x000E3F3C File Offset: 0x000E213C
	public string getNameTemplate(MetaType pType)
	{
		if (this._name_set_asset == null)
		{
			if (string.IsNullOrEmpty(this.data.name_template_set))
			{
				this.data.name_template_set = this.getActorAsset().name_template_sets.GetRandom<string>();
			}
			if (string.IsNullOrEmpty(this.data.name_template_set))
			{
				this._name_set_asset = null;
				return AssetManager.name_generator.get(this.getActorAsset().getNameTemplate(pType)).id;
			}
		}
		this._name_set_asset = AssetManager.name_sets.get(this.data.name_template_set);
		return this._name_set_asset.get(pType);
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000E3FDC File Offset: 0x000E21DC
	public void loadAllOnomasticsData()
	{
		foreach (MetaType tType in NameSetAsset.getTypes())
		{
			this.getOnomasticData(tType, false);
		}
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000E402C File Offset: 0x000E222C
	public OnomasticsData getOnomasticData(MetaType pType, bool pReset = false)
	{
		OnomasticsData tData;
		if (!this._onomastics_data.TryGetValue(pType, out tData) || pReset)
		{
			if (tData == null)
			{
				tData = new OnomasticsData();
			}
			else
			{
				tData.clearTemplateData();
			}
			string tNameTemplate = this.getNameTemplate(pType);
			if (!string.IsNullOrEmpty(tNameTemplate))
			{
				NameGeneratorAsset tNameGeneratorAsset = AssetManager.name_generator.get(tNameTemplate);
				if (tNameGeneratorAsset.hasOnomastics())
				{
					OnomasticsData tOriginalData = OnomasticsCache.getOriginalData(tNameGeneratorAsset.onomastics_templates.GetRandom<string>());
					tData.cloneFrom(tOriginalData);
				}
				else
				{
					Debug.Log(string.Format("no onomastics data found for {0} for {1}", tNameGeneratorAsset.id, pType));
				}
			}
			if (tData.isEmpty())
			{
				Debug.Log("name set asset " + this._name_set_asset.id + " doesn't have " + pType.ToString());
				Debug.Log("loading from actor");
				string tActorNameTemplate = this.getActorAsset().getNameTemplate(pType);
				NameGeneratorAsset tNameGeneratorAsset2 = AssetManager.name_generator.get(tActorNameTemplate);
				if (tNameGeneratorAsset2.hasOnomastics())
				{
					OnomasticsData tOriginalData2 = OnomasticsCache.getOriginalData(tNameGeneratorAsset2.onomastics_templates.GetRandom<string>());
					tData.cloneFrom(tOriginalData2);
				}
				else
				{
					Debug.Log(string.Format("no onomastics data found for {0} for {1}", tNameGeneratorAsset2.id, pType));
				}
			}
			if (tData.isEmpty())
			{
				tData.setDebugTest();
				Debug.Log(string.Format("no onomastics data found for {0} for {1}, defaulting", this._name_set_asset.id, pType));
			}
			this._onomastics_data[pType] = tData;
		}
		return tData;
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x000E4198 File Offset: 0x000E2398
	public bool planAllowsToPlaceBuildingInZone(TileZone pZone, TileZone pCenterZone)
	{
		using (List<CultureTrait>.Enumerator enumerator = this._traits_town_plan_zones.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.passable_zone_checker(pZone, pCenterZone))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x000E41F8 File Offset: 0x000E23F8
	public bool hasSpecialTownPlans()
	{
		return this._traits_town_plan_zones.Count > 0;
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x000E4208 File Offset: 0x000E2408
	public bool hasTrueRoots()
	{
		return base.hasTrait("true_roots");
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x000E4215 File Offset: 0x000E2415
	public bool isPossibleToConvertToOtherMeta()
	{
		return !this.hasTrueRoots();
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000E4220 File Offset: 0x000E2420
	public override bool hasCities()
	{
		return this.cities.Count > 0;
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000E4230 File Offset: 0x000E2430
	public override IEnumerable<City> getCities()
	{
		return this.cities;
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x000E4238 File Offset: 0x000E2438
	public override bool hasKingdoms()
	{
		return this.kingdoms.Count > 0;
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x000E4248 File Offset: 0x000E2448
	public override IEnumerable<Kingdom> getKingdoms()
	{
		return this.kingdoms;
	}

	// Token: 0x040012AE RID: 4782
	public const int MAX_LEVEL = 5;

	// Token: 0x040012AF RID: 4783
	public readonly List<City> cities = new List<City>();

	// Token: 0x040012B0 RID: 4784
	public readonly List<Kingdom> kingdoms = new List<Kingdom>();

	// Token: 0x040012B1 RID: 4785
	public readonly BooksHandler books = new BooksHandler();

	// Token: 0x040012B2 RID: 4786
	private readonly List<string> _preferred_weapons_craft_subtypes = new List<string>();

	// Token: 0x040012B3 RID: 4787
	private readonly List<EquipmentAsset> _preferred_weapons_craft_assets = new List<EquipmentAsset>();

	// Token: 0x040012B4 RID: 4788
	private NameSetAsset _name_set_asset;

	// Token: 0x040012B5 RID: 4789
	private readonly List<CultureTrait> _traits_town_plan_zones = new List<CultureTrait>();

	// Token: 0x040012B6 RID: 4790
	private readonly Dictionary<MetaType, OnomasticsData> _onomastics_data = new Dictionary<MetaType, OnomasticsData>();
}
