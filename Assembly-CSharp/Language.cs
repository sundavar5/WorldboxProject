using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class Language : MetaObjectWithTraits<LanguageData, LanguageTrait>
{
	// Token: 0x17000182 RID: 386
	// (get) Token: 0x060018BF RID: 6335 RVA: 0x000EC19F File Offset: 0x000EA39F
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000EC1A2 File Offset: 0x000EA3A2
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.languages;
		}
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000EC1B0 File Offset: 0x000EA3B0
	public void newLanguage(Actor pActor, bool pAddDefaultTraits)
	{
		this.generateName(pActor);
		this.data.creator_name = pActor.getName();
		this.data.creator_id = pActor.getID();
		this.data.creator_species_id = pActor.asset.id;
		this.data.creator_subspecies_name = pActor.subspecies.name;
		this.data.creator_subspecies_id = pActor.subspecies.getID();
		this.data.creator_kingdom_id = pActor.kingdom.getID();
		this.data.creator_kingdom_name = pActor.kingdom.data.name;
		LanguageData data = this.data;
		Clan clan = pActor.clan;
		data.creator_clan_id = ((clan != null) ? clan.getID() : -1L);
		LanguageData data2 = this.data;
		Clan clan2 = pActor.clan;
		data2.creator_clan_name = ((clan2 != null) ? clan2.data.name : null);
		LanguageData data3 = this.data;
		City city = pActor.city;
		data3.creator_city_id = ((city != null) ? city.getID() : -1L);
		LanguageData data4 = this.data;
		City city2 = pActor.city;
		data4.creator_city_name = ((city2 != null) ? city2.name : null);
		this.generateNewMetaObject(pAddDefaultTraits);
		this.books.setMeta(null, this, null);
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x000EC2E9 File Offset: 0x000EA4E9
	public void countNewSpeaker()
	{
		this.increaseNewSpeakers();
		base.addRenown(1);
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000EC2F8 File Offset: 0x000EA4F8
	public void countConversion()
	{
		this.increaseConvertedSpeakers();
		base.addRenown(2);
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x000EC307 File Offset: 0x000EA507
	public override void save()
	{
		base.save();
		this.data.saved_traits = base.getTraitsAsStrings();
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000EC320 File Offset: 0x000EA520
	protected override List<string> default_traits
	{
		get
		{
			return this.getActorAsset().default_language_traits;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060018C6 RID: 6342 RVA: 0x000EC32D File Offset: 0x000EA52D
	protected override AssetLibrary<LanguageTrait> trait_library
	{
		get
		{
			return AssetManager.language_traits;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000EC334 File Offset: 0x000EA534
	protected override List<string> saved_traits
	{
		get
		{
			return this.data.saved_traits;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060018C8 RID: 6344 RVA: 0x000EC341 File Offset: 0x000EA541
	protected override string species_id
	{
		get
		{
			return this.data.creator_species_id;
		}
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x000EC34E File Offset: 0x000EA54E
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x060018CA RID: 6346 RVA: 0x000EC356 File Offset: 0x000EA556
	public override void increaseBirths()
	{
		new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x000EC369 File Offset: 0x000EA569
	public override void generateBanner()
	{
		this.data.banner_icon_id = AssetManager.language_banners_library.getNewIndexIcon();
		this.data.banner_background_id = AssetManager.language_banners_library.getNewIndexBackground();
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x000EC395 File Offset: 0x000EA595
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.languages_colors_library;
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x000EC39C File Offset: 0x000EA59C
	public void listCity(City pCity)
	{
		this.cities.Add(pCity);
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x000EC3AA File Offset: 0x000EA5AA
	public void listKingdom(Kingdom pKingdom)
	{
		this.kingdoms.Add(pKingdom);
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x000EC3B8 File Offset: 0x000EA5B8
	public void clearListCities()
	{
		this.cities.Clear();
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x000EC3C5 File Offset: 0x000EA5C5
	public void clearListKingdoms()
	{
		this.kingdoms.Clear();
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x000EC3D4 File Offset: 0x000EA5D4
	private void generateName(Actor pActor)
	{
		string tName = pActor.generateName(MetaType.Language, this.getID(), ActorSex.None);
		base.setName(tName, true);
		BaseSystemData data = this.data;
		Culture culture = pActor.culture;
		data.name_culture_id = ((culture != null) ? culture.getID() : -1L);
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x000EC416 File Offset: 0x000EA616
	public Sprite getBackgroundSprite()
	{
		return AssetManager.language_banners_library.getSpriteBackground(this.data.banner_background_id);
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x000EC42D File Offset: 0x000EA62D
	public Sprite getIconSprite()
	{
		return AssetManager.language_banners_library.getSpriteIcon(this.data.banner_icon_id);
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x000EC444 File Offset: 0x000EA644
	public override void loadData(LanguageData pData)
	{
		base.loadData(pData);
		this.books.setMeta(null, this, null);
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x000EC45B File Offset: 0x000EA65B
	public int countWrittenBooks()
	{
		return this.data.books_written;
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x000EC468 File Offset: 0x000EA668
	public int countCities()
	{
		return this.cities.Count;
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x000EC475 File Offset: 0x000EA675
	public int countKingdoms()
	{
		return this.kingdoms.Count;
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x000EC482 File Offset: 0x000EA682
	public int getSpeakersNew()
	{
		return this.data.speakers_new;
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x000EC48F File Offset: 0x000EA68F
	public int getSpeakersLost()
	{
		return this.data.speakers_lost;
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x000EC49C File Offset: 0x000EA69C
	public int getSpeakersConverted()
	{
		return this.data.speakers_converted;
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x000EC4A9 File Offset: 0x000EA6A9
	public void increaseConvertedSpeakers()
	{
		this.data.speakers_converted++;
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x000EC4BE File Offset: 0x000EA6BE
	public void increaseNewSpeakers()
	{
		this.data.speakers_new++;
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x000EC4D3 File Offset: 0x000EA6D3
	public void increaseSpeakersLost()
	{
		this.data.speakers_lost++;
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x000EC4E8 File Offset: 0x000EA6E8
	public override bool isReadyForRemoval()
	{
		return !this.books.hasBooks() && base.isReadyForRemoval();
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x000EC500 File Offset: 0x000EA700
	public override void convertSameSpeciesAroundUnit(Actor pActorMain, bool pOverrideExisting = false)
	{
		foreach (Actor tActor in base.getUnitFromChunkForConversion(pActorMain))
		{
			if (pOverrideExisting || !tActor.hasLanguage())
			{
				tActor.joinLanguage(this);
			}
		}
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x000EC55C File Offset: 0x000EA75C
	public override void forceConvertSameSpeciesAroundUnit(Actor pActorMain)
	{
		this.convertSameSpeciesAroundUnit(pActorMain, true);
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x000EC566 File Offset: 0x000EA766
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "language");
		this.books.clear();
		this.cities.Clear();
		this.kingdoms.Clear();
		base.Dispose();
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x000EC59F File Offset: 0x000EA79F
	public override bool hasCities()
	{
		return this.cities.Count > 0;
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x000EC5AF File Offset: 0x000EA7AF
	public override IEnumerable<City> getCities()
	{
		return this.cities;
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x000EC5B7 File Offset: 0x000EA7B7
	public override bool hasKingdoms()
	{
		return this.kingdoms.Count > 0;
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x000EC5C7 File Offset: 0x000EA7C7
	public override IEnumerable<Kingdom> getKingdoms()
	{
		return this.kingdoms;
	}

	// Token: 0x04001377 RID: 4983
	public readonly List<City> cities = new List<City>();

	// Token: 0x04001378 RID: 4984
	public readonly List<Kingdom> kingdoms = new List<Kingdom>();

	// Token: 0x04001379 RID: 4985
	public readonly BooksHandler books = new BooksHandler();
}
