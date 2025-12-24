using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class Religion : MetaObjectWithTraits<ReligionData, ReligionTrait>
{
	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000FB442 File Offset: 0x000F9642
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000FB445 File Offset: 0x000F9645
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.religions;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000FB451 File Offset: 0x000F9651
	public bool is_magic_only_clan_members
	{
		get
		{
			return this._has_bloodline_bond;
		}
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x000FB45C File Offset: 0x000F965C
	public void newReligion(Actor pActor, WorldTile pTile, bool pAddDefaultTraits)
	{
		this.data.creator_name = pActor.getName();
		this.data.creator_id = pActor.getID();
		this.data.creator_species_id = pActor.asset.id;
		this.data.creator_subspecies_name = pActor.subspecies.name;
		this.data.creator_subspecies_id = pActor.subspecies.getID();
		ReligionData data = this.data;
		Clan clan = pActor.clan;
		data.creator_clan_id = ((clan != null) ? clan.id : -1L);
		ReligionData data2 = this.data;
		Clan clan2 = pActor.clan;
		data2.creator_clan_name = ((clan2 != null) ? clan2.name : null);
		this.data.creator_kingdom_id = pActor.kingdom.getID();
		this.data.creator_kingdom_name = pActor.kingdom.name;
		ReligionData data3 = this.data;
		City city = pActor.city;
		data3.creator_city_name = ((city != null) ? city.name : null);
		ReligionData data4 = this.data;
		City city2 = pActor.city;
		data4.creator_city_id = ((city2 != null) ? city2.getID() : -1L);
		this.generateNewMetaObject(pAddDefaultTraits);
		this.generateName(pActor);
		this.books.setMeta(null, null, this);
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x000FB58B File Offset: 0x000F978B
	public override bool isReadyForRemoval()
	{
		return !this.books.hasBooks() && base.isReadyForRemoval();
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x000FB5A2 File Offset: 0x000F97A2
	public override void loadData(ReligionData pData)
	{
		base.loadData(pData);
		this.books.setDirty();
		this.books.setMeta(null, null, this);
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x000FB5C4 File Offset: 0x000F97C4
	protected override void recalcBaseStats()
	{
		base.recalcBaseStats();
		this._has_bloodline_bond = base.hasTrait("bloodline_bond");
		this.possible_rites.Clear();
		foreach (ReligionTrait tTrait in base.getTraits())
		{
			if (tTrait.hasPlotAsset())
			{
				this.possible_rites.Add(tTrait.plot_asset);
			}
		}
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x000FB648 File Offset: 0x000F9848
	public void countConversion()
	{
		base.addRenown(1);
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x000FB651 File Offset: 0x000F9851
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06001B04 RID: 6916 RVA: 0x000FB659 File Offset: 0x000F9859
	protected override List<string> default_traits
	{
		get
		{
			return this.getActorAsset().default_religion_traits;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000FB666 File Offset: 0x000F9866
	protected override AssetLibrary<ReligionTrait> trait_library
	{
		get
		{
			return AssetManager.religion_traits;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000FB66D File Offset: 0x000F986D
	protected override List<string> saved_traits
	{
		get
		{
			return this.data.saved_traits;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06001B07 RID: 6919 RVA: 0x000FB67A File Offset: 0x000F987A
	protected override string species_id
	{
		get
		{
			return this.data.creator_species_id;
		}
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000FB687 File Offset: 0x000F9887
	public override void increaseBirths()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x000FB699 File Offset: 0x000F9899
	public override void save()
	{
		base.save();
		this.data.saved_traits = base.getTraitsAsStrings();
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x000FB6B2 File Offset: 0x000F98B2
	public override void generateBanner()
	{
		this.data.banner_background_id = AssetManager.religion_banners_library.getNewIndexBackground();
		this.data.banner_icon_id = AssetManager.religion_banners_library.getNewIndexIcon();
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x000FB6DE File Offset: 0x000F98DE
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.religion_colors_library;
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x000FB6E5 File Offset: 0x000F98E5
	public void listCity(City pCity)
	{
		this.cities.Add(pCity);
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x000FB6F3 File Offset: 0x000F98F3
	public void listKingdom(Kingdom pKingdom)
	{
		this.kingdoms.Add(pKingdom);
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x000FB701 File Offset: 0x000F9901
	public void clearListCities()
	{
		this.cities.Clear();
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x000FB70E File Offset: 0x000F990E
	public void clearListKingdoms()
	{
		this.kingdoms.Clear();
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000FB71C File Offset: 0x000F991C
	private void generateName(Actor pActor)
	{
		string tName = pActor.generateName(MetaType.Religion, this.getID(), ActorSex.None);
		base.setName(tName, true);
		BaseSystemData data = this.data;
		Culture culture = pActor.culture;
		data.name_culture_id = ((culture != null) ? culture.getID() : -1L);
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000FB75E File Offset: 0x000F995E
	public Sprite getBackgroundSprite()
	{
		return AssetManager.religion_banners_library.getSpriteBackground(this.data.banner_background_id);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000FB775 File Offset: 0x000F9975
	public Sprite getIconSprite()
	{
		return AssetManager.religion_banners_library.getSpriteIcon(this.data.banner_icon_id);
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x000FB78C File Offset: 0x000F998C
	public int countCities()
	{
		return this.cities.Count;
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x000FB799 File Offset: 0x000F9999
	public int countKingdoms()
	{
		return this.kingdoms.Count;
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x000FB7A8 File Offset: 0x000F99A8
	public override void convertSameSpeciesAroundUnit(Actor pActorMain, bool pOverrideExisting = false)
	{
		foreach (Actor tActor in base.getUnitFromChunkForConversion(pActorMain))
		{
			if (pOverrideExisting || !tActor.hasReligion())
			{
				tActor.setReligion(this);
			}
		}
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x000FB804 File Offset: 0x000F9A04
	public override void forceConvertSameSpeciesAroundUnit(Actor pActorMain)
	{
		this.convertSameSpeciesAroundUnit(pActorMain, true);
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x000FB80E File Offset: 0x000F9A0E
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "religion");
		this.books.clear();
		this.cities.Clear();
		this.kingdoms.Clear();
		base.Dispose();
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x000FB847 File Offset: 0x000F9A47
	public override bool hasCities()
	{
		return this.cities.Count > 0;
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x000FB857 File Offset: 0x000F9A57
	public override IEnumerable<City> getCities()
	{
		return this.cities;
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x000FB85F File Offset: 0x000F9A5F
	public override bool hasKingdoms()
	{
		return this.kingdoms.Count > 0;
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x000FB86F File Offset: 0x000F9A6F
	public override IEnumerable<Kingdom> getKingdoms()
	{
		return this.kingdoms;
	}

	// Token: 0x040014ED RID: 5357
	public readonly List<City> cities = new List<City>();

	// Token: 0x040014EE RID: 5358
	public readonly List<Kingdom> kingdoms = new List<Kingdom>();

	// Token: 0x040014EF RID: 5359
	public BooksHandler books = new BooksHandler();

	// Token: 0x040014F0 RID: 5360
	public List<PlotAsset> possible_rites = new List<PlotAsset>();

	// Token: 0x040014F1 RID: 5361
	private bool _has_bloodline_bond;
}
